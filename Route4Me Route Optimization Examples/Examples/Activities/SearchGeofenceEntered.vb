Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities with the event Geofence Entered
        ''' </summary>
        Public Sub SearchGeofenceEntered()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim activityParameters = New ActivityParameters With {
                .ActivityType = "geofence-entered"
            }

            Dim errorString As String = Nothing
            Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)
        End Sub
    End Class
End Namespace