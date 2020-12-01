Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get User By ID
        ''' </summary>
        Public Sub GetUserById()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestUser()
            Dim memberId As Integer = Convert.ToInt32(usersToRemove(usersToRemove.Count - 1))

            Dim params = New MemberParametersV4 With {
                .member_id = memberId
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result As MemberResponseV4 = route4Me.GetUserById(params, errorString)

            PrintTestUsers(result, errorString)
            RemoveTestUsers()
        End Sub
    End Class
End Namespace