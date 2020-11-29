Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Rectangle Territory
        ''' </summary>
        Public Sub CreateRectTerritory()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim territoryParameters = New AvoidanceZoneParameters With {
                .TerritoryName = "Test Territory",
                .TerritoryColor = "ff0000",
                .Territory = New Territory With {
                    .Type = TerritoryType.Rect.GetEnumDescription(),
                    .Data = New String() {"43.51668853502909,-109.3798828125", "46.98025235521883,-101.865234375"}
                }
            }

            Dim errorString As String = Nothing
            Dim territory As TerritoryZone = route4Me.CreateTerritory(territoryParameters, errorString)

            If (If(territory?.TerritoryId, Nothing)) IsNot Nothing Then
                TerritoryZonesToRemove.Add(territory.TerritoryId)
            End If

            PrintExampleTerritory(territory, errorString)

            RemoveTestTerritoryZones()
        End Sub
    End Class
End Namespace

