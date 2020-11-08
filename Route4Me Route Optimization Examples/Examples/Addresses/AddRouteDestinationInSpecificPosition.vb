Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add route destination in specific position
        ''' </summary>
        Public Sub AddRouteDestinationInSpecificPosition()
            Dim route4Me = New Route4MeManager(Me.ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim route_id As String = SD10Stops_route_id

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.143526,
                .Longitude = -83.240354,
                .SequenceNo = 3,
                .Time = 0
            }}

            Dim optimalPosition As Boolean = False
            Dim errorString As String = Nothing
            Dim destinationIds As Integer() = route4Me.AddRouteDestinations(route_id, addresses, optimalPosition, errorString)

            PrintExampleDestination(destinationIds, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
