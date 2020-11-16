Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Create an order custom user field.
        ''' </summary>
        Public Sub CreateOrderCustomUserField()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
                .OrderCustomFieldName = "CustomField77",
                .OrderCustomFieldLabel = "Custom Field 77",
                .OrderCustomFieldType = "checkbox",
                .OrderCustomFieldTypeInfo = New Dictionary(Of String, Object)() From {
                    {"short_label", "cFl77"},
                    {"description", "This is test order custom field"},
                    {"custom field no", 11}
                }
            }

            Dim errorString As String = Nothing
            Dim orderCustomUserField = route4Me.CreateOrderCustomUserField(
                orderCustomFieldParams,
                errorString
            )

            PrintOrderCustomField(orderCustomUserField, errorString)

            If orderCustomUserField IsNot Nothing AndAlso
                orderCustomUserField.[GetType]() = GetType(OrderCustomFieldCreateResponse) AndAlso
                orderCustomUserField.Data IsNot Nothing Then

                OrderCustomFieldsToRemove.Add(orderCustomUserField.Data.OrderCustomFieldId)

            End If

            RemoveTestOrderCustomField()
        End Sub
    End Class
End Namespace
