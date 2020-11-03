Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get the address book contacts
        ''' </summary>
        Public Sub GetAddressBookContacts()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim addressBookParameters = New AddressBookParameters() With {
                .Limit = 10,
                .Offset = 0
            }

            Dim total As UInteger = Nothing, errorString As String = Nothing
            Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

            PrintExampleContact(contacts, total, errorString)
        End Sub
    End Class
End Namespace
