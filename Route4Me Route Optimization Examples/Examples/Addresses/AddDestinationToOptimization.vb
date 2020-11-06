Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add a destination to an optimization.
        ''' </summary>
        Public Sub AddDestinationToOptimization()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            ' Prepare the address that we are going to add to an existing route optimization
            Dim addresses As Address() = New Address() {New Address() With {
                .AddressString = "717 5th Ave New York, NY 10021",
                .[Alias] = "Giorgio Armani",
                .Latitude = 40.7669692,
                .Longitude = -73.9693864,
                .Time = 0
            }}

            ' Optionally change any route parameters, such as maximum route duration, maximum cubic constraints, etc.
            Dim optimizationParameters As OptimizationParameters = New OptimizationParameters() With {
                .OptimizationProblemID = SD10Stops_optimization_problem_id,
                .Addresses = addresses,
                .ReOptimize = True
            }

            Dim errorString As String = Nothing
            Dim dataObject As DataObject = route4Me.UpdateOptimization(optimizationParameters, errorString)

            PrintExampleOptimizationResult("AddDestinationToOptimization", dataObject, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace