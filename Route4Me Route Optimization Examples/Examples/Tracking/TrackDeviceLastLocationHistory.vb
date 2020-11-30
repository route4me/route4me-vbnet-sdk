Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of getting the last location history of a GPS device.
        ''' </summary>
        Public Sub TrackDeviceLastLocationHistory()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()
            OptimizationsToRemove = New List(Of String)()
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id)

            ' Create the GPS parameters
            Dim gpsParameters = New GPSParameters() With {
                .Format = Format.Csv.GetEnumDescription(),
                .RouteId = SD10Stops_route_id,
                .Latitude = 33.14384,
                .Longitude = -83.22466,
                .Course = 1,
                .Speed = 120,
                .DeviceType = DeviceType.IPhone.GetEnumDescription(),
                .MemberId = 1,
                .DeviceGuid = "TEST_GPS",
                .DeviceTimestamp = "2014-06-14 17:43:35"
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.SetGPS(gpsParameters, errorString)

            If Not String.IsNullOrEmpty(errorString) Then
                Console.WriteLine("SetGps error: {0}", errorString)
                Return
            End If

            Console.WriteLine("SetGps response: {0}", response.ToString())

            Dim genericParameters As GenericParameters = New GenericParameters()
            genericParameters.ParametersCollection.Add("route_id", SD10Stops_route_id)
            genericParameters.ParametersCollection.Add("device_tracking_history", "1")

            Dim dataObject = route4Me.GetLastLocation(genericParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("TrackDeviceLastLocationHistory executed successfully")
                Console.WriteLine("")
                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("")

                dataObject.TrackingHistory.ToList().ForEach(
                    Sub(th)
                        Console.WriteLine("Speed: {0}", th.Speed)
                        Console.WriteLine("Longitude: {0}", th.Longitude)
                        Console.WriteLine("Latitude: {0}", th.Latitude)
                        Console.WriteLine("Time Stamp: {0}", th.TimeStampFriendly)
                        Console.WriteLine("")
                    End Sub)
            Else
                Console.WriteLine("TrackDeviceLastLocationHistory error: {0}", errorString)
            End If

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
