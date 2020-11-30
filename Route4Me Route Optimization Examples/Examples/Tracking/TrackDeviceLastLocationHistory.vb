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

            Dim tsp2days = New TimeSpan(2, 0, 0, 0)
            Dim dtNow As DateTime = DateTime.Now

            RunOptimizationSingleDriverRoute10Stops()
            OptimizationsToRemove = New List(Of String)()
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id)

            Dim lat As Double = If(SD10Stops_route.Addresses.Length > 1, SD10Stops_route.Addresses(1).Latitude, 33.14384)
            Dim lng As Double = If(SD10Stops_route.Addresses.Length > 1, SD10Stops_route.Addresses(1).Longitude, -83.22466)

            Dim gpsParameters = New GPSParameters With {
                .Format = Format.Csv.GetEnumDescription(),
                .RouteId = SD10Stops_route_id,
                .Latitude = lat,
                .Longitude = lng,
                .Course = 1,
                .Speed = 120,
                .DeviceType = DeviceType.IPhone.GetEnumDescription(),
                .MemberId = CInt(SD10Stops_route.Addresses(1).MemberId),
                .DeviceGuid = "TEST_GPS",
                .DeviceTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.SetGPS(gpsParameters, errorString)

            If Not String.IsNullOrEmpty(errorString) Then
                Console.WriteLine("SetGps error: {0}", errorString)
                Return
            End If

            Console.WriteLine("SetGps response: {0}", response.Status.ToString())

            Dim trParameters = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id,
                .DeviceTrackingHistory = True
            }

            Dim dataObject = route4Me.GetLastLocation(trParameters, errorString)

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
