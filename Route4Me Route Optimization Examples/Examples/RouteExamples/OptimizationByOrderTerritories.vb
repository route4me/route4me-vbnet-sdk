Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples

        Public Sub OptimizationByOrderTerritories()

            Dim route4Me = New Route4MeManager(ActualApiKey)

            ' Set parameters
            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.CVRP_TW_SD,
                .RouteName = "Optimization by order territories, " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                .is_dynamic_start_time = False,
                .Optimize = "Time",
                .IgnoreTw = False,
                .Parts = 10,
                .RT = False,
                .LockLast = False,
                .DisableOptimization = False,
                .VehicleId = ""
            }

            Dim orderTerritories = New OrderTerritories() With {
                .SplitTerritories = True,
                .TerritoriesId = New String() {"5E66A5AFAB087B08E690DFAE4F8B151B", "6160CFC4CC3CD508409D238E04D6F6C4"},
                .filters = New FilterDetails() With {
                    .Display = "all",
                    .Scheduled_for_YYYYMMDD = New String() {"2021-09-21"}
                }
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Redirect = False,
                .OrderTerritories = orderTerritories,
                .Parameters = parameters
            }

            Dim errorString As String = Nothing
            Dim dataObjects = route4Me.RunOptimizationByOrderTerritories(optimizationParameters, errorString)

            If (If(dataObjects?.Length, 0)) > 0 Then
                OptimizationsToRemove = New List(Of String)()

                For Each dataObject In dataObjects
                    If dataObject.OptimizationProblemId IsNot Nothing Then OptimizationsToRemove.Add(dataObject.OptimizationProblemId)
                    Console.WriteLine($"Optimization Problem ID: {dataObject.OptimizationProblemId}")
                Next
            Else
                Console.WriteLine($"Optimization failed. {errorString}")
            End If

            RemoveTestOptimizations()

        End Sub

    End Class

End Namespace