Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Vehicle profile data structure.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleProfile
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' Vehicle profile ID
        ''' </summary>
        <DataMember(Name:="vehicle_profile_id", EmitDefaultValue:=False)>
        Public Property VehicleProfileId As Integer?

        ''' <summary>
        ''' Root member ID
        ''' </summary>
        <DataMember(Name:="root_member_id", EmitDefaultValue:=False)>
        Public Property RootMemberId As Integer?

        ''' <summary>
        ''' Vehicle profile name
        ''' </summary>
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Vehicle height
        ''' </summary>
        <DataMember(Name:="height", EmitDefaultValue:=False)>
        Public Property Height As Double?

        ''' <summary>
        ''' Vehicle width
        ''' </summary>
        <DataMember(Name:="width", EmitDefaultValue:=False)>
        Public Property Width As Double?

        ''' <summary>
        ''' Vehicle length
        ''' </summary>
        <DataMember(Name:="length", EmitDefaultValue:=False)>
        Public Property Length As Double?

        ''' <summary>
        ''' Vehicle weight
        ''' </summary>
        <DataMember(Name:="weight", EmitDefaultValue:=False)>
        Public Property Weight As Double?

        ''' <summary>
        ''' Maximum weight per axle
        ''' </summary>
        <DataMember(Name:="max_weight_per_axle", EmitDefaultValue:=False)>
        Public Property MaxWeightPerAxle As Double?

        ''' <summary>
        ''' When the profile deleted
        ''' </summary>
        <DataMember(Name:="deleted_at", EmitDefaultValue:=False)>
        Public Property DeletedAt As String

        ''' <summary>
        ''' When the profile created
        ''' </summary>
        <DataMember(Name:="created_at", EmitDefaultValue:=False)>
        Public Property CreatedAt As String

        ''' <summary>
        ''' When the profile updated
        ''' </summary>
        <DataMember(Name:="updated_at", EmitDefaultValue:=False)>
        Public Property UpdatedAt As String

        ''' <summary>
        ''' A type of the fuel
        ''' enum ['unleaded 87','unleaded 89','unleaded 91','unleaded 93','diesel','electric','hybrid']
        ''' </summary>
        <DataMember(Name:="fuel_type", EmitDefaultValue:=False)>
        Public Property FuelType As String

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
        ''' Type of a hazardous material.
        ''' enum ['general', 'explosives', 'flammable', 'inhalants', 'caustic', 'radioactive']
        ''' </summary>
        <DataMember(Name:="hazmat_type", EmitDefaultValue:=False)>
        Public Property HazmatType As String

        ''' <summary>
        ''' If true, the profile is predefined.
        ''' </summary>
        <DataMember(Name:="is_predefined", EmitDefaultValue:=False)>
        Public Property IsPredefined As Boolean?

        ''' <summary>
        ''' If true, the profile is default.
        ''' </summary>
        <DataMember(Name:="is_default", EmitDefaultValue:=False)>
        Public Property IsDefault As Boolean?

        ''' <summary>
        ''' Height units (e.g. 'ft', 'm')
        ''' </summary>
        <DataMember(Name:="height_units", EmitDefaultValue:=False)>
        Public Property HeightUnits As String

        ''' <summary>
        ''' Width units (e.g. 'ft', 'm')
        ''' </summary>
        <DataMember(Name:="width_units", EmitDefaultValue:=False)>
        Public Property WidthUnits As String

        ''' <summary>
        ''' Length units (e.g. 'ft', 'm')
        ''' </summary>
        <DataMember(Name:="length_units", EmitDefaultValue:=False)>
        Public Property LengthUnits As String

        ''' <summary>
        ''' Weight units (e.g. 'lb', 'kg')
        ''' </summary>
        <DataMember(Name:="weight_units", EmitDefaultValue:=False)>
        Public Property WeightUnits As String

        ''' <summary>
        ''' Maximum weight per axle units (e.g. 'lb', 'kg')
        ''' </summary>
        <DataMember(Name:="max_weight_per_axle_units", EmitDefaultValue:=False)>
        Public Property MaxWeightPerAxleUnits As String

        ''' <summary>
        ''' Fuel consumption units in the city area (e.g. mpg)
        ''' </summary>
        <DataMember(Name:="fuel_consumption_city_unit", EmitDefaultValue:=False)>
        Public Property FuelConsumptionCityUnit As String

        ''' <summary>
        ''' Fuel consumption units in the highway area (e.g. mpg)
        ''' </summary>
        <DataMember(Name:="fuel_consumption_highway_unit", EmitDefaultValue:=False)>
        Public Property FuelConsumptionHighwayUnit As String

        ''' <summary>
        ''' Height UF value (e.g. "7'")
        ''' </summary>
        <DataMember(Name:="height_uf_value", EmitDefaultValue:=False)>
        Public Property HeightUfValue As String

        ''' <summary>
        ''' Width UF value (e.g. "8'")
        ''' </summary>
        <DataMember(Name:="width_uf_value", EmitDefaultValue:=False)>
        Public Property WidthUfValue As String

        ''' <summary>
        ''' Length UF value (e.g. "20'")
        ''' </summary>
        <DataMember(Name:="length_uf_value", EmitDefaultValue:=False)>
        Public Property LengthUfValue As String

        ''' <summary>
        ''' Weight UF value (e.g. "8,500lb")
        ''' </summary>
        <DataMember(Name:="weight_uf_value", EmitDefaultValue:=False)>
        Public Property WeightUfValue As String

        ''' <summary>
        ''' Maximum weight per axle (UF value, e.g. "8,500lb")
        ''' </summary>
        <DataMember(Name:="max_weight_per_axle_uf_value", EmitDefaultValue:=False)>
        Public Property MaxWeightPerAxleUfValue As String

        ''' <summary>
        ''' Fuel consumption city (UF value, e.g. "20.01 mi/l")
        ''' </summary>
        <DataMember(Name:="fuel_consumption_city_uf_value", EmitDefaultValue:=False)>
        Public Property FuelConsumptionCityUfValue As String

        ''' <summary>
        ''' Fuel consumption highway (UF value, e.g. "2,000.01 mpg")
        ''' </summary>
        <DataMember(Name:="fuel_consumption_highway_uf_value", EmitDefaultValue:=False)>
        Public Property FuelConsumptionHighwayUfValue As String

    End Class
End Namespace
