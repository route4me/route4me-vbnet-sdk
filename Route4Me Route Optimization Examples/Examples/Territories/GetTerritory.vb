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
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim territoryId As String = "03C0330958139E3EDF61EFFCEFBBD64E"

            Dim territoryQuery As New AvoidanceZoneQuery() With { _
                .TerritoryId = territoryId _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim territory As AvoidanceZone = route4Me.GetTerritory(territoryQuery, errorString)

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