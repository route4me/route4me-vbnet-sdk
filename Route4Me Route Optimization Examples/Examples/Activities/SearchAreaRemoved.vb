Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities with the event Area Removed
        ''' </summary>
        Public Sub SearchAreaRemoved()
            Dim route4Me As Route4MeManager = New Route4MeManager(ActualApiKey)

            Dim activityParameters = New ActivityParameters With {
                .ActivityType = "area-removed"
            }

            Dim errorString As String = Nothing
            Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)
        End Sub
    End Class
End Namespace