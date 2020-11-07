Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update an address book contact by sending whole modified contact object.
        ''' </summary>
        Public Sub UpdateWholeAddressBookContact()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestContacts()

            Dim contactClone = R4MeUtils.ObjectDeepClone(Of AddressBookContact)(contact1)

            ' Modify the parameters of the contactClone
            contactClone.address_group = "Updated"
            contactClone.schedule_blacklist = New String() {"2020-03-14", "2020-03-15"}

            contactClone.address_custom_data = New Dictionary(Of String, Object) From {
                {"key1", "value1"},
                {"key2", "value2"}
            }

            Dim errorString0 As String = ""
            contactClone.local_time_window_start = R4MeUtils.DDHHMM2Seconds("7:05", errorString0)
            contactClone.local_time_window_end = R4MeUtils.DDHHMM2Seconds("7:22", errorString0)
            contactClone.AddressCube = 5
            contactClone.AddressPieces = 6
            contactClone.AddressRevenue = 700
            contactClone.AddressWeight = 80
            contactClone.AddressPriority = 9

            Dim sched1 = New Schedule("daily", False) With {
                .enabled = True,
                .mode = "daily",
                .daily = New ScheduleDaily(1)
            }

            contactClone.schedule = New List(Of Schedule)() From {
                sched1
            }

            Dim errorString As String = Nothing
            contact1 = route4Me.UpdateAddressBookContact(contactClone, contact1, errorString)

            PrintExampleContact(contact1, 0, errorString)

            RemoveTestContacts()
        End Sub
    End Class
End Namespace
