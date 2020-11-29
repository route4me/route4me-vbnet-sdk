Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManager

Namespace Route4MeSDKTest.Examples
    Public Enum GeocodingPrintOption
        Geocodings = 1
        StreetData = 2
        StreetService = 3
        StreetZipCode = 4
    End Enum

    Partial Public NotInheritable Class Route4MeExamples

        Public ActualApiKey As String = R4MeUtils.ReadSetting("actualApiKey")
        Public DemoApiKey As String = R4MeUtils.ReadSetting("demoApiKey")

        Public AvoidanceZonesToRemove As List(Of String) = New List(Of String)()
        Public TerritoryZonesToRemove As List(Of String) = New List(Of String)()
        Public ContactsToRemove As List(Of String)
        Public RoutesToRemove As List(Of String)
        Public OptimizationsToRemove As List(Of String)
        Public addressBookGroupsToRemove As List(Of String)
        Public configKeysToRemove As List(Of String) = New List(Of String)()
        Public CustomNoteTypesToRemove As List(Of String) = New List(Of String)()
        Public OrdersToRemove As List(Of String) = New List(Of String)()
        Public OrderCustomFieldsToRemove As List(Of Integer) = New List(Of Integer)()

        Private lastCreatedOrder As Order

        Private dataObjectSD10Stops As DataObject
        Private SD10Stops_optimization_problem_id As String
        Private SD10Stops_route As DataObjectRoute
        Private SD10Stops_route_id As String

        Public contact1, contact2 As AddressBookContact

        Private scheduledContact1, scheduledContact1Response As AddressBookContact
        Private scheduledContact2, scheduledContact2Response As AddressBookContact
        Private scheduledContact3, scheduledContact3Response As AddressBookContact
        Private scheduledContact4, scheduledContact4Response As AddressBookContact
        Private scheduledContact5, scheduledContact5Response As AddressBookContact

        Private contactToRemove As AddressBookContact

        Public dataObjectSDRT As DataObject
        Public SDRT_optimization_problem_id As String
        Public SDRT_route As DataObjectRoute
        Public SDRT_route_id As String

        Dim avoidanceZone As AvoidanceZone
        Dim territoryZone As TerritoryZone

#Region "Optimizations, Routes, Destinations"

        Private Sub PrintExampleOptimizationResult(ByVal dataObject As Object, ByVal errorString As String)
            Dim testName As String = (New System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("{0} executed successfully", testName)
                Console.WriteLine("")

                If dataObject.[GetType]() = GetType(DataObject) Then
                    Dim dataObject1 = CType(dataObject, DataObject)

                    Console.WriteLine("Optimization Problem ID: {0}", dataObject1.OptimizationProblemId)
                    Console.WriteLine("State: {0}", dataObject1.State)

                    dataObject1.UserErrors.ToList().ForEach(
                        Sub([error]) Console.WriteLine("UserError : '{0}'", [error]))

                    Console.WriteLine("")

                    dataObject1.Addresses.ToList().ForEach(
                        Sub(address)
                            Console.WriteLine("Address: {0}", address.AddressString)
                            Console.WriteLine("Route ID: {0}", address.RouteId)
                        End Sub)
                Else
                    Dim optimizations = CType(dataObject, DataObject())

                    Console.WriteLine(
                        testName & " executed successfully, {0} optimizations returned",
                        optimizations.Length)

                    Console.WriteLine("")

                    optimizations.ToList().ForEach(
                        Sub(optimization)
                            Console.WriteLine(
                            "Optimization Problem ID: {0}",
                            optimization.OptimizationProblemId)

                            Console.WriteLine("")
                        End Sub)
                End If
            Else
                Console.WriteLine("{0} error {1}", testName, errorString)
            End If
        End Sub

        Private Sub PrintExampleRouteResult(ByVal dataObjectRoute As Object,
                                            ByVal errorString As String)

            Dim testName As String = (New StackTrace()).
                GetFrame(1).
                GetMethod().
                Name

            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If dataObjectRoute Is Nothing Then
                Console.WriteLine("{0} error {1}", testName, errorString)
            ElseIf dataObjectRoute.[GetType]() = GetType(DataObjectRoute()) Then
                Console.WriteLine("{0} executed successfully", testName)

                For Each route In CType(dataObjectRoute, DataObjectRoute())
                    Console.WriteLine("Route ID: {0}", route.RouteID)
                Next
            Else
                Dim route1 = If(
                    dataObjectRoute.[GetType]() = GetType(DataObjectRoute),
                    CType(dataObjectRoute, DataObjectRoute),
                    Nothing
                )

                Dim route2 = If(
                    dataObjectRoute.[GetType]() = GetType(RouteResponse),
                    CType(dataObjectRoute, RouteResponse),
                    Nothing
                )

                Console.WriteLine("{0} executed successfully", testName)
                Console.WriteLine("")
                Console.WriteLine("Optimization Problem ID: {0}",
                                   If(route1 IsNot Nothing,
                                      route1.OptimizationProblemId,
                                      route2.OptimizationProblemId)
                                 )
                Console.WriteLine("")

                If route1 IsNot Nothing Then
                    route1.Addresses.ToList().ForEach(
                        Sub(address)
                            Console.WriteLine("Address: {0}", address.AddressString)
                            Console.WriteLine("Route ID: {0}", address.RouteId)
                        End Sub)

                    If (If(route1?.Directions?.Length, 0)) > 0 Then
                        Console.WriteLine("")
                        Console.WriteLine(
                            String.Format("Directions length: {0}",
                            route1.Directions.Length)
                        )
                    End If

                    If (If(route1?.Path?.Length, 0)) > 0 Then
                        Console.WriteLine("")
                        Console.WriteLine(String.Format("Path points: {0}", route1.Path.Length))
                    End If
                Else
                    route2.Addresses.ToList().ForEach(
                        Sub(address)
                            Console.WriteLine("Address: {0}", address.AddressString)
                            Console.WriteLine("Route ID: {0}", address.RouteId)
                        End Sub)

                    If (If(route2?.Directions?.Length, 0)) > 0 Then
                        Console.WriteLine("")
                        Console.WriteLine(
                            String.Format(
                                "Directions length: {0}",
                                route2.Directions.Length)
                            )
                    End If

                    If (If(route2?.Path?.Length, 0)) > 0 Then
                        Console.WriteLine("")
                        Console.WriteLine(
                            String.Format("Path points: {0}",
                            route2.Path.Length)
                        )
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Console print of an example results.
        ''' </summary>
        ''' <param name="obj">An Address type object, Or boolean value</param>
        ''' <param name="errorString">Error string</param>
        Private Sub PrintExampleDestination(ByVal obj As Object, ByVal Optional errorString As String = "")
            If obj Is Nothing AndAlso obj.[GetType]() <> GetType(Address) AndAlso obj.[GetType]() <> GetType(Boolean) Then
                Console.WriteLine("Wrong address. Cannot print." & Environment.NewLine & errorString)
                Return
            End If

            Console.WriteLine("")

            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name

            testName = If(testName IsNot Nothing, testName, "")

            If obj.[GetType]() = GetType(Address) Then
                Dim address = CType(obj, Address)

                If address.RouteDestinationId IsNot Nothing Then
                    Console.WriteLine(testName & " executed successfully")
                    Console.WriteLine("Destination ID: {0}", address.RouteDestinationId)
                Else
                    Console.WriteLine(testName & " error: {0}", errorString)
                End If
            ElseIf obj.[GetType]().IsArray Then
                Dim addresses = CType(obj, Integer())
                Console.WriteLine(testName & " executed successfully")
                Console.WriteLine("Affected destinations: " & addresses.Length)
            Else
                Console.WriteLine(If(CBool(obj), testName & " executed successfully", String.Format(testName & " error: {0}", errorString)))
            End If
        End Sub

        Public Function RunOptimizationSingleDriverRoute10Stops(ByVal Optional routeName As String = Nothing)
            Dim r4mm As New Route4MeManager(ActualApiKey)

#Region "Prepare the addresses"

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "151 Arbor Way Milledgeville GA 31061",
                .IsDepot = True,
                .Latitude = 33.132675170898,
                .Longitude = -83.244743347168,
                .Time = 0,
                .CustomFields = New Dictionary(Of String, String)() From {
                    {"color", "red"},
                    {"size", "huge"}
                }
            }, New Address() With {
                .AddressString = "230 Arbor Way Milledgeville GA 31061",
                .Latitude = 33.129695892334,
                .Longitude = -83.24577331543,
                .Time = 0
            }, New Address() With {
                .AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                .Latitude = 33.143497,
                .Longitude = -83.224487,
                .Time = 0
            }, New Address() With {
                .AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.141784667969,
                .Longitude = -83.237518310547,
                .Time = 0
            }, New Address() With {
                .AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.141086578369,
                .Longitude = -83.238258361816,
                .Time = 0
            }, New Address() With {
                .AddressString = "131 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.142036437988,
                .Longitude = -83.238845825195,
                .Time = 0
            },
                New Address() With {
                .AddressString = "138 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.14307,
                .Longitude = -83.239334,
                .Time = 0
            }, New Address() With {
                .AddressString = "139 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.142734527588,
                .Longitude = -83.237442016602,
                .Time = 0
            }, New Address() With {
                .AddressString = "145 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.143871307373,
                .Longitude = -83.237342834473,
                .Time = 0
            }, New Address() With {
                .AddressString = "221 Blake Cir Milledgeville GA 31061",
                .Latitude = 33.081462860107,
                .Longitude = -83.208511352539,
                .Time = 0
            }}

#End Region

            ' Set parameters

            Dim parameters As New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = If(
                                routeName Is Nothing,
                                "SD Route 10 Stops Test " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                routeName
                             ),
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription()
            }

            Dim optimizationParameters As New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim errorString As String = ""

            Try
                dataObjectSD10Stops = r4mm.RunOptimization(optimizationParameters, errorString)
                SD10Stops_optimization_problem_id = dataObjectSD10Stops.OptimizationProblemId
                SD10Stops_route = If(
                    (dataObjectSD10Stops IsNot Nothing AndAlso dataObjectSD10Stops.Routes IsNot Nothing AndAlso dataObjectSD10Stops.Routes.Length > 0),
                    dataObjectSD10Stops.Routes(0),
                    Nothing)
                SD10Stops_route_id = If(
                    (SD10Stops_route IsNot Nothing), SD10Stops_route.RouteID, Nothing)

                If dataObjectSD10Stops IsNot Nothing AndAlso dataObjectSD10Stops.Routes IsNot Nothing AndAlso dataObjectSD10Stops.Routes.Length > 0 Then
                    SD10Stops_route = dataObjectSD10Stops.Routes(0)
                Else
                    SD10Stops_route = Nothing
                End If

                Return True
            Catch ex As Exception
                Console.WriteLine("Single Driver Route 10 Stops generation failed. " & ex.Message)
                Return False
            End Try

        End Function

        Private Sub RemoveTestRoutes()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing

            If RoutesToRemove.Count > 0 Then
                Try
                    Dim deletedRouteIds As String() = route4Me.DeleteRoutes(RoutesToRemove.ToArray(), errorString)
                Catch ex As Exception
                    Console.WriteLine("Cannot remove test routes." & Environment.NewLine + ex.Message)
                End Try
            End If
        End Sub

        Private Sub RemoveTestOptimizations()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing

            If (If(OptimizationsToRemove?.Count, 0)) > 0 Then
                Try
                    Dim result As Boolean = route4Me.RemoveOptimization(OptimizationsToRemove.ToArray(), errorString)

                    OptimizationsToRemove = New List(Of String)()
                Catch ex As Exception
                    Console.WriteLine("Cannot remove test routes." & Environment.NewLine + ex.Message)
                End Try
            End If
        End Sub

        Public Function RunSingleDriverRoundTrip() As Boolean
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "754 5th Ave New York, NY 10019",
                .[Alias] = "Bergdorf Goodman",
                .IsDepot = True,
                .Latitude = 40.7636197,
                .Longitude = -73.9744388,
                .Time = 0
            }, New Address() With {
                .AddressString = "717 5th Ave New York, NY 10022",
                .[Alias] = "Giorgio Armani",
                .Latitude = 40.7669692,
                .Longitude = -73.9693864,
                .Time = 0
            }, New Address() With {
                .AddressString = "888 Madison Ave New York, NY 10014",
                .[Alias] = "Ralph Lauren Women's and Home",
                .Latitude = 40.7715154,
                .Longitude = -73.9669241,
                .Time = 0
            }, New Address() With {
                .AddressString = "1011 Madison Ave New York, NY 10075",
                .[Alias] = "Yigal Azrou'l",
                .Latitude = 40.7772129,
                .Longitude = -73.9669,
                .Time = 0
            }, New Address() With {
                .AddressString = "440 Columbus Ave New York, NY 10024",
                .[Alias] = "Frank Stella Clothier",
                .Latitude = 40.7808364,
                .Longitude = -73.9732729,
                .Time = 0
            }, New Address() With {
                .AddressString = "324 Columbus Ave #1 New York, NY 10023",
                .[Alias] = "Liana",
                .Latitude = 40.7803123,
                .Longitude = -73.9793079,
                .Time = 0
            }, New Address() With {
                .AddressString = "110 W End Ave New York, NY 10023",
                .[Alias] = "Toga Bike Shop",
                .Latitude = 40.7753077,
                .Longitude = -73.9861529,
                .Time = 0
            }, New Address() With {
                .AddressString = "555 W 57th St New York, NY 10019",
                .[Alias] = "BMW of Manhattan",
                .Latitude = 40.7718005,
                .Longitude = -73.9897716,
                .Time = 0
            }, New Address() With {
                .AddressString = "57 W 57th St New York, NY 10019",
                .[Alias] = "Verizon Wireless",
                .Latitude = 40.7558695,
                .Longitude = -73.9862019,
                .Time = 0
            }}

            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Round Trip",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = 1,
                .VehicleMaxDistanceMI = 10000,
                .RT = True,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription()
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            Dim errorString As String = Nothing

            Try
                dataObjectSDRT = route4Me.RunOptimization(optimizationParameters, errorString)
                SDRT_optimization_problem_id = dataObjectSDRT.OptimizationProblemId
                SDRT_route = If((dataObjectSDRT IsNot Nothing AndAlso dataObjectSDRT.Routes IsNot Nothing AndAlso dataObjectSDRT.Routes.Length > 0), dataObjectSDRT.Routes(0), Nothing)
                SDRT_route_id = If((SDRT_route IsNot Nothing), SDRT_route.RouteID, Nothing)
                Return True
            Catch ex As Exception
                Console.WriteLine("Single Driver Round Trip generation failed... " & ex.Message)
                Return False
            End Try
        End Function

#End Region

        Private Sub PrintExampleActivities(ByVal activities As Activity(), ByVal Optional errorString As String = "")
            Dim testName As String = (New System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name

            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If activities IsNot Nothing Then
                Console.WriteLine("The test {0} executed successfully, " & "{1} activities returned", testName, activities.Length)
                Console.WriteLine("")

                activities.ToList().
                    ForEach(Sub(activity)
                                Console.WriteLine("Activity ID: {0}", activity.ActivityId)
                            End Sub)

                Console.WriteLine("")
            Else
                Console.WriteLine("{0} error: {1}", testName, errorString)
            End If
        End Sub

        Public Sub PrintExampleGeocodings(ByVal result As Object, ByVal printOption As GeocodingPrintOption, ByVal errorString As String)

            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Select Case printOption
                Case GeocodingPrintOption.Geocodings
                    Console.WriteLine("")

                    If result IsNot Nothing AndAlso result.ToString().Length > 0 Then
                        Console.WriteLine(testName & " executed successfully")
                    Else
                        Console.WriteLine(testName & " error: {0}", errorString)
                    End If

                Case GeocodingPrintOption.StreetData,
                     GeocodingPrintOption.StreetService,
                     GeocodingPrintOption.StreetZipCode
                    Console.WriteLine("")

                    If result IsNot Nothing AndAlso result.[GetType]() = GetType(ArrayList) Then
                        Console.WriteLine(testName & " executed successfully")

                        For Each res1 As Dictionary(Of String, String) In CType(result, ArrayList)
                            Console.WriteLine("Zipcode: " & res1("zipcode"))
                            Console.WriteLine("Street name: " & res1("street_name"))
                            Console.WriteLine("---------------------------")
                        Next
                    Else
                        Console.WriteLine(testName & " error: {0}", errorString)
                    End If
            End Select
        End Sub


#Region "Avoidance Zones"
        Private Sub PrintExampleAvoidanceZone(
                            ByVal avoidanceZone As Object,
                            ByVal Optional errorString As String = "")

            Dim testName As String = (New System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name

            Console.WriteLine("")

            If avoidanceZone IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")

                Dim avoidanceZoneId As String = If(avoidanceZone.[GetType]() = GetType(AvoidanceZone), (CType(avoidanceZone, AvoidanceZone)).TerritoryId, avoidanceZone.ToString())

                Console.WriteLine("Territory ID: {0}", avoidanceZoneId)
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

        ''' <summary>
        ''' Remove an avoidance zone
        ''' </summary>
        ''' <param name="avoidanceZoneId">Avoidance zone ID (territory ID)</param>
        ''' <returns>If true, an avoidance zone removed successfully</returns>
        Public Function RemoveAvidanceZone(ByVal avoidanceZoneId As String) As Boolean
            Dim avZoneQuery As AvoidanceZoneQuery = New AvoidanceZoneQuery() With {
                .TerritoryId = avoidanceZoneId
            }

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing
            Dim deleted As Boolean = route4Me.DeleteAvoidanceZone(avZoneQuery, errorString)

            Console.WriteLine("")
            Console.WriteLine(If(deleted, "The avoidance zone " & avZoneQuery.TerritoryId & " removed successfully", "Cannot remove avoidance zone " & avZoneQuery.TerritoryId))

            Return deleted
        End Function

        Private Sub RemoveAvoidanceZones()
            If AvoidanceZonesToRemove IsNot Nothing OrElse AvoidanceZonesToRemove.Count < 1 Then Return

            For Each avZone As String In AvoidanceZonesToRemove
                RemoveAvidanceZone(avZone)
            Next
        End Sub

        Public Sub CreateAvoidanceZone()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim avoidanceZoneParameters = New AvoidanceZoneParameters() With {
                .TerritoryName = "Test Territory",
                .TerritoryColor = "ff0000",
                .Territory = New Territory() With {
                    .Type = TerritoryType.Circle.GetEnumDescription(),
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"}
                }
            }

            Dim errorString As String = Nothing
            avoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, errorString)

            If avoidanceZone IsNot Nothing Then
                If (If(AvoidanceZonesToRemove?.Count, 0)) < 1 Then
                    AvoidanceZonesToRemove = New List(Of String)()
                End If

                AvoidanceZonesToRemove.Add(avoidanceZone.TerritoryId)
            End If

            PrintExampleAvoidanceZone(avoidanceZone, errorString)
        End Sub

#End Region

#Region "Address Book Contacts"
        Public Sub CreateTestContacts()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim contact = New AddressBookContact() With {
                .first_name = "Test FirstName " & (New Random()).[Next]().ToString(),
                .address_1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .cached_lat = 38.024654,
                .cached_lng = -77.338814
            }

            Dim errorString As String = Nothing

            contact1 = route4Me.AddAddressBookContact(contact, errorString)

            Assert.IsNotNull(contact1, "AddAddressBookContactsTest failed... " & errorString)

            Dim location1 As Integer = If(contact1.address_id IsNot Nothing, Convert.ToInt32(contact1.address_id), -1)

            ContactsToRemove = New List(Of String)()

            If location1 > 0 Then ContactsToRemove.Add(location1.ToString())

            Dim dCustom = New Dictionary(Of String, Object)() From {
                {"FirstFieldName1", "FirstFieldValue1"},
                {"FirstFieldName2", "FirstFieldValue2"}
            }

            contact = New AddressBookContact() With {
                .first_name = "Test FirstName " & (New Random()).[Next]().ToString(),
                .address_1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .cached_lat = 38.024654,
                .cached_lng = -77.338814,
                .address_custom_data = dCustom
            }

            contact2 = route4Me.AddAddressBookContact(contact, errorString)

            Assert.IsNotNull(contact2, "AddAddressBookContactsTest failed... " & errorString)

            Dim location2 As Integer = If(contact2.address_id IsNot Nothing, Convert.ToInt32(contact2.address_id), -1)

            If location2 > 0 Then ContactsToRemove.Add(location2.ToString())

            Dim contactParams = New AddressBookContact() With {
                .first_name = "Test FirstName Rem" & (New Random()).[Next]().ToString(),
                .address_1 = "Test Address1 Rem " & (New Random()).[Next]().ToString(),
                .cached_lat = 38.02466,
                .cached_lng = -77.33882
            }

            contactToRemove = route4Me.AddAddressBookContact(contactParams, errorString)

            If contactToRemove IsNot Nothing AndAlso contactToRemove.[GetType]() = GetType(AddressBookContact) Then ContactsToRemove.Add(contactToRemove.address_id.ToString())
        End Sub

        ''' <summary>
        ''' Remove the contacts created in an example.
        ''' </summary>
        Private Sub RemoveTestContacts()
            Dim route4Me = New Route4MeManager(ActualApiKey)
            Dim errorString As String = Nothing

            If ContactsToRemove.Count > 0 Then

                Try
                    If contactToRemove IsNot Nothing Then ContactsToRemove.Add(contactToRemove.address_id.ToString())

                    Dim removed As Boolean = route4Me.RemoveAddressBookContacts(ContactsToRemove.ToArray(), errorString)

                    ContactsToRemove = New List(Of String)()
                Catch ex As Exception
                    Console.WriteLine("Cannot remove test contacts." & Environment.NewLine + ex.Message)
                End Try
            End If
        End Sub

        Private Sub PrintExampleContact(ByVal contacts As Object, ByVal total As UInteger, ByVal Optional errorString As String = "")
            If contacts Is Nothing OrElse (contacts.[GetType]() <> GetType(AddressBookContact) AndAlso contacts.[GetType]() <> GetType(AddressBookContact())) Then
                Console.WriteLine("Wrong contact(s). Cannot print." & Environment.NewLine & errorString)
                Return
            End If

            Console.WriteLine("")

            If contacts.[GetType]() = GetType(AddressBookContact) Then
                Dim resultContact As AddressBookContact = CType(contacts, AddressBookContact)

                Console.WriteLine("AddAddressBookContact executed successfully")
                Console.WriteLine("AddressId: {0}", resultContact.address_id)
                Console.WriteLine("Custom data:")

                For Each cdata In CType(resultContact.address_custom_data, Dictionary(Of String, Object))
                    Console.WriteLine(cdata.Key & ": " + cdata.Value)
                Next
            Else
                Console.WriteLine("GetAddressBookContacts executed successfully, {0} contacts returned, total = {1}", (CType(contacts, AddressBookContact())).Length, total)
                Console.WriteLine("")
            End If
        End Sub

        ''' <summary>
        ''' Console print of a scheduled contact response.
        ''' </summary>
        ''' <param name="contactResponse">Scheduled contact</param>
        ''' <param name="scheduleType">Schedule type 'daily', 'weekly', monthly'</param>
        Private Sub PrintExampleScheduledContact(
                        ByVal contactResponse As AddressBookContact,
                        ByVal scheduleType As String,
                        ByVal Optional errorString As String = "")

            Dim location1 As Integer = If(
                contactResponse.address_id IsNot Nothing,
                Convert.ToInt32(contactResponse.address_id),
                -1)

            If location1 > 0 Then
                ContactsToRemove.Add(location1.ToString())

                Console.WriteLine("A location with the " & scheduleType & " scheduling was created. AddressId: {0}", location1)
            Else
                Console.WriteLine("Creating of a location with " & scheduleType & " scheduling failed." & Environment.NewLine & errorString)
            End If
        End Sub

#End Region

#Region "Address Book Groups"
        Public Sub AddAddressBookGroupToRemoveList(ByVal groupId As String)
            If addressBookGroupsToRemove Is Nothing Then addressBookGroupsToRemove = New List(Of String)()
            addressBookGroupsToRemove.Add(groupId)
        End Sub

        Public Sub CreateAddressBookGroup()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim addressBookGroupRule = New AddressBookGroupRule() With {
                .ID = "address_1",
                .Field = "address_1",
                .[Operator] = "not_equal",
                .Value = "qwerty123456"
            }

            Dim addressBookGroupFilter = New AddressBookGroupFilter() With {
                .Condition = "AND",
                .Rules = New AddressBookGroupRule() {addressBookGroupRule}
            }

            Dim addressBookGroupParameters = New AddressBookGroup() With {
                .groupName = "All Group",
                .groupColor = "92e1c0",
                .Filter = addressBookGroupFilter
            }

            Dim errorString As String = Nothing
            Dim addressBookGroup = route4Me.AddAddressBookGroup(addressBookGroupParameters, errorString)

            If addressBookGroup Is Nothing OrElse addressBookGroup.[GetType]() <> GetType(AddressBookGroup) Then
                Console.WriteLine("Cannot create an address book group." & Environment.NewLine & errorString)
                Return
            End If

            AddAddressBookGroupToRemoveList(addressBookGroup.groupID)
        End Sub

        Public Sub RemoveAddressBookGroups()
            If addressBookGroupsToRemove Is Nothing OrElse addressBookGroupsToRemove.Count < 1 Then Return

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing

            For Each groupId As String In addressBookGroupsToRemove
                Dim addressGroupParams = New AddressBookGroupParameters() With {
                    .GroupId = groupId
                }

                Dim response = route4Me.RemoveAddressBookGroup(addressGroupParams, errorString)

                Console.WriteLine(If((If(response?.Status, False)), "Removed the address book group " & groupId, "Cannot removed the address book group " & groupId))
                Console.WriteLine("")
            Next
        End Sub

#End Region

#Region "Member Configuration Group"
        Public Sub CreateConfigKey()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim parametersArray = New MemberConfigurationParameters() _
            {
                New MemberConfigurationParameters With {
                    .config_key = "Test Config Key",
                    .config_value = "Test Config Value"
                }
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.CreateNewConfigurationKey(parametersArray, errorString)

            Console.WriteLine(If(result.result IsNot Nothing, "Created config key " & "Test Config Key", "Cannot create config key " & "Test Config Key." & Environment.NewLine & errorString))

            If (If(result?.result, Nothing)) IsNot Nothing Then configKeysToRemove.Add("Test Config Key")
        End Sub

        Public Sub PrintConfigKey(ByVal configResponse As MemberConfigurationResponse, ByVal Optional errorString As String = "")
            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If configResponse IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")
                Console.WriteLine("Result: " & configResponse.result)
                Console.WriteLine("Affected: " & configResponse.affected)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

        Public Sub PrintConfigKey(
                ByVal configDataResponse As MemberConfigurationDataResponse,
                ByVal Optional errorString As String = "")

            Dim testName As String = (New System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If configDataResponse IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")
                Console.WriteLine("Result: " & configDataResponse.result)

                For Each mc_data As MemberConfigurationData In configDataResponse.data
                    Console.WriteLine("member_id= " & mc_data.member_id)
                    Console.WriteLine("config_key= " & mc_data.config_key)
                    Console.WriteLine("config_value= " & mc_data.config_value)
                    Console.WriteLine("---------------------------")
                Next
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

        Public Sub RemoveConfigKeys()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing

            For Each configKey As String In configKeysToRemove
                Dim params = New MemberConfigurationParameters With {
                    .config_key = configKey
                }

                Dim result = route4Me.RemoveConfigurationKey(params, errorString)

                PrintConfigKey(result, errorString)
            Next
        End Sub
#End Region

#Region "Address Notes"
        Private Sub PrintExampleAddressNote(ByVal note As Object, ByVal Optional errorString As String = "")
            Console.WriteLine("")

            If note IsNot Nothing AndAlso note.[GetType]() = GetType(AddressNote) Then
                Console.WriteLine("AddAddressNote executed successfully")
                Console.WriteLine("Note ID: {0}", (CType(note, AddressNote)).NoteId)
            Else
                Console.WriteLine("AddAddressNote error: {0}", errorString)
            End If
        End Sub

        Private Sub CreateAddressNote(ByRef routeId As String, ByRef destinationId As Integer?)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunSingleDriverRoundTrip()

            OptimizationsToRemove = New List(Of String)() From {
                SDRT_optimization_problem_id
            }

            routeId = SDRT_route_id
            destinationId = CInt(SDRT_route.Addresses(1).RouteDestinationId)

            Dim lat As Double = If(SDRT_route.Addresses.Length > 1, SDRT_route.Addresses(1).Latitude, 33.132675170898)
            Dim lng As Double = If(SDRT_route.Addresses.Length > 1, SDRT_route.Addresses(1).Longitude, -83.244743347168)

            Dim noteParameters = New NoteParameters() With {
                .RouteId = routeId,
                .AddressId = CInt(destinationId),
                .Latitude = lat,
                .Longitude = lng,
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .ActivityType = StatusUpdateType.DropOff.GetEnumDescription()
            }

            Dim contents As String = "Test Note Contents " & DateTime.Now.ToString()

            Dim errorString As String = Nothing
            Dim note = route4Me.AddAddressNote(noteParameters, contents, errorString)

            PrintExampleAddressNote(note, errorString)
        End Sub

        Private Sub RemoveCustomNoteTypes()
            If CustomNoteTypesToRemove.Count < 1 Then Return

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errString As String = Nothing
            Dim response = route4Me.getAllCustomNoteTypes(errString)

            Dim allCustomNoteTypes As List(Of CustomNoteType) =
                If(
                    (response IsNot Nothing AndAlso response.[GetType]() = GetType(CustomNoteType())),
                    (CType(response, CustomNoteType())).ToList(),
                    Nothing
                )

            If allCustomNoteTypes Is Nothing OrElse allCustomNoteTypes.Count < 1 Then Return

            Dim errorString As String = Nothing

            For Each customNoteType As String In CustomNoteTypesToRemove
                Dim customNoteTypeId As Integer? =
                    If(
                        allCustomNoteTypes.
                        Where(Function(x) x.NoteCustomType = customNoteType).
                        FirstOrDefault()?.NoteCustomTypeID, -1
                    )

                If customNoteTypeId > 0 Then
                    Dim removeResult = route4Me.removeCustomNoteType(CInt(customNoteTypeId), errorString)

                    Console.WriteLine(
                        If(
                            (removeResult IsNot Nothing AndAlso removeResult.[GetType]() = GetType(Integer)),
                            "The custom note type " & customNoteTypeId & " removed",
                            "Cannot remove the custom note type " & customNoteTypeId)
                        )
                End If
            Next
        End Sub

        Private Sub PrintExampleCustomNoteType(ByVal response As Object, ByVal Optional errorString As String = "")
            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If response IsNot Nothing AndAlso response.[GetType]() = GetType(Integer) Then
                Console.WriteLine(testName & " executed successfully")
                Console.WriteLine("Affected custom note types: {0}", CInt(response))
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

        Private Function GetCustomNoteIdByName(ByVal customNoteTypeName As String) As Integer?
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errString As String = Nothing
            Dim response = route4Me.getAllCustomNoteTypes(errString)

            Dim allCustomNoteTypes As List(Of CustomNoteType) =
                If(
                    (response IsNot Nothing AndAlso response.[GetType]() = GetType(CustomNoteType())),
                    (CType(response, CustomNoteType())).ToList(),
                    Nothing
                )

            If allCustomNoteTypes Is Nothing OrElse allCustomNoteTypes.Count < 1 Then Return Nothing

            Dim customNoteTypeId As Integer? =
                If(
                    allCustomNoteTypes.
                        Where(Function(x) x.NoteCustomType = customNoteTypeName).
                        FirstOrDefault()?.
                        NoteCustomTypeID,
                        Nothing
                )

            Return customNoteTypeId
        End Function

        Private Sub CreateCustomNoteType()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim customType As String = "To Do 5"
            Dim customValues As String() = New String() _
            {
            "Pass a package 5", "Pickup package", "Do a service"
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.AddCustomNoteType(customType, customValues, errorString)

            PrintExampleCustomNoteType(response, errorString)

            CustomNoteTypesToRemove = New List(Of String)()

            If response IsNot Nothing AndAlso response.[GetType]() = GetType(Integer) Then
                CustomNoteTypesToRemove.Add("To Do 5")
            End If
        End Sub

#End Region

#Region "Orders"
        Private Sub CreateExampleOrder()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim orderParams = New Order() With {
                .address_1 = "318 S 39th St, Louisville, KY 40212, USA",
                .cached_lat = 38.259326,
                .cached_lng = -85.814979,
                .curbside_lat = 38.259326,
                .curbside_lng = -85.814979,
                .address_alias = "318 S 39th St 40212",
                .address_city = "Louisville",
                .EXT_FIELD_first_name = "Lui",
                .EXT_FIELD_last_name = "Carol",
                .EXT_FIELD_email = "lcarol654@yahoo.com",
                .EXT_FIELD_phone = "897946541",
                .EXT_FIELD_custom_data = New Dictionary(Of String, String)() From {
                    {"order_type", "scheduled order"}
                },
                .day_scheduled_for_YYMMDD = DateTime.Now.ToString("yyyy-MM-dd"),
                .local_time_window_end = 39000,
                .local_time_window_end_2 = 46200,
                .local_time_window_start = 37800,
                .local_time_window_start_2 = 45000,
                .local_timezone_string = "America/New_York",
                .order_icon = "emoji/emoji-bank"
            }

            Dim errorString As String = Nothing
            Dim newOrder = route4Me.AddOrder(orderParams, errorString)

            If newOrder IsNot Nothing Then
                OrdersToRemove.Add(newOrder.order_id.ToString())
                lastCreatedOrder = newOrder
            End If
        End Sub

        Private Sub PrintExampleOrder(ByVal result As Object, ByVal errorString As String)
            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")

                If result.[GetType]() = GetType(Order) Then
                    Console.WriteLine("Order ID: {0}", (CType(result, Order)).order_id)
                ElseIf result.[GetType]() = GetType(Order()) Then

                    For Each ord As Order In CType(result, Order())
                        Console.WriteLine("Order ID: {0}", ord.order_id)
                    Next
                Else

                    If result.[GetType]() = GetType(GetOrdersResponse) Then

                        For Each ord As Order In CType((CType(result, GetOrdersResponse)).Results, Order())
                            Console.WriteLine("Order ID: {0}", ord.order_id)
                        Next
                    ElseIf result.[GetType]() = GetType(SearchOrdersResponse) Then
                        Dim fieldValueList = CType((CType(result, SearchOrdersResponse)).Results, Object())

                        For Each fieldValues As Object In fieldValueList
                            Dim fieldValueString As String = ""

                            For Each fieldValue As Object In CType(fieldValues, Object())
                                fieldValueString += fieldValue.ToString() & ","
                            Next

                            fieldValueString = fieldValueString.TrimEnd(","c)
                            Console.WriteLine("Field values: {0}", fieldValueString)
                        Next

                        Console.WriteLine("")

                        For Each fieldName As String In (CType(result, SearchOrdersResponse)).Fields
                            Console.WriteLine(fieldName)
                        Next
                    Else
                        Console.WriteLine("Wrong orders search response type")
                    End If
                End If
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

        Private Sub RemoveTestOrders()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            If OrdersToRemove Is Nothing OrElse OrdersToRemove.Count < 1 Then Return

            Dim errorString As String = Nothing
            Dim removed As Boolean = route4Me.RemoveOrders(OrdersToRemove.ToArray(), errorString)

            Console.WriteLine("")
            Console.WriteLine(If(removed, "The test orders removed", "Cannot remove the test orders"))

            lastCreatedOrder = Nothing
        End Sub

#End Region

#Region "Order Custom Users Fields"

        Private Sub CreateTestOrderCustomUserField()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
                .OrderCustomFieldName = "CustomField77",
                .OrderCustomFieldLabel = "Custom Field 77",
                .OrderCustomFieldType = "checkbox",
                .OrderCustomFieldTypeInfo = New Dictionary(Of String, Object)() From {
                    {"short_label", "cFl77"},
                    {"description", "This is test order custom field"},
                    {"custom field no", 11}
                }
            }

            Dim errorString As String = Nothing
            Dim orderCustomUserField = route4Me.CreateOrderCustomUserField(orderCustomFieldParams, errorString)

            If (If(orderCustomUserField?.Data?.OrderCustomFieldId, -1)) > 0 Then
                OrderCustomFieldsToRemove.Add(orderCustomUserField.Data.OrderCustomFieldId)
            End If

            PrintOrderCustomField(orderCustomUserField, errorString)
        End Sub

        Private Sub PrintOrderCustomField(ByVal result As Object, ByVal Optional errorString As String = "")
            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")

                If result.[GetType]() = GetType(OrderCustomFieldCreateResponse) Then
                    Dim ocfResponse = CType(result, OrderCustomFieldCreateResponse)

                    If (If(ocfResponse?.Data?.OrderCustomFieldId, -1)) > 0 Then
                        Console.WriteLine(
                            "Order Custom user field ID: {0}",
                            ocfResponse.Data.OrderCustomFieldId)
                    Else
                        Console.WriteLine(
                            "Order Custom user fields affected: {0}",
                            ocfResponse.Affected)
                    End If
                Else

                    For Each customField In CType(result, OrderCustomField())
                        Console.WriteLine(
                            "Order Custom user field ID: {0}",
                            customField.OrderCustomFieldId)
                    Next
                End If
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

        Private Sub RemoveTestOrderCustomField()
            If OrderCustomFieldsToRemove Is Nothing OrElse OrderCustomFieldsToRemove.Count < 1 Then
                Return
            End If

            Dim route4Me = New Route4MeManager(ActualApiKey)
            Dim errorString As String = Nothing

            For Each fieldId As Integer In OrderCustomFieldsToRemove
                Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
                    .OrderCustomFieldId = fieldId
                }

                Dim response = route4Me.RemoveOrderCustomUserField(
                    orderCustomFieldParams,
                    errorString
                )

                Console.WriteLine(
                    If(
                        If(response?.Affected, 0) > 0,
                        String.Format("The custom field {0} removed.", fieldId),
                        String.Format("Cannot remove the custom field {0}.", fieldId)
                      )
                )
            Next
        End Sub

#End Region

#Region "Territories"

        Private Sub PrintExampleTerritory(ByVal territory As Object, ByVal Optional errorString As String = "")
            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name

            Console.WriteLine("")

            If territory IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")

                If territory.[GetType]() = GetType(TerritoryZone) Then
                    Dim territoryZoneId As String = If(
                        territory.[GetType]() = GetType(TerritoryZone),
                        (CType(territory, TerritoryZone)).TerritoryId,
                        territory.ToString()
                    )

                    Console.WriteLine("Territory ID: {0}", territoryZoneId)
                ElseIf territory.[GetType]() = GetType(AvoidanceZone()) Then
                    Dim territories = CType(territory, AvoidanceZone())

                    For Each terr In territories
                        Console.WriteLine("Territory ID: {0}", terr.TerritoryId)
                    Next
                ElseIf territory.[GetType]() = GetType(TerritoryZone()) Then
                    Dim territories = CType(territory, TerritoryZone())

                    For Each terr In territories
                        Console.WriteLine("Territory ID: {0}", terr.TerritoryId)
                    Next
                Else
                    Console.WriteLine("Unexpected territory type")
                End If
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

        Public Function RemoveTestTerritoryZone(ByVal territoryZoneId As String) As Boolean
            Dim terrZoneQuery = New TerritoryQuery() With {
                .TerritoryId = territoryZoneId
            }

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing
            Dim deleted As Boolean = route4Me.RemoveTerritory(terrZoneQuery, errorString)

            Console.WriteLine("")
            Console.WriteLine(If(
                              deleted,
                              "The territory zone " & terrZoneQuery.TerritoryId & " removed successfully",
                              "Cannot remove territory zone " & terrZoneQuery.TerritoryId)
            )

            Return deleted
        End Function

        Private Sub RemoveTestTerritoryZones()
            If (If(TerritoryZonesToRemove?.Count, 0)) < 1 Then Return

            For Each terrZone As String In TerritoryZonesToRemove
                RemoveTestTerritoryZone(terrZone)
            Next
        End Sub

        Public Sub CreateTerritoryZone()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim territoryZoneParameters = New AvoidanceZoneParameters() With {
                .TerritoryName = "Test Territory",
                .TerritoryColor = "ff0000",
                .Territory = New Territory() With {
                    .Type = TerritoryType.Circle.GetEnumDescription(),
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"}
                }
            }
            Dim errorString As String = Nothing
            territoryZone = route4Me.CreateTerritory(territoryZoneParameters, errorString)

            If territoryZone IsNot Nothing Then
                If (If(TerritoryZonesToRemove?.Count, 0)) < 1 Then
                    TerritoryZonesToRemove = New List(Of String)()
                End If

                TerritoryZonesToRemove.Add(territoryZone.TerritoryId)
            End If

            PrintExampleTerritory(territoryZone, errorString)
        End Sub

#End Region

#Region "Telematics GateWay API"

        Private Sub PrintExampleTelematicsVendor(ByVal result As Object, ByVal errorString As String)
            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")

                If result.[GetType]() = GetType(TelematicsVendorResponse) Then
                    Console.WriteLine("Vendor :" & (CType(result, TelematicsVendorResponse)).Vendor.Name)
                Else
                    For Each vendor In (CType(result, TelematicsVendorsResponse)).Vendors
                        Console.WriteLine("Vendor :" & vendor.Name)
                    Next
                End If
            Else
                Console.WriteLine(testName & " error: {0}", errorString)
            End If
        End Sub

#End Region

    End Class
End Namespace
