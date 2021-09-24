Imports System.Threading
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Unlink a route from an optimization.
        ''' </summary>
        Public Sub UnlinkRouteFromOptimization()
            Dim route4Me = New Route4MeManager(ActualApiKey)
            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeDuplicateParameters = New RouteParametersQuery() With {
                .DuplicateRoutesId = New String() {SD10Stops_route_id}
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim duplicatedResult = route4Me.DuplicateRoute(routeDuplicateParameters, errorString)

            If Not (If(duplicatedResult?.Status, False)) OrElse (If(duplicatedResult?.RouteIDs?.Length, 0)) < 1 Then
                Console.WriteLine($"Cannot duplicate the route. {errorString}")
                Return
            End If

            Thread.Sleep(5000)

            Dim duplicatedRoute = route4Me.GetRoute(New RouteParametersQuery() With {
                .RouteId = duplicatedResult.RouteIDs(0)
            }, errorString)

            If duplicatedRoute Is Nothing AndAlso duplicatedRoute.[GetType]() <> GetType(DataObjectRoute) Then
                Console.WriteLine($"Cannot retrieve the duplicated route. {errorString}")
                Return
            End If

            RoutesToRemove = New List(Of String)() From {
                duplicatedResult.RouteIDs(0)
            }

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = duplicatedResult.RouteIDs(0),
                .UnlinkFromMasterOptimization = True
            }

            Dim unlinkedRoute = route4Me.UpdateRoute(routeParameters, errorString)

            PrintExampleRouteResult(unlinkedRoute, errorString)

            RemoveTestRoutes()
            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace