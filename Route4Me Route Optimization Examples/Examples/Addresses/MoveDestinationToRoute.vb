Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Move a destination to a route after specified destination.
        ''' </summary>
        ''' <param name="toRouteId">Route ID of a destination route</param>
        ''' <param name="routeDestinationId">Source destination ID</param>
        ''' <param name="afterDestinationId">A destination id in a destination route. </param>
        Public Sub MoveDestinationToRoute(ByVal Optional toRouteId As String = Nothing,
                                          ByVal Optional routeDestinationId As Integer? = Nothing,
                                          ByVal Optional afterDestinationId As Integer? = Nothing)

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(toRouteId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                routeDestinationId = CInt(SD10Stops_route.Addresses(2).RouteDestinationId)

                RunSingleDriverRoundTrip()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id,
                    SDRT_optimization_problem_id
                }

                toRouteId = SDRT_route_id

                If toRouteId Is Nothing OrElse toRouteId.Length <> 32 Then
                    Console.WriteLine("Cannot get a route to move the destination")
                    RemoveTestOptimizations()
                    Return
                End If

                afterDestinationId = CInt(SDRT_route.Addresses(3).RouteDestinationId)
            End If

            Dim errorString As String = Nothing
            Dim success As Boolean = route4Me.MoveDestinationToRoute(toRouteId, CInt(routeDestinationId), CInt(afterDestinationId), errorString)

            Console.WriteLine("")

            If success Then
                Console.WriteLine("MoveDestinationToRoute executed successfully")
                Console.WriteLine("Destination {0} moved to Route {1} after Destination {2}", routeDestinationId, toRouteId, afterDestinationId)
            Else
                Console.WriteLine("MoveDestinationToRoute error: {0}", errorString)
            End If

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace