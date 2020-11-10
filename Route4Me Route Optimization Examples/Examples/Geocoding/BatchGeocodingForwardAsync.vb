Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Asynchronous batch geocoding
        ''' </summary>
        ''' <param name="geoParams">Geocoding parameters</param>
        Public Sub BatchGeocodingForwardAsync(ByVal Optional geoParams As GeocodingParameters = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            geoParams = New GeocodingParameters With {
                .Addresses = "Los Angeles International Airport, CA" & vbLf & "3495 Purdue St, Cuyahoga Falls, OH 44221",
                .ExportFormat = "json"
            }

            Dim errorString As String = Nothing
            Dim result As String = route4Me.BatchGeocodingAsync(geoParams, errorString)

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString)
        End Sub
    End Class
End Namespace
