Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get schedule calendar of the contacts, orders, routes.
        ''' </summary>
        Public Sub GetScheduleCalendar()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString0 As String = Nothing
            If Not route4Me.MemberHasCommercialCapability(
                                        ActualApiKey,
                                        DemoApiKey,
                                        errorString0) Then Return

            Dim days5 As TimeSpan = New TimeSpan(5, 0, 0, 0)

            Dim calendarQuery = New ScheduleCalendarQuery() With {
                .DateFromString = (DateTime.Now - days5).ToString("yyyy-MM-dd"),
                .DateToString = (DateTime.Now + days5).ToString("yyyy-MM-dd"),
                .TimezoneOffsetMinutes = 4 * 60,
                .ShowOrders = True,
                .ShowContacts = True,
                .RoutesCount = True
            }

            Dim errorString As String = Nothing
            Dim scheduleCalendar = route4Me.GetScheduleCalendar(calendarQuery, errorString)

            Console.WriteLine(If((If(scheduleCalendar?.AddressBook, Nothing)) Is Nothing OrElse (If(scheduleCalendar?.Orders, Nothing)) Is Nothing OrElse (If(scheduleCalendar?.RoutesCount, Nothing)) Is Nothing, "GetScheduleCalendarTest failed", "GetScheduleCalendar executed successfully"))
        End Sub
    End Class
End Namespace