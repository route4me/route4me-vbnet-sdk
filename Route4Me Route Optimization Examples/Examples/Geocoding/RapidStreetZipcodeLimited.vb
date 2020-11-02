Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Rapid Street Zipcode Limited
        ''' </summary>
        Public Sub RapidStreetZipcodeLimited()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)
            Dim geoParams As New GeocodingParameters With { _
                .Zipcode = "00601", _
                .Offset = 1, _
                .Limit = 10 _
            }
            ' Run the query
            Dim errorString As String = ""
            Dim result As ArrayList = route4Me.RapidStreetZipcode(geoParams, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("RapidStreetZipcodeLimited executed successfully")
                For Each res1 In result
                    Console.WriteLine("Zipcode: " & res1("zipcode"))
                    Console.WriteLine("Street name: " & res1("street_name"))
                    Console.WriteLine("---------------------------")
                Next
            Else
                Console.WriteLine("RapidStreetZipcodeLimited error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
