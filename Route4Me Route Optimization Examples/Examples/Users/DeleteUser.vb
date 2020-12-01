Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove User
        ''' </summary>
        Public Sub DeleteUser()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestUser()
            Dim createdMemberId As Integer = Convert.ToInt32(usersToRemove(usersToRemove.Count - 1))

            Dim params = New MemberParametersV4 With {
                .member_id = createdMemberId
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result As Boolean = route4Me.UserDelete(params, errorString)

            Console.WriteLine("")
            Console.WriteLine(If(
                              result,
                              String.Format("DeleteUser executed successfully"),
                              String.Format("DeleteUser error: {0}", errorString))
            )
        End Sub
    End Class
End Namespace