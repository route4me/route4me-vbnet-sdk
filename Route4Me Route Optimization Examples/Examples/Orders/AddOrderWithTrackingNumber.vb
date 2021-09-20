Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples

        Public Sub AddOrderWithTrackingNumber()

            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim order = New Order() With {
                .address_1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                .TrackingNumber = "AA11ZZCC",
                .AddressStopType = AddressStopType.PickUp.GetEnumDescription()
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim resultOrder = route4Me.AddOrder(order, errorString)

            If resultOrder IsNot Nothing AndAlso resultOrder.[GetType]() = GetType(Order) Then
                OrdersToRemove.Add(resultOrder.order_id.ToString())
            End If

            PrintExampleOrder(resultOrder, errorString)

            RemoveTestOrders()

        End Sub

    End Class

End Namespace