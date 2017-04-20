Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Mark Address As Marked As Visited
        ''' </summary>
        Public Sub MarkAddressAsMarkedAsVisited(aParams As AddressParameters)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim resultAddress As Address = route4Me.MarkAddressAsMarkedAsVisited(aParams, errorString)

            Console.WriteLine("")

            If resultAddress IsNot Nothing Then
                Console.WriteLine("MarkAddressAsMarkedAsVisited executed successfully")

                Console.WriteLine("Marked Address ID: {0}", resultAddress.RouteDestinationId)

            Else
                Console.WriteLine("MarkAddressAsMarkedAsVisited error: {0}", errorString)

            End If
        End Sub
    End Class
End Namespace