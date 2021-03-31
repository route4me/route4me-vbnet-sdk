Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSdkV5UnitTest.Route4MeSdkV5UnitTest.V5
Imports Xunit
Imports Xunit.Abstractions

Namespace Route4MeSdkV5UnitTest.VehiclesApi
    Public Class VehiclesApiTests
        Implements IDisposable

        Shared c_ApiKey As String = ApiKeys.actualApiKey
        Private ReadOnly _output As ITestOutputHelper
        Shared lsVehicles As List(Of Vehicle)
        Shared lsVehicleProfiles As List(Of VehicleProfile)
        Shared preferedUnit As String

        Public Sub New(ByVal output As ITestOutputHelper)
            _output = output
            lsVehicles = New List(Of Vehicle)()

            Dim class6TruckParams = New Vehicle() With {
                .VehicleAlias = "GMC TopKick C5500 TST 6",
                .VehicleVin = "SAJXA01A06FN08012",
                .VehicleLicensePlate = "CVH4561",
                .VehicleModel = "TopKick C5500",
                .VehicleModelYear = 1995,
                .VehicleYearAcquired = 2008,
                .VehicleRegCountryId = 223,
                .VehicleRegStateId = 12,
                .VehicleMake = "GMC",
                .VehicleTypeId = "pickup_truck",
                .VehicleCostNew = 60000,
                .PurchasedNew = True,
                .MpgCity = 7,
                .MpgHighway = 14,
                .FuelConsumptionCity = 7,
                .FuelConsumptionHighway = 14,
                .FuelType = "diesel",
                .LicenseStartDate = "2021-01-01",
                .LicenseEndDate = "2031-01-01"
            }

            Dim class6Truck = createVehicle(class6TruckParams)

            Assert.NotNull(class6Truck)
            Assert.IsType(Of Vehicle)(class6Truck)

            lsVehicles.Add(class6Truck)

            Dim class7TruckParams = New Vehicle() With {
                .VehicleAlias = "FORD F750 TST 7",
                .VehicleVin = "1NPAX6EX2YD550743",
                .VehicleLicensePlate = "FFV9547",
                .VehicleModel = "F-750",
                .VehicleModelYear = 2010,
                .VehicleYearAcquired = 2018,
                .VehicleRegCountryId = 223,
                .VehicleMake = "Ford",
                .VehicleTypeId = "livestock_carrier",
                .VehicleCostNew = 60000,
                .PurchasedNew = True,
                .MpgCity = 7,
                .MpgHighway = 14,
                .FuelConsumptionCity = 7,
                .FuelConsumptionHighway = 14,
                .FuelType = "diesel",
                .LicenseStartDate = "2021-01-01",
                .LicenseEndDate = "2031-01-01"
            }

            Dim class7Truck = createVehicle(class7TruckParams)

            Assert.NotNull(class7Truck)
            Assert.IsType(Of Vehicle)(class7Truck)

            lsVehicles.Add(class7Truck)
            lsVehicleProfiles = New List(Of VehicleProfile)()

            Dim route4me = New Route4MeManagerV5(c_ApiKey)
            Dim resultResponse As ResultResponse = Nothing

            preferedUnit = (If(route4me.GetAccountPreferedUnit(resultResponse)?.ToLower(), "mi"))

            Dim vehProfileParams1 = New VehicleProfile() With {
                .Name = "Heavy Duty - 28 Double Trailer " & DateTime.Now.ToString("yyMMddHHmmss"),
                .IsPredefined = False,
                .IsDefault = False,
                .Height = If(preferedUnit = "mi", 4 * 3.28, 4),
                .Width = If(preferedUnit = "mi", 2.44 * 3.28, 2.44),
                .Length = If(preferedUnit = "mi", 12.2 * 3.28, 12.2),
                .WeightUnits = VehicleWeightUnits.Kilogram.GetEnumDescription(),
                .Weight = 20400,
                .MaxWeightPerAxle = 15400,
                .FuelType = FuelTypes.Unleaded_91.GetEnumDescription(),
                .FuelConsumptionCity = 6,
                .FuelConsumptionHighway = 12,
                .FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription(),
                .FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription()
            }

            Dim resultResponse2 As ResultResponse = Nothing
            Dim vehProfile1 = route4me.CreateVehicleProfile(vehProfileParams1, resultResponse2)

            If vehProfile1 IsNot Nothing AndAlso vehProfile1.[GetType]() = GetType(VehicleProfile) AndAlso vehProfile1.VehicleProfileId > 0 Then
                lsVehicleProfiles.Add(vehProfile1)
            End If

            Dim vehProfileParams2 = New VehicleProfile() With {
                .Name = "Heavy Duty - 40 Straight Truck " & DateTime.Now.ToString("yyMMddHHmmss"),
                .HeightUnits = VehicleSizeUnits.Meter.GetEnumDescription(),
                .WidthUnits = VehicleSizeUnits.Meter.GetEnumDescription(),
                .LengthUnits = VehicleSizeUnits.Meter.GetEnumDescription(),
                .IsPredefined = False,
                .IsDefault = False,
                .Height = 4,
                .Width = 2.44,
                .Length = 14.6,
                .WeightUnits = VehicleWeightUnits.Kilogram.GetEnumDescription(),
                .Weight = 36300,
                .MaxWeightPerAxle = 15400,
                .FuelType = FuelTypes.Unleaded_87.GetEnumDescription(),
                .FuelConsumptionCity = 5,
                .FuelConsumptionHighway = 10,
                .FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription(),
                .FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription()
            }

            Dim resultResponse3 As ResultResponse = Nothing
            Dim vehProfile2 = route4me.CreateVehicleProfile(vehProfileParams2, resultResponse3)

            If vehProfile2 IsNot Nothing AndAlso vehProfile2.[GetType]() = GetType(VehicleProfile) AndAlso vehProfile2.VehicleProfileId > 0 Then
                lsVehicleProfiles.Add(vehProfile2)
            End If
        End Sub

        <Fact>
        Public Sub GetVehiclesPaginatedListTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehParams = New VehicleParameters() With {
                .Page = 1,
                .PerPage = 10
            }

            Dim resultResponse As ResultResponse = Nothing

            Dim result = route4Me.GetPaginatedVehiclesList(vehParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Vehicle())(result)
        End Sub

        <Fact>
        Public Sub CreateVehicleTest()
            Dim class7TruckParams = New Vehicle() With {
                .VehicleAlias = "FORD F750",
                .VehicleVin = "1NPAX6EX2YD550743",
                .VehicleLicensePlate = "FFV9547",
                .VehicleModel = "F-750",
                .VehicleModelYear = 2010,
                .VehicleYearAcquired = 2018,
                .VehicleRegCountryId = 223,
                .VehicleMake = "Ford",
                .VehicleTypeId = "livestock_carrier",
                .VehicleRegStateId = 12,
                .VehicleCostNew = 70000,
                .PurchasedNew = False,
                .MpgCity = 6,
                .MpgHighway = 12,
                .FuelConsumptionCity = 6,
                .FuelConsumptionHighway = 12,
                .FuelType = "diesel",
                .LicenseStartDate = "2020-03-01",
                .LicenseEndDate = "2028-12-01"
            }

            Dim class7Truck = createVehicle(class7TruckParams)

            Assert.NotNull(class7Truck)
            Assert.IsType(Of Vehicle)(class7Truck)

            lsVehicles.Add(class7Truck)
        End Sub

        Public Function createVehicle(ByVal vehicleParams As Vehicle) As Vehicle
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.CreateVehicle(vehicleParams, resultResponse)

            Assert.NotNull(result)

            Return result
        End Function

        <Fact(Skip:="The endpoint vehicles/assign is enabled for the accounts with the specified features")>
        Public Sub CreateTemporaryVehicle()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim tempVehicleParams = New VehicleTemporary() With {
                .VehicleId = lsVehicles(lsVehicles.Count - 1).VehicleId,
                .AssignedMemberId = "1",
                .ExpiresAt = "2028-12-01",
                .VehicleLicensePlate = "FFV9548",
                .ForceAssignment = True
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.CreateTemporaryVehicle(tempVehicleParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleTemporary)(result)
        End Sub

        <Fact(Skip:="The endpoint vehicles/execute is enabled for the account with the specified features")>
        Public Sub ExecuteVehicleOrder()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehOrderParams = New VehicleOrderParameters() With {
                .VehicleId = lsVehicles(lsVehicles.Count - 1).VehicleId,
                .Latitude = 38.247605,
                .Longitude = -85.746697
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.ExecuteVehicleOrder(vehOrderParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleOrderResponse)(result)
        End Sub

        <Fact>
        Public Sub GetLatestVehicleLocationsTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehicleParams = New VehicleParameters() With {
                .VehicleIDs = lsVehicles.[Select](Function(x) x.VehicleId).ToArray()
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetVehicleLocations(vehicleParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleLocationResponse)(result)
        End Sub

        <Fact>
        Public Sub GetVehicleByIdTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehicleId = lsVehicles(0).VehicleId

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetVehicleById(vehicleId, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Vehicle)(result)
            Assert.Equal(lsVehicles(0).VehicleId, result.VehicleId)
        End Sub

        <Fact>
        Public Sub GetVehicleTrackByIdTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehicleId = lsVehicles(0).VehicleId

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetVehicleTrack(vehicleId, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleTrackResponse)(result)
        End Sub

        <Fact>
        Public Sub DeleteVehicleTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehicleId = lsVehicles(lsVehicles.Count - 1).VehicleId

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.DeleteVehicle(vehicleId, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Vehicle)(result)

            lsVehicles.Remove(lsVehicles(lsVehicles.Count - 1))
        End Sub

        <Fact>
        Public Sub GetPaginatedVehicleProfilesTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim profileParams = New VehicleProfileParameters() With {
                .WithPagination = True,
                .Page = 1,
                .PerPage = 10
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetVehicleProfiles(profileParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleProfilesResponse)(result)
        End Sub

        <Fact>
        Public Sub CreateVehicleProfileTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehProfileParams3 = New VehicleProfile() With {
                .Name = "Heavy Duty - 48 Semitrailer " & DateTime.Now.ToString("yyMMddHHmmss"),
                .Height = If(preferedUnit = "mi", 3.5 * 3.28, 3.5),
                .Width = If(preferedUnit = "mi", 2.5 * 3.28, 2.5),
                .Length = If(preferedUnit = "mi", 16 * 3.28, 16),
                .IsPredefined = False,
                .IsDefault = False,
                .WeightUnits = VehicleWeightUnits.Kilogram.GetEnumDescription(),
                .Weight = 35000,
                .MaxWeightPerAxle = 17500,
                .FuelType = FuelTypes.Unleaded_87.GetEnumDescription(),
                .FuelConsumptionCity = 6,
                .FuelConsumptionHighway = 11,
                .FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription(),
                .FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription()
            }

            Dim resultResponse2 As ResultResponse = Nothing
            Dim result = route4Me.CreateVehicleProfile(vehProfileParams3, resultResponse2)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleProfile)(result)

            lsVehicleProfiles.Add(result)
        End Sub

        <Fact>
        Public Sub DeleteVehicleProfileTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehProfParams = New VehicleProfileParameters() With {
                .VehicleProfileId = lsVehicleProfiles(lsVehicleProfiles.Count - 1).VehicleProfileId
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.DeleteVehicleProfile(vehProfParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Object)(result)

            lsVehicleProfiles.Remove(lsVehicleProfiles(lsVehicleProfiles.Count - 1))
        End Sub

        <Fact>
        Public Sub GetVehicleProfileByIdTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehProfParams = New VehicleProfileParameters() With {
                .VehicleProfileId = lsVehicleProfiles(0).VehicleProfileId
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetVehicleProfileById(vehProfParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleProfile)(result)
        End Sub

        <Fact>
        Public Sub GetVehicleByLicensePlateTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim vehParams = New VehicleParameters() With {
                .VehicleLicensePlate = lsVehicles(0).VehicleLicensePlate
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetVehicleByLicensePlate(vehParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleResponse)(result)
            Assert.Equal(lsVehicles(0).VehicleLicensePlate, (If(result?.Data?.Vehicle?.VehicleLicensePlate, Nothing)))
        End Sub

        <Fact(Skip:="The tested method is temporary deprecated")>
        Public Sub SearchVehiclesTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim searchParams = New VehicleSearchParameters() With {
                .VehicleIDs = New String() {lsVehicles(0).VehicleId},
                .Latitude = 29.748868,
                .Longitude = -95.358473
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.SearchVehicles(searchParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Vehicle())(result)
        End Sub

        <Fact>
        Public Sub UpdateVehicleTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            lsVehicles(0).VehicleAlias += " Updated"
            lsVehicles(0).VehicleVin = "11111111111111111"

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.UpdateVehicle(lsVehicles(0), resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Vehicle)(result)
            Assert.Equal("11111111111111111", result.VehicleVin)
            Assert.Contains("Updated", result.VehicleAlias)
        End Sub

        <Fact>
        Public Sub UpdateVehicleProfileTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            lsVehicleProfiles(0).Name += " Updated"
            lsVehicleProfiles(0).FuelConsumptionCityUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription()
            lsVehicleProfiles(0).FuelConsumptionHighwayUnit = FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription()

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.UpdateVehicleProfile(lsVehicleProfiles(0), resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of VehicleProfile)(result)
            Assert.Equal(FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription(), result.FuelConsumptionCityUnit)
            Assert.Equal(FuelConsumptionUnits.MilesPerGallonUS.GetEnumDescription(), result.FuelConsumptionHighwayUnit)
            Assert.Contains("Updated", result.Name)
        End Sub

        Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)
            Dim resultResponse As ResultResponse = Nothing

            If lsVehicles.Count > 0 Then

                For Each veh In lsVehicles
                    Dim result = route4Me.DeleteVehicle(veh.VehicleId, resultResponse)
                Next
            End If

            'Dim resultResponse As ResultResponse = Nothing

            If lsVehicleProfiles.Count > 0 Then

                For Each vehProf In lsVehicleProfiles
                    Dim vehProfParams = New VehicleProfileParameters() With {
                        .VehicleProfileId = vehProf.VehicleProfileId
                    }
                    Dim result = route4Me.DeleteVehicleProfile(vehProfParams, resultResponse)
                Next
            End If
        End Sub
    End Class
End Namespace
