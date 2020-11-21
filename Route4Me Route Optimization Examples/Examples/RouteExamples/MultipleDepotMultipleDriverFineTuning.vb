Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of creating an optimization 
        ''' with multi-depot, multi-driver options, And fine-tuning.
        ''' </summary>
        Public Sub MultipleDepotMultipleDriverFineTuning()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            ' Prepare the addresses
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
                .Latitude = 41.143505096435,
                .Longitude = -81.46549987793,
                .Time = 300,
                .TimeWindowStart = 29465,
                .TimeWindowEnd = 30529
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 30529,
                .TimeWindowEnd = 33479
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 33479,
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
                .Latitude = 41.148579,
                .Longitude = -81.42923,
                .Time = 300,
                .TimeWindowStart = 43660,
                .TimeWindowEnd = 46392
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 46392,
                .TimeWindowEnd = 48089
            }, New Address() With {
                .AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                .Latitude = 41.315116882324,
                .Longitude = -81.558746337891,
                .Time = 300,
                .TimeWindowStart = 48089,
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
                .TimeWindowEnd = 51682
            }, New Address() With {
                .AddressString = "3731 Osage St, Stow, OH 44224",
                .Latitude = 41.161357879639,
                .Longitude = -81.42293548584,
                .Time = 300,
                .TimeWindowStart = 51682,
                .TimeWindowEnd = 54379
            }, New Address() With {
                .AddressString = "3862 Klein Ave, Stow, OH 44224",
                .Latitude = 41.167895123363,
                .Longitude = -81.429973393679,
                .Time = 300,
                .TimeWindowStart = 54379,
                .TimeWindowEnd = 54879
            }, New Address() With {
                .AddressString = "138 Northwood Ln, Tallmadge, OH 44278",
                .Latitude = 41.085464134812,
                .Longitude = -81.447411775589,
                .Time = 300,
                .TimeWindowStart = 54879,
                .TimeWindowEnd = 56613
            }, New Address() With {
                .AddressString = "3401 Saratoga Blvd, Stow, OH 44224",
                .Latitude = 41.148849487305,
                .Longitude = -81.407363891602,
                .Time = 300,
                .TimeWindowStart = 56613,
                .TimeWindowEnd = 57052
            }, New Address() With {
                .AddressString = "5169 Brockton Dr, Stow, OH 44224",
                .Latitude = 41.195003509521,
                .Longitude = -81.392700195312,
                .Time = 300,
                .TimeWindowStart = 57052,
                .TimeWindowEnd = 59004
            }, New Address() With {
                .AddressString = "5169 Brockton Dr, Stow, OH 44224",
                .Latitude = 41.195003509521,
                .Longitude = -81.392700195312,
                .Time = 300,
                .TimeWindowStart = 59004,
                .TimeWindowEnd = 60027
            }, New Address() With {
                .AddressString = "458 Aintree Dr, Munroe Falls, OH 44262",
                .Latitude = 41.1266746521,
                .Longitude = -81.445808410645,
                .Time = 300,
                .TimeWindowStart = 60027,
                .TimeWindowEnd = 60375
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 60375,
                .TimeWindowEnd = 63891
            }, New Address() With {
                .AddressString = "2299 Tyre Dr, Hudson, OH 44236",
                .Latitude = 41.250511169434,
                .Longitude = -81.420433044434,
                .Time = 300,
                .TimeWindowStart = 63891,
                .TimeWindowEnd = 65277
            }, New Address() With {
                .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221",
                .Latitude = 41.136692047119,
                .Longitude = -81.493492126465,
                .Time = 300,
                .TimeWindowStart = 65277,
                .TimeWindowEnd = 68545
            }}

            ' Set parameters
            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.CVRP_TW_MD,
                .RouteName = "Multiple Depot, Multiple Driver Fine Tuning, Time Window",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400 * 3,
                .VehicleCapacity = 5,
                .VehicleMaxDistanceMI = 10000,
                .Optimize = Optimize.TimeWithTraffic.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription(),
                .Metric = Metric.Matrix,
                .TargetDistance = 100,
                .TargetDuration = 1,
                .WaitingTime = 1
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim dataObjectFineTuning = route4Me.RunOptimization(
                                                    optimizationParameters,
                                                    errorString)

            PrintExampleOptimizationResult(dataObjectFineTuning, errorString)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObjectFineTuning?.OptimizationProblemId, Nothing)
            }

            Console.WriteLine("")
            Console.WriteLine(
                "TargetDistance: " & (If(dataObjectFineTuning?.Parameters?.TargetDistance, Nothing))
                )
            Console.WriteLine(
                "TargetDuration: " & (If(dataObjectFineTuning?.Parameters?.TargetDuration, Nothing))
                )
            Console.WriteLine(
                "WaitingTime: " & (If(dataObjectFineTuning?.Parameters?.WaitingTime, Nothing))
                )

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace