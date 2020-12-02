Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of getting a vehicle using the vehicleID path parameter.
        ''' </summary>
        Public Sub GetVehicle()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateTestVehcile()

            Dim vehicleParams = New VehicleParameters() With {
                .VehicleId = vehiclesToRemove(vehiclesToRemove.Count - 1)
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim vehicle = route4Me.GetVehicle(vehicleParams, errorString)

            PrintTestVehciles(vehicle, errorString)

            RemoveTestVehicles()
        End Sub
    End Class
End Namespace