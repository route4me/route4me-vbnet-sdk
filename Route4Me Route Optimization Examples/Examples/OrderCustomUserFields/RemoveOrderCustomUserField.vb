Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove an order custom user field.
        ''' </summary>
        Public Sub RemoveOrderCustomUserField()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestOrderCustomUserField()

            Dim orderCustomFieldId As Integer = OrderCustomFieldsToRemove(
                OrderCustomFieldsToRemove.Count - 1
            )

            Dim orderCustomFieldParams = New OrderCustomFieldParameters() With {
                .OrderCustomFieldId = orderCustomFieldId
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.RemoveOrderCustomUserField(
                orderCustomFieldParams,
                errorString
            )

            PrintOrderCustomField(result, errorString)
        End Sub
    End Class
End Namespace
