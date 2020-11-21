Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of creating an optimization 
        ''' with 10 stops And single-driver option.
        ''' </summary>
        Public Sub SingleDriverRoute10Stops()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

#Region "Prepare the addresses"

            'indicate that this is a departure stop
            'single depot routes can only have one departure depot 

            'required coordinates for every departure and stop on the route

            'the expected time on site, in seconds. this value is incorporated into the optimization engine
            'it also adjusts the estimated and dynamic eta's for a route

            'input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest

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
            }, New Address() With {
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

#End Region

            ' Set parameters
            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Route 10 Stops",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription()
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim dataObject As DataObject = route4Me.RunOptimization(optimizationParameters, errorString)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            PrintExampleOptimizationResult(dataObject, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
