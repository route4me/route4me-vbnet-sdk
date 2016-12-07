Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Forward geocoding
        ''' </summary>
        ''' <returns> xml object </returns>
        Public Sub GeocodingForward(geoParams As GeocodingParameters)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim result As String = route4Me.Geocoding(geoParams, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("GeocodingForward executed successfully")
            Else
                Console.WriteLine("GeocodingForward error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
