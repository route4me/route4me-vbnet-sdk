Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Search for Specified Text, Show Specified Fields
        ''' </summary>
        Public Sub GetSpecifiedFieldsSearchText()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestContacts()

            Dim addressBookParameters = New AddressBookParameters With {
                .Query = "Test FirstName",
                .Fields = "address_id,first_name,address_email,address_group,first_name,cached_lat,schedule",
                .Offset = 0,
                .Limit = 20
            }

            Dim contactsFromObjects As List(Of AddressBookContact) = Nothing
            Dim errorString As String = Nothing

            Dim response = route4Me.SearchAddressBookLocation(addressBookParameters, contactsFromObjects, errorString)

            PrintExampleContact(contactsFromObjects.ToArray(), (If(contactsFromObjects IsNot Nothing, CUInt(contactsFromObjects.Count), 0)), errorString)

            RemoveTestContacts()
        End Sub
    End Class
End Namespace
