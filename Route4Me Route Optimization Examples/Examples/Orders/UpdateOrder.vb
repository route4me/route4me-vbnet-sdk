Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update Order
        ''' </summary>
        ''' <param name="order1"> Order with updated attributes </param>
        Public Sub UpdateOrder(ByVal Optional order1 As Order = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(order1 Is Nothing, True, False)

            If isInnerExample Then CreateExampleOrder()

            Dim orderId As String = If(
                isInnerExample,
                OrdersToRemove(OrdersToRemove.Count - 1),
                order1.order_id.ToString()
            )

            Dim orderParameters = New OrderParameters() With {
                .order_id = orderId
            }

            Dim errorString As String = Nothing
            Dim order As Order = route4Me.GetOrderByID(orderParameters, errorString)

            order.EXT_FIELD_last_name = "Updated " & (New Random()).[Next]().ToString()

            Dim updatedOrder = route4Me.UpdateOrder(order, errorString)

            PrintExampleOrder(updatedOrder, errorString)

            If isInnerExample Then RemoveTestOrders()
        End Sub
    End Class
End Namespace

