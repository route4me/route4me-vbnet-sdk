Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update route avoidance zones.
        ''' </summary>
        Public Sub UpdateRouteAvoidanceZones()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

#Region "Get Avoidance Zones"

            CreateAvoidanceZone()

            AvoidanceZonesToRemove = New List(Of String)() From {
                avoidanceZone.TerritoryId
            }

            CreateAvoidanceZone()

            AvoidanceZonesToRemove.Add(avoidanceZone.TerritoryId)

#End Region

            Dim parameters = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id,
                .Parameters = New RouteParameters() With {
                    .AvoidanceZones = New String() {
                        AvoidanceZonesToRemove(0),
                        AvoidanceZonesToRemove(1)
                     }
                }
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.UpdateRoute(parameters, errorString)

            PrintExampleRouteResult(result, errorString)

            RemoveTestOptimizations()
            RemoveAvoidanceZones()
        End Sub
    End Class
End Namespace