Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub UpdateAvoidanceZone(ByVal Optional territoryId As String = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(territoryId Is Nothing, True, False)

            If isInnerExample Then
                CreateAvoidanceZone()
                territoryId = Me.avoidanceZone.TerritoryId
            End If

            Dim avoidanceZoneParameters = New AvoidanceZoneParameters() With {
                .TerritoryId = territoryId,
                .TerritoryName = "Test Territory Updated",
                .TerritoryColor = "ff00ff",
                .Territory = New Territory() With {
                    .Type = TerritoryType.Circle.GetEnumDescription(),
                    .Data = New String() {"38.41322259056806,-78.501953234", "3000"}
                }
            }

            Dim errorString As String = Nothing
            Dim avoidanceZone As AvoidanceZone = route4Me.UpdateAvoidanceZone(avoidanceZoneParameters, errorString)

            PrintExampleAvoidanceZone(avoidanceZone, errorString)

            If isInnerExample Then RemoveAvidanceZone(territoryId)
        End Sub
    End Class
End Namespace
