Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub MoveDestinationToRoute(toRouteId As String, routeDestinationId As Integer, afterDestinationId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim success As Boolean = route4Me.MoveDestinationToRoute(toRouteId, routeDestinationId, afterDestinationId, errorString)

            Console.WriteLine("")

            If success Then
                Console.WriteLine("MoveDestinationToRoute executed successfully")

                Console.WriteLine("Destination {0} moved to Route {1} after Destination {2}", routeDestinationId, toRouteId, afterDestinationId)
            Else
                Console.WriteLine("MoveDestinationToRoute error: {0}", errorString)
            End If

        End Sub
    End Class
End Namespace

