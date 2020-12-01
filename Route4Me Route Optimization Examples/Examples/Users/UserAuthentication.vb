Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' User Authetntication
        ''' </summary>
        Public Sub UserAuthentication()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestUser()

            Dim params = New MemberParameters With {
                .StrEmail = lastCreatedUser.member_email,
                .StrPassword = "123456",
                .Format = "json"
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result As MemberResponse = route4Me.UserAuthentication(params, errorString)

            PrintTestUsers(result, errorString)

            RemoveTestUsers()
        End Sub
    End Class
End Namespace