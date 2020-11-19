Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Share a route by email.
        ''' </summary>
        Public Sub ShareRoute()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_route_id
            }

            Dim routeId As String = SD10Stops_route_id
            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = routeId,
                .ResponseFormat = "json"
            }

            Dim email As String = "regression.autotests+testcsharp123@gmail.com"

            ' Run the query
            Dim errorString As String = Nothing
            Dim result = route4Me.RouteSharing(routeParameters, email, errorString)

            Console.WriteLine(
                If(
                    result,
                    String.Format("The route {0} shared successfully", routeId),
                    String.Format("Cannot share the route." & Environment.NewLine & errorString)
                )
            )

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace