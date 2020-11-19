Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get a route by route ID
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        ''' <param name="getRouteDirections">If true. the directions included in the response</param>
        ''' <param name="getRoutePathPoints">If true. the path points included in the response</param>
        Public Sub GetRoute(ByVal Optional routeId As String = Nothing,
                            ByVal Optional getRouteDirections As Boolean? = Nothing,
                            ByVal Optional getRoutePathPoints As Boolean? = Nothing)

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                routeId = SD10Stops_route_id

                getRouteDirections = True
                getRoutePathPoints = True
            End If

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = routeId,
                .Directions = getRouteDirections,
                .RoutePathOutput = If(getRoutePathPoints = True,
                                      RoutePathOutput.Points.ToString(),
                                      "")
            }

            Dim errorString As String = Nothing
            Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

            PrintExampleRouteResult(dataObject, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
