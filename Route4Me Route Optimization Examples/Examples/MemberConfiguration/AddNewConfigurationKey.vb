Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Create New Configuration Key
        ''' </summary>
        Public Sub AddNewConfigurationKey()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim params = New MemberConfigurationParameters With {
                .config_key = "Test Config Key 5",
                .config_value = "Test Config Value 5"
            }

            Dim errorString As String = Nothing
            Dim result As MemberConfigurationResponse = route4Me.CreateNewConfigurationKey(params, errorString)

            If (If(result?.result, Nothing)) = "OK" AndAlso (If(result?.affected, 0)) > 0 Then
                configKeysToRemove.Add("Test Config Key 5")
            End If

            PrintConfigKey(result, errorString)

            RemoveConfigKeys()
        End Sub
    End Class
End Namespace