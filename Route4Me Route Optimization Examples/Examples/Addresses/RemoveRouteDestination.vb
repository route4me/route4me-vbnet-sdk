Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove a destination from a route.
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        ''' <param name="destinationId">Destination ID</param>
        Public Sub RemoveRouteDestination(
                ByVal Optional routeId As String = Nothing,
                ByVal Optional destinationId As Integer? = Nothing)

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                routeId = SD10Stops_route_id

                destinationId = CInt(SD10Stops_route.Addresses(2).RouteDestinationId)
            End If

            Dim errorString As String = Nothing
            Dim deleted As Boolean = route4Me.RemoveRouteDestination(routeId, CInt(destinationId), errorString)

            PrintExampleDestination(deleted, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
