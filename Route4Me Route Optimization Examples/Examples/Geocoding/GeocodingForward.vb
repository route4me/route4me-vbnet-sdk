﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Forward Geocoding
        ''' </summary>
        Public Sub GeocodingForward(ByVal Optional geoParams As GeocodingParameters = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            If geoParams Is Nothing Then
                geoParams = New GeocodingParameters() With {
                    .Addresses = "Los Angeles International Airport, CA||3495 Purdue St, Cuyahoga Falls, OH 44221",
                    .Format = "json"
                }
            End If

            Dim errorString As String = Nothing
            Dim result As String = route4Me.Geocoding(geoParams, errorString)

            PrintExampleGeocodings(result, GeocodingPrintOption.Geocodings, errorString)
        End Sub
    End Class
End Namespace