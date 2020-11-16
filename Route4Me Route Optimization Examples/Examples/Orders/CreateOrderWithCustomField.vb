Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Create an order with custom fields.
        ''' </summary>
        Public Sub CreateOrderWithCustomField()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim orderParams = New Order() With {
                .address_1 = "1358 E Luzerne St, Philadelphia, PA 19124, US",
                .cached_lat = 48.335991,
                .cached_lng = 31.18287,
                .day_scheduled_for_YYMMDD = "2019-10-11",
                .address_alias = "Auto test address",
                .custom_user_fields = New OrderCustomField() _
                {
                    New OrderCustomField() With {
                        .OrderCustomFieldId = 93,
                        .OrderCustomFieldValue = "false"
                    }
                }
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.AddOrder(orderParams, errorString)

            PrintExampleOrder(result, errorString)

            If result IsNot Nothing AndAlso result.[GetType]() = GetType(Order) Then
                OrdersToRemove = New List(Of String)() From {
                    result.order_id.ToString()
                }
            End If

            RemoveTestOrders()
        End Sub
    End Class
End Namespace
