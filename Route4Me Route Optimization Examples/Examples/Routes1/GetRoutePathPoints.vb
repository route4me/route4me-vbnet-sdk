Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetRoutePathPoints(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = routeId, _
                .RoutePathOutput = "Points"
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("GetRoute executed successfully")

                Console.WriteLine("Route ID: {0}", dataObject.RouteID)
                'foreach (Address a in dataObject.Addresses)
                '        {
                '          Console.WriteLine("addr: {0}, {1}, {2}, {3}, {4}", a.RouteDestinationId, a.Latitude, a.Longitude, a.Alias, a.AddressString);
                '        }

                Console.WriteLine("State: {0}", dataObject.State)
            Else
                Console.WriteLine("GetRoute error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
