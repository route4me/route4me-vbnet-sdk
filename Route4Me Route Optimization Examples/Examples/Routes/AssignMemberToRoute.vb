Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Assign a team member to a route.
        ''' </summary>
        Public Sub AssignMemberToRoute()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing
            Dim members = route4Me.GetUsers(New GenericParameters(), errorString)

            Dim randomNumber As Integer = (New Random()).[Next](0, members.results.Length - 1)
            Dim memberId = If(
                members.results(randomNumber).member_id IsNot Nothing,
                Convert.ToInt32(members.results(randomNumber).member_id),
                -1
            )

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeId As String = SD10Stops_route_id

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = routeId,
                .Parameters = New RouteParameters() With {
                    .MemberId = memberId
                }
            }

            Dim updatedRoute = route4Me.UpdateRoute(routeParameters, errorString)

            PrintExampleRouteResult(updatedRoute, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace