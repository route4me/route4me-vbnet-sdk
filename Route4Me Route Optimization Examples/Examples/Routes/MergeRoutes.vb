Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Merge two routes in one.
        ''' </summary>
        ''' <param name="mergeRoutesParameters">MergeRoutesQuery type parameters</param>
        Public Sub MergeRoutes(ByVal Optional mergeRoutesParameters As MergeRoutesQuery = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)
            Dim isInnerExample As Boolean = If(mergeRoutesParameters Is Nothing,
                                               True,
                                               False
                                              )

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                RunSingleDriverRoundTrip()
                OptimizationsToRemove.Add(SDRT_optimization_problem_id)

                mergeRoutesParameters = New MergeRoutesQuery() With {
                    .RouteIds = SD10Stops_route_id & "," + SDRT_route_id,
                    .ToRouteId = SD10Stops_route_id,
                    .DepotAddress = SD10Stops_route.Addresses(0).AddressString,
                    .RouteDestinationId = SD10Stops_route.
                                            Addresses(0).
                                            RouteDestinationId.
                                            ToString(),
                    .DepotLat = SD10Stops_route.Addresses(0).Latitude,
                    .DepotLng = SD10Stops_route.Addresses(0).Longitude,
                    .RemoveOrigin = False
                }
            End If

            Dim errorString As String = Nothing
            Dim result As Boolean = route4Me.MergeRoutes(
                                    mergeRoutesParameters,
                                    errorString)

            Console.WriteLine(
                If(
                    result,
                    String.Format("MergeRoutes executed successfully, {0} routes merged", mergeRoutesParameters.RouteIds),
                    String.Format("MergeRoutes error {0}", errorString)
                )
            )

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
