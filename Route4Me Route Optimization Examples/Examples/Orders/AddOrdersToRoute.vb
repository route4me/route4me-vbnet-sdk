Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddOrdersToRoute()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim rQueryParams = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id,
                .Redirect = False
            }

            Dim addresses As Address() = New Address() {New Address With {
                .AddressString = "273 Canal St, New York, NY 10013, USA",
                .Latitude = 40.7191558,
                .Longitude = -74.0011966,
                .[Alias] = "",
                .CurbsideLatitude = 40.7191558,
                .CurbsideLongitude = -74.0011966
            }, New Address With {
                .AddressString = "106 Liberty St, New York, NY 10006, USA",
                .[Alias] = "BK Restaurant #: 2446",
                .Latitude = 40.709637,
                .Longitude = -74.011912,
                .CurbsideLatitude = 40.709637,
                .CurbsideLongitude = -74.011912,
                .Email = "",
                .Phone = "(917) 338-1887",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .OrderId = 7205705
            }, New Address With {
                .AddressString = "106 Fulton St, Farmingdale, NY 11735, USA",
                .[Alias] = "BK Restaurant #: 17871",
                .Latitude = 40.73073,
                .Longitude = -73.459283,
                .CurbsideLatitude = 40.73073,
                .CurbsideLongitude = -73.459283,
                .Email = "",
                .Phone = "(212) 566-5132",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .OrderId = 7205703
            }}

            Dim rParams = New RouteParameters() With {
                .RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)",
                .RouteDate = 1465948800,
                .RouteTime = 14400,
                .Optimize = "Time",
                .AlgorithmType = AlgorithmType.TSP,
                .RT = False,
                .LockLast = False,
                .VehicleId = "",
                .DisableOptimization = False
            }

            Dim errorString As String = Nothing
            Dim result As RouteResponse = route4Me.AddOrdersToRoute(
                rQueryParams,
                addresses,
                rParams,
                errorString
             )

            PrintExampleRouteResult(result, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace