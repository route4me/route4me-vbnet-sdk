Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDriverRoute_UTF8_strings() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the addresses

            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "דיזנגוף 229 תל אביב-יפו, 63,116, ישראל", _
                .IsDepot = True, _
                .Latitude = 32.090734, _
                .Longitude = 34.775779, _
                .Time = 0, _
                .CustomFields = New Dictionary(Of String, String)() From { _
                    {"color", "red"}, _
                    {"size", "huge"} _
                } _
            }, New Address() With { _
                .AddressString = "דיזנגוף 213 תל אביב-יפו, ישראל", _
                .Latitude = 32.088689, _
                .Longitude = 34.775371, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "שדרות בן גוריון 68 תל אביב-יפו, ישראל", _
                .Latitude = 32.08308, _
                .Longitude = 34.776894, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "מלכי Yisra'el רחוב תל אביב-יפו, ישראל", _
                .Latitude = 32.081417, _
                .Longitude = 34.779909, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "נצח ישראל רחוב 9 תל אביב-יפו, ישראל", _
                .Latitude = 32.075644, _
                .Longitude = 34.78331, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "שאול שדרות המלך 1-13 תל אביב-יפו, ישראל", _
                .Latitude = 32.075644, _
                .Longitude = 34.78331, _
                .Time = 0 _
            }, _
                New Address() With { _
                .AddressString = "ארלוזורוב 88 תל אביב-יפו, ישראל", _
                .Latitude = 32.085298, _
                .Longitude = 34.781916, _
                .Time = 0 _
            }}

            ' Set parameters

            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.TSP, _
                .StoreRoute = False, _
                .RouteName = "כביש נהג יחיד 7 מפסיקים (vb.net)", _
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
            Dim sOptimizationProblemID As String
            Dim route As DataObjectRoute

            If dataObject IsNot Nothing Then
                route = dataObject.Routes(0)

                Dim parametersNew As New RouteParameters() With { _
                    .RouteName = "כביש נהג יחיד 7 מפסיקים (vb.net)" _
                }

                Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = route.RouteID, _
                .Parameters = parametersNew _
                }

                Dim routeRanamed As DataObjectRoute = route4Me.UpdateRoute(routeParameters, errorString)
                Console.WriteLine("SingleDriverRoute_UTF8_strings")
                Return routeRanamed
            Else
                Console.WriteLine("Route geneation was failed")
            End If
            ' Output the result


            Return dataObject
        End Function
    End Class
End Namespace