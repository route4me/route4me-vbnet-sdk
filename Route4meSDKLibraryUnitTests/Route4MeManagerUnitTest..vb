Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Reflection
Imports System.CodeDom.Compiler

<TestClass()> Public Class RoutesGroup
    Shared c_ApiKey As String = "11111111111111111111111111111111"

    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)

    <ClassInitialize> _
    Public Shared Sub RoutesGroupInitialize(context As TestContext)
        lsOptimizationIDs = New List(Of String)()

        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.RunOptimizationSingleDriverRoute10Stops()

        Assert.IsTrue(result, "Single Driver 10 Stops generation failed...")

        Assert.IsTrue(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses...")

        lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id)
    End Sub

    <TestMethod> _
    Public Sub GetRoutesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeParameters As New RouteParametersQuery() With { _
            .Limit = 10, _
            .Offset = 5 _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObjects As DataObjectRoute() = route4Me.GetRoutes(routeParameters, errorString)

        Assert.IsInstanceOfType(dataObjects, GetType(DataObjectRoute()), Convert.ToString("GetRoutesTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeParameters As New RouteParametersQuery() With { _
            .RouteId = tdr.SD10Stops_route_id _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("GetRouteTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetRouteDirectionsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim routeParameters As New RouteParametersQuery() With { _
            .RouteId = routeId _
        }

        routeParameters.Directions = True

        ' Run the query
        Dim errorString As String = ""
        Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("GetRouteDirectionsTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetRoutePathPointsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim routeParameters As New RouteParametersQuery() With { _
            .RouteId = routeId _
        }

        routeParameters.RoutePathOutput = RoutePathOutput.Points.ToString()

        ' Run the query
        Dim errorString As String = ""
        Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("GetRoutePathPointsTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub ResequenceRouteDestinationsTest()
        Dim route As DataObjectRoute = tdr.SD10Stops_route
        Assert.IsNotNull(route, "Route for the test Route Destinations Resequence is null...")

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressesOrderInfo As New AddressesOrderInfo()
        addressesOrderInfo.RouteId = route.RouteID
        addressesOrderInfo.Addresses = New AddressInfo(-1) {}
        For i As Integer = 0 To route.Addresses.Length - 1
            Dim address As Address = route.Addresses(i)
            Dim addressInfo As New AddressInfo()
            addressInfo.DestinationId = address.RouteDestinationId.Value
            addressInfo.SequenceNo = i
            If i = 1 Then
                addressInfo.SequenceNo = 2
            ElseIf i = 2 Then
                addressInfo.SequenceNo = 1
            End If
            addressInfo.IsDepot = (addressInfo.SequenceNo = 0)
            Dim addressesList As New List(Of AddressInfo)(addressesOrderInfo.Addresses)
            addressesList.Add(addressInfo)
            addressesOrderInfo.Addresses = addressesList.ToArray()
        Next

        Dim errorString1 As String = ""
        Dim route1 As DataObjectRoute = route4Me.GetJsonObjectFromAPI(Of DataObjectRoute)(addressesOrderInfo, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, errorString1)
        Assert.IsNotNull(route1, "ResequenceRouteDestinationsTest failed...")
    End Sub

    <TestMethod> _
    Public Sub ResequenceReoptimizeRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim route_id As String = tdr.SD10Stops_route_id

        Assert.IsNotNull(route_id, "rote_id is null...")

        Dim roParameters As New Dictionary(Of String, String)() From { _
            {"route_id", route_id}, _
            {"disable_optimization", "0"}, _
            {"optimize", "Distance"} _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim result As Boolean = route4Me.ResequenceReoptimizeRoute(roParameters, errorString)

        Assert.IsTrue(result, "ResequenceReoptimizeRouteTest failed...")
    End Sub

    <TestMethod> _
    Public Sub UpdateRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim parametersNew As New RouteParameters() With { _
            .RouteName = "New name of the route" _
        }

        Dim routeParameters As New RouteParametersQuery() With { _
            .RouteId = routeId, _
            .Parameters = parametersNew _
        }

        Dim errorString As String = ""
        Dim dataObject As DataObjectRoute = route4Me.UpdateRoute(routeParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("UpdateRouteTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub ReoptimizeRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim routeParameters As New RouteParametersQuery() With { _
            .RouteId = routeId, _
            .ReOptimize = True _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObject As DataObjectRoute = route4Me.UpdateRoute(routeParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("ReoptimizeRouteTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub DuplicateRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId is null...")

        Dim routeParameters As New RouteParametersQuery() With { _
            .RouteId = routeId _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim routeId_DuplicateRoute As String = route4Me.DuplicateRoute(routeParameters, errorString)

        Assert.IsNotNull(routeId_DuplicateRoute, Convert.ToString("DuplicateRouteTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub DeleteRoutesTest()
        Dim routeIdsToDelete As New List(Of String)()

        Dim result As Boolean = tdr.SingleDriverRoundTripTest()

        Assert.IsTrue(result, "Single Driver Round Trip generation failed...")

        If tdr.SDRT_route_id IsNot Nothing Then
            routeIdsToDelete.Add(tdr.SDRT_route_id)
        End If

        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Run the query
        Dim errorString As String = ""
        Dim deletedRouteIds As String() = route4Me.DeleteRoutes(routeIdsToDelete.ToArray(), errorString)

        Assert.IsInstanceOfType(deletedRouteIds, GetType(String()), Convert.ToString("DeleteRoutesTest failed... ") & errorString)
    End Sub

    <ClassCleanup> _
    Public Shared Sub AddressGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the testing optimization problem failed...")
    End Sub

End Class

<TestClass()> Public Class NotesGroup
    Shared c_ApiKey As String = "11111111111111111111111111111111"
    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)

    <ClassInitialize> _
    Public Shared Sub NotesGroupInitialize(context As TestContext)
        Dim route4Me As New Route4MeManager(c_ApiKey)

        lsOptimizationIDs = New List(Of String)()

        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.SingleDriverRoundTripTest()

        Assert.IsTrue(result, "Single Driver Round Trip generation failed...")

        Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses...")

        lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id)
    End Sub

    <TestMethod> _
    Public Sub AddAddressNoteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeIdToMoveTo As String = tdr.SDRT_route_id
        Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null...")

        Dim addressId As Integer = If((tdr.dataObjectSDRT IsNot Nothing AndAlso tdr.SDRT_route IsNot Nothing AndAlso tdr.SDRT_route.Addresses.Length > 1 AndAlso tdr.SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing), tdr.SDRT_route.Addresses(1).RouteDestinationId.Value, 0)

        Dim lat As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Latitude, 33.132675170898)
        Dim lng As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Longitude, -83.244743347168)

        Dim noteParameters As New NoteParameters() With { _
            .RouteId = routeIdToMoveTo, _
            .AddressId = addressId, _
            .Latitude = lat, _
            .Longitude = lng, _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .ActivityType = StatusUpdateType.DropOff.GetEnumDescription() _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim contents As String = "Test Note Contents " + DateTime.Now.ToString()
        Dim note As AddressNote = route4Me.AddAddressNote(noteParameters, contents, errorString)

        Assert.IsNotNull(note, Convert.ToString("AddAddressNoteTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub AddAddressNoteWithFileTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SDRT_route_id

        Assert.IsNotNull(routeId, "routeId_SingleDriverRoundTrip is null...")

        Dim addressId As Integer = If((tdr.dataObjectSDRT IsNot Nothing AndAlso tdr.SDRT_route IsNot Nothing AndAlso tdr.SDRT_route.Addresses.Length > 1 AndAlso tdr.SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing), tdr.SDRT_route.Addresses(1).RouteDestinationId.Value, 0)

        Dim lat As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Latitude, 33.132675170898)
        Dim lng As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Longitude, -83.244743347168)

        Dim noteParameters As New NoteParameters() With { _
            .RouteId = routeId, _
            .AddressId = addressId, _
            .Latitude = lat, _
            .Longitude = lng, _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .ActivityType = StatusUpdateType.DropOff.GetEnumDescription() _
        }

        Dim tempFilePath As String = Nothing
        Using stream As Stream = File.Open("test.png", FileMode.Open)
            Dim tempFiles = New TempFileCollection()
            If True Then
                tempFilePath = tempFiles.AddExtension("png")
                System.Console.WriteLine(tempFilePath)
                Using fileStream As Stream = File.OpenWrite(tempFilePath)
                    stream.CopyTo(fileStream)
                End Using
            End If
        End Using

        'Dim tempFilePath As String = Nothing
        'Using stream As Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Route4MeSDKUnitTest.Resources.test.png")
        '    Dim tempFiles = New TempFileCollection()
        '    If True Then
        '        tempFilePath = tempFiles.AddExtension("png")
        '        System.Console.WriteLine(tempFilePath)
        '        Using fileStream As Stream = File.OpenWrite(tempFilePath)
        '            stream.CopyTo(fileStream)
        '        End Using
        '    End If
        'End Using

        ' Run the query
        Dim errorString As String = ""
        Dim contents As String = "Test Note Contents with Attachment " + DateTime.Now.ToString()
        Dim note As AddressNote = route4Me.AddAddressNote(noteParameters, contents, tempFilePath, errorString)

        Assert.IsNotNull(note, Convert.ToString("AddAddressNoteWithFileTest failed... ") & errorString)

    End Sub

    <TestMethod> _
    Public Sub GetAddressNotesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SDRT_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoundTrip is null...")

        Dim routeDestinationId As Integer = If((tdr.dataObjectSDRT IsNot Nothing AndAlso tdr.SDRT_route IsNot Nothing AndAlso tdr.SDRT_route.Addresses.Length > 1 AndAlso tdr.SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing), tdr.SDRT_route.Addresses(1).RouteDestinationId.Value, 0)

        Dim noteParameters As New NoteParameters() With { _
            .RouteId = routeId, _
            .AddressId = routeDestinationId _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim notes As AddressNote() = route4Me.GetAddressNotes(noteParameters, errorString)

        Assert.IsInstanceOfType(notes, GetType(AddressNote()), Convert.ToString("GetAddressNotesTest failed... ") & errorString)
    End Sub

    <ClassCleanup> _
    Public Shared Sub NotesGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the optimization with 24 stops failed...")
    End Sub

End Class

<TestClass()> Public Class RouteTypesGroup
    Shared c_ApiKey As String = "11111111111111111111111111111111"
    Shared tdr As New TestDataRepository()

    Shared dataObject As DataObject, dataObjectMDMD24 As DataObject

    <TestMethod> _
    Public Sub MultipleDepotMultipleDriverTest()
        ' Create the manager with the api key
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses
        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "3634 W Market St, Fairlawn, OH 44333", _
            .IsDepot = True, _
            .Latitude = 41.135762259364, _
            .Longitude = -81.629313826561, _
            .Time = 300, _
            .TimeWindowStart = 28800, _
            .TimeWindowEnd = 29465 _
        }, New Address() With { _
            .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.135762259364, _
            .Longitude = -81.629313826561, _
            .Time = 300, _
            .TimeWindowStart = 29465, _
            .TimeWindowEnd = 30529 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 30529, _
            .TimeWindowEnd = 33779 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 100, _
            .TimeWindowStart = 33779, _
            .TimeWindowEnd = 33944 _
        }, New Address() With { _
            .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.162971496582, _
            .Longitude = -81.479049682617, _
            .Time = 300, _
            .TimeWindowStart = 33944, _
            .TimeWindowEnd = 34801 _
        }, New Address() With { _
            .AddressString = "1659 Hibbard Dr, Stow, OH 44224", _
            .Latitude = 41.194505989552, _
            .Longitude = -81.443351581693, _
            .Time = 300, _
            .TimeWindowStart = 34801, _
            .TimeWindowEnd = 36366 _
        }, _
            New Address() With { _
            .AddressString = "2705 N River Rd, Stow, OH 44224", _
            .Latitude = 41.145240783691, _
            .Longitude = -81.410247802734, _
            .Time = 300, _
            .TimeWindowStart = 36366, _
            .TimeWindowEnd = 39173 _
        }, New Address() With { _
            .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087", _
            .Latitude = 41.340042114258, _
            .Longitude = -81.421226501465, _
            .Time = 300, _
            .TimeWindowStart = 39173, _
            .TimeWindowEnd = 41617 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148578643799, _
            .Longitude = -81.429229736328, _
            .Time = 300, _
            .TimeWindowStart = 41617, _
            .TimeWindowEnd = 43660 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148578643799, _
            .Longitude = -81.429229736328, _
            .Time = 300, _
            .TimeWindowStart = 43660, _
            .TimeWindowEnd = 46392 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 46392, _
            .TimeWindowEnd = 48389 _
        }, New Address() With { _
            .AddressString = "559 W Aurora Rd, Northfield, OH 44067", _
            .Latitude = 41.315116882324, _
            .Longitude = -81.558746337891, _
            .Time = 50, _
            .TimeWindowStart = 48389, _
            .TimeWindowEnd = 48449 _
        }, _
            New Address() With { _
            .AddressString = "3933 Klein Ave, Stow, OH 44224", _
            .Latitude = 41.169467926025, _
            .Longitude = -81.429420471191, _
            .Time = 300, _
            .TimeWindowStart = 48449, _
            .TimeWindowEnd = 50152 _
        }, New Address() With { _
            .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.136692047119, _
            .Longitude = -81.493492126465, _
            .Time = 300, _
            .TimeWindowStart = 50152, _
            .TimeWindowEnd = 51982 _
        }, New Address() With { _
            .AddressString = "3731 Osage St, Stow, OH 44224", _
            .Latitude = 41.161357879639, _
            .Longitude = -81.42293548584, _
            .Time = 100, _
            .TimeWindowStart = 51982, _
            .TimeWindowEnd = 52180 _
        }, New Address() With { _
            .AddressString = "3731 Osage St, Stow, OH 44224", _
            .Latitude = 41.161357879639, _
            .Longitude = -81.42293548584, _
            .Time = 300, _
            .TimeWindowStart = 52180, _
            .TimeWindowEnd = 54379 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
            .RouteName = "Multiple Depot, Multiple Driver", _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RouteMaxDuration = 86400, _
            .VehicleCapacity = "1", _
            .VehicleMaxDistanceMI = "10000", _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription(), _
            .Metric = Metric.Geodesic _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""
        dataObject = route4Me.RunOptimization(optimizationParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("MultipleDepotMultipleDriverTest failed... ") & errorString)

        tdr.RemoveOptimization(New String() {dataObject.OptimizationProblemId})
    End Sub

    <TestMethod> _
    Public Sub MultipleDepotMultipleDriverTimeWindowTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "455 S 4th St, Louisville, KY 40202", _
            .IsDepot = True, _
            .Latitude = 38.251698, _
            .Longitude = -85.757308, _
            .Time = 300, _
            .TimeWindowStart = 28800, _
            .TimeWindowEnd = 30477 _
        }, New Address() With { _
            .AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214", _
            .Latitude = 38.141598, _
            .Longitude = -85.793846, _
            .Time = 300, _
            .TimeWindowStart = 30477, _
            .TimeWindowEnd = 33406 _
        }, New Address() With { _
            .AddressString = "1407 א53MCCOY, Louisville, KY, 40215", _
            .Latitude = 38.202496, _
            .Longitude = -85.786514, _
            .Time = 300, _
            .TimeWindowStart = 33406, _
            .TimeWindowEnd = 36228 _
        }, New Address() With { _
            .AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215", _
            .Latitude = 38.178844, _
            .Longitude = -85.774864, _
            .Time = 300, _
            .TimeWindowStart = 36228, _
            .TimeWindowEnd = 37518 _
        }, New Address() With { _
            .AddressString = "730 CECIL AVENUE, Louisville, KY, 40211", _
            .Latitude = 38.248684, _
            .Longitude = -85.821121, _
            .Time = 300, _
            .TimeWindowStart = 37518, _
            .TimeWindowEnd = 39550 _
        }, New Address() With { _
            .AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211", _
            .Latitude = 38.251923, _
            .Longitude = -85.800034, _
            .Time = 300, _
            .TimeWindowStart = 39550, _
            .TimeWindowEnd = 41348 _
        }, _
            New Address() With { _
            .AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216", _
            .Latitude = 38.176067, _
            .Longitude = -85.824638, _
            .Time = 300, _
            .TimeWindowStart = 41348, _
            .TimeWindowEnd = 42261 _
        }, New Address() With { _
            .AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215", _
            .Latitude = 38.179806, _
            .Longitude = -85.775558, _
            .Time = 300, _
            .TimeWindowStart = 42261, _
            .TimeWindowEnd = 45195 _
        }, New Address() With { _
            .AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212", _
            .Latitude = 38.259335, _
            .Longitude = -85.815094, _
            .Time = 300, _
            .TimeWindowStart = 45195, _
            .TimeWindowEnd = 46549 _
        }, New Address() With { _
            .AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215", _
            .Latitude = 38.179253, _
            .Longitude = -85.785118, _
            .Time = 300, _
            .TimeWindowStart = 46549, _
            .TimeWindowEnd = 47353 _
        }, New Address() With { _
            .AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214", _
            .Latitude = 38.162472, _
            .Longitude = -85.792854, _
            .Time = 300, _
            .TimeWindowStart = 47353, _
            .TimeWindowEnd = 50924 _
        }, New Address() With { _
            .AddressString = "1661 W HILL ST, Louisville, KY, 40210", _
            .Latitude = 38.229584, _
            .Longitude = -85.783966, _
            .Time = 300, _
            .TimeWindowStart = 50924, _
            .TimeWindowEnd = 51392 _
        }, _
            New Address() With { _
            .AddressString = "3222 KINGSWOOD WAY, Louisville, KY, 40216", _
            .Latitude = 38.210606, _
            .Longitude = -85.822594, _
            .Time = 300, _
            .TimeWindowStart = 51392, _
            .TimeWindowEnd = 52451 _
        }, New Address() With { _
            .AddressString = "1922 PALATKA RD, Louisville, KY, 40214", _
            .Latitude = 38.153767, _
            .Longitude = -85.796783, _
            .Time = 300, _
            .TimeWindowStart = 52451, _
            .TimeWindowEnd = 55631 _
        }, New Address() With { _
            .AddressString = "1314 SOUTH 26TH STREET, Louisville, KY, 40210", _
            .Latitude = 38.235847, _
            .Longitude = -85.796852, _
            .Time = 300, _
            .TimeWindowStart = 55631, _
            .TimeWindowEnd = 58516 _
        }, New Address() With { _
            .AddressString = "2135 MCCLOSKEY AVENUE, Louisville, KY, 40210", _
            .Latitude = 38.218662, _
            .Longitude = -85.789032, _
            .Time = 300, _
            .TimeWindowStart = 58516, _
            .TimeWindowEnd = 61080 _
        }, New Address() With { _
            .AddressString = "1409 PHYLLIS AVE, Louisville, KY, 40215", _
            .Latitude = 38.206154, _
            .Longitude = -85.781387, _
            .Time = 100, _
            .TimeWindowStart = 61080, _
            .TimeWindowEnd = 61504 _
        }, New Address() With { _
            .AddressString = "4504 SUNFLOWER AVE, Louisville, KY, 40216", _
            .Latitude = 38.187511, _
            .Longitude = -85.839149, _
            .Time = 300, _
            .TimeWindowStart = 61504, _
            .TimeWindowEnd = 62061 _
        }, _
            New Address() With { _
            .AddressString = "2512 GREENWOOD AVE, Louisville, KY, 40210", _
            .Latitude = 38.241405, _
            .Longitude = -85.795059, _
            .Time = 300, _
            .TimeWindowStart = 62061, _
            .TimeWindowEnd = 65012 _
        }, New Address() With { _
            .AddressString = "5500 WILKE FARM AVE, Louisville, KY, 40216", _
            .Latitude = 38.166065, _
            .Longitude = -85.863319, _
            .Time = 300, _
            .TimeWindowStart = 65012, _
            .TimeWindowEnd = 67541 _
        }, New Address() With { _
            .AddressString = "3640 LENTZ AVE, Louisville, KY, 40215", _
            .Latitude = 38.193283, _
            .Longitude = -85.786201, _
            .Time = 300, _
            .TimeWindowStart = 67541, _
            .TimeWindowEnd = 69120 _
        }, New Address() With { _
            .AddressString = "1020 BLUEGRASS AVE, Louisville, KY, 40215", _
            .Latitude = 38.17952, _
            .Longitude = -85.780037, _
            .Time = 300, _
            .TimeWindowStart = 69120, _
            .TimeWindowEnd = 70572 _
        }, New Address() With { _
            .AddressString = "123 NORTH 40TH ST, Louisville, KY, 40212", _
            .Latitude = 38.26498, _
            .Longitude = -85.814156, _
            .Time = 300, _
            .TimeWindowStart = 70572, _
            .TimeWindowEnd = 73177 _
        }, New Address() With { _
            .AddressString = "7315 ST ANDREWS WOODS CIRCLE UNIT 104, Louisville, KY, 40214", _
            .Latitude = 38.151072, _
            .Longitude = -85.802867, _
            .Time = 300, _
            .TimeWindowStart = 73177, _
            .TimeWindowEnd = 75231 _
        }, _
            New Address() With { _
            .AddressString = "3210 POPLAR VIEW DR, Louisville, KY, 40216", _
            .Latitude = 38.182594, _
            .Longitude = -85.849937, _
            .Time = 300, _
            .TimeWindowStart = 75231, _
            .TimeWindowEnd = 77663 _
        }, New Address() With { _
            .AddressString = "4519 LOUANE WAY, Louisville, KY, 40216", _
            .Latitude = 38.1754, _
            .Longitude = -85.811447, _
            .Time = 300, _
            .TimeWindowStart = 77663, _
            .TimeWindowEnd = 79796 _
        }, New Address() With { _
            .AddressString = "6812 MANSLICK RD, Louisville, KY, 40214", _
            .Latitude = 38.161839, _
            .Longitude = -85.798279, _
            .Time = 300, _
            .TimeWindowStart = 79796, _
            .TimeWindowEnd = 80813 _
        }, New Address() With { _
            .AddressString = "1524 HUNTOON AVENUE, Louisville, KY, 40215", _
            .Latitude = 38.172031, _
            .Longitude = -85.788353, _
            .Time = 300, _
            .TimeWindowStart = 80813, _
            .TimeWindowEnd = 83956 _
        }, New Address() With { _
            .AddressString = "1307 LARCHMONT AVE, Louisville, KY, 40215", _
            .Latitude = 38.209663, _
            .Longitude = -85.779816, _
            .Time = 300, _
            .TimeWindowStart = 83956, _
            .TimeWindowEnd = 84365 _
        }, New Address() With { _
            .AddressString = "434 N 26TH STREET #2, Louisville, KY, 40212", _
            .Latitude = 38.26844, _
            .Longitude = -85.791962, _
            .Time = 300, _
            .TimeWindowStart = 84365, _
            .TimeWindowEnd = 85367 _
        }, _
            New Address() With { _
            .AddressString = "678 WESTLAWN ST, Louisville, KY, 40211", _
            .Latitude = 38.250397, _
            .Longitude = -85.80629, _
            .Time = 300, _
            .TimeWindowStart = 85367, _
            .TimeWindowEnd = 86400 _
        }, New Address() With { _
            .AddressString = "2308 W BROADWAY, Louisville, KY, 40211", _
            .Latitude = 38.248882, _
            .Longitude = -85.790421, _
            .Time = 300, _
            .TimeWindowStart = 86400, _
            .TimeWindowEnd = 88703 _
        }, New Address() With { _
            .AddressString = "2332 WOODLAND AVE, Louisville, KY, 40210", _
            .Latitude = 38.233579, _
            .Longitude = -85.794257, _
            .Time = 300, _
            .TimeWindowStart = 88703, _
            .TimeWindowEnd = 89320 _
        }, New Address() With { _
            .AddressString = "1706 WEST ST. CATHERINE, Louisville, KY, 40210", _
            .Latitude = 38.239697, _
            .Longitude = -85.783928, _
            .Time = 300, _
            .TimeWindowStart = 89320, _
            .TimeWindowEnd = 90054 _
        }, New Address() With { _
            .AddressString = "1699 WATHEN LN, Louisville, KY, 40216", _
            .Latitude = 38.216465, _
            .Longitude = -85.792397, _
            .Time = 300, _
            .TimeWindowStart = 90054, _
            .TimeWindowEnd = 91150 _
        }, New Address() With { _
            .AddressString = "2416 SUNSHINE WAY, Louisville, KY, 40216", _
            .Latitude = 38.186245, _
            .Longitude = -85.831787, _
            .Time = 300, _
            .TimeWindowStart = 91150, _
            .TimeWindowEnd = 91915 _
        }, _
            New Address() With { _
            .AddressString = "6925 MANSLICK RD, Louisville, KY, 40214", _
            .Latitude = 38.158466, _
            .Longitude = -85.798355, _
            .Time = 300, _
            .TimeWindowStart = 91915, _
            .TimeWindowEnd = 93407 _
        }, New Address() With { _
            .AddressString = "2707 7TH ST, Louisville, KY, 40215", _
            .Latitude = 38.212438, _
            .Longitude = -85.785082, _
            .Time = 300, _
            .TimeWindowStart = 93407, _
            .TimeWindowEnd = 95992 _
        }, New Address() With { _
            .AddressString = "2014 KENDALL LN, Louisville, KY, 40216", _
            .Latitude = 38.179394, _
            .Longitude = -85.826668, _
            .Time = 300, _
            .TimeWindowStart = 95992, _
            .TimeWindowEnd = 99307 _
        }, New Address() With { _
            .AddressString = "612 N 39TH ST, Louisville, KY, 40212", _
            .Latitude = 38.273354, _
            .Longitude = -85.812012, _
            .Time = 300, _
            .TimeWindowStart = 99307, _
            .TimeWindowEnd = 102906 _
        }, New Address() With { _
            .AddressString = "2215 ROWAN ST, Louisville, KY, 40212", _
            .Latitude = 38.261703, _
            .Longitude = -85.786781, _
            .Time = 300, _
            .TimeWindowStart = 102906, _
            .TimeWindowEnd = 106021 _
        }, New Address() With { _
            .AddressString = "1826 W. KENTUCKY ST, Louisville, KY, 40210", _
            .Latitude = 38.241611, _
            .Longitude = -85.78653, _
            .Time = 300, _
            .TimeWindowStart = 106021, _
            .TimeWindowEnd = 107276 _
        }, _
            New Address() With { _
            .AddressString = "1810 GREGG AVE, Louisville, KY, 40210", _
            .Latitude = 38.224716, _
            .Longitude = -85.796211, _
            .Time = 300, _
            .TimeWindowStart = 107276, _
            .TimeWindowEnd = 107948 _
        }, New Address() With { _
            .AddressString = "4103 BURRRELL DRIVE, Louisville, KY, 40216", _
            .Latitude = 38.191753, _
            .Longitude = -85.825836, _
            .Time = 300, _
            .TimeWindowStart = 107948, _
            .TimeWindowEnd = 108414 _
        }, New Address() With { _
            .AddressString = "359 SOUTHWESTERN PKWY, Louisville, KY, 40212", _
            .Latitude = 38.259903, _
            .Longitude = -85.823463, _
            .Time = 200, _
            .TimeWindowStart = 108414, _
            .TimeWindowEnd = 108685 _
        }, New Address() With { _
            .AddressString = "2407 W CHESTNUT ST, Louisville, KY, 40211", _
            .Latitude = 38.252781, _
            .Longitude = -85.792109, _
            .Time = 300, _
            .TimeWindowStart = 108685, _
            .TimeWindowEnd = 110109 _
        }, New Address() With { _
            .AddressString = "225 S 22ND ST, Louisville, KY, 40212", _
            .Latitude = 38.257616, _
            .Longitude = -85.786658, _
            .Time = 300, _
            .TimeWindowStart = 110109, _
            .TimeWindowEnd = 111375 _
        }, New Address() With { _
            .AddressString = "1404 MCCOY AVE, Louisville, KY, 40215", _
            .Latitude = 38.202122, _
            .Longitude = -85.786072, _
            .Time = 300, _
            .TimeWindowStart = 111375, _
            .TimeWindowEnd = 112120 _
        }, _
            New Address() With { _
            .AddressString = "117 FOUNT LANDING CT, Louisville, KY, 40212", _
            .Latitude = 38.270061, _
            .Longitude = -85.799438, _
            .Time = 300, _
            .TimeWindowStart = 112120, _
            .TimeWindowEnd = 114095 _
        }, New Address() With { _
            .AddressString = "5504 SHOREWOOD DRIVE, Louisville, KY, 40214", _
            .Latitude = 38.145851, _
            .Longitude = -85.7798, _
            .Time = 300, _
            .TimeWindowStart = 114095, _
            .TimeWindowEnd = 115743 _
        }, New Address() With { _
            .AddressString = "1406 CENTRAL AVE, Louisville, KY, 40208", _
            .Latitude = 38.211025, _
            .Longitude = -85.780251, _
            .Time = 300, _
            .TimeWindowStart = 115743, _
            .TimeWindowEnd = 117716 _
        }, New Address() With { _
            .AddressString = "901 W WHITNEY AVE, Louisville, KY, 40215", _
            .Latitude = 38.194115, _
            .Longitude = -85.77494, _
            .Time = 300, _
            .TimeWindowStart = 117716, _
            .TimeWindowEnd = 119078 _
        }, New Address() With { _
            .AddressString = "2109 SCHAFFNER AVE, Louisville, KY, 40210", _
            .Latitude = 38.219699, _
            .Longitude = -85.779363, _
            .Time = 300, _
            .TimeWindowStart = 119078, _
            .TimeWindowEnd = 121147 _
        }, New Address() With { _
            .AddressString = "2906 DIXIE HWY, Louisville, KY, 40216", _
            .Latitude = 38.209278, _
            .Longitude = -85.798653, _
            .Time = 300, _
            .TimeWindowStart = 121147, _
            .TimeWindowEnd = 124281 _
        }, _
            New Address() With { _
            .AddressString = "814 WWHITNEY AVE, Louisville, KY, 40215", _
            .Latitude = 38.193596, _
            .Longitude = -85.773521, _
            .Time = 300, _
            .TimeWindowStart = 124281, _
            .TimeWindowEnd = 124675 _
        }, New Address() With { _
            .AddressString = "1610 ALGONQUIN PWKY, Louisville, KY, 40210", _
            .Latitude = 38.222153, _
            .Longitude = -85.784187, _
            .Time = 300, _
            .TimeWindowStart = 124675, _
            .TimeWindowEnd = 127148 _
        }, New Address() With { _
            .AddressString = "3524 WHEELER AVE, Louisville, KY, 40215", _
            .Latitude = 38.195293, _
            .Longitude = -85.788643, _
            .Time = 300, _
            .TimeWindowStart = 127148, _
            .TimeWindowEnd = 130667 _
        }, New Address() With { _
            .AddressString = "5009 NEW CUT RD, Louisville, KY, 40214", _
            .Latitude = 38.165905, _
            .Longitude = -85.779701, _
            .Time = 300, _
            .TimeWindowStart = 130667, _
            .TimeWindowEnd = 131980 _
        }, New Address() With { _
            .AddressString = "3122 ELLIOTT AVE, Louisville, KY, 40211", _
            .Latitude = 38.251213, _
            .Longitude = -85.804199, _
            .Time = 300, _
            .TimeWindowStart = 131980, _
            .TimeWindowEnd = 134402 _
        }, New Address() With { _
            .AddressString = "911 GAGEL AVE, Louisville, KY, 40216", _
            .Latitude = 38.173512, _
            .Longitude = -85.807854, _
            .Time = 300, _
            .TimeWindowStart = 134402, _
            .TimeWindowEnd = 136787 _
        }, _
            New Address() With { _
            .AddressString = "4020 GARLAND AVE #lOOA, Louisville, KY, 40211", _
            .Latitude = 38.246181, _
            .Longitude = -85.818901, _
            .Time = 300, _
            .TimeWindowStart = 136787, _
            .TimeWindowEnd = 138073 _
        }, New Address() With { _
            .AddressString = "5231 MT HOLYOKE DR, Louisville, KY, 40216", _
            .Latitude = 38.169369, _
            .Longitude = -85.85704, _
            .Time = 300, _
            .TimeWindowStart = 138073, _
            .TimeWindowEnd = 141407 _
        }, New Address() With { _
            .AddressString = "1339 28TH S #2, Louisville, KY, 40211", _
            .Latitude = 38.235275, _
            .Longitude = -85.800156, _
            .Time = 300, _
            .TimeWindowStart = 141407, _
            .TimeWindowEnd = 143561 _
        }, New Address() With { _
            .AddressString = "836 S 36TH ST, Louisville, KY, 40211", _
            .Latitude = 38.24651, _
            .Longitude = -85.811234, _
            .Time = 300, _
            .TimeWindowStart = 143561, _
            .TimeWindowEnd = 145941 _
        }, New Address() With { _
            .AddressString = "2132 DUNCAN STREET, Louisville, KY, 40212", _
            .Latitude = 38.262135, _
            .Longitude = -85.785172, _
            .Time = 300, _
            .TimeWindowStart = 145941, _
            .TimeWindowEnd = 148296 _
        }, New Address() With { _
            .AddressString = "3529 WHEELER AVE, Louisville, KY, 40215", _
            .Latitude = 38.195057, _
            .Longitude = -85.787949, _
            .Time = 300, _
            .TimeWindowStart = 148296, _
            .TimeWindowEnd = 150177 _
        }, _
            New Address() With { _
            .AddressString = "2829 DE MEL #11, Louisville, KY, 40214", _
            .Latitude = 38.171662, _
            .Longitude = -85.807271, _
            .Time = 300, _
            .TimeWindowStart = 150177, _
            .TimeWindowEnd = 150981 _
        }, New Address() With { _
            .AddressString = "1325 EARL AVENUE, Louisville, KY, 40215", _
            .Latitude = 38.204556, _
            .Longitude = -85.781555, _
            .Time = 300, _
            .TimeWindowStart = 150981, _
            .TimeWindowEnd = 151854 _
        }, New Address() With { _
            .AddressString = "3632 MANSLICK RD #10, Louisville, KY, 40215", _
            .Latitude = 38.193542, _
            .Longitude = -85.801147, _
            .Time = 300, _
            .TimeWindowStart = 151854, _
            .TimeWindowEnd = 152613 _
        }, New Address() With { _
            .AddressString = "637 S 41ST ST, Louisville, KY, 40211", _
            .Latitude = 38.253632, _
            .Longitude = -85.81897, _
            .Time = 300, _
            .TimeWindowStart = 152613, _
            .TimeWindowEnd = 156131 _
        }, New Address() With { _
            .AddressString = "3420 VIRGINIA AVENUE, Louisville, KY, 40211", _
            .Latitude = 38.238693, _
            .Longitude = -85.811386, _
            .Time = 300, _
            .TimeWindowStart = 156131, _
            .TimeWindowEnd = 157212 _
        }, New Address() With { _
            .AddressString = "3501 MALIBU CT APT 6, Louisville, KY, 40216", _
            .Latitude = 38.166481, _
            .Longitude = -85.825928, _
            .Time = 300, _
            .TimeWindowStart = 157212, _
            .TimeWindowEnd = 158655 _
        }, _
            New Address() With { _
            .AddressString = "4912 DIXIE HWY, Louisville, KY, 40216", _
            .Latitude = 38.170728, _
            .Longitude = -85.826817, _
            .Time = 300, _
            .TimeWindowStart = 158655, _
            .TimeWindowEnd = 159145 _
        }, New Address() With { _
            .AddressString = "7720 DINGLEDELL RD, Louisville, KY, 40214", _
            .Latitude = 38.162472, _
            .Longitude = -85.792854, _
            .Time = 300, _
            .TimeWindowStart = 159145, _
            .TimeWindowEnd = 161831 _
        }, New Address() With { _
            .AddressString = "2123 RATCLIFFE AVE, Louisville, KY, 40210", _
            .Latitude = 38.21978, _
            .Longitude = -85.797615, _
            .Time = 300, _
            .TimeWindowStart = 161831, _
            .TimeWindowEnd = 163705 _
        }, New Address() With { _
            .AddressString = "1321 OAKWOOD AVE, Louisville, KY, 40215", _
            .Latitude = 38.17704, _
            .Longitude = -85.783829, _
            .Time = 300, _
            .TimeWindowStart = 163705, _
            .TimeWindowEnd = 164953 _
        }, New Address() With { _
            .AddressString = "2223 WEST KENTUCKY STREET, Louisville, KY, 40210", _
            .Latitude = 38.242516, _
            .Longitude = -85.790695, _
            .Time = 300, _
            .TimeWindowStart = 164953, _
            .TimeWindowEnd = 166189 _
        }, New Address() With { _
            .AddressString = "8025 GLIMMER WAY #3308, Louisville, KY, 40214", _
            .Latitude = 38.131981, _
            .Longitude = -85.77935, _
            .Time = 300, _
            .TimeWindowStart = 166189, _
            .TimeWindowEnd = 166640 _
        }, _
            New Address() With { _
            .AddressString = "1155 S 28TH ST, Louisville, KY, 40211", _
            .Latitude = 38.238621, _
            .Longitude = -85.799911, _
            .Time = 300, _
            .TimeWindowStart = 166640, _
            .TimeWindowEnd = 168147 _
        }, New Address() With { _
            .AddressString = "840 IROQUOIS AVE, Louisville, KY, 40214", _
            .Latitude = 38.166355, _
            .Longitude = -85.779396, _
            .Time = 300, _
            .TimeWindowStart = 168147, _
            .TimeWindowEnd = 170385 _
        }, New Address() With { _
            .AddressString = "5573 BRUCE AVE, Louisville, KY, 40214", _
            .Latitude = 38.145222, _
            .Longitude = -85.779205, _
            .Time = 300, _
            .TimeWindowStart = 170385, _
            .TimeWindowEnd = 171096 _
        }, New Address() With { _
            .AddressString = "1727 GALLAGHER, Louisville, KY, 40210", _
            .Latitude = 38.239334, _
            .Longitude = -85.784882, _
            .Time = 300, _
            .TimeWindowStart = 171096, _
            .TimeWindowEnd = 171951 _
        }, New Address() With { _
            .AddressString = "1309 CATALPA ST APT 204, Louisville, KY, 40211", _
            .Latitude = 38.236524, _
            .Longitude = -85.801619, _
            .Time = 300, _
            .TimeWindowStart = 171951, _
            .TimeWindowEnd = 172393 _
        }, New Address() With { _
            .AddressString = "1330 ALGONQUIN PKWY, Louisville, KY, 40208", _
            .Latitude = 38.219846, _
            .Longitude = -85.777344, _
            .Time = 300, _
            .TimeWindowStart = 172393, _
            .TimeWindowEnd = 175337 _
        }, _
            New Address() With { _
            .AddressString = "823 SUTCLIFFE, Louisville, KY, 40211", _
            .Latitude = 38.246956, _
            .Longitude = -85.811569, _
            .Time = 300, _
            .TimeWindowStart = 175337, _
            .TimeWindowEnd = 176867 _
        }, New Address() With { _
            .AddressString = "4405 CHURCHMAN AVENUE #2, Louisville, KY, 40215", _
            .Latitude = 38.177768, _
            .Longitude = -85.792545, _
            .Time = 300, _
            .TimeWindowStart = 176867, _
            .TimeWindowEnd = 178051 _
        }, New Address() With { _
            .AddressString = "3211 DUMESNIL ST #1, Louisville, KY, 40211", _
            .Latitude = 38.237789, _
            .Longitude = -85.807968, _
            .Time = 300, _
            .TimeWindowStart = 178051, _
            .TimeWindowEnd = 179083 _
        }, New Address() With { _
            .AddressString = "3904 WEWOKA AVE, Louisville, KY, 40212", _
            .Latitude = 38.270367, _
            .Longitude = -85.813118, _
            .Time = 300, _
            .TimeWindowStart = 179083, _
            .TimeWindowEnd = 181543 _
        }, New Address() With { _
            .AddressString = "660 SO. 42ND STREET, Louisville, KY, 40211", _
            .Latitude = 38.252865, _
            .Longitude = -85.822624, _
            .Time = 300, _
            .TimeWindowStart = 181543, _
            .TimeWindowEnd = 184193 _
        }, New Address() With { _
            .AddressString = "3619  LENTZ  AVE, Louisville, KY, 40215", _
            .Latitude = 38.193249, _
            .Longitude = -85.785492, _
            .Time = 300, _
            .TimeWindowStart = 184193, _
            .TimeWindowEnd = 185853 _
        }, _
            New Address() With { _
            .AddressString = "4305  STOLTZ  CT, Louisville, KY, 40215", _
            .Latitude = 38.178707, _
            .Longitude = -85.787292, _
            .Time = 300, _
            .TimeWindowStart = 185853, _
            .TimeWindowEnd = 187252 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
            .RouteName = "Multiple Depot, Multiple Driver, Time Window", _
            .StoreRoute = False, _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RT = True, _
            .RouteMaxDuration = 86400 * 3, _
            .VehicleCapacity = "99", _
            .VehicleMaxDistanceMI = "99999", _
            .Optimize = Optimize.Time.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription(), _
            .Metric = Metric.Geodesic _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""
        dataObject = route4Me.RunOptimization(optimizationParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("MultipleDepotMultipleDriverTimeWindowTest failed... ") & errorString)

        tdr.RemoveOptimization(New String() {dataObject.OptimizationProblemId})
    End Sub

    <TestMethod> _
    Public Sub SingleDepotMultipleDriverNoTimeWindowTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "40 Mercer st, New York, NY", _
            .IsDepot = True, _
            .Latitude = 40.7213583, _
            .Longitude = -74.0013082, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york, ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Manhatten Island NYC", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "503 W139 St, NY,NY", _
            .Latitude = 40.7109062, _
            .Longitude = -74.0091848, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "203 grand st, new york, ny", _
            .Latitude = 40.718899, _
            .Longitude = -73.996732, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "119 Church Street", _
            .Latitude = 40.7137757, _
            .Longitude = -74.0088238, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "new york ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "broadway street, new york", _
            .Latitude = 40.7191551, _
            .Longitude = -74.0020849, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Ground Zero, Vesey-Liberty-Church-West Streets New York NY 10038", _
            .Latitude = 40.7233126, _
            .Longitude = -74.0116602, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "226 ilyssa way staten lsland ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "185 franklin st.", _
            .Latitude = 40.7192099, _
            .Longitude = -74.009767, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "new york city,", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "11 e. broaway 11038", _
            .Latitude = 40.713206, _
            .Longitude = -73.9974019, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Brooklyn Bridge, NY", _
            .Latitude = 40.7053804, _
            .Longitude = -73.9962503, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "World Trade Center Site, NY", _
            .Latitude = 40.711498, _
            .Longitude = -74.012299, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York Stock Exchange, NY", _
            .Latitude = 40.7074242, _
            .Longitude = -74.0116342, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Wall Street, NY", _
            .Latitude = 40.7079825, _
            .Longitude = -74.0079781, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Trinity Church, NY", _
            .Latitude = 40.7081426, _
            .Longitude = -74.0120511, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "World Financial Center, NY", _
            .Latitude = 40.710475, _
            .Longitude = -74.015493, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Federal Hall, NY", _
            .Latitude = 40.7073034, _
            .Longitude = -74.0102734, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Flatiron Building, NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "South Street Seaport, NY", _
            .Latitude = 40.706921, _
            .Longitude = -74.003638, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Rockefeller Center, NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "FAO Schwarz, NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Woolworth Building, NY", _
            .Latitude = 40.7123903, _
            .Longitude = -74.0083309, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Met Life Building, NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "SOHO/Tribeca, NY", _
            .Latitude = 40.718565, _
            .Longitude = -74.012017, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "MacyГўв‚¬в„ўs, NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "City Hall, NY, NY", _
            .Latitude = 40.7127047, _
            .Longitude = -74.0058663, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Macy&amp;acirc;в‚¬в„ўs, NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "1452 potter blvd bayshore ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "55 Church St. New York, NY", _
            .Latitude = 40.711232, _
            .Longitude = -74.010268, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "55 Church St, New York, NY", _
            .Latitude = 40.711232, _
            .Longitude = -74.010268, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "79 woodlawn dr revena ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "135 main st revena ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "250 greenwich st, new york, ny", _
            .Latitude = 40.713159, _
            .Longitude = -74.011889, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "79 grand, new york, ny", _
            .Latitude = 40.7216958, _
            .Longitude = -74.0024352, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "World trade center" & vbLf, _
            .Latitude = 40.711626, _
            .Longitude = -74.010714, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "World trade centern", _
            .Latitude = 40.713291, _
            .Longitude = -74.011835, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "391 broadway new york", _
            .Latitude = 40.7183693, _
            .Longitude = -74.00278, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Fletcher street", _
            .Latitude = 40.7063954, _
            .Longitude = -74.0056353, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "2 Plum LanenPlainview New York", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "50 Kennedy drivenPlainview New York", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "7 Crestwood DrivenPlainview New York", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "85 west street nyc", _
            .Latitude = 40.709646, _
            .Longitude = -74.014614, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York, New York", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "89 Reade St, New York City, New York 10013", _
            .Latitude = 40.714297, _
            .Longitude = -74.005966, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "100 white st", _
            .Latitude = 40.7172477, _
            .Longitude = -74.0014351, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "100 white st" & vbLf & "33040", _
            .Latitude = 40.7172477, _
            .Longitude = -74.0014351, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Canal st and mulberry", _
            .Latitude = 40.717088, _
            .Longitude = -73.9986025, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "91-83 111st st" & vbLf & "Richmond hills ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "122-09 liberty avenOzone park ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "80-16 101 avenOzone park ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "6302 woodhaven blvdnRego park ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "39-02 64th stnWoodside ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York City, NY,", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Pine st", _
            .Latitude = 40.7069754, _
            .Longitude = -74.0089557, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Wall st", _
            .Latitude = 40.7079825, _
            .Longitude = -74.0079781, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "32 avenue of the Americas, NY, NY", _
            .Latitude = 40.720114, _
            .Longitude = -74.005092, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "260 west broadway, NY, NY", _
            .Latitude = 40.720621, _
            .Longitude = -74.005567, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Long island, ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "27 Carley ave" & vbLf & "Huntington ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "17 west neck RdnHuntington ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "206 washington st", _
            .Latitude = 40.7131577, _
            .Longitude = -74.0126091, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Cipriani new york", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Byshnell Basin. NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "89 Reade St, New York, New York 10013", _
            .Latitude = 40.714297, _
            .Longitude = -74.005966, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "250 Greenwich St, New York, New York 10007", _
            .Latitude = 40.7133, _
            .Longitude = -74.012, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "64 Bowery, New York, New York 10013", _
            .Latitude = 40.716554, _
            .Longitude = -73.99627, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "142-156 Mulberry St, New York, New York 10013", _
            .Latitude = 40.7192764, _
            .Longitude = -73.9973096, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "80 Spring St, New York, New York 10012", _
            .Latitude = 40.722659, _
            .Longitude = -73.998182, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "182 Duane street ny", _
            .Latitude = 40.7170879, _
            .Longitude = -74.010121, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "182 Duane St, New York, New York 10013", _
            .Latitude = 40.7170879, _
            .Longitude = -74.010121, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "462 broome street nyc", _
            .Latitude = 40.72258, _
            .Longitude = -74.000898, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "117 mercer street nyc", _
            .Latitude = 40.7239679, _
            .Longitude = -73.9991585, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Lucca antiques" & vbLf & "182 Duane St, New York, New York 10013", _
            .Latitude = 40.7167516, _
            .Longitude = -74.0087482, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Room and board" & vbLf & "105 Wooster street nyc", _
            .Latitude = 40.7229097, _
            .Longitude = -74.0021852, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Lucca antiquesn182 Duane St, New York, New York 10013", _
            .Latitude = 40.7167516, _
            .Longitude = -74.0087482, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Room and boardn105 Wooster street nyc", _
            .Latitude = 40.7229097, _
            .Longitude = -74.0021852, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Lucca antiques 182 Duane st new York ny", _
            .Latitude = 40.7170879, _
            .Longitude = -74.010121, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Property" & vbLf & "14 Wooster street nyc", _
            .Latitude = 40.7229097, _
            .Longitude = -74.0021852, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "101 Crosby street nyc", _
            .Latitude = 40.723573, _
            .Longitude = -73.996954, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Room and board " & vbLf & "105 Wooster street nyc", _
            .Latitude = 40.7229097, _
            .Longitude = -74.0021852, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Propertyn14 Wooster street nyc", _
            .Latitude = 40.7229097, _
            .Longitude = -74.0021852, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Room and board n105 Wooster street nyc", _
            .Latitude = 40.7229097, _
            .Longitude = -74.0021852, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Mecox gardens" & vbLf & "926 Lexington nyc", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "25 sybil&apos;s crossing Kent lakes, ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "10149 ASHDALE LANE" & vbTab & "67" & vbTab & "67393253" & vbTab & vbTab & vbTab & "SANTEE" & vbTab & "CA" & vbTab & "92071" & vbTab & vbTab & "280501691" & vbTab & "67393253" & vbTab & "IFI" & vbTab & "280501691" & vbTab & "05-JUN-10" & vbTab & "67393253", _
            .Latitude = 40.7143, _
            .Longitude = -74.0067, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "193 Lakebridge Dr, Kings Paark, NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "219 west creek", _
            .Latitude = 40.7198564, _
            .Longitude = -74.0121098, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "14 North Moore Street" & vbLf & "New York, ny", _
            .Latitude = 40.719697, _
            .Longitude = -74.00661, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "14 North Moore StreetnNew York, ny", _
            .Latitude = 40.719697, _
            .Longitude = -74.00661, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "14 North Moore Street New York, ny", _
            .Latitude = 40.719697, _
            .Longitude = -74.00661, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "30-38 Fulton St, New York, New York 10038", _
            .Latitude = 40.7077737, _
            .Longitude = -74.0043299, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "73 Spring Street Ny NY", _
            .Latitude = 40.7225378, _
            .Longitude = -73.9976742, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "119 Mercer Street Ny NY", _
            .Latitude = 40.724139, _
            .Longitude = -73.999311, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "525 Broadway Ny NY", _
            .Latitude = 40.723041, _
            .Longitude = -73.999165, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Church St", _
            .Latitude = 40.7154338, _
            .Longitude = -74.007543, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "135 union stnWatertown ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "21101 coffeen stnWatertown ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "215 Washington stnWatertown ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "619 mill stnWatertown ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "3 canel st, new York, ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york city new york", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "50 grand street", _
            .Latitude = 40.722578, _
            .Longitude = -74.0038019, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Orient ferry, li ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Hilton hotel river head li ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "116 park pl", _
            .Latitude = 40.7140565, _
            .Longitude = -74.0110155, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "long islans new york", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "1 prospect pointe niagra falls ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York City" & vbTab & "NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "pink berry ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York City" & vbTab & " NY", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "10108", _
            .Latitude = 40.7143, _
            .Longitude = -74.0067, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Ann st", _
            .Latitude = 40.7105937, _
            .Longitude = -74.0073715, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Hok 620 ave of Americas new York ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Som 14 wall st nyc", _
            .Latitude = 40.7076179, _
            .Longitude = -74.010763, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York ,ny", _
            .Latitude = 40.7142691, _
            .Longitude = -74.0059729, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "52 prince st. 10012", _
            .Latitude = 40.723584, _
            .Longitude = -73.996117, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "451 broadway 10013", _
            .Latitude = 40.7205177, _
            .Longitude = -74.0009557, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Dover street", _
            .Latitude = 40.7087886, _
            .Longitude = -74.0008644, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Murray st", _
            .Latitude = 40.7148929, _
            .Longitude = -74.0113349, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "85 West St, New York, New York", _
            .Latitude = 40.709646, _
            .Longitude = -74.014614, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "NYC", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "64 trinity place, ny, ny", _
            .Latitude = 40.7081649, _
            .Longitude = -74.0127168, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "150 broadway ny ny", _
            .Latitude = 40.709185, _
            .Longitude = -74.010033, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Pinegrove Dude Ranch 31 cherrytown Rd Kerhinkson Ny", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Front street", _
            .Latitude = 40.706399, _
            .Longitude = -74.0045493, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "234 canal St new York, NY 10013", _
            .Latitude = 40.717701, _
            .Longitude = -73.999957, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "72 spring street, new york ny 10012", _
            .Latitude = 40.7225093, _
            .Longitude = -73.997654, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "150 spring street, new york, ny 10012", _
            .Latitude = 40.7242393, _
            .Longitude = -74.0014922, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "580 broadway street, new york, ny 10012", _
            .Latitude = 40.724421, _
            .Longitude = -73.997026, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "42 trinity place, new york, ny 10007", _
            .Latitude = 40.7074, _
            .Longitude = -74.013551, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "baco ny", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Micro Tel Inn Alburn New York", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "20 Cedar Close", _
            .Latitude = 40.7068734, _
            .Longitude = -74.0078613, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "South street", _
            .Latitude = 40.7080184, _
            .Longitude = -73.9999414, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "47 Lafayette street", _
            .Latitude = 40.7159204, _
            .Longitude = -74.0027332, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Newyork", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Ground Zero, NY", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "GROUND ZERO NY", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "33400 SE Harrison", _
            .Latitude = 40.71884, _
            .Longitude = -74.010333, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york, new york", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "8 Greene St, New York, 10013", _
            .Latitude = 40.720616, _
            .Longitude = -74.00276, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "226 w 44st new york city", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "s street seaport 11 fulton st new york city", _
            .Latitude = 40.706915, _
            .Longitude = -74.0033215, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "30 Rockefeller Plaza w 49th St New York City", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "30 Rockefeller Plaza 50th St New York City", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "S. Street Seaport 11 Fulton St. New York City", _
            .Latitude = 40.706915, _
            .Longitude = -74.0033215, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "30 rockefeller plaza w 49th st, new york city", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "30 rockefeller plaza 50th st, new york city", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "11 fulton st, new york city", _
            .Latitude = 40.706915, _
            .Longitude = -74.0033215, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york city ny", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Big apple", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Ny", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "New York new York", _
            .Latitude = 40.7143528, _
            .Longitude = -74.0059731, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "83-85 Chambers St, New York, New York 10007", _
            .Latitude = 40.714813, _
            .Longitude = -74.006889, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York", _
            .Latitude = 40.7145502, _
            .Longitude = -74.0071249, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "102 North End Ave NY, NY", _
            .Latitude = 40.714798, _
            .Longitude = -74.015969, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "57 Thompson St, New York, New York 10012", _
            .Latitude = 40.72414, _
            .Longitude = -74.003586, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york city", _
            .Latitude = 40.71455, _
            .Longitude = -74.007125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "nyc, ny", _
            .Latitude = 40.7145502, _
            .Longitude = -74.0071249, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York NY", _
            .Latitude = 40.71455, _
            .Longitude = -74.007125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "285 West Broadway New York, NY 10013", _
            .Latitude = 40.720875, _
            .Longitude = -74.004631, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "100 avenue of the americas New York, NY 10013", _
            .Latitude = 40.723312, _
            .Longitude = -74.004395, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "270 Lafeyette st New York, NY 10012", _
            .Latitude = 40.723879, _
            .Longitude = -73.996527, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "560 Broadway New York, NY 10012", _
            .Latitude = 40.723854, _
            .Longitude = -73.997498, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "42 Wooster St New York, NY 10013", _
            .Latitude = 40.722386, _
            .Longitude = -74.002422, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "42 Wooster StreetNew York, NY 10013-2230", _
            .Latitude = 40.7223633, _
            .Longitude = -74.002624, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "504 Broadway, New York, NY 10012", _
            .Latitude = 40.7221444, _
            .Longitude = -73.9992714, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "426 Broome Street, New York, NY 10013", _
            .Latitude = 40.7213295, _
            .Longitude = -73.9987121, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "City hall, nyc", _
            .Latitude = 40.7122066, _
            .Longitude = -74.0055026, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "South street seaport, nyc", _
            .Latitude = 40.7069501, _
            .Longitude = -74.0030848, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Ground zero, nyc", _
            .Latitude = 40.711641, _
            .Longitude = -74.012253, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Ground zero", _
            .Latitude = 40.711641, _
            .Longitude = -74.012253, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Mulberry and canal, NYC", _
            .Latitude = 40.71709, _
            .Longitude = -73.99859, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "World Trade Center, NYC", _
            .Latitude = 40.711667, _
            .Longitude = -74.0125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "South Street Seaport", _
            .Latitude = 40.7069501, _
            .Longitude = -74.0030848, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Wall Street and Nassau Street, NYC", _
            .Latitude = 40.70714, _
            .Longitude = -74.01069, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Trinity Church, NYC", _
            .Latitude = 40.7081269, _
            .Longitude = -74.0125691, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Federal Hall National Memorial", _
            .Latitude = 40.7069515, _
            .Longitude = -74.0101638, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Little Italy, NYC", _
            .Latitude = 40.719692, _
            .Longitude = -73.997765, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York, NY", _
            .Latitude = 40.71455, _
            .Longitude = -74.007125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York City, NY,", _
            .Latitude = 40.71455, _
            .Longitude = -74.007125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york,ny", _
            .Latitude = 40.71455, _
            .Longitude = -74.00713, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Odeon cinema", _
            .Latitude = 40.71683, _
            .Longitude = -74.00803, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York City", _
            .Latitude = 40.71455, _
            .Longitude = -74.00713, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "52 broadway, ny,ny 1004", _
            .Latitude = 40.7065, _
            .Longitude = -74.0123, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "52 broadway, ny,ny 10004", _
            .Latitude = 40.7065, _
            .Longitude = -74.0123, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "22 beaver st, ny,ny 10004", _
            .Latitude = 40.70482, _
            .Longitude = -74.01218, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "54 pine st,ny,ny 10005", _
            .Latitude = 40.70686, _
            .Longitude = -74.00849, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "114 liberty st, ny,ny 10006", _
            .Latitude = 40.70977, _
            .Longitude = -74.0122, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "215 canal st,ny,ny 10013", _
            .Latitude = 40.71747, _
            .Longitude = -73.99895, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york city ny", _
            .Latitude = 40.71455, _
            .Longitude = -74.00713, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "World Trade Center, New York, NY", _
            .Latitude = 40.71167, _
            .Longitude = -74.0125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Chinatown, New York, NY", _
            .Latitude = 40.71596, _
            .Longitude = -73.99741, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "101 murray street new york, ny", _
            .Latitude = 40.71526, _
            .Longitude = -74.01251, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "nyc", _
            .Latitude = 40.71455, _
            .Longitude = -74.00712, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "510 broadway new york", _
            .Latitude = 40.72234, _
            .Longitude = -73.999016, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "nyc", _
            .Latitude = 40.7145502, _
            .Longitude = -74.0071249, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Little Italy", _
            .Latitude = 40.719692, _
            .Longitude = -73.9977647, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "463 Broadway, New York, NY", _
            .Latitude = 40.721059, _
            .Longitude = -74.000688, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "222 West Broadway, New York, NY", _
            .Latitude = 40.719352, _
            .Longitude = -74.006417, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "270 Lafayette street new York new york", _
            .Latitude = 40.723879, _
            .Longitude = -73.996527, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "New York, NY USA", _
            .Latitude = 40.71455, _
            .Longitude = -74.007125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "97 Kenmare Street, New York, NY 10012", _
            .Latitude = 40.721437, _
            .Longitude = -73.996911, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "19 Beekman St, New York, New York 10038", _
            .Latitude = 40.710754, _
            .Longitude = -74.006287, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Soho", _
            .Latitude = 40.7241404, _
            .Longitude = -74.0020213, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Bergen, New York", _
            .Latitude = 40.71455, _
            .Longitude = -74.007125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "478 Broadway, NY, NY", _
            .Latitude = 40.721336, _
            .Longitude = -73.999771, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "555 broadway, ny, ny", _
            .Latitude = 40.723883, _
            .Longitude = -73.998296, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "375 West Broadway, NY, NY", _
            .Latitude = 40.7235, _
            .Longitude = -74.002602, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "35 howard st, NY, NY", _
            .Latitude = 40.719524, _
            .Longitude = -74.00103, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Pier 17 NYC", _
            .Latitude = 40.706366, _
            .Longitude = -74.002689, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "120 Liberty St NYC", _
            .Latitude = 40.709774, _
            .Longitude = -74.012451, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "80 White Street, NY, NY", _
            .Latitude = 40.717834, _
            .Longitude = -74.002052, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "Manhattan, NY", _
            .Latitude = 40.71443, _
            .Longitude = -74.0061, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "22 read st, ny", _
            .Latitude = 40.714201, _
            .Longitude = -74.004491, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "130 Mulberry St, New York, NY 10013-5547", _
            .Latitude = 40.718288, _
            .Longitude = -73.997711, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "new york city, ny", _
            .Latitude = 40.71455, _
            .Longitude = -74.007125, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "10038", _
            .Latitude = 40.7092119, _
            .Longitude = -74.0033631, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "11 Wall St, New York, NY 10005-1905", _
            .Latitude = 40.70729, _
            .Longitude = -74.011201, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "89 Reade St, New York, New York 10007", _
            .Latitude = 40.713456, _
            .Longitude = -74.003499, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "265 Canal St, New York, NY 10013-6010", _
            .Latitude = 40.718885, _
            .Longitude = -74.0009, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "39 Broadway, New York, NY 10006-3003", _
            .Latitude = 40.713345, _
            .Longitude = -73.996132, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "25 beaver street new york ny", _
            .Latitude = 40.705111, _
            .Longitude = -74.012007, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "100 church street new york ny", _
            .Latitude = 40.713043, _
            .Longitude = -74.009637, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "69 Mercer St, New York, NY 10012-4440", _
            .Latitude = 40.722649, _
            .Longitude = -74.00061, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "111 Worth St, New York, NY 10013-4008", _
            .Latitude = 40.715921, _
            .Longitude = -74.00341, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "240-248 Broadway, New York, New York 10038", _
            .Latitude = 40.712769, _
            .Longitude = -74.007681, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "12 Maiden Ln, New York, NY 10038-4002", _
            .Latitude = 40.709446, _
            .Longitude = -74.009576, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "291 Broadway, New York, NY 10007-1814", _
            .Latitude = 40.715, _
            .Longitude = -74.006134, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "55 Liberty St, New York, NY 10005-1003", _
            .Latitude = 40.708843, _
            .Longitude = -74.009384, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "Brooklyn Bridge, NY", _
            .Latitude = 40.706344, _
            .Longitude = -73.997439, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "wall street", _
            .Latitude = 40.7063889, _
            .Longitude = -74.0094444, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "south street seaport, ny", _
            .Latitude = 40.7069501, _
            .Longitude = -74.0030848, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "little italy, ny", _
            .Latitude = 40.719692, _
            .Longitude = -73.9977647, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "47 Pine St, New York, NY 10005-1513", _
            .Latitude = 40.706734, _
            .Longitude = -74.008928, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "22 cortlandt street new york ny", _
            .Latitude = 40.710082, _
            .Longitude = -74.010251, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "105 reade street new york ny", _
            .Latitude = 40.715633, _
            .Longitude = -74.008522, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "2 lafayette street new york ny", _
            .Latitude = 40.714031, _
            .Longitude = -74.003891, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "53 crosby street new york ny", _
            .Latitude = 40.721977, _
            .Longitude = -73.998245, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "2 Lafayette St, New York, NY 10007-1307", _
            .Latitude = 40.714031, _
            .Longitude = -74.003891, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "105 Reade St, New York, NY 10013-3840", _
            .Latitude = 40.715633, _
            .Longitude = -74.008522, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "chinatown, ny", _
            .Latitude = 40.7159556, _
            .Longitude = -73.9974133, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, _
            New Address() With { _
            .AddressString = "250 Broadway, New York, NY 10007-2516", _
            .Latitude = 40.713018, _
            .Longitude = -74.00747, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "156 William St, New York, NY 10038-2609", _
            .Latitude = 40.709797, _
            .Longitude = -74.005577, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "100 Church St, New York, NY 10007-2601", _
            .Latitude = 40.713043, _
            .Longitude = -74.009637, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }, New Address() With { _
            .AddressString = "33 Beaver St, New York, NY 10004-2736", _
            .Latitude = 40.705098, _
            .Longitude = -74.01172, _
            .Time = 0, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
            .RouteName = "Single Depot, Multiple Driver, No Time Window", _
            .StoreRoute = False, _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RT = True, _
            .RouteMaxDuration = 86400, _
            .VehicleCapacity = "20", _
            .VehicleMaxDistanceMI = "99999", _
            .Parts = 4, _
            .Optimize = Optimize.Time.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription(), _
            .Metric = Metric.Geodesic _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String
        dataObject = route4Me.RunOptimization(optimizationParameters, errorString)

        'Note: The addresses of this test aren't permited for api_key=11111111111111111111111111111111. 
        ' If you put in the parameter api_key your valid key, the test will be finished successfuly.

        Assert.IsNull(dataObject, Convert.ToString("SingleDepotMultipleDriverNoTimeWindowTest failed... ") & errorString)

        'tdr.RemoveOptimization(new string[] { dataObject.OptimizationProblemId });
    End Sub

    <TestMethod> _
    Public Sub MultipleDepotMultipleDriverWith24StopsTimeWindowTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "3634 W Market St, Fairlawn, OH 44333", _
            .IsDepot = True, _
            .Latitude = 41.135762259364, _
            .Longitude = -81.629313826561, _
            .Time = 300, _
            .TimeWindowStart = 28800, _
            .TimeWindowEnd = 29465 _
        }, New Address() With { _
            .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.143505096435, _
            .Longitude = -81.46549987793, _
            .Time = 300, _
            .TimeWindowStart = 29465, _
            .TimeWindowEnd = 30529 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 30529, _
            .TimeWindowEnd = 33479 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 33479, _
            .TimeWindowEnd = 33944 _
        }, New Address() With { _
            .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.162971496582, _
            .Longitude = -81.479049682617, _
            .Time = 300, _
            .TimeWindowStart = 33944, _
            .TimeWindowEnd = 34801 _
        }, New Address() With { _
            .AddressString = "1659 Hibbard Dr, Stow, OH 44224", _
            .Latitude = 41.194505989552, _
            .Longitude = -81.443351581693, _
            .Time = 300, _
            .TimeWindowStart = 34801, _
            .TimeWindowEnd = 36366 _
        }, _
            New Address() With { _
            .AddressString = "2705 N River Rd, Stow, OH 44224", _
            .Latitude = 41.145240783691, _
            .Longitude = -81.410247802734, _
            .Time = 300, _
            .TimeWindowStart = 36366, _
            .TimeWindowEnd = 39173 _
        }, New Address() With { _
            .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087", _
            .Latitude = 41.340042114258, _
            .Longitude = -81.421226501465, _
            .Time = 300, _
            .TimeWindowStart = 39173, _
            .TimeWindowEnd = 41617 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148578643799, _
            .Longitude = -81.429229736328, _
            .Time = 300, _
            .TimeWindowStart = 41617, _
            .TimeWindowEnd = 43660 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148579, _
            .Longitude = -81.42923, _
            .Time = 300, _
            .TimeWindowStart = 43660, _
            .TimeWindowEnd = 46392 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 46392, _
            .TimeWindowEnd = 48089 _
        }, New Address() With { _
            .AddressString = "559 W Aurora Rd, Northfield, OH 44067", _
            .Latitude = 41.315116882324, _
            .Longitude = -81.558746337891, _
            .Time = 300, _
            .TimeWindowStart = 48089, _
            .TimeWindowEnd = 48449 _
        }, _
            New Address() With { _
            .AddressString = "3933 Klein Ave, Stow, OH 44224", _
            .Latitude = 41.169467926025, _
            .Longitude = -81.429420471191, _
            .Time = 300, _
            .TimeWindowStart = 48449, _
            .TimeWindowEnd = 50152 _
        }, New Address() With { _
            .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.136692047119, _
            .Longitude = -81.493492126465, _
            .Time = 300, _
            .TimeWindowStart = 50152, _
            .TimeWindowEnd = 51682 _
        }, New Address() With { _
            .AddressString = "3731 Osage St, Stow, OH 44224", _
            .Latitude = 41.161357879639, _
            .Longitude = -81.42293548584, _
            .Time = 300, _
            .TimeWindowStart = 51682, _
            .TimeWindowEnd = 54379 _
        }, New Address() With { _
            .AddressString = "3862 Klein Ave, Stow, OH 44224", _
            .Latitude = 41.167895123363, _
            .Longitude = -81.429973393679, _
            .Time = 300, _
            .TimeWindowStart = 54379, _
            .TimeWindowEnd = 54879 _
        }, New Address() With { _
            .AddressString = "138 Northwood Ln, Tallmadge, OH 44278", _
            .Latitude = 41.085464134812, _
            .Longitude = -81.447411775589, _
            .Time = 300, _
            .TimeWindowStart = 54879, _
            .TimeWindowEnd = 56613 _
        }, New Address() With { _
            .AddressString = "3401 Saratoga Blvd, Stow, OH 44224", _
            .Latitude = 41.148849487305, _
            .Longitude = -81.407363891602, _
            .Time = 300, _
            .TimeWindowStart = 56613, _
            .TimeWindowEnd = 57052 _
        }, _
            New Address() With { _
            .AddressString = "5169 Brockton Dr, Stow, OH 44224", _
            .Latitude = 41.195003509521, _
            .Longitude = -81.392700195312, _
            .Time = 300, _
            .TimeWindowStart = 57052, _
            .TimeWindowEnd = 59004 _
        }, New Address() With { _
            .AddressString = "5169 Brockton Dr, Stow, OH 44224", _
            .Latitude = 41.195003509521, _
            .Longitude = -81.392700195312, _
            .Time = 300, _
            .TimeWindowStart = 59004, _
            .TimeWindowEnd = 60027 _
        }, New Address() With { _
            .AddressString = "458 Aintree Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.1266746521, _
            .Longitude = -81.445808410645, _
            .Time = 300, _
            .TimeWindowStart = 60027, _
            .TimeWindowEnd = 60375 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 60375, _
            .TimeWindowEnd = 63891 _
        }, New Address() With { _
            .AddressString = "2299 Tyre Dr, Hudson, OH 44236", _
            .Latitude = 41.250511169434, _
            .Longitude = -81.420433044434, _
            .Time = 300, _
            .TimeWindowStart = 63891, _
            .TimeWindowEnd = 65277 _
        }, New Address() With { _
            .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.136692047119, _
            .Longitude = -81.493492126465, _
            .Time = 300, _
            .TimeWindowStart = 65277, _
            .TimeWindowEnd = 68545 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
            .RouteName = "Multiple Depot, Multiple Driver with 24 Stops, Time Window", _
            .StoreRoute = False, _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RouteMaxDuration = 86400, _
            .VehicleCapacity = "1", _
            .VehicleMaxDistanceMI = "10000", _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription(), _
            .Metric = Metric.Geodesic _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""
        dataObjectMDMD24 = route4Me.RunOptimization(optimizationParameters, errorString)

        Assert.IsNotNull(dataObjectMDMD24, Convert.ToString("MultipleDepotMultipleDriverWith24StopsTimeWindowTest failed... ") & errorString)

        MoveDestinationToRouteTest()

        MergeRoutesTest()
    End Sub

    Public Sub MoveDestinationToRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Assert.IsNotNull(dataObjectMDMD24, "dataObjectMDMD24 is null...")

        Assert.IsTrue(dataObjectMDMD24.Routes.Length >= 2, "There is no 2 routes for moving a destination to other route...")

        Dim route1 As DataObjectRoute = dataObjectMDMD24.Routes(0)

        Assert.IsTrue(route1.Addresses.Length >= 2, "There is less than 2 addresses in the generated route...")

        Dim routeDestinationIdToMove As Integer = If(route1.Addresses(1).RouteDestinationId IsNot Nothing, Convert.ToInt32(route1.Addresses(1).RouteDestinationId), -1)

        Assert.IsTrue(routeDestinationIdToMove > 0, "Wrong destination_id to move: " & routeDestinationIdToMove)

        Dim route2 As DataObjectRoute = dataObjectMDMD24.Routes(1)

        Assert.IsTrue(route1.Addresses.Length >= 2, "There is less than 2 addresses in the generated route...")

        Dim afterDestinationIdToMoveAfter As Integer = If(route2.Addresses(1).RouteDestinationId IsNot Nothing, Convert.ToInt32(route2.Addresses(1).RouteDestinationId), -1)

        Assert.IsTrue(afterDestinationIdToMoveAfter > 0, "Wrong destination_id to move after: " & afterDestinationIdToMoveAfter)

        Dim errorString As String = ""

        Dim result As Boolean = route4Me.MoveDestinationToRoute(route2.RouteID, routeDestinationIdToMove, afterDestinationIdToMoveAfter, errorString)

        Assert.IsTrue(result, Convert.ToString("MoveDestinationToRouteTest failed... ") & errorString)
    End Sub

    Public Sub MergeRoutesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Assert.IsNotNull(dataObjectMDMD24, "dataObjectMDMD24 is null...")

        Assert.IsTrue(dataObjectMDMD24.Routes.Length >= 2, "There is no 2 routes for moving a destination to other route...")

        Dim route1 As DataObjectRoute = dataObjectMDMD24.Routes(0)

        Assert.IsTrue(route1.Addresses.Length >= 2, "There is less than 2 addresses in the generated route...")

        Dim route2 As DataObjectRoute = dataObjectMDMD24.Routes(1)

        Dim mergeRoutesParameters As New MergeRoutesQuery() With { _
            .RouteIds = route1.RouteID + "," + route2.RouteID, _
            .DepotAddress = route1.Addresses(0).AddressString.ToString(), _
            .RemoveOrigin = False, _
            .DepotLat = route1.Addresses(0).Latitude, _
            .DepotLng = route1.Addresses(0).Longitude _
        }

        Dim errorString As String = ""
        Dim result As Boolean = route4Me.MergeRoutes(mergeRoutesParameters, errorString)

        Assert.IsTrue(result, Convert.ToString("MergeRoutesTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SingleDriverMultipleTimeWindowsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "3634 W Market St, Fairlawn, OH 44333", _
            .IsDepot = True, _
            .Latitude = 41.135762259364, _
            .Longitude = -81.629313826561, _
            .TimeWindowStart = Nothing, _
            .TimeWindowEnd = Nothing, _
            .TimeWindowStart2 = Nothing, _
            .TimeWindowEnd2 = Nothing, _
            .Time = Nothing _
        }, New Address() With { _
            .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.135762259364, _
            .Longitude = -81.629313826561, _
            .TimeWindowStart = 6 * 3600 + 0 * 60, _
            .TimeWindowEnd = 6 * 3600 + 30 * 60, _
            .TimeWindowStart2 = 7 * 3600 + 0 * 60, _
            .TimeWindowEnd2 = 7 * 3600 + 20 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .TimeWindowStart = 7 * 3600 + 30 * 60, _
            .TimeWindowEnd = 7 * 3600 + 40 * 60, _
            .TimeWindowStart2 = 8 * 3600 + 0 * 60, _
            .TimeWindowEnd2 = 8 * 3600 + 10 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .TimeWindowStart = 8 * 3600 + 30 * 60, _
            .TimeWindowEnd = 8 * 3600 + 40 * 60, _
            .TimeWindowStart2 = 8 * 3600 + 50 * 60, _
            .TimeWindowEnd2 = 9 * 3600 + 0 * 60, _
            .Time = 100 _
        }, New Address() With { _
            .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.162971496582, _
            .Longitude = -81.479049682617, _
            .TimeWindowStart = 9 * 3600 + 0 * 60, _
            .TimeWindowEnd = 9 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 9 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 9 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "1659 Hibbard Dr, Stow, OH 44224", _
            .Latitude = 41.194505989552, _
            .Longitude = -81.443351581693, _
            .TimeWindowStart = 10 * 3600 + 0 * 60, _
            .TimeWindowEnd = 10 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 10 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 10 * 3600 + 45 * 60, _
            .Time = 300 _
        }, _
            New Address() With { _
            .AddressString = "2705 N River Rd, Stow, OH 44224", _
            .Latitude = 41.145240783691, _
            .Longitude = -81.410247802734, _
            .TimeWindowStart = 11 * 3600 + 0 * 60, _
            .TimeWindowEnd = 11 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 11 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 11 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087", _
            .Latitude = 41.340042114258, _
            .Longitude = -81.421226501465, _
            .TimeWindowStart = 12 * 3600 + 0 * 60, _
            .TimeWindowEnd = 12 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 12 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 12 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148578643799, _
            .Longitude = -81.429229736328, _
            .TimeWindowStart = 13 * 3600 + 0 * 60, _
            .TimeWindowEnd = 13 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 13 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 13 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148578643799, _
            .Longitude = -81.429229736328, _
            .TimeWindowStart = 14 * 3600 + 0 * 60, _
            .TimeWindowEnd = 14 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 14 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 14 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .TimeWindowStart = 15 * 3600 + 0 * 60, _
            .TimeWindowEnd = 15 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 15 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 15 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "559 W Aurora Rd, Northfield, OH 44067", _
            .Latitude = 41.315116882324, _
            .Longitude = -81.558746337891, _
            .TimeWindowStart = 16 * 3600 + 0 * 60, _
            .TimeWindowEnd = 16 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 16 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 17 * 3600 + 0 * 60, _
            .Time = 50 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.TSP, _
            .StoreRoute = False, _
            .RouteName = "Single Driver Multiple TimeWindows 12 Stops", _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 5 * 3600 + 30 * 60, _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription() _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""
        dataObject = route4Me.RunOptimization(optimizationParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("SingleDriverMultipleTimeWindowsTest failed... ") & errorString)

        tdr.RemoveOptimization(New String() {dataObject.OptimizationProblemId})
    End Sub

    <TestMethod> _
    Public Sub SingleDriverRoundTripGenericTest()
        Const uri As String = R4MEInfrastructureSettings.MainHost + "/api.v4/optimization_problem.php"
        Const myApiKey As String = "11111111111111111111111111111111"

        ' Create the manager with the api key
        Dim route4Me As New Route4MeManager(myApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "754 5th Ave New York, NY 10019", _
            .[Alias] = "Bergdorf Goodman", _
            .IsDepot = True, _
            .Latitude = 40.7636197, _
            .Longitude = -73.9744388, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "717 5th Ave New York, NY 10022", _
            .[Alias] = "Giorgio Armani", _
            .Latitude = 40.7669692, _
            .Longitude = -73.9693864, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "888 Madison Ave New York, NY 10014", _
            .[Alias] = "Ralph Lauren Women's and Home", _
            .Latitude = 40.7715154, _
            .Longitude = -73.9669241, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "1011 Madison Ave New York, NY 10075", _
            .[Alias] = "Yigal Azrou'l", _
            .Latitude = 40.7772129, _
            .Longitude = -73.9669, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "440 Columbus Ave New York, NY 10024", _
            .[Alias] = "Frank Stella Clothier", _
            .Latitude = 40.7808364, _
            .Longitude = -73.9732729, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "324 Columbus Ave #1 New York, NY 10023", _
            .[Alias] = "Liana", _
            .Latitude = 40.7803123, _
            .Longitude = -73.9793079, _
            .Time = 0 _
        }, _
            New Address() With { _
            .AddressString = "110 W End Ave New York, NY 10023", _
            .[Alias] = "Toga Bike Shop", _
            .Latitude = 40.7753077, _
            .Longitude = -73.9861529, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "555 W 57th St New York, NY 10019", _
            .[Alias] = "BMW of Manhattan", _
            .Latitude = 40.7718005, _
            .Longitude = -73.9897716, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "57 W 57th St New York, NY 10019", _
            .[Alias] = "Verizon Wireless", _
            .Latitude = 40.7558695, _
            .Longitude = -73.9862019, _
            .Time = 0 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.TSP, _
            .StoreRoute = False, _
            .RouteName = "Single Driver Round Trip", _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RouteMaxDuration = 86400, _
            .VehicleCapacity = "1", _
            .VehicleMaxDistanceMI = "10000", _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription() _
        }

        Dim myParameters As New MyAddressAndParametersHolder() With { _
            .addresses = addresses, _
            .parameters = parameters _
        }

        ' Run the query
        Dim errorString As String
        Dim dataObject0 As MyDataObjectGeneric = route4Me.GetJsonObjectFromAPI(Of MyDataObjectGeneric)(myParameters, uri, HttpMethodType.Post, errorString)

        Assert.IsNotNull(dataObject0, "SingleDriverRoundTripGenericTest failed...")

        tdr.RemoveOptimization(New String() {dataObject0.OptimizationProblemId})
    End Sub

    <TestMethod> _
    Public Sub SingleDriverRoundTripTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "754 5th Ave New York, NY 10019", _
            .[Alias] = "Bergdorf Goodman", _
            .IsDepot = True, _
            .Latitude = 40.7636197, _
            .Longitude = -73.9744388, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "717 5th Ave New York, NY 10022", _
            .[Alias] = "Giorgio Armani", _
            .Latitude = 40.7669692, _
            .Longitude = -73.9693864, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "888 Madison Ave New York, NY 10014", _
            .[Alias] = "Ralph Lauren Women's and Home", _
            .Latitude = 40.7715154, _
            .Longitude = -73.9669241, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "1011 Madison Ave New York, NY 10075", _
            .[Alias] = "Yigal Azrou'l", _
            .Latitude = 40.7772129, _
            .Longitude = -73.9669, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "440 Columbus Ave New York, NY 10024", _
            .[Alias] = "Frank Stella Clothier", _
            .Latitude = 40.7808364, _
            .Longitude = -73.9732729, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "324 Columbus Ave #1 New York, NY 10023", _
            .[Alias] = "Liana", _
            .Latitude = 40.7803123, _
            .Longitude = -73.9793079, _
            .Time = 0 _
        }, _
            New Address() With { _
            .AddressString = "110 W End Ave New York, NY 10023", _
            .[Alias] = "Toga Bike Shop", _
            .Latitude = 40.7753077, _
            .Longitude = -73.9861529, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "555 W 57th St New York, NY 10019", _
            .[Alias] = "BMW of Manhattan", _
            .Latitude = 40.7718005, _
            .Longitude = -73.9897716, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "57 W 57th St New York, NY 10019", _
            .[Alias] = "Verizon Wireless", _
            .Latitude = 40.7558695, _
            .Longitude = -73.9862019, _
            .Time = 0 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.TSP, _
            .StoreRoute = False, _
            .RouteName = "Single Driver Round Trip", _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RouteMaxDuration = 86400, _
            .VehicleCapacity = "1", _
            .VehicleMaxDistanceMI = "10000", _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription() _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""
        dataObject = route4Me.RunOptimization(optimizationParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("SingleDriverRoundTripTest failed... ") & errorString)

        tdr.RemoveOptimization(New String() {dataObject.OptimizationProblemId})
    End Sub

    <TestMethod> _
    Public Sub RunOptimizationSingleDriverRoute10StopsTest()
        Dim r4mm As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "151 Arbor Way Milledgeville GA 31061", _
            .IsDepot = True, _
            .Latitude = 33.132675170898, _
            .Longitude = -83.244743347168, _
            .Time = 0, _
            .CustomFields = New Dictionary(Of String, String)() From { _
                {"color", "red"}, _
                {"size", "huge"} _
            } _
        }, New Address() With { _
            .AddressString = "230 Arbor Way Milledgeville GA 31061", _
            .Latitude = 33.129695892334, _
            .Longitude = -83.24577331543, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "148 Bass Rd NE Milledgeville GA 31061", _
            .Latitude = 33.143497, _
            .Longitude = -83.224487, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.141784667969, _
            .Longitude = -83.237518310547, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.141086578369, _
            .Longitude = -83.238258361816, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "131 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.142036437988, _
            .Longitude = -83.238845825195, _
            .Time = 0 _
        }, _
            New Address() With { _
            .AddressString = "138 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.14307, _
            .Longitude = -83.239334, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "139 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.142734527588, _
            .Longitude = -83.237442016602, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "145 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.143871307373, _
            .Longitude = -83.237342834473, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "221 Blake Cir Milledgeville GA 31061", _
            .Latitude = 33.081462860107, _
            .Longitude = -83.208511352539, _
            .Time = 0 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.TSP, _
            .StoreRoute = False, _
            .RouteName = "Single Driver Route 10 Stops Test", _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription() _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String
        Dim dataObject1 As DataObject = r4mm.RunOptimization(optimizationParameters, errorString)

        Assert.IsNotNull(dataObject1, Convert.ToString("Run optimization test with Single Driver Route 10 Stops failed... ") & errorString)

        tdr.RemoveOptimization(New String() {dataObject1.OptimizationProblemId})

    End Sub

    <ClassCleanup> _
    Public Shared Sub RouteTypesGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(New String() {dataObjectMDMD24.OptimizationProblemId})

        Assert.IsTrue(result, "Removing of the optimization with 24 stops failed...")
    End Sub

End Class

<TestClass()> Public Class AddressbookContactsGroup
    Shared c_ApiKey As String = "11111111111111111111111111111111"

    Shared contact1 As AddressBookContact, contact2 As AddressBookContact

    Shared lsRemoveContacts As New List(Of Integer)()

    <ClassInitialize> _
    Public Shared Sub AddAddressBookContactsTest(context As TestContext)
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim contact As New AddressBookContact() With { _
            .first_name = "Test FirstName " + (New Random()).[Next]().ToString(), _
            .address_1 = "Test Address1 " + (New Random()).[Next]().ToString(), _
            .cached_lat = 38.024654, _
            .cached_lng = -77.338814 _
        }

        ' Run the query
        Dim errorString As String = ""
        contact1 = route4Me.AddAddressBookContact(contact, errorString)

        Assert.IsNotNull(contact1, Convert.ToString("AddAddressBookContactsTest failed... ") & errorString)

        Dim location1 As Integer = If(contact1.address_id IsNot Nothing, Convert.ToInt32(contact1.address_id), -1)
        If location1 > 0 Then
            lsRemoveContacts.Add(location1)
        End If

        contact = New AddressBookContact() With { _
            .first_name = "Test FirstName " + (New Random()).[Next]().ToString(), _
            .address_1 = "Test Address1 " + (New Random()).[Next]().ToString(), _
            .cached_lat = 38.024654, _
            .cached_lng = -77.338814 _
        }

        contact2 = route4Me.AddAddressBookContact(contact, errorString)

        Assert.IsNotNull(contact2, Convert.ToString("AddAddressBookContactsTest failed... ") & errorString)

        Dim location2 As Integer = If(contact2.address_id IsNot Nothing, Convert.ToInt32(contact2.address_id), -1)
        If location2 > 0 Then
            lsRemoveContacts.Add(location2)
        End If
    End Sub

    <TestMethod> _
    Public Sub UpdateAddressBookContactTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Assert.IsNotNull(contact1, "contact1 is null..")

        contact1.address_group = "Updated"
        ' Run the query
        Dim errorString As String = ""
        Dim updatedContact As AddressBookContact = route4Me.UpdateAddressBookContact(contact1, errorString)

        Assert.IsNotNull(updatedContact, Convert.ToString("UpdateAddressBookContactTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchLocationsByTextTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressBookParameters As New AddressBookParameters() With { _
            .Query = "Test Address1", _
            .Offset = 0, _
            .Limit = 20 _
        }

        ' Run the query
        Dim total As UInteger = 0
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("SearchLocationsByTextTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchLocationsByIDsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Assert.IsNotNull(contact1, "contact1 is null...")
        Assert.IsNotNull(contact2, "contact2 is null...")

        Dim addresses As String = contact1.address_id & "," & contact2.address_id

        Dim addressBookParameters As New AddressBookParameters() With { _
            .AddressId = addresses _
        }

        ' Run the query
        Dim total As UInteger = 0
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("SearchLocationsByIDsTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetSpecifiedFieldsSearchTextTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressBookParameters As New AddressBookParameters() With { _
            .Query = "Test Address1", _
            .Fields = "first_name,address_email", _
            .Offset = 0, _
            .Limit = 20 _
        }

        ' Run the query
        Dim total As UInteger = 0
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("GetSpecifiedFieldsSearchTextTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetAddressBookContactsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressBookParameters As New AddressBookParameters() With { _
            .Limit = 10, _
            .Offset = 0 _
        }

        ' Run the query
        Dim total As UInteger
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("GetAddressBookContactsTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub RemoveAllAddressbookContactsTest()
        Dim ApiKey As String = "bd48828717021141485a701453273458"
        Dim route4Me As New Route4MeManager(ApiKey)
        Dim tdr As New TestDataRepository()

        Dim blContinue As Boolean = True

        Dim iCurOffset As Integer = 0
        Dim lsAddresses As New List(Of String)()

        While blContinue
            Dim addressBookParameters As New AddressBookParameters() With { _
                .Limit = 40, _
                .Offset = CUInt(iCurOffset) _
            }

            Dim total As UInteger
            Dim errorString As String = ""
            Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)
            If contacts.Length = 0 Then
                blContinue = False
            End If
            Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("Getting of the contacts failed...") & errorString)

            For Each contact As AddressBookContact In contacts
                lsAddresses.Add(contact.address_id.ToString())
            Next

            tdr.RemoveAddressBookContacts(lsAddresses, ApiKey)

            iCurOffset += 40
        End While
    End Sub

    <TestMethod> _
    Public Sub SearchRoutedLocationsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressBookParameters As New AddressBookParameters() With { _
            .Display = "routed", _
            .Offset = 0, _
            .Limit = 20 _
        }

        ' Run the query
        Dim total As UInteger = 0
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("SearchRoutedLocationsTest failed... ") & errorString)
    End Sub

    <TestMethod> _
<ClassCleanup> _
    Public Shared Sub RemoveAddressBookContactsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim lsRemLocations As New List(Of String)()
        If lsRemoveContacts.Count > 0 Then
            For Each loc1 As Integer In lsRemoveContacts
                lsRemLocations.Add(loc1.ToString())
            Next

            Dim errorString As String = ""
            Dim removed As Boolean = route4Me.RemoveAddressBookContacts(lsRemLocations.ToArray(), errorString)

            Assert.IsTrue(removed, Convert.ToString("RemoveAddressBookContactsTest failed... ") & errorString)
        End If
    End Sub

End Class

<TestClass()> Public Class AvoidanseZonesGroup
    Shared c_ApiKey As String = "11111111111111111111111111111111"

    Shared lsAvoidanceZones As New List(Of String)()

    <TestMethod> _
<TestInitialize> _
    Public Sub AddAvoidanceZonesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim circleAvoidanceZoneParameters As New AvoidanceZoneParameters() With { _
            .TerritoryName = "Test Circle Territory", _
            .TerritoryColor = "ff0000", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Circle.GetEnumDescription(), _
                .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"} _
            } _
        }

        Dim errorString As String = ""
        Dim circleAvoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(circleAvoidanceZoneParameters, errorString)

        If circleAvoidanceZone IsNot Nothing Then
            lsAvoidanceZones.Add(circleAvoidanceZone.TerritoryId)
        End If

        Assert.IsNotNull(circleAvoidanceZone, Convert.ToString("Add Circle Avoidance Zone test failed... ") & errorString)

        Dim polyAvoidanceZoneParameters As New AvoidanceZoneParameters() With { _
            .TerritoryName = "Test Poly Territory", _
            .TerritoryColor = "ff0000", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Poly.GetEnumDescription(), _
                .Data = New String() {"37.569752822786455,-77.47833251953125", "37.75886716305343,-77.68974800109863", "37.74763966054455,-77.6917221069336", "37.74655084306813,-77.68863220214844", "37.7502255383101,-77.68125076293945", "37.74797991274437,-77.67498512268066", _
                    "37.73327960206065,-77.6411678314209", "37.74430510679532,-77.63172645568848", "37.76641925847049,-77.66846199035645"} _
            } _
        }

        Dim polyAvoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(polyAvoidanceZoneParameters, errorString)

        Assert.IsNotNull(polyAvoidanceZone, Convert.ToString("Add Polygon Avoidance Zone test failed... ") & errorString)

        If polyAvoidanceZone IsNot Nothing Then
            lsAvoidanceZones.Add(polyAvoidanceZone.TerritoryId)
        End If

        Dim rectAvoidanceZoneParameters As New AvoidanceZoneParameters() With { _
            .TerritoryName = "Test Rect Territory", _
            .TerritoryColor = "ff0000", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Rect.GetEnumDescription(), _
                .Data = New String() {"43.51668853502909,-109.3798828125", "46.98025235521883,-101.865234375"} _
            } _
        }

        Dim rectAvoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(rectAvoidanceZoneParameters, errorString)

        Assert.IsNotNull(rectAvoidanceZone, Convert.ToString("Add Rectangular Avoidance Zone test failed... ") & errorString)

        If lsAvoidanceZones IsNot Nothing Then
            lsAvoidanceZones.Add(rectAvoidanceZone.TerritoryId)
        End If
    End Sub

    <TestMethod> _
    Public Sub GetAvoidanceZonesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)


        Dim avoidanceZoneQuery As New AvoidanceZoneQuery()

        ' Run the query
        Dim errorString As String = ""
        Dim avoidanceZones As AvoidanceZone() = route4Me.GetAvoidanceZones(avoidanceZoneQuery, errorString)

        Assert.IsInstanceOfType(avoidanceZones, GetType(AvoidanceZone()), Convert.ToString("GetAvoidanceZonesTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetAvoidanceZoneTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim territoryId As String = ""
        If lsAvoidanceZones.Count > 0 Then
            territoryId = lsAvoidanceZones(0)
        End If
        Dim avoidanceZoneQuery As New AvoidanceZoneQuery() With { _
            .TerritoryId = territoryId _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim avoidanceZone As AvoidanceZone = route4Me.GetAvoidanceZone(avoidanceZoneQuery, errorString)

        Assert.IsNotNull(avoidanceZone, Convert.ToString("GetAvoidanceZonesTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub UpdateAvoidanceZoneTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim territoryId As String = ""
        If lsAvoidanceZones.Count > 0 Then
            territoryId = lsAvoidanceZones(0)
        End If

        Dim avoidanceZoneParameters As New AvoidanceZoneParameters() With { _
            .TerritoryId = territoryId, _
            .TerritoryName = "Test Territory Updated", _
            .TerritoryColor = "ff00ff", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Circle.GetEnumDescription(), _
                .Data = New String() {"38.41322259056806,-78.501953234", "3000"} _
            } _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim avoidanceZone As AvoidanceZone = route4Me.UpdateAvoidanceZone(avoidanceZoneParameters, errorString)

        Assert.IsNotNull(avoidanceZone, Convert.ToString("UpdateAvoidanceZoneTest failed... ") & errorString)
    End Sub

    <TestMethod> _
<ClassCleanup> _
    Public Shared Sub RemoveAvoidanceZoneTest()
        For Each territoryId As String In lsAvoidanceZones
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim avoidanceZoneQuery As New AvoidanceZoneQuery() With { _
                .TerritoryId = territoryId _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As Boolean = route4Me.DeleteAvoidanceZone(avoidanceZoneQuery, errorString)

            Assert.IsTrue(result, Convert.ToString("RemoveAvoidanceZoneTest failed... ") & errorString)
        Next
    End Sub

End Class

<TestClass()> Public Class TerritoriesGroup
    Shared c_ApiKey As String = "11111111111111111111111111111111"

    Shared lsTerritories As New List(Of String)()

    <TestMethod> _
<TestInitialize> _
    Public Sub AddTerritoriesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim circleTerritoryParameters As New AvoidanceZoneParameters() With { _
            .TerritoryName = "Test Circle Territory", _
            .TerritoryColor = "ff0000", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Circle.GetEnumDescription(), _
                .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"} _
            } _
        }

        Dim errorString As String = ""
        Dim circleTerritory As TerritoryZone = route4Me.CreateTerritory(circleTerritoryParameters, errorString)

        If circleTerritory IsNot Nothing Then
            lsTerritories.Add(circleTerritory.TerritoryId)
        End If

        Assert.IsNotNull(circleTerritory, Convert.ToString("Add Circle Territory test failed... ") & errorString)

        Dim polyTerritoryParameters As New AvoidanceZoneParameters() With { _
            .TerritoryName = "Test Poly Territory", _
            .TerritoryColor = "ff0000", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Poly.GetEnumDescription(), _
                .Data = New String() {"37.569752822786455,-77.47833251953125", "37.75886716305343,-77.68974800109863", "37.74763966054455,-77.6917221069336", "37.74655084306813,-77.68863220214844", "37.7502255383101,-77.68125076293945", "37.74797991274437,-77.67498512268066", _
                    "37.73327960206065,-77.6411678314209", "37.74430510679532,-77.63172645568848", "37.76641925847049,-77.66846199035645"} _
            } _
        }

        Dim polyTerritory As TerritoryZone = route4Me.CreateTerritory(polyTerritoryParameters, errorString)

        Assert.IsNotNull(polyTerritory, Convert.ToString("Add Polygon Territory test failed... ") & errorString)

        If polyTerritory IsNot Nothing Then
            lsTerritories.Add(polyTerritory.TerritoryId)
        End If

        Dim rectTerritoryParameters As New AvoidanceZoneParameters() With { _
            .TerritoryName = "Test Rect Territory", _
            .TerritoryColor = "ff0000", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Rect.GetEnumDescription(), _
                .Data = New String() {"43.51668853502909,-109.3798828125", "46.98025235521883,-101.865234375"} _
            } _
        }

        Dim rectTerritory As TerritoryZone = route4Me.CreateTerritory(rectTerritoryParameters, errorString)

        Assert.IsNotNull(rectTerritory, Convert.ToString("Add Rectangular Avoidance Zone test failed... ") & errorString)

        If lsTerritories IsNot Nothing Then
            lsTerritories.Add(rectTerritory.TerritoryId)
        End If
    End Sub

    <TestMethod> _
    Public Sub GetTerritoriesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)


        Dim territoryQuery As New AvoidanceZoneQuery()

        ' Run the query
        Dim errorString As String = ""
        Dim territories As AvoidanceZone() = route4Me.GetTerritories(territoryQuery, errorString)

        Assert.IsInstanceOfType(territories, GetType(AvoidanceZone()), Convert.ToString("GetTerritoriesTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetTerritoryTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim territoryId As String = ""
        If lsTerritories.Count > 0 Then
            territoryId = lsTerritories(0)
        End If
        Dim territoryQuery As New TerritoryQuery() With { _
            .TerritoryId = territoryId _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim territory As TerritoryZone = route4Me.GetTerritory(territoryQuery, errorString)

        Assert.IsNotNull(territory, Convert.ToString("GetTerritoryTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub UpdateTerritoryTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim territoryId As String = ""
        If lsTerritories.Count > 0 Then
            territoryId = lsTerritories(0)
        End If

        Dim territoryParameters As New AvoidanceZoneParameters() With { _
            .TerritoryId = territoryId, _
            .TerritoryName = "Test Territory Updated", _
            .TerritoryColor = "ff00ff", _
            .Territory = New Territory() With { _
                .Type = TerritoryType.Circle.GetEnumDescription(), _
                .Data = New String() {"38.41322259056806,-78.501953234", "3000"} _
            } _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim territory As AvoidanceZone = route4Me.UpdateTerritory(territoryParameters, errorString)

        Assert.IsNotNull(territory, Convert.ToString("UpdateTerritoryTest failed... ") & errorString)
    End Sub

    <TestMethod> _
<ClassCleanup> _
    Public Shared Sub RemoveTerritoriesTest()
        For Each territoryId As String In lsTerritories
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim territoryQuery As New TerritoryQuery() With { _
                .TerritoryId = territoryId _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As Boolean = route4Me.RemoveTerritory(territoryQuery, errorString)

            Assert.IsTrue(result, Convert.ToString("RemoveTerritoriesTest failed... ") & errorString)
        Next
    End Sub

End Class

Public Class TestDataRepository
    Private c_ApiKey As String = "11111111111111111111111111111111"


    Public Sub New()
    End Sub

    Public Property dataObjectSD10Stops As DataObject

    Public Property SD10Stops_optimization_problem_id As String

    Public Property SD10Stops_route As DataObjectRoute

    Public Property SD10Stops_route_id As String


    Public Property dataObjectSDRT As DataObject

    Public Property SDRT_optimization_problem_id As String

    Public Property SDRT_route As DataObjectRoute

    Public Property SDRT_route_id As String


    Public Property dataObjectMDMD24 As DataObject

    Public Property MDMD24_optimization_problem_id As String

    Public Property MDMD24_route() As DataObjectRoute

    Public Property MDMD24_route_id() As String

    Public Function RunOptimizationSingleDriverRoute10Stops() As Boolean
        Dim r4mm As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses
        '

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "151 Arbor Way Milledgeville GA 31061", _
            .IsDepot = True, _
            .Latitude = 33.132675170898, _
            .Longitude = -83.244743347168, _
            .Time = 0, _
            .CustomFields = New Dictionary(Of String, String)() From { _
                {"color", "red"}, _
                {"size", "huge"} _
            } _
        }, New Address() With { _
            .AddressString = "230 Arbor Way Milledgeville GA 31061", _
            .Latitude = 33.129695892334, _
            .Longitude = -83.24577331543, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "148 Bass Rd NE Milledgeville GA 31061", _
            .Latitude = 33.143497, _
            .Longitude = -83.224487, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.141784667969, _
            .Longitude = -83.237518310547, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.141086578369, _
            .Longitude = -83.238258361816, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "131 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.142036437988, _
            .Longitude = -83.238845825195, _
            .Time = 0 _
        }, _
            New Address() With { _
            .AddressString = "138 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.14307, _
            .Longitude = -83.239334, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "139 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.142734527588, _
            .Longitude = -83.237442016602, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "145 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.143871307373, _
            .Longitude = -83.237342834473, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "221 Blake Cir Milledgeville GA 31061", _
            .Latitude = 33.081462860107, _
            .Longitude = -83.208511352539, _
            .Time = 0 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.TSP, _
            .StoreRoute = False, _
            .RouteName = "Single Driver Route 10 Stops Test", _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription() _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""
        Try
            dataObjectSD10Stops = r4mm.RunOptimization(optimizationParameters, errorString)

            SD10Stops_optimization_problem_id = dataObjectSD10Stops.OptimizationProblemId
            SD10Stops_route = If((dataObjectSD10Stops IsNot Nothing AndAlso dataObjectSD10Stops.Routes IsNot Nothing AndAlso dataObjectSD10Stops.Routes.Length > 0), dataObjectSD10Stops.Routes(0), Nothing)
            SD10Stops_route_id = If((SD10Stops_route IsNot Nothing), SD10Stops_route.RouteID, Nothing)

            Return True
        Catch ex As Exception
            Console.WriteLine("Single Driver Route 10 Stops generation failed... " + ex.Message)
            Return False
        End Try

    End Function

    Public Function SingleDriverRoundTripTest() As Boolean
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "754 5th Ave New York, NY 10019", _
            .[Alias] = "Bergdorf Goodman", _
            .IsDepot = True, _
            .Latitude = 40.7636197, _
            .Longitude = -73.9744388, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "717 5th Ave New York, NY 10022", _
            .[Alias] = "Giorgio Armani", _
            .Latitude = 40.7669692, _
            .Longitude = -73.9693864, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "888 Madison Ave New York, NY 10014", _
            .[Alias] = "Ralph Lauren Women's and Home", _
            .Latitude = 40.7715154, _
            .Longitude = -73.9669241, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "1011 Madison Ave New York, NY 10075", _
            .[Alias] = "Yigal Azrou'l", _
            .Latitude = 40.7772129, _
            .Longitude = -73.9669, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "440 Columbus Ave New York, NY 10024", _
            .[Alias] = "Frank Stella Clothier", _
            .Latitude = 40.7808364, _
            .Longitude = -73.9732729, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "324 Columbus Ave #1 New York, NY 10023", _
            .[Alias] = "Liana", _
            .Latitude = 40.7803123, _
            .Longitude = -73.9793079, _
            .Time = 0 _
        }, _
            New Address() With { _
            .AddressString = "110 W End Ave New York, NY 10023", _
            .[Alias] = "Toga Bike Shop", _
            .Latitude = 40.7753077, _
            .Longitude = -73.9861529, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "555 W 57th St New York, NY 10019", _
            .[Alias] = "BMW of Manhattan", _
            .Latitude = 40.7718005, _
            .Longitude = -73.9897716, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "57 W 57th St New York, NY 10019", _
            .[Alias] = "Verizon Wireless", _
            .Latitude = 40.7558695, _
            .Longitude = -73.9862019, _
            .Time = 0 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.TSP, _
            .StoreRoute = False, _
            .RouteName = "Single Driver Round Trip", _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RouteMaxDuration = 86400, _
            .VehicleCapacity = "1", _
            .VehicleMaxDistanceMI = "10000", _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription() _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""
        Try
            dataObjectSDRT = route4Me.RunOptimization(optimizationParameters, errorString)
            SDRT_optimization_problem_id = dataObjectSDRT.OptimizationProblemId
            SDRT_route = If((dataObjectSDRT IsNot Nothing AndAlso dataObjectSDRT.Routes IsNot Nothing AndAlso dataObjectSDRT.Routes.Length > 0), dataObjectSDRT.Routes(0), Nothing)
            SDRT_route_id = If((SDRT_route IsNot Nothing), SDRT_route.RouteID, Nothing)
            Return True
        Catch ex As Exception
            Console.WriteLine("Single Driver Round Trip generation failed... " + ex.Message)
            Return False
        End Try

    End Function

    Public Function RemoveOptimization(optimizationProblemIDs As String()) As Boolean
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Run the query
        Dim errorString As String = ""

        Try
            Dim removed As Boolean = route4Me.RemoveOptimization(optimizationProblemIDs, errorString)
            Return removed
        Catch ex As Exception
            Console.WriteLine("Removing of an optimization failed... " + ex.Message)
            Return False
            Throw
        End Try

    End Function

    Public Function MultipleDepotMultipleDriverWith24StopsTimeWindowTest() As Boolean
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "3634 W Market St, Fairlawn, OH 44333", _
            .IsDepot = True, _
            .Latitude = 41.135762259364, _
            .Longitude = -81.629313826561, _
            .Time = 300, _
            .TimeWindowStart = 28800, _
            .TimeWindowEnd = 29465 _
        }, New Address() With { _
            .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.143505096435, _
            .Longitude = -81.46549987793, _
            .Time = 300, _
            .TimeWindowStart = 29465, _
            .TimeWindowEnd = 30529 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 30529, _
            .TimeWindowEnd = 33479 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 33479, _
            .TimeWindowEnd = 33944 _
        }, New Address() With { _
            .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.162971496582, _
            .Longitude = -81.479049682617, _
            .Time = 300, _
            .TimeWindowStart = 33944, _
            .TimeWindowEnd = 34801 _
        }, New Address() With { _
            .AddressString = "1659 Hibbard Dr, Stow, OH 44224", _
            .Latitude = 41.194505989552, _
            .Longitude = -81.443351581693, _
            .Time = 300, _
            .TimeWindowStart = 34801, _
            .TimeWindowEnd = 36366 _
        }, _
            New Address() With { _
            .AddressString = "2705 N River Rd, Stow, OH 44224", _
            .Latitude = 41.145240783691, _
            .Longitude = -81.410247802734, _
            .Time = 300, _
            .TimeWindowStart = 36366, _
            .TimeWindowEnd = 39173 _
        }, New Address() With { _
            .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087", _
            .Latitude = 41.340042114258, _
            .Longitude = -81.421226501465, _
            .Time = 300, _
            .TimeWindowStart = 39173, _
            .TimeWindowEnd = 41617 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148578643799, _
            .Longitude = -81.429229736328, _
            .Time = 300, _
            .TimeWindowStart = 41617, _
            .TimeWindowEnd = 43660 _
        }, New Address() With { _
            .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.148579, _
            .Longitude = -81.42923, _
            .Time = 300, _
            .TimeWindowStart = 43660, _
            .TimeWindowEnd = 46392 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 46392, _
            .TimeWindowEnd = 48089 _
        }, New Address() With { _
            .AddressString = "559 W Aurora Rd, Northfield, OH 44067", _
            .Latitude = 41.315116882324, _
            .Longitude = -81.558746337891, _
            .Time = 300, _
            .TimeWindowStart = 48089, _
            .TimeWindowEnd = 48449 _
        }, _
            New Address() With { _
            .AddressString = "3933 Klein Ave, Stow, OH 44224", _
            .Latitude = 41.169467926025, _
            .Longitude = -81.429420471191, _
            .Time = 300, _
            .TimeWindowStart = 48449, _
            .TimeWindowEnd = 50152 _
        }, New Address() With { _
            .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.136692047119, _
            .Longitude = -81.493492126465, _
            .Time = 300, _
            .TimeWindowStart = 50152, _
            .TimeWindowEnd = 51682 _
        }, New Address() With { _
            .AddressString = "3731 Osage St, Stow, OH 44224", _
            .Latitude = 41.161357879639, _
            .Longitude = -81.42293548584, _
            .Time = 300, _
            .TimeWindowStart = 51682, _
            .TimeWindowEnd = 54379 _
        }, New Address() With { _
            .AddressString = "3862 Klein Ave, Stow, OH 44224", _
            .Latitude = 41.167895123363, _
            .Longitude = -81.429973393679, _
            .Time = 300, _
            .TimeWindowStart = 54379, _
            .TimeWindowEnd = 54879 _
        }, New Address() With { _
            .AddressString = "138 Northwood Ln, Tallmadge, OH 44278", _
            .Latitude = 41.085464134812, _
            .Longitude = -81.447411775589, _
            .Time = 300, _
            .TimeWindowStart = 54879, _
            .TimeWindowEnd = 56613 _
        }, New Address() With { _
            .AddressString = "3401 Saratoga Blvd, Stow, OH 44224", _
            .Latitude = 41.148849487305, _
            .Longitude = -81.407363891602, _
            .Time = 300, _
            .TimeWindowStart = 56613, _
            .TimeWindowEnd = 57052 _
        }, _
            New Address() With { _
            .AddressString = "5169 Brockton Dr, Stow, OH 44224", _
            .Latitude = 41.195003509521, _
            .Longitude = -81.392700195312, _
            .Time = 300, _
            .TimeWindowStart = 57052, _
            .TimeWindowEnd = 59004 _
        }, New Address() With { _
            .AddressString = "5169 Brockton Dr, Stow, OH 44224", _
            .Latitude = 41.195003509521, _
            .Longitude = -81.392700195312, _
            .Time = 300, _
            .TimeWindowStart = 59004, _
            .TimeWindowEnd = 60027 _
        }, New Address() With { _
            .AddressString = "458 Aintree Dr, Munroe Falls, OH 44262", _
            .Latitude = 41.1266746521, _
            .Longitude = -81.445808410645, _
            .Time = 300, _
            .TimeWindowStart = 60027, _
            .TimeWindowEnd = 60375 _
        }, New Address() With { _
            .AddressString = "512 Florida Pl, Barberton, OH 44203", _
            .Latitude = 41.003671512008, _
            .Longitude = -81.598461046815, _
            .Time = 300, _
            .TimeWindowStart = 60375, _
            .TimeWindowEnd = 63891 _
        }, New Address() With { _
            .AddressString = "2299 Tyre Dr, Hudson, OH 44236", _
            .Latitude = 41.250511169434, _
            .Longitude = -81.420433044434, _
            .Time = 300, _
            .TimeWindowStart = 63891, _
            .TimeWindowEnd = 65277 _
        }, New Address() With { _
            .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221", _
            .Latitude = 41.136692047119, _
            .Longitude = -81.493492126465, _
            .Time = 300, _
            .TimeWindowStart = 65277, _
            .TimeWindowEnd = 68545 _
        }}

        ' Set parameters

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
            .RouteName = "Multiple Depot, Multiple Driver with 24 Stops, Time Window", _
            .StoreRoute = False, _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
            .RouteTime = 60 * 60 * 7, _
            .RouteMaxDuration = 86400, _
            .VehicleCapacity = "1", _
            .VehicleMaxDistanceMI = "10000", _
            .Optimize = Optimize.Distance.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .TravelMode = TravelMode.Driving.GetEnumDescription(), _
            .Metric = Metric.Geodesic _
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        ' Run the query
        Dim errorString As String = ""

        Try
            dataObjectMDMD24 = route4Me.RunOptimization(optimizationParameters, errorString)

            MDMD24_route_id = If((dataObjectMDMD24 IsNot Nothing AndAlso dataObjectMDMD24.Routes IsNot Nothing AndAlso dataObjectMDMD24.Routes.Length > 0), dataObjectMDMD24.Routes(0).RouteID, Nothing)
            MDMD24_optimization_problem_id = dataObjectMDMD24.OptimizationProblemId
            MDMD24_route = If((dataObjectMDMD24 IsNot Nothing AndAlso dataObjectMDMD24.Routes IsNot Nothing AndAlso dataObjectMDMD24.Routes.Length > 0), dataObjectMDMD24.Routes(0), Nothing)
            MDMD24_route_id = If((MDMD24_route IsNot Nothing), MDMD24_route.RouteID, Nothing)

            Return True
        Catch ex As Exception
            Console.WriteLine("Generation of the Multiple Depot, Multiple Driver with 24 Stops optimization problem failed... " + ex.Message)
            Return False
        End Try

    End Function

    Public Function GenerateSQLCEDatabaseTest() As Boolean
        Dim sqlDB As New cDatabase(DB_Type.SQLCE)

        Try
            Dim sAddressbookSqlCom As String = ""
            Dim sOrdersSqlCom As String = ""
            Dim sDictionaryDDLSqlCom As String = ""
            Dim sDictionaryDMLSqlCom As String = ""

            sAddressbookSqlCom = File.ReadAllText("Data/SQL/SQLCE/addressbook_v4.sql")
            sOrdersSqlCom = File.ReadAllText("Data/SQL/SQLCE/orders.sql")
            sDictionaryDDLSqlCom = File.ReadAllText("Data/SQL/SQLCE/csv_to_api_dictionary_DDL.sql")
            sDictionaryDMLSqlCom = File.ReadAllText("Data/SQL/SQLCE/csv_to_api_dictionary_DML.sql")

            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            dropSQLCEtable("addressbook_v4", sqlDB)

            Dim iResult As Integer = sqlDB.ExecuteMulticoomandSql(sAddressbookSqlCom)
            If iResult < 1 Then
                Return False
            End If

            dropSQLCEtable("orders", sqlDB)

            iResult = sqlDB.ExecuteMulticoomandSql(sOrdersSqlCom)
            If iResult < 1 Then
                Return False
            End If

            dropSQLCEtable("csv_to_api_dictionary", sqlDB)

            iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDDLSqlCom)
            If iResult < 1 Then
                Return False
            End If

            If iResult > 0 Then
                iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDMLSqlCom)
                If iResult < 1 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Console.WriteLine("Generating of the SQL tables failed!.. " + ex.Message)
            Return False
        Finally
            sqlDB.CloseConnection()
        End Try
    End Function

    Public Sub dropSQLCEtable(tableName As String, sqlDB As cDatabase)
        Dim oExists As Object = sqlDB.ExecuteScalar((Convert.ToString("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '") & tableName) + "'")
        Assert.IsNotNull(oExists, "Query about table existing failed...")

        If oExists.ToString() = "1" Then
            Dim iDroRresult As Integer = sqlDB.ExecuteNon(Convert.ToString("DROP TABLE ") & tableName)
        End If
    End Sub

    Public Function RemoveAddressBookContacts(lsRemLocations As List(Of String), ApiKey As String) As Boolean
        Dim route4Me As New Route4MeManager(ApiKey)

        If lsRemLocations.Count > 0 Then
            Dim errorString As String = ""
            Dim removed As Boolean = route4Me.RemoveAddressBookContacts(lsRemLocations.ToArray(), errorString)

            Return removed
        Else
            Return False
        End If
    End Function

    Public Function RemoveOrders(lsOrders As List(Of String), ApiKey As String) As Boolean
        Dim route4Me As New Route4MeManager(ApiKey)

        ' Run the query
        Dim errorString As String = ""
        Dim removed As Boolean = route4Me.RemoveOrders(lsOrders.ToArray(), errorString)

        Return removed

    End Function

End Class

#Region "Types"

<DataContract> _
Class AddressInfo
    Inherits GenericParameters
    <DataMember(Name:="route_destination_id")> _
    Public Property DestinationId As Integer

    <DataMember(Name:="sequence_no")> _
    Public Property SequenceNo As Integer

    <DataMember(Name:="is_depot")> _
    Public Property IsDepot As Boolean
End Class

<DataContract> _
Class AddressesOrderInfo
    Inherits GenericParameters
    <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
    Public Property RouteId As String

    <DataMember(Name:="addresses")> _
    Public Property Addresses As AddressInfo()
End Class

<DataContract> _
Class MyAddressAndParametersHolder
    Inherits GenericParameters
    <DataMember> _
    Public Property addresses As Address()

    <DataMember> _
    Public Property parameters As RouteParameters
End Class

<DataContract> _
Class MyDataObjectGeneric
    <DataMember(Name:="optimization_problem_id")> _
    Public Property OptimizationProblemId As String

    <DataMember(Name:="state")> _
    Public Property MyState As Integer

    <DataMember(Name:="addresses")> _
    Public Property Addresses As Address()

    <DataMember(Name:="parameters")> _
    Public Property Parameters As RouteParameters

End Class
#End Region