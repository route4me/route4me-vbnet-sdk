Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetVehicles()
            Dim route4Me As Route4MeManager = New Route4MeManager(ActualApiKey)
            Dim vehicleParameters As VehicleParameters = New VehicleParameters With {
                .WithPagination = True,
                .Page = 1,
                .PerPage = 10
            }
            Dim errorString As String = ""
            Dim vehicles As VehiclesPaginated = route4Me.GetVehicles(vehicleParameters, errorString)
            Console.WriteLine("")

            If vehicles IsNot Nothing Then
                Console.WriteLine("GetVehicles executed successfully, {0} vehicles returned", vehicles.Total)
                Console.WriteLine("")

                For Each vehicle As VehicleV4Response In vehicles.Data
                    Console.WriteLine("Vehicle ID: {0}", vehicle.VehicleId)
                Next

                Console.WriteLine("")
            Else
                Console.WriteLine("GetVehicles error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
