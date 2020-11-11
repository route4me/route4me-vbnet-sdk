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
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateConfigKey()

            Dim newConfigKey As String = configKeysToRemove(configKeysToRemove.Count - 1)

            Dim params = New MemberConfigurationParameters With {
                .config_key = newConfigKey,
                .config_value = "Test Config Value Updated"
            }

            Dim errorString As String = Nothing
            Dim result As MemberConfigurationResponse = route4Me.
                UpdateConfigurationKey(params, errorString)

            PrintConfigKey(result, errorString)

            RemoveConfigKeys()
        End Sub
    End Class
End Namespace