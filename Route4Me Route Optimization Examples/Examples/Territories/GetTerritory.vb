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
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim territoryId As String = "596A2A44FE9FB19EEB9C3C072BF2D0BE"

            Dim territoryQuery1 As New TerritoryQuery() With { _
                .TerritoryId = territoryId, _
                .addresses = 1
            }

            ' Run the query
            Dim errorString As String = ""
            Dim territory As TerritoryZone = route4Me.GetTerritory(territoryQuery1, errorString)

            Console.WriteLine("")

            If territory IsNot Nothing Then
                Console.WriteLine("GetTerritory executed successfully")

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId)
            Else
                Console.WriteLine("GetTerritory error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace