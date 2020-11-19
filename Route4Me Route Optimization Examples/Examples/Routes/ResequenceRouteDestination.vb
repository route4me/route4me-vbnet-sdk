Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Resequence Route Destination
        ''' </summary>
        Public Sub ResequenceRouteDestination()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim route = SD10Stops_route
            Dim rParams = New RouteParametersQuery() With {
                .RouteId = route.RouteID
            }

            Dim lsAddresses = New List(Of Address)()

            Dim address1 = route.Addresses(2)
            Dim address2 = route.Addresses(3)

            address1.SequenceNo = 4
            address2.SequenceNo = 3

            lsAddresses.Add(address1)
            lsAddresses.Add(address2)

            Dim errorString As String = Nothing
            Dim route1 = route4Me.ManuallyResequenceRoute(
                rParams,
                lsAddresses.ToArray(),
                errorString
            )

            PrintExampleRouteResult(route1, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace