Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddEditCustomDataToUser()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestUser()

            Dim memberId As Integer = Convert.ToInt32(usersToRemove(usersToRemove.Count - 1))

            Dim customParams = New MemberParametersV4 With {
                .member_id = memberId,
                .custom_data = New Dictionary(Of String, Object)() From {
                    {"Custom Key 2", "Custom Value 2"}
                }
            }

            Dim errorString As String = Nothing
            Dim result2 = route4Me.UserUpdate(customParams, errorString)

            PrintTestUsers(result2, errorString)

            If result2 IsNot Nothing AndAlso result2.[GetType]() = GetType(MemberResponseV4) Then
                Dim customData = result2.custom_data

                If customData.Keys.ElementAt(0) <> "Custom Key 2" Then
                    Console.WriteLine("Custom Key is not 'Custom Key 2'")
                End If

                If customData("Custom Key 2") <> "Custom Value 2" Then
                    Console.WriteLine("Custom Value is not 'Custom Value 2'")
                End If
            End If

            RemoveTestUsers()
        End Sub
    End Class
End Namespace