Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Avoidance Zone
        ''' </summary>
        Public Sub CreateTerritory()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim territoryParameters As New AvoidanceZoneParameters() With { _
                .TerritoryName = "Test Territory", _
                .TerritoryColor = "ff0000", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Circle), _
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"} _
                } _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim territory As TerritoryZone = route4Me.CreateTerritory(territoryParameters, errorString)

            Console.WriteLine("")

            If territory IsNot Nothing Then
                Console.WriteLine("CreateTerritory executed successfully")

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId)
            Else
                Console.WriteLine("CreateTerritory error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace

