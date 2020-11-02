Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples

        Public ActualApiKey As String = R4MeUtils.ReadSetting("actualApiKey")
        Public DemoApiKey As String = R4MeUtils.ReadSetting("demoApiKey")

        Public ContactsToRemove As List(Of String)
        Public RoutesToRemove As List(Of String)
        Public OptimizationsToRemove As List(Of String)

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

        Private Sub PrintExampleOptimizationResult(ByVal exampleName As String, ByVal dataObjectRoute As DataObjectRoute, ByVal errorString As String)
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

                If dataObjectSD10Stops IsNot Nothing And dataObjectSD10Stops.Routes IsNot Nothing And dataObjectSD10Stops.Routes.Length > 0 Then
                    SD10Stops_route = dataObjectSD10Stops.Routes(0)
                    SD10Stops_route_id = SD10Stops_route.RouteID
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

            If OptimizationsToRemove.Count > 0 Then
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

    End Class
End Namespace
