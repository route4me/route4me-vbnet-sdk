﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Avoidance Zone
        ''' </summary>
        ''' <param name="territoryId"> Avoidance Zone Id </param>
        Public Sub GetAvoidanceZone(ByVal Optional territoryId As String = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(territoryId Is Nothing, True, False)

            If isInnerExample Then
                CreateAvoidanceZone()
                territoryId = Me.avoidanceZone.TerritoryId
            End If

            Dim avoidanceZoneQuery = New AvoidanceZoneQuery() With {
                .TerritoryId = territoryId
            }

            Dim errorString As String = Nothing
            Dim avoidanceZone As AvoidanceZone = route4Me.GetAvoidanceZone(avoidanceZoneQuery, errorString)

            PrintExampleAvoidanceZone(avoidanceZone, errorString)

            If isInnerExample Then RemoveAvidanceZone(territoryId)
        End Sub
    End Class
End Namespace
