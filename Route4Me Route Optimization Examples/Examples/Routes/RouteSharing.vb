Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Sharing a Route with someone by email
        ''' </summary>
        Public Sub RouteSharing(routeId As String, Email As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim rParameters As New RouteParametersQuery() With { _
                .RouteId = routeId, _
                .ResponseFormat = "jon"
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As Boolean = route4Me.RouteSharing(rParameters, Email, errorString)

            Console.WriteLine("")

            If result Then
                Console.WriteLine("Route Sharing executed successfully")
            Else
                Console.WriteLine("RouteSharing error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace