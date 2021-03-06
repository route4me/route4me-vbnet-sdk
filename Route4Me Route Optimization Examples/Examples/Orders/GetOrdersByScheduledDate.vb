﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManager

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Orders by Scheduled Date
        ''' </summary>
        Public Sub GetOrdersByScheduledDate(Optional ScheduleddDate As String = Nothing)
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(
                ScheduleddDate Is Nothing,
                True,
                False
            )

            If isInnerExample Then
                CreateExampleOrder()
                ScheduleddDate = DateTime.Now.ToString("yyyy-MM-dd")
            End If

            Dim oParams = New OrderParameters With {
                .scheduled_for_YYMMDD = ScheduleddDate
            }

            Dim errorString As String = Nothing
            Dim result = route4Me.SearchOrders(oParams, errorString)

            PrintExampleOrder(result, errorString)

            If isInnerExample AndAlso
                result IsNot Nothing AndAlso
                result.[GetType]() = GetType(GetOrdersResponse) Then

                OrdersToRemove = New List(Of String)()

                For Each ord As Order In (CType(result, GetOrdersResponse)).Results
                    OrdersToRemove.Add(ord.order_id.ToString())
                Next

                RemoveTestOrders()
            End If
        End Sub
    End Class
End Namespace