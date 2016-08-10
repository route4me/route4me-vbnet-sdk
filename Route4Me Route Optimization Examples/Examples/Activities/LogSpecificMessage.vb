Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Log Specific Message
        ''' </summary>
        Public Sub LogSpecificMessage()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim params As ActivityParameters = New ActivityParameters() With { _
                .ActivityType = "user_message", _
                .ActivityMessage = "Hello vb.net !", _
                .RouteId = "2EA70721624592FC41522A708603876D" _
            }
            ' Run the query
            Dim errorString As String = ""
            Dim result As Boolean = route4Me.LogSpecificMessage(params, errorString)

            Console.WriteLine("")

            If result Then
                Console.WriteLine("LogSpecificMessage executed successfully")
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("LogSpecificMessage error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace