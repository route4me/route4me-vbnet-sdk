Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports System.Threading
Imports Newtonsoft.Json

Namespace Route4MeSDK.FastProcessing
    Public Class FastValidateData
        Private manualResetEvent As ManualResetEvent = Nothing
        Private mainResetEvent As ManualResetEvent = Nothing
        Public Message As String

        Private Flag As Boolean
        Public Shared con As Connection = New Connection()

        Private fileReading As FastFileReading
        Private largeJsonFileProcessingIsDone As Boolean
        Private largeCsvFileProcessingIsDone As Boolean

        Private totalCsvChunks As Integer
        Private jsSer As JsonSerializer = New JsonSerializer()
        Public Property apiKey As String

        Public Property CsvChunkSize As Integer = 300
        Public Property JsonChunkSize As Integer = 300
        Public Property ChunkPause As Integer = 2000

        Shared taskList As List(Of Task)
        Shared threadPackage As List(Of List(Of DataTypes.V5.AddressBookContact))
        Public Property MandatoryFields As String()

        Public Property ConsoleWriteMessage As Boolean = False

        Public Sub New(ByVal _apiKey As String, ByVal Optional EnableTraceSource As Boolean = False)
            If _apiKey <> "" Then apiKey = _apiKey
            taskList = New List(Of Task)()
            threadPackage = New List(Of List(Of DataTypes.V5.AddressBookContact))()
        End Sub

        Public Sub readLargeContactsCsvFile(ByVal fileName As String, ByRef errorString As String)
            errorString = ""
            totalCsvChunks = 0

            If Not File.Exists(fileName) Then
                errorString = "The file " & fileName & " doesn't exist."
                Return
            End If

            Dim route4Me = New Route4MeManager(apiKey)

            largeCsvFileProcessingIsDone = False

            fileReading = New FastFileReading()
            fileReading.csvObjectsChunkSize = CsvChunkSize
            fileReading.chunkPause = ChunkPause
            fileReading.jsonObjectsChunkSize = JsonChunkSize

            AddHandler fileReading.CsvFileChunkIsReady, AddressOf FileReading_CsvFileChunkIsReady
            AddHandler fileReading.CsvFileReadingIsDone, AddressOf FileReading_CsvFileReadingIsDone

            mainResetEvent = New ManualResetEvent(False)
            fileReading.readingChunksFromLargeCsvFile(fileName, errorString)
        End Sub

        Private Sub FileReading_CsvFileReadingIsDone(ByVal sender As Object, ByVal e As FastFileReading.CsvFileReadingIsDoneArgs)
            Parallel.ForEach(threadPackage, Sub(chunk)
                                                CsvFileChunkIsReady(chunk)
                                            End Sub)
            threadPackage = New List(Of List(Of DataTypes.V5.AddressBookContact))()
        End Sub

        Private Sub FileReading_CsvFileChunkIsReady(ByVal sender As Object, ByVal e As FastFileReading.CsvFileChunkIsReadyArgs)
            threadPackage.Add(e.multiContacts)

            If threadPackage.Count > 15 Then
                Parallel.ForEach(threadPackage, Sub(chunk)
                                                    CsvFileChunkIsReady(chunk)
                                                End Sub)
                threadPackage = New List(Of List(Of DataTypes.V5.AddressBookContact))()
            End If
        End Sub

        Private Async Sub CsvFileChunkIsReady(ByVal contactsChunk As List(Of DataTypes.V5.AddressBookContact))
            Dim route4Me = New Route4MeManagerV5(apiKey)
            Dim contactParams = New Route4MeManagerV5.BatchCreatingAddressBookContactsRequest() With {
                .Data = contactsChunk.ToArray()
            }

            For Each contact In contactsChunk
                Dim errorMessages = ValidateContact(contact)
                If errorMessages.Count > 0 AndAlso ConsoleWriteMessage Then Console.WriteLine("===================================")
            Next
        End Sub

        Private Function ValidateContact(ByVal contact As DataTypes.V5.AddressBookContact) As List(Of String)
            Dim errorMessages = New List(Of String)()
            Dim err As String = ""

            For Each fieldName In MandatoryFields
                Dim oval As Object = GetType(DataTypes.V5.AddressBookContact).GetProperty(fieldName).GetValue(contact)

                Select Case fieldName
                    Case "CachedLat", "CurbsideLat"
                        Dim latok = PropertyValidation.ValidateLatitude(oval)

                        If latok IsNot Nothing AndAlso Not latok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If
                    Case "CachedLng", "CurbsideLng"
                        Dim lngok = PropertyValidation.ValidateLongitude(oval)

                        If lngok IsNot Nothing AndAlso Not lngok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "LocalTimeWindowStart", "LocalTimeWindowEnd", "LocalTimeWindowStart2", "LocalTimeWindowEnd2"
                        Dim twok = PropertyValidation.ValidateTimeWindow(oval)

                        If twok IsNot Nothing AndAlso Not twok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "ServiceTime"
                        Dim stok = PropertyValidation.ValidateServiceTime(oval)

                        If stok IsNot Nothing AndAlso Not stok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "AddressStateId"
                        Dim stateok = PropertyValidation.ValidationStateId(oval)

                        If stateok IsNot Nothing AndAlso Not stateok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}. Error: {stateok.ErrorMessage}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "AddressCountryId"
                        Dim aciok = PropertyValidation.ValidateCountryId(oval)

                        If aciok IsNot Nothing AndAlso Not aciok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}. Error: {aciok.ErrorMessage}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "AddressStopType"
                        Dim astok = PropertyValidation.ValidateAddressStopType(oval)

                        If astok IsNot Nothing AndAlso Not astok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}. Error: {astok.ErrorMessage}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "AddressZip"
                        Dim azok = PropertyValidation.ValidateZipCode(oval)

                        If azok IsNot Nothing AndAlso Not azok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}. Error: {azok.ErrorMessage}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "AddressCube", "AddressPieces", "AddressPriority", "AddressRevenue", "AddressWeight"
                        Dim numbok = PropertyValidation.ValidateIsNumber(oval)

                        If numbok IsNot Nothing AndAlso Not numbok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}. Error: {numbok.ErrorMessage}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "Color"
                        Dim colok = PropertyValidation.ValidateColorValue(oval)

                        If colok IsNot Nothing AndAlso Not colok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}. Error: {colok.ErrorMessage}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If

                    Case "EligibleDepot", "EligiblePickup"
                        Dim boolok = PropertyValidation.ValidateIsBoolean(oval)

                        If boolok IsNot Nothing AndAlso Not boolok Is ValidationResult.Success Then
                            err = $"Wrong {fieldName}: {oval.ToString()} in the address: {contact.Address1}. Error: {boolok.ErrorMessage}"
                            errorMessages.Add(err)
                            If ConsoleWriteMessage Then Console.WriteLine(err)
                        End If
                End Select
            Next

            Return errorMessages
        End Function
    End Class
End Namespace