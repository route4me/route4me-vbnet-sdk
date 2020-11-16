Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Order
        ''' </summary>
        Public Sub AddOrder()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim order = New Order() With {
                .address_1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .address_alias = "Test AddressAlias " & (New Random()).[Next]().ToString(),
                .cached_lat = 37.773972,
                .cached_lng = -122.431297
            }

            Dim errorString As String = Nothing
            Dim resultOrder As Order = route4Me.AddOrder(order, errorString)

            If resultOrder IsNot Nothing AndAlso resultOrder.[GetType]() = GetType(Order) Then
                OrdersToRemove.Add(resultOrder.order_id.ToString())
            End If

            PrintExampleOrder(resultOrder, errorString)

            RemoveTestOrders()
        End Sub
    End Class
End Namespace
