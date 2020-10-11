Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    <DataContract>
    Public NotInheritable Class VehicleV4Parameters
        Inherits GenericParameters

        <IgnoreDataMember>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As String

        <DataMember(Name:="is_deleted", EmitDefaultValue:=False)>
        Public Property IsDeleted As Boolean?

        ''' <summary>
        ''' Required for a vehicle creating.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="vehicle_name", EmitDefaultValue:=False)>
        Public Property VehicleName As String

        <DataMember(Name:="vehicle_alias", EmitDefaultValue:=False)>
        Public Property VehicleAlias As String

        <DataMember(Name:="vehicle_vin", EmitDefaultValue:=False)>
        Public Property VehicleVin As String

        <DataMember(Name:="created_time", EmitDefaultValue:=False)>
        Public Property CreatedTime As String

        <DataMember(Name:="vehicle_reg_state_id", EmitDefaultValue:=False)>
        Public Property VehicleRegStateId As String

        <DataMember(Name:="vehicle_reg_country_id", EmitDefaultValue:=False)>
        Public Property VehicleRegCountryId As Integer?

        <DataMember(Name:="vehicle_license_plate", EmitDefaultValue:=False)>
        Public Property VehicleLicensePlate As String

        <DataMember(Name:="vehicle_type_id", EmitDefaultValue:=False)>
        Public Property VehicleTypeID As String

        <DataMember(Name:="timestamp_added", EmitDefaultValue:=False)>
        Public Property TimestampAdded As String

        <DataMember(Name:="vehicle_make", EmitDefaultValue:=False)>
        Public Property VehicleMake As String

        <DataMember(Name:="vehicle_model_year", EmitDefaultValue:=False)>
        Public Property VehicleModelYear As Integer?

        <DataMember(Name:="vehicle_model", EmitDefaultValue:=False)>
        Public Property VehicleModel As String

        <DataMember(Name:="vehicle_year_acquired", EmitDefaultValue:=False)>
        Public Property VehicleYearAcquired As Integer?

        <DataMember(Name:="vehicle_cost_new", EmitDefaultValue:=False)>
        Public Property VehicleCostNew As Double?

        <DataMember(Name:="purchased_new", EmitDefaultValue:=False)>
        Public Property PurchasedNew As Boolean?

        <DataMember(Name:="license_start_date", EmitDefaultValue:=False)>
        Public Property LicenseStartDate As String

        <DataMember(Name:="license_end_date", EmitDefaultValue:=False)>
        Public Property LicenseEndDate As String

        ''' <summary>
        ''' Vehicle axle count.
        ''' Available values: 2,3,4
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="vehicle_axle_count", EmitDefaultValue:=False)>
        Public Property VehicleAxleCount As Integer?

        <DataMember(Name:="mpg_city", EmitDefaultValue:=False)>
        Public Property MpgCity As Integer?

        <DataMember(Name:="mpg_highway", EmitDefaultValue:=False)>
        Public Property MpgHighway As Integer?

        <DataMember(Name:="fuel_type", EmitDefaultValue:=False)>
        Public Property FuelType As String

        <DataMember(Name:="height_inches", EmitDefaultValue:=False)>
        Public Property HeightInches As Integer?

        <DataMember(Name:="weight_lb", EmitDefaultValue:=False)>
        Public Property WeightLb As Integer?

        <DataMember(Name:="is_operational", EmitDefaultValue:=False)>
        Public Property IsOperational As String

        <DataMember(Name:="external_telematics_vehicle_id", EmitDefaultValue:=False)>
        Public Property ExternalTelematicsVehicleID As String

        <DataMember(Name:="has_trailer", EmitDefaultValue:=False)>
        Public Property HasTrailer As Boolean

        <DataMember(Name:="heightInInches", EmitDefaultValue:=False)>
        Public Property HeightInInches As String

        <DataMember(Name:="lengthInInches", EmitDefaultValue:=False)>
        Public Property LengthInInches As Integer?

        <DataMember(Name:="widthInInches", EmitDefaultValue:=False)>
        Public Property WidthInInches As Integer?

        <DataMember(Name:="maxWeightPerAxleGroupInPounds", EmitDefaultValue:=False)>
        Public Property MaxWeightPerAxleGroupInPounds As Integer?

        <DataMember(Name:="numAxles", EmitDefaultValue:=False)>
        Public Property NumAxles As Integer?

        <DataMember(Name:="weightInPounds", EmitDefaultValue:=False)>
        Public Property WeightInPounds As Integer?

        <DataMember(Name:="HazmatType", EmitDefaultValue:=False)>
        Public Property HazmatType As String

        <DataMember(Name:="LowEmissionZonePref", EmitDefaultValue:=False)>
        Public Property LowEmissionZonePref As String

        <DataMember(Name:="Use53FootTrailerRouting", EmitDefaultValue:=False)>
        Public Property Use53FootTrailerRouting As String

        <DataMember(Name:="UseNationalNetwork", EmitDefaultValue:=False)>
        Public Property UseNationalNetwork As String

        <DataMember(Name:="UseTruckRestrictions", EmitDefaultValue:=False)>
        Public Property UseTruckRestrictions As String

        <DataMember(Name:="AvoidFerries", EmitDefaultValue:=False)>
        Public Property AvoidFerries As String

        <DataMember(Name:="DividedHighwayAvoidPreference", EmitDefaultValue:=False)>
        Public Property DividedHighwayAvoidPreference As String

        <DataMember(Name:="FreewayAvoidPreference", EmitDefaultValue:=False)>
        Public Property FreewayAvoidPreference As String

        <DataMember(Name:="InternationalBordersOpen", EmitDefaultValue:=False)>
        Public Property InternationalBordersOpen As String

        <DataMember(Name:="TollRoadUsage", EmitDefaultValue:=False)>
        Public Property TollRoadUsage As String

        <DataMember(Name:="hwy_only", EmitDefaultValue:=False)>
        Public Property HwyOnly As Boolean

        <DataMember(Name:="long_combination_vehicle", EmitDefaultValue:=False)>
        Public Property LongCombinationVehicle As Boolean

        <DataMember(Name:="avoid_highways", EmitDefaultValue:=False)>
        Public Property AvoidHighways As Boolean

        <DataMember(Name:="side_street_adherence", EmitDefaultValue:=False)>
        Public Property SideStreetAdherence As String

        <DataMember(Name:="truck_config", EmitDefaultValue:=False)>
        Public Property TruckConfig As String

        <DataMember(Name:="height_metric", EmitDefaultValue:=False)>
        Public Property HeightMetric As Single?

        <DataMember(Name:="length_metric", EmitDefaultValue:=False)>
        Public Property LengthMetric As Single?

        <DataMember(Name:="width_metric", EmitDefaultValue:=False)>
        Public Property WidthMetric As Single?

        <DataMember(Name:="weight_metric", EmitDefaultValue:=False)>
        Public Property WeightMetric As Single?

        <DataMember(Name:="max_weight_per_axle_group_metric", EmitDefaultValue:=False)>
        Public Property MaxWeightPerAxleGroupMetric As Single?

        <DataMember(Name:="timestamp_removed", EmitDefaultValue:=False)>
        Public Property TimestampRemoved As String
    End Class
End Namespace