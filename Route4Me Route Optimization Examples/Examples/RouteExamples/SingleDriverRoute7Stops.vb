Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDriverRoute7Stops() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

#Region "Prepare the addresses"

            'input as many custom fields as needed, custom data is passed 
            ' through to mobile devices and to the manifest

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "128 Woodland Dr, Stafford, VA 22556",
                .IsDepot = True,
                .[Alias] = "HQ",
                .Latitude = 38.5022586,
                .Longitude = -77.5402276
            }, New Address() With {
                .AddressString = "2232 Aquia Dr, Stafford, VA 22554",
                .Latitude = 38.4613311,
                .Longitude = -77.3733942,
                .IsDepot = False,
                .[Alias] = "1",
                .TimeWindowStart = 32400,
                .TimeWindowEnd = 82800
            }, New Address() With {
                .AddressString = "94 The Vance Way, Fredericksburg, VA 22405",
                .Latitude = 38.343827,
                .Longitude = -77.358127,
                .IsDepot = False,
                .[Alias] = "2",
                .TimeWindowStart = 32400,
                .TimeWindowEnd = 82800
            }, New Address() With {
                .AddressString = "3 Edgewood Circle, Fredericksburg, VA 22405",
                .Latitude = 38.3560299,
                .Longitude = -77.44275,
                .IsDepot = False,
                .[Alias] = "3",
                .TimeWindowStart = 32400,
                .TimeWindowEnd = 82800
            }, New Address() With {
                .AddressString = "609 Jett St, Fredericksburg, VA 22405",
                .Latitude = 38.321677,
                .Longitude = -77.434507,
                .IsDepot = False,
                .[Alias] = "4",
                .TimeWindowStart = 39600,
                .TimeWindowEnd = 82800
            }, New Address() With {
                .AddressString = "1120 Potomac Ave, Fredericksburg, VA 22405",
                .Latitude = 38.3115498,
                .Longitude = -77.4349647,
                .[Alias] = "5",
                .TimeWindowStart = 39600,
                .TimeWindowEnd = 82800
            }, New Address() With {
                .AddressString = "10809 Stacy Run, Fredericksburg, VA 22408",
                .Latitude = 38.258764,
                .Longitude = -77.425318,
                .[Alias] = "6",
                .TimeWindowStart = 39600,
                .TimeWindowEnd = 82800
            }}

#End Region

            ' Set parameters
            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Test for equal sequences Single Driver Route 7 Stops",
                .DisableOptimization = False,
                .MemberId = 403634,
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 24 * 3600,
                .TravelMode = TravelMode.Driving.GetEnumDescription(),
                .VehicleCapacity = 1,
                .VehicleMaxDistanceMI = 10000,
                .RT = False,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription()
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim dataObject As DataObject = route4Me.RunOptimization(
                                                        optimizationParameters,
                                                        errorString)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            PrintExampleOptimizationResult(dataObject, errorString)

            RemoveTestOptimizations()
        End Function
    End Class
End Namespace