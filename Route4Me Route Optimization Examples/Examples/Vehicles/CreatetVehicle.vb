Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of creating a new vehicle.
        ''' </summary>
        Public Sub CreatetVehicle()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim class7TruckParams = New VehicleV4Parameters() With {
                .VehicleName = "FORD F750",
                .VehicleAlias = "FORD F750",
                .VehicleVin = "1NPAX6EX2YD550743",
                .VehicleLicensePlate = "FFV9547",
                .VehicleModel = "F-750",
                .VehicleModelYear = 2010,
                .VehicleYearAcquired = 2018,
                .VehicleRegCountryId = 223,
                .VehicleMake = "Ford",
                .VehicleTypeID = "livestock_carrier",
                .VehicleAxleCount = 2,
                .MpgCity = 8,
                .MpgHighway = 15,
                .FuelType = "diesel",
                .HeightInches = 96,
                .HeightMetric = 244,
                .WeightLb = 26000,
                .MaxWeightPerAxleGroupInPounds = 15000,
                .MaxWeightPerAxleGroupMetric = 6800,
                .WidthInInches = 96,
                .WidthMetric = 240,
                .LengthInInches = 312,
                .LengthMetric = 793,
                .Use53FootTrailerRouting = "NO",
                .UseTruckRestrictions = "YES",
                .DividedHighwayAvoidPreference = "FAVOR",
                .FreewayAvoidPreference = "NEUTRAL",
                .TruckConfig = "26_STRAIGHT_TRUCK",
                .TollRoadUsage = "ALWAYS_AVOID",
                .InternationalBordersOpen = "NO",
                .PurchasedNew = True
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim result = route4Me.CreateVehicle(class7TruckParams, errorString)

            PrintTestVehciles(result, errorString)

            If result IsNot Nothing AndAlso result.[GetType]() = GetType(VehicleV4CreateResponse) Then
                Console.WriteLine("The test vehicle {0} created successfully.", result.VehicleGuid)

                vehiclesToRemove.Add(result.VehicleGuid)

                RemoveTestVehicles()
            End If

        End Sub
    End Class
End Namespace