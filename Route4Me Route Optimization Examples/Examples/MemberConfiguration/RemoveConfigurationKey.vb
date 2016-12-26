Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' User Registration (v4)
        ''' </summary>
        Public Sub RemoveConfigurationKey()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim params As MemberConfigurationParameters = New MemberConfigurationParameters() With { _
                .config_key = "My height" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberConfigurationResponse = route4Me.RemoveConfigurationKey(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("RemoveConfigurationKey executed successfully")
                Console.WriteLine("Result: " & result.result)
                Console.WriteLine("Affected: " & result.affected)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("UserRegistration error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
