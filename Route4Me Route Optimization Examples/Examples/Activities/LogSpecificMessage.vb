Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Create User Activity
        ''' </summary>
        Public Sub LogCustomActivity()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeId As String = SD10Stops_route_id

            Dim activity = New Activity() With {
                .ActivityType = "user_message",
                .ActivityMessage = "Test User Activity " & DateTime.Now.ToString(),
                .RouteId = routeId
            }

            Dim errorString As String = Nothing
            Dim added As Boolean = route4Me.LogCustomActivity(activity, errorString)

            Console.WriteLine("")

            If added Then
                Console.WriteLine("LogCustomActivity executed successfully")
            Else
                Console.WriteLine("LogCustomActivity error: {0}", errorString)
            End If

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace