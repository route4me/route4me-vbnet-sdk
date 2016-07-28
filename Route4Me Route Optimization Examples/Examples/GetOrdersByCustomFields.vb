Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get single Order by order_id
        ''' </summary>
        Public Sub GetOrdersByCustomFields(CustomFields As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim oParams As New OrderParameters() With { _
                .Fields = CustomFields, _
                .Offset = 0, _
                .Limit = 20 _
            }

            Dim errorString As String = ""
            Dim results As List(Of Integer()) = route4Me.SearchOrdersByCustomFields(oParams, errorString)

            Console.WriteLine("")

            If results IsNot Nothing Then
                Console.WriteLine("GetOrderByCustomFields executed successfully, orders searched total = {0}", results.Count)
            Else
                Console.WriteLine("GetOrderByInsertedDate error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace