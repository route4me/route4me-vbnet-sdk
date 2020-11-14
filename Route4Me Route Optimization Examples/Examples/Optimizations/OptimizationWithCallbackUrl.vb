Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function OptimizationWithCallbackUrl() As DataObject
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "3634 W Market St, Fairlawn, OH 44333",
                .IsDepot = True,
                .Latitude = 41.135762259364,
                .Longitude = -81.629313826561,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing,
                .TimeWindowStart2 = Nothing,
                .TimeWindowEnd2 = Nothing,
                .Time = Nothing
            }, New Address() With {
                .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                .Latitude = 41.135762259364,
                .Longitude = -81.629313826561,
                .TimeWindowStart = 6 * 3600 + 0 * 60,
                .TimeWindowEnd = 6 * 3600 + 30 * 60,
                .TimeWindowStart2 = 7 * 3600 + 0 * 60,
                .TimeWindowEnd2 = 7 * 3600 + 20 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .TimeWindowStart = 7 * 3600 + 30 * 60,
                .TimeWindowEnd = 7 * 3600 + 40 * 60,
                .TimeWindowStart2 = 8 * 3600 + 0 * 60,
                .TimeWindowEnd2 = 8 * 3600 + 10 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .TimeWindowStart = 8 * 3600 + 30 * 60,
                .TimeWindowEnd = 8 * 3600 + 40 * 60,
                .TimeWindowStart2 = 8 * 3600 + 50 * 60,
                .TimeWindowEnd2 = 9 * 3600 + 0 * 60,
                .Time = 100
            }, New Address() With {
                .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                .Latitude = 41.162971496582,
                .Longitude = -81.479049682617,
                .TimeWindowStart = 9 * 3600 + 0 * 60,
                .TimeWindowEnd = 9 * 3600 + 15 * 60,
                .TimeWindowStart2 = 9 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 9 * 3600 + 45 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "1659 Hibbard Dr, Stow, OH 44224",
                .Latitude = 41.194505989552,
                .Longitude = -81.443351581693,
                .TimeWindowStart = 10 * 3600 + 0 * 60,
                .TimeWindowEnd = 10 * 3600 + 15 * 60,
                .TimeWindowStart2 = 10 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 10 * 3600 + 45 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "2705 N River Rd, Stow, OH 44224",
                .Latitude = 41.145240783691,
                .Longitude = -81.410247802734,
                .TimeWindowStart = 11 * 3600 + 0 * 60,
                .TimeWindowEnd = 11 * 3600 + 15 * 60,
                .TimeWindowStart2 = 11 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 11 * 3600 + 45 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087",
                .Latitude = 41.340042114258,
                .Longitude = -81.421226501465,
                .TimeWindowStart = 12 * 3600 + 0 * 60,
                .TimeWindowEnd = 12 * 3600 + 15 * 60,
                .TimeWindowStart2 = 12 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 12 * 3600 + 45 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                .Latitude = 41.148578643799,
                .Longitude = -81.429229736328,
                .TimeWindowStart = 13 * 3600 + 0 * 60,
                .TimeWindowEnd = 13 * 3600 + 15 * 60,
                .TimeWindowStart2 = 13 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 13 * 3600 + 45 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                .Latitude = 41.148578643799,
                .Longitude = -81.429229736328,
                .TimeWindowStart = 14 * 3600 + 0 * 60,
                .TimeWindowEnd = 14 * 3600 + 15 * 60,
                .TimeWindowStart2 = 14 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 14 * 3600 + 45 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .TimeWindowStart = 15 * 3600 + 0 * 60,
                .TimeWindowEnd = 15 * 3600 + 15 * 60,
                .TimeWindowStart2 = 15 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 15 * 3600 + 45 * 60,
                .Time = 300
            }, New Address() With {
                .AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                .Latitude = 41.315116882324,
                .Longitude = -81.558746337891,
                .TimeWindowStart = 16 * 3600 + 0 * 60,
                .TimeWindowEnd = 16 * 3600 + 15 * 60,
                .TimeWindowStart2 = 16 * 3600 + 30 * 60,
                .TimeWindowEnd2 = 17 * 3600 + 0 * 60,
                .Time = 50
            }}

            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Multiple TimeWindows 12 Stops",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 5 * 3600 + 30 * 60,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription()
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .OptimizedCallbackURL = "https://requestb.in/1o6cgge1",
                .ShowDirections = True,
                .Redirect = False,
                .Parameters = parameters
            }

            Dim errorString As String = Nothing
            Dim dataObject As DataObject = route4Me.RunOptimization(
                optimizationParameters,
                errorString)

            PrintExampleOptimizationResult(dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
