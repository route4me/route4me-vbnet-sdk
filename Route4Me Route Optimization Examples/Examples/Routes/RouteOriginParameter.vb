Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get origin route of the duplicated route.
        ''' </summary>
        Public Sub RouteOriginParameter()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New System.Collections.Generic.List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

#Region "Duplicate Route"
            Dim duplParameters = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id
            }

            Dim errorString As String = Nothing
            Dim duplicateResult = route4Me.DuplicateRoute(duplParameters, errorString)

            If (If(duplicateResult?.RouteIDs?.Length, 0)) > 0 Then
                RoutesToRemove = New List(Of String)() From {
                    duplicateResult.RouteIDs(0)
                }
            Else
                Console.WriteLine("Cannot duplicate the route")
                Return
            End If
#End Region

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = duplicateResult.RouteIDs(0),
                .Original = True
            }

            Dim route = route4Me.GetRoute(routeParameters, errorString)

            PrintExampleRouteResult((If(route?.OriginalRoute, Nothing)), errorString)

            RemoveTestRoutes()
            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace