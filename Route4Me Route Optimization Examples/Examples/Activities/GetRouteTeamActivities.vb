Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetRouteTeamActivities()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim routeId As String = "06B655F27E0D6A74BD37F6F9758E4D2E"

            Dim activityParameters As New ActivityParameters() With { _
                .RouteId = routeId, _
                .team = "true" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

            Console.WriteLine("")

            If activities IsNot Nothing Then
                Console.WriteLine("GetActivities executed successfully, {0} activities returned", activities.Length)
                Console.WriteLine("")

                For Each Activity As Activity In activities
                    Console.WriteLine("Activity ID: {0}", Activity.ActivityId)
                Next

                Console.WriteLine("")
            Else
                Console.WriteLine("GetActivities error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
