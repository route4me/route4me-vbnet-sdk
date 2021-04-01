Imports Quobject.SocketIoClientDotNet.Client
Imports System.Net
Imports System.Threading
Imports System.Collections.Immutable
Imports Quobject.SocketIoClientDotNet.EngineIoClientDotNet
Imports Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client.Transports
Imports IO = Quobject.SocketIoClientDotNet.Client.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Newtonsoft.Json.Serialization

Namespace Route4MeSDK.FastProcessing
    Public Class FastBulkGeocoding
        Inherits Connection

        Private manualResetEvent As ManualResetEvent = Nothing
        Private mainResetEvent As ManualResetEvent = Nothing
        Private socket As Socket
        Public Message As String
        Private Number As Integer
        Private Flag As Boolean
        Public Shared con As Connection = New Connection()
        Private requestedAddresses As Nullable(Of Integer)
        Private nextDownloadStage As Integer
        Private loadedAddressesCount As Integer
        Private TEMPORARY_ADDRESSES_STORAGE_ID As String
        Private fileReading As FastFileReading
        Private largeJsonFileProcessingIsDone As Boolean
        Private geocodedAddressesDownloadingIsDone As Boolean
        Private savedAddresses As List(Of AddressGeocoded)
        Private jsSer As JsonSerializer = New JsonSerializer()
        Public Property _apiKey As String

        Public Sub New(ByVal ApiKey As String, Optional ByVal EnableTraceSource As Boolean = False)
            If ApiKey <> "" Then _apiKey = ApiKey
            Quobject.SocketIoClientDotNet.TraceSourceTools.LogTraceSource.TraceSourceLogging(EnableTraceSource)
        End Sub

        Public Event AddressesChunkGeocoded As EventHandler(Of AddressesChunkGeocodedArgs)

        Protected Overridable Sub OnAddressesChunkGeocoded(ByVal e As AddressesChunkGeocodedArgs)
            'Dim handler As AddressesChunkGeocodedEventHandler = Nothing
            'AddHandler AddressesChunkGeocoded, AddressOf handler.Invoke
            'Dim handler As EventHandler(Of AddressesChunkGeocodedArgs) = AddressesChunkGeocoded

            RaiseEvent AddressesChunkGeocoded(Me, e)
        End Sub

        Public Class AddressesChunkGeocodedArgs
            Inherits EventArgs

            Public Property lsAddressesChunkGeocoded As List(Of AddressGeocoded)
        End Class

        Public Delegate Sub AddressesChunkGeocodedEventHandler(ByVal sender As Object, ByVal e As AddressesChunkGeocodedArgs)

        Public Event GeocodingIsFinished As EventHandler(Of GeocodingIsFinishedArgs)

        Protected Overridable Sub OnGeocodingIsFinished(ByVal e As GeocodingIsFinishedArgs)
            'Dim handler As EventHandler(Of GeocodingIsFinishedArgs) = GeocodingIsFinished
            'RaiseEvent handler(Me, e)
            RaiseEvent GeocodingIsFinished(Me, e)
        End Sub

        Public Class GeocodingIsFinishedArgs
            Inherits EventArgs

            Public Property isFinished As Boolean
        End Class

        Public Delegate Sub GeocodingIsFinishedEventHandler(ByVal sender As Object, ByVal e As AddressesChunkGeocodedArgs)

        Public Sub uploadAndGeocodeLargeJsonFile(ByVal fileName As String)
            Dim route4Me As Route4MeManager = New Route4MeManager(_apiKey)

            largeJsonFileProcessingIsDone = False
            fileReading = New FastFileReading()
            fileReading.jsonObjectsChunkSize = 200
            savedAddresses = New List(Of AddressGeocoded)()

            'fileReading.JsonFileChunkIsReady += AddressOf FileReading_JsonFileChunkIsReady
            AddHandler fileReading.JsonFileChunkIsReady, AddressOf FileReading_JsonFileChunkIsReady

            'fileReading.JsonFileReadingIsDone += AddressOf FileReading_JsonFileReadingIsDone
            AddHandler fileReading.JsonFileReadingIsDone, AddressOf FileReading_JsonFileReadingIsDone

            mainResetEvent = New ManualResetEvent(False)
            fileReading.readingChunksFromLargeJsonFile(fileName)
        End Sub

        Private Sub FileReading_JsonFileReadingIsDone(ByVal sender As Object, ByVal e As FastFileReading.JsonFileReadingIsDoneArgs)
            Dim isDone As Boolean = e.IsDone

            If isDone Then
                largeJsonFileProcessingIsDone = True
                mainResetEvent.[Set]()

                If geocodedAddressesDownloadingIsDone Then
                    OnGeocodingIsFinished(New GeocodingIsFinishedArgs() With {
                        .isFinished = True
                    })
                End If
            End If
        End Sub

        Private Sub FileReading_JsonFileChunkIsReady(ByVal sender As Object, ByVal e As FastFileReading.JsonFileChunkIsReadyArgs)
            Dim jsonAddressesChunk As String = e.AddressesChunk
            Dim uploadAddressesResponse = uploadAddressesToTemporaryStorage(jsonAddressesChunk)

            If uploadAddressesResponse IsNot Nothing Then
                Dim tempAddressesStorageID As String = uploadAddressesResponse.optimization_problem_id
                Dim addressesInChunk As Integer = CInt(uploadAddressesResponse.address_count)
                If addressesInChunk < fileReading.jsonObjectsChunkSize Then requestedAddresses = addressesInChunk
                downloadGeocodedAddresses(tempAddressesStorageID, addressesInChunk)
            End If
        End Sub

        Public Function uploadAddressesToTemporaryStorage(ByVal streamSource As String) As Route4MeManager.uploadAddressesToTemporaryStorageResponse
            Dim route4Me As Route4MeManager = New Route4MeManager(_apiKey)
            Dim jsonText As String = ""

            If streamSource.Contains("{") AndAlso streamSource.Contains("}") Then
                jsonText = streamSource
            Else
                jsonText = readJsonTextFromLargeJsonFileOfAddresses(streamSource)
            End If

            Dim errorString As String = ""
            Dim uploadResponse As Route4MeManager.uploadAddressesToTemporaryStorageResponse _
            = route4Me.uploadAddressesToTemporaryStorage(jsonText, errorString)

            If uploadResponse Is Nothing OrElse Not uploadResponse.status Then Return Nothing

            Return uploadResponse
        End Function

        Public Async Sub downloadGeocodedAddresses(ByVal temporarryAddressesStorageID As String, ByVal addressesInFile As Nullable(Of Integer))
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or
                                                    SecurityProtocolType.Tls11 Or
                                                    SecurityProtocolType.Tls12 Or
                                                    SecurityProtocolType.Ssl3

            geocodedAddressesDownloadingIsDone = False

            savedAddresses = New List(Of AddressGeocoded)()

            TEMPORARY_ADDRESSES_STORAGE_ID = temporarryAddressesStorageID
            If addressesInFile IsNot Nothing Then requestedAddresses = addressesInFile

            manualResetEvent = New ManualResetEvent(False)
            Flag = False

            Dim options = CreateOptions()
            options.Path = "/socket.io"
            options.Host = "validator.route4me.com/"
            options.AutoConnect = True
            options.IgnoreServerCertificateValidation = True
            options.Timeout = 60000
            options.Upgrade = True
            options.ForceJsonp = True
            options.Transports = ImmutableList.Create(Of String)(New String() {Polling.NAME, WebSocket.NAME})

            Dim uri = CreateUri()

            socket = Await Task.Run(Function()
                                        Return IO.Socket(uri, options)
                                    End Function)

            socket.[On]("error", Sub(message)
                                                                 Debug.Print("Error -> " & message)
                                                                 Thread.Sleep(500)
                                                                 manualResetEvent.[Set]()
                                                                 socket.Disconnect()
                                                             End Sub)

                                        socket.[On](Socket.EVENT_ERROR, Sub(e)
                                                                            Dim exception = CType(e, Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client.EngineIOException)
                                                                            Console.WriteLine("EVENT_ERROR. " & exception.Message)
                                                                            Console.WriteLine("BASE EXCEPTION. " & exception.GetBaseException().Message())
                                                                            Console.WriteLine("DATA COUNT. " & exception.Data.Count)
                                                                            socket.Disconnect()
                                                                            manualResetEvent.[Set]()
                                                                        End Sub)

                                        socket.[On](Socket.EVENT_MESSAGE, Sub(message)
                                                                              Thread.Sleep(500)
                                                                          End Sub)

                                        socket.[On]("data", Sub(d)
                                                                Thread.Sleep(1000)
                                                            End Sub)

                                        socket.[On](Socket.EVENT_DISCONNECT, Sub(e)
                                                                                 Thread.Sleep(700)

                                                                             End Sub)

                                        socket.[On](Socket.EVENT_RECONNECT_ATTEMPT, Sub(e)
                                                                                        Thread.Sleep(1500)
                                                                                    End Sub)

                                        socket.[On]("addresses_bulk", Sub(addresses_chunk)
                                                                          Dim jsonChunkText As String = addresses_chunk.ToString()
                                                                          Dim errors As List(Of String) = New List(Of String)()
                                                                          Dim jsonSettings As JsonSerializerSettings = New JsonSerializerSettings() With {
                                                  .[Error] = Sub(sender As Object, args As ErrorEventArgs)
                                                                 errors.Add(args.ErrorContext.[Error].Message)
                                                                 args.ErrorContext.Handled = True
                                                             End Sub,
                                                  .NullValueHandling = NullValueHandling.Ignore
                                              }
                                                                          Dim addressesChunk = JsonConvert.DeserializeObject(Of AddressGeocoded())(jsonChunkText, jsonSettings)

                                                                          If errors.Count > 0 Then
                                                                              Debug.Print("Json serializer errors:")

                                                                              For Each errMessage As String In errors
                                                                                  Debug.Print(errMessage)
                                                                              Next
                                                                          End If

                                                                          savedAddresses = savedAddresses.Concat(addressesChunk).ToList()
                                                                          loadedAddressesCount += addressesChunk.Length

                                                                          If loadedAddressesCount = nextDownloadStage Then
                                                                              download(loadedAddressesCount)
                                                                          End If

                                                                          If loadedAddressesCount = requestedAddresses Then
                                                                              socket.Emit("disconnect", TEMPORARY_ADDRESSES_STORAGE_ID)
                                                                              loadedAddressesCount = 0
                                                                              Dim args As AddressesChunkGeocodedArgs = New AddressesChunkGeocodedArgs() With {
                                                      .lsAddressesChunkGeocoded = savedAddresses
                                                  }
                                                                              OnAddressesChunkGeocoded(args)
                                                                              manualResetEvent.[Set]()
                                                                              geocodedAddressesDownloadingIsDone = True

                                                                              If largeJsonFileProcessingIsDone Then
                                                                                  OnGeocodingIsFinished(New GeocodingIsFinishedArgs() With {
                                                          .isFinished = True
                                                      })
                                                                              End If

                                                                              socket.Close()
                                                                          End If
                                                                      End Sub)

                                        socket.[On]("geocode_progress", Sub(message)
                                                                            Dim progressMessage = JsonConvert.DeserializeObject(Of clsProgress)(message.ToString())

                                                                            If progressMessage.total = progressMessage.done Then
                                                                                If requestedAddresses Is Nothing Then requestedAddresses = progressMessage.total
                                                                                download(0)
                                                                            End If
                                                                        End Sub)

                                        Dim jobj = New JObject()
                                        jobj.Add("temporary_addresses_storage_id", TEMPORARY_ADDRESSES_STORAGE_ID)
                                        jobj.Add("force_restart", True)

                                        Dim _args As Object = New List(Of Object)

                                        _args.Add(jobj)

                                        Try
                                            socket.Emit("geocode", _args)
                                        Catch ex As Exception
                                            Debug.Print("Socket connection failed. " & ex.Message)
                                        End Try

                                        manualResetEvent.WaitOne()
                                End Sub

        Public Sub download(ByVal start As Integer)
            Dim bufferFailSafeMaxAddresses As Integer = 100
            Dim chunkSize As Integer = CInt(Math.Round(CDec((Math.Min(200, Math.Max(10, Convert.ToDecimal(requestedAddresses / 100)))))))
            Dim chunksLimit As Integer = CInt(Math.Ceiling((CDec((bufferFailSafeMaxAddresses / chunkSize)))))
            Dim maxAddressesToBeDownloaded As Integer = chunkSize * chunksLimit

            nextDownloadStage = loadedAddressesCount + maxAddressesToBeDownloaded

            Dim jobj = New JObject()

            jobj.Add("temporary_addresses_storage_id", TEMPORARY_ADDRESSES_STORAGE_ID)
            jobj.Add("from_index", start)
            jobj.Add("chunks_limit", chunksLimit)
            jobj.Add("chunk_size", chunkSize)

            Dim _args As Object = New List(Of Object)

            _args.Add(jobj)
            socket.Emit("download", _args)
        End Sub

        Public Function readJsonTextFromLargeJsonFileOfAddresses(ByVal sFileName As String) As String
            Dim fileRead As FastFileReading = New FastFileReading()
            Return If(fileRead IsNot Nothing, fileRead.readJsonTextFromFile(sFileName), String.Empty)
        End Function
    End Class

    Class clsProgress
        Public Property total As Integer
        Public Property done As Integer
    End Class
End Namespace
