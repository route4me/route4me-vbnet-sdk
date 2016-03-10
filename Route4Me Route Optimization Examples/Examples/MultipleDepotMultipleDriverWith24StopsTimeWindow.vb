Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function MultipleDepotMultipleDriverWith24StopsTimeWindow() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the addresses
            '#Region "Addresses"


            '#End Region
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "3634 W Market St, Fairlawn, OH 44333", _
                .IsDepot = True, _
                .Latitude = 41.135762259364, _
                .Longitude = -81.629313826561, _
                .Time = 300, _
                .TimeWindowStart = 28800, _
                .TimeWindowEnd = 29465 _
            }, New Address() With { _
                .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221", _
                .Latitude = 41.143505096435, _
                .Longitude = -81.46549987793, _
                .Time = 300, _
                .TimeWindowStart = 29465, _
                .TimeWindowEnd = 30529 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .Time = 300, _
                .TimeWindowStart = 30529, _
                .TimeWindowEnd = 33479 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .Time = 300, _
                .TimeWindowStart = 33479, _
                .TimeWindowEnd = 33944 _
            }, New Address() With { _
                .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221", _
                .Latitude = 41.162971496582, _
                .Longitude = -81.479049682617, _
                .Time = 300, _
                .TimeWindowStart = 33944, _
                .TimeWindowEnd = 34801 _
            }, New Address() With { _
                .AddressString = "1659 Hibbard Dr, Stow, OH 44224", _
                .Latitude = 41.194505989552, _
                .Longitude = -81.443351581693, _
                .Time = 300, _
                .TimeWindowStart = 34801, _
                .TimeWindowEnd = 36366 _
            }, _
                New Address() With { _
                .AddressString = "2705 N River Rd, Stow, OH 44224", _
                .Latitude = 41.145240783691, _
                .Longitude = -81.410247802734, _
                .Time = 300, _
                .TimeWindowStart = 36366, _
                .TimeWindowEnd = 39173 _
            }, New Address() With { _
                .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087", _
                .Latitude = 41.340042114258, _
                .Longitude = -81.421226501465, _
                .Time = 300, _
                .TimeWindowStart = 39173, _
                .TimeWindowEnd = 41617 _
            }, New Address() With { _
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
                .Latitude = 41.148578643799, _
                .Longitude = -81.429229736328, _
                .Time = 300, _
                .TimeWindowStart = 41617, _
                .TimeWindowEnd = 43660 _
            }, New Address() With { _
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
                .Latitude = 41.148579, _
                .Longitude = -81.42923, _
                .Time = 300, _
                .TimeWindowStart = 43660, _
                .TimeWindowEnd = 46392 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .Time = 300, _
                .TimeWindowStart = 46392, _
                .TimeWindowEnd = 48089 _
            }, New Address() With { _
                .AddressString = "559 W Aurora Rd, Northfield, OH 44067", _
                .Latitude = 41.315116882324, _
                .Longitude = -81.558746337891, _
                .Time = 300, _
                .TimeWindowStart = 48089, _
                .TimeWindowEnd = 48449 _
            }, _
                New Address() With { _
                .AddressString = "3933 Klein Ave, Stow, OH 44224", _
                .Latitude = 41.169467926025, _
                .Longitude = -81.429420471191, _
                .Time = 300, _
                .TimeWindowStart = 48449, _
                .TimeWindowEnd = 50152 _
            }, New Address() With { _
                .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221", _
                .Latitude = 41.136692047119, _
                .Longitude = -81.493492126465, _
                .Time = 300, _
                .TimeWindowStart = 50152, _
                .TimeWindowEnd = 51682 _
            }, New Address() With { _
                .AddressString = "3731 Osage St, Stow, OH 44224", _
                .Latitude = 41.161357879639, _
                .Longitude = -81.42293548584, _
                .Time = 300, _
                .TimeWindowStart = 51682, _
                .TimeWindowEnd = 54379 _
            }, New Address() With { _
                .AddressString = "3862 Klein Ave, Stow, OH 44224", _
                .Latitude = 41.167895123363, _
                .Longitude = -81.429973393679, _
                .Time = 300, _
                .TimeWindowStart = 54379, _
                .TimeWindowEnd = 54879 _
            }, New Address() With { _
                .AddressString = "138 Northwood Ln, Tallmadge, OH 44278", _
                .Latitude = 41.085464134812, _
                .Longitude = -81.447411775589, _
                .Time = 300, _
                .TimeWindowStart = 54879, _
                .TimeWindowEnd = 56613 _
            }, New Address() With { _
                .AddressString = "3401 Saratoga Blvd, Stow, OH 44224", _
                .Latitude = 41.148849487305, _
                .Longitude = -81.407363891602, _
                .Time = 300, _
                .TimeWindowStart = 56613, _
                .TimeWindowEnd = 57052 _
            }, _
                New Address() With { _
                .AddressString = "5169 Brockton Dr, Stow, OH 44224", _
                .Latitude = 41.195003509521, _
                .Longitude = -81.392700195312, _
                .Time = 300, _
                .TimeWindowStart = 57052, _
                .TimeWindowEnd = 59004 _
            }, New Address() With { _
                .AddressString = "5169 Brockton Dr, Stow, OH 44224", _
                .Latitude = 41.195003509521, _
                .Longitude = -81.392700195312, _
                .Time = 300, _
                .TimeWindowStart = 59004, _
                .TimeWindowEnd = 60027 _
            }, New Address() With { _
                .AddressString = "458 Aintree Dr, Munroe Falls, OH 44262", _
                .Latitude = 41.1266746521, _
                .Longitude = -81.445808410645, _
                .Time = 300, _
                .TimeWindowStart = 60027, _
                .TimeWindowEnd = 60375 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .Time = 300, _
                .TimeWindowStart = 60375, _
                .TimeWindowEnd = 63891 _
            }, New Address() With { _
                .AddressString = "2299 Tyre Dr, Hudson, OH 44236", _
                .Latitude = 41.250511169434, _
                .Longitude = -81.420433044434, _
                .Time = 300, _
                .TimeWindowStart = 63891, _
                .TimeWindowEnd = 65277 _
            }, New Address() With { _
                .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221", _
                .Latitude = 41.136692047119, _
                .Longitude = -81.493492126465, _
                .Time = 300, _
                .TimeWindowStart = 65277, _
                .TimeWindowEnd = 68545 _
            }}

            ' Set parameters


            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
                .RouteName = "Multiple Depot, Multiple Driver with 24 Stops, Time Window", _
                .StoreRoute = False, _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 60 * 60 * 7, _
                .RouteMaxDuration = 86400, _
                .VehicleCapacity = "1", _
                .VehicleMaxDistanceMI = "10000", _
                .Optimize = Optimize.Distance.Description(), _
                .DistanceUnit = DistanceUnit.MI.Description(), _
                .DeviceType = DeviceType.Web.Description(), _
                .TravelMode = TravelMode.Driving.Description(), _
                .Metric = Metric.Geodesic _
            }

            Dim optimizationParameters As New OptimizationParameters() With { _
                .Addresses = addresses, _
                .Parameters = parameters _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.RunOptimization(optimizationParameters, errorString)

            ' Output the result
            PrintExampleOptimizationResult("MultipleDepotMultipleDriverWith24StopsTimeWindow", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
