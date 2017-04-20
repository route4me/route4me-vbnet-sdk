Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Reverse geocoding
        ''' </summary>
        Public Sub ReverseGeocoding()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)
            Dim geoParams As New GeocodingParameters With { _
                 .Addresses = "42.35863,-71.05670"
            }
            ' Run the query
            Dim errorString As String = ""
            Dim result As String = route4Me.Geocoding(geoParams, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("ReverseGeocoding executed successfully")
            Else
                Console.WriteLine("ReverseGeocoding error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace