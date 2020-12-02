Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of deleting a vehicle.
        ''' </summary>
        Public Sub DeleteVehicle()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestVehcile()

            Dim vehicleParams = New VehicleV4Parameters() With {
                .VehicleId = vehiclesToRemove(vehiclesToRemove.Count - 1)
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim vehicles = route4Me.deleteVehicle(vehicleParams, errorString)

            PrintTestVehciles(vehicles, errorString)
        End Sub
    End Class
End Namespace