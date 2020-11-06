Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Delete Avoidance Zone
        ''' </summary>
        ''' <param name="territoryId"> Avoidance Zone Id </param>
        Public Sub DeleteAvoidanceZone(ByVal Optional territoryId As String = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(territoryId Is Nothing, True, False)

            If isInnerExample Then
                CreateAvoidanceZone()
                territoryId = avoidanceZone.TerritoryId
            End If

            Dim avoidanceZoneQuery = New AvoidanceZoneQuery() With {
                .TerritoryId = territoryId
            }

            Dim errorString As String = Nothing
            route4Me.DeleteAvoidanceZone(avoidanceZoneQuery, errorString)

            PrintExampleAvoidanceZone(territoryId, errorString)
        End Sub
    End Class
End Namespace
