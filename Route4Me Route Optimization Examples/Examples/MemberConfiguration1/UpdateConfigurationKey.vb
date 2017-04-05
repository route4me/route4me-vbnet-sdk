Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update a Configuration Key Value
        ''' </summary>
        Public Sub UpdateConfigurationKey()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim params As MemberConfigurationParameters = New MemberConfigurationParameters() With { _
                .config_key = "destination_icon_uri", _
                .config_value = "444" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberConfigurationResponse = route4Me.UpdateConfigurationKey(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("UpdateConfigurationKey executed successfully")
                Console.WriteLine("Result: " & result.result)
                Console.WriteLine("Affected: " & result.affected)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("UpdateConfigurationKey error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace