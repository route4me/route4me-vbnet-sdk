Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Rapid Street Data All
        ''' </summary>
        Public Sub RapidStreetDataAll()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)
            Dim geoParams As New GeocodingParameters
            ' Run the query
            Dim errorString As String = ""
            Dim result As ArrayList = route4Me.RapidStreetData(geoParams, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("RapidStreetDataAll executed successfully")
                For Each res1 In result
                    Console.WriteLine("Zipcode: " & res1("zipcode"))
                    Console.WriteLine("Street name: " & res1("street_name"))
                    Console.WriteLine("---------------------------")
                Next
            Else
                Console.WriteLine("RapidStreetDataAll error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace