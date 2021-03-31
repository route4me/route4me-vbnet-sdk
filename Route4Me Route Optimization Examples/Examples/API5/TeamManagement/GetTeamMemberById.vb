Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManagerV5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetTeamMemberById()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim randomMember = GetRandomTeamMember()

            If (If(randomMember?.MemberId, 0)) < 1 Then
                Console.WriteLine("Cannot retrieve a random team member")
                Return
            End If

            Dim memberParams = New MemberQueryParameters() With {
                .UserId = randomMember.MemberId.ToString()
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim member = route4Me.GetTeamMemberById(memberParams, resultResponse)

            PrintTeamMembers(member, resultResponse)
        End Sub
    End Class
End Namespace
