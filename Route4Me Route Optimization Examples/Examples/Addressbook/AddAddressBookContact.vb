Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add address book contact
        ''' </summary>
        Public Sub AddAddressBookContact()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim contact = New AddressBookContact() With {
                .first_name = "Test FirstName " & (New Random()).[Next]().ToString(),
                .address_1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .cached_lat = 38.024654,
                .cached_lng = -77.338814,
                .address_custom_data = New Dictionary(Of String, Object)() From {
                    {"Service type", "publishing"},
                    {"Facilities", "storage"},
                    {"Parking", "temporarry"}
                }
            }

            Dim errorString As String = Nothing
            Dim resultContact = route4Me.AddAddressBookContact(contact, errorString)

            If resultContact Is Nothing OrElse resultContact.[GetType]() <> GetType(AddressBookContact) Then
                Console.WriteLine("Cannot create an address book contact." & Environment.NewLine & errorString)
                Return
            End If

            ContactsToRemove = New List(Of String)()
            ContactsToRemove.Add(resultContact.address_id.ToString())

            PrintExampleContact(resultContact, 0, errorString)

            RemoveTestContacts()
        End Sub
    End Class
End Namespace
