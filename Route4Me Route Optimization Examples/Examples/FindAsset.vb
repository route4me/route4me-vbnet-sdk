Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Find Asset (Asset tracking)
        ''' </summary>
        Public Sub FindAsset()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)
            Dim query As String = "2121541"
            ' Run the query
            Dim errorString As String = ""
            Dim result As Dictionary(Of String, String) = route4Me.FindAsset(query, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("FindAsset executed successfully")
                Console.WriteLine("route_id: " & result("route_id"))
                Console.WriteLine("sequence_no: " & result("sequence_no"))
                Console.WriteLine("datetime: " & result("mDateTime"))
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("FindAsset error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace