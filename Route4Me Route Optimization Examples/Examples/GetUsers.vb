Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetUsers()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim parameters As New GenericParameters()

            ' Run the query
            Dim errorString As String = ""
            Dim dataObjects As User() = route4Me.GetUsers(parameters, errorString)

            Console.WriteLine("")

            If dataObjects IsNot Nothing Then
                Console.WriteLine("GetUsers executed successfully, {0} users returned", dataObjects.Length)

                'dataObjects.ForEach(user =>
                '{
                '  Console.WriteLine("User ID: {0}", user.MemberId);
                '});
                Console.WriteLine("")
            Else
                Console.WriteLine("GetUsers error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
