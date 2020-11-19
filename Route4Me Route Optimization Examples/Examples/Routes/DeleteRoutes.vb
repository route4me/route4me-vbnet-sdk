Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove specified routes.
        ''' </summary>
        ''' <param name="routeIds">An array of the route IDs</param>
        Public Sub DeleteRoutes(ByVal Optional routeIds As String() = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(routeIds Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()
                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }
                RunSingleDriverRoundTrip()
                OptimizationsToRemove.Add(SDRT_optimization_problem_id)
                routeIds = New String() {SD10Stops_route_id, SDRT_route_id}
            End If

            Dim errorString As String = Nothing
            Dim deletedRouteIds As String() = route4Me.DeleteRoutes(routeIds, errorString)

            Console.WriteLine("")
            Console.WriteLine(
                If(
                    deletedRouteIds IsNot Nothing,
                    String.Format("DeleteRoutes executed successfully, {0} routes deleted", deletedRouteIds.Length),
                    String.Format("DeleteRoutes error {0}", errorString))
                )

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
