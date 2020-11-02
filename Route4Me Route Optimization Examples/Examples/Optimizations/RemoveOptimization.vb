Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove Optimization
        ''' </summary>
        ''' <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        Public Sub RemoveOptimization(optimizationProblemIDs As String())
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim deleted As Boolean = route4Me.RemoveOptimization(optimizationProblemIDs, errorString)

            Console.WriteLine("")

            If deleted Then
                Console.WriteLine("RemoveOptimization executed successfully")

                Console.WriteLine("Optimization Problem ID: {0}", optimizationProblemIDs)
            Else
                Console.WriteLine("RemoveOptimization error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace