Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove Orders
        ''' </summary>
        ''' <param name="orderIds"> Order Ids </param>
        Public Sub RemoveOrders(orderIds As String())
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim removed As Boolean = route4Me.RemoveOrders(orderIds, errorString)

            Console.WriteLine("")

            If removed Then
                Console.WriteLine("RemoveOrders executed successfully, {0} orders removed", orderIds.Length)
            Else
                Console.WriteLine("RemoveOrders error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
