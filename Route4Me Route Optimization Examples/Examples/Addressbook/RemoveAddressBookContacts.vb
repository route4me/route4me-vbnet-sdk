Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove an array of the address book contacts.
        ''' </summary>
        ''' <param name="addressIds">An array of the address IDs</param>
        Public Sub RemoveAddressBookContacts(ByVal Optional addressIds As String() = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestContacts()

            If addressIds Is Nothing Then
                addressIds = New String() {contact1.address_id.ToString(), contact2.address_id.ToString()}
            End If

            Dim errorString As String = Nothing
            Dim removed As Boolean = route4Me.RemoveAddressBookContacts(addressIds, errorString)

            Console.WriteLine("")
            Console.WriteLine(If(removed, String.Format("RemoveAddressBookContacts executed successfully, {0} contacts deleted", addressIds.Length), String.Format("RemoveAddressBookContacts error: {0}", errorString)))

            ContactsToRemove.Remove(contact1.address_id.ToString())
            ContactsToRemove.Remove(contact2.address_id.ToString())

            RemoveTestContacts()
        End Sub
    End Class
End Namespace
