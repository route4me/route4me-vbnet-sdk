Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetVehicles()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim vehicleParameters As New VehicleParameters() With { _
                .Limit = 10, _
                .Offset = 0 _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim vehicles As VehicleResponse() = route4Me.GetVehicles(vehicleParameters, errorString)

            Console.WriteLine("")

            If vehicles IsNot Nothing Then
                Console.WriteLine("GetVehicles executed successfully, {0} vehicles returned", vehicles.Length)
                Console.WriteLine("")

                For Each vehicle As VehicleResponse In vehicles
                    Console.WriteLine("Vehicle ID: {0}", vehicle.VehicleId)
                Next

                Console.WriteLine("")
            Else
                Console.WriteLine("GetVehicles error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
