﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Mark Address as Visited
        ''' </summary>
        ''' <param name="aParams">Address parameters</param>
        Public Sub MarkAddressVisited(ByVal Optional aParams As AddressParameters = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(aParams Is Nothing, True, False)

            If isInnerExample Then
                RunOptimizationSingleDriverRoute10Stops()
                OptimizationsToRemove = New List(Of String)() From {
                    SD10Stops_optimization_problem_id
                }
                aParams = New AddressParameters With {
                    .RouteId = SD10Stops_route_id,
                    .AddressId = CInt(SD10Stops_route.Addresses(2).RouteDestinationId),
                    .IsVisited = True
                }
            End If

            Dim errorString As String = Nothing
            Dim oResult As Object = route4Me.MarkAddressVisited(aParams, errorString)
            Dim iResult As Integer

            Dim marked As Boolean = If(Integer.TryParse(oResult.ToString(), iResult), (If(Convert.ToInt32(oResult) > 0, True, False)), False)

            PrintExampleDestination(marked, errorString)

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
