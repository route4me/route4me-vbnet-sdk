Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of setting the GPS position of a device.
        ''' </summary>
        Public Sub SetGPSPosition()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()
            OptimizationsToRemove = New List(Of String)()
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id)

            Dim lat As Double = If(SD10Stops_route.Addresses.Length > 1, SD10Stops_route.Addresses(1).Latitude, 33.14384)
            Dim lng As Double = If(SD10Stops_route.Addresses.Length > 1, SD10Stops_route.Addresses(1).Longitude, -83.22466)

            ' Create the gps parameters
            Dim gpsParameters = New GPSParameters() With {
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

            ' Run query
            Dim errorString As String = Nothing
            Dim response = route4Me.SetGPS(gpsParameters, errorString)

            Console.WriteLine("")
            Console.WriteLine(If(
                              String.IsNullOrEmpty(errorString),
                              String.Format("SetGps response: {0}", response.ToString()),
                              String.Format("SetGps error: {0}", errorString))
            )

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
