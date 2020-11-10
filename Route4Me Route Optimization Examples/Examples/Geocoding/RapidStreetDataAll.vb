Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Rapid Street Data All
        ''' </summary>
        Public Sub RapidStreetDataAll()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim geoParams = New GeocodingParameters()

            Dim errorString As String = Nothing
            Dim result As ArrayList = route4Me.RapidStreetData(geoParams, errorString)

            PrintExampleGeocodings(result, GeocodingPrintOption.StreetData, errorString)
        End Sub
    End Class
End Namespace