Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update an User
        ''' </summary>
        Public Sub UpdateUser()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestUser()
            Dim memberId As Integer = Convert.ToInt32(usersToRemove(usersToRemove.Count - 1))

            Dim params = New MemberParametersV4 With {
                .member_id = memberId,
                .member_phone = "571-259-5939"
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result As MemberResponseV4 = route4Me.UserUpdate(params, errorString)

            PrintTestUsers(result, errorString)

            If result IsNot Nothing AndAlso result.[GetType]() = GetType(MemberResponseV4) Then
                Console.WriteLine(If(
                                  result.member_phone <> "571-259-5939",
                                  "The user phone is not '571-259-5939'",
                                  "The user phone is '571-259-5939'")
                )
            End If

            RemoveTestUsers()
        End Sub
    End Class
End Namespace
