Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManager

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Orders by Inserted Date
        ''' </summary>
        Public Sub GetOrdersByInsertedDate()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateExampleOrder()

            Dim InsertedDate As String = DateTime.Now.ToString("yyyy-MM-dd")

            Dim oParams = New OrderParameters With {
                .day_added_YYMMDD = InsertedDate
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.SearchOrders(oParams, errorString)

            If result IsNot Nothing AndAlso result.[GetType]() = GetType(GetOrdersResponse) Then
                OrdersToRemove = New List(Of String)()

                For Each ord As Order In (CType(result, GetOrdersResponse)).Results
                    OrdersToRemove.Add(ord.order_id.ToString())
                Next
            End If

            PrintExampleOrder(result, errorString)
            RemoveTestOrders()
        End Sub
    End Class
End Namespace
