Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    ''' <summary>
    ''' Add Orders to an Optimization
    ''' </summary>
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddOrdersToOptimization(rQueryParams As OptimizationParameters, addresses As Address(), rParams As RouteParameters)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.AddOrdersToOptimization(rQueryParams, addresses, rParams, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("UpdateRouteCustomData executed successfully")

                Console.WriteLine("Optmization Problem ID: {0}", dataObject.OptimizationProblemId)
            Else
                Console.WriteLine("UpdateRouteCustomData error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
