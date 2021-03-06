﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities from a user account.
        ''' </summary>
        Public Sub GetActivities()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim activityParameters As New ActivityParameters() With {
                .Limit = 10,
                .Offset = 0
            }

            ' Run the query
            Dim errorString As String = ""
            Dim activities As Activity() = route4Me.GetActivities(activityParameters, errorString)

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
