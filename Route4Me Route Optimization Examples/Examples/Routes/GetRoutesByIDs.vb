Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get the routes by list of the route IDs.
        ''' </summary>
        Public Sub GetRoutesByIDs()
            Dim route4Me = New Route4MeManager(ActualApiKey)

#Region "Retrieve first 3 routes"

            Dim routesParameters = New RouteParametersQuery() With {
                .Offset = 0,
                .Limit = 3
            }

            Dim errorString As String = Nothing
            Dim threeRoutes As DataObjectRoute() = route4Me.
                GetRoutes(routesParameters, errorString)

#End Region

#Region "Retrieve 2 route by their IDs"
            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = threeRoutes(0).RouteID & "," + threeRoutes(1).RouteID
            }

            Dim twoRoutes = route4Me.GetRoutes(routeParameters, errorString)

            PrintExampleRouteResult(twoRoutes, errorString)
#End Region
        End Sub
    End Class
End Namespace