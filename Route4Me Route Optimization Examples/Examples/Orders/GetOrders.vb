Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get limited number of the Orders
        ''' </summary>
        Public Sub GetOrders()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim orderParameters = New OrderParameters() With {
                .limit = 10
            }

            Dim total As UInteger = Nothing
            Dim errorString As String = Nothing

            Dim orders As Order() = route4Me.GetOrders(
                orderParameters,
                total,
                errorString
            )

            PrintExampleOrder(orders, errorString)
        End Sub
    End Class
End Namespace