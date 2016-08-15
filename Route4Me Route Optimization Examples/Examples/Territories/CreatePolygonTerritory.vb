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
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim territoryParameters As New AvoidanceZoneParameters() With { _
                .TerritoryName = "Test Territory", _
                .TerritoryColor = "ff0000", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Circle), _
                    .Data = New String() {"37.569752822786455,-77.47833251953125", _
                                          "37.75886716305343,-77.68974800109863", _
                                        "37.74763966054455,-77.6917221069336", _
                                        "37.74655084306813,-77.68863220214844", _
                                        "37.7502255383101,-77.68125076293945", _
                                        "37.74797991274437,-77.67498512268066", _
                                        "37.73327960206065,-77.6411678314209", _
                                        "37.74430510679532,-77.63172645568848", _
                                        "37.76641925847049,-77.66846199035645"} _
                } _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim territory As AvoidanceZone = route4Me.CreateTerritory(territoryParameters, errorString)

            Console.WriteLine("")

            If territory IsNot Nothing Then
                Console.WriteLine("CreatePolygonTerritory executed successfully")

                Console.WriteLine("Territory ID: {0}", territory.TerritoryId)
            Else
                Console.WriteLine("CreatePolygonTerritory error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
