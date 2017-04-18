Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add Order
        ''' </summary>
        ''' <returns> Added Order </returns>
        Public Function AddOrder() As Order
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim order As New Order() With { _
                .address_1 = "Test Address1 " + (New Random()).[Next]().ToString(), _
                .address_alias = "Test AddressAlias " + (New Random()).[Next]().ToString(), _
                .cached_lat = 37.773972, _
                .cached_lng = -122.431297 _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim resultOrder As Order = route4Me.AddOrder(order, errorString)

            Console.WriteLine("")

            If resultOrder IsNot Nothing Then
                Console.WriteLine("AddOrder executed successfully")

                Console.WriteLine("Order ID: {0}", resultOrder.order_id)

                Return resultOrder
            Else
                Console.WriteLine("AddOrder error: {0}", errorString)

                Return Nothing
            End If
        End Function
    End Class
End Namespace
