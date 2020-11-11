Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add configuration key array to a user account
        ''' </summary>
        Public Sub AddConfigurationKeyArray()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim parametersArray = New MemberConfigurationParameters() {New MemberConfigurationParameters With {
                .config_key = "Test Height 5",
                .config_value = "155"
            }, New MemberConfigurationParameters With {
                .config_key = "Test Weight 5",
                .config_value = "55"
            }}

            Dim errorString As String = Nothing
            Dim result = route4Me.CreateNewConfigurationKey(parametersArray, errorString)

            If (If(result?.result, Nothing)) = "OK" AndAlso (If(result?.affected, 0)) > 0 Then
                configKeysToRemove.Add("Test Height 5")
                configKeysToRemove.Add("Test Weight 5")
            End If

            PrintConfigKey(result, errorString)

            RemoveConfigKeys()
        End Sub
    End Class
End Namespace
