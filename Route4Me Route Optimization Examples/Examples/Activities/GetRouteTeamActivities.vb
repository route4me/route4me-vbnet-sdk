Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Team Activities on a Route
        ''' </summary>
        Public Sub GetRouteTeamActivities()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            Dim routeId As String = SD10Stops_route_id

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim activityParameters = New ActivityParameters() With {
                .RouteId = routeId,
                .team = "true",
                .Limit = 10,
                .Offset = 0
            }
            Dim errorString As String = Nothing
            Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
