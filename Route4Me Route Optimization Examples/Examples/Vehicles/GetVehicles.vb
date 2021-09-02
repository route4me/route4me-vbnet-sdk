Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get Vehicles List
        ''' </summary>
        Public Sub GetVehicles()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim vehicleParameters = New VehicleParameters With {
                .WithPagination = True,
                .Page = 1,
                .PerPage = 10
            }

            Dim errorString As String = Nothing
            Dim vehicles As V5.Vehicle() = route4Me.GetVehicles(vehicleParameters, errorString)

            PrintTestVehciles(vehicles, errorString)

        End Sub
    End Class
End Namespace
