Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add a scheduled address book contact
        ''' </summary>
        Public Sub AddScheduledAddressBookContact()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            ContactsToRemove = New List(Of String)()

#Region "Add a location, scheduled daily"
            Dim sched1 = New Schedule("daily", False) With {
                .enabled = True,
                .from = DateTime.Today.ToString("yyyy-MM-dd"),
                .mode = "daily",
                .daily = New ScheduleDaily(1)
            }

            scheduledContact1 = New AddressBookContact() With {
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
            scheduledContact1Response = route4Me.AddAddressBookContact(scheduledContact1, errorString)

            PrintExampleScheduledContact(scheduledContact1Response, "daily", errorString)
#End Region

#Region "Add a location, scheduled weekly"
            Dim sched2 = New Schedule("weekly", False) With {
                .enabled = True,
                .from = DateTime.Today.ToString("yyyy-MM-dd"),
                .weekly = New schedule_weekly(1, New Integer() {1, 2, 3, 4, 5})
            }

            scheduledContact2 = New AddressBookContact() With {
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

            scheduledContact2Response = route4Me.AddAddressBookContact(scheduledContact2, errorString)

            PrintExampleScheduledContact(scheduledContact2Response, "weekly", errorString)
#End Region

#Region "Add a location, scheduled monthly (dates mode)"
            Dim sched3 = New Schedule("monthly", False) With {
                .enabled = True,
                .from = DateTime.Today.ToString("yyyy-MM-dd"),
                .monthly = New schedule_monthly(_every:=1, _mode:="dates", _dates:=New Integer() {20, 22, 23, 24, 25})
            }

            scheduledContact3 = New AddressBookContact() With {
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

            scheduledContact3Response = route4Me.AddAddressBookContact(scheduledContact3, errorString)

            PrintExampleScheduledContact(scheduledContact3Response, "monthly (dates mode)", errorString)
#End Region

#Region "Add a location, scheduled monthly (nth mode)"
            Dim sched4 = New Schedule("monthly", False) With {
                .enabled = True,
                .from = DateTime.Today.ToString("yyyy-MM-dd"),
                .monthly = New schedule_monthly(
                    _every:=1,
                    _mode:="nth",
                    _nth:=New Dictionary(Of Integer, Integer)() From {
                    {1, 4}
                })
            }

            scheduledContact4 = New AddressBookContact() With {
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

            scheduledContact4Response = route4Me.AddAddressBookContact(scheduledContact4, errorString)

            PrintExampleScheduledContact(scheduledContact4Response, "monthly (nth mode)", errorString)
#End Region

#Region "Add a location with the daily scheduling and blacklist"
            Dim sched5 = New Schedule("daily", False) With {
                .enabled = True,
                .mode = "daily",
                .from = DateTime.Today.ToString("yyyy-MM-dd"),
                .daily = New ScheduleDaily(1)
            }

            scheduledContact5 = New AddressBookContact() With {
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

            scheduledContact5Response = route4Me.AddAddressBookContact(scheduledContact5, errorString)

            PrintExampleScheduledContact(scheduledContact5Response, "daily (with blacklist)", errorString)
#End Region

            RemoveTestContacts()
        End Sub
    End Class
End Namespace