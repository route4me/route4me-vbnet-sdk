﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Search Locations By IDs
        ''' </summary>
        Public Sub SearchLocationsByIDs()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestContacts()

            Dim addressBookParameters = New AddressBookParameters With {
                .AddressId = contact1.address_id.ToString() & "," + contact2.address_id.ToString()
            }

            Dim total As UInteger = Nothing, errorString As String = Nothing
            Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

            PrintExampleContact(contacts, total, errorString)

            RemoveTestContacts()
        End Sub
    End Class
End Namespace