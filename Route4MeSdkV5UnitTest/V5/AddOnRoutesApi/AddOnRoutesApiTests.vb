Imports System
Imports Xunit
Imports System.Collections.Generic
Imports System.Text
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSdkV5UnitTest.Route4MeSdkV5UnitTest.V5
Imports Xunit.Abstractions

Namespace Route4MeSdkV5UnitTest.AddOnRoutesApi
    Public Class AddOnRoutesApiTests
        Implements IDisposable

        Shared c_ApiKey As String = ApiKeys.actualApiKey
        Private ReadOnly _output As ITestOutputHelper
        Shared tdr As TestDataRepository
        Shared tdr2 As TestDataRepository
        Shared lsOptimizationIDs As List(Of String)
        Shared lsRoutenIDs As List(Of String)

        Public Sub New(ByVal output As ITestOutputHelper)
            _output = output

            lsOptimizationIDs = New List(Of String)()
            lsRoutenIDs = New List(Of String)()

            tdr = New TestDataRepository()
            tdr2 = New TestDataRepository()

            Dim result As Boolean = tdr.RunOptimizationSingleDriverRoute10Stops()
            Dim result2 As Boolean = tdr2.RunOptimizationSingleDriverRoute10Stops()
            Dim result3 As Boolean = tdr2.SingleDriverRoundTripTest()
            Dim result4 As Boolean = tdr2.MultipleDepotMultipleDriverWith24StopsTimeWindowTest()

            Assert.[True](result, "Single Driver 10 Stops generation failed.")
            Assert.[True](result2, "Single Driver 10 Stops generation failed.")
            Assert.[True](result4, "Multi-Depot Multi-Driver 24 Stops generation failed.")
            Assert.[True](tdr.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.")
            Assert.[True](tdr2.SD10Stops_route.Addresses.Length > 0, "The route has no addresses.")

            lsOptimizationIDs.Add(tdr.SD10Stops_optimization_problem_id)
            lsOptimizationIDs.Add(tdr2.SD10Stops_optimization_problem_id)
            lsOptimizationIDs.Add(tdr2.SDRT_optimization_problem_id)
            lsOptimizationIDs.Add(tdr2.MDMD24_optimization_problem_id)
        End Sub

        <Fact>
        Public Sub GetAllRoutesTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim routeParameters = New RouteParametersQuery() With {
                .Limit = 1,
                .Offset = 15
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.GetRoutes(routeParameters, resultResponse)

            Assert.IsType(Of DataObjectRoute())(dataObjects)
        End Sub

        <Fact>
        Public Sub GetAllRoutesWithPaginationTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim routeParameters = New RouteParametersQuery() With {
                .Page = 1,
                .PerPage = 20
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.GetAllRoutesWithPagination(routeParameters, resultResponse)

            Assert.IsType(Of DataObjectRoute())(dataObjects)
        End Sub

        <Fact>
        Public Sub GetPaginatedRouteListWithoutElasticSearchTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim routeParameters = New RouteParametersQuery() With {
                .Page = 1,
                .PerPage = 20
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.GetPaginatedRouteListWithoutElasticSearch(routeParameters, resultResponse)

            Assert.IsType(Of DataObjectRoute())(dataObjects)
        End Sub

        <Fact>
        Public Sub GetRouteDataTableWithoutElasticSearchTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim routeParameters = New RouteFilterParameters() With {
                .Page = 1,
                .PerPage = 20,
                .Filters = New RouteFilterParametersFilters() With {
                    .ScheduleDate = New String() {"2021-02-01", "2021-02-01"}
                },
                .OrderBy = New List(Of String())() From {
                    New String() {"route_created_unix", "desc"}
                },
                .TimeZone = "UTC"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.GetRouteDataTableWithElasticSearch(routeParameters, resultResponse)

            Assert.IsType(Of DataObjectRoute())(dataObjects)
        End Sub

        <Fact>
        Public Sub GetRouteDatatableWithElasticSearchTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim routeParameters = New RouteFilterParameters() With {
                .Page = 1,
                .PerPage = 20,
                .Filters = New RouteFilterParametersFilters() With {
                    .ScheduleDate = New String() {"2021-02-01", "2021-02-01"}
                },
                .OrderBy = New List(Of String())() From {
                    New String() {"route_created_unix", "desc"}
                },
                .Timezone = "UTC"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.GetRouteDataTableWithElasticSearch(routeParameters, resultResponse)

            Assert.IsType(Of DataObjectRoute())(dataObjects)
        End Sub

        <Fact>
        Public Sub GetRouteListWithoutElasticSearchTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim routeParameters = New RouteParametersQuery() With {
                .Offset = 0,
                .Limit = 10
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.GetRouteListWithoutElasticSearch(routeParameters, resultResponse)

            Assert.IsType(Of DataObjectRoute())(dataObjects)
        End Sub

        <Fact>
        Public Sub DuplicateRoutesTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)
            Dim routeIDs = New String() {tdr.SD10Stops_route.RouteID}
            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.DuplicateRoute(routeIDs, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of RouteDuplicateResponse)(result)
            Assert.[True](result.Status)

            If result.RouteIDs.Length > 0 Then

                For Each routeId As String In result.RouteIDs
                    lsRoutenIDs.Add(routeId)
                Next
            End If
        End Sub

        <Fact>
        Public Sub DeleteRoutesTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim routeIDs = New String() {tdr2.MDMD24_route_id}
            Dim resultResponse As ResultResponse = Nothing

            Dim result = route4Me.DeleteRoutes(routeIDs, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of RoutesDeleteResponse)(result)
            Assert.[True](result.Deleted)
        End Sub

        <Fact>
        Public Sub GetRouteDataTableConfig()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)
            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetRouteDataTableConfig(resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of RouteDataTableConfigResponse)(result)
        End Sub

        <Fact>
        Public Sub GetRouteDataTableFallbackConfig()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)
            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetRouteDataTableFallbackConfig(resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of RouteDataTableConfigResponse)(result)
        End Sub

        <Fact(Skip:="Will be finished after implementing Route Destinations API")>
        Public Sub UpdateRouteTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            tdr.SD10Stops_route.Parameters.DistanceUnit = DistanceUnit.KM.GetEnumDescription()
            tdr.SD10Stops_route.Parameters.Parts = 2
            tdr.SD10Stops_route.Parameters = Nothing

            Dim addresses = New List(Of Address)()
            tdr.SD10Stops_route.Addresses(2).SequenceNo = 4
            tdr.SD10Stops_route.Addresses(2).[Alias] = "Address 2"
            tdr.SD10Stops_route.Addresses(2).AddressStopType = AddressStopType.Delivery.GetEnumDescription()

            addresses.Add(tdr.SD10Stops_route.Addresses(2))

            tdr.SD10Stops_route.Addresses(3).SequenceNo = 3
            tdr.SD10Stops_route.Addresses(3).[Alias] = "Address 3"
            tdr.SD10Stops_route.Addresses(3).AddressStopType = AddressStopType.PickUp.GetEnumDescription()

            addresses.Add(tdr.SD10Stops_route.Addresses(3))
            tdr.SD10Stops_route.Addresses = addresses.ToArray()

            Dim resultResponse As ResultResponse = Nothing
            Dim updatedRoute = route4Me.UpdateRoute(tdr.SD10Stops_route, resultResponse)
            Assert.NotNull(updatedRoute)
            Assert.IsType(Of DataObjectRoute)(updatedRoute)
        End Sub

        Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
            Dim optimizationResult As Boolean = tdr.RemoveOptimization(lsOptimizationIDs.ToArray())
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)
            Dim resultResponse As ResultResponse = Nothing

            If lsRoutenIDs.Count > 0 Then
                Dim routesDeleteResponse As RoutesDeleteResponse = route4Me.DeleteRoutes(lsRoutenIDs.ToArray(), resultResponse)
            End If

            Assert.[True](optimizationResult, "Removing of the testing optimization problem failed.")
        End Sub
    End Class
End Namespace
