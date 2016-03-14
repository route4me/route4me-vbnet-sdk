Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDriverRoute10Stops() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the addresses
            '#Region "Addresses"

            'indicate that this is a departure stop
            'single depot routes can only have one departure depot 

            'required coordinates for every departure and stop on the route

            'the expected time on site, in seconds. this value is incorporated into the optimization engine
            'it also adjusts the estimated and dynamic eta's for a route


            'input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest

            '#End Region
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "151 Arbor Way Milledgeville GA 31061", _
                .IsDepot = True, _
                .Latitude = 33.132675170898, _
                .Longitude = -83.244743347168, _
                .Time = 0, _
                .CustomFields = New Dictionary(Of String, String)() From { _
                    {"color", "red"}, _
                    {"size", "huge"} _
                } _
            }, New Address() With { _
                .AddressString = "230 Arbor Way Milledgeville GA 31061", _
                .Latitude = 33.129695892334, _
                .Longitude = -83.24577331543, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "148 Bass Rd NE Milledgeville GA 31061", _
                .Latitude = 33.143497, _
                .Longitude = -83.224487, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061", _
                .Latitude = 33.141784667969, _
                .Longitude = -83.237518310547, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061", _
                .Latitude = 33.141086578369, _
                .Longitude = -83.238258361816, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "131 Bill Johnson Rd NE Milledgeville GA 31061", _
                .Latitude = 33.142036437988, _
                .Longitude = -83.238845825195, _
                .Time = 0 _
            }, _
                New Address() With { _
                .AddressString = "138 Bill Johnson Rd NE Milledgeville GA 31061", _
                .Latitude = 33.14307, _
                .Longitude = -83.239334, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "139 Bill Johnson Rd NE Milledgeville GA 31061", _
                .Latitude = 33.142734527588, _
                .Longitude = -83.237442016602, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "145 Bill Johnson Rd NE Milledgeville GA 31061", _
                .Latitude = 33.143871307373, _
                .Longitude = -83.237342834473, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "221 Blake Cir Milledgeville GA 31061", _
                .Latitude = 33.081462860107, _
                .Longitude = -83.208511352539, _
                .Time = 0 _
            }}

            ' Set parameters

            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.TSP, _
                .StoreRoute = False, _
                .RouteName = "Single Driver Route 10 Stops", _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 60 * 60 * 7, _
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
            PrintExampleOptimizationResult("SingleDriverRoute10Stops", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
