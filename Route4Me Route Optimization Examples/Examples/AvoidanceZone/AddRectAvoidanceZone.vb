Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Rectangular Avoidance Zone
        ''' </summary>
        ''' <returns> Id of added territory </returns>
        Public Function AddRectAvoidanceZone() As String
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim avoidanceZoneParameters As New AvoidanceZoneParameters() With { _
                .TerritoryName = "Test Territory", _
                .TerritoryColor = "ff0000", _
                .Territory = New Territory() With { _
                    .Type = EnumHelper.GetEnumDescription(TerritoryType.Rect), _
                    .Data = New String() { _
                        "43.51668853502909,-109.3798828125", _
                        "46.98025235521883,-101.865234375"} _
                } _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim avoidanceZone As AvoidanceZone = route4Me.AddAvoidanceZone(avoidanceZoneParameters, errorString)

            Console.WriteLine("")

            If avoidanceZone IsNot Nothing Then
                Console.WriteLine("AddRectAvoidanceZone executed successfully")

                Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId)

                Return avoidanceZone.TerritoryId
            Else
                Console.WriteLine("AddRectAvoidanceZone error: {0}", errorString)

                Return Nothing
            End If
        End Function
    End Class
End Namespace

