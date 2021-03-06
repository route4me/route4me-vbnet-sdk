﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Territories
        ''' </summary>
        Public Sub GetTerritories()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim territoryQuery = New AvoidanceZoneQuery()

            Dim errorString As String = Nothing
            Dim territories As AvoidanceZone() = route4Me.GetTerritories(territoryQuery, errorString)

            PrintExampleTerritory(territories, errorString)
        End Sub
    End Class
End Namespace