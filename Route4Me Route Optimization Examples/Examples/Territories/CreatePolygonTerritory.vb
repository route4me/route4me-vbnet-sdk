Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Polygon Territory
        ''' </summary>
        Public Sub CreatePolygonTerritory()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim territoryParameters = New AvoidanceZoneParameters With {
                .TerritoryName = "Test Territory",
                .TerritoryColor = "ff0000",
                .Territory = New Territory With {
                    .Type = TerritoryType.Poly.GetEnumDescription(),
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "37.75886716305343,-77.68974800109863", "37.74763966054455,-77.6917221069336", "37.74655084306813,-77.68863220214844", "37.7502255383101,-77.68125076293945", "37.74797991274437,-77.67498512268066", "37.73327960206065,-77.6411678314209", "37.74430510679532,-77.63172645568848", "37.76641925847049,-77.66846199035645"}
                }
            }

            Dim errorString As String = Nothing
            Dim territory As TerritoryZone = route4Me.CreateTerritory(territoryParameters, errorString)

            If (If(territory?.TerritoryId, Nothing)) IsNot Nothing Then TerritoryZonesToRemove.Add(territory.TerritoryId)

            PrintExampleTerritory(territory, errorString)

            RemoveTestTerritoryZones()
        End Sub
    End Class
End Namespace
