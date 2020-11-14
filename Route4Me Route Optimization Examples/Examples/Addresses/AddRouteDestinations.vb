Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add destinations to a route.
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        ''' <returns>An array of the added address IDs</returns>
        Public Function AddRouteDestinations(ByVal Optional routeId As String = Nothing) As Integer()
            Dim route4Me As Route4MeManager = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()
                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                routeId = SD10Stops_route_id
            End If

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.143526,
                .Longitude = -83.240354,
                .Time = 0
            }, New Address() With {
                .AddressString = "222 Blake Cir Milledgeville GA 31061",
                .Latitude = 33.177852,
                .Longitude = -83.263535,
                .Time = 0
            }}

            Dim optimalPosition As Boolean = True

            Dim errorString As String = Nothing
            Dim destinationIds As Integer() = route4Me.AddRouteDestinations(routeId, addresses, optimalPosition, errorString)

            PrintExampleRouteResult(SD10Stops_route, errorString)

            If isInnerExample Then
                RemoveTestOptimizations()
                Return Nothing
            Else
                Return destinationIds
            End If
        End Function
    End Class
End Namespace