Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        Public Sub ReoptimizeRoute(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = routeId, _
                .ReOptimize = True _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObjectRoute = route4Me.UpdateRoute(routeParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("ReoptimizeRoute executed successfully")

                Console.WriteLine("Route ID: {0}", dataObject.RouteID)
            Else
                Console.WriteLine("ReoptimizeRoute error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
