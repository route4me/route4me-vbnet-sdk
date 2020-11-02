Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDriverRoute7Stops() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Prepare the addresses
            '#Region "Addresses"

            'input as many custom fields as needed, custom data is passed through to mobile devices and to the manifest

            '#End Region
            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "דיזנגוף 229 תל אביב-יפו, 63,116, ישראל",
                .IsDepot = True,
                .Latitude = 32.090734,
                .Longitude = 34.775779,
                .Time = 0,
                .CustomFields = New Dictionary(Of String, String)() From {
                    {"color", "red"},
                    {"size", "huge"}
                }
            }, New Address() With {
                .AddressString = "דיזנגוף 213 תל אביב-יפו, ישראל",
                .Latitude = 32.088689,
                .Longitude = 34.775371,
                .Time = 0
            }, New Address() With {
                .AddressString = "שדרות בן גוריון 68 תל אביב-יפו, ישראל",
                .Latitude = 32.08308,
                .Longitude = 34.776894,
                .Time = 0
            }, New Address() With {
                .AddressString = "מלכי Yisra'el רחוב תל אביב-יפו, ישראל",
                .Latitude = 32.081417,
                .Longitude = 34.779909,
                .Time = 0
            }, New Address() With {
                .AddressString = "נצח ישראל רחוב 9 תל אביב-יפו, ישראל",
                .Latitude = 32.075644,
                .Longitude = 34.78331,
                .Time = 0
            }, New Address() With {
                .AddressString = "שאול שדרות המלך 1-13 תל אביב-יפו, ישראל",
                .Latitude = 32.075644,
                .Longitude = 34.78331,
                .Time = 0
            },
                New Address() With {
                .AddressString = "ארלוזורוב 88 תל אביב-יפו, ישראל",
                .Latitude = 32.085298,
                .Longitude = 34.781916,
                .Time = 0
            }}

            ' Set parameters

            Dim parameters As New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Route 7 Stops (vb.net)",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Distance),
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI),
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web)
            }

            Dim optimizationParameters As New OptimizationParameters() With { _
                .Addresses = addresses, _
                .Parameters = parameters _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.RunOptimization(optimizationParameters, errorString)

            ' Output the result
            PrintExampleOptimizationResult("SingleDriverRoute7Stops", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace