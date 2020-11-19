Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Assign a vehicle to a route.
        ''' </summary>
        Public Sub AssignVehicleToRoute()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim vehicleParameters = New VehicleParameters With {
                .WithPagination = True,
                .Page = 1,
                .PerPage = 10
            }

            Dim errorString As String = Nothing
            Dim vehicles = route4Me.GetVehicles(vehicleParameters, errorString)

            Dim randomNumber As Integer = (New Random()).[Next](0, vehicles.PerPage - 1)

            Dim vehicleId = vehicles.Data(randomNumber).VehicleId

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeId As String = SD10Stops_route_id

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = routeId,
                .Parameters = New RouteParameters() With {
                    .VehicleId = vehicleId
                }
            }

            Dim route = route4Me.UpdateRoute(routeParameters, errorString)

            PrintExampleRouteResult(route, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace