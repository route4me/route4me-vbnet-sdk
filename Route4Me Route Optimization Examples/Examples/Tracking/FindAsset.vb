Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Find Asset (Asset tracking)
        ''' </summary>
        Public Sub FindAsset()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()
            OptimizationsToRemove = New List(Of String)()
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id)

            Dim tracking As String = SD10Stops_route.Addresses(1).tracking_number

            ' Run query
            Dim errorString As String = Nothing
            Dim result As FindAssetResponse = route4Me.FindAsset(tracking, errorString)

            Dim nDateTime As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("FindAsset executed successfully")
                Console.WriteLine("tracking_number: " & result.TrackingNumber)

                For Each loc1 As FindAssetResponseLocations In result.Locations
                    Console.WriteLine("lat: " & loc1.Latitude)
                    Console.WriteLine("lng: " & loc1.Longitude)
                    Console.WriteLine("icon: " & loc1.Icon)
                Next

                If (If(result?.CustomData?.Count, 0)) > 0 Then

                    For Each kvp As KeyValuePair(Of String, String) In result.CustomData
                        Console.WriteLine(kvp.Key & ": " & kvp.Value)
                    Next
                End If

                For Each arriv1 As FindAssetResponseArrival In result.Arrival
                    Console.WriteLine(
                        "from_unix_timestamp: " & nDateTime.AddSeconds(
                        If(arriv1.FromUnixTimestamp >= 0, CDbl(arriv1.FromUnixTimestamp), 0))
                    )
                    Console.WriteLine(
                        "to_unix_timestamp: " & nDateTime.AddSeconds(
                        If(arriv1.ToUnixTimestamp >= 0, CDbl(arriv1.ToUnixTimestamp), 0))
                    )
                Next

                Console.WriteLine("delivered: " & result.Delivered)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("FindAsset error: {0}", errorString)
            End If

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace