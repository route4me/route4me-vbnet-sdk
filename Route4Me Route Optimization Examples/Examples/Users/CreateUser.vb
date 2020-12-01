Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' User Registration (v4)
        ''' </summary>
        Public Sub CreateUser()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim params = New MemberParametersV4 With {
                .HIDE_ROUTED_ADDRESSES = "FALSE",
                .member_phone = "571-259-5939",
                .member_zipcode = "22102",
                .member_email = "skrynkovskyy+newdispatcher" & DateTime.Now.ToString("yyMMddHHmmss") & "@gmail.com",
                .HIDE_VISITED_ADDRESSES = "FALSE",
                .READONLY_USER = "FALSE",
                .member_type = "SUB_ACCOUNT_DISPATCHER",
                .date_of_birth = "2010",
                .member_first_name = "Clay",
                .member_password = "123456",
                .HIDE_NONFUTURE_ROUTES = "FALSE",
                .member_last_name = "Abraham",
                .SHOW_ALL_VEHICLES = "FALSE",
                .SHOW_ALL_DRIVERS = "FALSE"
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result As MemberResponseV4 = route4Me.CreateUser(params, errorString)

            PrintTestUsers(result, errorString)

            If result IsNot Nothing AndAlso result.[GetType]() = GetType(MemberResponseV4) Then
                usersToRemove = New List(Of String)()
                usersToRemove.Add(result.member_id)

                RemoveTestUsers()
            End If
        End Sub
    End Class
End Namespace
