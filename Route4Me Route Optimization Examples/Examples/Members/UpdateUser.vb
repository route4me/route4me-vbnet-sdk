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
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim params As MemberParametersV4 = New MemberParametersV4() With { _
                .member_id = 220461, _
                .member_phone = "571-259-5939" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberResponseV4 = route4Me.UserUpdate(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("UpdateUser executed successfully")
                Console.WriteLine("status: " & result.member_first_name & " " & result.member_last_name)
                Console.WriteLine("member_id: " & result.member_id)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("UpdateUser error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
