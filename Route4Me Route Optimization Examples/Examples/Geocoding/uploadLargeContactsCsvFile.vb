Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.FastProcessing

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub uploadLargeContactsCsvFile()

            Dim fastProcessing = New FastBulkGeocoding(ActualApiKey, False) With {
                .ChunkPause = 0,
                .CsvChunkSize = 500,
                .DoGeocoding = True,
                .GeocodeOnlyEmpty = True
            }
            Dim lsAddresses = New List(Of String)()

            Dim ab = New AddressBookContact()

            Dim csvAddressMapping = New Dictionary(Of String, String)() From {
                {"Alias", R4MeUtils.GetPropertyName(Function() ab.address_alias)},
                {"Address", R4MeUtils.GetPropertyName(Function() ab.address_1)},
                {"City", R4MeUtils.GetPropertyName(Function() ab.address_city)},
                {"State", R4MeUtils.GetPropertyName(Function() ab.address_state_id)},
                {"Group", R4MeUtils.GetPropertyName(Function() ab.address_group)},
                {"Zip", R4MeUtils.GetPropertyName(Function() ab.address_zip)},
                {"Lat", R4MeUtils.GetPropertyName(Function() ab.cached_lat)},
                {"Lng", R4MeUtils.GetPropertyName(Function() ab.cached_lng)},
                {"Time", R4MeUtils.GetPropertyName(Function() ab.service_time)},
                {"Time_window_start", R4MeUtils.GetPropertyName(Function() ab.local_time_window_start)},
                {"Time_window_end", R4MeUtils.GetPropertyName(Function() ab.local_time_window_end)},
                {"Custom_Data", R4MeUtils.GetPropertyName(Function() ab.address_custom_data)}
            }

            FastFileReading.csvAddressMapping = csvAddressMapping
            fastProcessing.MandatoryFields = csvAddressMapping.Values.ToArray()

            Console.WriteLine("Start: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

            Dim stPath = AppDomain.CurrentDomain.BaseDirectory
            Dim errorString As String = Nothing
            fastProcessing.uploadLargeContactsCsvFile(stPath & "Data\CSV\60k_prob.csv", errorString)

            Console.WriteLine("End: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        End Sub
    End Class
End Namespace