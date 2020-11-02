Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub TrackDeviceLastLocationHistory(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Create the gps parametes
            Dim gpsParameters As New GPSParameters() With { _
                .Format = EnumHelper.GetEnumDescription(Format.Csv), _
                .RouteId = routeId, _
                .Latitude = 33.14384, _
                .Longitude = -83.22466, _
                .Course = 1, _
                .Speed = 120, _
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.IPhone), _
                .MemberId = 1, _
                .DeviceGuid = "TEST_GPS", _
                .DeviceTimestamp = "2014-06-14 17:43:35" _
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
                Console.WriteLine("TrackDeviceLastLocationHistory executed successfully")
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
                Console.WriteLine("TrackDeviceLastLocationHistory error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
