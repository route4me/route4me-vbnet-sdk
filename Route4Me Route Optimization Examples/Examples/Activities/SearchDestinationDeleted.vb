Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities with the event Destination Deleted
        ''' </summary>
        Public Sub SearchDestinationDeleted()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            Dim routeId As String = SD10Stops_route_id

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim addressId As Integer = CInt(SD10Stops_route.Addresses(2).RouteDestinationId)

            Dim errorString As String = Nothing
            Dim removed As Boolean = route4Me.RemoveRouteDestination(routeId, addressId, errorString)

            If Not removed Then
                Console.WriteLine("Cannot remove the test destination." & Environment.NewLine & errorString)
                Return
            End If

            Dim activityParameters = New ActivityParameters With {
                .ActivityType = "delete-destination",
                .RouteId = routeId
            }
            Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
