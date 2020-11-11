Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

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

        Public ContactsToRemove As List(Of String)
        Public RoutesToRemove As List(Of String)
        Public OptimizationsToRemove As List(Of String)
        Public addressBookGroupsToRemove As List(Of String)

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


        Private Sub PrintExampleOptimizationResult(exampleName As String, dataObject As DataObject, errorString As String)
            Dim err1 As String
            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("{0} executed successfully", exampleName)
                Console.WriteLine("")

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("State: {0}", dataObject.State)
                For Each err1 In dataObject.UserErrors
                    Console.WriteLine("UserError : '{0}'", err1)
                Next

                Console.WriteLine("")
                For Each address As Address In dataObject.Addresses
                    Console.WriteLine("Address: {0}", address.AddressString)
                    Console.WriteLine("Route ID: {0}", address.RouteId)
                Next
            Else
                Console.WriteLine("{0} error {1}", exampleName, errorString)
            End If
        End Sub

        Private Sub PrintExampleRouteResult(ByVal exampleName As String, ByVal dataObjectRoute As DataObjectRoute, ByVal errorString As String)
            Console.WriteLine("")

            If dataObjectRoute IsNot Nothing Then
                Console.WriteLine("{0} executed successfully", exampleName)
                Console.WriteLine("")
                Console.WriteLine("Optimization Problem ID: {0}", dataObjectRoute.OptimizationProblemId)
                Console.WriteLine("")

                dataObjectRoute.Addresses.ToList().
                    ForEach(Sub(address)
                                Console.WriteLine("Address: {0}", address.AddressString)
                                Console.WriteLine("Route ID: {0}", address.RouteId)
                            End Sub)
            Else
                Console.WriteLine("{0} error {1}", exampleName, errorString)
            End If
        End Sub

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

        Public Function RunOptimizationSingleDriverRoute10Stops()
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
                .RouteName = "Single Driver Route 10 Stops Test",
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

            PrintExampleAvoidanceZone(avoidanceZone)
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

    End Class
End Namespace
