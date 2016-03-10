Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Avoidance Zone list
        ''' </summary>
        Public Sub GetAvoidanceZones()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)


            Dim avoidanceZoneQuerry As New AvoidanceZoneQuerry()

            ' Run the query
            Dim errorString As String = ""
            Dim avoidanceZones As AvoidanceZone() = route4Me.GetAvoidanceZones(avoidanceZoneQuerry, errorString)

            Console.WriteLine("")

            If avoidanceZones IsNot Nothing Then
                Console.WriteLine("GetAvoidanceZones executed successfully, {0} zones returned", avoidanceZones.Length)
            Else
                Console.WriteLine("GetAvoidanceZones error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
