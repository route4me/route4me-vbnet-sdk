Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetAddress(routeId As String, routeDestinationId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim addressParameters As New AddressParameters() With { _
                .RouteId = routeId, _
                .RouteDestinationId = routeDestinationId, _
                .Notes = True _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As Address = route4Me.GetAddress(addressParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("GetAddress executed successfully")
                Console.WriteLine("RouteId: {0}; RouteDestinationId: {1}", dataObject.RouteId, dataObject.RouteDestinationId)
                Console.WriteLine("")
            Else
                Console.WriteLine("GetAddress error: {0}", errorString)
                Console.WriteLine("")
            End If
        End Sub
    End Class
End Namespace
