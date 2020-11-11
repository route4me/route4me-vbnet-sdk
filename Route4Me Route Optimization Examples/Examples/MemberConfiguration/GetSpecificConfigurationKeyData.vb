Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Specific Configuration Key Value (v4)
        ''' </summary>
        Public Sub GetSpecificConfigurationKeyData()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateConfigKey()

            Dim newConfigKey As String = configKeysToRemove(configKeysToRemove.Count - 1)

            Dim params = New MemberConfigurationParameters With {
                .config_key = newConfigKey
            }

            Dim errorString As String = Nothing
            Dim result As MemberConfigurationDataResponse = route4Me.GetConfigurationData(params, errorString)

            PrintConfigKey(result, errorString)

            RemoveConfigKeys()
        End Sub
    End Class
End Namespace
