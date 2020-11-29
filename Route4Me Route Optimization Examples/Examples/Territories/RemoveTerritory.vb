Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove Territory
        ''' </summary>
        Public Sub RemoveTerritory()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTerritoryZone()

            Dim territoryId As String = TerritoryZonesToRemove(TerritoryZonesToRemove.Count - 1)

            Dim territoryQuery = New TerritoryQuery With {
                .TerritoryId = territoryId
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim removed As Boolean = route4Me.RemoveTerritory(territoryQuery, errorString)

            Console.WriteLine(
                If(
                    removed,
                    String.Format("The territory {0} removed successfully", territoryId),
                    String.Format("Cannot remove the territory {0}", territoryId) & Environment.NewLine & errorString
                  )
            )
        End Sub
    End Class
End Namespace
