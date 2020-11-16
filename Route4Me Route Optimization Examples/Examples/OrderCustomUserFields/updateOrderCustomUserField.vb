Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update an order custom user field. 
        ''' </summary>
        Public Sub updateOrderCustomUserField()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestOrderCustomUserField()

            Dim orderCustomFieldId = OrderCustomFieldsToRemove(OrderCustomFieldsToRemove.Count - 1)

            Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
                .OrderCustomFieldId = orderCustomFieldId,
                .OrderCustomFieldLabel = "Custom Field 55",
                .OrderCustomFieldType = "checkbox",
                .OrderCustomFieldTypeInfo = New Dictionary(Of String, Object)() From {
                    {"short_label", "cFl55"},
                    {"description", "This is updated test order custom field"},
                    {"custom field no", 12}
                }
            }

            Dim errorString As String = Nothing
            Dim orderCustomUserField = route4Me.UpdateOrderCustomUserField(
                orderCustomFieldParams,
                errorString
            )

            PrintOrderCustomField(orderCustomUserField, errorString)

            RemoveTestOrderCustomField()
        End Sub
    End Class
End Namespace
