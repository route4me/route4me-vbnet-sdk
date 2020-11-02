Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function AddRouteDestinations(routeId As String) As Integer()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Prepare the addresses

            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061", _
                .Latitude = 33.143526, _
                .Longitude = -83.240354, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "222 Blake Cir Milledgeville GA 31061", _
                .Latitude = 33.177852, _
                .Longitude = -83.263535, _
                .Time = 0 _
            }}

            ' Run the query
            Dim errorString As String = ""
            Dim destinationIds As Integer() = route4Me.AddRouteDestinations(routeId, addresses, errorString)

            Console.WriteLine("")

            If destinationIds IsNot Nothing Then
                Console.WriteLine("AddRouteDestinations executed successfully")

                Console.WriteLine("Destination IDs: {0}", String.Join(" ", destinationIds))
            Else
                Console.WriteLine("AddRouteDestinations error: {0}", errorString)
            End If

            Return destinationIds

        End Function
    End Class
End Namespace
