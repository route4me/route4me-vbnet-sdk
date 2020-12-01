Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of getting the existing sub-users.
        ''' </summary>
        Public Sub GetUsers()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim parameters = New GenericParameters()

            ' Run the query
            Dim errorString As String = Nothing
            Dim dataObjects As Route4MeManager.GetUsersResponse = route4Me.GetUsers(parameters, errorString)

            PrintTestUsers(dataObjects, errorString)
        End Sub
    End Class
End Namespace
