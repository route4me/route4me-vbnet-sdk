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
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim params As MemberParameters = New MemberParameters() With { _
                .StrEmail = "oooooo@gmail.com", _
                .StrPassword_1 = "11111111", _
                .StrPassword_2 = "11111111", _
                .StrFirstName = "Olman", _
                .StrLastName = "Progman", _
                .StrIndustry = "Transportation", _
                .ChkTerms = 1, _
                .Plan = "free" _
            }
            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberResponse = route4Me.UserRegistration(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("UserRegistration executed successfully")
                Console.WriteLine("status: " & result.Status)
                Console.WriteLine("api_key: " & result.ApiKey)
                Console.WriteLine("member_id: " & result.MemberId)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("UserRegistration error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace