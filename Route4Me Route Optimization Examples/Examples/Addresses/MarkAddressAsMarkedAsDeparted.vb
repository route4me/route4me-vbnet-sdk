Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Mark Address as Marked as Departed
        ''' </summary>
        ''' <param name="aParams">Address parameters</param>
        Public Sub MarkAddressAsMarkedAsDeparted(ByVal Optional aParams As AddressParameters = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(aParams Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()
                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }
                aParams = New AddressParameters With {
                    .RouteId = SD10Stops_route_id,
                    .RouteDestinationId = CInt(SD10Stops_route.Addresses(2).RouteDestinationId),
                    .IsDeparted = True
                }
            End If

            Dim errorString As String = Nothing
            Dim resultAddress As Address = route4Me.MarkAddressAsMarkedAsDeparted(aParams, errorString)

            PrintExampleDestination(resultAddress, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace