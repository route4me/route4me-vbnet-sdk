Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetOptimizations()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim queryParameters As New RouteParametersQuery() With { _
                .Limit = 10, _
                .Offset = 5 _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObjects As DataObject() = route4Me.GetOptimizations(queryParameters, errorString)

            Console.WriteLine("")

            If dataObjects IsNot Nothing Then
                Console.WriteLine("GetOptimizations executed successfully, {0} optimizations returned", dataObjects.Length)
                Console.WriteLine("")

                For Each optimization As DataObject In dataObjects
                    Console.WriteLine("Optimization Problem ID: {0}", optimization.OptimizationProblemId)
                    Console.WriteLine("")
                Next

            Else
                Console.WriteLine("GetOptimizations error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
