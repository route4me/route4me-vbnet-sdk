Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Polygon Avoidance Zone
        ''' </summary>
        ''' <returns> Id of added territory </returns>
        Public Function AddPolygonAvoidanceZone() As String
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim avoidanceZoneParameters As New AvoidanceZoneParameters() With { _
                .TerritoryName = "Test Territory", _
                .TerritoryColor = "ff0000", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Poly), _
                    .Data = New String() { _
                        "37.569752822786455,-77.47833251953125", _
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
            Dim avoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, errorString)

            Console.WriteLine("")

            If avoidanceZone IsNot Nothing Then
                Console.WriteLine("AddPolygonAvoidanceZone executed successfully")

                Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId)

                Return avoidanceZone.TerritoryId
            Else
                Console.WriteLine("AddPolygonAvoidanceZone error: {0}", errorString)

                Return Nothing
            End If
        End Function
    End Class
End Namespace
