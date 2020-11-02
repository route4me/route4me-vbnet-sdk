Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddOrdersToRoute(rQueryParams As RouteParametersQuery, addresses As Address(), rParams As RouteParameters)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As RouteResponse = route4Me.AddOrdersToRoute(rQueryParams, addresses, rParams, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("UpdateRouteCustomData executed successfully")

                Console.WriteLine("Route ID: {0}", dataObject.RouteID)
            Else
                Console.WriteLine("UpdateRouteCustomData error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace