Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Device History from Time Range
        ''' </summary>
        Public Sub GetDeviceHistoryTimeRange()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim zeroTime As DateTime = New DateTime(1970, 1, 1, 0, 0, 0)
            Dim uStartTime As Integer = CInt((New DateTime(2016, 10, 20, 0, 0, 0) - zeroTime).TotalSeconds)
            Dim uEndTime As Integer = CInt((New DateTime(2026, 10, 26, 23, 59, 59) - zeroTime).TotalSeconds)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)()
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id)

            Dim gpsParameters = New GPSParameters With {
                .Format = "csv",
                .RouteId = SD10Stops_route_id,
                .TimePeriod = "custom",
                .StartDate = uStartTime,
                .EndDate = uEndTime
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.SetGPS(gpsParameters, errorString)

            If Not String.IsNullOrEmpty(errorString) Then
                Console.WriteLine("SetGps error: {0}", errorString)
                Return
            End If

            Console.WriteLine("SetGps response: {0}", response.Status.ToString())

            Dim genericParameters As GenericParameters = New GenericParameters()
            genericParameters.ParametersCollection.Add("route_id", SD10Stops_route_id)
            genericParameters.ParametersCollection.Add("device_tracking_history", "1")

            Dim dataObject = route4Me.GetLastLocation(genericParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("GetDeviceHistoryTimeRange executed successfully")
                Console.WriteLine("")
                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("")

                For Each th As TrackingHistory In dataObject.TrackingHistory
                    Console.WriteLine("Speed: {0}", th.Speed)
                    Console.WriteLine("Longitude: {0}", th.Longitude)
                    Console.WriteLine("Latitude: {0}", th.Latitude)
                    Console.WriteLine("Time Stamp: {0}", th.TimeStampFriendly)
                    Console.WriteLine("")
                Next
            Else
                Console.WriteLine("GetDeviceHistoryTimeRange error: {0}", errorString)
            End If

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace