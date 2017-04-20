Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        Public Sub UpdateRouteCustomData(routeId As String, routeDestionationId As Integer, CustomData As Dictionary(Of String, String))
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = routeId, _
                .RouteDestinationId = routeDestionationId
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As Address = route4Me.UpdateRouteCustomData(routeParameters, CustomData, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("UpdateRouteCustomData executed successfully")

                Console.WriteLine("Route ID: {0}", dataObject.RouteId)
                Console.WriteLine("Route Destination ID: {0}", dataObject.RouteDestinationId)
            Else
                Console.WriteLine("UpdateRouteCustomData error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace