Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get single Order by order_id
        ''' </summary>
        Public Sub GetOrderByID(OrderId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim orderParameters As New OrderParameters() With { _
                .order_id = OrderId _
            }

            Dim errorString As String = ""
            Dim order As Order = route4Me.GetOrderByID(orderParameters, errorString)

            Console.WriteLine("")

            If order IsNot Nothing Then
                Console.WriteLine("GetOrderByID executed successfully, order_id = {0}", order.order_id)
            Else
                Console.WriteLine("GetOrderByID error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
