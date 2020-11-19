Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of updating a route 
        ''' by sending custom data of an address with HTTP PUT method.
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        ''' <param name="routeDestionationId">Route destination ID</param>
        ''' <param name="CustomData">Custom data</param>
        Public Sub UpdateRouteCustomData(
                    ByVal Optional routeId As String = Nothing,
                    ByVal Optional routeDestionationId As Integer? = Nothing,
                    ByVal Optional CustomData As Dictionary(Of String, String) = Nothing)

            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                routeId = SD10Stops_route_id
                routeDestionationId = SD10Stops_route.Addresses(1).RouteDestinationId
            End If

            Dim parameters = New RouteParametersQuery() With {
                .RouteId = routeId,
                .RouteDestinationId = routeDestionationId
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result As Address = route4Me.
                UpdateRouteCustomData(parameters, CustomData, errorString)

            PrintExampleDestination(result, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace