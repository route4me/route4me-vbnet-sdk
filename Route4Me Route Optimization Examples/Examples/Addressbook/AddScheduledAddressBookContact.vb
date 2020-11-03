Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add a scheduled address book contact
        ''' </summary>
        Public Sub AddScheduledAddressBookContact()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim lsRemoveContacts = New List(Of String)()

            Dim sched1 As Schedule = New Schedule("daily", False) With {
                .enabled = True,
                .mode = "daily",
                .daily = New ScheduleDaily(1)
            }

            Dim scheduledContact1 As AddressBookContact = New AddressBookContact() With {
                .address_1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                .address_alias = "1604 PARKRIDGE PKWY 40214",
                .address_group = "Scheduled daily",
                .first_name = "Peter",
                .last_name = "Newman",
                .address_email = "pnewman6564@yahoo.com",
                .address_phone_number = "65432178",
                .cached_lat = 38.141598,
                .cached_lng = -85.793846,
                .address_city = "Louisville",
                .address_custom_data = New Dictionary(Of String, Object)() From {
                    {"scheduled", "yes"},
                    {"service type", "publishing"}
                },
                .schedule = New List(Of Schedule)() From {
                    sched1
                }
            }

            Dim errorString As String = Nothing
            Dim scheduledContact1Response = route4Me.AddAddressBookContact(scheduledContact1, errorString)

            Dim location1 As Integer = If(scheduledContact1Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact1Response.address_id), -1)

            If location1 > 0 Then
                lsRemoveContacts.Add(location1.ToString())
                Console.WriteLine("A location with the daily scheduling was created. AddressId: {0}", location1)
            Else
                Console.WriteLine("Creating if a location with daily scheduling failed")
            End If

            Dim sched2 As Schedule = New Schedule("weekly", False) With {
                .enabled = True,
                .weekly = New schedule_weekly(1, New Integer() {1, 2, 3, 4, 5})
            }
            Dim scheduledContact2 As AddressBookContact = New AddressBookContact() With {
                .address_1 = "1407 MCCOY, Louisville, KY, 40215",
                .address_alias = "1407 MCCOY 40215",
                .address_group = "Scheduled weekly",
                .first_name = "Bart",
                .last_name = "Douglas",
                .address_email = "bdouglas9514@yahoo.com",
                .address_phone_number = "95487454",
                .cached_lat = 38.202496,
                .cached_lng = -85.786514,
                .address_city = "Louisville",
                .service_time = 600,
                .schedule = New List(Of Schedule)() From {
                    sched2
                }
            }

            Dim scheduledContact2Response = route4Me.AddAddressBookContact(scheduledContact2, errorString)
            Dim location2 As Integer = If(scheduledContact2Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact2Response.address_id), -1)

            If location2 > 0 Then
                lsRemoveContacts.Add(location2.ToString())
                Console.WriteLine("A location with the weekly scheduling was created. AddressId: {0}", location2)
            Else
                Console.WriteLine("Creating if a location with weekly scheduling failed")
            End If

            Dim sched3 As Schedule = New Schedule("monthly", False) With {
                .enabled = True,
                .monthly = New schedule_monthly(_every:=1, _mode:="dates", _dates:=New Integer() {20, 22, 23, 24, 25})
            }

            Dim scheduledContact3 As AddressBookContact = New AddressBookContact() With {
                .address_1 = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                .address_2 = "4806 BELLEVUE AVE, Louisville, KY, 40215",
                .address_alias = "4805 BELLEVUE AVE 40215",
                .address_group = "Scheduled monthly",
                .first_name = "Bart",
                .last_name = "Douglas",
                .address_email = "bdouglas9514@yahoo.com",
                .address_phone_number = "95487454",
                .cached_lat = 38.178844,
                .cached_lng = -85.774864,
                .address_country_id = "US",
                .address_state_id = "KY",
                .address_zip = "40215",
                .address_city = "Louisville",
                .service_time = 750,
                .schedule = New List(Of Schedule)() From {
                    sched3
                },
                .color = "red"
            }

            Dim scheduledContact3Response = route4Me.AddAddressBookContact(scheduledContact3, errorString)
            Dim location3 As Integer = If(scheduledContact3Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact3Response.address_id), -1)

            If location3 > 0 Then
                lsRemoveContacts.Add(location3.ToString())
                Console.WriteLine("A location with the monthly scheduling (mode 'dates') was created. AddressId: {0}", location3)
            Else
                Console.WriteLine("Creating if a location with monthly scheduling (mode 'dates') failed")
            End If

            Dim sched4 As Schedule = New Schedule("monthly", False) With {
                .enabled = True,
                .monthly = New schedule_monthly(_every:=1, _mode:="nth", _nth:=New Dictionary(Of Integer, Integer)() From {
                    {1, 4}
                })
            }

            Dim scheduledContact4 As AddressBookContact = New AddressBookContact() With {
                .address_1 = "730 CECIL AVENUE, Louisville, KY, 40211",
                .address_alias = "730 CECIL AVENUE 40211",
                .address_group = "Scheduled monthly",
                .first_name = "David",
                .last_name = "Silvester",
                .address_email = "dsilvester5874@yahoo.com",
                .address_phone_number = "36985214",
                .cached_lat = 38.248684,
                .cached_lng = -85.821121,
                .address_city = "Louisville",
                .service_time = 450,
                .address_custom_data = New Dictionary(Of String, Object)() From {
                    {"scheduled", "yes"},
                    {"service type", "library"}
                },
                .schedule = New List(Of Schedule)() From {
                    sched4
                },
                .address_icon = "emoji/emoji-bus"
            }

            Dim scheduledContact4Response = route4Me.AddAddressBookContact(scheduledContact4, errorString)
            Dim location4 As Integer = If(scheduledContact4Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact4Response.address_id), -1)

            If location4 > 0 Then
                lsRemoveContacts.Add(location4.ToString())
                Console.WriteLine("A location with the monthly scheduling (mode 'nth') was created. AddressId: {0}", location4)
            Else
                Console.WriteLine("Creating if a location with monthly scheduling (mode 'nth') failed")
            End If

            Dim sched5 As Schedule = New Schedule("daily", False) With {
                .enabled = True,
                .mode = "daily",
                .daily = New ScheduleDaily(1)
            }

            Dim scheduledContact5 As AddressBookContact = New AddressBookContact() With {
                .address_1 = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                .address_alias = "4629 HILLSIDE DRIVE 40216",
                .address_group = "Scheduled daily",
                .first_name = "Kim",
                .last_name = "Shandor",
                .address_email = "kshand8524@yahoo.com",
                .address_phone_number = "9874152",
                .cached_lat = 38.176067,
                .cached_lng = -85.824638,
                .address_city = "Louisville",
                .address_custom_data = New Dictionary(Of String, Object)() From {
                    {"scheduled", "yes"},
                    {"service type", "appliance"}
                },
                .schedule = New List(Of Schedule)() From {
                    sched5
                },
                .schedule_blacklist = New String() {"2017-12-22", "2017-12-23"},
                .service_time = 300
            }

            Dim scheduledContact5Response = route4Me.AddAddressBookContact(scheduledContact5, errorString)
            Dim location5 As Integer = If(scheduledContact5Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact5Response.address_id), -1)

            If location5 > 0 Then
                lsRemoveContacts.Add(location5.ToString())
                Console.WriteLine("A location with the blacklist was created. AddressId: {0}", location5)
            Else
                Console.WriteLine("Creating of a location with the blacklist failed")
            End If

            Dim removed = route4Me.RemoveAddressBookContacts(lsRemoveContacts.ToArray(), errorString)

            If CBool(removed) Then
                Console.WriteLine("The added testing address book locations were removed successfuly")
            Else
                Console.WriteLine("Removing of the added testing address book locations failed")
            End If
        End Sub
    End Class
End Namespace
