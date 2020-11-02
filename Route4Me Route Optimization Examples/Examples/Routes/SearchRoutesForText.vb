Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub SearchRoutesForText(query As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim routeParameters As New RouteParametersQuery() With { _
                .Query = query _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObjects As DataObjectRoute() = route4Me.GetRoutes(routeParameters, errorString)

            Console.WriteLine("")

            If dataObjects IsNot Nothing Then
                Console.WriteLine("SearchRoutesForText executed successfully, {0} routes returned", dataObjects.Length)
                Console.WriteLine("")

                For Each dataobject As DataObjectRoute In dataObjects
                    Console.WriteLine("RouteID: {0}", dataobject.RouteID)
                    Console.WriteLine("")
                Next
            Else
                Console.WriteLine("SearchRoutesForText error {0}", errorString)
            End If

        End Sub
    End Class
End Namespace