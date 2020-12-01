Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' User Registration
        ''' </summary>
        Public Sub UserRegistration()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim params = New MemberParameters With {
                .StrEmail = "skrynkovskyy+newdispatcher" & DateTime.Now.ToString("yyMMddHHmmss") & "@gmail.com",
                .StrPassword_1 = "11111111",
                .StrPassword_2 = "11111111",
                .StrFirstName = "Olas",
                .StrLastName = "Progman",
                .StrIndustry = "Transportation",
                .Format = "json",
                .ChkTerms = 1,
                .DeviceType = "web",
                .Plan = "free",
                .MemberType = 5
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result As MemberResponse = route4Me.UserRegistration(params, errorString)

            If result IsNot Nothing AndAlso result.[GetType]() = GetType(MemberResponse) Then
                usersToRemove = New List(Of String)()
                usersToRemove.Add(result.MemberId.ToString())
            End If

            PrintTestUsers(result, errorString)
            RemoveTestUsers()
        End Sub
    End Class
End Namespace