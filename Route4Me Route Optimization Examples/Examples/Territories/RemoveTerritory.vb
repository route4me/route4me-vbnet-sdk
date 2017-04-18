Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove Territory
        ''' </summary>
        Public Sub RemoveTerritory()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim territoryId As String = "12ABBFA3B5E4FB007BB0ED73291576C2"

            Dim territoryQuery As New TerritoryQuery() With { _
                .TerritoryId = territoryId _
            }

            ' Run the query
            Dim errorString As String = ""
            route4Me.RemoveTerritory(territoryQuery, errorString)

            Console.WriteLine("")

            If errorString = "" Then
                Console.WriteLine("RemoveTerritory executed successfully")

                Console.WriteLine("Territory ID: {0}", territoryId)
            Else
                Console.WriteLine("RemoveTerritory error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
