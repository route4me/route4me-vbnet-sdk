Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetTeamMembers()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim failResponse As ResultResponse = Nothing

            Dim members = route4Me.GetTeamMembers(failResponse)

            PrintTeamMembers(members, failResponse)
        End Sub
    End Class
End Namespace