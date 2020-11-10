Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Reverse Geocoding
        ''' </summary>
        Public Sub ReverseGeocoding()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim geoParams = New GeocodingParameters With {
                .Addresses = "42.35863,-71.05670",
                .Format = "json"
            }

            Dim errorString As String = Nothing
            Dim result As String = route4Me.Geocoding(geoParams, errorString)

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString)
        End Sub
    End Class
End Namespace