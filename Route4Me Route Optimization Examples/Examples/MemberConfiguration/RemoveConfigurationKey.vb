Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove a Configuration Key
        ''' </summary>
        Public Sub RemoveConfigurationKey()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateConfigKey()

            Dim newConfigKey As String = configKeysToRemove(configKeysToRemove.Count - 1)

            Dim params = New MemberConfigurationParameters With {
                .config_key = newConfigKey
            }

            Dim errorString As String = Nothing
            Dim result As MemberConfigurationResponse = route4Me.
                RemoveConfigurationKey(params, errorString)

            PrintConfigKey(result, errorString)
        End Sub
    End Class
End Namespace
