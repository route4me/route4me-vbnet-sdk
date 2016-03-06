Imports Route4MeSDK.DataTypes
Imports System
'Imports System.Linq
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Const c_ApiKey As String = "11111111111111111111111111111111"

        Private Sub PrintExampleOptimizationResult(exampleName As String, dataObject As DataObject, errorString As String)
            Dim err1 As String
            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("{0} executed successfully", exampleName)
                Console.WriteLine("")

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("State: {0}", dataObject.State)
                For Each err1 In dataObject.UserErrors
                    Console.WriteLine("UserError : '{0}'", err1)
                Next

                Console.WriteLine("")
                For Each address As Address In dataObject.Addresses
                    Console.WriteLine("Address: {0}", address.AddressString)
                    Console.WriteLine("Route ID: {0}", address.RouteId)
                Next
            Else
                Console.WriteLine("{0} error {1}", exampleName, errorString)
            End If
        End Sub
    End Class
End Namespace
