Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update a contact by modifying the specified parameters
        ''' </summary>
        ''' <param name="contact">Initial address book contact</param>
        Public Sub UpdateAddressBookContact(ByVal Optional contact As AddressBookContact = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestContacts()

            If contact IsNot Nothing Then contact1 = contact

            contact1.address_group = "Updated"
            contact1.schedule_blacklist = New String() {"2020-03-14", "2020-03-15"}

            contact1.address_custom_data = New Dictionary(Of String, Object) From {
                {"key1", "value1"},
                {"key2", "value2"}
            }

            Dim errorString0 As String = ""

            contact1.local_time_window_start = R4MeUtils.DDHHMM2Seconds("7:03", errorString0)
            contact1.local_time_window_end = R4MeUtils.DDHHMM2Seconds("7:37", errorString0)
            contact1.AddressCube = 5
            contact1.AddressPieces = 6
            contact1.AddressRevenue = 700
            contact1.AddressWeight = 80
            contact1.AddressPriority = 9

            Dim updatableProperties = New List(Of String)() From {
                "address_id",
                "address_group",
                "schedule_blacklist",
                "address_custom_data",
                "local_time_window_start",
                "local_time_window_end",
                "AddressCube",
                "AddressPieces",
                "AddressRevenue",
                "AddressWeight",
                "AddressPriority",
                "ConvertBooleansToInteger"
            }

            Dim errorString As String = Nothing
            Dim updatedContact = route4Me.UpdateAddressBookContact(contact1, updatableProperties, errorString)

            PrintExampleContact(updatedContact, 0, errorString)

            RemoveTestContacts()
        End Sub
    End Class
End Namespace
