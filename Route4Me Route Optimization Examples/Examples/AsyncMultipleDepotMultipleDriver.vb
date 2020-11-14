Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Async Function AsyncMultipleDepotMultipleDriver() As Task(Of DataObject)

            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "3634 W Market St, Fairlawn, OH 44333",
                .IsDepot = True,
                .Latitude = 41.135762259364,
                .Longitude = -81.629313826561,
                .Time = 300,
                .TimeWindowStart = 28800,
                .TimeWindowEnd = 29465
            }, New Address() With {
                .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                .Latitude = 41.135762259364,
                .Longitude = -81.629313826561,
                .Time = 300,
                .TimeWindowStart = 29465,
                .TimeWindowEnd = 30529
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 30529,
                .TimeWindowEnd = 33779
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 100,
                .TimeWindowStart = 33779,
                .TimeWindowEnd = 33944
            }, New Address() With {
                .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                .Latitude = 41.162971496582,
                .Longitude = -81.479049682617,
                .Time = 300,
                .TimeWindowStart = 33944,
                .TimeWindowEnd = 34801
            }, New Address() With {
                .AddressString = "1659 Hibbard Dr, Stow, OH 44224",
                .Latitude = 41.194505989552,
                .Longitude = -81.443351581693,
                .Time = 300,
                .TimeWindowStart = 34801,
                .TimeWindowEnd = 36366
            }, New Address() With {
                .AddressString = "2705 N River Rd, Stow, OH 44224",
                .Latitude = 41.145240783691,
                .Longitude = -81.410247802734,
                .Time = 300,
                .TimeWindowStart = 36366,
                .TimeWindowEnd = 39173
            }, New Address() With {
                .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087",
                .Latitude = 41.340042114258,
                .Longitude = -81.421226501465,
                .Time = 300,
                .TimeWindowStart = 39173,
                .TimeWindowEnd = 41617
            }, New Address() With {
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                .Latitude = 41.148578643799,
                .Longitude = -81.429229736328,
                .Time = 300,
                .TimeWindowStart = 41617,
                .TimeWindowEnd = 43660
            }, New Address() With {
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                .Latitude = 41.148578643799,
                .Longitude = -81.429229736328,
                .Time = 300,
                .TimeWindowStart = 43660,
                .TimeWindowEnd = 46392
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 46392,
                .TimeWindowEnd = 48389
            }, New Address() With {
                .AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                .Latitude = 41.315116882324,
                .Longitude = -81.558746337891,
                .Time = 50,
                .TimeWindowStart = 48389,
                .TimeWindowEnd = 48449
            }, New Address() With {
                .AddressString = "3933 Klein Ave, Stow, OH 44224",
                .Latitude = 41.169467926025,
                .Longitude = -81.429420471191,
                .Time = 300,
                .TimeWindowStart = 48449,
                .TimeWindowEnd = 50152
            }, New Address() With {
                .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221",
                .Latitude = 41.136692047119,
                .Longitude = -81.493492126465,
                .Time = 300,
                .TimeWindowStart = 50152,
                .TimeWindowEnd = 51982
            }, New Address() With {
                .AddressString = "3731 Osage St, Stow, OH 44224",
                .Latitude = 41.161357879639,
                .Longitude = -81.42293548584,
                .Time = 100,
                .TimeWindowStart = 51982,
                .TimeWindowEnd = 52180
            }, New Address() With {
                .AddressString = "3731 Osage St, Stow, OH 44224",
                .Latitude = 41.161357879639,
                .Longitude = -81.42293548584,
                .Time = 300,
                .TimeWindowStart = 52180,
                .TimeWindowEnd = 54379
            }}

            Dim parameters As RouteParameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.CVRP_TW_MD,
                .RouteName = "Async Multiple Depot, Multiple Driver",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = 1,
                .VehicleMaxDistanceMI = 10000,
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Distance),
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI),
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web),
                .TravelMode = EnumHelper.GetEnumDescription(TravelMode.Driving),
                .Metric = Metric.Geodesic
            }

            Dim optimizationParameters As OptimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            Dim dataObject As DataObject = Nothing
            Dim errorString As String = ""

            Await Task(Of DataObject).Run(
                Function()
                    dataObject = route4Me.RunAsyncOptimization(optimizationParameters, errorString)
                    PrintExampleOptimizationResult(dataObject, errorString)
                    Return dataObject
                End Function)
            Return dataObject
        End Function
    End Class
End Namespace
