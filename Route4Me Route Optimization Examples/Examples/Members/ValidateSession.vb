Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Validate Session
        ''' </summary>
        Public Sub ValidateSession()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim params As MemberParameters = New MemberParameters() With { _
                .SessionGuid = "ad9001f33ed6875b5f0e75bce52cbc34", _
                .MemberId = 1, _
                .Format = "json" _
            }
            ' Run the query
            Dim errorString As String = ""
            Dim result As MemberResponse = route4Me.ValidateSession(params, errorString)

            Console.WriteLine("")

            If result IsNot Nothing Then
                Console.WriteLine("ValidateSession executed successfully")
                Console.WriteLine("status: " & result.Status)
                Console.WriteLine("api_key: " & result.ApiKey)
                Console.WriteLine("member_id: " & result.MemberId)
                Console.WriteLine("---------------------------")
            Else
                Console.WriteLine("ValidateSession error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace