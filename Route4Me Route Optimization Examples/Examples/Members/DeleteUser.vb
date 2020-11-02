Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove User
        ''' </summary>
        Public Sub DeleteUser()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim params As MemberParametersV4 = New MemberParametersV4() With { _
                .member_id = 147824 _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As Boolean = route4Me.UserDelete(params, errorString)

            Console.WriteLine("")

            If result Then
                Console.WriteLine("DeleteUser executed successfully")
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("DeleteUser error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace