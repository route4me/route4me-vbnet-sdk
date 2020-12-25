Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub SearchAreaAdded()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Possible values of the parameter ActivityType are:
            ' "delete-destination", "insert-destination", "mark-destination-departed", "move-destination", "update-destinations", 
            ' "mark-destination-visited", "member-created", "member-deleted", "member-modified", "note-insert", "route-delete", "route-optimized", "route-owner-changed"
            Dim activityParameters As New ActivityParameters() With { _
                .ActivityType = "area-added" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim activities As Activity() = route4Me.GetActivities(activityParameters, errorString)

            Console.WriteLine("")

            If activities IsNot Nothing Then
                Console.WriteLine("SearchAreaAdded executed successfully, {0} activities returned", activities.Length)
                Console.WriteLine("")

                For Each Activity As Activity In activities
                    Console.WriteLine("Activity ID: {0}", Activity.ActivityId)
                Next

                Console.WriteLine("")
            Else
                Console.WriteLine("SearchAreaAdded error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace