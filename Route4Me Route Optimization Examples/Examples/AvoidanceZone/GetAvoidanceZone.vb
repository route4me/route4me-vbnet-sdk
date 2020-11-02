Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Avoidance Zone
        ''' </summary>
        ''' <param name="territoryId"> Avoidance Zone Id </param>
        Public Sub GetAvoidanceZone(territoryId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim avoidanceZoneQuery As New AvoidanceZoneQuery() With { _
                .TerritoryId = territoryId _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim avoidanceZone As AvoidanceZone = route4Me.GetAvoidanceZone(avoidanceZoneQuery, errorString)

            Console.WriteLine("")

            If avoidanceZone IsNot Nothing Then
                Console.WriteLine("GetAvoidanceZone executed successfully")

                Console.WriteLine("Territory ID: {0}", avoidanceZone.TerritoryId)
            Else
                Console.WriteLine("GetAvoidanceZone error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
