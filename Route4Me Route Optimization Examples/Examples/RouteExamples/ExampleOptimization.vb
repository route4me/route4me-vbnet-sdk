﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub ExampleOptimization()
            Dim Route4Me = New Route4MeManager(ActualApiKey)

            Dim dateWhenTheRouteStart = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1))

            Dim addresses = New List(Of Address)() From {
                New Address() With {
                    .AddressString = "754 5th Ave New York, NY 10019",
                    .[Alias] = "Bergdorf Goodman",
                    .IsDepot = True,
                    .Latitude = 40.7636197,
                    .Longitude = -73.9744388,
                    .Time = 0
                },
                New Address() With {
                    .AddressString = "717 5th Ave New York, NY 10022",
                    .[Alias] = "Giorgio Armani",
                    .Latitude = 40.7669692,
                    .Longitude = -73.9693864,
                    .Time = 0
                },
                New Address() With {
                    .AddressString = "888 Madison Ave New York, NY 10014",
                    .[Alias] = "Ralph Lauren Women's and Home",
                    .Latitude = 40.7715154,
                    .Longitude = -73.9669241,
                    .Time = 0
                },
                New Address() With {
                    .AddressString = "1011 Madison Ave New York, NY 10075",
                    .[Alias] = "Yigal Azrou'l",
                    .Latitude = 40.7772129,
                    .Longitude = -73.9669,
                    .Time = 0
                },
                New Address() With {
                    .AddressString = "440 Columbus Ave New York, NY 10024",
                    .[Alias] = "Frank Stella Clothier",
                    .Latitude = 40.7808364,
                    .Longitude = -73.9732729,
                    .Time = 0
                },
                New Address() With {
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
                },
                New Address() With {
                    .AddressString = "555 W 57th St New York, NY 10019",
                    .[Alias] = "BMW of Manhattan",
                    .Latitude = 40.7718005,
                    .Longitude = -73.9897716,
                    .Time = 0
                },
                New Address() With {
                    .AddressString = "57 W 57th St New York, NY 10019",
                    .[Alias] = "Verizon Wireless",
                    .Latitude = 40.7558695,
                    .Longitude = -73.9862019,
                    .Time = 0
                }
            }

            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Round Trip Customer Case",
                .RouteDate = dateWhenTheRouteStart,
                .RouteTime = 60 * 60 * 7,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription()
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses.ToArray(),
                .Parameters = parameters
            }

            Dim errorString As String = Nothing
            Dim dataObject = Route4Me.RunOptimization(optimizationParameters, errorString)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            Console.WriteLine(
                If(
                    dataObject IsNot Nothing,
                    "Optimization Problem ID = " & dataObject.OptimizationProblemId,
                    "ExampleOptimization failed"
                )
            )

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
