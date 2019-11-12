Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetDeviceHistoryTimeRange(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Create the gps parametes
            Dim uStartTime As Integer
            Dim uEndTime As Integer
            uStartTime = ((New DateTime(2016, 10, 20, 0, 0, 0)) - (New DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds
            uEndTime = ((New DateTime(2016, 10, 26, 23, 59, 59)) - (New DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds

            Dim gpsParameters As New GPSParameters() With { _
                .Format = EnumHelper.GetEnumDescription(Format.Csv), _
                .RouteId = routeId, _
                .time_period = "custom", _
                .start_date = uStartTime, _
                .end_date = uEndTime _
            }

            Dim errorString As String = ""
            Dim response As String = route4Me.SetGPS(gpsParameters, errorString)

            If Not String.IsNullOrEmpty(errorString) Then
                Console.WriteLine("SetGps error: {0}", errorString)
                Return
            End If

            Console.WriteLine("SetGps response: {0}", response)

            Dim genericParameters As New GenericParameters()
            genericParameters.ParametersCollection.Add("route_id", routeId)
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
        End Sub
    End Class
End Namespace