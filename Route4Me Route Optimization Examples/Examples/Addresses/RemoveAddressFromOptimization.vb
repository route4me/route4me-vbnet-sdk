Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove a destination from an optimization.
        ''' </summary>
        ''' <param name="optimizationId">Optimization ID</param>
        ''' <param name="destinationId">Destination ID</param>
        ''' <param name="andReOptimize">If true, re-optimize an optimization </param>
        Public Sub RemoveDestinationFromOptimization(
                    ByVal Optional optimizationId As String = Nothing,
                    ByVal Optional destinationId As Integer? = Nothing,
                    ByVal Optional andReOptimize As Boolean? = Nothing)

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(optimizationId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                optimizationId = SD10Stops_optimization_problem_id

                destinationId = CInt(SD10Stops_route.Addresses(2).RouteDestinationId)

                andReOptimize = True
            End If

            Dim errorString As String = Nothing
            Dim removed As Boolean = route4Me.RemoveDestinationFromOptimization(optimizationId, CInt(destinationId), errorString)

            Console.WriteLine("")

            If removed Then
                Console.WriteLine("RemoveAddressFromOptimization executed successfully")
                Console.WriteLine("Optimization Problem ID: {0}, Destination ID: {1}", optimizationId, destinationId)
            Else
                Console.WriteLine("RemoveAddressFromOptimization error: {0}", errorString)
            End If

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
