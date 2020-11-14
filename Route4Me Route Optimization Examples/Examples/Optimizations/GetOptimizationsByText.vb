Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get the optimizations by specified text.
        ''' </summary>
        Public Sub GetOptimizationsByText()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)()
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id)

            Dim queryText As String = "SD Route 10 Stops Test"
            Dim queryParameters = New RouteParametersQuery() With {
                .Limit = 3,
                .Offset = 0,
                .Query = queryText
            }

            Dim errorString As String = Nothing
            Dim dataObjects = route4Me.GetOptimizations(queryParameters, errorString)

            Dim foundOptimizations As Integer = dataObjects.Where(Function(x) x.Parameters.RouteName.Contains(queryText)).Count()

            Console.WriteLine(
                If(
                    foundOptimizations > 0,
                    "Found the optimizations searched by text: " & foundOptimizations,
                    "Cannot found the optimizations searched by text"
                  )
            )

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace