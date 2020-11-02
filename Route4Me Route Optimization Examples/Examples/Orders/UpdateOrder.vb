Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update Order
        ''' </summary>
        ''' <param name="order"> Order with updated attributes </param>
        Public Sub UpdateOrder(order As Order)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim updatedOrder As Order = route4Me.UpdateOrder(order, errorString)

            Console.WriteLine("")

            If updatedOrder IsNot Nothing Then
                Console.WriteLine("UpdateOrder executed successfully")
            Else
                Console.WriteLine("UpdateOrder error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace

