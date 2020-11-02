Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function AddAddressBookContact() As AddressBookContact
            ' Create the manager with the api key
            Dim route4Me As New Route4MeSDKLibrary.Route4MeSDK.Route4MeManager(ActualApiKey)

            Dim contact As New AddressBookContact() With {
                 .first_name = "Test FirstName " + (New Random()).[Next]().ToString(),
                 .address_1 = "Test Address1 " + (New Random()).[Next]().ToString(),
                 .cached_lat = 38.024654,
                 .cached_lng = -77.338814,
                 .address_custom_data = New Dictionary(Of String, Object)() From {
                        {"Service type", "publishing"},
                        {"Facilities", "storage"},
                        {"Parking", "temporarry"}
                    }
            }

            ' Run the query
            Dim errorString As String = ""
            Dim resultContact As AddressBookContact = route4Me.AddAddressBookContact(contact, errorString)

            Console.WriteLine("")

            If resultContact IsNot Nothing Then
                Console.WriteLine("AddAddressBookContact executed successfully")

                Console.WriteLine("AddressId: {0}", resultContact.address_id)

                For Each cdata In resultContact.address_custom_data
                    Console.WriteLine(cdata.Key & ": " + cdata.Value)
                Next

                Return resultContact
            Else
                Console.WriteLine("AddAddressBookContact error: {0}", errorString)

                Return Nothing
            End If
        End Function
    End Class
End Namespace
