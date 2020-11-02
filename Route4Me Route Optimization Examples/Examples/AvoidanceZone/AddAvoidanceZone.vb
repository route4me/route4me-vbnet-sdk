Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Avoidance Zone
        ''' </summary>
        ''' <returns> Id of added territory </returns>
        Public Function AddAvoidanceZone() As String
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim avoidanceZoneParameters As New AvoidanceZoneParameters() With { _
                .TerritoryName = "Test Territory", _
                .TerritoryColor = "ff0000", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Circle), _
                    .Data = New String() {"37.569752822786455,-77.47833251953125", "5000"} _
                } _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim avoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, errorString)

            Console.WriteLine("")

            If avoidanceZone IsNot Nothing Then
                Console.WriteLine("AddAvoidanceZone executed successfully")

                Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId)

                Return avoidanceZone.TerritoryId
            Else
                Console.WriteLine("AddAvoidanceZone error: {0}", errorString)

                Return Nothing
            End If
        End Function
    End Class
End Namespace
