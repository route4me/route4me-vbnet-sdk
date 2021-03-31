Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub BulkCreateTeamMembers()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim ownerId As Integer? = GetOwnerMemberId()
            If ownerId Is Nothing Then Return

            Dim memberParameters1 = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "John1",
                .MemberLastName = "Doe1",
                .MemberCompany = "Test Member Created",
                .MemberEmail = GetTestEmail().Replace("+", "1+"),
                .OwnerMemberId = CInt(ownerId)
            }

            memberParameters1.SetMemberType(MemberTypes.Driver)

            Dim memberParameters2 = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "John2",
                .MemberLastName = "Doe2",
                .MemberCompany = "Test Member Created",
                .MemberEmail = GetTestEmail().Replace("+", "2+"),
                .OwnerMemberId = CInt(ownerId)
            }

            memberParameters2.SetMemberType(MemberTypes.Driver)

            Dim members = New TeamRequest() {memberParameters1, memberParameters2}
            Dim errorResponse As ResultResponse = Nothing
            Dim result = route4Me.BulkCreateTeamMembers(members, errorResponse)

            Console.WriteLine(result.Code)

            Dim errorResponse0 As ResultResponse = Nothing

            If (If(errorResponse?.Messages?.Count, 0)) < 1 Then
                Dim allMembers = route4Me.GetTeamMembers(errorResponse0)
                Dim membersCreated = allMembers.Where(
                    Function(x) x.MemberCompany = "Test Member Created" _
                                    AndAlso Array.IndexOf(New String() {"John1", "John2"}, x.MemberFirstName) > -1 _
                                    AndAlso Array.IndexOf(New String() {"Doe1", "Doe2"}, x.MemberLastName) > -1)

                If (If(allMembers?.Length, 0)) > 0 Then
                    membersToRemove = membersCreated.ToList()
                    RemoveTestTeamMembers()
                End If

            End If
        End Sub
    End Class
End Namespace
