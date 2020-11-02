Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function InsertAddressIntoRouteOptimalPosition(routeId As String) As Integer()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Prepare the addresses

            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "Cabo Rojo, Cabo Rojo 00623, Puerto Rico",
                .Alias = "",
                .Latitude = 18.086627,
                .Longitude = -67.145735,
                .CurbsideLatitude = 18.086627,
                .CurbsideLongitude = -67.145735,
                .ContactId = Nothing,
                .IsDeparted = False,
                .IsVisited = False
            }}

            Dim OptimalPosition As Boolean = True

            ' Run the query
            Dim errorString As String = ""

            Dim destinationIds As Integer() = route4Me.InsertAddressIntoRouteOptimalPosition(routeId, addresses, OptimalPosition, errorString)

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
