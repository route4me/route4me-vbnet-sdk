Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Orders by Custom Fields
        ''' </summary>
        Public Sub GetOrdersByCustomFields(Optional CustomFields As String = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim oParams = New OrderParameters() With {
                .fields = If(
                    CustomFields Is Nothing,
                    "order_id,member_id",
                    CustomFields),
                .offset = 0,
                .limit = 20
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.SearchOrders(oParams, errorString)

            PrintExampleOrder(result, errorString)
        End Sub
    End Class
End Namespace