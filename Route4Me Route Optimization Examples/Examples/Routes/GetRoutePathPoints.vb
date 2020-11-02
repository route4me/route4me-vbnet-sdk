Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetRoutePathPoints(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = routeId, _
                .RoutePathOutput = "Points"
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("GetRoutePathPoints executed successfully")

                Console.WriteLine("Route ID: {0}", dataObject.RouteID)
            Else
                Console.WriteLine("GetRoutePathPoints error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
