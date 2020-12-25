Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities with the event Destination Inserted
        ''' </summary>
        Public Sub SearchDestinationInserted()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            Dim routeId As String = SD10Stops_route_id

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim newAddress = New Address() With {
                .AddressString = "118 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.141784667969,
                .Longitude = -83.237518310547,
                .Time = 0,
                .SequenceNo = 4
            }

            Dim errorString As String = Nothing
            Dim insertedDestinations As Integer() = route4Me.AddRouteDestinations(routeId, New Address() {newAddress}, errorString)

            If insertedDestinations Is Nothing OrElse insertedDestinations.Length < 1 Then
                Console.WriteLine("Cannot insert the test destination." & Environment.NewLine & errorString)

                RemoveTestOptimizations()

                Return
            End If

            Dim activityParameters As ActivityParameters = New ActivityParameters With {
                .ActivityType = "insert-destination",
                .RouteId = routeId
            }

            Dim activities As Activity() = route4Me.GetActivities(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
