Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update an order with custom field.
        ''' </summary>
        Public Sub UpdateOrderWithCustomField()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateExampleOrder()

            lastCreatedOrder.custom_user_fields = New OrderCustomField() _
            {
                New OrderCustomField() With {
                    .OrderCustomFieldId = 93,
                    .OrderCustomFieldValue = "true"
                }
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.UpdateOrder(lastCreatedOrder, errorString)

            PrintExampleOrder(result, errorString)

            RemoveTestOrders()
        End Sub
    End Class
End Namespace
