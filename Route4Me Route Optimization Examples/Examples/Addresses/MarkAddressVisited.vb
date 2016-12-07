Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Mark Address Visited
        ''' </summary>
        ''' <returns> status </returns>
        Public Sub MarkAddressVisited(aParams As AddressParameters)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim result As Dictionary(Of String, Boolean) = route4Me.MarkAddressVisited(aParams, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                If result.Item("status") Then
                    Console.WriteLine("MarkAddressVisited executed successfully")
                Else
                    Console.WriteLine("MarkAddressVisited error: {0}", errorString)
                End If
            Else
                Console.WriteLine("MarkAddressVisited error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
