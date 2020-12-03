Imports System.Text
Imports System.IO
Imports System.IO.MemoryMappedFiles
Imports Newtonsoft.Json
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Threading

Namespace Route4MeSDK.FastProcessing
    Public Class FastFileReading
        Const offset As Long = &H10000000
        Const length As Long = &H20000000
        Private jsonFileName As String
        Public Property jsonObjectsChunkSize As Integer
        Private manualResetEvent As ManualResetEvent = Nothing

        Public Event JsonFileChunkIsReady As EventHandler(Of JsonFileChunkIsReadyArgs)

        Public Event JsonFileReadingIsDone As EventHandler(Of JsonFileReadingIsDoneArgs)

        Public Delegate Sub JsonFileChunkIsReadyEventHandler(ByVal sender As Object, ByVal e As JsonFileChunkIsReadyArgs)

        Public Class JsonFileChunkIsReadyArgs
            Inherits EventArgs

            Public Property AddressesChunk As String
        End Class

        Protected Overridable Sub OnJsonFileChunkIsReady(ByVal e As JsonFileChunkIsReadyArgs)
            'Dim handler As EventHandler(Of JsonFileChunkIsReadyArgs) = JsonFileChunkIsReady
            RaiseEvent JsonFileChunkIsReady(Me, e)
        End Sub

        Protected Overridable Sub OnJsonFileReadingIsDone(ByVal e As JsonFileReadingIsDoneArgs)
            'Dim handler As EventHandler(Of JsonFileReadingIsDoneArgs) = JsonFileReadingIsDone
            RaiseEvent JsonFileReadingIsDone(Me, e)
        End Sub

        Public Delegate Sub JsonFileReadingIsDoneEventHandler(ByVal sender As Object, ByVal e As JsonFileReadingIsDoneArgs)

        Public Class JsonFileReadingIsDoneArgs
            Inherits EventArgs

            Public Property IsDone As Boolean
        End Class

        Public Sub fastReadFromFile(ByVal sFileName As String)
            If sFileName.Substring(1, 1) <> ":" Then
                Dim startupPath As String = AppDomain.CurrentDomain.BaseDirectory
                sFileName = startupPath & "/" & sFileName
            End If

            Try

                Using memoryMappedFile As MemoryMappedFile = MemoryMappedFile.CreateFromFile(sFileName)

                    Using memoryMappedViewStream As MemoryMappedViewStream = memoryMappedFile.CreateViewStream(0, 1204, MemoryMappedFileAccess.Read)
                        Dim contentArray = New Byte(1023) {}
                        memoryMappedViewStream.Read(contentArray, 0, contentArray.Length)
                        Dim content As String = Encoding.UTF8.GetString(contentArray)
                    End Using
                End Using

            Catch ex As Exception
                Console.WriteLine("Error during JSON file readinf. " & ex.Message)
            End Try
        End Sub

        Public Sub readingChunksFromLargeJsonFile(ByVal fileName As String)
            Dim serializer As JsonSerializer = New JsonSerializer()
            Dim o As AddressField = Nothing
            Dim sJsonAddressesChunk As String = ""
            Dim curJsonObjects As Integer = 0

            Using s As FileStream = File.Open(fileName, FileMode.Open)

                Using sr As StreamReader = New StreamReader(s)

                    Using reader As JsonReader = New JsonTextReader(sr)
                        Dim blStartAdresses As Boolean = False

                        While reader.Read()
                            If reader.TokenType = JsonToken.StartArray Then blStartAdresses = True

                            If reader.TokenType = JsonToken.StartObject AndAlso blStartAdresses Then
                                o = serializer.Deserialize(Of AddressField)(reader)
                                If o.Address Is Nothing Then Continue While
                                sJsonAddressesChunk += JsonConvert.SerializeObject(o, Formatting.None) & ","
                                curJsonObjects += 1

                                If curJsonObjects >= jsonObjectsChunkSize Then
                                    sJsonAddressesChunk = "{""rows"":[" & sJsonAddressesChunk.TrimEnd(","c) & "]}"
                                    Dim chunkIsReady As JsonFileChunkIsReadyArgs = New JsonFileChunkIsReadyArgs()
                                    chunkIsReady.AddressesChunk = sJsonAddressesChunk
                                    sJsonAddressesChunk = ""
                                    curJsonObjects = 0
                                    OnJsonFileChunkIsReady(chunkIsReady)
                                    Thread.Sleep(5000)
                                End If
                            End If
                        End While

                        If sJsonAddressesChunk <> "" Then
                            sJsonAddressesChunk = "{""rows"":[" & sJsonAddressesChunk.TrimEnd(","c) & "]}"
                            Dim chunkIsReady As JsonFileChunkIsReadyArgs = New JsonFileChunkIsReadyArgs()
                            chunkIsReady.AddressesChunk = sJsonAddressesChunk
                            sJsonAddressesChunk = ""
                            OnJsonFileChunkIsReady(chunkIsReady)
                            Thread.Sleep(5000)
                        End If

                        Dim args As JsonFileReadingIsDoneArgs = New JsonFileReadingIsDoneArgs() With {
                    .IsDone = True
                }
                        OnJsonFileReadingIsDone(args)
                    End Using
                End Using
            End Using
        End Sub

        Public Sub readingChunksFromLargeJsonFile_2(ByVal fileName As String)
            Dim serializer As JsonSerializer = New JsonSerializer()
            Dim o As AddressField = Nothing
            Dim sJsonAddressesChunk As String = ""
            Dim curJsonObjects As Integer = 0

            Dim fullFileName As String = AppDomain.CurrentDomain.BaseDirectory & "\\" & fileName

            Using s As FileStream = File.Open(fullFileName, FileMode.Open)

                Using sr As StreamReader = New StreamReader(s)

                    Using reader As JsonReader = New JsonTextReader(sr)
                        Dim blStartAdresses As Boolean = False

                        While reader.Read()
                            If reader.TokenType = JsonToken.StartArray Then blStartAdresses = True

                            If reader.TokenType = JsonToken.StartObject AndAlso blStartAdresses Then
                                o = serializer.Deserialize(Of AddressField)(reader)
                                If o.Address Is Nothing Then Continue While
                                sJsonAddressesChunk += JsonConvert.SerializeObject(o, Formatting.None) & ","
                                curJsonObjects += 1

                                If curJsonObjects >= jsonObjectsChunkSize Then
                                    sJsonAddressesChunk = "{""rows"":[" & sJsonAddressesChunk.TrimEnd(","c) & "]}"
                                    Dim chunkIsReady As JsonFileChunkIsReadyArgs = New JsonFileChunkIsReadyArgs()
                                    chunkIsReady.AddressesChunk = sJsonAddressesChunk
                                    sJsonAddressesChunk = ""
                                    curJsonObjects = 0
                                    OnJsonFileChunkIsReady(chunkIsReady)
                                    Thread.Sleep(5000)
                                End If
                            End If
                        End While

                        If sJsonAddressesChunk <> "" Then
                            sJsonAddressesChunk = "{""rows"":[" & sJsonAddressesChunk.TrimEnd(","c) & "]}"
                            Dim chunkIsReady As JsonFileChunkIsReadyArgs = New JsonFileChunkIsReadyArgs()
                            chunkIsReady.AddressesChunk = sJsonAddressesChunk
                            sJsonAddressesChunk = ""
                            OnJsonFileChunkIsReady(chunkIsReady)
                            Thread.Sleep(5000)
                        End If

                        Dim args As JsonFileReadingIsDoneArgs = New JsonFileReadingIsDoneArgs() With {
                            .IsDone = True
                        }
                        OnJsonFileReadingIsDone(args)
                    End Using
                End Using
            End Using
        End Sub

        Private Sub FbGeocoding_GeocodingIsFinished(ByVal sender As Object, ByVal e As FastBulkGeocoding.GeocodingIsFinishedArgs)
        End Sub

        Public Function readJsonTextFromFile(ByVal sFileName As String) As String
            If Not File.Exists(sFileName) Then
                Console.WriteLine("The file " & sFileName & " doesn't exist...")
                Return ""
            End If

            Dim jsonContent As String = File.ReadAllText(sFileName)
            Return jsonContent
        End Function

        Public Function getObjectsListfromJsonText(Of T)(ByVal jsonText As String) As List(Of T)
            Dim lsObjects As List(Of T) = fastJSON.JSON.ToObject(Of List(Of T))(jsonText)
            Return lsObjects
        End Function

        Public Function getObjectsListFromJsonFile(Of T)(ByVal sFileName As String) As List(Of T)
            Dim jsonText As String = readJsonTextFromFile(sFileName)
            Dim lsObjects As List(Of T) = getObjectsListfromJsonText(Of T)(jsonText)
            Return lsObjects
        End Function
    End Class
End Namespace