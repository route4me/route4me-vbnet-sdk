Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Find Asset (Asset tracking)
        ''' </summary>
        Public Sub FindAsset()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)
            Dim tracking As String = "Q7G9P1L9"
            ' Run the query
            Dim errorString As String = ""
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

                For Each kvp As KeyValuePair(Of String, String) In result.CustomData
                    Console.WriteLine(kvp.Key & ": " & kvp.Value)
                Next

                For Each arriv1 As FindAssetResponseArrival In result.Arrival
                    Console.WriteLine("from_unix_timestamp: " & nDateTime.AddSeconds(arriv1.FromUnixTimestamp))
                    Console.WriteLine("to_unix_timestamp: " & nDateTime.AddSeconds(arriv1.ToUnixTimestamp))
                Next
                Console.WriteLine("delivered: " & result.Delivered)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("FindAsset error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace