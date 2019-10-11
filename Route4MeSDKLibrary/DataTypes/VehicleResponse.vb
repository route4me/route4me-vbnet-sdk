Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class VehicleResponse
        Inherits GenericParameters

        <DataMember(Name:="vehicle_id")>
        Public Property VehicleId As String

        <DataMember(Name:="created_time")>
        Public Property CreatedTime As String

        <DataMember(Name:="member_id")>
        Public Property MemberId As String

        <DataMember(Name:="vehicle_alias")>
        Public Property VehicleAlias As String

        <DataMember(Name:="vehicle_vin")>
        Public Property VehicleVin As String

        <DataMember(Name:="vehicle_reg_state")>
        Public Property VehicleRegState As String

        <DataMember(Name:="vehicle_reg_state_id")>
        Public Property VehicleRegStateId As Integer?

        <DataMember(Name:="vehicle_reg_country")>
        Public Property VehicleRegCountry As String

        <DataMember(Name:="vehicle_reg_country_id")>
        Public Property VehicleRegCountryId As Integer?

        <DataMember(Name:="vehicle_license_plate")>
        Public Property VehicleLicensePlate As String

        <DataMember(Name:="vehicle_make")>
        Public Property VehicleMake As String

        <DataMember(Name:="vehicle_model_year")>
        Public Property VehicleModelYear As Integer?

        <DataMember(Name:="vehicle_model")>
        Public Property VehicleModel As String

        <DataMember(Name:="vehicle_year_acquired")>
        Public Property VehicleYearAcquired As Integer?

        <DataMember(Name:="vehicle_cost_new")>
        Public Property VehicleCostNew As Double?

        <DataMember(Name:="license_start_date")>
        Public Property LicenseStartDate As String

        <DataMember(Name:="license_end_date")>
        Public Property LicenseEndDate As String

        <DataMember(Name:="vehicle_axle_count")>
        Public Property VehicleAxleCount As Integer?

        <DataMember(Name:="mpg_city")>
        Public Property MpgCity As Double?

        <DataMember(Name:="mpg_highway")>
        Public Property MpgHighway As Double?

        <DataMember(Name:="fuel_type")>
        Public Property FuelType As String

        <DataMember(Name:="height_inches")>
        Public Property HeightInches As Double?

        <DataMember(Name:="weight_lb")>
        Public Property WeightLb As Double?

    End Class
End Namespace
