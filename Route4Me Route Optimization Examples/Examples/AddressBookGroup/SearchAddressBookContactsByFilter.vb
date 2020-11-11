Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Search the address book contacts by filter
        ''' </summary>
        Public Sub SearchAddressBookContactsByFilter()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim filterParam = New AddressBookGroupFilterParameter() With {
                .Query = "Louisville",
                .Display = "all"
            }

            Dim addressBookGroupParameters = New AddressBookGroupParameters() With {
                .Fields = New String() {"address_id", "address_1", "address_group"},
                .Offset = 0,
                .Limit = 10,
                .filter = filterParam
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.
                SearchAddressBookContactsByFilter(addressBookGroupParameters, errorString)

            Console.WriteLine(
                If(
                    If(response?.results?.Length, 0) < 1,
                    "Cannot retrieve the contacts by filter" & Environment.NewLine & errorString,
                    "Retrieved the contacts by filter: " & response.results.Length
                )
             )
        End Sub
    End Class
End Namespace
