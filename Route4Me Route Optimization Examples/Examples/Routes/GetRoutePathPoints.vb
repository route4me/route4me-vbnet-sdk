Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get route path points
        ''' </summary>
        Public Sub GetRoutePathPoints()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id,
                .RoutePathOutput = RoutePathOutput.Points.ToString()
            }

            Dim errorString As String = Nothing
            Dim dataObject = route4Me.GetRoute(routeParameters, errorString)

            PrintExampleRouteResult(dataObject, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
