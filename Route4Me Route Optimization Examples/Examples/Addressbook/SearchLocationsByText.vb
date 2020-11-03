Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Search for the address book Locations by query text.
        ''' </summary>
        Public Sub SearchLocationsByText()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim addressBookParameters = New AddressBookParameters With {
                .Query = "david",
                .Offset = 0,
                .Limit = 20
            }

            Dim total As UInteger = Nothing, errorString As String = Nothing
            Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

            PrintExampleContact(contacts, total, errorString)
        End Sub
    End Class
End Namespace
