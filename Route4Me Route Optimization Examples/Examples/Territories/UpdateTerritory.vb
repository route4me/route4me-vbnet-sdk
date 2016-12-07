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

            Dim territoryId As String = "4703BC24F64E43C45DA852ABAFB5B404"

            Dim territoryParameters As New AvoidanceZoneParameters() With { _
                .TerritoryId = territoryId, _
                .TerritoryName = "Test Territory Updated", _
                .TerritoryColor = "ff0000", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Circle), _
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "6000"} _
                } _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim territory As AvoidanceZone = route4Me.UpdateTerritory(territoryParameters, errorString)

            Console.WriteLine("")

            If territory IsNot Nothing Then
                Console.WriteLine("UpdateTerritory executed successfully")

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId)
            Else
                Console.WriteLine("UpdateTerritory error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace