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
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim params As MemberParametersV4 = New MemberParametersV4() With { _
                .HIDE_ROUTED_ADDRESSES = "FALSE", _
                .member_phone = "571-259-5939", _
                .member_zipcode = "22102", _
                .member_email = "skrynkovskyy+newdispatcher@gmail.com", _
                .HIDE_VISITED_ADDRESSES = "FALSE", _
                .READONLY_USER = "FALSE", _
                .member_type = "SUB_ACCOUNT_DISPATCHER", _
                .date_of_birth = "2010", _
                .member_first_name = "Clay", _
                .member_password = "123456", _
                .HIDE_NONFUTURE_ROUTES = "FALSE", _
                .member_last_name = "Abraham", _
                .SHOW_ALL_VEHICLES = "FALSE", _
                .SHOW_ALL_DRIVERS = "FALSE" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberResponseV4 = route4Me.CreateUser(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("UserRegistration executed successfully")
                Console.WriteLine("User: " & result.member_first_name & " " & result.member_last_name)
                Console.WriteLine("member_id: " & result.member_id)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("UserRegistration error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
