Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Avoidance Zone list
        ''' </summary>
        Public Sub GetAvoidanceZones()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim avoidanceZoneQuery = New AvoidanceZoneQuery()

            Dim errorString As String = Nothing
            Dim avoidanceZones As AvoidanceZone() = route4Me.GetAvoidanceZones(avoidanceZoneQuery, errorString)

            Console.WriteLine("")
            Console.WriteLine(If(avoidanceZones IsNot Nothing, String.Format("GetAvoidanceZones executed successfully, {0} zones returned", avoidanceZones.Length), String.Format("GetAvoidanceZones error: {0}", errorString)))
        End Sub
    End Class
End Namespace