Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response from the vehicle request
    ''' </summary>
    <DataContract>
    Public NotInheritable Class Vehicle
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' Member ID assigned to the vehicle.
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' If true, the vehicle is marked as deleted.
        ''' </summary>
        <DataMember(Name:="is_deleted", EmitDefaultValue:=False)>
        Public Property IsDeleted As Boolean?

        ''' <summary>
        ''' Vehicle alias
        ''' </summary>
        <DataMember(Name:="vehicle_alias", EmitDefaultValue:=False)>
        Public Property VehicleAlias As String

        ''' <summary>
        ''' Vehicle VIN
        ''' </summary>
        <DataMember(Name:="vehicle_vin", EmitDefaultValue:=False)>
        Public Property VehicleVin As String

        ''' <summary>
        ''' Vehicle registration state ID.
        ''' </summary>
        <DataMember(Name:="vehicle_reg_state_id", EmitDefaultValue:=False)>
        Public Property VehicleRegStateId As Integer?

        ''' <summary>
        ''' Vehicle registration country ID.
        ''' </summary>
        <DataMember(Name:="vehicle_reg_country_id", EmitDefaultValue:=False)>
        Public Property VehicleRegCountryId As Integer?

        ''' <summary>
        ''' A license plate of the vehicle.
        ''' </summary>
        <DataMember(Name:="vehicle_license_plate", EmitDefaultValue:=False)>
        Public Property VehicleLicensePlate As String

        ''' <summary>
        ''' Vehicle type.
        ''' <para>Availbale values</para>
        ''' sedan', 'suv', 'pickup_truck', 'van', '18wheeler', 'cabin', 'hatchback',
        ''' '<para>motorcyle', 'waste_disposal', 'tree_cutting', 'bigrig', 'cement_mixer', </para>
        ''' 'livestock_carrier', 'dairy','tractor_trailer'.
        ''' </summary>
        <DataMember(Name:="vehicle_type_id", EmitDefaultValue:=False)>
        Public Property VehicleTypeId As String

        ''' <summary>
        ''' When the vehicle was added.
        ''' </summary>
        <DataMember(Name:="timestamp_added", EmitDefaultValue:=False)>
        Public Property TimestampAdded As String

        ''' <summary>
        ''' Vehicle maker brend.
        ''' <para>Available values</para>
        ''' "american coleman", "bmw", "chevrolet", "ford", "freightliner", "gmc",
        ''' <para>"hino", "honda", "isuzu", "kenworth", "mack", "mercedes-benz", "mitsubishi", </para>
        ''' "navistar", "nissan", "peterbilt", "renault", "scania", "sterling", "toyota",
        ''' <para>"volvo", "western star" </para>
        ''' </summary>
        <DataMember(Name:="vehicle_make", EmitDefaultValue:=False)>
        Public Property VehicleMake As String

        ''' <summary>
        ''' Vehicle model year
        ''' </summary>
        <DataMember(Name:="vehicle_model_year", EmitDefaultValue:=False)>
        Public Property VehicleModelYear As Integer?

        ''' <summary>
        ''' Vehicle model
        ''' </summary>
        <DataMember(Name:="vehicle_model", EmitDefaultValue:=False)>
        Public Property VehicleModel As String

        ''' <summary>
        ''' The year, vehicle was acquired
        ''' </summary>
        <DataMember(Name:="vehicle_year_acquired", EmitDefaultValue:=False)>
        Public Property VehicleYearAcquired As Integer?

        ''' <summary>
        ''' A cost of the new vehicle
        ''' </summary>
        <DataMember(Name:="vehicle_cost_new", EmitDefaultValue:=False)>
        Public Property VehicleCostNew As Double?

        ''' <summary>
        ''' If true, the vehicle was purchased new.
        ''' </summary>
        <DataMember(Name:="purchased_new", EmitDefaultValue:=False)>
        Public Property PurchasedNew As Boolean?

        ''' <summary>
        ''' Start date of the license
        ''' </summary>
        <DataMember(Name:="license_start_date", EmitDefaultValue:=False)>
        Public Property LicenseStartDate As String

        ''' <summary>
        ''' End date of the license
        ''' </summary>
        <DataMember(Name:="license_end_date", EmitDefaultValue:=False)>
        Public Property LicenseEndDate As String

        ''' <summary>
        ''' If equal to '1', the vehicle is operational.
        ''' </summary>
        <DataMember(Name:="is_operational", EmitDefaultValue:=False)>
        Public Property IsOsperational As Boolean?

        ''' <summary>
        ''' A type of the fuel
        ''' enum ['unleaded 87','unleaded 89','unleaded 91','unleaded 93','diesel','electric','hybrid']
        ''' </summary>
        <DataMember(Name:="fuel_type", EmitDefaultValue:=False)>
        Public Property FuelType As String

        ''' <summary>
        ''' External telematics vehicle IDs
        ''' </summary>
        <DataMember(Name:="external_telematics_vehicle_ids", EmitDefaultValue:=False)>
        Public Property ExternalTelematicsVehicleIDs As Integer?

        ''' <summary>
        ''' When the vehcile was marked as deleted.
        ''' </summary>
        <DataMember(Name:="timestamp_removed", EmitDefaultValue:=False)>
        Public Property TimestampRemoved As String

        ''' <summary>
        ''' Vehicle profile ID
        ''' </summary>
        <DataMember(Name:="vehicle_profile_id", EmitDefaultValue:=False)>
        Public Property VehicleProfileId As Integer?

        ''' <summary>
        ''' Fuel consumption city
        ''' </summary>
        <DataMember(Name:="fuel_consumption_city", EmitDefaultValue:=False)>
        Public Property FuelConsumptionCity As Double?

        ''' <summary>
        ''' Fuel consumption in the highway area
        ''' </summary>
        <DataMember(Name:="fuel_consumption_highway", EmitDefaultValue:=False)>
        Public Property FuelConsumptionHighway As Double?

        ''' <summary>
        ''' Fuel consumption units in the city area (e.g. mi/l)
        ''' </summary>
        <DataMember(Name:="fuel_consumption_city_unit", EmitDefaultValue:=False)>
        Public Property FuelConsumptionCityUnit As String

        ''' <summary>
        ''' Fuel consumption units in the highway area (e.g. mi/l)
        ''' </summary>
        <DataMember(Name:="fuel_consumption_highway_unit", EmitDefaultValue:=False)>
        Public Property FuelConsumptionHighwayUnit As String

        ''' <summary>
        ''' Miles per gallon in the city area
        ''' </summary>
        <DataMember(Name:="mpg_city", EmitDefaultValue:=False)>
        Public Property MpgCity As Double?

        ''' <summary>
        ''' Miles per gallon in the highway area
        ''' </summary>
        <DataMember(Name:="mpg_highway", EmitDefaultValue:=False)>
        Public Property MpgHighway As Double?

        ''' <summary>
        ''' Fuel consumption UF value in the city area (e.g. '20.01 mi/l')
        ''' </summary>
        <DataMember(Name:="fuel_consumption_city_uf_value", EmitDefaultValue:=False)>
        Public Property FuelConsumptionCityUfValue As String

        ''' <summary>
        ''' Fuel consumption UF value in the highway area (e.g. '2,000.01 mpg')
        ''' </summary>
        <DataMember(Name:="fuel_consumption_highway_uf_value", EmitDefaultValue:=False)>
        Public Property FuelConsumptionHighwayUfValue As String

    End Class
End Namespace
