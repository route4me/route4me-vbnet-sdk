Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update an optimization destination
        ''' </summary>
        Public Sub UpdateOptimizationDestination()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)()
            OptimizationsToRemove.Add(SD10Stops_optimization_problem_id)

            Dim address = SD10Stops_route.Addresses(3)
            address.FirstName = "UpdatedFirstName"
            address.LastName = "UpdatedLastName"

            Dim errorString As String = Nothing
            Dim updatedAddress = route4Me.UpdateOptimizationDestination(address, errorString)

            PrintExampleDestination(updatedAddress, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
