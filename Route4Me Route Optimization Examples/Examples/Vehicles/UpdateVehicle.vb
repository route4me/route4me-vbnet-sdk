Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of updating a vehicle.
        ''' </summary>
        Public Sub UpdateVehicle()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestVehcile()

            ' TO DO: on this stage specifying of the parameter vehicle_alias is mandatory. Will be checked later
            Dim vehicleParams = New VehicleV4Parameters() With {
                .VehicleModelYear = 1995,
                .VehicleYearAcquired = 2018,
                .VehicleMake = "Ford",
                .VehicleAxleCount = 2,
                .FuelType = "unleaded 93",
                .HeightInches = 72,
                .WeightLb = 2000
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim updatedVehicle = route4Me.updateVehicle(
                                            vehicleParams,
                                            vehiclesToRemove(vehiclesToRemove.Count - 1),
                                            errorString)

            PrintTestVehciles(updatedVehicle, errorString)

            RemoveTestVehicles()
        End Sub
    End Class
End Namespace