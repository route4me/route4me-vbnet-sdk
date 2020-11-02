Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub RemoveAddressFromOptimization(optimizationId As String, destinationId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim deleted As Boolean = route4Me.RemoveAddressFromOptimization(optimizationId, destinationId, errorString)

            Console.WriteLine("")

            If deleted Then
                Console.WriteLine("RemoveAddressFromOptimizatio executed successfully")

                Console.WriteLine("Destination ID: {0}", destinationId)
            Else
                Console.WriteLine("RemoveAddressFromOptimizatio error: {0}", errorString)
            End If

        End Sub
    End Class
End Namespace

