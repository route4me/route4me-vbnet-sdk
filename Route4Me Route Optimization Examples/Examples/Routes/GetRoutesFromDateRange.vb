Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Gets the routes from specified date range.
        ''' </summary>
        Public Sub GetRoutesFromDateRange()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim t10days As TimeSpan = New TimeSpan(10, 0, 0, 0)
            Dim dtNow As DateTime = DateTime.Now

            Dim routeParameters = New RouteParametersQuery() With {
                .StartDate = (dtNow - t10days).ToString("yyyy-MM-dd"),
                .EndDate = dtNow.ToString("yyyy-MM-dd")
            }

            Dim errorString As String = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.
                GetRoutes(routeParameters, errorString)

            PrintExampleRouteResult(dataObjects, errorString)
        End Sub
    End Class
End Namespace