Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Response from the vehicle request. See also <seealso cref="VehiclesPaginated"/>.
    ''' </summary>
    ''' <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    <DataContract>
    Public NotInheritable Class VehicleV4Response
        Inherits GenericParameters
        ''' <summary>
        ''' The vehicle ID.
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' Member ID assigned to the vehicle.
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As String

        ''' <summary>
        ''' Vehicle alias.
        ''' </summary>
        <DataMember(Name:="vehicle_alias", EmitDefaultValue:=False)>
        Public Property VehicleAlias As String

        ''' <summary>
        ''' Vehicle VIN.
        ''' </summary>
        <DataMember(Name:="vehicle_vin", EmitDefaultValue:=False)>
        Public Property VehicleVin As String

        ''' <summary>
        ''' Vehicle registration state ID.
        ''' </summary>
        <DataMember(Name:="vehicle_reg_state_id", EmitDefaultValue:=False)>
        Public Property VehicleRegStateId As String

        ''' <summary>
        ''' Vehicle registration country ID.
        ''' </summary>
        <DataMember(Name:="vehicle_reg_country_id", EmitDefaultValue:=False)>
        Public Property VehicleRegCountryId As String

        ''' <summary>
        ''' A license plate of the vehicle.
        ''' </summary>
        <DataMember(Name:="vehicle_license_plate", EmitDefaultValue:=False)>
        Public Property VehicleLicensePlate As String

        ''' <summary>
        ''' Vehicle type.
        ''' <para>Availbale values:</para>
        ''' <value>
        ''' 'sedan', 'suv', 'pickup_truck', 'van', '18wheeler', 'cabin', 'hatchback', 
        ''' '<para>motorcyle', 'waste_disposal', 'tree_cutting', 'bigrig', 'cement_mixer', </para>
        ''' 'livestock_carrier', 'dairy','tractor_trailer'.
        ''' </value>
        ''' </summary>
        <DataMember(Name:="vehicle_type_id", EmitDefaultValue:=False)>
        Public Property VehicleTypeID As String

        ''' <summary>
        ''' timestamp_added
        ''' </summary>
        <DataMember(Name:="timestamp_added", EmitDefaultValue:=False)>
        Public Property TimestampAdded As String

        ''' <summary>
        ''' Vehicle maker brend. 
        ''' <para>Available values:</para>
        ''' <value>
        ''' "american coleman", "bmw", "chevrolet", "ford", "freightliner", "gmc", 
        ''' <para>"hino", "honda", "isuzu", "kenworth", "mack", "mercedes-benz", "mitsubishi", </para>
        ''' "navistar", "nissan", "peterbilt", "renault", "scania", "sterling", "toyota", 
        ''' <para>"volvo", "western star" </para>
        ''' </value>"
        ''' </summary>
        <DataMember(Name:="vehicle_make", EmitDefaultValue:=False)>
        Public Property VehicleMake As String

        ''' <summary>
        ''' Vehicle model year.
        ''' </summary>
        <DataMember(Name:="vehicle_model_year", EmitDefaultValue:=False)>
        Public Property VehicleModelYear As String

        ''' <summary>
        ''' Vehicle model.
        ''' </summary>
        <DataMember(Name:="vehicle_model", EmitDefaultValue:=False)>
        Public Property VehicleModel As String

        ''' <summary>
        ''' The year, vehicle was acquired.
        ''' </summary>
        <DataMember(Name:="vehicle_year_acquired", EmitDefaultValue:=False)>
        Public Property VehicleYearAcquired As String

        ''' <summary>
        ''' A cost of the new vehicle.
        ''' </summary>
        <DataMember(Name:="vehicle_cost_new", EmitDefaultValue:=False)>
        Public Property VehicleCostNew As String

        ''' <summary>
        ''' If true, the vehicle was purchased New.
        ''' TO DO: this Property should be Boolean> type, but there Is bug And it can contain
        ''' wrong value (e.g. \u0001). Until fixing of the bug this property will be object type.
        ''' </summary>
        <DataMember(Name:="purchased_new", EmitDefaultValue:=False)>
        Public Property PurchasedNew As String

        ''' <summary>
        ''' Start date of the license.
        ''' </summary>
        <DataMember(Name:="license_start_date", EmitDefaultValue:=False)>
        Public Property LicenseStartDate As String

        ''' <summary>
        ''' End date of the license.
        ''' </summary>
        <DataMember(Name:="license_end_date", EmitDefaultValue:=False)>
        Public Property LicenseEndDate As String

        ''' <summary>
        ''' A number of the vecile's axles.
        ''' </summary>
        <DataMember(Name:="vehicle_axle_count", EmitDefaultValue:=False)>
        Public Property VehicleAxleCount As String

        ''' <summary>
        ''' If equal to '1', the vehicle is operational.
        ''' </summary>
        <DataMember(Name:="is_operational", EmitDefaultValue:=False)>
        Public Property IsOperational As String

        ''' <summary>
        ''' Miles per gallon in the city area.
        ''' </summary>
        <DataMember(Name:="mpg_city", EmitDefaultValue:=False)>
        Public Property MpgCity As String

        ''' <summary>
        ''' Miles per gallon in the highway area.
        ''' </summary>
        <DataMember(Name:="mpg_highway", EmitDefaultValue:=False)>
        Public Property MpgHighway As String

        ''' <summary>
        ''' A type of the fuel.
        ''' <para>Available values:</para>
        ''' <value>unleaded 87, unleaded 89, unleaded 91, unleaded 93, diesel, electric, hybrid</value>
        ''' </summary>
        <DataMember(Name:="fuel_type", EmitDefaultValue:=False)>
        Public Property FuelType As String

        ''' <summary>
        ''' Height of the vehicle in the inches.
        ''' </summary>
        <DataMember(Name:="height_inches", EmitDefaultValue:=False)>
        Public Property HeightInches As String

        ''' <summary>
        ''' Weight of the vehicle in the pounds.
        ''' </summary>
        <DataMember(Name:="weight_lb", EmitDefaultValue:=False)>
        Public Property WeightLb As String

        ''' <summary>
        ''' Gets Or sets the route4me telematics internal API key.
        ''' </summary>
        ''' <value>
        ''' The route4me telematics internal API key.
        ''' </value>
        <DataMember(Name:="route4me_telematics_internal_api_key", EmitDefaultValue:=False)>
        Public Property Route4meTelematicsInternalApiKey As String

        ''' <summary>
        ''' External telematics vehicle ID.
        ''' </summary>
        <DataMember(Name:="External telematics vehicle ID", EmitDefaultValue:=False)>
        Public Property ExternalTelematicsVehicleID As String

        ''' <summary>
        ''' Gets Or sets the R4M telematics gateway connection identifier.
        ''' </summary>
        ''' <value>
        ''' The R4M telematics gateway connection identifier.
        ''' </value>
        <DataMember(Name:="r4m_telematics_gateway_connection_id", EmitDefaultValue:=False)>
        Public Property R4mTelematicsGatewayConnectionId As String

        ''' <summary>
        ''' Gets Or sets the R4M telematics gateway vehicle identifier.
        ''' </summary>
        ''' <value>
        ''' The R4M telematics gateway vehicle identifier.
        ''' </value>
        <DataMember(Name:="r4m_telematics_gateway_vehicle_id", EmitDefaultValue:=False)>
        Public Property R4mTelematicsGatewayVehicleId As String

        ''' <summary>
        ''' If true, the vehicle has trailer.
        ''' </summary>
        <DataMember(Name:="has_trailer", EmitDefaultValue:=False)>
        Public Property HasTrailer As String

        ''' <summary>
        ''' Vehicle height in inches.
        ''' </summary>
        <DataMember(Name:="heightInInches", EmitDefaultValue:=False)>
        Public Property HeightInInches As String

        ''' <summary>
        ''' Vehicle length in inches.
        ''' </summary>
        <DataMember(Name:="lengthInInches", EmitDefaultValue:=False)>
        Public Property LengthInInches As String

        ''' <summary>
        ''' Vehicle width in inches.
        ''' </summary>
        <DataMember(Name:="widthInInches", EmitDefaultValue:=False)>
        Public Property WidthInInches As String

        ''' <summary>
        ''' Maximum weight per axle group in pounds.
        ''' </summary>
        <DataMember(Name:="maxWeightPerAxleGroupInPounds", EmitDefaultValue:=False)>
        Public Property MaxWeightPerAxleGroupInPounds As String

        ''' <summary>
        ''' Number of the axles.
        ''' </summary>
        <DataMember(Name:="numAxles", EmitDefaultValue:=False)>
        Public Property NumAxles As String

        ''' <summary>
        ''' Weight in pounds.
        ''' </summary>
        <DataMember(Name:="weightInPounds", EmitDefaultValue:=False)>
        Public Property WeightInPounds As String

        ''' <summary>
        ''' Hazardous materials type.
        ''' <para>Available values:</para>
        ''' <value>'INVALID', 'NONE', 'GENERAL', 'EXPLOSIVE', 
        ''' 'INHALANT', 'RADIOACTIVE', 'CAUSTIC', 'FLAMMABLE', 'HARMFUL_TO_WATER'</value>
        ''' </summary>
        <DataMember(Name:="HazmatType", EmitDefaultValue:=False)>
        Public Property HazmatType As String

        ''' <summary>
        ''' Low emission zone preference.
        ''' </summary>
        <DataMember(Name:="LowEmissionZonePref", EmitDefaultValue:=False)>
        Public Property LowEmissionZonePref As String

        ''' <summary>
        ''' If equal to 'YES', optimization algorithm will use 53 foot trailer routing.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>
        ''' </summary>
        <DataMember(Name:="Use53FootTrailerRouting", EmitDefaultValue:=False)>
        Public Property Use53FootTrailerRouting As String

        ''' <summary>
        ''' If equal to 'YES', optimization algorithm will use national network.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>
        ''' </summary>
        <DataMember(Name:="UseNationalNetwork", EmitDefaultValue:=False)>
        Public Property UseNationalNetwork As String

        ''' <summary>
        ''' If equal to 'YES', optimization algorithm will use truck restrictions.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>
        ''' </summary>
        <DataMember(Name:="UseTruckRestrictions", EmitDefaultValue:=False)>
        Public Property UseTruckRestrictions As String

        ''' <summary>
        ''' If equal to 'YES', optimization algorithm will avoid ferries.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>.
        ''' </summary>
        <DataMember(Name:="AvoidFerries", EmitDefaultValue:=False)>
        Public Property AvoidFerries As String

        ''' <summary>
        ''' Divided highway avoid preference (e.g. NEUTRAL).
        ''' </summary>
        <DataMember(Name:="DividedHighwayAvoidPreference", EmitDefaultValue:=False)>
        Public Property DividedHighwayAvoidPreference As String

        ''' <summary>
        ''' Freeway avoid preference.
        ''' <para>Available values:</para>
        ''' <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'</value>
        ''' </summary>
        <DataMember(Name:="FreewayAvoidPreference", EmitDefaultValue:=False)>
        Public Property FreewayAvoidPreference As String

        ''' <summary>
        ''' If equal to 'YES', optimization algorithm will use 'International borders open' option.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>
        ''' </summary>
        <DataMember(Name:="InternationalBordersOpen", EmitDefaultValue:=False)>
        Public Property InternationalBordersOpen As String

        ''' <summary>
        ''' Toll road usage.
        ''' <para>Available values:</para>
        ''' <value>'STRONG_AVOID', 'AVOID', 'NEUTRAL', 'FAVOR', 'STRONG_FAVOR'</value>
        ''' </summary>
        <DataMember(Name:="TollRoadUsage", EmitDefaultValue:=False)>
        Public Property TollRoadUsage As String

        ''' <summary>
        ''' If equal to 'YES', the vehicle uses only highway. default = 'NO'.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>
        ''' </summary>
        <DataMember(Name:="hwy_only", EmitDefaultValue:=False)>
        Public Property HwyOnly As String

        ''' <summary>
        ''' If equal to 'YES', the vehicle is long combination. default = 'NO'.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>
        ''' </summary>
        <DataMember(Name:="long_combination_vehicle", EmitDefaultValue:=False)>
        Public Property LongCombinationVehicle As String

        ''' <summary>
        ''' If equal to 'YES', the vehicle should avoid highways. default = 'NO'.
        ''' <para>Available values:</para>
        ''' <value>'YES', 'NO'</value>
        ''' </summary>
        <DataMember(Name:="avoid_highways", EmitDefaultValue:=False)>
        Public Property AvoidHighways As String

        ''' <summary>
        ''' Side street adherence.
        ''' <para>Available values:</para>
        ''' <value>'OFF', 'MINIMAL', 'MODERATE', 'AVERAGE', 'STRICT', 'ADHERE', 'STRONGLYHERE'</value>
        ''' </summary>
        <DataMember(Name:="side_street_adherence", EmitDefaultValue:=False)>
        Public Property SideStreetAdherence As String

        ''' <summary>
        ''' Truck configuration.
        ''' <para>Available values:</para>
        ''' <value>'NONE', 'PASSENGER', '28_DOUBLETRAILER', '48_STRAIGHT_TRUCK', '48_SEMI_TRAILER', 
        ''' '53_SEMI_TRAILER', 'FULLSIZEVAN', '26_STRAIGHT_TRUCK'</value>
        ''' </summary>
        <DataMember(Name:="truck_config", EmitDefaultValue:=False)>
        Public Property TruckConfig As String

        ''' <summary>
        ''' Vehicle height in metric unit.
        ''' </summary>
        <DataMember(Name:="height_metric", EmitDefaultValue:=False)>
        Public Property HeightMetric As String

        ''' <summary>
        ''' Vehicle length in metric unit.
        ''' </summary>
        <DataMember(Name:="length_metric", EmitDefaultValue:=False)>
        Public Property LengthMetric As String

        ''' <summary>
        ''' Vehicle width in metric unit.
        ''' </summary>
        <DataMember(Name:="width_metric", EmitDefaultValue:=False)>
        Public Property WidthMetric As String

        ''' <summary>
        ''' Maximum weight per axle group in metric unit.
        ''' </summary>
        <DataMember(Name:="max_weight_per_axle_group_metric", EmitDefaultValue:=False)>
        Public Property MaxWeightPerAxleGroupMetric As String

        ''' <summary>
        ''' Vehicle weight in metric unit.
        ''' </summary>
        <DataMember(Name:="weight_metric", EmitDefaultValue:=False)>
        Public Property WeightMetric As String

    End Class
End Namespace