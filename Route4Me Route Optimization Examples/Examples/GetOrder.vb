Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Orders
        ''' </summary>
        Public Sub GetOrders()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim orderParameters As New OrderParameters() With { _
                .Limit = 10 _
            }

            Dim total As UInteger
            Dim errorString As String = ""
            Dim orders As Order() = route4Me.GetOrders(orderParameters, total, errorString)

            Console.WriteLine("")

            If orders IsNot Nothing Then
                Console.WriteLine("GetOrders executed successfully, {0} orders returned, total = {1}", orders.Length, total)
            Else
                Console.WriteLine("GetOrders error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
