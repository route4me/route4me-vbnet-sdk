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
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim territoryId As String = "39236C3A30F92CA338C41EB0978F9D8A"

            Dim territoryParameters As New AvoidanceZoneParameters() With { _
                .TerritoryId = territoryId, _
                .TerritoryName = "Test Territory Updated", _
                .TerritoryColor = "ff00ff", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Circle), _
                    .Data = New String() {"38.41322259056806,-78.501953234", "3000"} _
                } _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim territory As AvoidanceZone = route4Me.UpdateAvoidanceZone(territoryParameters, errorString)

            Console.WriteLine("")

            If territory IsNot Nothing Then
                Console.WriteLine("UpdateAvoidanceZone executed successfully")

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId)
            Else
                Console.WriteLine("UpdateAvoidanceZone error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace