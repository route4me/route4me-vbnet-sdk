Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples

        ''' <summary>
        ''' Resequence and Reoptimze a Route
        ''' </summary>
        Public Sub ResequenceReoptimizeRoute(routeId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim roParameters As New Dictionary(Of String, String)
            With roParameters
                .Add("route_id", "CA902292134DBC134EAF8363426BD247")
                .Add("disable_optimization", "0")
                .Add("optimize", "Distance")
            End With

            ' Run the query
            Dim errorString As String = ""
            Dim result As Boolean = route4Me.ResequenceReoptimizeRoute(roParameters, errorString)

            Console.WriteLine("")

            If result Then
                Console.WriteLine("ReoptimizeRoute executed successfully")
            Else
                Console.WriteLine("ReoptimizeRoute error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
