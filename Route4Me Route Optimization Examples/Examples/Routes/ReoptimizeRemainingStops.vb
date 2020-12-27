Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    ''' <summary>
    ''' Reoptimize non visited addresses of the active route
    ''' </summary>
    Partial Public NotInheritable Class Route4MeExamples

        Public Sub ReoptimizeRemainingStops()
            Dim route4Me = New Route4MeManager(ActualApiKey)

#Region "== Prepare Test Data =="

            RunSingleDriverRoundTrip()
            OptimizationsToRemove = New List(Of String)() From {
                SDRT_optimization_problem_id
            }

            Dim route = SDRT_route

#End Region

#Region "== Visit Second Address =="
            Dim visitedParams = New AddressParameters With {
                .RouteId = route.RouteID,
                .AddressId = CInt(route.Addresses(1).RouteDestinationId),
                .IsVisited = True
            }

            Dim errorString As String = Nothing
            Dim result As Integer = route4Me.MarkAddressVisited(visitedParams, errorString)

            If result = 1 Then
                Console.WriteLine("The address " & visitedParams.AddressId & " visited")
            Else
                Console.WriteLine("Cannot visit the address " & visitedParams.AddressId)
                Return
            End If

#End Region

#Region "== Visit Third Address =="
            visitedParams = New AddressParameters With {
                .RouteId = route.RouteID,
                .AddressId = CInt(route.Addresses(2).RouteDestinationId),
                .IsVisited = True
            }

            result = route4Me.MarkAddressVisited(visitedParams, errorString)

            If result = 1 Then
                Console.WriteLine("The address " & visitedParams.AddressId & " visited")
            Else
                Console.WriteLine("Cannot visit the address " & visitedParams.AddressId)
                Return
            End If

#End Region

#Region "== Reoptimize Remaining Addresses =="
            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = route.RouteID,
                .ReOptimize = True,
                .Remaining = True
            }

            Dim updatedRoute = route4Me.UpdateRoute(routeParameters, errorString)

            PrintExampleRouteResult(updatedRoute, errorString)

#End Region

            RemoveTestOptimizations()
        End Sub

    End Class
End Namespace


