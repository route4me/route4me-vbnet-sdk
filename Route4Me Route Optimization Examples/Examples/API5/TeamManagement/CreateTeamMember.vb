Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub CreateTeamMember()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim ownerId As Integer? = GetOwnerMemberId()
            If ownerId Is Nothing Then Return

            Dim newMemberParameters = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "John",
                .MemberLastName = "Doe",
                .MemberCompany = "Test Member Created",
                .MemberEmail = GetTestEmail(),
                .OwnerMemberId = CInt(ownerId)
            }

            newMemberParameters.SetMemberType(MemberTypes.Driver)

            Dim resultResponse As ResultResponse = Nothing
            Dim member = route4Me.CreateTeamMember(newMemberParameters, resultResponse)

            If member IsNot Nothing AndAlso member.[GetType]() = GetType(TeamResponse) Then
                membersToRemove.Add(member)
            End If

            PrintTeamMembers(member, resultResponse)
            RemoveTestTeamMembers()
        End Sub
    End Class
End Namespace