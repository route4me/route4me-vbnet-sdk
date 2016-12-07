Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function DuplicateRoute(routeId As String) As String
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = routeId _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim duplicatedRouteId As String = route4Me.DuplicateRoute(routeParameters, errorString)

            Console.WriteLine("")

            If duplicatedRouteId IsNot Nothing Then
                Console.WriteLine("DuplicateRoute executed successfully, duplicated route ID: {0}", duplicatedRouteId)
                Console.WriteLine("")
            Else
                Console.WriteLine("DuplicateRoute error {0}", errorString)
            End If

            Return duplicatedRouteId
        End Function
    End Class
End Namespace