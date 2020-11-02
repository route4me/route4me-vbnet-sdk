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
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim params As MemberParameters = New MemberParameters() With { _
                .StrEmail = "demo333@yahoo.com", _
                .StrPassword = "1111111", _
                .Format = "json" _
            }
            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberResponse = route4Me.UserAuthentication(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("UserAuthentication executed successfully")
                Console.WriteLine("status: " & result.Status)
                Console.WriteLine("api_key: " & result.ApiKey)
                Console.WriteLine("member_id: " & result.MemberId)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("UserAuthentication error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace