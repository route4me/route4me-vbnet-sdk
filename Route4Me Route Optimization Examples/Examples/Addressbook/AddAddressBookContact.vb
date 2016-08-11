Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function AddAddressBookContact() As AddressBookContact
            ' Create the manager with the api key
            Dim route4Me As New Route4MeSDKLibrary.Route4MeSDK.Route4MeManager(c_ApiKey)

            Dim contact As New AddressBookContact() With { _
                 .FirstName = "Test FirstName " + (New Random()).[Next]().ToString(), _
                 .Address1 = "Test Address1 " + (New Random()).[Next]().ToString(), _
                 .CachedLat = 38.024654, _
                 .CachedLng = -77.338814 _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim resultContact As AddressBookContact = route4Me.AddAddressBookContact(contact, errorString)

            Console.WriteLine("")

            If resultContact IsNot Nothing Then
                Console.WriteLine("AddAddressBookContact executed successfully")

                Console.WriteLine("AddressId: {0}", resultContact.AddressId)

                Return resultContact
            Else
                Console.WriteLine("AddAddressBookContact error: {0}", errorString)

                Return Nothing
            End If
        End Function
    End Class
End Namespace
