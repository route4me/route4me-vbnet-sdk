Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Mark Address Visited
        ''' </summary>
        Public Sub MarkAddressVisited(aParams As AddressParameters)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim result As Integer = route4Me.MarkAddressVisited(aParams, errorString)

            Console.WriteLine("")

            If result = 1 Then
                Console.WriteLine("MarkAddressVisited executed successfully")
            Else
                Console.WriteLine("MarkAddressVisited error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
