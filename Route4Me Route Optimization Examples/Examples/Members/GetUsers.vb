Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetUsers()
            Dim route4Me As Route4MeManager = New Route4MeManager(ActualApiKey)
            Dim parameters As GenericParameters = New GenericParameters()
            Dim errorString As String
            Dim dataObjects As Route4MeManager.GetUsersResponse = route4Me.GetUsers(parameters, errorString)
            Console.WriteLine("")

            If dataObjects IsNot Nothing Then

                If dataObjects.results IsNot Nothing Then
                    Console.WriteLine("GetUsers executed successfully, {0} users returned", dataObjects.results.Length)
                    Console.WriteLine("")
                Else
                    Console.WriteLine("GetUsers error: {0}", errorString)
                End If
            Else
                Console.WriteLine("GetUsers error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
