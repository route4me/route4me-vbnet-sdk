Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Unlink a route from an optimization.
        ''' </summary>
        Public Sub UnlinkRouteFromOptimization()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

#Region "Duplicate the route"

            Dim routeDuplicateParameters = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id
            }

            Dim errorString As String = Nothing
            Dim duplicatedRouteId = route4Me.
                DuplicateRoute(routeDuplicateParameters, errorString)

            If duplicatedRouteId Is Nothing Then
                Console.WriteLine("Cannot duplicate the route")
                Return
            End If

            Dim duplicatedRoute = route4Me.GetRoute(New RouteParametersQuery() With {
                .RouteId = duplicatedRouteId
            }, errorString)

            If duplicatedRoute Is Nothing AndAlso
                duplicatedRoute.[GetType]() <> GetType(DataObjectRoute) Then

                Console.WriteLine("Cannot retrieve the duplicated route.")

                Return
            End If

            RoutesToRemove = New List(Of String)() From {
                duplicatedRouteId
            }

#End Region

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = duplicatedRouteId,
                .UnlinkFromMasterOptimization = True
            }

            Dim unlinkedRoute = route4Me.UpdateRoute(routeParameters, errorString)

            PrintExampleRouteResult(unlinkedRoute, errorString)

            RemoveTestRoutes()
            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace