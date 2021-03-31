Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManagerV5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub RemoveTeamMember()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            membersToRemove = New List(Of TeamResponse)()

            CreateTestTeamMember()

            If membersToRemove.Count < 1 Then
                Console.WriteLine("Cannot create a team member to remove")
                Return
            End If

            Dim member = membersToRemove(membersToRemove.Count - 1)
            Dim memberParams = New MemberQueryParameters() With {
                .UserId = member.MemberId.ToString()
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim removedMember = route4Me.RemoveTeamMember(memberParams, resultResponse)

            PrintTeamMembers(removedMember, resultResponse)

            Console.WriteLine(
                If((If(removedMember?.MemberEmail?.Contains(".deleted"), False)),
                    String.Format("A member {0} removed succsessfully", removedMember.MemberId),
                    String.Format("Cannot remove a member {0}", removedMember.MemberId)))
        End Sub
    End Class
End Namespace
