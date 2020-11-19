Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get limited number of the routes.
        ''' </summary>
        Public Sub GetRoutes()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim routeParameters = New RouteParametersQuery() With {
                .Limit = 10,
                .Offset = 5
            }

            Dim errorString As String = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.
                        GetRoutes(routeParameters, errorString)

            PrintExampleRouteResult(dataObjects, errorString)
        End Sub
    End Class
End Namespace
