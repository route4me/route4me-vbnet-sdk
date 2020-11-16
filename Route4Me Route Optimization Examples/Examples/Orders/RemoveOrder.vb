Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove Orders
        ''' </summary>
        ''' <param name="orderIds"> Order Ids </param>
        Public Sub RemoveOrders(Optional orderIds As String() = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(orderIds Is Nothing, True, False)

            If isInnerExample Then CreateExampleOrder()

            orderIds = If(
                orderIds Is Nothing,
                New String() {lastCreatedOrder.order_id.ToString()},
                orderIds
            )

            Dim errorString As String = Nothing
            Dim removed As Boolean = route4Me.RemoveOrders(orderIds, errorString)

            Console.WriteLine("")
            Console.WriteLine(
                If(
                    removed,
                    String.Format("RemoveOrders executed successfully, {0} orders removed", orderIds.Length),
                    String.Format("RemoveOrders error: {0}", errorString)
                  )
            )
        End Sub
    End Class
End Namespace
