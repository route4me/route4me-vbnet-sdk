Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities with the event Destination Marked as Departed
        ''' </summary>
        Public Sub SearchDestinationMarkedAsDeparted()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            Dim routeId As String = SD10Stops_route_id

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim addressId As Integer = CInt(SD10Stops_route.Addresses(2).RouteDestinationId)

            Dim addressVisitedParams = New AddressParameters() With {
                .RouteId = routeId,
                .AddressId = addressId,
                .IsVisited = True
            }

            Dim errorString As String = Nothing
            Dim visitedStatus As Integer = route4Me.MarkAddressVisited(addressVisitedParams, errorString)

            If visitedStatus <> 1 Then
                Console.WriteLine("Cannot mark the test destination as visited." & Environment.NewLine & errorString)

                RemoveTestOptimizations()

                Return
            End If

            Dim addressDepartParams = New AddressParameters() With {
                .RouteId = routeId,
                .AddressId = addressId,
                .IsDeparted = True
            }

            Dim departedStatus As Integer = route4Me.MarkAddressDeparted(addressDepartParams, errorString)

            If departedStatus <> 1 Then
                Console.WriteLine("Cannot mark the test destination as departed." & Environment.NewLine & errorString)
                RemoveTestOptimizations()
                Return
            End If

            Dim activityParameters = New ActivityParameters With {
                .ActivityType = "mark-destination-departed",
                .RouteId = routeId
            }

            Dim activities As Activity() = route4Me.GetActivities(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
