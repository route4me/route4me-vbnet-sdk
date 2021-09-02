Imports System.Text
Imports System.IO
Imports System.IO.MemoryMappedFiles
Imports Newtonsoft.Json
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Threading
Imports CsvHelper
Imports System.Text.RegularExpressions

Namespace Route4MeSDK.FastProcessing
    Public Class FastFileReading
        Const offset As Long = &H10000000
        Const length As Long = &H20000000
        Private jsonFileName As String
        Private manualResetEvent As ManualResetEvent = Nothing

        Public Property chunkPause As Integer = 2000
        Public Property jsonObjectsChunkSize As Integer = 300
        Public Property csvObjectsChunkSize As Integer = 300
        Public Shared Property csvAddressMapping As Dictionary(Of String, String)

#Region "Event handler for the JsonFileChunkIsReady event"

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

#End Region

#Region "Event handler for the JsonFileReadingIsDone event"
        Public Delegate Sub JsonFileReadingIsDoneEventHandler(ByVal sender As Object, ByVal e As JsonFileReadingIsDoneArgs)

        Public Class JsonFileReadingIsDoneArgs
            Inherits EventArgs

            Public Property IsDone As Boolean
        End Class

        Protected Overridable Sub OnJsonFileReadingIsDone(ByVal e As JsonFileReadingIsDoneArgs)
            'Dim handler As EventHandler(Of JsonFileReadingIsDoneArgs) = JsonFileReadingIsDone
            RaiseEvent JsonFileReadingIsDone(Me, e)
        End Sub

#End Region

#Region "Event handler for the CsvFileChunkIsReady"
        Public Event CsvFileChunkIsReady As EventHandler(Of CsvFileChunkIsReadyArgs)
        Public Event CsvFileReadingIsDone As EventHandler(Of CsvFileReadingIsDoneArgs)

        Public Delegate Sub CsvFileChunkIsReadyEventHandler(ByVal sender As Object, ByVal e As CsvFileChunkIsReadyArgs)

        Public Class CsvFileChunkIsReadyArgs
            Inherits EventArgs

            Public Property AddressesChunk As String
            Public Property multiContacts As List(Of DataTypes.V5.AddressBookContact)
        End Class

        Protected Overridable Sub OnCsvFileChunkIsReady(ByVal e As CsvFileChunkIsReadyArgs)
            RaiseEvent CsvFileChunkIsReady(Me, e)
        End Sub
#End Region

#Region "Event handler for the CsvFileReadingIsDone event"
        Protected Overridable Sub OnCsvFileReadingIsDone(ByVal e As CsvFileReadingIsDoneArgs)
            RaiseEvent CsvFileReadingIsDone(Me, e)
        End Sub

        Public Delegate Sub CsvFileReadingIsDoneEventHandler(ByVal sender As Object, ByVal e As CsvFileReadingIsDoneArgs)

        Public Class CsvFileReadingIsDoneArgs
            Inherits EventArgs

            Public Property IsDone As Boolean
        End Class
#End Region

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

        Public Sub readingChunksFromLargeCsvFile(ByVal fileName As String, ByRef errorString As String)
            errorString = Nothing
            Dim curJsonObjects As Integer = 0
            Dim wrongCoordPattern As String = "^\-{0,1}\d{1,3}\.\d$"
            Dim lsMultiContacts = New List(Of DataTypes.V5.AddressBookContact)()
            Dim errorString2 As String = Nothing

            Using reader As TextReader = File.OpenText(fileName)
                Dim csv = New CsvReader(reader)
                csv.ReadHeader()
                Dim csvHeaders As String() = csv.FieldHeaders.Where(Function(x) x.Length > 0).ToArray()

                For Each csvHeader In csvHeaders

                    If Not csvAddressMapping.ContainsKey(csvHeader) Then
                        errorString = "CSV file header " & csvHeader & " is not specified in the CSV address mapping."
                        Return
                    End If
                Next

                For Each k1 As String In csvAddressMapping.Keys

                    If Not csvHeaders.Contains(k1) Then
                        errorString = "The CSV address mapping key " & k1 & " is not found in the CSV header"
                        Return
                    End If
                Next

                While csv.Read()
                    Dim abContact = New DataTypes.V5.AddressBookContact()

                    For Each csvHeader In csvHeaders
                        Dim fieldIndex As Integer = Array.IndexOf(csvHeaders, csvHeader)
                        Dim fieldValue = csv.GetField(fieldIndex)
                        Dim oFieldValue As Object = CObj(fieldValue)

                        If (If(fieldValue?.Length, 0)) > 0 Then
                            Dim propinfo = abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader))
                            Dim fieldType As String = If(Nullable.GetUnderlyingType(propinfo.PropertyType) IsNot Nothing, Nullable.GetUnderlyingType(propinfo.PropertyType).Name, propinfo.PropertyType.Name)

                            Select Case fieldType
                                Case "String"
                                    If csvAddressMapping(csvHeader) = "AddressAlias" AndAlso fieldValue.ToString().Length > 59 Then fieldValue = fieldValue.ToString().Substring(0, 59)
                                    If csvAddressMapping(csvHeader) = "AddressZip" AndAlso fieldValue.ToString().Length > 6 Then fieldValue = fieldValue.ToString().Substring(0, 5)
                                    abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader)).SetValue(abContact, fieldValue)
                                Case "Int32"
                                    Dim intVal As Integer? = R4MeUtils.ConvertObjectToType(Of Int32)(oFieldValue)

                                    If intVal IsNot Nothing Then
                                        abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader)).SetValue(abContact, intVal)
                                    Else
                                        Console.WriteLine("The field " & csvHeader & " of the address book contact " & abContact.Address1 & " ommited")
                                    End If

                                Case "Int64"

                                    If (New String() {"LocalTimeWindowStart", "LocalTimeWindowEnd", "LocalTimeWindowStart2", "LocalTimeWindowEnd2"}).Contains(csvAddressMapping(csvHeader)) AndAlso fieldValue.Contains(":") Then
                                        Dim hmValue As String = If(fieldValue.Length < 5, "0" & fieldValue, fieldValue)
                                        hmValue = If(hmValue.Length = 7, "0" & hmValue, hmValue)
                                        hmValue = If(hmValue.Length < 8, "00:" & hmValue, hmValue)
                                        Dim secValue = R4MeUtils.DDHHMM2Seconds(hmValue, errorString2)
                                        Dim seconds As Long? = If(secValue IsNot Nothing, CLng(secValue), Nothing)
                                        If seconds IsNot Nothing Then abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader)).SetValue(abContact, seconds)
                                        Continue For
                                    End If

                                    Dim longVal As Long? = R4MeUtils.ConvertObjectToType(Of Long)(oFieldValue)
                                    If csvAddressMapping(csvHeader) = "ServiceTime" Then longVal = longVal * 60

                                    If longVal IsNot Nothing Then
                                        abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader)).SetValue(abContact, longVal)
                                        Continue For
                                    End If

                                Case "Double"

                                    If (New String() {"CachedLat", "CachedLng", "CurbsideLat", "CurbsideLng"}).Contains(csvAddressMapping(csvHeader)) Then
                                        Dim problematic = Regex.IsMatch(fieldValue, wrongCoordPattern)

                                        If problematic Then
                                            fieldValue += "0000001"
                                            oFieldValue = CObj(fieldValue)
                                        End If
                                    End If

                                    Dim dbVal As Double? = R4MeUtils.ConvertObjectToType(Of Double)(oFieldValue)

                                    If dbVal IsNot Nothing Then
                                        abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader)).SetValue(abContact, dbVal)
                                    Else
                                        Console.WriteLine("The field " & csvHeader & " of the address book contact " & abContact.Address1 & " ommited")
                                    End If

                                Case "Boolean"
                                    Dim blVal As Boolean? = R4MeUtils.ConvertObjectToType(Of Boolean)(oFieldValue)

                                    If blVal IsNot Nothing Then
                                        abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader)).SetValue(abContact, blVal)
                                    Else
                                        Console.WriteLine("The field " & csvHeader & " of the address book contact " & abContact.Address1 & " ommited")
                                    End If

                                Case Else

                                    If fieldType = "Object" AndAlso propinfo.Name = "AddressCustomData" Then
                                        Dim customData = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(oFieldValue.ToString())
                                        abContact.[GetType]().GetProperty(csvAddressMapping(csvHeader)).SetValue(abContact, customData)
                                    End If
                            End Select
                        End If
                    Next

                    If Not csvAddressMapping.Values.Contains("AddressStopType") Then
                        abContact.AddressStopType = AddressStopType.Delivery.GetEnumDescription()
                    End If

                    lsMultiContacts.Add(abContact)
                    curJsonObjects += 1

                    If curJsonObjects >= csvObjectsChunkSize Then
                        Dim chunkIsReady As CsvFileChunkIsReadyArgs = New CsvFileChunkIsReadyArgs()

                        chunkIsReady.multiContacts = lsMultiContacts

                        curJsonObjects = 0

                        OnCsvFileChunkIsReady(chunkIsReady)

                        Thread.Sleep(chunkPause)

                        lsMultiContacts = New List(Of DataTypes.V5.AddressBookContact)()
                    End If
                End While

                If lsMultiContacts.Count > 0 Then
                    Dim chunkIsReady As CsvFileChunkIsReadyArgs = New CsvFileChunkIsReadyArgs()

                    chunkIsReady.multiContacts = lsMultiContacts

                    OnCsvFileChunkIsReady(chunkIsReady)

                    Thread.Sleep(chunkPause)

                    lsMultiContacts = New List(Of DataTypes.V5.AddressBookContact)()
                End If

                Dim args As CsvFileReadingIsDoneArgs = New CsvFileReadingIsDoneArgs() With {
                    .IsDone = True
                }

                OnCsvFileReadingIsDone(args)
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