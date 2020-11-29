Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update Territory
        ''' </summary>
        Public Sub UpdateTerritory()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTerritoryZone()

            Dim territoryId As String = TerritoryZonesToRemove(TerritoryZonesToRemove.Count - 1)

            Dim territoryParameters = New AvoidanceZoneParameters With {
                .TerritoryId = territoryId,
                .TerritoryName = "Test Territory Updated",
                .TerritoryColor = "ff0000",
                .Territory = New Territory With {
                    .Type = TerritoryType.Circle.GetEnumDescription(),
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "6000"}
                }
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim territory As AvoidanceZone = route4Me.UpdateTerritory(territoryParameters, errorString)

            PrintExampleTerritory(territory, errorString)

            RemoveTestTerritoryZones()
        End Sub
    End Class
End Namespace