Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub ReOptimization(optimizationProblemID As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim optimizationParameters As New OptimizationParameters() With { _
                .OptimizationProblemID = optimizationProblemID, _
                .ReOptimize = True _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.UpdateOptimization(optimizationParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("ReOptimization executed successfully")

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("State: {0}", dataObject.State)
            Else
                Console.WriteLine("ReOptimization error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
