Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Reoptimize a route
        ''' </summary>
        ''' <param name="routeId">Route Id</param>
        Public Sub ReoptimizeRoute(ByVal Optional routeId As String = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                routeId = SD10Stops_route_id
            End If

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = routeId,
                .ReOptimize = True
            }

            Dim errorString As String = Nothing
            Dim dataObject As DataObjectRoute = route4Me.UpdateRoute(routeParameters, errorString)

            PrintExampleRouteResult(dataObject, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
