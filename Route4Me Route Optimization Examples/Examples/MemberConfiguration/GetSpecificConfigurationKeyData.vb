Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Specific Configuration Key Value (v4)
        ''' </summary>
        Public Sub GetSpecificConfigurationKeyData()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim params As MemberConfigurationParameters = New MemberConfigurationParameters() With { _
                .config_key = "destination_icon_uri" _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberConfigurationDataRersponse = route4Me.GetConfigurationData(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("GetSpecificConfigurationKeyData executed successfully")
                Console.WriteLine("Result: " & result.result)
                For Each mc_data As MemberConfigurationData In result.data
                    Console.WriteLine("member_id= " & mc_data.member_id)
                    Console.WriteLine("config_key= " & mc_data.config_key)
                    Console.WriteLine("config_value= " & mc_data.config_value)
                    Console.WriteLine("---------------------------")
                Next
            Else
                Console.WriteLine("GetSpecificConfigurationKeyData error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
