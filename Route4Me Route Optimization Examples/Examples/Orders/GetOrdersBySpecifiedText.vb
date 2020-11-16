Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Orders be containing specified text in any text field.
        ''' </summary>
        ''' <param name="query">The query text</param>
        Public Sub GetOrdersBySpecifiedText(Optional query As String = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(
                query Is Nothing,
                True,
                False
            )

            If isInnerExample Then
                CreateExampleOrder()
                query = "Carol"
            End If

            Dim oParams = New OrderParameters() With {
                .query = query,
                .offset = 0,
                .limit = 20
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.SearchOrders(oParams, errorString)

            PrintExampleOrder(result, errorString)

            If isInnerExample Then RemoveTestOrders()
        End Sub
    End Class
End Namespace