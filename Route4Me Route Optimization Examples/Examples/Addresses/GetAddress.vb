Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get destination from a route.
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        ''' <param name="routeDestinationId">A route destination ID</param>
        Public Sub GetAddress(ByVal Optional routeId As String = Nothing, ByVal Optional routeDestinationId As Integer? = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            If routeId Is Nothing Then
                RunOptimizationSingleDriverRoute10Stops()
                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }
            End If

            Dim addressParameters = New AddressParameters() With {
                .RouteId = If((routeId Is Nothing), SD10Stops_route_id, routeId),
                .RouteDestinationId = If((routeDestinationId Is Nothing), CInt(SD10Stops_route.Addresses(2).RouteDestinationId), CInt(routeDestinationId)),
                .Notes = True
            }

            Dim errorString As String = Nothing
            Dim destination As Address = route4Me.GetAddress(addressParameters, errorString)

            PrintExampleDestination(destination, errorString)

            If routeId Is Nothing Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
