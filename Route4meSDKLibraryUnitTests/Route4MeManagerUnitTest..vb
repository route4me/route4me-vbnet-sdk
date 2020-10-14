Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.Route4MeSDK.FastProcessing
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Reflection
Imports System.CodeDom.Compiler
Imports System.Threading
Imports CsvHelper

Public Class ApiKeys
    Public Shared actualApiKey As String = R4MeUtils.ReadSetting("actualApiKey")
    Public Shared demoApiKey As String = R4MeUtils.ReadSetting("actualApiKey")
End Class


<TestClass()> Public Class RoutesGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey

    Shared tdr As TestDataRepository
    Shared tdr2 As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)

    <ClassInitialize> _
    Public Shared Sub RoutesGroupInitialize(context As TestContext)
        lsOptimizationIDs = New List(Of String)()

        tdr = New TestDataRepository()
        tdr2 = New TestDataRepository()

        Dim result As Boolean = tdr.RunOptimizationSingleDriverRoute10Stops()

        Dim result2 As Boolean = tdr2.RunOptimizationSingleDriverRoute10Stops()
        Dim result3 As Boolean = tdr2.SingleDriverRoundTripTest()
        Dim result4 As Boolean = tdr2.MultipleDepotMultipleDriverWith24StopsTimeWindowTest()

        Assert.IsTrue(result, "Single Driver 10 Stops generation failed...")
        Assert.IsTrue(result2, "Single Driver 10 Stops generation failed...")
        Assert.IsTrue(result4, "Multi-Depot Multi-Driver 24 Stops generation failed...")
        Assert.IsTrue(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses...")
        Assert.IsTrue(tdr2.SD10Stops_route.Addresses.Length > 0, "The route has no addresses...")

        Assert.IsTrue(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses...")

        lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id)
        lsOptimizationIDs.Add(tdr2.SD10Stops_optimization_problem_id)
        lsOptimizationIDs.Add(tdr2.SDRT_optimization_problem_id)
        lsOptimizationIDs.Add(tdr2.MDMD24_optimization_problem_id)
    End Sub

    <TestMethod>
    Public Sub GetRoutesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeParameters As New RouteParametersQuery() With {
            .Limit = 20,
            .Offset = 5
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObjects As DataObjectRoute() = route4Me.GetRoutes(routeParameters, errorString)

        Assert.IsInstanceOfType(dataObjects, GetType(DataObjectRoute()), Convert.ToString("GetRoutesTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetRoutesFromDateRangeTest()
        If c_ApiKey = ApiKeys.demoApiKey Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeParameters As New RouteParametersQuery() With {
            .StartDate = "2019-08-01",
            .EndtDate = "2019-08-05"
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObjects As DataObjectRoute() = route4Me.GetRoutes(routeParameters, errorString)

        Assert.IsInstanceOfType(dataObjects, GetType(DataObjectRoute()), Convert.ToString("GetRoutesFromDateRangeTest failed... ") & errorString)
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

    <TestMethod>
    Public Sub GetRoutesByIDsTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)

        Dim routesParameters As RouteParametersQuery = New RouteParametersQuery() With {
            .Offset = 0,
            .Limit = 3
        }

        Dim errorString As String = Nothing
        Dim threeRoutes As DataObjectRoute() = route4Me.GetRoutes(routesParameters, errorString)
        Dim routeParameters = New RouteParametersQuery() With {
            .RouteId = threeRoutes(0).RouteID & "," + threeRoutes(1).RouteID
        }

        Dim twoRoutes = route4Me.GetRoutes(routeParameters, errorString)

        Assert.IsInstanceOfType(twoRoutes, GetType(DataObjectRoute()), "GetRoutesByIDsTest failed")
        Assert.IsTrue(twoRoutes.Length = 2, "GetRoutesByIDsTest failed")
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

    <TestMethod>
    Public Sub ResequenceRouteDestinationsTest()
        Dim route As DataObjectRoute = tdr.SD10Stops_route
        Assert.IsNotNull(route, "Route for the test Route Destinations Resequence is null...")

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim rParams As RouteParametersQuery = New RouteParametersQuery() With {
            .RouteId = route.RouteID
        }

        Dim lsAddresses As List(Of Address) = New List(Of Address)()

        Dim address1 As Address = route.Addresses(2)
        Dim address2 As Address = route.Addresses(3)

        address1.SequenceNo = 4
        address2.SequenceNo = 3
        lsAddresses.Add(address1)
        lsAddresses.Add(address2)
        Dim errorString As String = ""
        Dim route1 As DataObjectRoute = route4Me.ManuallyResequenceRoute(rParams, lsAddresses.ToArray(), errorString)

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

    <TestMethod>
    Public Sub UpdateRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim parametersNew As New RouteParameters() With {
            .RouteName = "New name of the route"
        }

        Dim routeParameters As New RouteParametersQuery() With {
            .RouteId = routeId,
            .Parameters = parametersNew
        }

        Dim errorString As String = ""
        Dim dataObject As DataObjectRoute = route4Me.UpdateRoute(routeParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("UpdateRouteTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub UpdateWholeRouteTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr2.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim initialRoute = R4MeUtils.ObjectDeepClone(Of DataObjectRoute)(tdr2.SD10Stops_route)
        Dim errorString5 As String = Nothing
        Dim customNotesResponse = route4Me.getAllCustomNoteTypes(errorString5)
        Dim allCustomNotes = If(customNotesResponse IsNot Nothing AndAlso customNotesResponse.[GetType]() = GetType(CustomNoteType()), CType(customNotesResponse, CustomNoteType()), Nothing)
        Dim tempFilePath As String = Nothing

        Using stream As Stream = File.Open("test.png", FileMode.Open)
            Dim tempFiles = New TempFileCollection()

            If True Then
                tempFilePath = tempFiles.AddExtension("png")
                Console.WriteLine(tempFilePath)

                Using fileStream As Stream = File.OpenWrite(tempFilePath)
                    stream.CopyTo(fileStream)
                End Using
            End If
        End Using

        tdr2.SD10Stops_route.Addresses(1).Notes = New AddressNote() {New AddressNote() With {
        .NoteId = -1,
        .RouteId = tdr2.SD10Stops_route.RouteID,
        .Latitude = tdr2.SD10Stops_route.Addresses(1).Latitude,
        .Longitude = tdr2.SD10Stops_route.Addresses(1).Longitude,
        .ActivityType = "dropoff",
        .Contents = "C# SDK Test Content",
        .CustomTypes = New AddressCustomNote() {New AddressCustomNote() With {
                .NoteCustomTypeID = allCustomNotes(0).NoteCustomTypeID.ToString(),
                .NoteCustomValue = allCustomNotes(0).NoteCustomTypeValues(0)
            }},
            .UploadUrl = tempFilePath
        }}

        Dim errorString0 As String = Nothing
        Dim updatedRoute0 = route4Me.UpdateRoute(tdr2.SD10Stops_route, initialRoute, errorString0)

        Assert.IsTrue(updatedRoute0.Addresses(1).Notes.Length = 1, "UpdateRouteTest failed: cannot create a note")
        Assert.IsTrue(updatedRoute0.Addresses(1).Notes(0).CustomTypes.Length = 1, "UpdateRouteTest failed: cannot create a custom type note")
        Assert.IsTrue(updatedRoute0.Addresses(1).Notes(0).UploadId.Length = 32, "UpdateRouteTest failed: cannot create a custom type note")

        tdr2.SD10Stops_route.ApprovedForExecution = True
        tdr2.SD10Stops_route.Parameters.RouteName += " Edited"
        tdr2.SD10Stops_route.Parameters.Metric = Metric.Manhattan
        tdr2.SD10Stops_route.Addresses(1).AddressString += " Edited"
        tdr2.SD10Stops_route.Addresses(1).Group = "Example Group"
        tdr2.SD10Stops_route.Addresses(1).CustomerPo = "CPO 456789"
        tdr2.SD10Stops_route.Addresses(1).InvoiceNo = "INO 789654"
        tdr2.SD10Stops_route.Addresses(1).ReferenceNo = "RNO 313264"
        tdr2.SD10Stops_route.Addresses(1).OrderNo = "ONO 654878"
        tdr2.SD10Stops_route.Addresses(1).Notes = New AddressNote() {New AddressNote() With {
            .RouteDestinationId = -1,
            .RouteId = tdr.SD10Stops_route.RouteID,
            .Latitude = tdr.SD10Stops_route.Addresses(1).Latitude,
            .Longitude = tdr.SD10Stops_route.Addresses(1).Longitude,
            .ActivityType = "dropoff",
            .Contents = "C# SDK Test Content"
        }}

        tdr2.SD10Stops_route.Addresses(2).SequenceNo = 5

        Dim addressID = tdr2.SD10Stops_route.Addresses(2).RouteDestinationId
        Dim errorString As String = Nothing
        Dim dataObject = route4Me.UpdateRoute(tdr2.SD10Stops_route, initialRoute, errorString)

        Assert.IsTrue(dataObject.Addresses.Where(Function(x) x.RouteDestinationId = addressID).FirstOrDefault().SequenceNo = 5, "UpdateWholeRouteTest failed  Cannot resequence addresses")
        Assert.IsTrue(tdr2.SD10Stops_route.ApprovedForExecution, "UpdateRouteTest failed, ApprovedForExecution cannot set to true")
        Assert.IsNotNull(dataObject, "UpdateRouteTest failed. " & errorString)
        Assert.IsTrue(dataObject.Parameters.RouteName.Contains("Edited"), "UpdateRouteTest failed, the route name not changed.")
        Assert.IsTrue(dataObject.Addresses(1).AddressString.Contains("Edited"), "UpdateRouteTest failed, second address name not changed.")
        Assert.IsTrue(dataObject.Addresses(1).Group = "Example Group", "UpdateWholeRouteTest failed.")
        Assert.IsTrue(dataObject.Addresses(1).CustomerPo = "CPO 456789", "UpdateWholeRouteTest failed.")
        Assert.IsTrue(dataObject.Addresses(1).InvoiceNo = "INO 789654", "UpdateWholeRouteTest failed.")
        Assert.IsTrue(dataObject.Addresses(1).ReferenceNo = "RNO 313264", "UpdateWholeRouteTest failed.")
        Assert.IsTrue(dataObject.Addresses(1).OrderNo = "ONO 654878", "UpdateWholeRouteTest failed.")

        initialRoute = R4MeUtils.ObjectDeepClone(Of DataObjectRoute)(tdr2.SD10Stops_route)

        tdr2.SD10Stops_route.ApprovedForExecution = False
        tdr2.SD10Stops_route.Addresses(1).Group = Nothing
        tdr2.SD10Stops_route.Addresses(1).CustomerPo = Nothing
        tdr2.SD10Stops_route.Addresses(1).InvoiceNo = Nothing
        tdr2.SD10Stops_route.Addresses(1).ReferenceNo = Nothing
        tdr2.SD10Stops_route.Addresses(1).OrderNo = Nothing

        dataObject = route4Me.UpdateRoute(tdr2.SD10Stops_route, initialRoute, errorString)

        Assert.IsFalse(tdr2.SD10Stops_route.ApprovedForExecution, "UpdateRouteTest failed, ApprovedForExecution cannot set to false")
        Assert.IsNull(dataObject.Addresses(1).Group, "UpdateWholeRouteTest failed.")
        Assert.IsNull(dataObject.Addresses(1).CustomerPo, "UpdateWholeRouteTest failed.")
        Assert.IsNull(dataObject.Addresses(1).InvoiceNo, "UpdateWholeRouteTest failed.")
        Assert.IsNull(dataObject.Addresses(1).ReferenceNo, "UpdateWholeRouteTest failed.")
        Assert.IsNull(dataObject.Addresses(1).OrderNo, "UpdateWholeRouteTest failed.")
    End Sub

    <TestMethod>
    Public Sub AssignVehicleToRouteTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)

        Dim vehicleGroup = New VehiclesGroup()
        Dim vehicles = vehicleGroup.getVehiclesList()

        Dim randomNumber As Integer = (New Random()).[Next](0, vehicles.PerPage - 1)
        Dim vehicleId = vehicles.Data(randomNumber).VehicleId

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim routeParameters = New RouteParametersQuery() With {
            .RouteId = routeId,
            .Parameters = New RouteParameters() With {
                .VehicleId = vehicleId
            }
        }
        Dim errorString As String
        route4Me.UpdateRoute(routeParameters, errorString)

        Dim route = route4Me.GetRoute(New RouteParametersQuery() With {
            .RouteId = routeId
        }, errorString)

        Assert.IsInstanceOfType(route.Vehilce, GetType(VehicleV4Response), "AssignVehicleToRouteTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub AssignMemberToRouteTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)

        Dim errorString As String
        Dim members = route4Me.GetUsers(New GenericParameters(), errorString)

        Dim randomNumber As Integer = (New Random()).[Next](0, members.results.Length - 1)
        Dim memberId = members.results(randomNumber).member_id

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim routeParameters = New RouteParametersQuery() With {
            .RouteId = routeId,
            .Parameters = New RouteParameters() With {
                .MemberId = memberId
            }
        }

        route4Me.UpdateRoute(routeParameters, errorString)

        Dim route = route4Me.GetRoute(New RouteParametersQuery() With {
            .RouteId = routeId
        }, errorString)

        Assert.IsTrue(route.MemberId = memberId, "AssignMemberToRouteTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub UpdateRouteCustomDataTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Dim routeDestionationId = tdr.SD10Stops_route.Addresses(3).RouteDestinationId

        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim parameters = New RouteParametersQuery() With {
            .RouteId = routeId,
            .RouteDestinationId = routeDestionationId
        }

        Dim customData = New Dictionary(Of String, String)() From {
            {"animal", "lion"},
            {"bird", "budgie"}
        }

        Dim errorString As String
        Dim result = route4Me.UpdateRouteCustomData(parameters, customData, errorString)

        Assert.IsNotNull(result, "UpdateRouteCustomDataTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub UpdateRouteAvoidanceZonesTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)
        Dim routeId As String = tdr.SD10Stops_route_id

        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null")

        Dim parameters = New RouteParametersQuery() With {
            .RouteId = routeId,
            .Parameters = New RouteParameters() With {
                .AvoidanceZones = New String() {"FAA49711A0F1144CE4E203DC18ABDFFB", "9C48E8008E9865006336B99D3595E66A"}
            }
        }

        Dim errorString As String = Nothing
        Dim result = route4Me.UpdateRoute(parameters, errorString)

        Assert.IsNotNull(result, "UpdateRouteAvoidanceZonesTest failed... " & errorString)
        Assert.IsTrue(result.Parameters.AvoidanceZones.Length = 2, "UpdateRouteAvoidanceZonesTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub RouteOriginParameterTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim routeParameters = New RouteParametersQuery() With {
            .RouteId = routeId,
            .Original = True
        }

        Dim errorString As String
        Dim route = route4Me.GetRoute(routeParameters, errorString)

        Assert.IsNotNull(route, "RouteOriginParameterTest failed. " & errorString)
        Assert.IsNotNull(route.OriginalRoute, "RouteOriginParameterTest failed. " & errorString)
        Assert.IsInstanceOfType(route.OriginalRoute, GetType(DataObjectRoute), "RouteOriginParameterTest failed. " & errorString)
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

    <TestMethod>
    Public Sub DuplicateRouteTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId is null")

        Dim routeParameters As New RouteParametersQuery() With {
            .RouteId = routeId
        }

        ' Run the query
        Dim errorString As String = ""
        Dim routeId_DuplicateRoute As String = route4Me.DuplicateRoute(routeParameters, errorString)

        Assert.IsNotNull(routeId_DuplicateRoute, "DuplicateRouteTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub ShareRouteTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)
        Dim routeId As String = tdr.SD10Stops_route_id

        Assert.IsNotNull(routeId, "routeId is null...")

        Dim routeParameters = New RouteParametersQuery() With {
            .RouteId = routeId,
            .ResponseFormat = "json"
        }

        Dim email As String = "regression.autotests+testcsharp123@gmail.com"

        Dim errorString As String
        Dim result = route4Me.RouteSharing(routeParameters, email, errorString)

        Assert.IsTrue(result, "ShareRouteTest failed... " & errorString)
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
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)

    Shared lastCustomNoteTypeID As Integer
    Shared firstCustomNoteTypeID As Integer

    <ClassInitialize> _
    Public Shared Sub NotesGroupInitialize(ByVal context As TestContext)
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        lsOptimizationIDs = New List(Of String)()
        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.SingleDriverRoundTripTest()
        Assert.IsTrue(result, "Single Driver Round Trip generation failed...")
        Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses...")
        lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id)
        Dim routeIdToMoveTo As String = tdr.SDRT_route_id
        Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null...")
        Dim addressId As Integer = If((tdr.dataObjectSDRT IsNot Nothing AndAlso tdr.SDRT_route IsNot Nothing AndAlso tdr.SDRT_route.Addresses.Length > 1 AndAlso tdr.SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing), tdr.SDRT_route.Addresses(1).RouteDestinationId.Value, 0)
        Dim lat As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Latitude, 33.132675170898)
        Dim lng As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Longitude, -83.244743347168)
        Dim noteParameters As NoteParameters = New NoteParameters() With {
            .RouteId = routeIdToMoveTo,
            .AddressId = addressId,
            .Latitude = lat,
            .Longitude = lng,
            .DeviceType = GetEnumDescription(DeviceType.Web),
            .ActivityType = GetEnumDescription(StatusUpdateType.DropOff),
            .Format = "json"
        }
        Dim errorString As String
        Dim contents As String = "Test Note Contents " & DateTime.Now.ToString()
        Dim note As AddressNote = route4Me.AddAddressNote(noteParameters, contents, errorString)
        Assert.IsNotNull(note, "AddAddressNoteTest failed... " & errorString)
        Dim response = route4Me.getAllCustomNoteTypes(errorString)
        Assert.IsTrue(response.[GetType]() = GetType(CustomNoteType()), errorString)
        Dim notesGroup As NotesGroup = New NotesGroup()

        If (CType(response, CustomNoteType())).Length < 2 Then
            notesGroup.addCustomNoteType("Conditions at Site", New String() {"safe", "mild", "dangerous", "slippery"})
            notesGroup.addCustomNoteType("To Do", New String() {"Pass a package", "Pickup package", "Do a service"})
            response = route4Me.getAllCustomNoteTypes(errorString)
        End If

        Assert.IsTrue((CType(response, CustomNoteType())).Length > 0, "Can not find custom note type in the account")
        lastCustomNoteTypeID = (CType(response, CustomNoteType()))((CType(response, CustomNoteType())).Length - 1).NoteCustomTypeID
        firstCustomNoteTypeID = (CType(response, CustomNoteType()))(0).NoteCustomTypeID
    End Sub

    <TestMethod>
    Public Sub AddAddressNoteTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim routeIdToMoveTo As String = tdr.SDRT_route_id
        Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null...")
        Dim addressId As Integer = If((tdr.dataObjectSDRT IsNot Nothing AndAlso tdr.SDRT_route IsNot Nothing AndAlso tdr.SDRT_route.Addresses.Length > 1 AndAlso tdr.SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing), tdr.SDRT_route.Addresses(1).RouteDestinationId.Value, 0)
        Dim lat As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Latitude, 33.132675170898)
        Dim lng As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Longitude, -83.244743347168)
        Dim noteParameters As NoteParameters = New NoteParameters() With {
            .RouteId = routeIdToMoveTo,
            .AddressId = addressId,
            .Latitude = lat,
            .Longitude = lng,
            .DeviceType = GetEnumDescription(DeviceType.Web),
            .ActivityType = GetEnumDescription(StatusUpdateType.DropOff),
            .Format = "json"
        }
        Dim errorString As String
        Dim contents As String = "Test Note Contents " & DateTime.Now.ToString()
        Dim note As AddressNote = route4Me.AddAddressNote(noteParameters, contents, errorString)
        Assert.IsNotNull(note, "AddAddressNoteTest failed... " & errorString)
    End Sub

    'this test excluded because website refuses sending of the file
    <TestMethod>
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

    <TestMethod>
    Public Sub AddCustomNoteToRouteTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String
        Dim noteParameters As NoteParameters = New NoteParameters() With {
            .RouteId = tdr.SDRT_route.RouteID,
            .AddressId = If(tdr.SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing, CInt(tdr.SDRT_route.Addresses(1).RouteDestinationId), 0),
            .Format = "json",
            .Latitude = tdr.SDRT_route.Addresses(1).Latitude,
            .Longitude = tdr.SDRT_route.Addresses(1).Longitude
        }
        Dim customNotes As Dictionary(Of String, String) = New Dictionary(Of String, String)() From {
            {"custom_note_type[11]", "slippery"},
            {"custom_note_type[10]", "Backdoor"},
            {"strUpdateType", "dropoff"},
            {"strNoteContents", "test1111"}
        }
        Dim response = route4Me.addCustomNoteToRoute(noteParameters, customNotes, errorString)
        Assert.IsTrue(response.[GetType]() <> GetType(String), errorString)
        Assert.IsTrue(response.[GetType]() = GetType(AddressNote))
    End Sub

    <TestMethod>
    Public Sub AddCustomNoteTypeTest()
        Dim response = addCustomNoteType("To Do", New String() {"Pass a package", "Pickup package", "Do a service"})
        Assert.IsTrue(response.[GetType]() <> GetType(String), response.ToString())
        Assert.IsTrue(Convert.ToInt32(response) >= 0, "Can not create new custom note type")
    End Sub

    Public Function addCustomNoteType(ByVal customType As String, ByVal customValues As String()) As Object
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String
        Dim response = route4Me.AddCustomNoteType(customType, customValues, errorString)
        Return response
    End Function

    <TestMethod>
    Public Sub GetAllCustomNoteTypesTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String
        Dim response = route4Me.getAllCustomNoteTypes(errorString)
        Assert.IsTrue(response.[GetType]() <> GetType(String), errorString)
        Assert.IsTrue(response.[GetType]() = GetType(CustomNoteType()))
    End Sub

    <TestMethod>
    Public Sub RemoveCustomNoteTypeTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String
        Dim response = route4Me.removeCustomNoteType(lastCustomNoteTypeID, errorString)
        Assert.IsTrue(response.[GetType]() <> GetType(String), errorString)
        Assert.IsTrue(Convert.ToInt32(response) >= 0, "Can not remove the custom note type")
    End Sub

    <ClassCleanup> _
    Public Shared Sub NotesGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the optimization with 24 stops failed...")
    End Sub

End Class

<TestClass()> Public Class RouteTypesGroup
    Shared skip As String
    ' The optimizations with the Trucking, Multiple Depots, Multiple Drivers allowed only for business and higher account types 
    ' put in the parameter an appropriate API key
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared c_ApiKey_1 As String = ApiKeys.demoApiKey
    Shared tdr As New TestDataRepository()

    Shared dataObject As DataObject, dataObjectMDMD24 As DataObject

    <ClassInitialize()>
    Public Shared Sub RouteTypesGroupInitialize(ByVal context As TestContext)
        If c_ApiKey = c_ApiKey_1 Then
            skip = "yes"
        Else
            skip = "no"
        End If
    End Sub

    <TestMethod> _
    Public Sub MultipleDepotMultipleDriverTest()
        If skip = "yes" Then Return

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
        If skip = "yes" Then Return

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

        Dim parameters As RouteParameters = New RouteParameters() With {
            .AlgorithmType = AlgorithmType.CVRP_TW_MD,
            .RouteName = "Single Depot, Multiple Driver, No Time Window",
            .StoreRoute = False,
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
            .RouteTime = 60 * 60 * 7,
            .RT = True,
            .RouteMaxDuration = 86400,
            .VehicleCapacity = 20,
            .VehicleMaxDistanceMI = 99999,
            .Parts = 4,
            .Optimize = GetEnumDescription(Optimize.Time),
            .DistanceUnit = GetEnumDescription(DistanceUnit.MI),
            .DeviceType = GetEnumDescription(DeviceType.Web),
            .TravelMode = GetEnumDescription(TravelMode.Driving),
            .Metric = Metric.Geodesic
        }

        Dim optimizationParameters As New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }

        If skip = "yes" Then
            Return
        Else
            Dim errorString As String
            dataObject = route4Me.RunOptimization(optimizationParameters, errorString)
            Assert.IsNotNull(dataObject, "SingleDepotMultipleDriverNoTimeWindowTest failed... " & errorString)


            tdr.RemoveOptimization(New String() {dataObject.OptimizationProblemId})
        End If

    End Sub

    <TestMethod> _
    Public Sub MultipleDepotMultipleDriverWith24StopsTimeWindowTest()
        If skip = "yes" Then Return

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

    <TestMethod>
    Public Sub TruckingSingleDriverMultipleTimeWindowsTest()
        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "455 S 4th St, Louisville, KY 40202", _
            .IsDepot = True, _
            .Latitude = 38.251698, _
            .Longitude = -85.757308 _
        }, New Address() With { _
            .AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214", _
            .Latitude = 38.141598, _
            .Longitude = -85.7938461, _
            .TimeWindowStart = 7 * 3600 + 30 * 60, _
            .TimeWindowEnd = 7 * 3600 + 40 * 60, _
            .TimeWindowStart2 = 8 * 3600 + 0 * 60, _
            .TimeWindowEnd2 = 8 * 3600 + 10 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "1407 MCCOY, Louisville, KY, 40215", _
            .Latitude = 38.202496, _
            .Longitude = -85.786514, _
            .TimeWindowStart = 8 * 3600 + 30 * 60, _
            .TimeWindowEnd = 8 * 3600 + 40 * 60, _
            .TimeWindowStart2 = 8 * 3600 + 50 * 60, _
            .TimeWindowEnd2 = 9 * 3600 + 0 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215", _
            .Latitude = 38.178844, _
            .Longitude = -85.774864, _
            .TimeWindowStart = 9 * 3600 + 0 * 60, _
            .TimeWindowEnd = 9 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 9 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 9 * 3600 + 45 * 60, _
            .Time = 100 _
        }, New Address() With { _
            .AddressString = "730 CECIL AVENUE, Louisville, KY, 40211", _
            .Latitude = 38.248684, _
            .Longitude = -85.821121, _
            .TimeWindowStart = 10 * 3600 + 0 * 60, _
            .TimeWindowEnd = 10 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 10 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 10 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211", _
            .Latitude = 38.251923, _
            .Longitude = -85.800034, _
            .TimeWindowStart = 11 * 3600 + 0 * 60, _
            .TimeWindowEnd = 11 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 11 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 11 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216", _
            .Latitude = 38.176067, _
            .Longitude = -85.824638, _
            .TimeWindowStart = 12 * 3600 + 0 * 60, _
            .TimeWindowEnd = 12 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 12 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 12 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215", _
            .Latitude = 38.179806, _
            .Longitude = -85.775558, _
            .TimeWindowStart = 13 * 3600 + 0 * 60, _
            .TimeWindowEnd = 13 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 13 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 13 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212", _
            .Latitude = 38.259335, _
            .Longitude = -85.815094, _
            .TimeWindowStart = 14 * 3600 + 0 * 60, _
            .TimeWindowEnd = 14 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 14 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 14 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215", _
            .Latitude = 38.179253, _
            .Longitude = -85.785118, _
            .TimeWindowStart = 15 * 3600 + 0 * 60, _
            .TimeWindowEnd = 15 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 15 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 15 * 3600 + 45 * 60, _
            .Time = 300 _
        }, New Address() With { _
            .AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214", _
            .Latitude = 38.162472, _
            .Longitude = -85.792854, _
            .TimeWindowStart = 16 * 3600 + 0 * 60, _
            .TimeWindowEnd = 16 * 3600 + 15 * 60, _
            .TimeWindowStart2 = 16 * 3600 + 30 * 60, _
            .TimeWindowEnd2 = 16 * 3600 + 45 * 60, _
            .Time = 300 _
        }}

        Dim parameters As New RouteParameters() With { _
            .AlgorithmType = AlgorithmType.CVRP_TW_SD, _
            .RouteName = "Trucking SD Multiple TW 09-02-2018 from c# SDK " & DateTime.Now.ToString("yymMddHHmmss"), _
            .OptimizationQuality = 3, _
            .DeviceType = DeviceType.Web.GetEnumDescription(), _
            .DistanceUnit = DistanceUnit.MI.GetEnumDescription(), _
            .Dirm = 3, _
            .DM = 6, _
            .Optimize = Optimize.TimeWithTraffic.GetEnumDescription(), _
            .RouteMaxDuration = 8 * 3600 + 30 * 60, _
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)), _
            .RouteTime = 7 * 3600 + 0 * 60, _
            .StoreRoute = True, _
            .TravelMode = TravelMode.Trucking.GetEnumDescription(), _
            .VehicleMaxCargoVolume = 30, _
            .VehicleCapacity = 10, _
            .VehicleMaxDistanceMI = 10000, _
            .TruckHeightMeters = 4, _
            .TruckLengthMeters = 12, _
            .TruckWidthMeters = 3, _
            .TrailerWeightT = 10, _
            .WeightPerAxleT = 10, _
            .LimitedWeightT = 20, _
            .RT = True _
        }

        Dim optimizationParameters As OptimizationParameters = New OptimizationParameters() With { _
            .Addresses = addresses, _
            .Parameters = parameters _
        }
        Dim errorString As String
        dataObject = route4Me.RunAsyncOptimization(optimizationParameters, errorString)
        Assert.IsNotNull(dataObject, "SingleDriverMultipleTimeWindowsTest failed... " & errorString)
        tdr.RemoveOptimization(New String() {dataObject.OptimizationProblemId})
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
        Dim myApiKey As String = ApiKeys.actualApiKey

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
        Dim errorString As String = ""
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

    <TestMethod>
    Public Sub Route300StopsTest()
        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim addresses As Address() = New Address() {New Address() With {
            .AddressString = "449 Schiller st Elizabeth, NJ, 07206",
            .[Alias] = "449 Schiller st Elizabeth, NJ, 07206",
            .IsDepot = True,
            .Latitude = 40.6608211,
            .Longitude = -74.1827578,
            .Time = 0
        }, New Address() With {
            .AddressString = "24 Convenience Store LLC, 2519 Pacific Ave, Atlantic City, NJ, 08401",
            .[Alias] = "24 Convenience Store LLC, 2519 Pacific Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.355035,
            .Longitude = -74.441433,
            .Time = 0
        }, New Address() With {
            .AddressString = "24/7, 1406 Atlantic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "24/7, 1406 Atlantic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.361713,
            .Longitude = -74.428145,
            .Time = 0
        }, New Address() With {
            .AddressString = "6-12 Convienece, 1 South Main Street, Marlboro, NJ, 07746",
            .[Alias] = "6-12 Convienece, 1 South Main Street, Marlboro, NJ, 07746",
            .IsDepot = False,
            .Latitude = 40.314719,
            .Longitude = -74.248578,
            .Time = 0
        }, New Address() With {
            .AddressString = "6Th Ave Groc, 214 6th Ave, Newark, NJ, 07102",
            .[Alias] = "6Th Ave Groc, 214 6th Ave, Newark, NJ, 07102",
            .IsDepot = False,
            .Latitude = 40.756385,
            .Longitude = -74.187419,
            .Time = 0
        }, New Address() With {
            .AddressString = "76 Express Mart, 46 Ryan Rd, Manalapan, NJ, 07726",
            .[Alias] = "76 Express Mart, 46 Ryan Rd, Manalapan, NJ, 07726",
            .IsDepot = False,
            .Latitude = 40.301426,
            .Longitude = -74.259929,
            .Time = 0
        }, New Address() With {
            .AddressString = "801 W Groc, 801 N Indiana, Atlantic City, NJ, 08401",
            .[Alias] = "801 W Groc, 801 N Indiana, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.368782,
            .Longitude = -74.438739,
            .Time = 0
        }, New Address() With {
            .AddressString = "91 Farmers Market, 34 Lanes Mill Road, Bricktown, NJ, 08724",
            .[Alias] = "91 Farmers Market, 34 Lanes Mill Road, Bricktown, NJ, 08724",
            .IsDepot = False,
            .Latitude = 40.095338,
            .Longitude = -74.144739,
            .Time = 0
        }, New Address() With {
            .AddressString = "A&L Mini, 103 Central Ave, Newark, NJ, 07103",
            .[Alias] = "A&L Mini, 103 Central Ave, Newark, NJ, 07103",
            .IsDepot = False,
            .Latitude = 40.763848,
            .Longitude = -74.228196,
            .Time = 0
        }, New Address() With {
            .AddressString = "AC Deli & Food Market, 3104 Pacific Ave, Atlantic City, NJ, 08401",
            .[Alias] = "AC Deli & Food Market, 3104 Pacific Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.351864,
            .Longitude = -74.448293,
            .Time = 0
        }, New Address() With {
            .AddressString = "AC Food Market & Deli 2, 2401 Pacific Ave, Atlantic City, NJ, 08401",
            .[Alias] = "AC Food Market & Deli 2, 2401 Pacific Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.357207,
            .Longitude = -74.440922,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ag Mini, 503 Gregory Ave, Passaic, NJ, 07055",
            .[Alias] = "Ag Mini, 503 Gregory Ave, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.864225,
            .Longitude = -74.139027,
            .Time = 0
        }, New Address() With {
            .AddressString = "Alexca Groc, 525 Elizabeth Ave, Newark, NJ, 07108",
            .[Alias] = "Alexca Groc, 525 Elizabeth Ave, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.708466,
            .Longitude = -74.201882,
            .Time = 0
        }, New Address() With {
            .AddressString = "Alpha And Omega, 404 Oriental Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Alpha And Omega, 404 Oriental Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.36423,
            .Longitude = -74.414019,
            .Time = 0
        }, New Address() With {
            .AddressString = "Always at Home Adult Day care, 8a Jocama Blvd, OldBridge, NJ, 08857",
            .[Alias] = "Always at Home Adult Day care, 8a Jocama Blvd, OldBridge, NJ, 08857",
            .IsDepot = False,
            .Latitude = 40.37812,
            .Longitude = -74.305547,
            .Time = 0
        }, New Address() With {
            .AddressString = "AM PM, 1338 Atlantic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "AM PM, 1338 Atlantic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.362037,
            .Longitude = -74.427806,
            .Time = 0
        }, New Address() With {
            .AddressString = "Amaury Groc, 84 North Walnut, East Orange, NJ, 07021",
            .[Alias] = "Amaury Groc, 84 North Walnut, East Orange, NJ, 07021",
            .IsDepot = False,
            .Latitude = 40.76518,
            .Longitude = -74.211008,
            .Time = 0
        }, New Address() With {
            .AddressString = "American Way Food, 2005 Route 35 North, Oakhurst, NJ, 07755",
            .[Alias] = "American Way Food, 2005 Route 35 North, Oakhurst, NJ, 07755",
            .IsDepot = False,
            .Latitude = 40.263924,
            .Longitude = -74.040861,
            .Time = 0
        }, New Address() With {
            .AddressString = "Amezquita, 126 Gouvernor St, Paterson, NJ, 07524",
            .[Alias] = "Amezquita, 126 Gouvernor St, Paterson, NJ, 07524",
            .IsDepot = False,
            .Latitude = 40.922167,
            .Longitude = -74.163824,
            .Time = 0
        }, New Address() With {
            .AddressString = "Anita's Corner Deli, 664 Brace Avenue, Perth Amboy, NJ, 08861",
            .[Alias] = "Anita's Corner Deli, 664 Brace Avenue, Perth Amboy, NJ, 08861",
            .IsDepot = False,
            .Latitude = 40.524289,
            .Longitude = -74.287035,
            .Time = 0
        }, New Address() With {
            .AddressString = "Anthony's Pizza, 65 Church Street, Keansburg, NJ, 07734",
            .[Alias] = "Anthony's Pizza, 65 Church Street, Keansburg, NJ, 07734",
            .IsDepot = False,
            .Latitude = 40.441791,
            .Longitude = -74.133082,
            .Time = 0
        }, New Address() With {
            .AddressString = "Antonia's Café, 47 3rd Avenue, Long Branch, NJ, 07740",
            .[Alias] = "Antonia's Café, 47 3rd Avenue, Long Branch, NJ, 07740",
            .IsDepot = False,
            .Latitude = 40.302707,
            .Longitude = -73.987299,
            .Time = 0
        }, New Address() With {
            .AddressString = "AP Grocery, 534 Broadway, Elmwood Park, NJ, 07407",
            .[Alias] = "AP Grocery, 534 Broadway, Elmwood Park, NJ, 07407",
            .IsDepot = False,
            .Latitude = 40.918104,
            .Longitude = -74.151194,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ashley Groc, 506 Clinton St, Newark, NJ, 07108",
            .[Alias] = "Ashley Groc, 506 Clinton St, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.721587,
            .Longitude = -74.201352,
            .Time = 0
        }, New Address() With {
            .AddressString = "Atlantic Bagel Co, 113 E River Road, Rumson, NJ, 07760",
            .[Alias] = "Atlantic Bagel Co, 113 E River Road, Rumson, NJ, 07760",
            .IsDepot = False,
            .Latitude = 40.371677,
            .Longitude = -73.999631,
            .Time = 0
        }, New Address() With {
            .AddressString = "Atlantic Bagel Co, 283 Route 35, Middletown, NJ, 07701",
            .[Alias] = "Atlantic Bagel Co, 283 Route 35, Middletown, NJ, 07701",
            .IsDepot = False,
            .Latitude = 40.366843,
            .Longitude = -74.08326,
            .Time = 0
        }, New Address() With {
            .AddressString = "Atlantic Bagel Co, 642 Newman spring Rd, Lincroft, NJ, 07738",
            .[Alias] = "Atlantic Bagel Co, 642 Newman spring Rd, Lincroft, NJ, 07738",
            .IsDepot = False,
            .Latitude = 40.366843,
            .Longitude = -74.08326,
            .Time = 0
        }, New Address() With {
            .AddressString = "Atlantic Bagel Co, 74 1st Avenue, Atlantic Highlands, NJ, 07732",
            .[Alias] = "Atlantic Bagel Co, 74 1st Avenue, Atlantic Highlands, NJ, 07732",
            .IsDepot = False,
            .Latitude = 40.4138,
            .Longitude = -74.037514,
            .Time = 0
        }, New Address() With {
            .AddressString = "Atlantic City Fuel, 864 N Main St, Pleasantville, NJ, 08232",
            .[Alias] = "Atlantic City Fuel, 864 N Main St, Pleasantville, NJ, 08232",
            .IsDepot = False,
            .Latitude = 39.403741,
            .Longitude = -74.511303,
            .Time = 0
        }, New Address() With {
            .AddressString = "Atlantic City Gas, 8006 Black Horse Pike, Pleasantville, NJ, 08232",
            .[Alias] = "Atlantic City Gas, 8006 Black Horse Pike, Pleasantville, NJ, 08232",
            .IsDepot = False,
            .Latitude = 39.380853,
            .Longitude = -74.495093,
            .Time = 0
        }, New Address() With {
            .AddressString = "Awan Convience, 3701 Vetnor Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Awan Convience, 3701 Vetnor Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.351437,
            .Longitude = -74.455519,
            .Time = 0
        }, New Address() With {
            .AddressString = "Babes Corner, 132 Sumner Avenue, Seaside Heights, NJ, 08751",
            .[Alias] = "Babes Corner, 132 Sumner Avenue, Seaside Heights, NJ, 08751",
            .IsDepot = False,
            .Latitude = 39.941312,
            .Longitude = -74.074916,
            .Time = 0
        }, New Address() With {
            .AddressString = "Bagel Mania, 347 Matawan Rd, Lawrence Harbor, NJ, 08879",
            .[Alias] = "Bagel Mania, 347 Matawan Rd, Lawrence Harbor, NJ, 08879",
            .IsDepot = False,
            .Latitude = 40.430159,
            .Longitude = -74.251723,
            .Time = 0
        }, New Address() With {
            .AddressString = "Bagel One, 700 Old Bridge Tpke, South River, NJ, 08882",
            .[Alias] = "Bagel One, 700 Old Bridge Tpke, South River, NJ, 08882",
            .IsDepot = False,
            .Latitude = 40.462466,
            .Longitude = -74.402632,
            .Time = 0
        }, New Address() With {
            .AddressString = "Bagel One, 777 Washington Road, Parlin, NJ, 08859",
            .[Alias] = "Bagel One, 777 Washington Road, Parlin, NJ, 08859",
            .IsDepot = False,
            .Latitude = 40.462783,
            .Longitude = -74.327999,
            .Time = 0
        }, New Address() With {
            .AddressString = "Bagel Station, 168 Monmouth St, Red Bank, NJ, 07721",
            .[Alias] = "Bagel Station, 168 Monmouth St, Red Bank, NJ, 07721",
            .IsDepot = False,
            .Latitude = 40.348985,
            .Longitude = -74.073624,
            .Time = 0
        }, New Address() With {
            .AddressString = "Barry Mini Mart, 498 12th st, Paterson, NJ, 07504",
            .[Alias] = "Barry Mini Mart, 498 12th st, Paterson, NJ, 07504",
            .IsDepot = False,
            .Latitude = 40.91279,
            .Longitude = -74.138676,
            .Time = 0
        }, New Address() With {
            .AddressString = "Best Tropical Grocery 2, 284 Verona Ave, Passaic, NJ, 07055",
            .[Alias] = "Best Tropical Grocery 2, 284 Verona Ave, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.782701,
            .Longitude = -74.166163,
            .Time = 0
        }, New Address() With {
            .AddressString = "Bevaquas, 305 Port Monmouth Rd, Middleton, NJ, 07748",
            .[Alias] = "Bevaquas, 305 Port Monmouth Rd, Middleton, NJ, 07748",
            .IsDepot = False,
            .Latitude = 40.442036,
            .Longitude = -74.116429,
            .Time = 0
        }, New Address() With {
            .AddressString = "Bhavani, 1050 Route 9 South, Old Bridge, NJ, 08859",
            .[Alias] = "Bhavani, 1050 Route 9 South, Old Bridge, NJ, 08859",
            .IsDepot = False,
            .Latitude = 40.452799,
            .Longitude = -74.299858,
            .Time = 0
        }, New Address() With {
            .AddressString = "Big Hamilton Grocery, 117 Hamilton Avenue, Paterson, NJ, 07514",
            .[Alias] = "Big Hamilton Grocery, 117 Hamilton Avenue, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.920487,
            .Longitude = -74.166298,
            .Time = 0
        }, New Address() With {
            .AddressString = "BP Gas Station, 409 Rt 46 West, Newark, NJ, 07104",
            .[Alias] = "BP Gas Station, 409 Rt 46 West, Newark, NJ, 07104",
            .IsDepot = False,
            .Latitude = 40.893342,
            .Longitude = -74.107102,
            .Time = 0
        }, New Address() With {
            .AddressString = "BP Gas Station, 780 Market St, Newark, NJ, 07112",
            .[Alias] = "BP Gas Station, 780 Market St, Newark, NJ, 07112",
            .IsDepot = False,
            .Latitude = 40.905749,
            .Longitude = -74.147813,
            .Time = 0
        }, New Address() With {
            .AddressString = "Bray Ave Deli, 190 Bray Ave, Middletown, NJ, 07748",
            .[Alias] = "Bray Ave Deli, 190 Bray Ave, Middletown, NJ, 07748",
            .IsDepot = False,
            .Latitude = 40.436711,
            .Longitude = -74.111739,
            .Time = 0
        }, New Address() With {
            .AddressString = "Brennans Deli, 4 W River Rd, Rumson, NJ, 07760",
            .[Alias] = "Brennans Deli, 4 W River Rd, Rumson, NJ, 07760",
            .IsDepot = False,
            .Latitude = 40.374892,
            .Longitude = -74.013428,
            .Time = 0
        }, New Address() With {
            .AddressString = "Brick Convenience, 438 Mantoloking Road, Brick, NJ, 08723",
            .[Alias] = "Brick Convenience, 438 Mantoloking Road, Brick, NJ, 08723",
            .IsDepot = False,
            .Latitude = 40.045475,
            .Longitude = -74.094392,
            .Time = 0
        }, New Address() With {
            .AddressString = "Brothers Produce, 327 East Railway Ave, Passaic, NJ, 07055",
            .[Alias] = "Brothers Produce, 327 East Railway Ave, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.891322,
            .Longitude = -74.149694,
            .Time = 0
        }, New Address() With {
            .AddressString = "Brown Bag Convience, 297 Spotswood Englishtown Rd, Monroe, NJ, 08831",
            .[Alias] = "Brown Bag Convience, 297 Spotswood Englishtown Rd, Monroe, NJ, 08831",
            .IsDepot = False,
            .Latitude = 40.380837,
            .Longitude = -74.388253,
            .Time = 0
        }, New Address() With {
            .AddressString = "Butler Food Store, 109 Easton Avenue, New Brunswick, NJ, 08901",
            .[Alias] = "Butler Food Store, 109 Easton Avenue, New Brunswick, NJ, 08901",
            .IsDepot = False,
            .Latitude = 40.499122,
            .Longitude = -74.451908,
            .Time = 0
        }, New Address() With {
            .AddressString = "Café Columbia, 495 Mcbride Ave, Irvington, NJ, 07111",
            .[Alias] = "Café Columbia, 495 Mcbride Ave, Irvington, NJ, 07111",
            .IsDepot = False,
            .Latitude = 40.734721,
            .Longitude = -74.223831,
            .Time = 0
        }, New Address() With {
            .AddressString = "Café Sical, 56 Obert Street, South River, NJ, 08882",
            .[Alias] = "Café Sical, 56 Obert Street, South River, NJ, 08882",
            .IsDepot = False,
            .Latitude = 40.45067,
            .Longitude = -74.380567,
            .Time = 0
        }, New Address() With {
            .AddressString = "Calis General Str, 2701 Atlantic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Calis General Str, 2701 Atlantic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.35569,
            .Longitude = -74.444721,
            .Time = 0
        }, New Address() With {
            .AddressString = "Carolyn Park Ave Groc, 76 Park Ave, Hackensack, NJ, 07601",
            .[Alias] = "Carolyn Park Ave Groc, 76 Park Ave, Hackensack, NJ, 07601",
            .IsDepot = False,
            .Latitude = 40.888972,
            .Longitude = -74.045214,
            .Time = 0
        }, New Address() With {
            .AddressString = "Cavo Crepe, 122 North Michigan Avenue, Atlantic City, NJ, 08401",
            .[Alias] = "Cavo Crepe, 122 North Michigan Avenue, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.361182,
            .Longitude = -74.437285,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ccs - New Vista, 300 Broadway, Cedar Grove, NJ, 07009",
            .[Alias] = "Ccs - New Vista, 300 Broadway, Cedar Grove, NJ, 07009",
            .IsDepot = False,
            .Latitude = 40.76121,
            .Longitude = -74.169224,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ccs- Fountainview, 527 River Avenue, Lakewood, NJ, 08701",
            .[Alias] = "Ccs- Fountainview, 527 River Avenue, Lakewood, NJ, 08701",
            .IsDepot = False,
            .Latitude = 40.074549,
            .Longitude = -74.215903,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ccs-Tallwoods, 18 Butler Blvd, Bayville, NJ, 08721",
            .[Alias] = "Ccs-Tallwoods, 18 Butler Blvd, Bayville, NJ, 08721",
            .IsDepot = False,
            .Latitude = 39.887461,
            .Longitude = -74.156648,
            .Time = 0
        }, New Address() With {
            .AddressString = "Cedar 15, 501 Atlantic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Cedar 15, 501 Atlantic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.368863,
            .Longitude = -74.416528,
            .Time = 0
        }, New Address() With {
            .AddressString = "Cedar Meat Market, 6407 Vetnor Avenue, Vetnor, NJ, 08406",
            .[Alias] = "Cedar Meat Market, 6407 Vetnor Avenue, Vetnor, NJ, 08406",
            .IsDepot = False,
            .Latitude = 39.338153,
            .Longitude = -74.482597,
            .Time = 0
        }, New Address() With {
            .AddressString = "Center City Deli, 1714 Atlantic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Center City Deli, 1714 Atlantic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.360264,
            .Longitude = -74.432264,
            .Time = 0
        }, New Address() With {
            .AddressString = "Charlie'S Deli, 164 Port Monmouth, Keansburg, NJ, 07734",
            .[Alias] = "Charlie'S Deli, 164 Port Monmouth, Keansburg, NJ, 07734",
            .IsDepot = False,
            .Latitude = 40.441981,
            .Longitude = -74.12276,
            .Time = 0
        }, New Address() With {
            .AddressString = "Chikeeza, 1305 Baltic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Chikeeza, 1305 Baltic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.365509,
            .Longitude = -74.429001,
            .Time = 0
        }, New Address() With {
            .AddressString = "Choice Food, 182 Route 35 North, Keyport, NJ, 07735",
            .[Alias] = "Choice Food, 182 Route 35 North, Keyport, NJ, 07735",
            .IsDepot = False,
            .Latitude = 40.449313,
            .Longitude = -74.236787,
            .Time = 0
        }, New Address() With {
            .AddressString = "Circle K, 102 Bay Avenue, Highlands, NJ, 07732",
            .[Alias] = "Circle K, 102 Bay Avenue, Highlands, NJ, 07732",
            .IsDepot = False,
            .Latitude = 40.400419,
            .Longitude = -73.984715,
            .Time = 0
        }, New Address() With {
            .AddressString = "Circle K, 2001 Ridgeway Road, Toms River, NJ, 08757",
            .[Alias] = "Circle K, 2001 Ridgeway Road, Toms River, NJ, 08757",
            .IsDepot = False,
            .Latitude = 40.006828,
            .Longitude = -74.242188,
            .Time = 0
        }, New Address() With {
            .AddressString = "Circle K, 220 Oceangate Drive, Bayville, NJ, 08721",
            .[Alias] = "Circle K, 220 Oceangate Drive, Bayville, NJ, 08721",
            .IsDepot = False,
            .Latitude = 39.916714,
            .Longitude = -74.15386,
            .Time = 0
        }, New Address() With {
            .AddressString = "Citgi Come and Go, 519 Route 33, Millstone, NJ, 08535",
            .[Alias] = "Citgi Come and Go, 519 Route 33, Millstone, NJ, 08535",
            .IsDepot = False,
            .Latitude = 40.260143,
            .Longitude = -74.409921,
            .Time = 0
        }, New Address() With {
            .AddressString = "Citgo Gas Station, 473 Broadway, Paterson, NJ, 07501",
            .[Alias] = "Citgo Gas Station, 473 Broadway, Paterson, NJ, 07501",
            .IsDepot = False,
            .Latitude = 40.918597,
            .Longitude = -74.154093,
            .Time = 0
        }, New Address() With {
            .AddressString = "Citrus Rest, 305 Main St, W Paterson, NJ, 07424",
            .[Alias] = "Citrus Rest, 305 Main St, W Paterson, NJ, 07424",
            .IsDepot = False,
            .Latitude = 40.887559,
            .Longitude = -74.041441,
            .Time = 0
        }, New Address() With {
            .AddressString = "City Farm, 294 North 8th St, Paterson, NJ, 07501",
            .[Alias] = "City Farm, 294 North 8th St, Paterson, NJ, 07501",
            .IsDepot = False,
            .Latitude = 40.933369,
            .Longitude = -74.172208,
            .Time = 0
        }, New Address() With {
            .AddressString = "City Mkt, 26 S Main St, Pleasantville, NJ, 08232",
            .[Alias] = "City Mkt, 26 S Main St, Pleasantville, NJ, 08232",
            .IsDepot = False,
            .Latitude = 39.391235,
            .Longitude = -74.522571,
            .Time = 0
        }, New Address() With {
            .AddressString = "Clinton News, 31 Clinton Street, Passaic, NJ, 07055",
            .[Alias] = "Clinton News, 31 Clinton Street, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.735982,
            .Longitude = -74.169955,
            .Time = 0
        }, New Address() With {
            .AddressString = "Collins Convience, 201 E Collins Ave, Galloway, NJ, 08025",
            .[Alias] = "Collins Convience, 201 E Collins Ave, Galloway, NJ, 08025",
            .IsDepot = False,
            .Latitude = 39.491728,
            .Longitude = -74.503715,
            .Time = 0
        }, New Address() With {
            .AddressString = "Community Deli, 546 Market St, East Orange, NJ, 07042",
            .[Alias] = "Community Deli, 546 Market St, East Orange, NJ, 07042",
            .IsDepot = False,
            .Latitude = 40.911747,
            .Longitude = -74.155516,
            .Time = 0
        }, New Address() With {
            .AddressString = "Convenience Corner, 355 Monmouth Road, West Long Branch, NJ, 07764",
            .[Alias] = "Convenience Corner, 355 Monmouth Road, West Long Branch, NJ, 07764",
            .IsDepot = False,
            .Latitude = 40.2842,
            .Longitude = -74.02012,
            .Time = 0
        }, New Address() With {
            .AddressString = "Correita El Paisa, 326 21st Ave, Paterson, NJ, 07514",
            .[Alias] = "Correita El Paisa, 326 21st Ave, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.906704,
            .Longitude = -74.158671,
            .Time = 0
        }, New Address() With {
            .AddressString = "Cositas Ricas, 535 21st Ave, Paterson, NJ, 07504",
            .[Alias] = "Cositas Ricas, 535 21st Ave, Paterson, NJ, 07504",
            .IsDepot = False,
            .Latitude = 40.90601,
            .Longitude = -74.150362,
            .Time = 0
        }, New Address() With {
            .AddressString = "Country Farm, 1320 Seagirt Avenue, Seagirt, NJ, 08750",
            .[Alias] = "Country Farm, 1320 Seagirt Avenue, Seagirt, NJ, 08750",
            .IsDepot = False,
            .Latitude = 40.135683,
            .Longitude = -74.062333,
            .Time = 0
        }, New Address() With {
            .AddressString = "Country Farms, 125 Main Street, Bradley Beach, NJ, 07720",
            .[Alias] = "Country Farms, 125 Main Street, Bradley Beach, NJ, 07720",
            .IsDepot = False,
            .Latitude = 40.200035,
            .Longitude = -74.019095,
            .Time = 0
        }, New Address() With {
            .AddressString = "Country Farms, 2588 Tilton Rd, Egg Harbor, NJ, 08234",
            .[Alias] = "Country Farms, 2588 Tilton Rd, Egg Harbor, NJ, 08234",
            .IsDepot = False,
            .Latitude = 39.416868,
            .Longitude = -74.569141,
            .Time = 0
        }, New Address() With {
            .AddressString = "Country Farms, 3122 Route 88, Point Pleasant, NJ, 08742",
            .[Alias] = "Country Farms, 3122 Route 88, Point Pleasant, NJ, 08742",
            .IsDepot = False,
            .Latitude = 40.079909,
            .Longitude = -74.083889,
            .Time = 0
        }, New Address() With {
            .AddressString = "Country Farms, 892 Fisher Blvd, Toms River, NJ, 08753",
            .[Alias] = "Country Farms, 892 Fisher Blvd, Toms River, NJ, 08753",
            .IsDepot = False,
            .Latitude = 39.973935,
            .Longitude = -74.137087,
            .Time = 0
        }, New Address() With {
            .AddressString = "Country Food Market, 921 Atlantic City Blvd, Bayville, NJ, 08721",
            .[Alias] = "Country Food Market, 921 Atlantic City Blvd, Bayville, NJ, 08721",
            .IsDepot = False,
            .Latitude = 39.882705,
            .Longitude = -74.164435,
            .Time = 0
        }, New Address() With {
            .AddressString = "Country Store Raceway, 454 Rt 33 West, Millstone, NJ, 07726",
            .[Alias] = "Country Store Raceway, 454 Rt 33 West, Millstone, NJ, 07726",
            .IsDepot = False,
            .Latitude = 40.258843,
            .Longitude = -74.398019,
            .Time = 0
        }, New Address() With {
            .AddressString = "Crossroads Deli, 479 Route 79, Morganville, NJ, 07751",
            .[Alias] = "Crossroads Deli, 479 Route 79, Morganville, NJ, 07751",
            .IsDepot = False,
            .Latitude = 40.383938,
            .Longitude = -74.241525,
            .Time = 0
        }, New Address() With {
            .AddressString = "Crystal Express Deli, 308 Ernston Road, Parlin, NJ, 08859",
            .[Alias] = "Crystal Express Deli, 308 Ernston Road, Parlin, NJ, 08859",
            .IsDepot = False,
            .Latitude = 40.458048,
            .Longitude = -74.305937,
            .Time = 0
        }, New Address() With {
            .AddressString = "Crystal Kitchen, 1600 1600 Perrinville Rd, Monroe, NJ, 08831",
            .[Alias] = "Crystal Kitchen, 1600 1600 Perrinville Rd, Monroe, NJ, 08831",
            .IsDepot = False,
            .Latitude = 40.316134,
            .Longitude = -74.440031,
            .Time = 0
        }, New Address() With {
            .AddressString = "Deal Food, 112 Norwood Ave, Deal, NJ, 67723",
            .[Alias] = "Deal Food, 112 Norwood Ave, Deal, NJ, 67723",
            .IsDepot = False,
            .Latitude = 40.253485,
            .Longitude = -74.000852,
            .Time = 0
        }, New Address() With {
            .AddressString = "Dehuit Market, 70 Market Street, Passaic, NJ, 07055",
            .[Alias] = "Dehuit Market, 70 Market Street, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.863711,
            .Longitude = -74.116357,
            .Time = 0
        }, New Address() With {
            .AddressString = "Delta Gas, 100 Madison Avenue, Lakewood, NJ, 08701",
            .[Alias] = "Delta Gas, 100 Madison Avenue, Lakewood, NJ, 08701",
            .IsDepot = False,
            .Latitude = 40.091107,
            .Longitude = -74.216751,
            .Time = 0
        }, New Address() With {
            .AddressString = "Demarcos, 3809 Crossan Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Demarcos, 3809 Crossan Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.358413,
            .Longitude = -74.462155,
            .Time = 0
        }, New Address() With {
            .AddressString = "Dios Fe Ezperanza, 548 Market St, Orange, NJ, 07050",
            .[Alias] = "Dios Fe Ezperanza, 548 Market St, Orange, NJ, 07050",
            .IsDepot = False,
            .Latitude = 40.768005,
            .Longitude = -74.232605,
            .Time = 0
        }, New Address() With {
            .AddressString = "Dollar Variety, 292 Main St, Paterson, NJ, 07502",
            .[Alias] = "Dollar Variety, 292 Main St, Paterson, NJ, 07502",
            .IsDepot = False,
            .Latitude = 40.915152,
            .Longitude = -74.173859,
            .Time = 0
        }, New Address() With {
            .AddressString = "Doms Cherry Street Deli, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
            .[Alias] = "Doms Cherry Street Deli, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
            .IsDepot = False,
            .Latitude = 40.332559,
            .Longitude = -74.074423,
            .Time = 0
        }, New Address() With {
            .AddressString = "Doniele Lotz, 206-220 First Ave, Asbury Park, NJ, 07712",
            .[Alias] = "Doniele Lotz, 206-220 First Ave, Asbury Park, NJ, 07712",
            .IsDepot = False,
            .Latitude = 40.219227,
            .Longitude = -74.003708,
            .Time = 0
        }, New Address() With {
            .AddressString = "Dover Market, 3920 Vetnor Avenue, Atlantic City, NJ, 08401",
            .[Alias] = "Dover Market, 3920 Vetnor Avenue, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.349847,
            .Longitude = -74.457832,
            .Time = 0
        }, New Address() With {
            .AddressString = "Dunkin Donuts 117-02, 545 Chancellor Ave, Paterson, NJ, 07513",
            .[Alias] = "Dunkin Donuts 117-02, 545 Chancellor Ave, Paterson, NJ, 07513",
            .IsDepot = False,
            .Latitude = 40.713875,
            .Longitude = -74.229677,
            .Time = 0
        }, New Address() With {
            .AddressString = "El Apache, 44 East Front Street, Keyport, NJ, 07735",
            .[Alias] = "El Apache, 44 East Front Street, Keyport, NJ, 07735",
            .IsDepot = False,
            .Latitude = 40.438094,
            .Longitude = -74.199867,
            .Time = 0
        }, New Address() With {
            .AddressString = "El Bohio, 510 Park Ave, Paterson, NJ, 07504",
            .[Alias] = "El Bohio, 510 Park Ave, Paterson, NJ, 07504",
            .IsDepot = False,
            .Latitude = 40.913352,
            .Longitude = -74.143493,
            .Time = 0
        }, New Address() With {
            .AddressString = "El Colmado Supermarket, 126 Hope Street, Passaic, NJ, 07055",
            .[Alias] = "El Colmado Supermarket, 126 Hope Street, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.867712,
            .Longitude = -74.122705,
            .Time = 0
        }, New Address() With {
            .AddressString = "El Paisa, 471 21st Ave, Irvington, NJ, 07111",
            .[Alias] = "El Paisa, 471 21st Ave, Irvington, NJ, 07111",
            .IsDepot = False,
            .Latitude = 40.906332,
            .Longitude = -74.153318,
            .Time = 0
        }, New Address() With {
            .AddressString = "El Poblano Deli & Grocery, 1241 Lakewood Rd, Toms River, NJ, 08753",
            .[Alias] = "El Poblano Deli & Grocery, 1241 Lakewood Rd, Toms River, NJ, 08753",
            .IsDepot = False,
            .Latitude = 39.985037,
            .Longitude = -74.20969,
            .Time = 0
        }, New Address() With {
            .AddressString = "El Triangulo, 156 Hawthorne Ave, Paterson, NJ, 07514",
            .[Alias] = "El Triangulo, 156 Hawthorne Ave, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.949274,
            .Longitude = -74.149605,
            .Time = 0
        }, New Address() With {
            .AddressString = "Eliany Groc, 146 Sherman Ave, Newark, NJ, 07108",
            .[Alias] = "Eliany Groc, 146 Sherman Ave, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.720118,
            .Longitude = -74.186768,
            .Time = 0
        }, New Address() With {
            .AddressString = "Eli's Bagels, 1055 Route 34 North, Matawan, NJ, 07747",
            .[Alias] = "Eli's Bagels, 1055 Route 34 North, Matawan, NJ, 07747",
            .IsDepot = False,
            .Latitude = 40.401578,
            .Longitude = -74.228494,
            .Time = 0
        }, New Address() With {
            .AddressString = "Era Min Mart, 291 Clinton Place, Newark, NJ, 07105",
            .[Alias] = "Era Min Mart, 291 Clinton Place, Newark, NJ, 07105",
            .IsDepot = False,
            .Latitude = 40.713666,
            .Longitude = -74.214332,
            .Time = 0
        }, New Address() With {
            .AddressString = "Essex County Hospital Center, 204 Grove St, Irvington, NJ, 07111",
            .[Alias] = "Essex County Hospital Center, 204 Grove St, Irvington, NJ, 07111",
            .IsDepot = False,
            .Latitude = 40.851854,
            .Longitude = -74.234064,
            .Time = 0
        }, New Address() With {
            .AddressString = "Exxon Mart, 3164  Highway 88, Point Pleasant, NJ, 08742",
            .[Alias] = "Exxon Mart, 3164  Highway 88, Point Pleasant, NJ, 08742",
            .IsDepot = False,
            .Latitude = 40.079245,
            .Longitude = -74.087066,
            .Time = 0
        }, New Address() With {
            .AddressString = "Exxon, 200 Rt 46 West, Passaic, NJ, 07055",
            .[Alias] = "Exxon, 200 Rt 46 West, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.872671,
            .Longitude = -74.192393,
            .Time = 0
        }, New Address() With {
            .AddressString = "Exxon, 70 US Rt 9 North, Morganville, NJ, 07751",
            .[Alias] = "Exxon, 70 US Rt 9 North, Morganville, NJ, 07751",
            .IsDepot = False,
            .Latitude = 40.353539,
            .Longitude = -74.306081,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ez Check, 1015 N Wood Ave, Linden, NJ, 07036",
            .[Alias] = "Ez Check, 1015 N Wood Ave, Linden, NJ, 07036",
            .IsDepot = False,
            .Latitude = 40.637435,
            .Longitude = -74.265105,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ez Check, 80 Main St, Sayerville, NJ, 08872",
            .[Alias] = "Ez Check, 80 Main St, Sayerville, NJ, 08872",
            .IsDepot = False,
            .Latitude = 40.460145,
            .Longitude = -74.360907,
            .Time = 0
        }, New Address() With {
            .AddressString = "F & L Groc, 133 Parker Street, Passaic, NJ, 07055",
            .[Alias] = "F & L Groc, 133 Parker Street, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.872287,
            .Longitude = -74.12229,
            .Time = 0
        }, New Address() With {
            .AddressString = "Famous Deli, 400 N Massachusetss Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Famous Deli, 400 N Massachusetss Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.372044,
            .Longitude = -74.421096,
            .Time = 0
        }, New Address() With {
            .AddressString = "Fatima, 222 Park Ave, Paterson, NJ, 07514",
            .[Alias] = "Fatima, 222 Park Ave, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.914935,
            .Longitude = -74.156819,
            .Time = 0
        }, New Address() With {
            .AddressString = "First Cup, 96 First ave, Atlantic Highlands, NJ, 07716",
            .[Alias] = "First Cup, 96 First ave, Atlantic Highlands, NJ, 07716",
            .IsDepot = False,
            .Latitude = 40.413219,
            .Longitude = -74.037804,
            .Time = 0
        }, New Address() With {
            .AddressString = "Five 2 Nine, 241 Highway 36 N, Hazlet, NJ, 07730",
            .[Alias] = "Five 2 Nine, 241 Highway 36 N, Hazlet, NJ, 07730",
            .IsDepot = False,
            .Latitude = 40.437628,
            .Longitude = -74.141357,
            .Time = 0
        }, New Address() With {
            .AddressString = "Flagship, 60 N Main Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Flagship, 60 N Main Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.369874,
            .Longitude = -74.412611,
            .Time = 0
        }, New Address() With {
            .AddressString = "Florida Grocery, 2501 Artic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Florida Grocery, 2501 Artic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.358182,
            .Longitude = -74.443172,
            .Time = 0
        }, New Address() With {
            .AddressString = "G.A.P., 620 15th Avenue, Paterson, NJ, 07501",
            .[Alias] = "G.A.P., 620 15th Avenue, Paterson, NJ, 07501",
            .IsDepot = False,
            .Latitude = 40.9144,
            .Longitude = -74.140202,
            .Time = 0
        }, New Address() With {
            .AddressString = "George Street Ale House, 378 George street, New Brunswick, NJ, 08901",
            .[Alias] = "George Street Ale House, 378 George street, New Brunswick, NJ, 08901",
            .IsDepot = False,
            .Latitude = 40.495678,
            .Longitude = -74.444192,
            .Time = 0
        }, New Address() With {
            .AddressString = "Getty Mart, 1940 St Rt 34, Wall, NJ, 07719",
            .[Alias] = "Getty Mart, 1940 St Rt 34, Wall, NJ, 07719",
            .IsDepot = False,
            .Latitude = 40.158248,
            .Longitude = -74.099694,
            .Time = 0
        }, New Address() With {
            .AddressString = "Glenny Groc, 237 Roseland Ave, East Orange, NJ, 07018",
            .[Alias] = "Glenny Groc, 237 Roseland Ave, East Orange, NJ, 07018",
            .IsDepot = False,
            .Latitude = 40.831038,
            .Longitude = -74.283425,
            .Time = 0
        }, New Address() With {
            .AddressString = "Golden Years Adult Day Care, 108 Woodward Rd, Manalapan, NJ, 07726",
            .[Alias] = "Golden Years Adult Day Care, 108 Woodward Rd, Manalapan, NJ, 07726",
            .IsDepot = False,
            .Latitude = 40.249947,
            .Longitude = -74.366984,
            .Time = 0
        }, New Address() With {
            .AddressString = "Good Neighbor Mini Mkt, 918 Lee Avenue, New Brunswick, NJ, 08902",
            .[Alias] = "Good Neighbor Mini Mkt, 918 Lee Avenue, New Brunswick, NJ, 08902",
            .IsDepot = False,
            .Latitude = 40.473348,
            .Longitude = -74.461946,
            .Time = 0
        }, New Address() With {
            .AddressString = "Grandma's Bagels, 479 Prospect Ave, Little Silver, NJ, 07739",
            .[Alias] = "Grandma's Bagels, 479 Prospect Ave, Little Silver, NJ, 07739",
            .IsDepot = False,
            .Latitude = 40.339464,
            .Longitude = -74.042152,
            .Time = 0
        }, New Address() With {
            .AddressString = "Green Leaf, 870 Springfield Ave, Newark, NJ, 07104",
            .[Alias] = "Green Leaf, 870 Springfield Ave, Newark, NJ, 07104",
            .IsDepot = False,
            .Latitude = 40.728082,
            .Longitude = -74.22216,
            .Time = 0
        }, New Address() With {
            .AddressString = "Grover Groc, 509 Grove St, Irvington, NJ, 07111",
            .[Alias] = "Grover Groc, 509 Grove St, Irvington, NJ, 07111",
            .IsDepot = False,
            .Latitude = 40.739888,
            .Longitude = -74.213131,
            .Time = 0
        }, New Address() With {
            .AddressString = "Hazlet Shell, 1355 Rt 36, Hazlet, NJ, 07730",
            .[Alias] = "Hazlet Shell, 1355 Rt 36, Hazlet, NJ, 07730",
            .IsDepot = False,
            .Latitude = 40.429843,
            .Longitude = -74.188241,
            .Time = 0
        }, New Address() With {
            .AddressString = "Hole Lot of Bagels, 1171 Route 35, Middletown, NJ, 07748",
            .[Alias] = "Hole Lot of Bagels, 1171 Route 35, Middletown, NJ, 07748",
            .IsDepot = False,
            .Latitude = 40.39891,
            .Longitude = -74.111004,
            .Time = 0
        }, New Address() With {
            .AddressString = "Hong Kong Supermarket, 275 Rt 18 South, East Brunswick, NJ, 08816",
            .[Alias] = "Hong Kong Supermarket, 275 Rt 18 South, East Brunswick, NJ, 08816",
            .IsDepot = False,
            .Latitude = 40.459219,
            .Longitude = -74.404777,
            .Time = 0
        }, New Address() With {
            .AddressString = "Hudson Manor, 40 Hudson Street, Old Bridge, NJ, 07728",
            .[Alias] = "Hudson Manor, 40 Hudson Street, Old Bridge, NJ, 07728",
            .IsDepot = False,
            .Latitude = 40.26011,
            .Longitude = -74.270311,
            .Time = 0
        }, New Address() With {
            .AddressString = "I&K Conv Deli, 3109 Bordontown Avenue, Parlin, NJ, 08859",
            .[Alias] = "I&K Conv Deli, 3109 Bordontown Avenue, Parlin, NJ, 08859",
            .IsDepot = False,
            .Latitude = 40.450615,
            .Longitude = -74.314199,
            .Time = 0
        }, New Address() With {
            .AddressString = "Indiana Chicken, 501 N Indiana, Atlantic City, NJ, 08401",
            .[Alias] = "Indiana Chicken, 501 N Indiana, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.366317,
            .Longitude = -74.437075,
            .Time = 0
        }, New Address() With {
            .AddressString = "International Supermarket, 128 Ocean Ave, Lakewood, NJ, 08701",
            .[Alias] = "International Supermarket, 128 Ocean Ave, Lakewood, NJ, 08701",
            .IsDepot = False,
            .Latitude = 40.090073,
            .Longitude = -74.209407,
            .Time = 0
        }, New Address() With {
            .AddressString = "Italian Delight, 226 Adephia Road, Howell, NJ, 07737",
            .[Alias] = "Italian Delight, 226 Adephia Road, Howell, NJ, 07737",
            .IsDepot = False,
            .Latitude = 40.20265,
            .Longitude = -74.196459,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jackies Deli, 3201 Fairmount Avenue, Atlantic City, NJ, 08401",
            .[Alias] = "Jackies Deli, 3201 Fairmount Avenue, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.356058,
            .Longitude = -74.452228,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jays Food Market, 358 Herbertsville Road, Brick, NJ, 08724",
            .[Alias] = "Jays Food Market, 358 Herbertsville Road, Brick, NJ, 08724",
            .IsDepot = False,
            .Latitude = 40.097688,
            .Longitude = -74.10064,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jersey Pride Food Mart, 18 Snowhill Street, Spotswood, NJ, 08884",
            .[Alias] = "Jersey Pride Food Mart, 18 Snowhill Street, Spotswood, NJ, 08884",
            .IsDepot = False,
            .Latitude = 40.391643,
            .Longitude = -74.39227,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jersey Shore Coffee, 64 Thompson Avenue, Leonardo, NJ, 07737",
            .[Alias] = "Jersey Shore Coffee, 64 Thompson Avenue, Leonardo, NJ, 07737",
            .IsDepot = False,
            .Latitude = 40.414913,
            .Longitude = -74.058999,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jersey Shore Deli, 331 Main Street, West Creek, NJ, 08092",
            .[Alias] = "Jersey Shore Deli, 331 Main Street, West Creek, NJ, 08092",
            .IsDepot = False,
            .Latitude = 39.666109,
            .Longitude = -74.280843,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jersey Shore Deli, 613 Hope Road, Eatontown, NJ, 07724",
            .[Alias] = "Jersey Shore Deli, 613 Hope Road, Eatontown, NJ, 07724",
            .IsDepot = False,
            .Latitude = 40.289643,
            .Longitude = -74.078431,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jiminez Groc, 310 14th Ave, Newark, NJ, 07103",
            .[Alias] = "Jiminez Groc, 310 14th Ave, Newark, NJ, 07103",
            .IsDepot = False,
            .Latitude = 40.740142,
            .Longitude = -74.206301,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jimmy Leeds, 68 W Jimmie Leeds Rd, Galloway, NJ, 08025",
            .[Alias] = "Jimmy Leeds, 68 W Jimmie Leeds Rd, Galloway, NJ, 08025",
            .IsDepot = False,
            .Latitude = 39.47564,
            .Longitude = -74.541319,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jj Groc, 246 Summer St, Passaic, NJ, 07055",
            .[Alias] = "Jj Groc, 246 Summer St, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.867849,
            .Longitude = -74.13855,
            .Time = 0
        }, New Address() With {
            .AddressString = "Joes Food Mkt, 401 N Dr Martin King Blvd, Atlantic City, NJ, 08401",
            .[Alias] = "Joes Food Mkt, 401 N Dr Martin King Blvd, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.366221,
            .Longitude = -74.435404,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jose Mini Market, 75 3rd Street, Newark, NJ, 07104",
            .[Alias] = "Jose Mini Market, 75 3rd Street, Newark, NJ, 07104",
            .IsDepot = False,
            .Latitude = 40.743912,
            .Longitude = -74.196031,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jose Spmkt, 219 Tremont Ave, Orange, NJ, 07050",
            .[Alias] = "Jose Spmkt, 219 Tremont Ave, Orange, NJ, 07050",
            .IsDepot = False,
            .Latitude = 40.751429,
            .Longitude = -74.227549,
            .Time = 0
        }, New Address() With {
            .AddressString = "Jr Freeway, 301 Osbourne, Newark, NJ, 07108",
            .[Alias] = "Jr Freeway, 301 Osbourne, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.712976,
            .Longitude = -74.210091,
            .Time = 0
        }, New Address() With {
            .AddressString = "Juquila Mexican Groc, 100 Lee Ave, New Brunswick, NJ, 08901",
            .[Alias] = "Juquila Mexican Groc, 100 Lee Ave, New Brunswick, NJ, 08901",
            .IsDepot = False,
            .Latitude = 40.486612,
            .Longitude = -74.448257,
            .Time = 0
        }, New Address() With {
            .AddressString = "Keyport News, 37 W Front St, Keyport, NJ, 07735",
            .[Alias] = "Keyport News, 37 W Front St, Keyport, NJ, 07735",
            .IsDepot = False,
            .Latitude = 40.437284,
            .Longitude = -74.203406,
            .Time = 0
        }, New Address() With {
            .AddressString = "Kings Highway Glatt, 250 Norwood Avenue, Oakhurst, NJ, 07755",
            .[Alias] = "Kings Highway Glatt, 250 Norwood Avenue, Oakhurst, NJ, 07755",
            .IsDepot = False,
            .Latitude = 40.25995,
            .Longitude = -74.000243,
            .Time = 0
        }, New Address() With {
            .AddressString = "Kpp Grocery, 108 William St, Newark, NJ, 07107",
            .[Alias] = "Kpp Grocery, 108 William St, Newark, NJ, 07107",
            .IsDepot = False,
            .Latitude = 40.752345,
            .Longitude = -74.187634,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krausers, 1227 Halsey St, Newark, NJ, 07103",
            .[Alias] = "Krausers, 1227 Halsey St, Newark, NJ, 07103",
            .IsDepot = False,
            .Latitude = 40.734446,
            .Longitude = -74.174573,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krausers, 200 North Broadway, South Amboy, NJ, 08879",
            .[Alias] = "Krausers, 200 North Broadway, South Amboy, NJ, 08879",
            .IsDepot = False,
            .Latitude = 40.485749,
            .Longitude = -74.283706,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krausers, 595 Palmer Avenue, Hazlet, NJ, 07734",
            .[Alias] = "Krausers, 595 Palmer Avenue, Hazlet, NJ, 07734",
            .IsDepot = False,
            .Latitude = 40.429671,
            .Longitude = -74.132774,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krauszer's Food Store, 3193 Washington Ave, Parlin, NJ, 08859",
            .[Alias] = "Krauszer's Food Store, 3193 Washington Ave, Parlin, NJ, 08859",
            .IsDepot = False,
            .Latitude = 40.468223,
            .Longitude = -74.306587,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krauzer Foods, 58 Jackson Street, South River, NJ, 08882",
            .[Alias] = "Krauzer Foods, 58 Jackson Street, South River, NJ, 08882",
            .IsDepot = False,
            .Latitude = 40.450583,
            .Longitude = -74.382182,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krauzers Food Market, 546 Park Avenue, Freehold, NJ, 07728",
            .[Alias] = "Krauzers Food Market, 546 Park Avenue, Freehold, NJ, 07728",
            .IsDepot = False,
            .Latitude = 40.249606,
            .Longitude = -74.271902,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krauzers South River, 81 Main Street, South River, NJ, 08882",
            .[Alias] = "Krauzers South River, 81 Main Street, South River, NJ, 08882",
            .IsDepot = False,
            .Latitude = 40.450583,
            .Longitude = -74.382182,
            .Time = 0
        }, New Address() With {
            .AddressString = "Krauzers, 309 Main Street, Sayerville, NJ, 08872",
            .[Alias] = "Krauzers, 309 Main Street, Sayerville, NJ, 08872",
            .IsDepot = False,
            .Latitude = 40.470359,
            .Longitude = -74.359202,
            .Time = 0
        }, New Address() With {
            .AddressString = "Kushira, 22-24 Paterson Ave, Newark, NJ, 07105",
            .[Alias] = "Kushira, 22-24 Paterson Ave, Newark, NJ, 07105",
            .IsDepot = False,
            .Latitude = 40.723126,
            .Longitude = -74.141612,
            .Time = 0
        }, New Address() With {
            .AddressString = "Kwik Farms, 590 Shrewsbury Ave, Tinton Falls, NJ, 07724",
            .[Alias] = "Kwik Farms, 590 Shrewsbury Ave, Tinton Falls, NJ, 07724",
            .IsDepot = False,
            .Latitude = 40.330025,
            .Longitude = -74.074258,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Bagel #3, 377 Georges Street, New Brunswick, NJ, 08776",
            .[Alias] = "La Bagel #3, 377 Georges Street, New Brunswick, NJ, 08776",
            .IsDepot = False,
            .Latitude = 40.495696,
            .Longitude = -74.443867,
            .Time = 0
        }, New Address() With {
            .AddressString = "La China Poblana, 114-116 Shrewbury Avenue, Red Bank, NJ, 07701",
            .[Alias] = "La China Poblana, 114-116 Shrewbury Avenue, Red Bank, NJ, 07701",
            .IsDepot = False,
            .Latitude = 40.346942,
            .Longitude = -74.076597,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Chiquita, 36 Astor St, Newark, NJ, 07108",
            .[Alias] = "La Chiquita, 36 Astor St, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.722866,
            .Longitude = -74.183561,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Esperamza Food Market, 279 Ellis Avenue, Newark, NJ, 07103",
            .[Alias] = "La Esperamza Food Market, 279 Ellis Avenue, Newark, NJ, 07103",
            .IsDepot = False,
            .Latitude = 40.756185,
            .Longitude = -74.17351,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Mich Oceana, 109 Taylor Ave, Manasquan, NJ, 08736",
            .[Alias] = "La Mich Oceana, 109 Taylor Ave, Manasquan, NJ, 08736",
            .IsDepot = False,
            .Latitude = 40.124934,
            .Longitude = -74.046072,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Nany, 356 Union Ave, Paterson, NJ, 07503",
            .[Alias] = "La Nany, 356 Union Ave, Paterson, NJ, 07503",
            .IsDepot = False,
            .Latitude = 40.919728,
            .Longitude = -74.187345,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Palma Villa Grocery 1, 18 Broad Street, Freehold, NJ, 07728",
            .[Alias] = "La Palma Villa Grocery 1, 18 Broad Street, Freehold, NJ, 07728",
            .IsDepot = False,
            .Latitude = 40.259859,
            .Longitude = -74.278887,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Posada, 1055 Main Ave, Passaic, NJ, 07055",
            .[Alias] = "La Posada, 1055 Main Ave, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.872648,
            .Longitude = -74.137303,
            .Time = 0
        }, New Address() With {
            .AddressString = "La Tipica Grocery, 2500 Artic Avenue, Atlantic City, NJ, 08401",
            .[Alias] = "La Tipica Grocery, 2500 Artic Avenue, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.357862,
            .Longitude = -74.442969,
            .Time = 0
        }, New Address() With {
            .AddressString = "Las Paisas, 143 Broadway, Prospect Park, NJ, 07508",
            .[Alias] = "Las Paisas, 143 Broadway, Prospect Park, NJ, 07508",
            .IsDepot = False,
            .Latitude = 40.858621,
            .Longitude = -74.130554,
            .Time = 0
        }, New Address() With {
            .AddressString = "Las Placitas Mexicana, 317 Handy St, New Brunswick, NJ, 08070",
            .[Alias] = "Las Placitas Mexicana, 317 Handy St, New Brunswick, NJ, 08070",
            .IsDepot = False,
            .Latitude = 40.490362,
            .Longitude = -74.452509,
            .Time = 0
        }, New Address() With {
            .AddressString = "Latino Groc, 752 River St, Paterson, NJ, 07524",
            .[Alias] = "Latino Groc, 752 River St, Paterson, NJ, 07524",
            .IsDepot = False,
            .Latitude = 40.93793,
            .Longitude = -74.151234,
            .Time = 0
        }, New Address() With {
            .AddressString = "Linar, 162 Monmouth St, Red Bank, NJ, 07701",
            .[Alias] = "Linar, 162 Monmouth St, Red Bank, NJ, 07701",
            .IsDepot = False,
            .Latitude = 40.349033,
            .Longitude = -74.073362,
            .Time = 0
        }, New Address() With {
            .AddressString = "Lincroft Senior Center, 41 Hurley Lane, Lincroft, NJ, 07758",
            .[Alias] = "Lincroft Senior Center, 41 Hurley Lane, Lincroft, NJ, 07758",
            .IsDepot = False,
            .Latitude = 40.33218,
            .Longitude = -74.124581,
            .Time = 0
        }, New Address() With {
            .AddressString = "Long Branch Deli, 156 Long Branch Avenue, Long Branch, NJ, 07740",
            .[Alias] = "Long Branch Deli, 156 Long Branch Avenue, Long Branch, NJ, 07740",
            .IsDepot = False,
            .Latitude = 40.31097,
            .Longitude = -73.984022,
            .Time = 0
        }, New Address() With {
            .AddressString = "Lopez Spmkt, 995 Bergen St, Newark, NJ, 07108",
            .[Alias] = "Lopez Spmkt, 995 Bergen St, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.709927,
            .Longitude = -74.206793,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ls Deli, 175 Elizabeth Ave, Newark, NJ, 07108",
            .[Alias] = "Ls Deli, 175 Elizabeth Ave, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.717116,
            .Longitude = -74.190947,
            .Time = 0
        }, New Address() With {
            .AddressString = "Lucky 7 Deli, 1017 Rt 36, Union Beach, NJ, 07735",
            .[Alias] = "Lucky 7 Deli, 1017 Rt 36, Union Beach, NJ, 07735",
            .IsDepot = False,
            .Latitude = 40.438336,
            .Longitude = -74.163793,
            .Time = 0
        }, New Address() With {
            .AddressString = "Lucky Superstore, 715 Main Street, Asbury Park, NJ, 07712",
            .[Alias] = "Lucky Superstore, 715 Main Street, Asbury Park, NJ, 07712",
            .IsDepot = False,
            .Latitude = 40.220269,
            .Longitude = -74.012208,
            .Time = 0
        }, New Address() With {
            .AddressString = "Luisa Deli & Groc, 123 Elizabeth Ave, Newark, NJ, 07108",
            .[Alias] = "Luisa Deli & Groc, 123 Elizabeth Ave, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.718769,
            .Longitude = -74.190058,
            .Time = 0
        }, New Address() With {
            .AddressString = "Lymensada Mini Mart, 132 East 4th Street, Lakewood, NJ, 08701",
            .[Alias] = "Lymensada Mini Mart, 132 East 4th Street, Lakewood, NJ, 08701",
            .IsDepot = False,
            .Latitude = 40.094515,
            .Longitude = -74.206256,
            .Time = 0
        }, New Address() With {
            .AddressString = "Madison Deli, 69 Route 34, South Amboy, NJ, 08831",
            .[Alias] = "Madison Deli, 69 Route 34, South Amboy, NJ, 08831",
            .IsDepot = False,
            .Latitude = 40.432301,
            .Longitude = -74.297294,
            .Time = 0
        }, New Address() With {
            .AddressString = "Manhattan Bagel, 720 Newman Springs Rd, Tinton Falls, NJ, 07738",
            .[Alias] = "Manhattan Bagel, 720 Newman Springs Rd, Tinton Falls, NJ, 07738",
            .IsDepot = False,
            .Latitude = 40.331405,
            .Longitude = -74.123225,
            .Time = 0
        }, New Address() With {
            .AddressString = "Manhattan Bagels, 881 Main Street, Sayerville, NJ, 08872",
            .[Alias] = "Manhattan Bagels, 881 Main Street, Sayerville, NJ, 08872",
            .IsDepot = False,
            .Latitude = 40.476242,
            .Longitude = -74.31866,
            .Time = 0
        }, New Address() With {
            .AddressString = "Maywood Mkt, 74 West Pleasant Ave, Hackensack, NJ, 07601",
            .[Alias] = "Maywood Mkt, 74 West Pleasant Ave, Hackensack, NJ, 07601",
            .IsDepot = False,
            .Latitude = 40.904762,
            .Longitude = -74.063701,
            .Time = 0
        }, New Address() With {
            .AddressString = "Mcbride Conv, 500 Mcbride Ave, Paterson, NJ, 07513",
            .[Alias] = "Mcbride Conv, 500 Mcbride Ave, Paterson, NJ, 07513",
            .IsDepot = False,
            .Latitude = 40.907133,
            .Longitude = -74.195232,
            .Time = 0
        }, New Address() With {
            .AddressString = "McKinley Convenience, 100 McKinley Convenience, Manahawkin, NJ, 08050",
            .[Alias] = "McKinley Convenience, 100 McKinley Convenience, Manahawkin, NJ, 08050",
            .IsDepot = False,
            .Latitude = 39.69202,
            .Longitude = -74.268679,
            .Time = 0
        }, New Address() With {
            .AddressString = "Mejias Grc, 164 Madison Ave, Passaic, NJ, 07055",
            .[Alias] = "Mejias Grc, 164 Madison Ave, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.864086,
            .Longitude = -74.124176,
            .Time = 0
        }, New Address() With {
            .AddressString = "Mendoker Quality Bakery, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
            .[Alias] = "Mendoker Quality Bakery, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
            .IsDepot = False,
            .Latitude = 40.348865,
            .Longitude = -74.437009,
            .Time = 0
        }, New Address() With {
            .AddressString = "Metropan, 420 21st Ave, Paterson, NJ, 07514",
            .[Alias] = "Metropan, 420 21st Ave, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.906114,
            .Longitude = -74.154414,
            .Time = 0
        }, New Address() With {
            .AddressString = "Mi Casa, 67 E South St, Freehold, NJ, 07728",
            .[Alias] = "Mi Casa, 67 E South St, Freehold, NJ, 07728",
            .IsDepot = False,
            .Latitude = 40.256763,
            .Longitude = -74.273335,
            .Time = 0
        }, New Address() With {
            .AddressString = "Mid Shore Meats, 801 Fisher Blvd, Toms River, NJ, 08753",
            .[Alias] = "Mid Shore Meats, 801 Fisher Blvd, Toms River, NJ, 08753",
            .IsDepot = False,
            .Latitude = 39.967992,
            .Longitude = -74.131572,
            .Time = 0
        }, New Address() With {
            .AddressString = "Millstone Food Market, 673 Route 33, Millstone, NJ, 08535",
            .[Alias] = "Millstone Food Market, 673 Route 33, Millstone, NJ, 08535",
            .IsDepot = False,
            .Latitude = 40.261439,
            .Longitude = -74.435975,
            .Time = 0
        }, New Address() With {
            .AddressString = "Minute Mart, 148 White Head Ave, South River, NJ, 08882",
            .[Alias] = "Minute Mart, 148 White Head Ave, South River, NJ, 08882",
            .IsDepot = False,
            .Latitude = 40.447007,
            .Longitude = -74.373784,
            .Time = 0
        }, New Address() With {
            .AddressString = "Mm Groc, 1272 Springfield Ave, Passaic, NJ, 07055",
            .[Alias] = "Mm Groc, 1272 Springfield Ave, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.724698,
            .Longitude = -74.241679,
            .Time = 0
        }, New Address() With {
            .AddressString = "Moosas Market, 230 N. New Jersey Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Moosas Market, 230 N. New Jersey Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.369259,
            .Longitude = -74.421825,
            .Time = 0
        }, New Address() With {
            .AddressString = "Morell & Cepeda, 315 21st Street, Paterson, NJ, 07514",
            .[Alias] = "Morell & Cepeda, 315 21st Street, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.907441,
            .Longitude = -74.16033,
            .Time = 0
        }, New Address() With {
            .AddressString = "Morganville Deli & Liquor, 172 Tenant Road, Morganville, NJ, 07751",
            .[Alias] = "Morganville Deli & Liquor, 172 Tenant Road, Morganville, NJ, 07751",
            .IsDepot = False,
            .Latitude = 40.368815,
            .Longitude = -74.264081,
            .Time = 0
        }, New Address() With {
            .AddressString = "Munchies, 314 Sylvania Ave, Neptune, NJ, 07753",
            .[Alias] = "Munchies, 314 Sylvania Ave, Neptune, NJ, 07753",
            .IsDepot = False,
            .Latitude = 40.205113,
            .Longitude = -74.045022,
            .Time = 0
        }, New Address() With {
            .AddressString = "My Placita, 204 Dayron, Paterson, NJ, 07504",
            .[Alias] = "My Placita, 204 Dayron, Paterson, NJ, 07504",
            .IsDepot = False,
            .Latitude = 40.91279,
            .Longitude = -74.138676,
            .Time = 0
        }, New Address() With {
            .AddressString = "Nashville Market, 5101 Vetnor Ave, Vetnor City, NJ, 08406",
            .[Alias] = "Nashville Market, 5101 Vetnor Ave, Vetnor City, NJ, 08406",
            .IsDepot = False,
            .Latitude = 39.344312,
            .Longitude = -74.469856,
            .Time = 0
        }, New Address() With {
            .AddressString = "Natures Reward, 3124 Bridge Ave, Point Pleasant, NJ, 08742",
            .[Alias] = "Natures Reward, 3124 Bridge Ave, Point Pleasant, NJ, 08742",
            .IsDepot = False,
            .Latitude = 40.076345,
            .Longitude = -74.085964,
            .Time = 0
        }, New Address() With {
            .AddressString = "Neptune Deli, 1-211 Route 35 South, Neptune City, NJ, 07753",
            .[Alias] = "Neptune Deli, 1-211 Route 35 South, Neptune City, NJ, 07753",
            .IsDepot = False,
            .Latitude = 40.214545,
            .Longitude = -74.030095,
            .Time = 0
        }, New Address() With {
            .AddressString = "New Bergen Food, 943 Bergen St, Elizabeth, NJ, 07206",
            .[Alias] = "New Bergen Food, 943 Bergen St, Elizabeth, NJ, 07206",
            .IsDepot = False,
            .Latitude = 40.653461,
            .Longitude = -74.197261,
            .Time = 0
        }, New Address() With {
            .AddressString = "New City Grocery, 2608 Pacific Ave, Atlantic City, NJ, 08401",
            .[Alias] = "New City Grocery, 2608 Pacific Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.354239,
            .Longitude = -74.442334,
            .Time = 0
        }, New Address() With {
            .AddressString = "New Farmers Market, 2732 Atlantic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "New Farmers Market, 2732 Atlantic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.354931,
            .Longitude = -74.445481,
            .Time = 0
        }, New Address() With {
            .AddressString = "New Latin Deli, 3623 Winchester Ave, Atlantic City, NJ, 08401",
            .[Alias] = "New Latin Deli, 3623 Winchester Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.35306,
            .Longitude = -74.455537,
            .Time = 0
        }, New Address() With {
            .AddressString = "New York Deli, 649 N. New York avenue, Atlantic City, NJ, 08401",
            .[Alias] = "New York Deli, 649 N. New York avenue, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.369198,
            .Longitude = -74.434158,
            .Time = 0
        }, New Address() With {
            .AddressString = "Nicasias, 253 Main St, Keansburg, NJ, 07734",
            .[Alias] = "Nicasias, 253 Main St, Keansburg, NJ, 07734",
            .IsDepot = False,
            .Latitude = 40.443224,
            .Longitude = -74.129878,
            .Time = 0
        }, New Address() With {
            .AddressString = "Nohelis Groc, 364 5th Ave, Newark, NJ, 07112",
            .[Alias] = "Nohelis Groc, 364 5th Ave, Newark, NJ, 07112",
            .IsDepot = False,
            .Latitude = 40.760559,
            .Longitude = -74.18531,
            .Time = 0
        }, New Address() With {
            .AddressString = "Nouri Bros, 999 Main St, Paterson, NJ, 07503",
            .[Alias] = "Nouri Bros, 999 Main St, Paterson, NJ, 07503",
            .IsDepot = False,
            .Latitude = 40.894001,
            .Longitude = -74.158519,
            .Time = 0
        }, New Address() With {
            .AddressString = "Noya Bazaar, 139 Wayne Ave, Paterson, NJ, 07505",
            .[Alias] = "Noya Bazaar, 139 Wayne Ave, Paterson, NJ, 07505",
            .IsDepot = False,
            .Latitude = 40.919132,
            .Longitude = -74.186458,
            .Time = 0
        }, New Address() With {
            .AddressString = "Oakhill Deli, 78 South Street, Freehold, NJ, 07728",
            .[Alias] = "Oakhill Deli, 78 South Street, Freehold, NJ, 07728",
            .IsDepot = False,
            .Latitude = 40.2566,
            .Longitude = -74.273895,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ocean Bay Diner, 1803 Route 35 South, Sayerville, NJ, 08872",
            .[Alias] = "Ocean Bay Diner, 1803 Route 35 South, Sayerville, NJ, 08872",
            .IsDepot = False,
            .Latitude = 40.464455,
            .Longitude = -74.267104,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ocean County Correctional Facility, 114 Hooper Ave, Toms River, NJ, 08753",
            .[Alias] = "Ocean County Correctional Facility, 114 Hooper Ave, Toms River, NJ, 08753",
            .IsDepot = False,
            .Latitude = 39.953836,
            .Longitude = -74.194426,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ocean Gate Market, 216 Ocean Gate Market, Ocean Gate, NJ, 08740",
            .[Alias] = "Ocean Gate Market, 216 Ocean Gate Market, Ocean Gate, NJ, 08740",
            .IsDepot = False,
            .Latitude = 39.928493,
            .Longitude = -74.140786,
            .Time = 0
        }, New Address() With {
            .AddressString = "One Stop Deli, 1826 Rt 35 North, Sayerville, NJ, 08872",
            .[Alias] = "One Stop Deli, 1826 Rt 35 North, Sayerville, NJ, 08872",
            .IsDepot = False,
            .Latitude = 40.465032,
            .Longitude = -74.267977,
            .Time = 0
        }, New Address() With {
            .AddressString = "One Stop, 450 Rt 35 N, Neptune, NJ, 07753",
            .[Alias] = "One Stop, 450 Rt 35 N, Neptune, NJ, 07753",
            .IsDepot = False,
            .Latitude = 40.219447,
            .Longitude = -74.032187,
            .Time = 0
        }, New Address() With {
            .AddressString = "Original Quality Market, 416 11th Avenue, East Orange, NJ, 07017",
            .[Alias] = "Original Quality Market, 416 11th Avenue, East Orange, NJ, 07017",
            .IsDepot = False,
            .Latitude = 40.921862,
            .Longitude = -74.150214,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pacific Mini, 2610 Pacific Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Pacific Mini, 2610 Pacific Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.35422,
            .Longitude = -74.442381,
            .Time = 0
        }, New Address() With {
            .AddressString = "Patel Foods, 989 Route 9 North, Parlin, NJ, 08859",
            .[Alias] = "Patel Foods, 989 Route 9 North, Parlin, NJ, 08859",
            .IsDepot = False,
            .Latitude = 40.455067,
            .Longitude = -74.295686,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pathway Market, 42 Pilgrim pathway, Ocean Grove, NJ, 07756",
            .[Alias] = "Pathway Market, 42 Pilgrim pathway, Ocean Grove, NJ, 07756",
            .IsDepot = False,
            .Latitude = 40.212786,
            .Longitude = -74.00703,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pats Mkt, 677 Newman Springs Rd, Lincroft, NJ, 07738",
            .[Alias] = "Pats Mkt, 677 Newman Springs Rd, Lincroft, NJ, 07738",
            .IsDepot = False,
            .Latitude = 40.330632,
            .Longitude = -74.119752,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pena And Morei, 307 Broadway, Heldon, NJ, 07508",
            .[Alias] = "Pena And Morei, 307 Broadway, Heldon, NJ, 07508",
            .IsDepot = False,
            .Latitude = 40.956753,
            .Longitude = -74.188582,
            .Time = 0
        }, New Address() With {
            .AddressString = "Picks Deli, 1500 Main Street, Belmar, NJ, 07719",
            .[Alias] = "Picks Deli, 1500 Main Street, Belmar, NJ, 07719",
            .IsDepot = False,
            .Latitude = 40.175265,
            .Longitude = -74.026946,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pinebrook Liquors & Deli, 1870 Wayside Road, Tinton Falls, NJ, 07724",
            .[Alias] = "Pinebrook Liquors & Deli, 1870 Wayside Road, Tinton Falls, NJ, 07724",
            .IsDepot = False,
            .Latitude = 40.28636,
            .Longitude = -74.095283,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pleasantville Gas, 1101  S Main St, Pleasantville, NJ, 08232",
            .[Alias] = "Pleasantville Gas, 1101  S Main St, Pleasantville, NJ, 08232",
            .IsDepot = False,
            .Latitude = 39.381348,
            .Longitude = -74.532306,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pochi, 1341 Springfield Ave, Maywood, NJ, 07607",
            .[Alias] = "Pochi, 1341 Springfield Ave, Maywood, NJ, 07607",
            .IsDepot = False,
            .Latitude = 40.724029,
            .Longitude = -74.244734,
            .Time = 0
        }, New Address() With {
            .AddressString = "Ponte Vecchio, 3863 Route 516, Old Bridge, NJ, 08857",
            .[Alias] = "Ponte Vecchio, 3863 Route 516, Old Bridge, NJ, 08857",
            .IsDepot = False,
            .Latitude = 40.402437,
            .Longitude = -74.298044,
            .Time = 0
        }, New Address() With {
            .AddressString = "Prime Deli, 1221 Asbury Avenue, Asbury Park, NJ, 07712",
            .[Alias] = "Prime Deli, 1221 Asbury Avenue, Asbury Park, NJ, 07712",
            .IsDepot = False,
            .Latitude = 40.222173,
            .Longitude = -74.01957,
            .Time = 0
        }, New Address() With {
            .AddressString = "Prime Market, 449 Herbertsville Road, Bricktown, NJ, 08724",
            .[Alias] = "Prime Market, 449 Herbertsville Road, Bricktown, NJ, 08724",
            .IsDepot = False,
            .Latitude = 40.102325,
            .Longitude = -74.104611,
            .Time = 0
        }, New Address() With {
            .AddressString = "Prompt Catering LLC, 521 Raritan Street, Sayerville, NJ, 08872",
            .[Alias] = "Prompt Catering LLC, 521 Raritan Street, Sayerville, NJ, 08872",
            .IsDepot = False,
            .Latitude = 40.478299,
            .Longitude = -74.297118,
            .Time = 0
        }, New Address() With {
            .AddressString = "Pse & G, 60 S Newark St, Paterson, NJ, 07514",
            .[Alias] = "Pse & G, 60 S Newark St, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.915172,
            .Longitude = -74.171049,
            .Time = 0
        }, New Address() With {
            .AddressString = "Que Chula Es Puebla, 210 Dayton Avenue, Newark, NJ, 07108",
            .[Alias] = "Que Chula Es Puebla, 210 Dayton Avenue, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.874758,
            .Longitude = -74.122109,
            .Time = 0
        }, New Address() With {
            .AddressString = "Quick Food Mart, 250 Route 9, Barnegat, NJ, 08005",
            .[Alias] = "Quick Food Mart, 250 Route 9, Barnegat, NJ, 08005",
            .IsDepot = False,
            .Latitude = 39.753011,
            .Longitude = -74.222953,
            .Time = 0
        }, New Address() With {
            .AddressString = "Quick Food, 234 Old Stage Rd, East Brunswick, NJ, 08816",
            .[Alias] = "Quick Food, 234 Old Stage Rd, East Brunswick, NJ, 08816",
            .IsDepot = False,
            .Latitude = 40.40657,
            .Longitude = -74.386443,
            .Time = 0
        }, New Address() With {
            .AddressString = "Quick Stop Deli, 814 Amboy Ave, Perth Amboy, NJ, 08861",
            .[Alias] = "Quick Stop Deli, 814 Amboy Ave, Perth Amboy, NJ, 08861",
            .IsDepot = False,
            .Latitude = 40.527608,
            .Longitude = -74.275038,
            .Time = 0
        }, New Address() With {
            .AddressString = "Quick Stop Foods, 120 West Sylvania, Neptune City, NJ, 07753",
            .[Alias] = "Quick Stop Foods, 120 West Sylvania, Neptune City, NJ, 07753",
            .IsDepot = False,
            .Latitude = 40.198761,
            .Longitude = -74.03182,
            .Time = 0
        }, New Address() With {
            .AddressString = "R & G Food Ctr, 144 Vreeland Ave, Newark, NJ, 07107",
            .[Alias] = "R & G Food Ctr, 144 Vreeland Ave, Newark, NJ, 07107",
            .IsDepot = False,
            .Latitude = 40.76638,
            .Longitude = -74.185647,
            .Time = 0
        }, New Address() With {
            .AddressString = "R&R Groc, 286 14th Ave, Irvington, NJ, 07111",
            .[Alias] = "R&R Groc, 286 14th Ave, Irvington, NJ, 07111",
            .IsDepot = False,
            .Latitude = 40.73986,
            .Longitude = -74.204978,
            .Time = 0
        }, New Address() With {
            .AddressString = "Rainbow Deli & L, 292 Lakeview Avenue, Little Falls, NJ, 07424",
            .[Alias] = "Rainbow Deli & L, 292 Lakeview Avenue, Little Falls, NJ, 07424",
            .IsDepot = False,
            .Latitude = 40.885017,
            .Longitude = -74.138497,
            .Time = 0
        }, New Address() With {
            .AddressString = "Randy Grocery, 46 Quincy, Passaic, NJ, 07055",
            .[Alias] = "Randy Grocery, 46 Quincy, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.867337,
            .Longitude = -74.122781,
            .Time = 0
        }, New Address() With {
            .AddressString = "Rd Spmkt, 346 14th Ave, Newark, NJ, 07103",
            .[Alias] = "Rd Spmkt, 346 14th Ave, Newark, NJ, 07103",
            .IsDepot = False,
            .Latitude = 40.73986,
            .Longitude = -74.204978,
            .Time = 0
        }, New Address() With {
            .AddressString = "Rhode Island Market, 101 N Rhone Island Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Rhode Island Market, 101 N Rhone Island Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.368863,
            .Longitude = -74.416528,
            .Time = 0
        }, New Address() With {
            .AddressString = "Rio Via Supermarket, 562 S Park St, Clifton, NJ, 07011",
            .[Alias] = "Rio Via Supermarket, 562 S Park St, Clifton, NJ, 07011",
            .IsDepot = False,
            .Latitude = 40.881156,
            .Longitude = -74.141612,
            .Time = 0
        }, New Address() With {
            .AddressString = "Robert Grocery, 71 Clinton Place, Newark, NJ, 07102",
            .[Alias] = "Robert Grocery, 71 Clinton Place, Newark, NJ, 07102",
            .IsDepot = False,
            .Latitude = 40.720399,
            .Longitude = -74.210768,
            .Time = 0
        }, New Address() With {
            .AddressString = "Rodriguez Grocery, 224 S. Orange Ave, Paterson, NJ, 07513",
            .[Alias] = "Rodriguez Grocery, 224 S. Orange Ave, Paterson, NJ, 07513",
            .IsDepot = False,
            .Latitude = 40.738746,
            .Longitude = -74.192629,
            .Time = 0
        }, New Address() With {
            .AddressString = "S&M Groc, 487 Market St, Irvington, NJ, 07111",
            .[Alias] = "S&M Groc, 487 Market St, Irvington, NJ, 07111",
            .IsDepot = False,
            .Latitude = 40.726324,
            .Longitude = -74.228643,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sams Food Mart, 303 Rt 36 N, Hazlet, NJ, 07730",
            .[Alias] = "Sams Food Mart, 303 Rt 36 N, Hazlet, NJ, 07730",
            .IsDepot = False,
            .Latitude = 40.438034,
            .Longitude = -74.143822,
            .Time = 0
        }, New Address() With {
            .AddressString = "San Martin Grocery, 127 Passaic Ave, Passaic, NJ, 07055",
            .[Alias] = "San Martin Grocery, 127 Passaic Ave, Passaic, NJ, 07055",
            .IsDepot = False,
            .Latitude = 40.859614,
            .Longitude = -74.124275,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sandwich Stop, 104 S Franklin Ave, Pleasantville, NJ, 08232",
            .[Alias] = "Sandwich Stop, 104 S Franklin Ave, Pleasantville, NJ, 08232",
            .IsDepot = False,
            .Latitude = 39.389619,
            .Longitude = -74.52014,
            .Time = 0
        }, New Address() With {
            .AddressString = "Save More, 506 21st Ave, Paterson, NJ, 07513",
            .[Alias] = "Save More, 506 21st Ave, Paterson, NJ, 07513",
            .IsDepot = False,
            .Latitude = 40.905816,
            .Longitude = -74.151835,
            .Time = 0
        }, New Address() With {
            .AddressString = "Scarlett Groc, 75 5th St, W Paterson, NJ, 07424",
            .[Alias] = "Scarlett Groc, 75 5th St, W Paterson, NJ, 07424",
            .IsDepot = False,
            .Latitude = 40.927511,
            .Longitude = -74.176373,
            .Time = 0
        }, New Address() With {
            .AddressString = "Seastreak, 325 Shore Dr, Highland, NJ, 07732",
            .[Alias] = "Seastreak, 325 Shore Dr, Highland, NJ, 07732",
            .IsDepot = False,
            .Latitude = 40.409412,
            .Longitude = -73.996244,
            .Time = 0
        }, New Address() With {
            .AddressString = "Shahin Groc, 1542 Atlantic Ave, Atlantic City, NJ, 08401",
            .[Alias] = "Shahin Groc, 1542 Atlantic Ave, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.361097,
            .Longitude = -74.430216,
            .Time = 0
        }, New Address() With {
            .AddressString = "Shalay Shaleish Café, 130 Livingston ave, New Brunswick, NJ, 08091",
            .[Alias] = "Shalay Shaleish Café, 130 Livingston ave, New Brunswick, NJ, 08091",
            .IsDepot = False,
            .Latitude = 40.488921,
            .Longitude = -74.448212,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sheepshead Bagels, 2095 Rt 35 N, Holmdel, NJ, 07748",
            .[Alias] = "Sheepshead Bagels, 2095 Rt 35 N, Holmdel, NJ, 07748",
            .IsDepot = False,
            .Latitude = 40.414517,
            .Longitude = -74.142318,
            .Time = 0
        }, New Address() With {
            .AddressString = "Shell Gas, 390 Shrewsbury Avenue, Red Bank, NJ, 07701",
            .[Alias] = "Shell Gas, 390 Shrewsbury Avenue, Red Bank, NJ, 07701",
            .IsDepot = False,
            .Latitude = 40.337487,
            .Longitude = -74.075389,
            .Time = 0
        }, New Address() With {
            .AddressString = "Shop Smart, 773 Springfield Ave, Newark, NJ, 07108",
            .[Alias] = "Shop Smart, 773 Springfield Ave, Newark, NJ, 07108",
            .IsDepot = False,
            .Latitude = 40.728866,
            .Longitude = -74.217217,
            .Time = 0
        }, New Address() With {
            .AddressString = "Silverton Pharm, 1824 Hooper Ave, Toms River, NJ, 08753",
            .[Alias] = "Silverton Pharm, 1824 Hooper Ave, Toms River, NJ, 08753",
            .IsDepot = False,
            .Latitude = 40.011365,
            .Longitude = -74.147465,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sipo's Bakery, 365 Smith Street, Perth Amboy, NJ, 08861",
            .[Alias] = "Sipo's Bakery, 365 Smith Street, Perth Amboy, NJ, 08861",
            .IsDepot = False,
            .Latitude = 40.511425,
            .Longitude = -74.278813,
            .Time = 0
        }, New Address() With {
            .AddressString = "Smiths Farm Market, 2810 Allaire Road, Wall Township, NJ, 07719",
            .[Alias] = "Smiths Farm Market, 2810 Allaire Road, Wall Township, NJ, 07719",
            .IsDepot = False,
            .Latitude = 40.152529,
            .Longitude = -74.073501,
            .Time = 0
        }, New Address() With {
            .AddressString = "Snack Station Barnet Hospital, 680 Broadway, Irvington, NJ, 07111",
            .[Alias] = "Snack Station Barnet Hospital, 680 Broadway, Irvington, NJ, 07111",
            .IsDepot = False,
            .Latitude = 40.917592,
            .Longitude = -74.144103,
            .Time = 0
        }, New Address() With {
            .AddressString = "St.Benedicts, 165 Bethany Rd, Holmdel, NJ, 07730",
            .[Alias] = "St.Benedicts, 165 Bethany Rd, Holmdel, NJ, 07730",
            .IsDepot = False,
            .Latitude = 40.40416,
            .Longitude = -74.203567,
            .Time = 0
        }, New Address() With {
            .AddressString = "Stop & Save Mini Market, 420 Central Ave, Paterson, NJ, 07514",
            .[Alias] = "Stop & Save Mini Market, 420 Central Ave, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.757528,
            .Longitude = -74.218494,
            .Time = 0
        }, New Address() With {
            .AddressString = "Subzi Mundi, 2058 Route 130 Suite#10, Monmouth Junction, NJ, 08852",
            .[Alias] = "Subzi Mundi, 2058 Route 130 Suite#10, Monmouth Junction, NJ, 08852",
            .IsDepot = False,
            .Latitude = 40.40805,
            .Longitude = -74.506502,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sunoco A Plus Food Store, 943 Route 9 North, Sayreville, NJ, 08879",
            .[Alias] = "Sunoco A Plus Food Store, 943 Route 9 North, Sayreville, NJ, 08879",
            .IsDepot = False,
            .Latitude = 40.409682,
            .Longitude = -74.348256,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sunoco, 95 Highway 36, Keyport, NJ, 07735",
            .[Alias] = "Sunoco, 95 Highway 36, Keyport, NJ, 07735",
            .IsDepot = False,
            .Latitude = 40.427168,
            .Longitude = -74.197201,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sunrise, 1600 Main Street, Belmar, NJ, 07719",
            .[Alias] = "Sunrise, 1600 Main Street, Belmar, NJ, 07719",
            .IsDepot = False,
            .Latitude = 40.174388,
            .Longitude = -74.026944,
            .Time = 0
        }, New Address() With {
            .AddressString = "Sunshine Deli, 61 White Head Ave, South River, NJ, 08882",
            .[Alias] = "Sunshine Deli, 61 White Head Ave, South River, NJ, 08882",
            .IsDepot = False,
            .Latitude = 40.445177,
            .Longitude = -74.371003,
            .Time = 0
        }, New Address() With {
            .AddressString = "The Broadway Diner, 126 Broadway (North), South Amboy, NJ, 08879",
            .[Alias] = "The Broadway Diner, 126 Broadway (North), South Amboy, NJ, 08879",
            .IsDepot = False,
            .Latitude = 40.484253,
            .Longitude = -74.280944,
            .Time = 0
        }, New Address() With {
            .AddressString = "The Country Kitchen, 2501 Belmar Blvd, Belmar, NJ, 07719",
            .[Alias] = "The Country Kitchen, 2501 Belmar Blvd, Belmar, NJ, 07719",
            .IsDepot = False,
            .Latitude = 40.176369,
            .Longitude = -74.062386,
            .Time = 0
        }, New Address() With {
            .AddressString = "The New Eastside Groc, 462 10th Ave, Paterson, NJ, 07514",
            .[Alias] = "The New Eastside Groc, 462 10th Ave, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.923346,
            .Longitude = -74.145269,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tinton Falls Deli, 1191 Sycamore Avenue, Tinton Falls, NJ, 07724",
            .[Alias] = "Tinton Falls Deli, 1191 Sycamore Avenue, Tinton Falls, NJ, 07724",
            .IsDepot = False,
            .Latitude = 40.305772,
            .Longitude = -74.09978,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tm Family Conv, 51 N Main St, Paterson, NJ, 07514",
            .[Alias] = "Tm Family Conv, 51 N Main St, Paterson, NJ, 07514",
            .IsDepot = False,
            .Latitude = 40.924336,
            .Longitude = -74.17162,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tnc Mini Mart, 80 Eats 1St Ave, Atlantic Highlands, NJ, 07716",
            .[Alias] = "Tnc Mini Mart, 80 Eats 1St Ave, Atlantic Highlands, NJ, 07716",
            .IsDepot = False,
            .Latitude = 40.413674,
            .Longitude = -74.037695,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tony's Freehold Grill, 59 East Main Street, Freehold, NJ, 07728",
            .[Alias] = "Tony's Freehold Grill, 59 East Main Street, Freehold, NJ, 07728",
            .IsDepot = False,
            .Latitude = 40.261613,
            .Longitude = -74.272369,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tony'S Mini Mart, 305 Park Ave, Paterson, NJ, 07524",
            .[Alias] = "Tony'S Mini Mart, 305 Park Ave, Paterson, NJ, 07524",
            .IsDepot = False,
            .Latitude = 40.914833,
            .Longitude = -74.152895,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tony'S Pizza, 759 Main Ave, Paterson, NJ, 07501",
            .[Alias] = "Tony'S Pizza, 759 Main Ave, Paterson, NJ, 07501",
            .IsDepot = False,
            .Latitude = 40.863545,
            .Longitude = -74.128925,
            .Time = 0
        }, New Address() With {
            .AddressString = "Torres Mini Market, 403 Bruck Avenue, Perth Amboy, NJ, 08861",
            .[Alias] = "Torres Mini Market, 403 Bruck Avenue, Perth Amboy, NJ, 08861",
            .IsDepot = False,
            .Latitude = 40.528262,
            .Longitude = -74.271842,
            .Time = 0
        }, New Address() With {
            .AddressString = "Torres Supermarket, 168 Grove Street, Newark, NJ, 07105",
            .[Alias] = "Torres Supermarket, 168 Grove Street, Newark, NJ, 07105",
            .IsDepot = False,
            .Latitude = 40.749295,
            .Longitude = -74.207298,
            .Time = 0
        }, New Address() With {
            .AddressString = "Town & Surf, 77 First Ave, Atlantic Highlands, NJ, 07716",
            .[Alias] = "Town & Surf, 77 First Ave, Atlantic Highlands, NJ, 07716",
            .IsDepot = False,
            .Latitude = 40.413983,
            .Longitude = -74.038003,
            .Time = 0
        }, New Address() With {
            .AddressString = "Town n Country Inn, 35 Broadway @ Rt 35 North, Keyport, NJ, 07735",
            .[Alias] = "Town n Country Inn, 35 Broadway @ Rt 35 North, Keyport, NJ, 07735",
            .IsDepot = False,
            .Latitude = 40.38752,
            .Longitude = -74.097893,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tropico Mini Mart, 40 Broad Street, Keyport, NJ, 07725",
            .[Alias] = "Tropico Mini Mart, 40 Broad Street, Keyport, NJ, 07725",
            .IsDepot = False,
            .Latitude = 40.437838,
            .Longitude = -74.202413,
            .Time = 0
        }, New Address() With {
            .AddressString = "Tulcingo Grocery, 256 Ocean Ave, Lakewood, NJ, 08701",
            .[Alias] = "Tulcingo Grocery, 256 Ocean Ave, Lakewood, NJ, 08701",
            .IsDepot = False,
            .Latitude = 40.090165,
            .Longitude = -74.205323,
            .Time = 0
        }, New Address() With {
            .AddressString = "Twin Pond Farm, 1459 - 1473 Route 9 North, Howell, NJ, 07731",
            .[Alias] = "Twin Pond Farm, 1459 - 1473 Route 9 North, Howell, NJ, 07731",
            .IsDepot = False,
            .Latitude = 40.192329,
            .Longitude = -74.25131,
            .Time = 0
        }, New Address() With {
            .AddressString = "Victoria Mini Mt, 394 E 18th St, Clifton, NJ, 07011",
            .[Alias] = "Victoria Mini Mt, 394 E 18th St, Clifton, NJ, 07011",
            .IsDepot = False,
            .Latitude = 40.881156,
            .Longitude = -74.141612,
            .Time = 0
        }, New Address() With {
            .AddressString = "Viva Mexico, 296 River Avenue, Lakeood, NJ, 08701",
            .[Alias] = "Viva Mexico, 296 River Avenue, Lakeood, NJ, 08701",
            .IsDepot = False,
            .Latitude = 40.079432,
            .Longitude = -74.216707,
            .Time = 0
        }, New Address() With {
            .AddressString = "W Fresh, 4412 Rt 9 South, Freehold, NJ, 07728",
            .[Alias] = "W Fresh, 4412 Rt 9 South, Freehold, NJ, 07728",
            .IsDepot = False,
            .Latitude = 40.286583,
            .Longitude = -74.295474,
            .Time = 0
        }, New Address() With {
            .AddressString = "Waterside, 101 Boardwalk, Atlantic City, NJ, 08401",
            .[Alias] = "Waterside, 101 Boardwalk, Atlantic City, NJ, 08401",
            .IsDepot = False,
            .Latitude = 39.365196,
            .Longitude = -74.410547,
            .Time = 0
        }, New Address() With {
            .AddressString = "Welsh Farms (76 Gas), 659 Rt 36, Belford, NJ, 07718",
            .[Alias] = "Welsh Farms (76 Gas), 659 Rt 36, Belford, NJ, 07718",
            .IsDepot = False,
            .Latitude = 40.41911,
            .Longitude = -74.084993,
            .Time = 0
        }, New Address() With {
            .AddressString = "Welsh Farms, 33 West Main Street, Farmingdale, NJ, 07727",
            .[Alias] = "Welsh Farms, 33 West Main Street, Farmingdale, NJ, 07727",
            .IsDepot = False,
            .Latitude = 40.199729,
            .Longitude = -74.174155,
            .Time = 0
        }, New Address() With {
            .AddressString = "Willow Deli, 290 Willow Drive, Little Silver, NJ, 07739",
            .[Alias] = "Willow Deli, 290 Willow Drive, Little Silver, NJ, 07739",
            .IsDepot = False,
            .Latitude = 40.328604,
            .Longitude = -74.040089,
            .Time = 0
        }, New Address() With {
            .AddressString = "Wilson Ave Deli, 198 Wilson Ave, Port Monmouth, NJ, 07758",
            .[Alias] = "Wilson Ave Deli, 198 Wilson Ave, Port Monmouth, NJ, 07758",
            .IsDepot = False,
            .Latitude = 40.426408,
            .Longitude = -74.103064,
            .Time = 0
        }, New Address() With {
            .AddressString = "Winward Deli, 254 Maple Ave, Red Bank, NJ, 07701",
            .[Alias] = "Winward Deli, 254 Maple Ave, Red Bank, NJ, 07701",
            .IsDepot = False,
            .Latitude = 40.342252,
            .Longitude = -74.067954,
            .Time = 0
        }, New Address() With {
            .AddressString = "Wp Grocery, 497 12th Ave, Paterson, NJ, 07513",
            .[Alias] = "Wp Grocery, 497 12th Ave, Paterson, NJ, 07513",
            .IsDepot = False,
            .Latitude = 40.919401,
            .Longitude = -74.142398,
            .Time = 0
        }, New Address() With {
            .AddressString = "Yellow Rose Diner, 41 Route 36 North, Keyport, NJ, 07735",
            .[Alias] = "Yellow Rose Diner, 41 Route 36 North, Keyport, NJ, 07735",
            .IsDepot = False,
            .Latitude = 40.427893,
            .Longitude = -74.194583,
            .Time = 0
        }, New Address() With {
            .AddressString = "Zoilas, 124 Pasaic St, Paterson, NJ, 07513",
            .[Alias] = "Zoilas, 124 Pasaic St, Paterson, NJ, 07513",
            .IsDepot = False,
            .Latitude = 40.913417,
            .Longitude = -74.170402,
            .Time = 0
        }}
        Dim parameters As RouteParameters = New RouteParameters() With {
            .AlgorithmType = AlgorithmType.CVRP_TW_MD,
            .RouteName = "C# TEST 3",
            .RT = True,
            .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Today.Date),
            .RouteTime = 25200,
            .RouteMaxDuration = 36000,
            .VehicleMaxDistanceMI = 10000,
            .Optimize = GetEnumDescription(Optimize.Distance),
            .DistanceUnit = GetEnumDescription(DistanceUnit.MI),
            .DeviceType = GetEnumDescription(DeviceType.Web),
            .TravelMode = GetEnumDescription(TravelMode.Driving),
            .Parts = 5
        }
        Dim optimizationParameters As OptimizationParameters = New OptimizationParameters() With {
            .Addresses = addresses,
            .Parameters = parameters
        }
        Dim errorString As String
        Dim dataObject As DataObject = route4Me.RunAsyncOptimization(optimizationParameters, errorString)

        If dataObject IsNot Nothing Then
            Console.WriteLine("Optimization finished with the state: " & dataObject.State.ToString())
        Else
            Console.WriteLine("Optimization failed ")
        End If

        Assert.IsNotNull(dataObject, "Optimization failed")
    End Sub

    <ClassCleanup> _
    Public Shared Sub RouteTypesGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(New String() {dataObjectMDMD24.OptimizationProblemId})

        Assert.IsTrue(result, "Removing of the optimization with 24 stops failed...")
    End Sub

End Class

<TestClass()>
Public Class AddressbookContactsGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared c_ApiKey_1 As String = ApiKeys.demoApiKey
    Shared skip As String

    Shared contact1 As AddressBookContact, contact2 As AddressBookContact

    Shared scheduledContact1 As AddressBookContact, scheduledContact1Response As AddressBookContact
    Shared scheduledContact2 As AddressBookContact, scheduledContact2Response As AddressBookContact
    Shared scheduledContact3 As AddressBookContact, scheduledContact3Response As AddressBookContact
    Shared scheduledContact4 As AddressBookContact, scheduledContact4Response As AddressBookContact
    Shared scheduledContact5 As AddressBookContact, scheduledContact5Response As AddressBookContact
    Shared scheduledContact6 As AddressBookContact, scheduledContact6Response As AddressBookContact

    Shared lsRemoveContacts As New List(Of Integer)()

    <ClassInitialize>
    Public Shared Sub AddAddressBookContactsTest(context As TestContext)
        Dim route4Me As New Route4MeManager(c_ApiKey)

        If c_ApiKey = c_ApiKey_1 Then
            skip = "yes"
        Else
            skip = "no"
        End If

        Dim contact As New AddressBookContact() With {
            .first_name = "Test FirstName " + (New Random()).[Next]().ToString(),
            .address_1 = "Test Address1 " + (New Random()).[Next]().ToString(),
            .cached_lat = 38.024654,
            .cached_lng = -77.338814
        }

        ' Run the query
        Dim errorString As String = ""
        contact1 = route4Me.AddAddressBookContact(contact, errorString)

        Assert.IsNotNull(contact1, Convert.ToString("AddAddressBookContactsTest failed... ") & errorString)

        Dim location1 As Integer = If(contact1.address_id IsNot Nothing, Convert.ToInt32(contact1.address_id), -1)
        If location1 > 0 Then
            lsRemoveContacts.Add(location1)
        End If

        Dim dCustom As Dictionary(Of String, Object) = New Dictionary(Of String, Object)() From {
            {"FirstFieldName1", "FirstFieldValue1"},
            {"FirstFieldName2", "FirstFieldValue2"}
        }

        contact = New AddressBookContact() With {
            .first_name = "Test FirstName " + (New Random()).[Next]().ToString(),
            .address_1 = "Test Address1 " + (New Random()).[Next]().ToString(),
            .cached_lat = 38.024654,
            .cached_lng = -77.338814,
            .address_custom_data = dCustom
        }

        contact2 = route4Me.AddAddressBookContact(contact, errorString)

        Assert.IsNotNull(contact2, Convert.ToString("AddAddressBookContactsTest failed... ") & errorString)

        Dim location2 As Integer = If(contact2.address_id IsNot Nothing, Convert.ToInt32(contact2.address_id), -1)
        If location2 > 0 Then
            lsRemoveContacts.Add(location2)
        End If
    End Sub

    <TestMethod>
    Public Sub AddCustomDataToContactTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)

        contact1.address_custom_data = New Dictionary(Of String, Object)() From {
            {"Service type", "publishing"},
            {"Facilities", "storage"},
            {"Parking", "temporarry"}
        }

        Dim errorString As String
        Dim updatedContact = route4Me.UpdateAddressBookContact(contact1, errorString)

        Assert.IsNotNull(updatedContact.address_custom_data, "AddCustomDataToContactTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub AddScheduledAddressBookContactsTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)


        Dim sched1 As Schedule = New Schedule("daily", False) With {
            .enabled = True,
            .mode = "daily",
            .daily = New schedule_daily(1)
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
        Dim errorString As String
        scheduledContact1Response = route4Me.AddAddressBookContact(scheduledContact1, errorString)
        Assert.IsNotNull(scheduledContact1Response, "AddAddressBookContactsTest failed... " & errorString)
        Dim location1 As Integer = If(scheduledContact1Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact1Response.address_id), -1)
        If location1 > 0 Then lsRemoveContacts.Add(location1)


        Dim sched2 As Schedule = New Schedule("weekly", False) With {
            .enabled = True,
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
        Assert.IsNotNull(scheduledContact2Response, "AddAddressBookContactsTest failed... " & errorString)
        Dim location2 As Integer = If(scheduledContact2Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact2Response.address_id), -1)
        If location2 > 0 Then lsRemoveContacts.Add(location2)


        Dim sched3 As Schedule = New Schedule("monthly", False) With {
            .enabled = True,
            .monthly = New schedule_monthly(1, "dates", New Integer() {20, 22, 23, 24, 25})
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
        Assert.IsNotNull(scheduledContact3Response, "AddAddressBookContactsTest failed... " & errorString)
        Dim location3 As Integer = If(scheduledContact3Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact3Response.address_id), -1)
        If location3 > 0 Then lsRemoveContacts.Add(location3)


        Dim sched4 As Schedule = New Schedule("monthly", False) With {
            .enabled = True,
            .monthly = New schedule_monthly(1, "nth", Nothing, _nth:=New Dictionary(Of Integer, Integer)() From {
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
        Assert.IsNotNull(scheduledContact4Response, "AddAddressBookContactsTest failed... " & errorString)
        Dim location4 As Integer = If(scheduledContact4Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact4Response.address_id), -1)
        If location4 > 0 Then lsRemoveContacts.Add(location4)


        Dim sched5 As Schedule = New Schedule("daily", False) With {
            .enabled = True,
            .mode = "daily",
            .daily = New schedule_daily(1)
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
        Assert.IsNotNull(scheduledContact5Response, "AddAddressBookContactsTest failed... " & errorString)
        Dim location5 As Integer = If(scheduledContact5Response.address_id IsNot Nothing, Convert.ToInt32(scheduledContact5Response.address_id), -1)
        If location5 > 0 Then lsRemoveContacts.Add(location5)
    End Sub

    <TestMethod>
    Public Sub UpdateAddressBookContactTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Assert.IsNotNull(contact1, "contact1 is null..")

        contact1.address_group = "Updated"
        ' Run the query
        Dim errorString As String = ""
        Dim updatedContact As AddressBookContact = route4Me.UpdateAddressBookContact(contact1, errorString)

        Assert.IsNotNull(updatedContact, Convert.ToString("UpdateAddressBookContactTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub SearchLocationsByTextTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressBookParameters As New AddressBookParameters() With {
            .Query = "Test Address1",
            .Offset = 0,
            .Limit = 20
        }

        ' Run the query
        Dim total As UInteger = 0
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("SearchLocationsByTextTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub SearchLocationsByIDsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Assert.IsNotNull(contact1, "contact1 is null...")
        Assert.IsNotNull(contact2, "contact2 is null...")

        Dim addresses As String = contact1.address_id & "," & contact2.address_id

        Dim addressBookParameters As New AddressBookParameters() With {
            .AddressId = addresses
        }

        ' Run the query
        Dim total As UInteger = 0
        Dim errorString As String = ""

        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("SearchLocationsByIDsTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetSpecifiedFieldsSearchTextTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressBookParameters As New AddressBookParameters() With {
            .Query = "Test Address1",
            .Fields = "first_name,address_email",
            .Offset = 0,
            .Limit = 20
        }

        ' Run the query
        Dim errorString As String = ""
        Dim response As Route4MeManager.SearchAddressBookLocationResponse = route4Me.SearchAddressBookLocation(addressBookParameters, errorString)

        Assert.IsInstanceOfType(response.Total, GetType(UInt32), Convert.ToString("GetSpecifiedFieldsSearchTextTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetAddressBookContactsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim addressBookParameters As New AddressBookParameters() With {
            .Limit = 10,
            .Offset = 0
        }

        ' Run the query
        Dim total As UInteger
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("GetAddressBookContactsTest failed... ") & errorString)
    End Sub

    '<TestMethod> _
    Public Sub RemoveAllAddressbookContactsTest()
        Dim ApiKey As String = ApiKeys.actualApiKey
        Dim route4Me As New Route4MeManager(ApiKey)
        Dim tdr As New TestDataRepository()

        Dim blContinue As Boolean = True

        Dim iCurOffset As Integer = 0
        Dim lsAddresses As New List(Of String)()

        While blContinue
            Dim addressBookParameters As New AddressBookParameters() With {
                .Limit = 40,
                .Offset = CUInt(iCurOffset)
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

    <TestMethod>
    Public Sub SearchRoutedLocationsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        If skip = "yes" Then Return

        Dim addressBookParameters As New AddressBookParameters() With {
            .Display = "routed",
            .Offset = 0,
            .Limit = 20
        }

        ' Run the query
        Dim total As UInteger = 0
        Dim errorString As String = ""
        Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

        Assert.IsInstanceOfType(contacts, GetType(AddressBookContact()), Convert.ToString("SearchRoutedLocationsTest failed... ") & errorString)
    End Sub

    <ClassCleanup>
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

<TestClass()>
Public Class AddressbookGroupsGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared group1, group2 As AddressBookGroup
    Shared lsGroups As List(Of String) = New List(Of String)()

    <ClassInitialize>
    Public Shared Sub AddressBookGroupsInitialize(ByVal context As TestContext)
        Dim errorString As String

        group1 = CreateAddreessBookGroup(errorString)
        Assert.IsNotNull(group1, "AddressBookGroupsInitialize failed... " & errorString)

        group2 = CreateAddreessBookGroup(errorString)
        Assert.IsNotNull(group2, "AddressBookGroupsInitialize failed... " & errorString)
        lsGroups.Add(group2.groupID)
    End Sub

    <TestMethod>
    Public Sub GetAddressBookGroupsTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim addressBookGroupParameters As AddressBookGroupParameters = New AddressBookGroupParameters() With {
            .Limit = 10,
            .Offset = 0
        }
        Dim errorString As String
        Dim groups As AddressBookGroup() = route4Me.GetAddressBookGroups(addressBookGroupParameters, errorString)
        Assert.IsInstanceOfType(groups, GetType(AddressBookGroup()), "GetAddressBookGroupsTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub GetAddressBookGroupTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim addressBookGroupParameters As AddressBookGroupParameters = New AddressBookGroupParameters() With {
            .GroupId = group2.groupID
        }

        Dim errorString As String
        Dim addressBookGroup As AddressBookGroup = route4Me.GetAddressBookGroup(addressBookGroupParameters, errorString)

        Assert.IsInstanceOfType(addressBookGroup, GetType(AddressBookGroup), "GetAddressBookGroupTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub GetAddressBookContactsByGroupTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim addressBookGroupParameters As AddressBookGroupParameters = New AddressBookGroupParameters() With {
            .GroupId_ = group2.groupID
        }

        Dim errorString As String
        Dim addressBookGroup As AddressBookContactsResponse = route4Me.GetAddressBookContactsByGroup(addressBookGroupParameters, errorString)

        Assert.IsInstanceOfType(addressBookGroup, GetType(AddressBookContactsResponse), "GetAddressBookContactsByGroupTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub SearchAddressBookContactsByFilterTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim filterParam As AddressBookGroupFilterParameter = New AddressBookGroupFilterParameter() With {
            .Query = "Louisville",
            .Display = "all"
        }

        Dim addressBookGroupParameters As AddressBookGroupParameters = New AddressBookGroupParameters() With {
            .Fields = New String() {"address_id", "address_1", "address_group"},
            .Offset = 0,
            .Limit = 10,
            .filter = filterParam
        }

        Dim errorString As String
        Dim results As AddressBookContactsResponse = route4Me.SearchAddressBookContactsByFilter(addressBookGroupParameters, errorString)

        Assert.IsInstanceOfType(results, GetType(AddressBookContactsResponse), "GetAddressBookContactsByGroupTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub UpdateAddressBookGroupTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim addressBookGroupRule As AddressBookGroupRule = New AddressBookGroupRule() With {
            .ID = "address_1",
            .Field = "address_1",
            .[Operator] = "not_equal",
            .Value = "qwerty1234567"
        }

        Dim addressBookGroupFilter As AddressBookGroupFilter = New AddressBookGroupFilter() With {
            .Condition = "AND",
            .Rules = New AddressBookGroupRule() {addressBookGroupRule}
        }

        Dim addressBookGroupParameters As AddressBookGroup = New AddressBookGroup() With {
            .groupID = group2.groupID,
            .groupColor = "cd74e6",
            .Filter = addressBookGroupFilter
        }

        Dim errorString As String
        Dim addressBookGroup As AddressBookGroup = route4Me.UpdateAddressBookGroup(addressBookGroupParameters, errorString)

        Assert.IsNotNull(addressBookGroup, "UpdateAddressBookGroupTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub AddAddressBookGroupTest()
        Dim errorString As String
        Dim addressBookGroup As AddressBookGroup = CreateAddreessBookGroup(errorString)

        Assert.IsNotNull(addressBookGroup, "AddAddreessBookGroupTest failed... " & errorString)

        lsGroups.Add(addressBookGroup.groupID)
    End Sub

    Private Shared Function CreateAddreessBookGroup(ByRef errorString As String) As AddressBookGroup
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim addressBookGroupRule As AddressBookGroupRule = New AddressBookGroupRule() With {
            .ID = "address_1",
            .Field = "address_1",
            .[Operator] = "not_equal",
            .Value = "qwerty123456"
        }

        Dim addressBookGroupFilter As AddressBookGroupFilter = New AddressBookGroupFilter() With {
            .Condition = "AND",
            .Rules = New AddressBookGroupRule() {addressBookGroupRule}
        }

        Dim addressBookGroupParameters As AddressBookGroup = New AddressBookGroup() With {
            .groupName = "All Group",
            .groupColor = "92e1c0",
            .Filter = addressBookGroupFilter
        }

        Dim addressBookGroup As AddressBookGroup = route4Me.AddAddressBookGroup(addressBookGroupParameters, errorString)

        Return addressBookGroup
    End Function

    Private Shared Function DeleteAddreessBookGroup(ByVal remeoveGroupID As String, ByRef errorString As String) As StatusResponse
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim addressGroupParams As AddressBookGroupParameters = New AddressBookGroupParameters() With {
            .GroupId_ = remeoveGroupID ' Payload parameter is GroupId_, url query parameter: GroupId
        }

        errorString = ""
        Dim status As StatusResponse = route4Me.RemoveAddressBookGroup(addressGroupParams, errorString)

        Return status
    End Function

    <TestMethod>
    Public Sub RemoveAddressBookGroupTest()
        Dim errorString As String = ""
        Dim response As StatusResponse = DeleteAddreessBookGroup(group1.groupID, errorString)

        Assert.IsTrue(response.Status, "RemoveAddressBookGroupTest failed... " & errorString)
    End Sub

    <ClassCleanup>
    Public Shared Sub AddressBookGroupsGroupCleanup()
        Dim errorString As String = ""

        For Each curGroupID As String In lsGroups
            Dim resposne As StatusResponse = DeleteAddreessBookGroup(curGroupID, errorString)
            Assert.IsTrue(resposne.Status, "Removing of the address book group with group ID = " & curGroupID & " failed.")
        Next

    End Sub

End Class

<TestClass()> Public Class AvoidanseZonesGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey

    Shared lsAvoidanceZones As New List(Of String)()

    <ClassInitialize> _
    Public Shared Sub AvoidanseZonesGroupInitialize(context As TestContext)
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
                .Data = New String() { _
                    "37.569752822786455,-77.47833251953125", _
                    "37.75886716305343,-77.68974800109863", _
                    "37.74763966054455,-77.6917221069336", _
                    "37.74655084306813,-77.68863220214844", _
                    "37.7502255383101,-77.68125076293945", _
                    "37.74797991274437,-77.67498512268066", _
                    "37.73327960206065,-77.6411678314209", _
                    "37.74430510679532,-77.63172645568848", _
                    "37.76641925847049,-77.66846199035645"} _
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
                .Data = New String() { _
                    "43.51668853502909,-109.3798828125", _
                    "46.98025235521883,-101.865234375"} _
            } _
        }

        Dim rectAvoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(rectAvoidanceZoneParameters, errorString)

        Assert.IsNotNull(rectAvoidanceZone, Convert.ToString("Add Rectangular Avoidance Zone test failed... ") & errorString)

        If lsAvoidanceZones IsNot Nothing Then
            lsAvoidanceZones.Add(rectAvoidanceZone.TerritoryId)
        End If
    End Sub

    <TestMethod> _
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
        If lsAvoidanceZones.Count > 1 Then
            territoryId = lsAvoidanceZones(1)
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
        If lsAvoidanceZones.Count > 1 Then
            territoryId = lsAvoidanceZones(1)
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
    Public Sub RemoveAvoidanceZoneTest()
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
        Dim result As Boolean = route4Me.DeleteAvoidanceZone(avoidanceZoneQuery, errorString)

        Assert.IsTrue(result, Convert.ToString("RemoveAvoidanceZoneTest failed... ") & errorString)

        If result Then lsAvoidanceZones.RemoveAt(0)
    End Sub

    <ClassCleanup> _
    Public Shared Sub AvoidanseZonesGroupCleanup()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        For Each territoryId As String In lsAvoidanceZones
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
    Shared c_ApiKey As String = ApiKeys.actualApiKey

    Shared lsTerritories As New List(Of String)()

    <ClassInitialize()>
    Public Shared Sub TerritoriesGroupInitialize(ByVal context As TestContext)
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim circleTerritoryParameters As AvoidanceZoneParameters = New AvoidanceZoneParameters() With {
            .TerritoryName = "Test Circle Territory",
            .TerritoryColor = "ff0000",
            .Territory = New Territory() With {
                .Type = GetEnumDescription(TerritoryType.Circle),
                .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"}
            }
        }

        Dim errorString As String
        Dim circleTerritory As TerritoryZone = route4Me.CreateTerritory(circleTerritoryParameters, errorString)
        If circleTerritory IsNot Nothing Then lsTerritories.Add(circleTerritory.TerritoryId)

        Assert.IsNotNull(circleTerritory, "Add Circle Territory test failed... " & errorString)

        Dim polyTerritoryParameters As AvoidanceZoneParameters = New AvoidanceZoneParameters() With {
            .TerritoryName = "Test Poly Territory",
            .TerritoryColor = "ff0000",
            .Territory = New Territory With {
                .Type = GetEnumDescription(TerritoryType.Poly),
                .Data = New String() {"37.569752822786455,-77.47833251953125", "37.75886716305343,-77.68974800109863", "37.74763966054455,-77.6917221069336", "37.74655084306813,-77.68863220214844", "37.7502255383101,-77.68125076293945", "37.74797991274437,-77.67498512268066", "37.73327960206065,-77.6411678314209", "37.74430510679532,-77.63172645568848", "37.76641925847049,-77.66846199035645"}
            }
        }

        Dim polyTerritory As TerritoryZone = route4Me.CreateTerritory(polyTerritoryParameters, errorString)

        Assert.IsNotNull(polyTerritory, "Add Polygon Territory test failed... " & errorString)

        If polyTerritory IsNot Nothing Then lsTerritories.Add(polyTerritory.TerritoryId)

        Dim rectTerritoryParameters As AvoidanceZoneParameters = New AvoidanceZoneParameters With {
            .TerritoryName = "Test Rect Territory",
            .TerritoryColor = "ff0000",
            .Territory = New Territory With {
                .Type = GetEnumDescription(TerritoryType.Rect),
                .Data = New String() {"43.51668853502909,-109.3798828125", "46.98025235521883,-101.865234375"}
            }
        }

        Dim rectTerritory As TerritoryZone = route4Me.CreateTerritory(rectTerritoryParameters, errorString)

        Assert.IsNotNull(rectTerritory, "Add Rectangular Avoidance Zone test failed... " & errorString)

        If lsTerritories IsNot Nothing Then lsTerritories.Add(rectTerritory.TerritoryId)
    End Sub

    <TestMethod> _
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

        If lsTerritories.Count > 1 Then
            territoryId = lsTerritories(1)
        End If

        Dim territoryQuery As New TerritoryQuery() With {
            .TerritoryId = territoryId,
            .Addresses = 1,
            .Orders = 1
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
        If lsTerritories.Count > 1 Then
            territoryId = lsTerritories(1)
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
    Public Sub RemoveTerritoriesTest()
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
        Dim result As Boolean = route4Me.RemoveTerritory(territoryQuery, errorString)

        Assert.IsTrue(result, Convert.ToString("RemoveTerritoriesTest failed... ") & errorString)

        If result Then lsTerritories.RemoveAt(0)
    End Sub

    <ClassCleanup> _
    Public Shared Sub TerritoriesGroupCleanup()
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

<TestClass()> Public Class OrdersGroup
    Shared skip As String
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared c_ApiKey_1 As String = ApiKeys.demoApiKey
    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)
    Shared lsOrderiDs As New List(Of String)()
    Shared lsOrders As New List(Of Order)()

    <ClassInitialize()>
    Public Shared Sub CreateOrderTest(ByVal context As TestContext)
        If c_ApiKey = c_ApiKey_1 Then
            skip = "yes"
        Else
            skip = "no"
        End If

        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        lsOptimizationIDs = New List(Of String)()
        context.Properties.Add("Categ", "Ignorable")
        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.SingleDriverRoundTripTest()
        Assert.IsTrue(result, "Single Driver Round Trip generation failed...")
        Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses...")
        lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id)
        Dim dtTomorrow As DateTime = DateTime.Now + (New TimeSpan(1, 0, 0, 0))
        Dim order As Order = New Order() With {
            .address_1 = "Test Address1 " & (New Random()).[Next]().ToString(),
            .address_alias = "Test AddressAlias " & (New Random()).[Next]().ToString(),
            .cached_lat = 37.773972,
            .cached_lng = -122.431297,
            .day_scheduled_for_YYMMDD = dtTomorrow.ToString("yyyy-MM-dd")
        }

        If c_ApiKey <> c_ApiKey_1 Then
            Dim errorString As String
            Dim resultOrder As Order = route4Me.AddOrder(order, errorString)
            Assert.IsNotNull(resultOrder, "CreateOrderTest failed... " & errorString)
            lsOrderiDs.Add(resultOrder.order_id.ToString())
            lsOrders.Add(resultOrder)
        Else
            Assert.AreEqual(c_ApiKey_1, c_ApiKey)
        End If
    End Sub

    <TestMethod> _
    Public Sub GetOrdersTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim orderParameters As New OrderParameters() With { _
            .offset = 0, _
            .limit = 10 _
        }

        Dim total As UInteger
        Dim errorString As String = ""
        Dim orders As Order() = route4Me.GetOrders(orderParameters, total, errorString)

        Assert.IsInstanceOfType(orders, GetType(Order()), Convert.ToString("GetOrdersTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetOrderByIDTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim orderParameters As New OrderParameters() With {
            .order_id = lsOrderiDs(0)
        }

        Dim errorString As String = ""
        Dim orders As Order = route4Me.GetOrderByID(orderParameters, errorString)

        Assert.IsInstanceOfType(orders, GetType(Order), Convert.ToString("GetOrderByIDTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetOrderByInsertedDateTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim InsertedDate As String = DateTime.Now.ToString("yyyy-MM-dd")

        Dim oParams As New OrderParameters() With { _
            .day_added_YYMMDD = InsertedDate _
        }

        Dim errorString As String = ""
        Dim orders As Order() = route4Me.SearchOrders(oParams, errorString)

        Assert.IsInstanceOfType(orders, GetType(Order()), Convert.ToString("GetOrderByInsertedDateTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetOrderByScheduledDateTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim dtTomorrow As DateTime = DateTime.Now + (New TimeSpan(1, 0, 0, 0))

        Dim oParams As New OrderParameters() With {
            .scheduled_for_YYMMDD = dtTomorrow.ToString("yyyy-MM-dd")
        }

        Dim errorString As String = ""
        Dim orders As Order() = route4Me.SearchOrders(oParams, errorString)

        Assert.IsInstanceOfType(orders, GetType(Order()), Convert.ToString("GetOrderByScheduledDateTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetOrdersByScheduleFilterTest()
        If skip = "yes" Then Return

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim startDate As String = (DateTime.Now + (New TimeSpan(1, 0, 0, 0))).ToString("yyyy-MM-dd")
        Dim endDate As String = (DateTime.Now + (New TimeSpan(31, 0, 0, 0))).ToString("yyyy-MM-dd")

        Dim oParams = New OrderFilterParameters() With {
            .Filter = New FilterDetails() With {
                .Display = "all",
                .Limit = 10,
                .Scheduled_for_YYMMDD = New String() {startDate, endDate}
            }
        }

        Dim errorString As String = Nothing
        Dim orders As Order() = route4Me.FilterOrders(oParams, errorString)

        Assert.IsInstanceOfType(orders, GetType(Order()), "GetOrdersByScheduleFilter failed... " & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetOrdersBySpecifiedTextTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim query As String = "Test Address1"

        Dim oParams As New OrderParameters() With { _
            .query = query, _
            .offset = 0, _
            .limit = 20 _
        }

        Dim errorString As String = ""
        Dim orders As Order() = route4Me.SearchOrders(oParams, errorString)

        Assert.IsInstanceOfType(orders, GetType(Order()), Convert.ToString("GetOrdersBySpecifiedTextTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetOrdersByCustomFieldsTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim CustomFields As String = "order_id,member_id"

        Dim oParams As New OrderParameters() With { _
            .fields = CustomFields, _
            .offset = 0, _
            .limit = 20 _
        }

        Dim errorString As String = ""
        Dim orders As Order() = route4Me.SearchOrders(oParams, errorString)

        Assert.IsInstanceOfType(orders, GetType(Order()), Convert.ToString("GetOrdersByCustomFieldsTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub UpdateOrderTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim orderId As String = If(lsOrderiDs.Count > 0, lsOrderiDs(0), "")

        Assert.IsFalse(orderId = "", "There is no order for updating...")

        Dim orderParameters As New OrderParameters() With { _
            .order_id = orderId _
        }

        Dim errorString As String = ""
        Dim order As Order = route4Me.GetOrderByID(orderParameters, errorString)

        Assert.IsTrue(order IsNot Nothing, Convert.ToString("There is no order for updating... ") & errorString)

        order.EXT_FIELD_last_name = "Updated " + (New Random()).[Next]().ToString()

        ' Run the query
        Dim updatedOrder As Order = route4Me.UpdateOrder(order, errorString)

        Assert.IsNotNull(updatedOrder, Convert.ToString("UpdateOrderTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub AddScheduledOrderTest()
        If skip = "yes" Then Return

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim orderParams As Order = New Order() With {
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
            .day_scheduled_for_YYMMDD = "2017-12-20",
            .local_time_window_end = 39000,
            .local_time_window_end_2 = 46200,
            .local_time_window_start = 37800,
            .local_time_window_start_2 = 45000,
            .local_timezone_string = "America/New_York",
            .order_icon = "emoji/emoji-bank"
        }
        Dim errorString As String = ""
        Dim newOrder = route4Me.AddOrder(orderParams, errorString)
        Assert.IsNotNull(newOrder, "AddScheduledOrdersTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub AddOrdersToOptimizationTest()
        If skip = "yes" Then Return

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim rQueryParams As OptimizationParameters = New OptimizationParameters() With {
            .OptimizationProblemID = tdr.SDRT_optimization_problem_id,
            .Redirect = False
        }

        Dim lsTimeWindowStart As List(Of Integer) = New List(Of Integer)()
        Dim dtCurDate As DateTime = DateTime.Now + (New TimeSpan(1, 0, 0, 0))
        dtCurDate = New DateTime(dtCurDate.Year, dtCurDate.Month, dtCurDate.Day, 8, 0, 0)

        Dim tsp1000sec As TimeSpan = New TimeSpan(0, 0, 1000)

        lsTimeWindowStart.Add(CInt(R4MeUtils.ConvertToUnixTimestamp(dtCurDate)))
        dtCurDate += tsp1000sec
        lsTimeWindowStart.Add(CInt(R4MeUtils.ConvertToUnixTimestamp(dtCurDate)))
        dtCurDate += tsp1000sec
        lsTimeWindowStart.Add(CInt(R4MeUtils.ConvertToUnixTimestamp(dtCurDate)))

        Dim addresses As Address() = New Address() {
            New Address With {
                .AddressString = "273 Canal St, New York, NY 10013, USA",
                .Latitude = 40.7191558,
                .Longitude = -74.0011966,
                .[Alias] = "",
                .CurbsideLatitude = 40.7191558,
                .CurbsideLongitude = -74.0011966,
                .IsDepot = True
            },
           New Address With {
                .AddressString = "106 Liberty St, New York, NY 10006, USA",
                .[Alias] = "BK Restaurant #: 2446",
                .Latitude = 40.709637,
                .Longitude = -74.011912,
                .CurbsideLatitude = 40.709637,
                .CurbsideLongitude = -74.011912,
                .Email = "",
                .Phone = "(917) 338-1887",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .TimeWindowStart = lsTimeWindowStart(0),
                .TimeWindowEnd = lsTimeWindowStart(0) + 300,
                .OrderId = 7205705
            },
           New Address With {
                .AddressString = "325 Broadway, New York, NY 10007, USA",
                .[Alias] = "BK Restaurant #: 20333",
                .Latitude = 40.71615,
                .Longitude = -74.00505,
                .CurbsideLatitude = 40.71615,
                .CurbsideLongitude = -74.00505,
                .Email = "",
                .Phone = "(212) 227-7535",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .TimeWindowStart = lsTimeWindowStart(1),
                .TimeWindowEnd = lsTimeWindowStart(1) + 300,
                .OrderId = 7205704
            },
           New Address With {
                .AddressString = "106 Fulton St, Farmingdale, NY 11735, USA",
                .[Alias] = "BK Restaurant #: 17871",
                .Latitude = 40.73073,
                .Longitude = -73.459283,
                .CurbsideLatitude = 40.73073,
                .CurbsideLongitude = -73.459283,
                .Email = "",
                .Phone = "(212) 566-5132",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .TimeWindowStart = lsTimeWindowStart(2),
                .TimeWindowEnd = lsTimeWindowStart(2) + 300,
                .OrderId = 7205703
            }
        }
        Dim rParams As RouteParameters = New RouteParameters() With {
            .RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)",
            .RouteDate = 1465948800,
            .RouteTime = 14400,
            .Optimize = "Time",
            .RouteType = "single",
            .AlgorithmType = AlgorithmType.TSP,
            .RT = False,
            .LockLast = False,
            .VehicleId = "",
            .DisableOptimization = False
        }

        Dim errorString As String = ""
        Dim dataObject As DataObject = route4Me.AddOrdersToOptimization(rQueryParams, addresses, rParams, errorString)

        Assert.IsNotNull(dataObject, "AddOrdersToOptimizationTest failed... " & errorString)

    End Sub

    <TestMethod>
    Public Sub CreateOrderWithCustomFieldTest()
        If skip = "yes" Then Return

        Dim route4Me = New Route4MeManager(c_ApiKey)

        Dim orderParams = New Order() With {
            .address_1 = "1358 E Luzerne St, Philadelphia, PA 19124, US",
            .cached_lat = 48.335991,
            .cached_lng = 31.18287,
            .day_scheduled_for_YYMMDD = "2019-10-11",
            .address_alias = "Auto test address",
            .custom_user_fields = New OrderCustomField() {New OrderCustomField() With {
                .OrderCustomFieldId = 93,
                .OrderCustomFieldValue = "false"
            }}
        }

        Dim errorString As String = Nothing
        Dim result = route4Me.AddOrder(orderParams, errorString)

        Assert.IsNotNull(result, "AddOrdersToRouteTest failed... " & errorString)

        lsOrderiDs.Add(result.order_id.ToString())
        lsOrders.Add(result)
    End Sub

    <TestMethod>
    Public Sub UpdateOrderWithCustomFieldTest()
        If skip = "yes" Then Return

        Dim route4Me = New Route4MeManager(c_ApiKey)
        Dim order = lsOrders(lsOrders.Count - 1)

        order.custom_user_fields = New OrderCustomField() {New OrderCustomField() With {
        .OrderCustomFieldId = 93,
        .OrderCustomFieldValue = "true"
    }}
        Dim errorString As String = Nothing
        Dim result = route4Me.UpdateOrder(order, errorString)
        Assert.IsNotNull(result, "AddOrdersToRouteTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub AddOrdersToRouteTest()
        If skip = "yes" Then Return

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim rQueryParams As RouteParametersQuery = New RouteParametersQuery() With {
            .RouteId = tdr.SDRT_route_id,
            .Redirect = False
        }

        Dim addresses As Address() = New Address() {
            New Address With {
                .AddressString = "273 Canal St, New York, NY 10013, USA",
                .Latitude = 40.7191558,
                .Longitude = -74.0011966,
                .[Alias] = "",
                .CurbsideLatitude = 40.7191558,
                .CurbsideLongitude = -74.0011966
            },
           New Address With {
                .AddressString = "106 Liberty St, New York, NY 10006, USA",
                .[Alias] = "BK Restaurant #: 2446",
                .Latitude = 40.709637,
                .Longitude = -74.011912,
                .CurbsideLatitude = 40.709637,
                .CurbsideLongitude = -74.011912,
                .Email = "",
                .Phone = "(917) 338-1887",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .OrderId = 7205705
            },
           New Address With {
                .AddressString = "106 Fulton St, Farmingdale, NY 11735, USA",
                .[Alias] = "BK Restaurant #: 17871",
                .Latitude = 40.73073,
                .Longitude = -73.459283,
                .CurbsideLatitude = 40.73073,
                .CurbsideLongitude = -73.459283,
                .Email = "",
                .Phone = "(212) 566-5132",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .OrderId = 7205703
            }
        }

        Dim rParams As RouteParameters = New RouteParameters() With {
            .RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)",
            .RouteDate = 1465948800,
            .RouteTime = 14400,
            .Optimize = "Time",
            .RouteType = "single",
            .AlgorithmType = AlgorithmType.TSP,
            .RT = False,
            .LockLast = False,
            .VehicleId = "",
            .DisableOptimization = False
        }

        Dim errorString As String
        Dim result As RouteResponse = route4Me.AddOrdersToRoute(rQueryParams, addresses, rParams, errorString)

        Assert.IsNotNull(result, "AddOrdersToRouteTest failed... " & errorString)
    End Sub

    <ClassCleanup> _
    Public Shared Sub RemoveOrdersTest()
        If skip = "yes" Then Return

        Dim route4Me = New Route4MeManager(c_ApiKey)

        Dim errorString As String
        Dim removed As Boolean = route4Me.RemoveOrders(lsOrderiDs.ToArray(), errorString)

        lsOrders.Clear()
        lsOrderiDs.Clear()

        Assert.IsTrue(removed, "RemoveOrdersTest failed... " & errorString)

        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the testing optimization problem failed...")

        lsOptimizationIDs.Clear()
    End Sub

End Class

<TestClass>
Public Class OrderCustomUserFieldsGroup
    Shared skip As String
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared c_ApiKey_1 As String = ApiKeys.demoApiKey
    Shared lsOrderCustomUserFieldIDs As List(Of Integer) = New List(Of Integer)()

    <ClassInitialize()>
    Public Shared Sub OrderCustomUserFieldsInitialize(ByVal context As TestContext)

        If c_ApiKey = c_ApiKey_1 Then
            skip = "yes"
            Return
        Else
            skip = "no"
        End If

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String = Nothing
        Dim orderCustomUserFields = route4Me.GetOrderCustomUserFields(errorString)
        Dim customFieldId As Integer

        If orderCustomUserFields.Where(Function(x) x.OrderCustomFieldName = "CustomField33").Count() > 0 Then
            customFieldId = orderCustomUserFields.Where(Function(x) x.OrderCustomFieldName = "CustomField33").FirstOrDefault().OrderCustomFieldId
        Else
            Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
                .OrderCustomFieldName = "CustomField33",
                .OrderCustomFieldLabel = "Custom Field 33",
                .OrderCustomFieldType = "checkbox",
                .OrderCustomFieldTypeInfo = New Dictionary(Of String, Object)() From {
                    {"short_label", "cFl33"},
                    {"description", "This is test order custom field"},
                    {"custom field no", 10}
                }
            }

            Dim createdCustomField = route4Me.CreateOrderCustomUserField(orderCustomFieldParams, errorString)

            Assert.IsInstanceOfType(createdCustomField, GetType(OrderCustomFieldCreateResponse), "Cannot initialize the class OrderCustomUserFieldsGroup. " & errorString)

            customFieldId = createdCustomField.Data.OrderCustomFieldId
        End If

        lsOrderCustomUserFieldIDs = New List(Of Integer)() From {
            customFieldId
        }

    End Sub

    <TestMethod>
    Public Sub GetOrderCustomUserFieldsTest()
        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String
        Dim orderCustomUserFields = route4Me.GetOrderCustomUserFields(errorString)
        Assert.IsInstanceOfType(orderCustomUserFields, GetType(OrderCustomField()), "GetOrderCustomUserFieldsTest failed. " & errorString)
    End Sub

    <TestMethod>
    Public Sub CreateOrderCustomUserFieldTest()
        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
            .OrderCustomFieldName = "CustomField44",
            .OrderCustomFieldLabel = "Custom Field 44",
            .OrderCustomFieldType = "checkbox",
            .OrderCustomFieldTypeInfo = New Dictionary(Of String, Object)() From {
                {"short_label", "cFl44"},
                {"description", "This is test order custom field"},
                {"custom field no", 11}
            }
        }
        Dim errorString As String = Nothing
        Dim orderCustomUserField = route4Me.CreateOrderCustomUserField(orderCustomFieldParams, errorString)
        Assert.IsInstanceOfType(orderCustomUserField, GetType(OrderCustomFieldCreateResponse), "CreateOrderCustomUserFieldTest failed. " & errorString)
        lsOrderCustomUserFieldIDs.Add(orderCustomUserField.Data.OrderCustomFieldId)
    End Sub

    <TestMethod>
    Public Sub UpdateOrderCustomUserFieldTest()
        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
            .OrderCustomFieldId = lsOrderCustomUserFieldIDs(lsOrderCustomUserFieldIDs.Count - 1),
            .OrderCustomFieldLabel = "Custom Field 55",
            .OrderCustomFieldType = "checkbox",
            .OrderCustomFieldTypeInfo = New Dictionary(Of String, Object)() From {
                {"short_label", "cFl55"},
                {"description", "This is updated test order custom field"},
                {"custom field no", 12}
            }
        }
        Dim errorString As String = Nothing
        Dim orderCustomUserField = route4Me.UpdateOrderCustomUserField(orderCustomFieldParams, errorString)
        Assert.IsInstanceOfType(orderCustomUserField, GetType(OrderCustomFieldCreateResponse), "UpdateOrderCustomUserFieldTest failed. " & errorString)
        Assert.AreEqual("Custom Field 55", orderCustomUserField.Data.OrderCustomFieldLabel, "UpdateOrderCustomUserFieldTest failed. " & errorString)
    End Sub

    <TestMethod>
    Public Sub RemoveOrderCustomUserFieldTest()
        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim orderCustomFieldId As Integer = lsOrderCustomUserFieldIDs(lsOrderCustomUserFieldIDs.Count - 1)
        Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
            .OrderCustomFieldId = orderCustomFieldId
        }
        Dim errorString As String = Nothing
        Dim response = route4Me.RemoveOrderCustomUserField(orderCustomFieldParams, errorString)
        Assert.IsTrue(response.Affected = 1, "RemoveOrderCustomUserFieldTest failed. " & errorString)
        lsOrderCustomUserFieldIDs.Remove(orderCustomFieldId)
    End Sub

    <ClassCleanup>
    Public Shared Sub RemoveOrderCustomUserFields()
        If skip = "yes" Then Return
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String = Nothing

        For Each customFieldId In lsOrderCustomUserFieldIDs
            Dim customFieldParam = New OrderCustomFieldParameters() With {
                .OrderCustomFieldId = customFieldId
            }
            Dim removeResult = route4Me.RemoveOrderCustomUserField(customFieldParam, errorString)
            Assert.IsTrue(removeResult.Affected = 1, "Cannot remove order customuser field with id=" & customFieldId & ". " & errorString)
        Next

        lsOrderCustomUserFieldIDs.Clear()
    End Sub
End Class

<TestClass()> Public Class ActivitiesGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey

    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)

    <ClassInitialize> _
    Public Shared Sub ActivitiesGroupInitialize(context As TestContext)
        lsOptimizationIDs = New List(Of String)()

        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.RunOptimizationSingleDriverRoute10Stops()

        Assert.IsTrue(result, "Single Driver 10 Stops generation failed...")

        Assert.IsTrue(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses...")

        lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id)
    End Sub

    <TestMethod> _
    Public Sub LogCustomActivityTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim message As String = "Test User Activity " + DateTime.Now.ToString()

        Dim activity As New Activity() With { _
            .ActivityType = "user_message", _
            .ActivityMessage = message, _
            .RouteId = routeId _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim added As Boolean = route4Me.LogCustomActivity(activity, errorString)

        Assert.IsTrue(added, Convert.ToString("LogCustomActivityTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetRouteTimeActivitiesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeId As String = tdr.SD10Stops_route_id
        Assert.IsNotNull(routeId, "routeId_SingleDriverRoute10Stops is null...")

        Dim activityParameters As New ActivityParameters() With { _
            .RouteId = routeId, _
            .team = "true", _
            .Limit = 10, _
            .Offset = 0 _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("GetActivitiesTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetActivitiesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With {
            .Limit = 10,
            .Offset = 0
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("GetActivitiesTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetActivitiesByMemberTest()
        If c_ApiKey = ApiKeys.demoApiKey Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim parameters As New GenericParameters()

        Dim userErrorString As String
        Dim response = route4Me.GetUsers(parameters, userErrorString)

        Assert.IsInstanceOfType(response.results, GetType(MemberResponseV4()), "GetActivitiesByMemberTest failed - cannot get users")
        Assert.IsTrue(response.results.Length > 1, "Cannot retrieve more than 1 users")

        Dim activityParameters As New ActivityParameters() With {
            .MemberId = If(response.results(1).member_id IsNot Nothing, Convert.ToInt32(response.results(1).member_id), -1),
            .Limit = 10,
            .Offset = 0
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivities(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("GetActivitiesTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetLastActivitiesTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activitiesAfterTime As DateTime = DateTime.Now - (New TimeSpan(7, 0, 0, 0))
        activitiesAfterTime = New DateTime(activitiesAfterTime.Year, activitiesAfterTime.Month, activitiesAfterTime.Day, 0, 0, 0)
        Dim uiActivitiesAfterTime As UInteger = CUInt(Route4MeSDKLibrary.Route4MeSDK.R4MeUtils.ConvertToUnixTimestamp(activitiesAfterTime))

        Dim activityParameters As New ActivityParameters() With { _
            .Limit = 10, _
            .Offset = 0, _
            .Start = 0 _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        For Each activity As Activity In activities
            Dim activityTime As UInteger = If(activity.ActivityTimestamp IsNot Nothing, CUInt(activity.ActivityTimestamp), 0)
            Assert.IsTrue(activityTime >= uiActivitiesAfterTime, "GetLastActivities failed. " & errorString)
        Next

    End Sub

    <TestMethod> _
    Public Sub SearchAreaUpdatedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "area-updated" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchAreaUpdatedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchAreaAddedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "area-added" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchAreaAddedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchAreaRemovedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "area-removed" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchAreaRemovedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDestinationDeletedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "delete-destination", _
            .RouteId = "5C15E83A4BE005BCD1537955D28D51D7" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDestinationDeletedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDestinationInsertedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "insert-destination", _
            .RouteId = "87B8873BAEA4E09942C68E2C92A9C4B7" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDestinationInsertedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDestinationMarkedAsDepartedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "mark-destination-departed", _
            .RouteId = "03CEF546324F727239ABA69EFF3766E1" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDestinationMarkedAsDepartedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDestinationOutSequenceTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "destination-out-sequence" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDestinationOutSequenceTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDestinationUpdatedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "update-destinations" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDestinationUpdatedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDriverArrivedEarlyTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "driver-arrived-early" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDriverArrivedEarlyTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDriverArrivedLateTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "driver-arrived-late" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDriverArrivedLateTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchDriverArrivedOnTimeTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "driver-arrived-on-time" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchDriverArrivedOnTimeTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchGeofenceEnteredTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "geofence-entered" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchGeofenceEnteredTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchGeofenceLeftTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "geofence-left" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchGeofenceLeftTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchInsertDestinationAllTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "insert-destination" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchInsertDestinationAllTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchMarkDestinationDepartedAllTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "mark-destination-departed" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchMarkDestinationDepartedAllTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchMarkDestinationVisitedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "mark-destination-visited" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchMarkDestinationVisitedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchMemberCreatedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "member-created" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchMemberCreatedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchMemberDeletedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "member-deleted" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchMemberDeletedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchMemberModifiedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "member-modified" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchMemberModifiedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchMoveDestinationTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "move-destination" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchMoveDestinationTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchNoteInsertedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "note-insert", _
            .RouteId = "C3E7FD2F8775526674AE5FD83E25B88A" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchNoteInsertedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchNoteInsertedAllTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "note-insert" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchNoteInsertedAllTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchRouteDeletedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "route-delete" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchRouteDeletedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchRouteOptimizedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "route-optimized" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchRouteOptimizedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub SearchRouteOwnerChanged()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim activityParameters As New ActivityParameters() With { _
            .ActivityType = "route-owner-changed", _
            .RouteId = "5C15E83A4BE005BCD1537955D28D51D7" _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

        Assert.IsInstanceOfType(activities, GetType(Activity()), Convert.ToString("SearchRouteOwnerChanged failed... ") & errorString)
    End Sub

    <ClassCleanup> _
    Public Shared Sub ActivitiesGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the testing optimization problem failed...")
    End Sub

End Class

<TestClass()> Public Class AddressesGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)
    Shared removdAddressId As Int32

    <ClassInitialize> _
    Public Shared Sub AddressGroupInitialize(context As TestContext)
        lsOptimizationIDs = New List(Of String)()

        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.SingleDriverRoundTripTest()

        Assert.IsTrue(result, "Single Driver Round Trip generation failed...")

        Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses...")

        lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id)

        removdAddressId = -1
    End Sub

    <TestMethod> _
    Public Sub GetAddressTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim routeIdToMoveTo As String = tdr.SDRT_route_id
        Assert.IsNotNull(routeIdToMoveTo, "routeId_SingleDriverRoundTrip is null...")

        Dim addressId As Integer = If((tdr.dataObjectSDRT IsNot Nothing AndAlso tdr.dataObjectSDRT.Routes IsNot Nothing AndAlso tdr.dataObjectSDRT.Routes.Length > 0 AndAlso tdr.dataObjectSDRT.Routes(0).Addresses.Length > 1 AndAlso tdr.dataObjectSDRT.Routes(0).Addresses(1).RouteDestinationId IsNot Nothing), tdr.dataObjectSDRT.Routes(0).Addresses(1).RouteDestinationId.Value, 0)

        Dim addressParameters As New AddressParameters() With { _
            .RouteId = routeIdToMoveTo, _
            .RouteDestinationId = addressId, _
            .Notes = True _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObject As Address = route4Me.GetAddress(addressParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("GetAddressTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub AddDestinationToOptimizationTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        ' Prepare the address that we are going to add to an existing route optimization
        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "717 5th Ave New York, NY 10021", _
            .[Alias] = "Giorgio Armani", _
            .Latitude = 40.7669692, _
            .Longitude = -73.9693864, _
            .Time = 0 _
        }}

        'Optionally change any route parameters, such as maximum route duration, maximum cubic constraints, etc.
        Dim optimizationParameters As New OptimizationParameters() With { _
            .OptimizationProblemID = tdr.SDRT_optimization_problem_id, _
            .Addresses = addresses, _
            .ReOptimize = True _
        }

        ' Execute the optimization to re-optimize and rebalance all the routes in this optimization
        Dim errorString As String = ""
        Dim dataObject As DataObject = route4Me.UpdateOptimization(optimizationParameters, errorString)

        tdr.SDRT_route_id = If(dataObject.Routes.Length > 0, dataObject.Routes(0).RouteID, "")

        Assert.IsNotNull(tdr.dataObjectSDRT, Convert.ToString("AddDestinationToOptimization and reoptimized Test  failed... ") & errorString)

        optimizationParameters.ReOptimize = False
        dataObject = route4Me.UpdateOptimization(optimizationParameters, errorString)

        tdr.SDRT_route_id = If(dataObject.Routes.Length > 0, dataObject.Routes(0).RouteID, "")

        Assert.IsNotNull(tdr.dataObjectSDRT, Convert.ToString("AddDestinationToOptimization and not reoptimized Test  failed... ") & errorString)

    End Sub

    <TestMethod> _
    Public Sub RemoveDestinationFromOptimizationTest()
        Dim destinationToRemove As Address = If((tdr.SDRT_route IsNot Nothing AndAlso tdr.SDRT_route.Addresses.Length > 0), tdr.SDRT_route.Addresses(tdr.SDRT_route.Addresses.Length - 1), Nothing)

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim OptimizationProblemId As String = tdr.SDRT_optimization_problem_id
        Assert.IsNotNull(OptimizationProblemId, "OptimizationProblemId is null...")

        Dim delta As Integer = If(removdAddressId = destinationToRemove.RouteDestinationId, 2, 1)

        Dim destinationId As Integer = If(destinationToRemove.RouteDestinationId IsNot Nothing, Convert.ToInt32(destinationToRemove.RouteDestinationId), -delta)
        Assert.AreNotEqual(-1, "destinationId is null...")
        ' Run the query
        Dim errorString As String = ""
        Dim removed As Boolean = route4Me.RemoveAddressFromOptimization(OptimizationProblemId, destinationId, errorString)

        Assert.IsTrue(removed, Convert.ToString("RemoveDestinationFromOptimizationTest failed... ") & errorString)

        removdAddressId = destinationId
    End Sub

    <TestMethod> _
    Public Sub AddRouteDestinationsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim route_id As String = tdr.SDRT_route_id

        Assert.IsNotNull(route_id, "rote_id is null...")

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.143526, _
            .Longitude = -83.240354, _
            .Time = 0 _
        }, New Address() With { _
            .AddressString = "222 Blake Cir Milledgeville GA 31061", _
            .Latitude = 33.177852, _
            .Longitude = -83.263535, _
            .Time = 0 _
        }}

        ' Run the query
        Dim optimalPosition As Boolean = True
        Dim errorString As String = ""
        Dim destinationIds As Integer() = route4Me.AddRouteDestinations(route_id, addresses, errorString)

        Assert.IsInstanceOfType(destinationIds, GetType(System.Int32()), "AddRouteDestinationsTest failed...")

    End Sub

    <TestMethod> _
    Public Sub AddRouteDestinationInSpecificPositionTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim route_id As String = tdr.SDRT_route_id

        Assert.IsNotNull(route_id, "rote_id is null...")

        ' Prepare the addresses

        Dim addresses As Address() = New Address() {New Address() With { _
            .AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061", _
            .Latitude = 33.143526, _
            .Longitude = -83.240354, _
            .SequenceNo = 3, _
            .Time = 0 _
        }}

        ' Run the query
        Dim optimalPosition As Boolean = False
        Dim errorString As String = ""
        Dim destinationIds As Integer() = route4Me.AddRouteDestinations(route_id, addresses, errorString)

        Assert.IsInstanceOfType(destinationIds, GetType(System.Int32()), "AddRouteDestinationsTest failed...")

    End Sub

    <TestMethod> _
    Public Sub RemoveRouteDestinationTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim route_id As String = tdr.SDRT_route_id

        Assert.IsNotNull(route_id, "rote_id is null...")

        Dim delta As Integer = If(removdAddressId = tdr.SDRT_route.Addresses(tdr.SDRT_route.Addresses.Length - 1).RouteDestinationId, 2, 1)

        Dim oDestinationId As Object = tdr.SDRT_route.Addresses(tdr.SDRT_route.Addresses.Length - delta).RouteDestinationId

        Dim destination_id As Integer = If(oDestinationId IsNot Nothing, Convert.ToInt32(oDestinationId), -1)
        Assert.IsNotNull(oDestinationId, "destination_id is null...")

        ' Run the query
        Dim errorString As String = ""
        Dim deleted As Boolean = route4Me.RemoveRouteDestination(tdr.SDRT_route.RouteID, destination_id, errorString)

        Assert.IsTrue(deleted, "RemoveRouteDestinationTest")

        removdAddressId = destination_id
    End Sub

    <TestMethod> _
    Public Sub MarkAddressAsMarkedAsDepartedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim aParams As New AddressParameters() With { _
            .RouteId = tdr.SDRT_route_id, _
            .RouteDestinationId = If(tdr.SDRT_route.Addresses(0).RouteDestinationId IsNot Nothing, Convert.ToInt32(tdr.SDRT_route.Addresses(0).RouteDestinationId), -1), _
            .IsDeparted = True _
        }

        ' Run the query

        Dim errorString As String = ""
        Dim resultAddress As Address = route4Me.MarkAddressAsMarkedAsDeparted(aParams, errorString)

        Assert.IsNotNull(resultAddress, Convert.ToString("MarkAddressAsMarkedAsDepartedTest... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub MarkAddressAsMarkedAsVisitedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim aParams As New AddressParameters() With { _
            .RouteId = tdr.SDRT_route_id, _
            .RouteDestinationId = If(tdr.SDRT_route.Addresses(0).RouteDestinationId IsNot Nothing, Convert.ToInt32(tdr.SDRT_route.Addresses(0).RouteDestinationId), -1), _
            .IsVisited = True _
        }

        ' Run the query

        Dim errorString As String = ""
        Dim resultAddress As Address = route4Me.MarkAddressAsMarkedAsVisited(aParams, errorString)

        Assert.IsNotNull(resultAddress, Convert.ToString("MarkAddressAsMarkedAsVisitedTest... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub MarkAddressDepartedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim aParams As New AddressParameters() With { _
            .RouteId = tdr.SDRT_route_id, _
            .AddressId = If(tdr.SDRT_route.Addresses(0).RouteDestinationId IsNot Nothing, Convert.ToInt32(tdr.SDRT_route.Addresses(0).RouteDestinationId), -1), _
            .IsDeparted = True _
        }

        ' Run the query

        Dim errorString As String = ""
        Dim result As Integer = route4Me.MarkAddressVisited(aParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("MarkAddressDepartedTest... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub MarkAddressVisitedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim aParams As New AddressParameters() With { _
            .RouteId = tdr.SDRT_route_id, _
            .AddressId = If(tdr.SDRT_route.Addresses(0).RouteDestinationId IsNot Nothing, Convert.ToInt32(tdr.SDRT_route.Addresses(0).RouteDestinationId), -1), _
            .IsVisited = True _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim oResult As Object = route4Me.MarkAddressVisited(aParams, errorString)

        Assert.IsNotNull(oResult, Convert.ToString("MarkAddressVisitedTest... ") & errorString)
    End Sub

    <ClassCleanup> _
    Public Shared Sub AddressGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the testing optimization problem failed...")
    End Sub

End Class

<TestClass()> Public Class TrackingGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey

    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)

    <ClassInitialize> _
    Public Shared Sub TrackingGroupInitialize(context As TestContext)
        lsOptimizationIDs = New List(Of String)()

        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.SingleDriverRoundTripTest()

        Assert.IsTrue(result, "Single Driver Round Trip generation failed...")

        Assert.IsTrue(tdr.SDRT_route.Addresses.Length > 0, "The route has no addresses...")

        lsOptimizationIDs.Add(tdr.SDRT_optimization_problem_id)
    End Sub

    <TestMethod> _
    Public Sub FindAssetTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim tracking As String = If(tdr.SDRT_route IsNot Nothing, (If(tdr.SDRT_route.Addresses.Length > 1, (If(tdr.SDRT_route.Addresses(1).tracking_number IsNot Nothing, tdr.SDRT_route.Addresses(1).tracking_number, "")), "")), "")

        Assert.IsTrue(tracking <> "", "Can not find valid tracking number in the newly generated route's second destination...")

        ' Run the query
        Dim errorString As String = ""
        Dim result As FindAssetResponse = route4Me.FindAsset(tracking, errorString)

        Assert.IsInstanceOfType(result, GetType(FindAssetResponse), Convert.ToString("FindAssetTest failed... ") & errorString)

    End Sub

    <TestMethod> _
    Public Sub SetGPSPositionTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim lat As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Latitude, 33.14384)
        Dim lng As Double = If(tdr.SDRT_route.Addresses.Length > 1, tdr.SDRT_route.Addresses(1).Longitude, -83.22466)
        ' Create the gps parametes
        Dim gpsParameters As New GPSParameters() With { _
            .Format = Format.Csv.GetEnumDescription(), _
            .RouteId = tdr.SDRT_route_id, _
            .Latitude = lat, _
            .Longitude = lng, _
            .Course = 1, _
            .Speed = 120, _
            .DeviceType = DeviceType.IPhone.GetEnumDescription(), _
            .MemberId = 1, _
            .DeviceGuid = "TEST_GPS", _
            .DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") _
        }

        Dim errorString As String = ""
        Dim response As String = route4Me.SetGPS(gpsParameters, errorString)

        Assert.IsNotNull(response, Convert.ToString("SetGPSPositionTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetDeviceHistoryTimeRangeTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim uStartTime As Integer = 0
        Dim uEndTime As Integer = 0
        uStartTime = CInt((New DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0) - (New DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds)
        uEndTime = CInt((DateTime.Now - (New DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds)

        Dim gpsParameters As New GPSParameters() With { _
            .Format = "csv", _
            .RouteId = tdr.SDRT_route_id, _
            .time_period = "custom", _
            .start_date = uStartTime, _
            .end_date = uEndTime _
        }

        Dim errorString As String = ""
        Dim response As String = route4Me.SetGPS(gpsParameters, errorString)

        Assert.IsNotNull(response, Convert.ToString("GetDeviceHistoryTimeRangeTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub TrackDeviceLastLocationHistoryTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim genericParameters As New GenericParameters()
        genericParameters.ParametersCollection.Add("route_id", tdr.SDRT_route_id)
        genericParameters.ParametersCollection.Add("device_tracking_history", "1")

        Dim errorString As String = ""
        Dim dataObject = route4Me.GetLastLocation(genericParameters, errorString)

        Assert.IsNotNull(dataObject, Convert.ToString("TrackDeviceLastLocationHistoryTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub GetAllUserLocationsTest()
        Dim route4Me = New Route4MeManager(ApiKeys.actualApiKey)

        Dim genericParameters = New GenericParameters()

        Dim errorString As String = Nothing
        Dim userLocations = route4Me.GetUserLocations(genericParameters, errorString)

        Assert.IsNotNull(userLocations, "GetAllUserLocationsTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub QueryUserLocationsTest()
        Dim route4Me = New Route4MeManager(ApiKeys.actualApiKey)
        Dim genericParameters = New GenericParameters()
        Dim errorString As String = Nothing
        Dim userLocations = route4Me.GetUserLocations(genericParameters, errorString)

        Assert.IsNotNull(userLocations, "GetAllUserLocationsTest failed... " & errorString)

        Dim userLocation = userLocations.Where(Function(x) x.UserTracking IsNot Nothing).FirstOrDefault()
        Dim email As String = userLocation.MemberData.MemberEmail

        genericParameters.ParametersCollection.Add("query", email)

        Dim queriedUserLocations = route4Me.GetUserLocations(genericParameters, errorString)

        Assert.IsNotNull(queriedUserLocations, "QueryUserLocationsTest failed... " & errorString)
        Assert.IsTrue(queriedUserLocations.Count() = 1, "QueryUserLocationsTest failed... " & errorString)
    End Sub

    <ClassCleanup> _
    Public Shared Sub TrackingGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the testing optimization problem failed...")
    End Sub

End Class

<TestClass()> Public Class UsersGroup
    Shared skip As String
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared c_ApiKey_1 As String = ApiKeys.demoApiKey
    Shared lsMembers As List(Of Integer)

    <ClassInitialize()>
    Public Shared Sub UserGroupInitialize(ByVal context As TestContext)
        If c_ApiKey = c_ApiKey_1 Then
            skip = "yes"
            Return
        Else
            skip = "no"
        End If

        lsMembers = New List(Of Integer)()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim parameters As GenericParameters = New GenericParameters()

        Dim errorString As String

        Dim dispetcher As MemberResponseV4 = (New UsersGroup()).CreateUser("SUB_ACCOUNT_DISPATCHER", errorString)
        Assert.IsInstanceOfType(dispetcher, GetType(MemberResponseV4), "Cannot create dispetcher")
        lsMembers.Add(dispetcher.member_id)

        Dim driver As MemberResponseV4 = (New UsersGroup()).CreateUser("SUB_ACCOUNT_DRIVER", errorString)
        Assert.IsInstanceOfType(driver, GetType(MemberResponseV4), "Cannot create driver")
        lsMembers.Add(driver.member_id)
    End Sub

    <TestMethod>
    Public Sub CreateUserTest()
        If skip = "yes" Then Return

        Dim errorString As String
        Dim dispetcher As MemberResponseV4 = CreateUser("SUB_ACCOUNT_DISPATCHER", errorString)

        Dim rightResponse As String = If(dispetcher IsNot Nothing, "ok", (If((errorString = "Email is used in system" OrElse errorString = "Registration: The e-mail address is missing or invalid."), "ok", "")))

        Assert.IsTrue(rightResponse = "ok", "CreateUserTest failed... " & errorString)

        lsMembers.Add(Convert.ToInt32(dispetcher.member_id))
    End Sub

    Public Function CreateUser(ByRef memberType As String, ByVal errorString As String)
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim userFirstName As String
        Dim userLastName As String
        Dim userPhone As String

        Select Case memberType
            Case "SUB_ACCOUNT_DISPATCHER"
                userFirstName = "Clay"
                userLastName = "Abraham"
                userPhone = "571-259-5939"
            Case "SUB_ACCOUNT_DRIVER"
                userFirstName = "Driver"
                userLastName = "Driverson"
                userPhone = "577-222-5555"
        End Select

        Dim params As MemberParametersV4 = New MemberParametersV4 With {
            .HIDE_ROUTED_ADDRESSES = "FALSE",
            .member_phone = userPhone,
            .member_zipcode = "22102",
            .member_email = "regression.autotests+" & DateTime.Now.ToString("yyyyMMddHHmmss") & "@gmail.com",
            .HIDE_VISITED_ADDRESSES = "FALSE",
            .READONLY_USER = "FALSE",
            .member_type = memberType,
            .date_of_birth = "2010",
            .member_first_name = userFirstName,
            .member_password = "123456",
            .HIDE_NONFUTURE_ROUTES = "FALSE",
            .member_last_name = userLastName,
            .SHOW_ALL_VEHICLES = "FALSE",
            .SHOW_ALL_DRIVERS = "FALSE"
        }

        Dim result = route4Me.CreateUser(params, errorString)

        CreateUser = result

    End Function

    <TestMethod>
    Public Sub AddEditCustomDataToUserTest()
        If skip = "yes" Then Return

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim memberId = lsMembers(lsMembers.Count - 1)

        Dim customPair As Dictionary(Of String, Object) = New Dictionary(Of String, Object)() From {
            {"Custom Key 2", "Custom Value 2"}
        }

        Dim customParams As MemberParametersV4 = New MemberParametersV4 With {
            .member_id = memberId,
            .custom_data = customPair
        }

        Dim errorString As String = ""
        Dim result2 As MemberResponseV4 = route4Me.UserUpdate(customParams, errorString)

        Assert.IsTrue(result2 IsNot Nothing, "UpdateUserTest failed... " & errorString)

        Dim customData As Dictionary(Of String, String) = result2.custom_data

        Assert.IsTrue(customData.Keys.ElementAt(0) = "Custom Key 2", "Custom Key is not 'Custom Key 2'")
        Assert.IsTrue(customData("Custom Key 2") = "Custom Value 2", "Custom Value is not 'Custom Value 2'")
    End Sub

    <TestMethod>
    Public Sub GetUserByIdTest()
        If skip = "yes" Then Return

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim memberID As Integer = lsMembers(0)
        Dim params As MemberParametersV4 = New MemberParametersV4 With {
            .member_id = memberID
        }

        Dim errorString As String = ""
        Dim result As MemberResponseV4 = route4Me.GetUserById(params, errorString)

        Assert.IsNotNull(result, "GetUserByIdTest... " & errorString)
    End Sub

    <TestMethod>
    Public Sub GetUsersTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim parameters As GenericParameters = New GenericParameters()

        Dim errorString As String
        Dim dataObjects As Route4MeManager.GetUsersResponse = route4Me.GetUsers(parameters, errorString)

        Assert.IsInstanceOfType(dataObjects, GetType(Route4MeManager.GetUsersResponse), "GetUsersTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub UpdateUserTest()
        If skip = "yes" Then Return

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim params As MemberParametersV4 = New MemberParametersV4 With {
            .member_id = lsMembers(lsMembers.Count - 1),
            .member_phone = "571-259-5939"
        }

        Dim errorString As String = ""
        Dim result As MemberResponseV4 = route4Me.UserUpdate(params, errorString)

        Assert.IsNotNull(result, "UpdateUserTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub UserAuthenticationTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim params As New MemberParameters() With {
            .StrEmail = "aaaaaaaa@gmail.com",
            .StrPassword = "11111111111",
            .Format = "json"
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As MemberResponse = route4Me.UserAuthentication(params, errorString)

        ' result is always non null object, but in case of successful autentication object properties have non nul values
        Assert.IsNotNull(result, Convert.ToString("UserAuthenticationTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub UserRegistrationTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim params As New MemberParameters() With {
            .StrEmail = "thewelco@gmail.com",
            .StrPassword_1 = "11111111",
            .StrPassword_2 = "11111111",
            .StrFirstName = "Olman",
            .StrLastName = "Progman",
            .StrIndustry = "Transportation",
            .Format = "json",
            .ChkTerms = 1,
            .DeviceType = "web",
            .Plan = "free",
            .MemberType = 5
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As MemberResponse = route4Me.UserRegistration(params, errorString)

        ' result is always non null object, but in case of successful autentication object property Status=true
        Assert.IsNotNull(result, Convert.ToString("UserRegistrationTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub ValidateSessionTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim params As New MemberParameters() With {
            .SessionGuid = "ad9001f33ed6875b5f0e75bce52cbc34",
            .MemberId = 1,
            .Format = "json"
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As MemberResponse = route4Me.ValidateSession(params, errorString)

        ' result is always non null object, but in case of successful autentication object properties have non nul values
        Assert.IsNotNull(result, Convert.ToString("ValidateSessionTest failed... ") & errorString)
    End Sub

    <TestMethod>
    Public Sub DeleteUserTest()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim params As New MemberParametersV4() With {
            .member_id = lsMembers(lsMembers.Count - 1)
        }

        ' Run the query
        Dim errorString As String = ""
        Dim result As Boolean = route4Me.UserDelete(params, errorString)

        Assert.IsNotNull(result, Convert.ToString("DeleteUserTest failed... ") & errorString)

        lsMembers.Remove(params.member_id)
    End Sub

    <ClassCleanup>
    Public Shared Sub UsersGroupCleanup()
        If skip = "yes" Then Return

        Dim route4Me As New Route4MeManager(c_ApiKey)
        Dim params As New MemberParametersV4()
        Dim errorString As String = ""
        Dim result As Boolean

        For Each memberId In lsMembers
            params.member_id = memberId
            result = route4Me.UserDelete(params, errorString)
        Next
    End Sub

End Class

<TestClass()> Public Class MemberConfigurationGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared lsConfigurationKeys As List(Of String)

    <ClassInitialize> _
    Public Shared Sub MemberConfigurationGroupInitialize(context As TestContext)
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        lsConfigurationKeys = New List(Of String)()

        Dim params As MemberConfigurationParameters = New MemberConfigurationParameters With {
            .config_key = "Test My height",
            .config_value = "180"
        }

        Dim errorString As String = Nothing
        Dim result = route4Me.CreateNewConfigurationKey(params, errorString)

        Assert.IsNotNull(result, "AddNewConfigurationKeyTest failed... " & errorString)

        lsConfigurationKeys.Add("Test My height")

        Dim keyrParams As MemberConfigurationParameters = New MemberConfigurationParameters With {
            .config_key = "Test Remove Key",
            .config_value = "remove"
        }

        Dim result2 = route4Me.CreateNewConfigurationKey(keyrParams, errorString)

        Assert.IsNotNull(result2, "AddNewConfigurationKeyTest failed... " & errorString)

        lsConfigurationKeys.Add("Test Remove Key")
    End Sub

    <TestMethod>
    Public Sub AddNewConfigurationKeyTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim params As MemberConfigurationParameters = New MemberConfigurationParameters With {
            .config_key = "Test My weight",
            .config_value = "100"
        }

        Dim errorString As String = Nothing
        Dim result = route4Me.CreateNewConfigurationKey(params, errorString)

        Assert.IsNotNull(result, "AddNewConfigurationKeyTest failed... " & errorString)

        lsConfigurationKeys.Add("Test My weight")
    End Sub

    <TestMethod>
    Public Sub AddConfigurationKeyArrayTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim parametersArray As MemberConfigurationParameters() = New MemberConfigurationParameters() _
        {
            New MemberConfigurationParameters With {
            .config_key = "Test My Height",
            .config_value = "185"
        },
        New MemberConfigurationParameters With {
            .config_key = "Test My Weight",
            .config_value = "110"
        }}

        Dim errorString As String = Nothing
        Dim result = route4Me.CreateNewConfigurationKey(parametersArray, errorString)

        Assert.IsNotNull(result, "AddNewConfigurationKeyTest failed... " & errorString)

        lsConfigurationKeys.Add("Test My Height")
        lsConfigurationKeys.Add("Test My Weight")
    End Sub

    <TestMethod> _
    Public Sub GetAllConfigurationDataTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim params As New MemberConfigurationParameters()

        ' Run the query
        Dim errorString As String = ""
        Dim result As MemberConfigurationDataRersponse = route4Me.GetConfigurationData(params, errorString)

        Assert.IsNotNull(result, Convert.ToString("GetAllConfigurationDataTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetSpecificConfigurationKeyDataTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim params = New MemberConfigurationParameters With {
            .config_key = "Test My height"
        }

        Dim errorString As String = Nothing
        Dim result = route4Me.GetConfigurationData(params, errorString)

        Assert.IsNotNull(result, "GetSpecificConfigurationKeyDataTest failed... " & errorString)
    End Sub

    <TestMethod> _
    Public Sub UpdateConfigurationKeyTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim params As MemberConfigurationParameters = New MemberConfigurationParameters With {
            .config_key = "Test My height",
            .config_value = "190"
        }

        Dim errorString As String = Nothing
        Dim result = route4Me.UpdateConfigurationKey(params, errorString)

        Assert.IsNotNull(result, "UpdateConfigurationKeyTest failed... " & errorString)
    End Sub

    <TestMethod> _
    Public Sub RemoveConfigurationKeyTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim params = New MemberConfigurationParameters With {
            .config_key = "Test Remove Key"
        }

        Dim errorString As String = Nothing
        Dim result = route4Me.RemoveConfigurationKey(params, errorString)

        Assert.IsNotNull(result, "RemoveConfigurationKeyTest failed... " & errorString)

        lsConfigurationKeys.Remove("Test Remove Key")
    End Sub

    <ClassCleanup> _
    Public Shared Sub MemberConfigurationGroupCleanup()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim errorString As String = Nothing

        For Each testKey In lsConfigurationKeys
            Dim params = New MemberConfigurationParameters With {
                .config_key = testKey
            }

            Dim result = route4Me.RemoveConfigurationKey(params, errorString)

            Assert.IsNotNull(result, "MemberConfigurationGroupCleanup failed...")
        Next
    End Sub

End Class

<TestClass()> Public Class VehiclesGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared lsVehicleIDs As List(Of String)

    <ClassInitialize()>
    Public Shared Sub VehiclesGroupInitialize(ByVal context As TestContext)
        lsVehicleIDs = New List(Of String)()

        Dim vehicleGroup As VehiclesGroup = New VehiclesGroup()

        Dim vehicles As VehiclesPaginated = vehicleGroup.getVehiclesList()

        If vehicles.Total < 1 Then
            Dim newVehicle As VehicleV4Parameters = New VehicleV4Parameters() With {
                .VehicleAlias = "Ford Transit Test 6"
            }
            Dim vehicle As VehicleV4Response = vehicleGroup.createVehicle(newVehicle)
            lsVehicleIDs.Add(vehicle.VehicleId)
        Else

            For Each veh1 As VehicleV4Response In vehicles.Data
                lsVehicleIDs.Add(veh1.VehicleId)
            Next
        End If
    End Sub

    <TestMethod>
    Public Sub GetVehiclesListTest()
        Dim vehicles As VehiclesPaginated = getVehiclesList()
    End Sub

    Public Function getVehiclesList() As VehiclesPaginated
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim vehicleParameters As VehicleParameters = New VehicleParameters With {
            .WithPagination = True,
            .Page = 1,
            .PerPage = 10
        }
        Dim errorString As String = ""
        Dim vehicles As VehiclesPaginated = route4Me.GetVehicles(vehicleParameters, errorString)
        Assert.IsInstanceOfType(vehicles, GetType(VehiclesPaginated), "getVehilesList failed... " & errorString)
        Return vehicles
    End Function

    Public Function createVehicle(ByVal vehicleParams As VehicleV4Parameters) As VehicleV4Response
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim errorString As String = ""
        Dim result As VehicleV4Response = route4Me.CreateVehicle(vehicleParams, errorString)

        Assert.IsNotNull(result, "CreatetVehiclTest failed... " & errorString)

        Return result
    End Function

    <TestMethod>
    Public Sub CreatetVehicleTest()
        If c_ApiKey = ApiKeys.demoApiKey Then Return

        Dim commonVehicleParams As VehicleV4Parameters = New VehicleV4Parameters() With {
            .VehicleName = "Ford Transit Test 6",
            .VehicleAlias = "Ford Transit Test 6"
        }

        Dim commonVehicle As VehicleV4Response = createVehicle(commonVehicleParams)

        Dim class6TruckParams As VehicleV4Parameters = New VehicleV4Parameters() With {
            .VehicleName = "GMC TopKick C5500",
            .VehicleAlias = "GMC TopKick C5500",
            .VehicleVin = "SAJXA01A06FN08012",
            .VehicleLicensePlate = "CVH4561",
            .VehicleModel = "TopKick C5500",
            .VehicleModelYear = 1995,
            .VehicleYearAcquired = 2008,
            .VehicleRegCountryId = 223,
            .VehicleMake = "GMC",
            .VehicleTypeID = "pickup_truck",
            .VehicleAxleCount = 2,
            .MpgCity = 7,
            .MpgHighway = 14,
            .FuelType = "diesel",
            .HeightInches = 97,
            .HeightMetric = 243,
            .WeightLb = 19000,
            .MaxWeightPerAxleGroupInPounds = 9500,
            .MaxWeightPerAxleGroupMetric = 4300,
            .WidthInInches = 96,
            .WidthMetric = 240,
            .LengthInInches = 244,
            .LengthMetric = 610,
            .Use53FootTrailerRouting = "NO",
            .UseTruckRestrictions = "NO",
            .DividedHighwayAvoidPreference = "NEUTRAL",
            .FreewayAvoidPreference = "NEUTRAL",
            .TruckConfig = "FULLSIZEVAN"
        }

        Dim class6Truck As VehicleV4Response = createVehicle(class6TruckParams)

        Dim class7TruckParams As VehicleV4Parameters = New VehicleV4Parameters() With {
            .VehicleName = "FORD F750",
            .VehicleAlias = "FORD F750",
            .VehicleVin = "1NPAX6EX2YD550743",
            .VehicleLicensePlate = "FFV9547",
            .VehicleModel = "F-750",
            .VehicleModelYear = 2010,
            .VehicleYearAcquired = 2018,
            .VehicleRegCountryId = 223,
            .VehicleMake = "Ford",
            .VehicleTypeID = "livestock_carrier",
            .VehicleAxleCount = 2,
            .MpgCity = 8,
            .MpgHighway = 15,
            .FuelType = "diesel",
            .HeightInches = 96,
            .HeightMetric = 244,
            .WeightLb = 26000,
            .MaxWeightPerAxleGroupInPounds = 15000,
            .MaxWeightPerAxleGroupMetric = 6800,
            .WidthInInches = 96,
            .WidthMetric = 240,
            .LengthInInches = 312,
            .LengthMetric = 793,
            .Use53FootTrailerRouting = "NO",
            .UseTruckRestrictions = "YES",
            .DividedHighwayAvoidPreference = "FAVOR",
            .FreewayAvoidPreference = "NEUTRAL",
            .TruckConfig = "26_STRAIGHT_TRUCK",
            .TollRoadUsage = "ALWAYS_AVOID",
            .InternationalBordersOpen = "NO",
            .PurchasedNew = True
        }

        Dim class7Truck As VehicleV4Response = createVehicle(class7TruckParams)

        Dim class8TruckParams As VehicleV4Parameters = New VehicleV4Parameters() With {
            .VehicleName = "Peterbilt 579",
            .VehicleAlias = "Peterbilt 579",
            .VehicleVin = "1NP5DB9X93N507873",
            .VehicleLicensePlate = "PPV7516",
            .VehicleModel = "579",
            .VehicleModelYear = 2015,
            .VehicleYearAcquired = 2018,
            .VehicleRegCountryId = 223,
            .VehicleMake = "Peterbilt",
            .VehicleTypeID = "tractor_trailer",
            .VehicleAxleCount = 4,
            .MpgCity = 6,
            .MpgHighway = 12,
            .FuelType = "diesel",
            .HeightInches = 114,
            .HeightMetric = 290,
            .WeightLb = 50350,
            .MaxWeightPerAxleGroupInPounds = 40000,
            .MaxWeightPerAxleGroupMetric = 18000,
            .WidthInInches = 102,
            .WidthMetric = 258,
            .LengthInInches = 640,
            .LengthMetric = 1625,
            .Use53FootTrailerRouting = "YES",
            .UseTruckRestrictions = "YES",
            .DividedHighwayAvoidPreference = "STRONG_FAVOR",
            .FreewayAvoidPreference = "STRONG_AVOID",
            .TruckConfig = "53_SEMI_TRAILER",
            .TollRoadUsage = "ALWAYS_AVOID",
            .InternationalBordersOpen = "YES",
            .PurchasedNew = True
        }

        Dim class8Truck As VehicleV4Response = createVehicle(class8TruckParams)

    End Sub

    <TestMethod>
    Public Sub getVehicleTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)

        Dim vehicleParameters As VehicleParameters = New VehicleParameters With {
            .VehicleId = lsVehicleIDs(lsVehicleIDs.Count - 1)
        }

        Dim errorString As String = ""
        Dim vehicles As VehicleV4Response = route4Me.GetVehicle(vehicleParameters, errorString)

        Assert.IsInstanceOfType(vehicles, GetType(VehicleV4Response), "getVehicleTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub updateVehicleTest()
        If c_ApiKey = ApiKeys.demoApiKey Then Return

        If lsVehicleIDs.Count < 1 Then
            Dim newVehicle As VehicleV4Parameters = New VehicleV4Parameters() With {
                .VehicleAlias = "Ford Transit Test 6"
            }
            Dim vehicle As VehicleV4Response = createVehicle(newVehicle)
            lsVehicleIDs.Add(vehicle.VehicleId)
        End If

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim vehicleParams As VehicleV4Parameters = New VehicleV4Parameters() With {
            .VehicleAlias = "Ford Transit Test 4",
            .VehicleModelYear = 1995,
            .VehicleRegCountryId = 223,
            .VehicleMake = "Ford",
            .VehicleAxleCount = 2,
            .FuelType = "unleaded 93",
            .HeightInches = 72,
            .WeightLb = 2000
        }

        Dim errorString As String = ""
        Dim vehicles As VehicleV4Response = route4Me.updateVehicle(vehicleParams, lsVehicleIDs(lsVehicleIDs.Count - 1), errorString)

        Assert.IsInstanceOfType(vehicles, GetType(VehicleV4Response), "updateVehicleTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub deleteVehicleTest()
        If lsVehicleIDs.Count < 1 Then
            Dim newVehicle As VehicleV4Parameters = New VehicleV4Parameters() With {
                .VehicleAlias = "Ford Transit Test 6"
            }
            Dim vehicle As VehicleV4Response = createVehicle(newVehicle)
            lsVehicleIDs.Add(vehicle.VehicleId)
        End If

        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim vehicleParams As VehicleV4Parameters = New VehicleV4Parameters() With {
            .VehicleId = lsVehicleIDs(lsVehicleIDs.Count - 1)
        }

        Dim errorString As String = ""
        Dim vehicles As VehicleV4Response = route4Me.deleteVehicle(vehicleParams, errorString)

        Assert.IsInstanceOfType(vehicles, GetType(VehicleV4Response), "updateVehicleTest failed... " & errorString)
        lsVehicleIDs.RemoveAt(lsVehicleIDs.Count - 1)
    End Sub

End Class


<TestClass()> Public Class GeocodingGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey

    <TestMethod> _
    Public Sub GeocodingForwardTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Addresses = "Los Angeles International Airport, CA||3495 Purdue St, Cuyahoga Falls, OH 44221", _
            .Format = "json" _
        }

        'Run the query
        Dim errorString As String = ""
        Dim result As String = route4Me.Geocoding(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("GeocodingForwardTest failed. ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub BatchGeocodingForwardTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim geoParams As GeocodingParameters = New GeocodingParameters With {
            .Addresses = "Los Angeles International Airport, CA" & vbLf & "3495 Purdue St, Cuyahoga Falls, OH 44221",
            .Format = "json"
        }
        Dim errorString As String = ""
        Dim result As String = route4Me.BatchGeocoding(geoParams, errorString)
        Assert.IsNotNull(result, "GeocodingForwardTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub BatchGeocodingForwardAsyncTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim geoParams As GeocodingParameters = New GeocodingParameters With {
            .Addresses = "Los Angeles International Airport, CA" & vbLf & "3495 Purdue St, Cuyahoga Falls, OH 44221",
            .Format = "json"
        }
        Dim errorString As String = ""
        Dim result As String = route4Me.BatchGeocodingAsync(geoParams, errorString)
        Assert.IsNotNull(result, "GeocodingForwardTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub uploadAndGeocodeLargeJsonFile()
        Dim fastbGeocoding As FastBulkGeocoding = New FastBulkGeocoding(c_ApiKey, False)
        Dim lsGeocodedAddressTotal As List(Of AddressGeocoded) = New List(Of AddressGeocoded)()
        Dim lsAddresses As List(Of String) = New List(Of String)()
        Dim addressesInFile As Integer = 13
        AddHandler fastbGeocoding.GeocodingIsFinished, Function(sender As Object, e As FastBulkGeocoding.GeocodingIsFinishedArgs)
                                                           Assert.IsNotNull(lsAddresses, "Geocoding process failed")
                                                           Assert.AreEqual(addressesInFile, lsAddresses.Count, "Not all the addresses were geocoded")
                                                           Console.WriteLine("Large addresses file geocoding is finished")

                                                       End Function

        AddHandler fastbGeocoding.AddressesChunkGeocoded, Function(sender As Object, e As FastBulkGeocoding.AddressesChunkGeocodedArgs)

                                                              If e.lsAddressesChunkGeocoded IsNot Nothing Then

                                                                  For Each addr1 In e.lsAddressesChunkGeocoded
                                                                      lsAddresses.Add(addr1.geocodedAddress.AddressString)
                                                                  Next
                                                              End If

                                                              Console.WriteLine("Total Geocoded Addresses -> " & lsAddresses.Count)
                                                          End Function

        fastbGeocoding.uploadAndGeocodeLargeJsonFile("Data\JSON\batch_socket_upload_error_addresses_data_5.json")
    End Sub

    <TestMethod> _
    Public Sub RapidStreetDataAllTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters()
        ' Run the query
        Dim errorString As String = ""
        Dim result As ArrayList = route4Me.RapidStreetData(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("RapidStreetDataAllTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub RapidStreetDataLimitedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Offset = 10, _
            .Limit = 10 _
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As ArrayList = route4Me.RapidStreetData(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("RapidStreetDataLimitedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub RapidStreetDataSingleTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Pk = 4 _
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As ArrayList = route4Me.RapidStreetData(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("RapidStreetDataSingleTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub RapidStreetServiceAllTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Zipcode = "00601", _
            .Housenumber = "17" _
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As ArrayList = route4Me.RapidStreetService(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("RapidStreetServiceAllTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub RapidStreetServiceLimitedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Zipcode = "00601", _
            .Housenumber = "17", _
            .Offset = 1, _
            .Limit = 10 _
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As ArrayList = route4Me.RapidStreetService(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("RapidStreetServiceLimitedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub RapidStreetZipcodeAllTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Zipcode = "00601" _
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As ArrayList = route4Me.RapidStreetZipcode(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("RapidStreetZipcodeAllTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub RapidStreetZipcodeLimitedTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Zipcode = "00601", _
            .Offset = 1, _
            .Limit = 10 _
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As ArrayList = route4Me.RapidStreetZipcode(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("RapidStreetZipcodeLimitedTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub ReverseGeocodingTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim geoParams As New GeocodingParameters() With { _
            .Addresses = "42.35863,-71.05670" _
        }
        ' Run the query
        Dim errorString As String = ""
        Dim result As String = route4Me.Geocoding(geoParams, errorString)

        Assert.IsNotNull(result, Convert.ToString("ReverseGeocodingTest failed... ") & errorString)
    End Sub

End Class

<TestClass()> Public Class DatabasesGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared db_type As DB_Type

    Shared tdr As TestDataRepository

    Private Shared _testContext As TestContext
    Public Property TestContext() As TestContext
        Get
            Return _testContext
        End Get

        Set(value As TestContext)
            _testContext = value
        End Set
    End Property

    <ClassInitialize> _
    Public Shared Sub DatabasesGroupInitialize(testContext As TestContext)
        _testContext = testContext

        db_type = DB_Type.SQLCE
        ' you can choose other types of the database engine.
        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.GenerateSQLCEDatabaseTest()

        Assert.IsTrue(result, "Generation of the SQL tables failed...")
    End Sub

    ' Uncomment line below if you have indicated valid MySQL connection parameters in the App.config file.
    '<TestMethod> _
    Public Sub GenerateMySQLDatabaseTest()
        Dim sqlDB As New cDatabase(DB_Type.MySQL)

        Try
            Dim sAddressbookSqlCom As String = ""
            Dim sOrdersSqlCom As String = ""
            Dim sDictionaryDDLSqlCom As String = ""
            Dim sDictionaryDMLSqlCom As String = ""

            sAddressbookSqlCom = File.ReadAllText("Data/SQL/MySQL/addressbook_v4.sql")
            sOrdersSqlCom = File.ReadAllText("Data/SQL/MySQL/orders.sql")
            sDictionaryDDLSqlCom = File.ReadAllText("Data/SQL/MySQL/csv_to_api_dictionary_DDL.sql")
            sDictionaryDMLSqlCom = File.ReadAllText("Data/SQL/MySQL/csv_to_api_dictionary_DML.sql")

            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            Dim iResult As Integer = sqlDB.ExecuteMulticoomandSql(sAddressbookSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'addressbook_v4' created successfuly!!!")
            Else
                Console.WriteLine(":( Creating of the SQL table 'addressbook_v4' failed...")
            End If

            iResult = sqlDB.ExecuteMulticoomandSql(sOrdersSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'orders' created successfuly!!!")
            Else
                Console.WriteLine(":( Creating of the SQL table 'orders' failed...")
            End If

            iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDDLSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'csv_to_api_dictionary' created successfuly!!!")

                iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDMLSqlCom)
                If iResult > 0 Then
                    Console.WriteLine(":) The data was inserted into SQL table 'csv_to_api_dictionary' successfuly!!!")
                Else
                    Console.WriteLine(":( Inserting of the data in the SQL table 'csv_to_api_dictionary' failed...")
                End If
            Else
                Console.WriteLine(":( Creating of the SQL table 'csv_to_api_dictionary' failed...")
            End If

            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Generating of the SQL tables failed!.. " + ex.Message)
            Assert.IsTrue(0 > 1, "GenerateMySQLDatabaseTest failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    ' Uncomment line below if you have indicated valid MsSQL connection parameters in the App.config file.
    '<TestMethod> _
    Public Sub GenerateMsSQLDatabaseTest()
        Dim sqlDB As New cDatabase(DB_Type.MSSQL)

        Try
            Dim sAddressbookSqlCom As String = ""
            Dim sOrdersSqlCom As String = ""
            Dim sDictionaryDDLSqlCom As String = ""
            Dim sDictionaryDMLSqlCom As String = ""

            sAddressbookSqlCom = File.ReadAllText("Data/SQL/MSSQL/addressbook_v4.sql")
            sOrdersSqlCom = File.ReadAllText("Data/SQL/MSSQL/orders.sql")
            sDictionaryDDLSqlCom = File.ReadAllText("Data/SQL/MSSQL/csv_to_api_dictionary_DDL.sql")
            sDictionaryDMLSqlCom = File.ReadAllText("Data/SQL/MSSQL/csv_to_api_dictionary_DML.sql")

            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            Dim iResult As Integer = sqlDB.ExecuteMulticoomandSql(sAddressbookSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'addressbook_v4' created successfuly!!!")
            Else
                Console.WriteLine(":( Creating of the SQL table 'addressbook_v4' failed...")
            End If

            iResult = sqlDB.ExecuteMulticoomandSql(sOrdersSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'orders' created successfuly!!!")
            Else
                Console.WriteLine(":( Creating of the SQL table 'orders' failed...")
            End If

            iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDDLSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'csv_to_api_dictionary' created successfuly!!!")

                iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDMLSqlCom)
                If iResult > 0 Then
                    Console.WriteLine(":) The data was inserted into SQL table 'csv_to_api_dictionary' successfuly!!!")
                Else
                    Console.WriteLine(":( Inserting of the data in the SQL table 'csv_to_api_dictionary' failed...")
                End If
            Else
                Console.WriteLine(":( Creating of the SQL table 'csv_to_api_dictionary' failed...")
            End If

            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Generating of the SQL tables failed!.. " + ex.Message)
            Assert.IsTrue(0 > 1, "GenerateMsSQLDatabaseTest failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    <TestMethod> _
    Public Sub GenerateSQLCEDatabaseTest()
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

            tdr.dropSQLCEtable("addressbook_v4", sqlDB)

            Dim iResult As Integer = sqlDB.ExecuteMulticoomandSql(sAddressbookSqlCom)
            Assert.IsTrue(iResult > 0, "Creating of the SQL table 'addressbook_v4' failed...")

            tdr.dropSQLCEtable("orders", sqlDB)

            iResult = sqlDB.ExecuteMulticoomandSql(sOrdersSqlCom)
            Assert.IsTrue(iResult > 0, "Creating of the SQL table 'orders' failed...")

            tdr.dropSQLCEtable("csv_to_api_dictionary", sqlDB)

            iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDDLSqlCom)
            Assert.IsTrue(iResult > 0, "Creating of the SQL table 'csv_to_api_dictionary' failed...")

            If iResult > 0 Then
                iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDMLSqlCom)
                Assert.IsTrue(iResult > 0, "Inserting of the data in the SQL table 'csv_to_api_dictionary' failed...")
            End If
        Catch ex As Exception
            Console.WriteLine("Generating of the SQL tables failed!.. " + ex.Message)
            Assert.IsTrue(0 > 1, "GenerateSQLCEDatabaseTest failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    ' Uncomment line below if you have indicated valid PostgreSQL connection parameters in the App.config file.
    '<TestMethod> _
    Public Sub GeneratePostgreSQLDatabaseTest()
        Dim sqlDB As New cDatabase(DB_Type.PostgreSQL)

        Try
            Dim sAddressbookSqlCom As String = ""
            Dim sOrdersSqlCom As String = ""
            Dim sDictionaryDDLSqlCom As String = ""
            Dim sDictionaryDMLSqlCom As String = ""

            sAddressbookSqlCom = File.ReadAllText("Data/SQL/PostgreSQL/addressbook_v4.sql")
            sOrdersSqlCom = File.ReadAllText("Data/SQL/PostgreSQL/orders.sql")
            sDictionaryDDLSqlCom = File.ReadAllText("Data/SQL/PostgreSQL/csv_to_api_dictionary_DDL.sql")
            sDictionaryDMLSqlCom = File.ReadAllText("Data/SQL/PostgreSQL/csv_to_api_dictionary_DML.sql")

            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            Dim iResult As Integer = sqlDB.ExecuteMulticoomandSql(sAddressbookSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'addressbook_v4' created successfuly!!!")
            Else
                Console.WriteLine(":( Creating of the SQL table 'addressbook_v4' failed...")
            End If

            iResult = sqlDB.ExecuteMulticoomandSql(sOrdersSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'orders' created successfuly!!!")
            Else
                Console.WriteLine(":( Creating of the SQL table 'orders' failed...")
            End If

            iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDDLSqlCom)
            If iResult > 0 Then
                Console.WriteLine(":) The SQL table 'csv_to_api_dictionary' created successfuly!!!")

                iResult = sqlDB.ExecuteMulticoomandSql(sDictionaryDMLSqlCom)
                If iResult > 0 Then
                    Console.WriteLine(":) The data was inserted into SQL table 'csv_to_api_dictionary' successfuly!!!")
                Else
                    Console.WriteLine(":( Inserting of the data in the SQL table 'csv_to_api_dictionary' failed...")
                End If
            Else
                Console.WriteLine(":( Creating of the SQL table 'csv_to_api_dictionary' failed...")
            End If

            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Generating of the SQL tables failed!.. " + ex.Message)
            Assert.IsTrue(0 > 1, "GeneratePostgreSQLDatabaseTest failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    <TestMethod> _
    Public Sub MakeAddressbookCSVsampleTest()
        Dim sqlDB As New cDatabase(db_type)

        Try
            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            sqlDB.Table2Csv("Data/CSV/addressbook v4.csv", "addressbook_v4", True)
            Console.WriteLine("The file addressbook v4.csv was created.")
            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Making of a addressbook csv file failed!.. " + ex.Message)
            Assert.IsTrue(0 > 1, "MakeAddressbookCSVsampleTest failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    <TestMethod> _
    Public Sub UploadAddressbookJSONtoSQLTest()
        Dim sqlDB As New cDatabase(db_type)

        Try
            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            sqlDB.Json2Table("Data/JSON/Addressbook Get Contacts RESPONSE.json", "addressbook_v4", "id", R4M_DataType.Addressbook)

            Console.WriteLine("The file 'Addressbook Get Contacts RESPONSE.json' was uploaded to the SQL server.")

            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Uploading of the JSON file to the SQL server failed!.. " & ex.Message)
            Assert.IsTrue(0 > 1, "UploadAddressbookJSONtoSQLTest failed... " & ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    <TestMethod> _
    Public Sub UploadCsvToAddressbookV4Test()
        Dim sqlDB As New cDatabase(db_type)

        Try
            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            sqlDB.Csv2Table("Data/CSV/Route4Me Address Book 03-09-2017.csv", "addressbook_v4", "id", 33, True)

            Console.WriteLine("The file orders.csv was uploaded to the SQL server.")

            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Uploading of the CSV file to the SQL server failed!.. " & ex.Message)
            Assert.IsTrue(0 > 1, "UploadCsvToAddressbookV4Test failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    <TestMethod> _
    Public Sub UploadCsvToOrdersTest()
        Dim sqlDB As New cDatabase(db_type)

        Try
            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            sqlDB.Csv2Table("Data/CSV/orders 1000 with order id.csv", "orders", "order_id", 10, True)

            Console.WriteLine("The orders CSV file was uploaded to the SQL server.")

            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Uploading of the CSV file to the SQL server failed!.. " + ex.Message)
            Assert.IsTrue(0 > 1, "UploadCsvToOrdersTest failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

    <TestMethod> _
    Public Sub UploadOrdersJSONtoSQLTest()
        Dim sqlDB As New cDatabase(db_type)

        Try
            sqlDB.OpenConnection()

            Console.WriteLine("Connection opened")

            sqlDB.Json2Table("Data/JSON/get orders RESPONSE.json", "orders", "id", R4M_DataType.Order)

            Console.WriteLine("The JSON file was uploaded to the SQL server.")

            Assert.IsTrue(1 > 0, "")
        Catch ex As Exception
            Console.WriteLine("Uploading of the JSON file to the SQL server failed!.. " & ex.Message)
            Assert.IsTrue(0 > 1, "UploadOrdersJSONtoSQLTest failed... " + ex.Message)
        Finally
            sqlDB.CloseConnection()
        End Try
    End Sub

End Class

<TestClass()> Public Class OptimizationsGroup
    Shared c_ApiKey As String = ApiKeys.actualApiKey
    Shared tdr As TestDataRepository
    Shared lsOptimizationIDs As List(Of String)
    Shared lsAddressbookContacts As List(Of String)
    Shared lsOrders As List(Of String)

    <ClassInitialize> _
    Public Shared Sub OptimizationsGroupInitialize(context As TestContext)
        lsOptimizationIDs = New List(Of String)()
        lsAddressbookContacts = New List(Of String)()
        lsOrders = New List(Of String)()

        tdr = New TestDataRepository()
        Dim result As Boolean = tdr.RunOptimizationSingleDriverRoute10Stops()

        Assert.IsTrue(result, "Single Driver 10 stops generation failed...")

        Assert.IsTrue(tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses...")

        lsOptimizationIDs.Add(tdr.dataObjectSD10Stops.OptimizationProblemId)
    End Sub

    <TestMethod>
    Public Sub GetOptimizationsTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim queryParameters As New RouteParametersQuery() With {
            .Limit = 10,
            .Offset = 5
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObjects As DataObject() = route4Me.GetOptimizations(queryParameters, errorString)

        Assert.IsInstanceOfType(dataObjects, GetType(DataObject()), "GetOptimizationsTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub GetOptimizationsFromDateRangeTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim queryParameters As New RouteParametersQuery() With {
            .StartDate = "2019-09-15",
            .EndtDate = "2019-09-20"
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObjects As DataObject() = route4Me.GetOptimizations(queryParameters, errorString)

        Assert.IsInstanceOfType(dataObjects, GetType(DataObject()), "GetOptimizationsFromDateRangeTest failed... " & errorString)
    End Sub

    <TestMethod> _
    Public Sub GetOptimizationTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim optimizationParameters As New OptimizationParameters() With { _
            .OptimizationProblemID = tdr.SD10Stops_optimization_problem_id _
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObject As DataObject = route4Me.GetOptimization(optimizationParameters, errorString)

        Assert.IsNotNull(dataObject, "GetOptimizationTest failed... " & errorString)
    End Sub

    <TestMethod>
    Public Sub ReOptimizationTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim optimizationParameters As New OptimizationParameters() With {
            .OptimizationProblemID = tdr.SD10Stops_optimization_problem_id,
            .ReOptimize = True
        }

        ' Run the query
        Dim errorString As String = ""
        Dim dataObject As DataObject = route4Me.UpdateOptimization(optimizationParameters, errorString)

        lsOptimizationIDs.Add(dataObject.OptimizationProblemId)

        Assert.IsNotNull(dataObject, Convert.ToString("ReOptimizationTest failed. ") & errorString)
    End Sub

    <TestMethod>
    Public Sub UpdateOptimizationDestinationTest()
        Dim route4Me = New Route4MeManager(c_ApiKey)
        Dim address = tdr.SD10Stops_route.Addresses(3)

        address.FirstName = "UpdatedFirstName"
        address.LastName = "UpdatedLastName"

        Dim errorString As String = ""
        Dim updatedAddress = route4Me.UpdateOptimizationDestination(address, errorString)

        Assert.IsNotNull(updatedAddress, "UpdateOptimizationDestinationTest failed")
    End Sub


    <TestMethod> _
    Public Sub RemoveOptimizationTest()
        Dim route4Me As New Route4MeManager(c_ApiKey)

        Dim result As Boolean = tdr.SingleDriverRoundTripTest()

        Assert.IsTrue(result, "Generation of the route Single Driver Round Trip failed...")

        Dim opt_id As String = tdr.SDRT_optimization_problem_id
        Assert.IsNotNull(opt_id, "optimizationProblemID is null...")

        Dim OptIDs As String() = New String() {opt_id}

        ' Run the query
        Dim errorString As String = ""
        Dim removed As Boolean = route4Me.RemoveOptimization(OptIDs, errorString)

        Assert.IsTrue(removed, Convert.ToString("RemoveOptimizationTest failed... ") & errorString)
    End Sub

    <TestMethod> _
    Public Sub HybridOptimizationFrom1000AddressesTest()
        Dim ApiKey As String = ApiKeys.actualApiKey
        ' The addresses in this test not allowed for this API key, you shuld put your valid API key.
        ' Comment 2 lines bellow if you have put in the above line your valid key.
        Assert.IsTrue(1 > 0, "")
        Return

        Dim route4Me As New Route4MeManager(ApiKey)

        Dim sAddressFile As String = "Data/CSV/addresses_1000.csv"
        Dim sched0 As New Schedule("daily", False)
        'var csv = new CsvReader(File.OpenText("file.csv"));

        Using reader As TextReader = File.OpenText(sAddressFile)
            Dim csv = New CsvReader(reader)
            'int iCount = 0;
            While csv.Read()
                Dim lng = csv.GetField(0)
                Dim lat = csv.GetField(1)
                Dim [alias] = csv.GetField(2)
                Dim address1 = csv.GetField(3)
                Dim city = csv.GetField(4)
                Dim state = csv.GetField(5)
                Dim zip = csv.GetField(6)
                Dim phone = csv.GetField(7)
                'var sched_date = csv.GetField(8);
                Dim sched_mode = csv.GetField(8)
                Dim sched_enabled = csv.GetField(9)
                Dim sched_every = csv.GetField(10)
                Dim sched_weekdays = csv.GetField(11)
                Dim sched_monthly_mode = csv.GetField(12)
                Dim sched_monthly_dates = csv.GetField(13)
                Dim sched_annually_usenth = csv.GetField(14)
                Dim sched_annually_months = csv.GetField(15)
                Dim sched_nth_n = csv.GetField(16)
                Dim sched_nth_what = csv.GetField(17)

                Dim sAddress As String = ""

                If address1 IsNot Nothing Then
                    sAddress += address1.ToString().Trim()
                End If
                If city IsNot Nothing Then
                    sAddress += ", " + city.ToString().Trim()
                End If
                If state IsNot Nothing Then
                    sAddress += ", " + state.ToString().Trim()
                End If
                If zip IsNot Nothing Then
                    sAddress += ", " + zip.ToString().Trim()
                End If

                If sAddress = "" Then
                    Continue While
                End If

                Dim newLocation As New AddressBookContact()

                If lng IsNot Nothing Then
                    newLocation.cached_lng = Convert.ToDouble(lng)
                End If
                If lat IsNot Nothing Then
                    newLocation.cached_lat = Convert.ToDouble(lat)
                End If
                If [alias] IsNot Nothing Then
                    newLocation.address_alias = [alias].ToString().Trim()
                End If
                newLocation.address_1 = sAddress
                If phone IsNot Nothing Then
                    newLocation.address_phone_number = phone.ToString().Trim()
                End If

                'newLocation.schedule = new Schedule[]{};
                If Not sched0.ValidateScheduleMode(sched_mode) Then
                    Continue While
                End If

                sched0.from = DateTime.Now.ToString("yyyy-MM-dd")

                Dim blNth As Boolean = False
                If sched0.ValidateScheduleMonthlyMode(sched_monthly_mode) Then
                    If sched_monthly_mode = "nth" Then
                        blNth = True
                    End If
                End If
                If sched0.ValidateScheduleUseNth(sched_annually_usenth) Then
                    If sched_annually_usenth.ToString().ToLower() = "true" Then
                        blNth = True
                    End If
                End If

                Dim schedule As New Schedule(sched_mode.ToString(), blNth)

                Dim dt As DateTime = DateTime.Now
                'if (schedule.ValidateScheduleMode(sched_mode))
                '{
                schedule.mode = sched_mode.ToString()
                If schedule.ValidateScheduleEnabled(sched_enabled) Then
                    schedule.enabled = Convert.ToBoolean(sched_enabled)
                    If schedule.ValidateScheduleEvery(sched_every) Then
                        Dim iEvery As Integer = Convert.ToInt32(sched_every)
                        Select Case schedule.mode
                            Case "daily"
                                schedule.daily.every = iEvery
                                Exit Select
                            Case "weekly"
                                If schedule.ValidateScheduleWeekdays(sched_weekdays) Then
                                    schedule.weekly.every = iEvery
                                    Dim arWeekdays As String() = sched_weekdays.Split(","c)
                                    Dim lsWeekdays As New List(Of Integer)()
                                    For i As Integer = 0 To arWeekdays.Length - 1
                                        lsWeekdays.Add(Convert.ToInt32(arWeekdays(i)))
                                    Next
                                    schedule.weekly.weekdays = lsWeekdays.ToArray()
                                End If
                                Exit Select
                            Case "monthly"
                                If schedule.ValidateScheduleMonthlyMode(sched_monthly_mode) Then
                                    schedule.monthly.every = iEvery
                                    schedule.monthly.mode = sched_monthly_mode.ToString()
                                    Select Case schedule.monthly.mode
                                        Case "dates"
                                            If schedule.ValidateScheduleMonthDays(sched_monthly_dates) Then
                                                Dim arMonthdays As String() = sched_monthly_dates.Split(","c)
                                                Dim lsMonthdays As New List(Of Integer)()
                                                For i As Integer = 0 To arMonthdays.Length - 1
                                                    lsMonthdays.Add(Convert.ToInt32(arMonthdays(i)))
                                                Next
                                                schedule.monthly.dates = lsMonthdays.ToArray()
                                            End If
                                            Exit Select
                                        Case "nth"
                                            If schedule.ValidateScheduleNthN(sched_nth_n) Then
                                                schedule.monthly.nth.n = Convert.ToInt32(sched_nth_n)
                                            End If
                                            If schedule.ValidateScheduleNthWhat(sched_nth_what) Then
                                                schedule.monthly.nth.what = Convert.ToInt32(sched_nth_what)
                                            End If
                                            Exit Select
                                    End Select
                                End If
                                Exit Select
                            Case "annually"
                                If schedule.ValidateScheduleUseNth(sched_annually_usenth) Then
                                    schedule.annually.every = iEvery
                                    schedule.annually.use_nth = Convert.ToBoolean(sched_annually_usenth)
                                    If schedule.annually.use_nth Then
                                        If schedule.ValidateScheduleNthN(sched_nth_n) Then
                                            schedule.annually.nth.n = Convert.ToInt32(sched_nth_n)
                                        End If
                                        If schedule.ValidateScheduleNthWhat(sched_nth_what) Then
                                            schedule.annually.nth.what = Convert.ToInt32(sched_nth_what)
                                        End If
                                    Else
                                        If schedule.ValidateScheduleYearMonths(sched_annually_months) Then
                                            Dim arYearmonths As String() = sched_annually_months.Split(","c)
                                            Dim lsMonths As New List(Of Integer)()
                                            For i As Integer = 0 To arYearmonths.Length - 1
                                                lsMonths.Add(Convert.ToInt32(arYearmonths(i)))
                                            Next
                                            schedule.annually.months = lsMonths.ToArray()
                                        End If
                                    End If
                                End If
                                Exit Select
                        End Select

                    End If
                End If
                newLocation.schedule = (New List(Of Schedule)() From { _
                    schedule _
                }).ToArray()
                '}

                Dim errorString As String = ""
                Dim resultContact As AddressBookContact = route4Me.AddAddressBookContact(newLocation, errorString)

                Assert.IsNotNull(resultContact, Convert.ToString("Creation of an addressbook contact failed... ") & errorString)

                If resultContact IsNot Nothing Then
                    Dim AddressId As String = If(resultContact.address_id IsNot Nothing, resultContact.address_id.ToString(), "")
                    If AddressId <> "" Then
                        lsAddressbookContacts.Add(AddressId)
                    End If
                End If

                Thread.Sleep(1000)
            End While
        End Using

        Thread.Sleep(2000)

        Dim tsp1day As New TimeSpan(1, 0, 0, 0)
        Dim lsScheduledDays As New List(Of String)()
        Dim curDate As DateTime = DateTime.Now
        For i As Integer = 0 To 4
            curDate += tsp1day
            lsScheduledDays.Add(curDate.ToString("yyyy-MM-dd"))
        Next

        Dim Depots As Address() = New Address() {New Address() With { _
            .AddressString = "2017 Ambler Ave, Abilene, TX, 79603-2239", _
            .IsDepot = True, _
            .Latitude = 32.474395, _
            .Longitude = -99.7447021, _
            .CurbsideLatitude = 32.474395, _
            .CurbsideLongitude = -99.7447021 _
        }, New Address() With { _
            .AddressString = "807 Ridge Rd, Alamo, TX, 78516-9596", _
            .IsDepot = True, _
            .Latitude = 26.170834, _
            .Longitude = -98.116201, _
            .CurbsideLatitude = 26.170834, _
            .CurbsideLongitude = -98.116201 _
        }, New Address() With { _
            .AddressString = "1430 W Amarillo Blvd, Amarillo, TX, 79107-5505", _
            .IsDepot = True, _
            .Latitude = 35.221969, _
            .Longitude = -101.835288, _
            .CurbsideLatitude = 35.221969, _
            .CurbsideLongitude = -101.835288 _
        }, New Address() With { _
            .AddressString = "3611 Ne 24Th Ave, Amarillo, TX, 79107-7242", _
            .IsDepot = True, _
            .Latitude = 35.236626, _
            .Longitude = -101.795117, _
            .CurbsideLatitude = 35.236626, _
            .CurbsideLongitude = -101.795117 _
        }, New Address() With { _
            .AddressString = "1525 New York Ave, Arlington, TX, 76010-4723", _
            .IsDepot = True, _
            .Latitude = 32.720524, _
            .Longitude = -97.080195, _
            .CurbsideLatitude = 32.720524, _
            .CurbsideLongitude = -97.080195 _
        }}


        Dim errorString1 As String = ""
        Dim errorString2 As String = ""

        For Each ScheduledDay As String In lsScheduledDays
            Dim hparams As New HybridOptimizationParameters() With { _
                .target_date_string = ScheduledDay, _
                .timezone_offset_minutes = -240 _
            }

            Dim resultOptimization As DataObject = route4Me.GetOHybridptimization(hparams, errorString1)

            Assert.IsNotNull(resultOptimization, Convert.ToString("Get Hybrid Optimization failed... ") & errorString1)

            Dim HybridOptimizationId As String = ""

            If resultOptimization IsNot Nothing Then
                HybridOptimizationId = resultOptimization.OptimizationProblemId
            Else
                Continue For
            End If

            '============== Reoptimization =================================
            Dim rParams As New RouteParameters()
            rParams.AlgorithmType = AlgorithmType.CVRP_TW_SD

            Dim optimizationParameters As New OptimizationParameters() With { _
                .OptimizationProblemID = HybridOptimizationId, _
                .ReOptimize = True, _
                .Parameters = rParams, _
                .Addresses = New Address() {Depots(lsScheduledDays.IndexOf(ScheduledDay))} _
            }

            Dim finalOptimization As DataObject = route4Me.UpdateOptimization(optimizationParameters, errorString2)

            Assert.IsNotNull(finalOptimization, Convert.ToString("Update optimization failed... ") & errorString1)

            If finalOptimization IsNot Nothing Then
                lsOptimizationIDs.Add(finalOptimization.OptimizationProblemId)
            End If

            '=================================================================
            Thread.Sleep(5000)
        Next

        Dim removeLocations As Boolean = tdr.RemoveAddressBookContacts(lsAddressbookContacts, ApiKey)

        Assert.IsTrue(removeLocations, "Removing of the addressbook contacts failed...")

    End Sub

    <TestMethod> _
    Public Sub HybridOptimizationFrom1000OrdersTest()
        Dim ApiKey As String = ApiKeys.actualApiKey
        ' The addresses in this test not allowed for this API key, you shuld put your valid API key.
        ' Comment 2 lines bellow if you have put in the above line your valid key.
        Assert.IsTrue(1 > 0, "")
        Return

        Dim route4Me As New Route4MeManager(ApiKey)

        Dim sAddressFile As String = "Data/CSV/orders_1000.csv"

        Using reader As TextReader = File.OpenText(sAddressFile)
            Dim csv = New CsvReader(reader)
            'int iCount = 0;
            While csv.Read()
                Dim lng = csv.GetField(0)
                Dim lat = csv.GetField(1)
                Dim [alias] = csv.GetField(2)
                Dim address1 = csv.GetField(3)
                Dim city = csv.GetField(4)
                Dim state = csv.GetField(5)
                Dim zip = csv.GetField(6)
                Dim phone = csv.GetField(7)
                Dim sched_date = csv.GetField(8)

                Dim sAddress As String = ""

                If address1 IsNot Nothing Then
                    sAddress += address1.ToString().Trim()
                End If
                If city IsNot Nothing Then
                    sAddress += ", " + city.ToString().Trim()
                End If
                If state IsNot Nothing Then
                    sAddress += ", " + state.ToString().Trim()
                End If
                If zip IsNot Nothing Then
                    sAddress += ", " + zip.ToString().Trim()
                End If

                If sAddress = "" Then
                    Continue While
                End If

                Dim newOrder As New Order()

                If lng IsNot Nothing Then
                    newOrder.cached_lat = Convert.ToDouble(lng)
                End If
                If lat IsNot Nothing Then
                    newOrder.cached_lng = Convert.ToDouble(lat)
                End If
                If [alias] IsNot Nothing Then
                    newOrder.address_alias = [alias].ToString().Trim()
                End If
                newOrder.address_1 = sAddress
                If phone IsNot Nothing Then
                    newOrder.EXT_FIELD_phone = phone.ToString().Trim()
                End If

                Dim dt As DateTime = DateTime.Now

                If sched_date IsNot Nothing Then
                    If DateTime.TryParse(sched_date.ToString(), dt) Then
                        dt = Convert.ToDateTime(sched_date)
                        newOrder.day_scheduled_for_YYMMDD = dt.ToString("yyyy-MM-dd")
                    End If
                End If

                Dim errorString As String = ""

                Dim resultOrder As Order = route4Me.AddOrder(newOrder, errorString)
                Assert.IsNotNull(resultOrder, Convert.ToString("Creating of an order failed... ") & errorString)

                If resultOrder IsNot Nothing Then
                    Dim OrderId As String = resultOrder.order_id.ToString()
                    If OrderId <> "" Then
                        lsOrders.Add(OrderId)
                    End If
                End If

                Thread.Sleep(1000)

            End While
        End Using


        Thread.Sleep(2000)

        Dim tsp1day As New TimeSpan(1, 0, 0, 0)
        Dim lsScheduledDays As New List(Of String)()
        Dim curDate As DateTime = DateTime.Now
        For i As Integer = 0 To 4
            curDate += tsp1day
            lsScheduledDays.Add(curDate.ToString("yyyy-MM-dd"))
        Next

        Dim Depots As Address() = New Address() {New Address() With { _
            .AddressString = "2017 Ambler Ave, Abilene, TX, 79603-2239", _
            .IsDepot = True, _
            .Latitude = 32.474395, _
            .Longitude = -99.7447021, _
            .CurbsideLatitude = 32.474395, _
            .CurbsideLongitude = -99.7447021 _
        }, New Address() With { _
            .AddressString = "807 Ridge Rd, Alamo, TX, 78516-9596", _
            .IsDepot = True, _
            .Latitude = 26.170834, _
            .Longitude = -98.116201, _
            .CurbsideLatitude = 26.170834, _
            .CurbsideLongitude = -98.116201 _
        }, New Address() With { _
            .AddressString = "1430 W Amarillo Blvd, Amarillo, TX, 79107-5505", _
            .IsDepot = True, _
            .Latitude = 35.221969, _
            .Longitude = -101.835288, _
            .CurbsideLatitude = 35.221969, _
            .CurbsideLongitude = -101.835288 _
        }, New Address() With { _
            .AddressString = "3611 Ne 24Th Ave, Amarillo, TX, 79107-7242", _
            .IsDepot = True, _
            .Latitude = 35.236626, _
            .Longitude = -101.795117, _
            .CurbsideLatitude = 35.236626, _
            .CurbsideLongitude = -101.795117 _
        }, New Address() With { _
            .AddressString = "1525 New York Ave, Arlington, TX, 76010-4723", _
            .IsDepot = True, _
            .Latitude = 32.720524, _
            .Longitude = -97.080195, _
            .CurbsideLatitude = 32.720524, _
            .CurbsideLongitude = -97.080195 _
        }}

        Dim errorString1 As String
        Dim errorString2 As String

        For Each ScheduledDay As String In lsScheduledDays
            Dim hparams As New HybridOptimizationParameters() With { _
                .target_date_string = ScheduledDay, _
                .timezone_offset_minutes = 480 _
            }

            Dim resultOptimization As DataObject = route4Me.GetOHybridptimization(hparams, errorString1)

            Dim HybridOptimizationId As String = ""

            If resultOptimization IsNot Nothing Then
                HybridOptimizationId = resultOptimization.OptimizationProblemId
                Console.WriteLine("Hybrid optimization generating executed successfully")

                Console.WriteLine("Generated hybrid optimization ID: {0}", HybridOptimizationId)
            Else
                Console.WriteLine("Hybrid optimization generating error: {0}", errorString1)
                Continue For
            End If

            '============== Reoptimization =================================
            Dim rParams As New RouteParameters()
            rParams.AlgorithmType = AlgorithmType.CVRP_TW_SD

            Dim optimizationParameters As New OptimizationParameters() With { _
                .OptimizationProblemID = HybridOptimizationId, _
                .ReOptimize = True, _
                .Parameters = rParams, _
                .Addresses = New Address() {Depots(lsScheduledDays.IndexOf(ScheduledDay))} _
            }

            Dim finalOptimization As DataObject = route4Me.UpdateOptimization(optimizationParameters, errorString2)

            Assert.IsNotNull(finalOptimization, Convert.ToString("Update optimization failed... ") & errorString1)

            If finalOptimization IsNot Nothing Then
                lsOptimizationIDs.Add(finalOptimization.OptimizationProblemId)
            End If


            '=================================================================
            Thread.Sleep(5000)
        Next

        Dim removeOrders As Boolean = tdr.RemoveOrders(lsOrders, ApiKey)

        Assert.IsTrue(removeOrders, "Removing of the orders failed...")

    End Sub

    <ClassCleanup> _
    Public Shared Sub OptimizationsGroupCleanup()
        Dim result As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())

        Assert.IsTrue(result, "Removing of the testing optimization problem failed...")
    End Sub
End Class


<TestClass>
Public Class TelematicsGateWayAPI
    Shared c_ApiKey As String = ApiKeys.actualApiKey

    <TestInitialize>
    Public Sub TelematicsGateWayAPIInitialize()
    End Sub

    <TestMethod>
    Public Sub getAllVendorsTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim vendorParameters As TelematicsVendorParameters = New TelematicsVendorParameters()
        Dim errorString As String = ""
        Dim vendors As TelematicsVendorsResponse = route4Me.GetAllTelematicsVendors(vendorParameters, errorString)
        Assert.IsNotNull(vendors, "The test getAllVendorsTest failed. " & errorString)
        Assert.IsInstanceOfType(vendors, GetType(TelematicsVendorsResponse), "The test getAllVendorsTest failed. " & errorString)
    End Sub

    <TestMethod>
    Public Sub getVendorTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim errorString As String = ""
        Dim vendors As TelematicsVendorsResponse = route4Me.GetAllTelematicsVendors(New TelematicsVendorParameters(), errorString)
        Dim randomNumber As Integer = (New Random()).[Next](0, vendors.Vendors.Count() - 1)
        Dim randomVendorID As String = vendors.Vendors(randomNumber).ID
        Dim vendorParameters As TelematicsVendorParameters = New TelematicsVendorParameters() With {
            .vendorID = Convert.ToUInt32(randomVendorID)
        }
        errorString = ""
        Dim vendor As TelematicsVendorResponse = route4Me.GetTelematicsVendor(vendorParameters, errorString)
        Assert.IsNotNull(vendors, "The test getVendorTest failed. " & errorString)
        Assert.IsInstanceOfType(vendors, GetType(TelematicsVendorsResponse), "The test getVendorTest failed. " & errorString)
    End Sub

    <TestMethod>
    Public Sub searchVendorsTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(c_ApiKey)
        Dim vendorParameters As TelematicsVendorParameters = New TelematicsVendorParameters() With {
            .isIntegrated = 1,
            .Search = "Fleet",
            .Page = 1,
            .perPage = 15
        }
        Dim errorString As String = ""
        Dim vendors As TelematicsVendorsSearchResponse = route4Me.SearchTelematicsVendors(vendorParameters, errorString)
        Assert.IsNotNull(vendors, "The test searchVendorsTest failed. " & errorString)
        Assert.IsInstanceOfType(vendors, GetType(TelematicsVendorsSearchResponse), "The test searchVendorsTest failed. " & errorString)
    End Sub

    <TestMethod>
    Public Sub vendorsComparisonTest()
        Dim route4Me As Route4MeManager = New Route4MeManager(ApiKeys.demoApiKey)
        Dim vendorParameters As TelematicsVendorParameters = New TelematicsVendorParameters() With {
            .Vendors = "55,56,57"
        }
        Dim errorString As String = ""
        Dim vendors As TelematicsVendorsSearchResponse = route4Me.SearchTelematicsVendors(vendorParameters, errorString)
        Assert.IsNotNull(vendors, "The test vendorsComparisonTest failed. " & errorString)
        Assert.IsInstanceOfType(vendors, GetType(TelematicsVendorsSearchResponse), "The test vendorsComparisonTest failed. " & errorString)
    End Sub
End Class

Public Class TestDataRepository
    Private c_ApiKey As String = ApiKeys.actualApiKey

    Public Sub New()
        c_ApiKey = ApiKeys.actualApiKey
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