Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Rapid Street Zipcode Limited
        ''' </summary>
        Public Sub RapidStreetZipcodeLimited()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim geoParams = New GeocodingParameters() With {
                .Zipcode = "00601",
                .Offset = 1,
                .Limit = 10
            }

            Dim errorString As String = Nothing
            Dim result As ArrayList = route4Me.RapidStreetZipcode(geoParams, errorString)

            PrintExampleGeocodings(result, GeocodingPrintOption.StreetZipCode, errorString)
        End Sub
    End Class
End Namespace