Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Mark Address as Departed
        ''' </summary>
        ''' <param name="aParams">Address parameters</param>
        Public Sub MarkAddressDeparted(ByVal Optional aParams As AddressParameters = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(aParams Is Nothing, True, False)

            Dim errorString0 As String = Nothing

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()
                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }
                Dim visitedParams = New AddressParameters With {
                    .RouteId = SD10Stops_route_id,
                    .AddressId = CInt(SD10Stops_route.Addresses(2).RouteDestinationId),
                    .IsVisited = True
                }
                Dim oVisited As Object = route4Me.MarkAddressVisited(visitedParams, errorString0)
                Dim iVisited As Integer
                Dim visited As Boolean = If(Integer.TryParse(oVisited.ToString(), iVisited), (If(Convert.ToInt32(oVisited) > 0, True, False)), False)

                If Not visited Then
                    Console.WriteLine("Cannot mark the address as visited")
                    Return
                End If

                aParams = New AddressParameters With {
                    .RouteId = SD10Stops_route_id,
                    .AddressId = CInt(SD10Stops_route.Addresses(2).RouteDestinationId),
                    .IsDeparted = True
                }
            End If

            Dim errorString As String = Nothing
            Dim result As Integer = route4Me.MarkAddressDeparted(aParams, errorString)

            Dim departed As Boolean = If(result > 0, True, False)

            PrintExampleDestination(departed, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
