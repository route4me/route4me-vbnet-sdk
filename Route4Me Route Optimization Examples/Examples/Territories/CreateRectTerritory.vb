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
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim territoryParameters As New AvoidanceZoneParameters() With { _
                .TerritoryName = "Test Territory", _
                .TerritoryColor = "ff0000", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Rect), _
                    .Data = New String() { _
                        "43.51668853502909,-109.3798828125", _
                        "46.98025235521883,-101.865234375" _
                        } _
                } _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim territory As AvoidanceZone = route4Me.CreateTerritory(territoryParameters, errorString)

            Console.WriteLine("")

            If territory IsNot Nothing Then
                Console.WriteLine("CreateRectTerritory executed successfully")

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId)
            Else
                Console.WriteLine("CreateRectTerritory error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace

