Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get single Order by order_id
        ''' </summary>
        ''' <param name="orderIds">Comma-delimited list of the order IDs</param>
        Public Sub GetOrderByID(Optional orderIds As String = Nothing)
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(orderIds Is Nothing, True, False)

            If isInnerExample Then CreateExampleOrder()

            Dim orderId = If(
                isInnerExample,
                OrdersToRemove(OrdersToRemove.Count - 1),
                orderIds
            )

            Dim orderParameters = New OrderParameters() With {
                .order_id = orderId
            }

            Dim errorString As String = Nothing
            Dim order As Order = route4Me.GetOrderByID(orderParameters, errorString)

            PrintExampleOrder(order, errorString)

            If isInnerExample Then RemoveTestOrders()
        End Sub
    End Class
End Namespace
