Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get All Configuration Data
        ''' </summary>
        Public Sub GetAllConfigurationData()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim params = New MemberConfigurationParameters()

            Dim errorString As String = Nothing
            Dim result As MemberConfigurationDataResponse = route4Me.
                GetConfigurationData(params, errorString)

            PrintConfigKey(result, errorString)
        End Sub
    End Class
End Namespace