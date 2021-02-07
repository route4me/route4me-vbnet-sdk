Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSdkV5UnitTest.V5

    Public Class ApiKeys
        Public Shared actualApiKey As String = R4MeUtils.ReadSetting("actualApiKey")
        Public Shared demoApiKey As String = R4MeUtils.ReadSetting("demoApiKey")
    End Class

    Public Class TestDataRepository

        Private c_ApiKey As String = ApiKeys.actualApiKey

        Public Sub New()
            c_ApiKey = ApiKeys.actualApiKey
        End Sub

        Public Property dataObjectSD10Stops As DataObject

        Public Property SD10Stops_optimization_problem_id As String

        Public Property SD10Stops_route As DataObjectRoute

        Public Property SD10Stops_route_id As String


        Public Property dataObjectSDRT As DataObject

        Public Property SDRT_optimization_problem_id As String

        Public Property SDRT_route As DataObjectRoute

        Public Property SDRT_route_id As String


        Public Property dataObjectMDMD24 As DataObject

        Public Property MDMD24_optimization_problem_id As String

        Public Property MDMD24_route() As DataObjectRoute

        Public Property MDMD24_route_id() As String

        Public Function RunOptimizationSingleDriverRoute10Stops() As Boolean
            Dim r4mm As New Route4MeManagerV5(c_ApiKey)

            ' Prepare the addresses
            '
            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "151 Arbor Way Milledgeville GA 31061",
                .IsDepot = True,
                .Latitude = 33.132675170898,
                .Longitude = -83.244743347168,
                .Time = 0,
                .CustomFields = New Dictionary(Of String, String)() From {
                    {"color", "red"},
                    {"size", "huge"}
                }
            }, New Address() With {
                .AddressString = "230 Arbor Way Milledgeville GA 31061",
                .Latitude = 33.129695892334,
                .Longitude = -83.24577331543,
                .Time = 0
            }, New Address() With {
                .AddressString = "148 Bass Rd NE Milledgeville GA 31061",
                .Latitude = 33.143497,
                .Longitude = -83.224487,
                .Time = 0
            }, New Address() With {
                .AddressString = "117 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.141784667969,
                .Longitude = -83.237518310547,
                .Time = 0
            }, New Address() With {
                .AddressString = "119 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.141086578369,
                .Longitude = -83.238258361816,
                .Time = 0
            }, New Address() With {
                .AddressString = "131 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.142036437988,
                .Longitude = -83.238845825195,
                .Time = 0
            },
                New Address() With {
                .AddressString = "138 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.14307,
                .Longitude = -83.239334,
                .Time = 0
            }, New Address() With {
                .AddressString = "139 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.142734527588,
                .Longitude = -83.237442016602,
                .Time = 0
            }, New Address() With {
                .AddressString = "145 Bill Johnson Rd NE Milledgeville GA 31061",
                .Latitude = 33.143871307373,
                .Longitude = -83.237342834473,
                .Time = 0
            }, New Address() With {
                .AddressString = "221 Blake Cir Milledgeville GA 31061",
                .Latitude = 33.081462860107,
                .Longitude = -83.208511352539,
                .Time = 0
            }}

            ' Set parameters

            Dim parameters As New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Route 10 Stops Test",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription()
            }

            Dim optimizationParameters As New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim resultResponse1 As ResultResponse = Nothing

            Try
                dataObjectSD10Stops = r4mm.RunOptimization(optimizationParameters, resultResponse1)

                SD10Stops_optimization_problem_id = dataObjectSD10Stops.OptimizationProblemId
                SD10Stops_route = If((dataObjectSD10Stops IsNot Nothing AndAlso dataObjectSD10Stops.Routes IsNot Nothing AndAlso dataObjectSD10Stops.Routes.Length > 0), dataObjectSD10Stops.Routes(0), Nothing)
                SD10Stops_route_id = If((SD10Stops_route IsNot Nothing), SD10Stops_route.RouteID, Nothing)

                Return True
            Catch ex As Exception
                Console.WriteLine("Single Driver Route 10 Stops generation failed... " + ex.Message)
                Return False
            End Try

        End Function

        Public Function SingleDriverRoundTripTest() As Boolean
            Dim route4Me As New Route4MeManagerV5(c_ApiKey)

            ' Prepare the addresses

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "754 5th Ave New York, NY 10019",
                .[Alias] = "Bergdorf Goodman",
                .IsDepot = True,
                .Latitude = 40.7636197,
                .Longitude = -73.9744388,
                .Time = 0
            }, New Address() With {
                .AddressString = "717 5th Ave New York, NY 10022",
                .[Alias] = "Giorgio Armani",
                .Latitude = 40.7669692,
                .Longitude = -73.9693864,
                .Time = 0
            }, New Address() With {
                .AddressString = "888 Madison Ave New York, NY 10014",
                .[Alias] = "Ralph Lauren Women's and Home",
                .Latitude = 40.7715154,
                .Longitude = -73.9669241,
                .Time = 0
            }, New Address() With {
                .AddressString = "1011 Madison Ave New York, NY 10075",
                .[Alias] = "Yigal Azrou'l",
                .Latitude = 40.7772129,
                .Longitude = -73.9669,
                .Time = 0
            }, New Address() With {
                .AddressString = "440 Columbus Ave New York, NY 10024",
                .[Alias] = "Frank Stella Clothier",
                .Latitude = 40.7808364,
                .Longitude = -73.9732729,
                .Time = 0
            }, New Address() With {
                .AddressString = "324 Columbus Ave #1 New York, NY 10023",
                .[Alias] = "Liana",
                .Latitude = 40.7803123,
                .Longitude = -73.9793079,
                .Time = 0
            },
                New Address() With {
                .AddressString = "110 W End Ave New York, NY 10023",
                .[Alias] = "Toga Bike Shop",
                .Latitude = 40.7753077,
                .Longitude = -73.9861529,
                .Time = 0
            }, New Address() With {
                .AddressString = "555 W 57th St New York, NY 10019",
                .[Alias] = "BMW of Manhattan",
                .Latitude = 40.7718005,
                .Longitude = -73.9897716,
                .Time = 0
            }, New Address() With {
                .AddressString = "57 W 57th St New York, NY 10019",
                .[Alias] = "Verizon Wireless",
                .Latitude = 40.7558695,
                .Longitude = -73.9862019,
                .Time = 0
            }}

            ' Set parameters

            Dim parameters As New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Round Trip",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = "1",
                .VehicleMaxDistanceMI = "10000",
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription()
            }

            Dim optimizationParameters As New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim resultResponse1 As ResultResponse = Nothing

            Try
                dataObjectSDRT = route4Me.RunOptimization(optimizationParameters, resultResponse1)

                SDRT_optimization_problem_id = dataObjectSDRT.OptimizationProblemId
                SDRT_route = If((dataObjectSDRT IsNot Nothing AndAlso dataObjectSDRT.Routes IsNot Nothing AndAlso dataObjectSDRT.Routes.Length > 0), dataObjectSDRT.Routes(0), Nothing)
                SDRT_route_id = If((SDRT_route IsNot Nothing), SDRT_route.RouteID, Nothing)
                Return True
            Catch ex As Exception
                Console.WriteLine("Single Driver Round Trip generation failed... " + ex.Message)
                Return False
            End Try

        End Function

        Public Function RemoveOptimization(optimizationProblemIDs As String()) As Boolean
            Dim route4Me As New Route4MeManagerV5(c_ApiKey)

            ' Run the query
            Dim resultResponse1 As ResultResponse = Nothing

            Try
                Dim removed As Boolean = route4Me.RemoveOptimization(optimizationProblemIDs, resultResponse1)
                Return removed
            Catch ex As Exception
                Console.WriteLine("Removing of an optimization failed... " + ex.Message)
                Return False
                Throw
            End Try

        End Function

        Public Function MultipleDepotMultipleDriverWith24StopsTimeWindowTest() As Boolean
            Dim route4Me As New Route4MeManagerV5(c_ApiKey)

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
            },
                New Address() With {
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
            },
                New Address() With {
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
            },
                New Address() With {
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

            Dim parameters As New RouteParameters() With {
                .AlgorithmType = AlgorithmType.CVRP_TW_MD,
                .RouteName = "Multiple Depot, Multiple Driver with 24 Stops, Time Window",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = "5",
                .VehicleMaxDistanceMI = "10000",
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription(),
                .Metric = Metric.Matrix
            }

            Dim optimizationParameters As New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim resultResponse1 As ResultResponse = Nothing

            Try
                dataObjectMDMD24 = route4Me.RunOptimization(optimizationParameters, resultResponse1)

                MDMD24_route_id = If((dataObjectMDMD24 IsNot Nothing AndAlso dataObjectMDMD24.Routes IsNot Nothing AndAlso dataObjectMDMD24.Routes.Length > 0), dataObjectMDMD24.Routes(0).RouteID, Nothing)
                MDMD24_optimization_problem_id = dataObjectMDMD24.OptimizationProblemId
                MDMD24_route = If((dataObjectMDMD24 IsNot Nothing AndAlso dataObjectMDMD24.Routes IsNot Nothing AndAlso dataObjectMDMD24.Routes.Length > 0), dataObjectMDMD24.Routes(0), Nothing)
                MDMD24_route_id = If((MDMD24_route IsNot Nothing), MDMD24_route.RouteID, Nothing)

                Return True
            Catch ex As Exception
                Console.WriteLine("Generation of the Multiple Depot, Multiple Driver with 24 Stops optimization problem failed. " + ex.Message)
                Return False
            End Try

        End Function

        Public Function RemoveAddressBookContacts(lsRemLocations As List(Of String), ApiKey As String) As Boolean
            Dim route4Me As New Route4MeManagerV5(ApiKey)

            If lsRemLocations.Count > 0 Then
                Dim resultResponse1 As ResultResponse = Nothing

                Dim removed As Boolean = route4Me.RemoveAddressBookContacts(lsRemLocations.ToArray(), resultResponse1)

                Return removed
            Else
                Return False
            End If
        End Function

    End Class

End Namespace



