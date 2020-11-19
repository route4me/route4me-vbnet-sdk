Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get origin route of the duplicated route.
        ''' </summary>
        Public Sub RouteOriginParameter()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim duplParameters = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id
            }

            Dim errorString As String = Nothing
            Dim duplicatedRouteId As String = route4Me.
                DuplicateRoute(duplParameters, errorString)

            If duplicatedRouteId IsNot Nothing Then
                RoutesToRemove = New List(Of String)() From {
                    duplicatedRouteId
                }
            Else
                Console.WriteLine("Cannot duplicate the route")
                Return
            End If

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = duplicatedRouteId,
                .Original = True
            }

            Dim route = route4Me.GetRoute(routeParameters, errorString)

            PrintExampleRouteResult(If(route?.OriginalRoute, Nothing), errorString)

            RemoveTestRoutes()
            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace