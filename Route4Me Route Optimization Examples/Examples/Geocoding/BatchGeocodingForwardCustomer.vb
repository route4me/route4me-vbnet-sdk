Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Forward batch geocoding
        ''' </summary>
        ''' <param name="geoParams">Geocoding parameters</param>
        Public Sub BatchGeocodingForward(ByVal Optional geoParams As GeocodingParameters = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            If geoParams Is Nothing Then
                geoParams = New GeocodingParameters() With {
                    .Addresses = "Los Angeles International Airport, CA" & vbLf & "3495 Purdue St, Cuyahoga Falls, OH 44221",
                    .ExportFormat = "json"
                }
            End If

            Dim errorString As String = Nothing
            Dim result As String = route4Me.BatchGeocoding(geoParams, errorString)

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString)
        End Sub
    End Class
End Namespace

