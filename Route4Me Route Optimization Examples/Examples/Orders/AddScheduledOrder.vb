﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Scheduled Order
        ''' </summary>
        Public Sub AddScheduledOrder()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim orderParams = New Order() With {
                .address_1 = "318 S 39th St, Louisville, KY 40212, USA",
                .cached_lat = 38.259326,
                .cached_lng = -85.814979,
                .curbside_lat = 38.259326,
                .curbside_lng = -85.814979,
                .address_alias = "318 S 39th St 40212",
                .address_city = "Louisville",
                .EXT_FIELD_first_name = "Lui",
                .EXT_FIELD_last_name = "Carol",
                .EXT_FIELD_email = "lcarol654@yahoo.com",
                .EXT_FIELD_phone = "897946541",
                .EXT_FIELD_custom_data = New Dictionary(Of String, String)() From {
                    {"order_type", "scheduled order"}
                },
                .day_scheduled_for_YYMMDD = "2017-12-20",
                .local_time_window_end = 39000,
                .local_time_window_end_2 = 46200,
                .local_time_window_start = 37800,
                .local_time_window_start_2 = 45000,
                .local_timezone_string = "America/New_York",
                .order_icon = "emoji/emoji-bank"
            }

            Dim errorString As String = Nothing
            Dim newOrder = route4Me.AddOrder(orderParams, errorString)

            PrintExampleOrder(newOrder, errorString)

            If newOrder IsNot Nothing AndAlso newOrder.[GetType]() = GetType(Order) Then
                OrdersToRemove = New List(Of String)() From {
                    newOrder.order_id.ToString()
                }
            End If

            RemoveTestOrders()
        End Sub
    End Class
End Namespace