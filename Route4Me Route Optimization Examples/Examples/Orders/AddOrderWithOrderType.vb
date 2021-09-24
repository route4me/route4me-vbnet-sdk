Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples

        ''' <summary>
        ''' The example demonstrates the process of creating an order with the specified tracking number and address stop type.
        ''' </summary>
        Public Sub AddOrderWithOrderType()

            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            'Using of an existing tracking number raises error
            Dim randomTrackingNumber = R4MeUtils.GenerateRandomString(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")

            Dim order = New Order() With {
                .address_1 = "201 LAVACA ST APT 746, AUSTIN, TX, 78701, US",
                .TrackingNumber = randomTrackingNumber,
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