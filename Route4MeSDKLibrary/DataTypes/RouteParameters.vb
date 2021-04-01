Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.ComponentModel

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' Route parameters
    ''' </summary>
    <DataContract>
    Public NotInheritable Class RouteParameters

        ''' <summary>
        ''' Let the R4M API know if this SDK request is coming 
        ''' from a file upload within your environment (for analytics).
        ''' </summary>
        <DataMember(Name:="is_upload", EmitDefaultValue:=False)>
        Public Property IsUpload As Boolean?

        ''' <summary>
        ''' The tour type of this route. rt is short for round trip, 
        ''' the optimization engine changes its behavior for round trip routes.
        ''' </summary>
        <DataMember(Name:="rt", EmitDefaultValue:=False)>
        Public Property RT As Boolean?

        ''' <summary>
        ''' By disabling optimization, the route optimization engine 
        ''' will not resequence the stops in your
        ''' </summary>
        <DataMember(Name:="disable_optimization", EmitDefaultValue:=False)>
        Public Property DisableOptimization As Boolean?

        ''' <summary>
        ''' The name of this route. this route name will be accessible in the search API, 
        ''' and also will be displayed on the mobile device of a user.
        ''' </summary>
        <DataMember(Name:="route_name", EmitDefaultValue:=False)>
        Public Property RouteName As String

        ''' <summary>
        ''' The route start date in UTC, unix timestamp seconds.
        ''' Used to show users when the route will begin, also used for reporting and analytics.
        ''' </summary>
        <DataMember(Name:="route_date", EmitDefaultValue:=False)>
        Public Property RouteDate As Long?

        ''' <summary>
        ''' Offset in seconds relative to the route start date (i.e. 9AM would be 60 * 60 * 9)
        ''' </summary>
        <DataMember(Name:="route_time", EmitDefaultValue:=False)>
        Public Property RouteTime As Integer?

        <Obsolete("Always false")>
        <DataMember(Name:="shared_publicly", EmitDefaultValue:=False)>
        Public Property SharedPublicly As String

        ''' <summary>Gets or sets the optimize parameter.
        ''' <para>Availabale values:</para>
        ''' <para>- Distance</para>, 
        ''' <para>- Time</para>, 
        ''' <para>- timeWithTraffic</para>
        ''' </summary>
        <DataMember(Name:="optimize", EmitDefaultValue:=False)>
        Public Property Optimize As String

        ''' <summary>
        ''' When the tour type is not round trip (rt = false), 
        ''' enable lock last so that the final destination is fixed.
        ''' <remarks>
        ''' <para>
        ''' Example: driver leaves a depot, but must always arrive at home 
        ''' (or a specific gas station) at the end of the route.
        ''' </para>
        ''' </remarks>
        ''' </summary>
        <DataMember(Name:="lock_last", EmitDefaultValue:=False)>
        Public Property LockLast As Boolean?

        ''' <summary>
        ''' Vehicle capacity.
        ''' <para>How much cargo can the vehicle carry (units, e.g. cubic meters)</para>
        ''' </summary>
        <DataMember(Name:="vehicle_capacity", EmitDefaultValue:=False)>
        Public Property VehicleCapacity As Integer?

        ''' <summary>
        ''' Maximum distance for a single vehicle in the route (always in miles)
        ''' </summary>
        <DataMember(Name:="vehicle_max_distance_mi", EmitDefaultValue:=False)>
        Public Property VehicleMaxDistanceMI As Integer?

        ''' <summary>
        ''' Maximum allowed revenue from a subtour
        ''' </summary>
        <DataMember(Name:="subtour_max_revenue", EmitDefaultValue:=False)>
        Public Property SubtourMaxRevenue As Integer?

        ''' <summary>
        ''' Maximum cargo volume a vehicle can cary
        ''' </summary>
        <DataMember(Name:="vehicle_max_cargo_volume", EmitDefaultValue:=False)>
        Public Property VehicleMaxCargoVolume As Double?

        ''' <summary>
        ''' Maximum cargo weight a vehicle can cary
        ''' </summary>
        <DataMember(Name:="vehicle_max_cargo_weight", EmitDefaultValue:=False)>
        Public Property VehicleMaxCargoWeight As Double?

        ''' <summary>
        ''' The distance measurement unit for the route.
        ''' </summary>
        ''' <remarks>km or mi, the route4me api will convert all measurements into these units</remarks>
        <DataMember(Name:="distance_unit", EmitDefaultValue:=False)>
        Public Property DistanceUnit As String

        ''' <summary>
        ''' The mode of travel that the directions should be optimized for.
        ''' <para>Available values:</para>
        ''' <para>- Driving</para>
        ''' <para>- Walking</para>
        ''' <para>- Trucking</para>
        ''' <para>- Cycling</para>
        ''' <para>- Transit</para>
        ''' </summary>
        <DataMember(Name:="travel_mode", EmitDefaultValue:=False)>
        Public Property TravelMode As String

        ''' <summary>
        ''' Options which let the user choose which road obstacles to avoid. 
        ''' This has no impact on route sequencing.
        ''' <para>Available values:</para>
        ''' <para>- Highways</para>
        ''' <para>- Tolls</para>
        ''' <para>- minimizeHighways</para>
        ''' <para>- minimizeTolls</para>
        ''' <para>- highways,tolls</para>
        ''' <para>- ""</para>
        ''' </summary>
        <DataMember(Name:="avoid", EmitDefaultValue:=False)>
        Public Property Avoid As String

        ''' <summary>
        ''' An array of the Avoidance zones IDs
        ''' </summary>
        <DataMember(Name:="avoidance_zones", EmitDefaultValue:=False)>
        Public Property AvoidanceZones As String()

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        <Obsolete("All new routes should be assigned to a member_id")>
        <DataMember(Name:="driver_id", EmitDefaultValue:=False)>
        Public Property DriverId As String

        ''' <summary>
        ''' The latitude of the device making this sdk request
        ''' </summary>
        <DataMember(Name:="dev_lat", EmitDefaultValue:=False)>
        Public Property DevLatitude As Double?

        ''' <summary>
        ''' The longitude of the device making this sdk request
        ''' </summary>
        <DataMember(Name:="dev_lng", EmitDefaultValue:=False)>
        Public Property DevLongitude As Double?

        ''' <summary>
        ''' <note type="note"><br />When using a multiple driver algorithm, this is the maximum permissible duration of a generated route.
        ''' <para>The optimization system will automatically create more routes when the route_max_duration is exceeded for a route.</para>
        ''' <para>However it will create an 'unrouted' list of addresses if the maximum number of drivers is exceeded</para>
        ''' </note>
        ''' </summary>
        ''' <value>The maximum duration of the route.</value>
        <DataMember(Name:="route_max_duration", EmitDefaultValue:=False)>
        Public Property RouteMaxDuration As Long?

        ''' <summary>
        ''' The parameter specifies fine-tuning of an optimization process
        ''' by route duration.
        ''' </summary>
        <DataMember(Name:="target_duration", EmitDefaultValue:=False)>
        Public Property TargetDuration As Double?

        ''' <summary>
        ''' The parameter specifies fine-tuning of an optimization process 
        ''' by route distance.
        ''' </summary>
        <DataMember(Name:="target_distance", EmitDefaultValue:=False)>
        Public Property TargetDistance As Double?

        ''' <summary>
        ''' The parameter specifies fine-tuning of an optimization process 
        ''' by waiting time.
        ''' </summary>
        <DataMember(Name:="target_wait_by_tail_size", EmitDefaultValue:=False)>
        Public Property WaitingTime As Double?

        ''' <summary>The email address to notify upon completion of an optimization request</summary>
        ''' <value>The route email.</value>
        <DataMember(Name:="route_email", EmitDefaultValue:=False)>
        Public Property RouteEmail As String

        <Obsolete("The parameter 'route_type' isn't included in route parameters.")>
        <DataMember(Name:="route_type", EmitDefaultValue:=False)>
        Public Property RouteType As String

        <Obsolete("All routes are stored by default at this time")>
        <DataMember(Name:="store_route", EmitDefaultValue:=False)>
        Public Property StoreRoute As Boolean?

        ''' <summary>
        ''' Metric system. Available values:
        ''' <para>1 = ROUTE4ME_METRIC_EUCLIDEAN (use euclidean distance when computing point to point distance)</para>
        ''' <para>2 = ROUTE4ME_METRIC_MANHATTAN (use manhattan distance (taxicab geometry) when computing point to point distance)</para>
        ''' <para>3 = ROUTE4ME_METRIC_GEODESIC (use geodesic distance when computing point to point distance)</para>
        ''' <para>4 = ROUTE4ME_METRIC_MATRIX (default) (use road network driving distance when computing point to point distance)</para>
        ''' <para>5 = ROUTE4ME_METRIC_EXACT_2D (use exact rectilinear distance)</para>
        ''' </summary>
        <DataMember(Name:="metric", EmitDefaultValue:=False)>
        Public Property Metric As Metric

        ''' <summary>
        ''' The algorithm type to use when optimizing the route. See <see cref="DataTypes.AlgorithmType"/>
        ''' </summary>
        <DataMember(Name:="algorithm_type", EmitDefaultValue:=False)>
        Public Property AlgorithmType As AlgorithmType

        ''' <summary>
        ''' The route owner's member ID.
        ''' <remarks>
        ''' <para>In order for users in your organization to have routes assigned to them, 
        ''' you must provide their member ID within the Route4Me system.</para>
        ''' <para>A list of member IDs can be retrieved with view_users API method.</para>
        ''' </remarks>
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Specify the ip address of the remote user making this optimization request.
        ''' </summary>
        <DataMember(Name:="ip", EmitDefaultValue:=False)>
        Public Property Ip As Long?

        ''' <summary>
        ''' The method to use when compute the distance between the points in a route.
        ''' <para>Available values:</para>
        ''' <para>1 = DEFAULT (R4M PROPRIETARY ROUTING)</para>
        ''' <para>2 = DEPRECRATED</para>
        ''' <para>3 = R4M TRAFFIC ENGINE</para>
        ''' <para>4 = DEPRECATED</para>
        ''' <para>5 = DEPRECATED</para>
        ''' <para>6 = TRUCKING</para>
        ''' </summary>
        <DataMember(Name:="dm", EmitDefaultValue:=False)>
        <Range(1, 12)>
        Public Property DM As Integer?

        ''' <summary>
        ''' Directions method.
        ''' <para>Available values:</para>
        ''' <para>1 = DEFAULT (R4M PROPRIETARY INTERNAL NAVIGATION SYSTEM)</para>
        ''' <para>2 = DEPRECATED</para>
        ''' <para>3 = TRUCKING</para>
        ''' <para>4 = DEPRECATED</para>
        ''' </summary>
        <DataMember(Name:="dirm", EmitDefaultValue:=False)>
        <Range(1, 10)>
        Public Property Dirm As Integer?

        ''' <summary>
        ''' Legacy feature which permits a user to request an example number of optimized routes.
        ''' </summary>
        <DataMember(Name:="parts", EmitDefaultValue:=False)>
        Public Property Parts As Integer?

        ''' <summary>
        ''' Minimum number of optimized routes.
        ''' </summary>
        <DataMember(Name:="parts_min", EmitDefaultValue:=False)>
        Public Property PartsMin As Integer?

        <Obsolete("Always null")>
        <DataMember(Name:="device_id", EmitDefaultValue:=False)>
        Public Property DeviceID As Object

        ''' <summary>
        ''' The type of device making this request.
        ''' <para>Available values:</para>
        ''' <para>- web</para>, 
        ''' <para>- iphone</para>, 
        ''' <para>- ipad</para>, 
        ''' <para>- android_phone</para>, 
        ''' <para>- android_tablet</para>
        ''' </summary>
        <DataMember(Name:="device_type", EmitDefaultValue:=False)>
        Public Property DeviceType As String

        ''' <summary>
        ''' If true, the vehicle has a trailer.
        ''' <remarks>
        ''' <para>For routes that have trucking directions enabled, directions generated
        ''' will ensure compliance so that road directions generated do not take the vehicle
        ''' where trailers are prohibited.</para>
        ''' </remarks>
        ''' </summary>
        <DataMember(Name:="has_trailer", EmitDefaultValue:=False)>
        Public Property HasTrailer As Boolean?

        ''' <summary>
        ''' If true, the vehicle will first drive then wait between stops.
        ''' </summary>
        <DataMember(Name:="first_drive_then_wait_between_stops", EmitDefaultValue:=False)>
        Public Property FirstDriveThenWaitBetweenStops As Boolean?

        ''' <summary>
        ''' The vehicle's trailer weight
        ''' <remarks><para>
        ''' For routes that have trucking directions enabled, directions generated 
        ''' will ensure compliance so that road directions generated do not take the vehicle 
        ''' on roads where the weight of the vehicle in tons exceeds this value.
        ''' </para></remarks>
        ''' </summary>
        <DataMember(Name:="trailer_weight_t", EmitDefaultValue:=False)>
        Public Property TrailerWeightT As Double?

        ''' <summary>
        ''' If travel_mode is Trucking, specifies the truck weight.
        ''' </summary>
        <DataMember(Name:="limited_weight_t", EmitDefaultValue:=False)>
        Public Property LimitedWeightT As Double?

        ''' <summary>
        ''' The vehicle's weight per axle (tons)
        ''' <remarks><para>
        ''' For routes that have trucking directions enabled, directions generated
        ''' will ensure compliance so that road directions generated do not take the vehicle
        ''' where the weight per axle in tons exceeds this value.
        ''' </para></remarks>
        ''' </summary>
        <DataMember(Name:="weight_per_axle_t", EmitDefaultValue:=False)>
        Public Property WeightPerAxleT As Double?

        ''' <summary>
        ''' The truck height.
        ''' <remarks><para>
        ''' For routes that have trucking directions enabled, directions generated 
        ''' will ensure compliance of this maximum height of truck when generating 
        ''' road network driving directions.
        ''' </para></remarks>
        ''' </summary>
        <DataMember(Name:="truck_height", EmitDefaultValue:=False)>
        Public Property TruckHeightMeters As Double?

        ''' <summary>
        ''' The truck width.
        ''' <remarks><para>
        ''' For routes that have trucking directions enabled, directions generated 
        ''' will ensure compliance of this width of the truck when generating road network 
        ''' driving directions.
        ''' </para></remarks>
        ''' </summary>
        <DataMember(Name:="truck_width", EmitDefaultValue:=False)>
        Public Property TruckWidthMeters As Double?

        ''' <summary>
        ''' The truck length.
        ''' <remarks><para>
        ''' For routes that have trucking directions enabled, directions generated 
        ''' will ensure compliance of this length of the truck when generating 
        ''' road network driving directions.
        ''' </para></remarks>
        ''' </summary>
        <DataMember(Name:="truck_length", EmitDefaultValue:=False)>
        Public Property TruckLengthMeters As Double?

        ''' <summary>
        ''' Comma-delimited list of the truck hazardous goods.
        ''' </summary>
        <DataMember(Name:="truck_hazardous_goods", EmitDefaultValue:=False)>
        Public Property TruckHazardousGoods As String

        ''' <summary>
        ''' Truck axles number.
        ''' </summary>
        <DataMember(Name:="truck_axles", EmitDefaultValue:=False)>
        Public Property TruckAxles As Integer?

        ''' <summary>
        ''' Truck toll road usage. enum: ["YES", "NO"]
        ''' </summary>
        <DataMember(Name:="truck_toll_road_usage", EmitDefaultValue:=False)>
        Public Property TruckTollRoadUsage As String

        ''' <summary>
        ''' Truck avoid ferries. enum: ["YES", "NO"]
        ''' </summary>
        <DataMember(Name:="truck_avoid_ferries", EmitDefaultValue:=False)>
        Public Property TruckAvoidFerries As String

        ''' <summary>
        ''' Truck highway only. enum: ["YES", "NO"]
        ''' </summary>
        <DataMember(Name:="truck_hwy_only", EmitDefaultValue:=False)>
        Public Property TruckHwyOnly As String

        ''' <summary>
        ''' Truck of the type Long Combination Vehicle. enum: ["YES", "NO"]
        ''' </summary>
        <DataMember(Name:="truck_lcv", EmitDefaultValue:=False)>
        Public Property TruckLcv As String

        ''' <summary>
        ''' Avoid international borders. enum: ["YES", "NO"]
        ''' </summary>
        <DataMember(Name:="truck_borders", EmitDefaultValue:=False)>
        Public Property TruckBorders As String

        ''' <summary>
        ''' Truck side street adherence.
        ''' enum: ["OFF", "MINIMAL","MODERATE","AVERAGE","STRICT","ADHERE","STRONGLYHERE"]
        ''' </summary>
        <DataMember(Name:="truck_side_street_adherence", EmitDefaultValue:=False)>
        Public Property TruckSideStreetAdherence As String

        ''' <summary>
        ''' Truck configuration.
        ''' enum: ["NONE","PASSENGER","28_DOUBLETRAILER","48_STRAIGHT_TRUCK",
        ''' "48_SEMI_TRAILER","53_SEMI_TRAILER","FULLSIZEVAN","26_STRAIGHT_TRUCK"]
        ''' </summary>
        <DataMember(Name:="truck_config", EmitDefaultValue:=False)>
        Public Property TruckConfig As String

        ''' <summary>
        ''' Truck dimension unit. enum: ["mi","km"]
        ''' </summary>
        <DataMember(Name:="truck_dim_unit", EmitDefaultValue:=False)>
        Public Property TruckDimUnit As String

        ''' <summary>
        ''' Truck type. 
        ''' enum: ["suv","pickup_truck","van","18wheeler","cabin","waste_disposal",
        ''' "tree_cutting","bigrig","cement_mixer","livestock_carrier","dairy",
        ''' "tractor_trailer"]
        ''' </summary>
        <DataMember(Name:="truck_type", EmitDefaultValue:=False)>
        Public Property TruckType As String

        ''' <summary>
        ''' If travel_mode = 'Trucking', specifies the truck weight (required)
        ''' </summary>
        <DataMember(Name:="truck_weight", EmitDefaultValue:=False)>
        Public Property TruckWeight As Double?

        ''' <summary>
        ''' The minimum number of stops permitted per created subroute.
        ''' </summary>
        <DataMember(Name:="min_tour_size", EmitDefaultValue:=False)>
        Public Property MinTourSize As Integer?

        ''' <summary>
        ''' The maximum number of stops permitted per created subroute.
        ''' </summary>
        <DataMember(Name:="max_tour_size", EmitDefaultValue:=False)>
        Public Property MaxTourSize As Integer?

        ''' <summary>
        ''' The optimization quality.
        ''' <para>Available values:</para>
        ''' <para>1 - Generate Optimized Routes As Quickly as Possible;</para>
        ''' <para>2 - Generate Routes That Look Better On A Map;</para>
        ''' <para>3 - Generate The Shortest And Quickest Possible Routes.</para>
        ''' </summary>
        <DataMember(Name:="optimization_quality", EmitDefaultValue:=False)>
        Public Property OptimizationQuality As Integer?

        ''' <summary>
        ''' If equal to 1, uturn is allowed for the vehicle.
        ''' </summary>
        <DataMember(Name:="uturn", EmitDefaultValue:=False)>
        Public Property Uturn As Integer?

        ''' <summary>
        ''' If equal to 1, leftturn is allowed for the vehicle.
        ''' </summary>
        <DataMember(Name:="leftturn", EmitDefaultValue:=False)>
        Public Property LeftTurn As Integer?

        ''' <summary>
        ''' If equal to 1, rightturn is allowed for the vehicle.
        ''' </summary>
        <DataMember(Name:="rightturn", EmitDefaultValue:=False)>
        Public Property RightTurn As Integer?

        ''' <summary>
        ''' Route travel time slowdown (e.g. 25 (means 25% slowdown))
        ''' </summary>
        <DataMember(Name:="route_time_multiplier", EmitDefaultValue:=False)>
        Public Property RouteTimeMultiplier As Integer?

        ''' <summary>
        ''' Route service time slowdown (e.g. 10 (means 10% slowdown))
        ''' </summary>
        <DataMember(Name:="route_service_time_multiplier", EmitDefaultValue:=False)>
        Public Property RouteServiceTimeMultiplier As Integer?

        ''' <summary>
        ''' Optimization engine (e.g. '1','2' etc)
        ''' </summary>
        <DataMember(Name:="optimization_engine", EmitDefaultValue:=False)>
        Public Property OptimizationEngine As String

        ''' <summary>
        ''' Slowdonw of the optimization parameters.
        ''' If true, the time windows ignored.
        ''' </summary>
        <DataMember(Name:="ignore_tw", EmitDefaultValue:=False)>
        Public Property IgnoreTw As Boolean?


        ''' <summary>
        ''' If the service time is specified, all the route addresses wil have same service time. 
        ''' See <see cref="OverrideAddresses"/>
        ''' </summary>
        <DataMember(Name:="override_addresses", EmitDefaultValue:=False)>
        Public Property overrideAddresses As OverrideAddresses

        ''' <summary>
        ''' Slowdown of the optimization parameters.
        ''' </summary>
        ''' <remarks>
        ''' <para>This Is only query parameter.</para>
        ''' <para>This parameter Is used in the optimization creation/generation process. </para>
        ''' </remarks>
        <DataMember(Name:="slowdowns", EmitDefaultValue:=False)>
        Public Property Slowdowns As SlowdownParams

        ''' <summary>
        ''' TO DO: adjust description
        ''' </summary>
        <DataMember(Name:="is_dynamic_start_time", EmitDefaultValue:=False)>
        <DefaultValue(False)>
        Public Property is_dynamic_start_time As Boolean

        ''' <summary>
        ''' Address bundling rules
        ''' </summary>
        <DataMember(Name:="bundling", EmitDefaultValue:=False)>
        <DefaultValue(False)>
        Public Property Bundling As AddressBundling

    End Class

    ''' <summary>
    ''' The service time specified or all the addresses in the route.
    ''' </summary>
    Public Class OverrideAddresses

        ''' <summary>
        ''' The service time specified or all the addresses in the route.
        ''' </summary>
        <DataMember(Name:="time", EmitDefaultValue:=False), CustomValidation(GetType(PropertyValidation), "ValidateEpochTime")>
        Public Property Time As Integer?

        ''' <summary>
        ''' Route address stop type
        ''' </summary>
        <DataMember(Name:="address_stop_type")>
        Public Property AddressStopType As String

        ''' <summary>
        ''' The address group
        ''' </summary>
        <DataMember(Name:="group", EmitDefaultValue:=False)>
        Public Property Group As String

    End Class

    ''' <summary>
    ''' Slowdown parameters for the optimization creating process.
    ''' </summary>
    <DataContract>
    Public Class SlowdownParams
        Inherits GenericParameters

        ''' <summary>
        ''' Class constructor
        ''' </summary>
        ''' <param name="_serviceTime">Service time slowdown (percent)</param>
        ''' <param name="_travelTime">Travel time slowdown (percent)</param>
        Public Sub New(ByVal _serviceTime As Integer?, ByVal _travelTime As Integer?)
            ServiceTime = _serviceTime
            TravelTime = _travelTime
        End Sub

        ''' <summary>
        ''' Class constructor
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Service time slowdowon (percent)
        ''' </summary>
        <DataMember(Name:="service_time", EmitDefaultValue:=False)>
        Public Property ServiceTime As Integer?

        ''' <summary>
        ''' Travel time slowdowon (percent)
        ''' </summary>
        <DataMember(Name:="travel_time", EmitDefaultValue:=False)>
        Public Property TravelTime As Integer?
    End Class

End Namespace
