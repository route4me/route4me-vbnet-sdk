Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Resequence Route Destination
        ''' </summary>
        Public Sub ResequenceRouteDestination(routeId As String, routeDestinationId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim rParameters As New RouteParametersQuery() With { _
                .RouteId = routeId, _
                .RouteDestinationId = routeDestinationId
            }

            Dim addresses As Address() = New Address() {New Address() With { _
                .RouteId = routeId, _
                .Latitude = 40.285026, _
                .Longitude = -74.333839, _
                .SequenceNo = 2
            }}

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As RouteResponse = route4Me.ResequenceRouteDestination(rParameters, addresses, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("Route waas resequenced successfully")

                Console.WriteLine("Route ID: {0}", dataObject.RouteID)
                ' Console.WriteLine("State: {0}", dataObject.State)
            Else
                Console.WriteLine("Route resequence error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace