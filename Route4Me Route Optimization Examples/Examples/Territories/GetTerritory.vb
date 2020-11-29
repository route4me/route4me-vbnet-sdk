Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Territory
        ''' </summary>
        Public Sub GetTerritory()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTerritoryZone()

            Dim territoryId As String = TerritoryZonesToRemove(TerritoryZonesToRemove.Count - 1)

            Dim territoryQuery = New TerritoryQuery With {
                .TerritoryId = territoryId,
                .Addresses = 1
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim territory As TerritoryZone = route4Me.GetTerritory(territoryQuery, errorString)

            PrintExampleTerritory(territory, errorString)

            RemoveTestTerritoryZones()
        End Sub
    End Class
End Namespace