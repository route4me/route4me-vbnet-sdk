Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Territories
        ''' </summary>
        Public Sub GetTerritories()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim territoryQuery As New AvoidanceZoneQuery()

            ' Run the query
            Dim errorString As String = ""
            Dim territories As AvoidanceZone() = route4Me.GetTerritories(territoryQuery, errorString)

            Console.WriteLine("")

            If territories IsNot Nothing Then
                Console.WriteLine("GetTerritories executed successfully")

                Console.WriteLine("GetAvoidanceZones executed successfully, {0} territories returned", territories.Count)
            Else
                Console.WriteLine("GetTerritories error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace