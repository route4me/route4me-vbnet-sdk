' See video tutorial here: http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter2:subchapter1

Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function MultipleDepotMultipleDriver() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Prepare the addresses
            '#Region "Addresses"

            'all possible originating locations are depots, should be marked as true
            'stylistically we recommend all depots should be at the top of the destinations list

            'the number of seconds at destination

            'together these two specify the time window of a destination
            'seconds offset relative to the route start time for the open availability of a destination

            'seconds offset relative to the route end time for the open availability of a destination

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
                .Latitude = 41.135762259364, _
                .Longitude = -81.629313826561, _
                .Time = 300, _
                .TimeWindowStart = 29465, _
                .TimeWindowEnd = 30529 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .Time = 300, _
                .TimeWindowStart = 30529, _
                .TimeWindowEnd = 33779 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .Time = 100, _
                .TimeWindowStart = 33779, _
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
                .Latitude = 41.148578643799, _
                .Longitude = -81.429229736328, _
                .Time = 300, _
                .TimeWindowStart = 43660, _
                .TimeWindowEnd = 46392 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .Time = 300, _
                .TimeWindowStart = 46392, _
                .TimeWindowEnd = 48389 _
            }, New Address() With { _
                .AddressString = "559 W Aurora Rd, Northfield, OH 44067", _
                .Latitude = 41.315116882324, _
                .Longitude = -81.558746337891, _
                .Time = 50, _
                .TimeWindowStart = 48389, _
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
                .TimeWindowEnd = 51982 _
            }, New Address() With { _
                .AddressString = "3731 Osage St, Stow, OH 44224", _
                .Latitude = 41.161357879639, _
                .Longitude = -81.42293548584, _
                .Time = 100, _
                .TimeWindowStart = 51982, _
                .TimeWindowEnd = 52180 _
            }, New Address() With { _
                .AddressString = "3731 Osage St, Stow, OH 44224", _
                .Latitude = 41.161357879639, _
                .Longitude = -81.42293548584, _
                .Time = 300, _
                .TimeWindowStart = 52180, _
                .TimeWindowEnd = 54379 _
            }}

            ' Set parameters
            'specify capacitated vehicle routing with time windows and multiple depots, with multiple drivers

            'set an arbitrary route name
            'this value shows up in the website, and all the connected mobile device

            'the route start date in UTC, unix timestamp seconds (Tomorrow)
            'the time in UTC when a route is starting (7AM)

            'the maximum duration of a route

            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
                .RouteName = "Multiple Depot, Multiple Driver", _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 60 * 60 * 7, _
                .RouteMaxDuration = 86400, _
                .VehicleCapacity = "1", _
                .VehicleMaxDistanceMI = "10000", _
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Distance), _
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI), _
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web), _
                .TravelMode = EnumHelper.GetEnumDescription(TravelMode.Driving), _
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
            PrintExampleOptimizationResult(dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
