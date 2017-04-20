Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Mark Address As Marked As Departed
        ''' </summary>
        Public Sub MarkAddressAsMarkedAsDeparted(aParams As AddressParameters)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim resultAddress As Address = route4Me.MarkAddressAsMarkedAsDeparted(aParams, errorString)

            Console.WriteLine("")

            If resultAddress IsNot Nothing Then
                Console.WriteLine("MarkAddressAsMarkedAsDeparted executed successfully")

                Console.WriteLine("Marked Address ID: {0}", resultAddress.RouteDestinationId)

            Else
                Console.WriteLine("MarkAddressAsMarkedAsDeparted error: {0}", errorString)

            End If
        End Sub
    End Class
End Namespace