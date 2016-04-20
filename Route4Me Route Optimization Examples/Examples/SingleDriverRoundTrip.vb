' See video tutorial here: http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter1:subchapter3

Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDriverRoundTrip() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the addresses
            '#Region "Addresses"
            '#End Region
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "754 5th Ave New York, NY 10019", _
                .[Alias] = "Bergdorf Goodman", _
                .IsDepot = True, _
                .Latitude = 40.7636197, _
                .Longitude = -73.9744388, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "717 5th Ave New York, NY 10022", _
                .[Alias] = "Giorgio Armani", _
                .Latitude = 40.7669692, _
                .Longitude = -73.9693864, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "888 Madison Ave New York, NY 10014", _
                .[Alias] = "Ralph Lauren Women's and Home", _
                .Latitude = 40.7715154, _
                .Longitude = -73.9669241, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "1011 Madison Ave New York, NY 10075", _
                .[Alias] = "Yigal Azrou'l", _
                .Latitude = 40.7772129, _
                .Longitude = -73.9669, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "440 Columbus Ave New York, NY 10024", _
                .[Alias] = "Frank Stella Clothier", _
                .Latitude = 40.7808364, _
                .Longitude = -73.9732729, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "324 Columbus Ave #1 New York, NY 10023", _
                .[Alias] = "Liana", _
                .Latitude = 40.7803123, _
                .Longitude = -73.9793079, _
                .Time = 0 _
            }, _
                New Address() With { _
                .AddressString = "110 W End Ave New York, NY 10023", _
                .[Alias] = "Toga Bike Shop", _
                .Latitude = 40.7753077, _
                .Longitude = -73.9861529, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "555 W 57th St New York, NY 10019", _
                .[Alias] = "BMW of Manhattan", _
                .Latitude = 40.7718005, _
                .Longitude = -73.9897716, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "57 W 57th St New York, NY 10019", _
                .[Alias] = "Verizon Wireless", _
                .Latitude = 40.7558695, _
                .Longitude = -73.9862019, _
                .Time = 0 _
            }}

            ' Set parameters

            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.TSP, _
                .StoreRoute = False, _
                .RouteName = "Single Driver Round Trip", _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 60 * 60 * 7, _
                .RouteMaxDuration = 86400, _
                .VehicleCapacity = "1", _
                .VehicleMaxDistanceMI = "10000", _
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Distance), _
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI), _
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web), _
                .TravelMode = EnumHelper.GetEnumDescription(TravelMode.Driving) _
            }

            Dim optimizationParameters As New OptimizationParameters() With { _
                .Addresses = addresses, _
                .Parameters = parameters _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.RunOptimization(OptimizationParameters, errorString)

            ' Output the result
            PrintExampleOptimizationResult("SingleDriverRoundTrip", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
