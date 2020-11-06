Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Avoidance Zone
        ''' </summary>
        ''' <param name="removeAvoidanceZone">If true, created avoidance zone removed</param>
        ''' <returns>Id of added territory </returns>
        Public Function AddAvoidanceZone(
                 ByVal Optional removeAvoidanceZone As Boolean = True) As String

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim avoidanceZoneParameters = New AvoidanceZoneParameters() With {
                .TerritoryName = "Test Territory",
                .TerritoryColor = "ff0000",
                .Territory = New Territory() With {
                    .Type = TerritoryType.Circle.GetEnumDescription(),
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"}
                }
            }

            Dim errorString As String = Nothing
            Dim avoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, errorString)

            PrintExampleAvoidanceZone(avoidanceZone, errorString)

            Dim avZoneId As String = If(avoidanceZone IsNot Nothing AndAlso avoidanceZone.[GetType]() = GetType(AvoidanceZone), avoidanceZone.TerritoryId, Nothing)

            If removeAvoidanceZone Then RemoveAvidanceZone(avZoneId)

            Return If(removeAvoidanceZone, Nothing, avZoneId)
        End Function
    End Class
End Namespace
