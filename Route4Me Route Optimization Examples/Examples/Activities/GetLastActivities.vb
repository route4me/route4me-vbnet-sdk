Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        '''The example refers to the process of query all the activities from the last specified days.
        ''' </summary>
        Public Sub GetLastActivities()
            Dim route4Me = New Route4MeManager(ActualApiKey)
            Dim activitiesAfterTime = DateTime.Now - (New TimeSpan(7, 0, 0, 0))

            activitiesAfterTime = New DateTime(activitiesAfterTime.Year, activitiesAfterTime.Month, activitiesAfterTime.Day, 0, 0, 0)

            Dim uiActivitiesAfterTime As UInteger = CUInt(R4MeUtils.ConvertToUnixTimestamp(activitiesAfterTime))

            Dim activityParameters = New ActivityParameters() With {
                .Limit = 10,
                .Offset = 0,
                .Start = uiActivitiesAfterTime
            }

            Dim errorString As String = Nothing
            Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

            Console.WriteLine("")

            For Each activity As Activity In activities
                Dim activityTime As UInteger = If(activity.ActivityTimestamp IsNot Nothing, CUInt(activity.ActivityTimestamp), 0)

                If activityTime < uiActivitiesAfterTime Then
                    Console.WriteLine("GetLastActivities failed - the last time filter not works.")
                    Exit For
                End If
            Next

            PrintExampleActivities(activities, errorString)
        End Sub
    End Class
End Namespace
