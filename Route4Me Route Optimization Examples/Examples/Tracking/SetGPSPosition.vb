Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub SetGPSPosition(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Create the gps parametes
            Dim gpsParameters As New GPSParameters() With { _
                .Format = Format.Csv, _
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

            Console.WriteLine("")

            If String.IsNullOrEmpty(errorString) Then
                Console.WriteLine("SetGps response: {0}", response)
            Else
                Console.WriteLine("SetGps error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
