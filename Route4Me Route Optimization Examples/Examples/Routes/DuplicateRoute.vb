Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Duplicate a route
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        Public Sub DuplicateRoute(ByVal Optional routeId As String = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerEample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerEample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                routeId = SD10Stops_route_id
            End If

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = routeId
            }

            Dim errorString As String = Nothing
            Dim duplicatedRouteId As String = route4Me.DuplicateRoute(routeParameters, errorString)

            If duplicatedRouteId IsNot Nothing Then RoutesToRemove = New List(Of String)() From {
                duplicatedRouteId
            }

            Console.WriteLine(
                If(
                    duplicatedRouteId IsNot Nothing,
                    String.Format("DuplicateRoute executed successfully, duplicated route ID: {0}", duplicatedRouteId),
                    String.Format("DuplicateRoute error {0}", errorString)
                  )
            )

            If isInnerEample Then
                RemoveTestRoutes()
                RemoveTestOptimizations()
            End If
        End Sub
    End Class
End Namespace