Imports Route4MeSDKLibrary
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples

        ''' <summary>
        ''' Resequence and Reoptimze a Route
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        Public Sub ResequenceReoptimizeRoute(ByVal Optional routeId As String = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)
            Dim isInnerExample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()

                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }

                routeId = SD10Stops_route_id
            End If

            Dim queryParameters = New RouteParametersQuery() With {
                .RouteId = routeId,
                .DisableOptimization = False,
                .Optimize = Optimize.Distance.GetEnumDescription()
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.ReoptimizeRoute(queryParameters, errorString)

            Console.WriteLine(
                If(
                result IsNot Nothing AndAlso result.[GetType]() = GetType(DataTypes.DataObjectRoute),
                "ResequenceReoptimizeRoute executed successfully",
                String.Format("ResequenceReoptimizeRoute error: {0}", errorString))
             )

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
