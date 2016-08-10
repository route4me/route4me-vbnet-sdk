Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub SearchDestinationMarkedAsDeparted()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim activityParameters As New ActivityParameters() With { _
                .ActivityType = "mark-destination-departed", _
                .RouteId = "03CEF546324F727239ABA69EFF3766E1" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim activities As Activity() = route4Me.GetActivityFeed(activityParameters, errorString)

            Console.WriteLine("")

            If activities IsNot Nothing Then
                Console.WriteLine("SearchDestinationMarkedAsDeparted executed successfully, {0} activities returned", activities.Length)
                Console.WriteLine("")

                For Each Activity As Activity In activities
                    Console.WriteLine("Activity ID: {0}", Activity.ActivityId)
                Next

                Console.WriteLine("")
            Else
                Console.WriteLine("SearchDestinationMarkedAsDeparted error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
