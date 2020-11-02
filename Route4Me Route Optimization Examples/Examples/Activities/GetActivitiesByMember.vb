Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities by member.
        ''' </summary>
        Public Sub GetActivitiesByMember()
            If ActualApiKey = DemoApiKey Then Return

            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim parameters = New GenericParameters()
            Dim userErrorString As String = Nothing
            Dim response = route4Me.GetUsers(parameters, userErrorString)

            If response Is Nothing OrElse response.results.[GetType]() <> GetType(MemberResponseV4()) Then
                Console.WriteLine("GetActivitiesByMemberTest failed - cannot get users")
                Return
            End If

            If response.results.Length < 2 Then
                Console.WriteLine("Cannot retrieve more than 1 users")
                Return
            End If

            Dim activityParameters = New ActivityParameters() With {
                .MemberId = If(response.results(1).member_id IsNot Nothing, Convert.ToInt32(response.results(1).member_id), -1),
                .Offset = 0,
                .Limit = 10
            }

            Dim errorString As String = Nothing
            Dim activities As Activity() = route4Me.GetActivities(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)
        End Sub
    End Class
End Namespace
