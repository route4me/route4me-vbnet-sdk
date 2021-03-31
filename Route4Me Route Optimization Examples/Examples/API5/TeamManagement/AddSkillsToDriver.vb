Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManagerV5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddSkillsToDriver()
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

            Dim skills As String() = New String() {"Class A CDL", "Forklift", "Skid Steer Loader"}

            Dim resultResponse As ResultResponse = Nothing
            Dim updatedMember = route4Me.AddSkillsToDriver(queryParams, skills, resultResponse)

            PrintTeamMembers(updatedMember, resultResponse)

            Console.WriteLine("")
            Console.WriteLine(
                If((If(updatedMember?.CustomData?.ContainsKey("driver_skills"), False)) = True,
                "Driver skills :" & updatedMember.CustomData("driver_skills"),
                "Cannot add skills to the driver"))

            RemoveTestTeamMembers()
        End Sub
    End Class
End Namespace
