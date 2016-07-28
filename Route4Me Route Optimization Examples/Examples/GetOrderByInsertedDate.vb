Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get single Order by order_id
        ''' </summary>
        Public Sub GetOrderByInsertedDate(InsertedDate As String)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim oParams As New OrderParameters() With { _
                .DayAddedYYMMDD = InsertedDate _
            }

            Dim errorString As String = ""
            Dim orders As Order() = route4Me.SearchOrders(oParams, errorString)

            Console.WriteLine("")

            If orders IsNot Nothing Then
                Console.WriteLine("GetOrderByInsertedDate executed successfully, orders searched total = {0}", orders.Count)
            Else
                Console.WriteLine("GetOrderByInsertedDate error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
