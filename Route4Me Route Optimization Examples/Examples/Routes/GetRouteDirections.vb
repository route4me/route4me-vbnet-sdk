Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetRouteDirections(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = routeId, _
                .Directions = True
            }

            ' Run the query
            Dim errorString As String = ""
            Dim routeResponse As RouteResponse = route4Me.GetRouteDirections(routeParameters, errorString)

            Console.WriteLine("")

            If routeResponse IsNot Nothing Then
                Console.WriteLine("GetRouteDirections executed successfully")

                Console.WriteLine("Route ID: {0}", routeResponse.RouteID)

                Console.WriteLine("Total directions: {0}", routeResponse.Directions.Count())
            Else
                Console.WriteLine("GetRouteDirections error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
