Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove Optimization
        ''' </summary>
        ''' <param name="optimizationProblemIDs"> Optimization Problem IDs </param>
        Public Sub RemoveOptimization(Optional optimizationProblemIDs As String() = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(optimizationProblemIDs Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()
                optimizationProblemIDs = New String() {SD10Stops_optimization_problem_id}
            End If

            Dim errorString As String = Nothing
            Dim removed As Boolean = route4Me.RemoveOptimization(optimizationProblemIDs, errorString)

            Console.WriteLine("")

            If removed Then
                Console.WriteLine("RemoveOptimization executed successfully")

                For Each optid As String In optimizationProblemIDs
                    Console.WriteLine("Removed Optimization Problem ID: {0}", optid)
                Next
            Else
                Console.WriteLine("RemoveOptimization error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace