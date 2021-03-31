Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManagerV5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub UpdateTeamMember()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            membersToRemove = New List(Of TeamResponse)()

            CreateTestTeamMember()

            If membersToRemove.Count < 1 Then
                Console.WriteLine("Cannot create a team member to remove")
                Return
            End If

            Dim member = membersToRemove(membersToRemove.Count - 1)
            Dim queryParams = New MemberQueryParameters() With {
                .UserId = member.MemberId.ToString()
            }

            Dim requestParams = New TeamRequest() With {
                .MemberPhone = "555-777-888",
                .ReadOnlyUser = True,
                .DrivingRate = 4
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim updatedMember = route4Me.UpdateTeamMember(queryParams, requestParams, resultResponse)

            PrintTeamMembers(updatedMember, resultResponse)

            Console.WriteLine(
                If((If(updatedMember?.MemberPhone, Nothing)) = requestParams.MemberPhone,
                "The member phone updated",
                "Cannot update the member phone"))
            Console.WriteLine(
                If((If(updatedMember?.ReadOnlyUser, Nothing)) = requestParams.ReadOnlyUser,
                "The member parameter ReadOnlyUser updated",
                "Cannot update the member parameter ReadOnlyUser"))
            Console.WriteLine(
                If((If(updatedMember?.DrivingRate, Nothing)) = requestParams.DrivingRate,
                "The member parameter DrivingRate updated",
                "Cannot update the member parameter DrivingRate"))

            RemoveTestTeamMembers()
        End Sub
    End Class
End Namespace
