Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Rectangular Avoidance Zone
        ''' </summary>
        ''' <param name="removeAvoidanceZone">If true, created avoidance zone removed</param>
        ''' <returns>Id of added territory</returns>
        Public Function AddRectAvoidanceZone(ByVal Optional removeAvoidanceZone As Boolean = True) As String
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim avoidanceZoneParameters = New AvoidanceZoneParameters With {
                .TerritoryName = "Test Territory",
                .TerritoryColor = "ff0000",
                .Territory = New Territory With {
                    .Type = TerritoryType.Rect.GetEnumDescription(),
                    .Data = New String() {"43.51668853502909,-109.3798828125", "46.98025235521883,-101.865234375"}
                }
            }

            Dim errorString As String = Nothing
            Dim avoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, errorString)

            PrintExampleAvoidanceZone(avoidanceZone, errorString)

            Dim avZoneId As String = If(avoidanceZone IsNot Nothing AndAlso avoidanceZone.[GetType]() = GetType(AvoidanceZone), avoidanceZone.TerritoryId, Nothing)

            If removeAvoidanceZone Then RemoveAvidanceZone(avZoneId)

            Return If(removeAvoidanceZone, Nothing, avZoneId)
        End Function
    End Class
End Namespace