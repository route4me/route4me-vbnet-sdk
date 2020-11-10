Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.FastProcessing

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples

        Public addressesInFile As Integer = 13
        Dim lsAddresses As List(Of String) = New List(Of String)()

        Public Sub uploadAndGeocodeLargeJsonFile()
            Dim fastbGeocoding As FastBulkGeocoding = New FastBulkGeocoding(ActualApiKey, False)
            Dim lsGeocodedAddressTotal As List(Of AddressGeocoded) = New List(Of AddressGeocoded)()

            AddHandler fastbGeocoding.GeocodingIsFinished,
                Sub(sender As Object, e As FastBulkGeocoding.GeocodingIsFinishedArgs)
                    If lsAddresses Is Nothing Then
                        Console.WriteLine("Geocoding process failed")
                        Return
                    End If

                    If addressesInFile <> lsAddresses.Count Then
                        Console.WriteLine("Not all the addresses were geocoded")
                        Return
                    End If

                    Console.WriteLine("Large addresses file geocoding is finished")
                End Sub

            AddHandler fastbGeocoding.AddressesChunkGeocoded,
                Sub(sender As Object, e As FastBulkGeocoding.AddressesChunkGeocodedArgs)

                    If e.lsAddressesChunkGeocoded IsNot Nothing Then

                        For Each addr1 In e.lsAddressesChunkGeocoded
                            Dim geocoding = If(
                                addr1.geocodedAddress.Geocodings.Length > 0,
                                addr1.geocodedAddress.Geocodings(0),
                                Nothing
                            )

                            lsAddresses.Add(
                                addr1.geocodedAddress.AddressString & ":  " &
                                    If(
                                        geocoding IsNot Nothing,
                                        geocoding.Latitude.ToString() & ", " & geocoding.Longtude.ToString(),
                                    "")
                            )
                        Next
                    End If

                    For Each geocodedAddress As String In lsAddresses
                        Console.WriteLine(geocodedAddress)
                        Console.WriteLine("")
                    Next

                    Console.WriteLine("Total Geocoded Addresses -> " & lsAddresses.Count)
                End Sub

            fastbGeocoding.uploadAndGeocodeLargeJsonFile("Data\JSON\batch_socket_upload_error_addresses_data_5.json")
        End Sub

    End Class
End Namespace