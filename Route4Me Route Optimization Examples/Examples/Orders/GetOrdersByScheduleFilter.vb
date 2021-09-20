Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get orders by schedule filter
        ''' </summary>
        Public Sub GetOrdersByScheduleFilter()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateExampleOrder()

            Dim startDate As String = (DateTime.Now - (New TimeSpan(1, 0, 0, 0))).ToString("yyyy-MM-dd")
            Dim endDate As String = (DateTime.Now + (New TimeSpan(31, 0, 0, 0))).ToString("yyyy-MM-dd")

            Dim oParams = New OrderFilterParameters() With {
                .Limit = 10,
                .Filter = New FilterDetails() With {
                    .Display = "all",
                    .Scheduled_for_YYYYMMDD = New String() {startDate, endDate}
                }
            }

            Dim errorString As String = Nothing
            Dim orders As Order() = route4Me.FilterOrders(oParams, errorString)

            PrintExampleOrder(orders, errorString)

            RemoveTestOrders()
        End Sub
    End Class
End Namespace
