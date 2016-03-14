Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDriverMultipleTimeWindows() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the addresses
            '#Region "Addresses"

            'all possible originating locations are depots, should be marked as true
            'stylistically we recommend all depots should be at the top of the destinations list


            'together these two specify the time window of a destination
            'seconds offset relative to the route start time for the open availability of a destination
            'seconds offset relative to the route end time for the open availability of a destination

            ' Second 'TimeWindowStart'
            ' Second 'TimeWindowEnd'

            'the number of seconds at destination



            '#End Region
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "3634 W Market St, Fairlawn, OH 44333", _
                .IsDepot = True, _
                .Latitude = 41.135762259364, _
                .Longitude = -81.629313826561, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing, _
                .TimeWindowStart2 = Nothing, _
                .TimeWindowEnd2 = Nothing, _
                .Time = Nothing _
            }, New Address() With { _
                .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221", _
                .Latitude = 41.135762259364, _
                .Longitude = -81.629313826561, _
                .TimeWindowStart = 6 * 3600 + 0 * 60, _
                .TimeWindowEnd = 6 * 3600 + 30 * 60, _
                .TimeWindowStart2 = 7 * 3600 + 0 * 60, _
                .TimeWindowEnd2 = 7 * 3600 + 20 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .TimeWindowStart = 7 * 3600 + 30 * 60, _
                .TimeWindowEnd = 7 * 3600 + 40 * 60, _
                .TimeWindowStart2 = 8 * 3600 + 0 * 60, _
                .TimeWindowEnd2 = 8 * 3600 + 10 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .TimeWindowStart = 8 * 3600 + 30 * 60, _
                .TimeWindowEnd = 8 * 3600 + 40 * 60, _
                .TimeWindowStart2 = 8 * 3600 + 50 * 60, _
                .TimeWindowEnd2 = 9 * 3600 + 0 * 60, _
                .Time = 100 _
            }, New Address() With { _
                .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221", _
                .Latitude = 41.162971496582, _
                .Longitude = -81.479049682617, _
                .TimeWindowStart = 9 * 3600 + 0 * 60, _
                .TimeWindowEnd = 9 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 9 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 9 * 3600 + 45 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "1659 Hibbard Dr, Stow, OH 44224", _
                .Latitude = 41.194505989552, _
                .Longitude = -81.443351581693, _
                .TimeWindowStart = 10 * 3600 + 0 * 60, _
                .TimeWindowEnd = 10 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 10 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 10 * 3600 + 45 * 60, _
                .Time = 300 _
            }, _
                New Address() With { _
                .AddressString = "2705 N River Rd, Stow, OH 44224", _
                .Latitude = 41.145240783691, _
                .Longitude = -81.410247802734, _
                .TimeWindowStart = 11 * 3600 + 0 * 60, _
                .TimeWindowEnd = 11 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 11 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 11 * 3600 + 45 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087", _
                .Latitude = 41.340042114258, _
                .Longitude = -81.421226501465, _
                .TimeWindowStart = 12 * 3600 + 0 * 60, _
                .TimeWindowEnd = 12 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 12 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 12 * 3600 + 45 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
                .Latitude = 41.148578643799, _
                .Longitude = -81.429229736328, _
                .TimeWindowStart = 13 * 3600 + 0 * 60, _
                .TimeWindowEnd = 13 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 13 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 13 * 3600 + 45 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262", _
                .Latitude = 41.148578643799, _
                .Longitude = -81.429229736328, _
                .TimeWindowStart = 14 * 3600 + 0 * 60, _
                .TimeWindowEnd = 14 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 14 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 14 * 3600 + 45 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "512 Florida Pl, Barberton, OH 44203", _
                .Latitude = 41.003671512008, _
                .Longitude = -81.598461046815, _
                .TimeWindowStart = 15 * 3600 + 0 * 60, _
                .TimeWindowEnd = 15 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 15 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 15 * 3600 + 45 * 60, _
                .Time = 300 _
            }, New Address() With { _
                .AddressString = "559 W Aurora Rd, Northfield, OH 44067", _
                .Latitude = 41.315116882324, _
                .Longitude = -81.558746337891, _
                .TimeWindowStart = 16 * 3600 + 0 * 60, _
                .TimeWindowEnd = 16 * 3600 + 15 * 60, _
                .TimeWindowStart2 = 16 * 3600 + 30 * 60, _
                .TimeWindowEnd2 = 17 * 3600 + 0 * 60, _
                .Time = 50 _
            }}

            ' Set parameters

            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.TSP, _
                .StoreRoute = False, _
                .RouteName = "Single Driver Multiple TimeWindows 12 Stops", _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 5 * 3600 + 30 * 60, _
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Distance), _
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI), _
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web) _
            }

            Dim optimizationParameters As New OptimizationParameters() With { _
                .Addresses = addresses, _
                .Parameters = parameters _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.RunOptimization(optimizationParameters, errorString)

            ' Output the result
            PrintExampleOptimizationResult("SingleDriverMultipleTimeWindows", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
