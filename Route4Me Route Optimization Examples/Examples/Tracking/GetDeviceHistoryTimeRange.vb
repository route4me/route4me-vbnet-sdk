Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManager

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Device History from Time Range
        ''' </summary>
        Public Sub GetDeviceHistoryTimeRange()
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

            Dim trParameters = New GPSParameters With {
                .Format = "json",
                .RouteId = SD10Stops_route_id,
                .TimePeriod = "custom",
                .StartDate = R4MeUtils.ConvertToUnixTimestamp(dtNow - tsp2days),
                .EndDate = R4MeUtils.ConvertToUnixTimestamp(dtNow + tsp2days)
            }

            Dim result = route4Me.GetDeviceLocationHistory(trParameters, errorString)

            Console.WriteLine(If(
                              result IsNot Nothing AndAlso result.[GetType]() = GetType(DeviceLocationHistoryResponse),
                              "GetDeviceHistoryTimeRangeTest executed successfully",
                              "GetDeviceHistoryTimeRangeTest failed. " & errorString))

            If result IsNot Nothing AndAlso result.[GetType]() = GetType(DeviceLocationHistoryResponse) Then
                Console.WriteLine("")
                Dim locationHistoryResul = CType(result, DeviceLocationHistoryResponse)

                If (If(locationHistoryResul.data?.Length, 0)) > 0 Then

                    For Each locationHistory In locationHistoryResul.data
                        Console.WriteLine("Location: {0}, {1}", locationHistory.Latitude, locationHistory.Longitude)
                    Next
                End If
            End If

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace