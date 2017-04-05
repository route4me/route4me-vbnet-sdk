Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub DeleteRoutes(routeIds As String())
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            'routeIds = new string[] { "1" }

            ' Run the query
            Dim errorString As String = ""
            Dim deletedRouteIds As String() = route4Me.DeleteRoutes(routeIds, errorString)

            Console.WriteLine("")

            If deletedRouteIds IsNot Nothing Then
                Console.WriteLine("DeleteRoutes executed successfully, {0} routes deleted", deletedRouteIds.Length)
                Console.WriteLine("")
            Else
                Console.WriteLine("DeleteRoutes error {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
