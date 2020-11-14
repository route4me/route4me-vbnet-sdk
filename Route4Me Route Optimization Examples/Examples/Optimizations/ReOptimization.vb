Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Re-optimize an optimization
        ''' </summary>
        ''' <param name="optimizationProblemID">Optimization problem ID</param>
        Public Sub ReOptimization(Optional optimizationProblemID As String = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(optimizationProblemID Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()
                optimizationProblemID = SD10Stops_optimization_problem_id
                OptimizationsToRemove = New List(Of String)()
                OptimizationsToRemove.Add(optimizationProblemID)
            End If

            Dim optimizationParameters = New OptimizationParameters() With {
                .OptimizationProblemID = optimizationProblemID,
                .ReOptimize = True
            }

            Dim errorString As String = Nothing
            Dim dataObject As DataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                errorString)

            PrintExampleOptimizationResult(dataObject, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
