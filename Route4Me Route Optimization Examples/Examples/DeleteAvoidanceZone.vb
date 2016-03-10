Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Delete Avoidance Zone
        ''' </summary>
        ''' <param name="territoryId"> Avoidance Zone Id </param>
        Public Sub DeleteAvoidanceZone(territoryId As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim avoidanceZoneQuerry As New AvoidanceZoneQuerry() With { _
                .TerritoryId = territoryId _
            }

            ' Run the query
            Dim errorString As String = ""
            route4Me.DeleteAvoidanceZone(avoidanceZoneQuerry, errorString)

            Console.WriteLine("")

            If errorString = "" Then
                Console.WriteLine("DeleteAvoidanceZone executed successfully")

                Console.WriteLine("Territory ID: {0}", territoryId)
            Else
                Console.WriteLine("DeleteAvoidanceZone error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
