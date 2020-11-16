Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Orders to an Optimization Problem object
        ''' </summary>
        Public Sub AddOrdersToOptimization()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim rQueryParams = New OptimizationParameters() With {
                .OptimizationProblemID = SD10Stops_optimization_problem_id,
                .Redirect = False
            }

            Dim lsTimeWindowStart = New List(Of Integer)()

            Dim dtCurDate = DateTime.Now + (New TimeSpan(1, 0, 0, 0))
            dtCurDate = New DateTime(dtCurDate.Year, dtCurDate.Month, dtCurDate.Day, 8, 0, 0)

            Dim tsp1000sec = New TimeSpan(0, 0, 1000)
            Dim tsp7days = New TimeSpan(7, 0, 0, 0)

            lsTimeWindowStart.Add(CInt(R4MeUtils.ConvertToUnixTimestamp(dtCurDate)))
            dtCurDate += tsp1000sec
            lsTimeWindowStart.Add(CInt(R4MeUtils.ConvertToUnixTimestamp(dtCurDate)))
            dtCurDate += tsp1000sec
            lsTimeWindowStart.Add(CInt(R4MeUtils.ConvertToUnixTimestamp(dtCurDate)))

            Dim addresses As Address() = New Address() {New Address With {
                .AddressString = "273 Canal St, New York, NY 10013, USA",
                .Latitude = 40.7191558,
                .Longitude = -74.0011966,
                .[Alias] = "",
                .CurbsideLatitude = 40.7191558,
                .CurbsideLongitude = -74.0011966,
                .IsDepot = True
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
                .TimeWindowStart = lsTimeWindowStart(0),
                .TimeWindowEnd = lsTimeWindowStart(0) + 300,
                .OrderId = 7205705
            }, New Address With {
                .AddressString = "325 Broadway, New York, NY 10007, USA",
                .[Alias] = "BK Restaurant #: 20333",
                .Latitude = 40.71615,
                .Longitude = -74.00505,
                .CurbsideLatitude = 40.71615,
                .CurbsideLongitude = -74.00505,
                .Email = "",
                .Phone = "(212) 227-7535",
                .FirstName = "",
                .LastName = "",
                .CustomFields = New Dictionary(Of String, String) From {
                    {"icon", Nothing}
                },
                .Time = 0,
                .TimeWindowStart = lsTimeWindowStart(1),
                .TimeWindowEnd = lsTimeWindowStart(1) + 300,
                .OrderId = 7205704
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
                .TimeWindowStart = lsTimeWindowStart(2),
                .TimeWindowEnd = lsTimeWindowStart(2) + 300,
                .OrderId = 7205703
            }}

            Dim rParams = New RouteParameters() With {
                .RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Now + tsp7days),
                .RouteTime = 14400,
                .Optimize = "Time",
                .AlgorithmType = AlgorithmType.TSP,
                .RT = False,
                .LockLast = False,
                .VehicleId = "",
                .DisableOptimization = False
            }

            Dim errorString As String = Nothing
            Dim dataObject = route4Me.AddOrdersToOptimization(
                rQueryParams,
                addresses,
                rParams,
                errorString
             )

            PrintExampleOptimizationResult(dataObject, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
