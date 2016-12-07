Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddDestinationToOptimization(optimizationProblemID As String, andReOptimize As Boolean)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the address that we are going to add to an existing route optimization
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "717 5th Ave New York, NY 10021", _
                .[Alias] = "Giorgio Armani", _
                .Latitude = 40.7669692, _
                .Longitude = -73.9693864, _
                .Time = 0 _
            }}

            'Optionally change any route parameters, such as maximum route duration, maximum cubic constraints, etc.
            Dim optimizationParameters As New OptimizationParameters() With { _
                .OptimizationProblemID = optimizationProblemID, _
                .Addresses = addresses, _
                .ReOptimize = andReOptimize _
            }

            ' Execute the optimization to re-optimize and rebalance all the routes in this optimization
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.UpdateOptimization(optimizationParameters, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("AddDestinationToOptimization executed successfully")

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("State: {0}", dataObject.State)
            Else
                Console.WriteLine("AddDestinationToOptimization error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
