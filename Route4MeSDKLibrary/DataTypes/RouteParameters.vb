Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class RouteParameters

        'let the R4M api know if this sdk request is coming from a file upload within your environment (for analytics)
        <DataMember(Name:="is_upload", EmitDefaultValue:=False)> _
        Public Property IsUpload As String

        'the tour type of this route. rt is short for round trip, the optimization engine changes its behavior for round trip routes
        <DataMember(Name:="rt", EmitDefaultValue:=False)> _
        Public Property RT As System.Nullable(Of Boolean)

        'by disabling optimization, the route optimization engine will not resequence the stops in your
        <DataMember(Name:="disable_optimization", EmitDefaultValue:=False)> _
        Public Property DisableOptimization As System.Nullable(Of Boolean)

        'the name of this route. this route name will be accessible in the search API, and also will be displayed on the mobile device of a user
        <DataMember(Name:="route_name", EmitDefaultValue:=False)> _
        Public Property RouteName As String

        'the route start date in UTC, unix timestamp seconds. 
        'used to show users when the route will begin, also used for reporting and analytics
        <DataMember(Name:="route_date", EmitDefaultValue:=False)> _
        Public Property RouteDate As System.Nullable(Of Long)

        'offset in seconds relative to the route start date (i.e. 9AM would be 60 * 60 * 9)
        <DataMember(Name:="route_time", EmitDefaultValue:=False)> _
        Public Property RouteTime As Object

        'deprecated
        'specify if the route can be viewed by unauthenticated users
        <DataMember(Name:="shared_publicly", EmitDefaultValue:=False)> _
        Public Property SharedPublicly As String

        <DataMember(Name:="optimize", EmitDefaultValue:=False)> _
        Public Property Optimize As String

        'when the tour type is not round trip (rt = false), enable lock last so that the final destination is fixed
        'example: driver leaves a depot, but must always arrive at home ( or a specific gas station) at the end of the route
        <DataMember(Name:="lock_last", EmitDefaultValue:=False)> _
        Public Property LockLast As System.Nullable(Of Boolean)

        <DataMember(Name:="vehicle_capacity", EmitDefaultValue:=False)> _
        Public Property VehicleCapacity As String

        <DataMember(Name:="vehicle_max_distance_mi", EmitDefaultValue:=False)> _
        Public Property VehicleMaxDistanceMI As String

        <DataMember(Name:="subtour_max_revenue", EmitDefaultValue:=False)>
        Public Property SubtourMaxRevenue As Integer?

        <DataMember(Name:="vehicle_max_cargo_volume", EmitDefaultValue:=False)>
        Public Property VehicleMaxCargoVolume As Double?

        <DataMember(Name:="vehicle_max_cargo_weight", EmitDefaultValue:=False)>
        Public Property VehicleMaxCargoWeight As Double?

        'km or mi, the route4me api will convert all measurements into these units
        <DataMember(Name:="distance_unit", EmitDefaultValue:=False)> _
        Public Property DistanceUnit As String

        <DataMember(Name:="travel_mode", EmitDefaultValue:=False)> _
        Public Property TravelMode As String

        <DataMember(Name:="avoid", EmitDefaultValue:=False)> _
        Public Property Avoid As String

        <DataMember(Name:="avoidance_zones", EmitDefaultValue:=False)>
        Public Property AvoidanceZones As String()

        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)> _
        Public Property VehicleId As String

        'deprecated, all new routes should be assigned to a member_id
        <DataMember(Name:="driver_id", EmitDefaultValue:=False)> _
        Public Property DriverId As String

        'the latitude of the device making this sdk request
        <DataMember(Name:="dev_lat", EmitDefaultValue:=False)> _
        Public Property DevLatitude As System.Nullable(Of Double)

        'the longitude of the device making this sdk request
        <DataMember(Name:="dev_lng", EmitDefaultValue:=False)> _
        Public Property DevLongitude As System.Nullable(Of Double)

        'when using a multiple driver algorithm, this is the maximum permissible duration of a generated route
        'the optimization system will automatically create more routes when the route_max_duration is exceeded for a route
        'however it will create an 'unrouted' list of addresses if the maximum number of drivers is exceeded
        <DataMember(Name:="route_max_duration", EmitDefaultValue:=False)> _
        Public Property RouteMaxDuration As System.Nullable(Of Integer)

        'the email address to notify upon completion of an optimization request
        <DataMember(Name:="route_email", EmitDefaultValue:=False)> _
        Public Property RouteEmail As String

        'type of route being created: ENUM(api,null)
        <DataMember(Name:="route_type", EmitDefaultValue:=False)> _
        Public Property RouteType As String

        'deprecated
        'all routes are stored by default at this time
        <DataMember(Name:="store_route", EmitDefaultValue:=False)> _
        Public Property StoreRoute As System.Nullable(Of Boolean)

        '1 = ROUTE4ME_METRIC_EUCLIDEAN (use euclidean distance when computing point to point distance)
        '2 = ROUTE4ME_METRIC_MANHATTAN (use manhattan distance (taxicab geometry) when computing point to point distance)
        '3 = ROUTE4ME_METRIC_GEODESIC (use geodesic distance when computing point to point distance)
        '#4 is the default and suggested metric
        '4 = ROUTE4ME_METRIC_MATRIX (use road network driving distance when computing point to point distance)
        '5 = ROUTE4ME_METRIC_EXACT_2D (use exact rectilinear distance)
        <DataMember(Name:="metric", EmitDefaultValue:=False)> _
        Public Property Metric As Metric

        'the type of algorithm to use when optimizing the route
        <DataMember(Name:="algorithm_type", EmitDefaultValue:=False)> _
        Public Property AlgorithmType As AlgorithmType

        'in order for users in your organization to have routes assigned to them, 
        'you must provide their member id within the route4me system
        'a list of member ids can be retrieved with view_users api method
        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId As String

        'specify the ip address of the remote user making this optimization request
        <DataMember(Name:="ip", EmitDefaultValue:=False)> _
        Public Property Ip As String

        'the method to use when compute the distance between the points in a route
        '1 = DEFAULT (R4M PROPRIETARY ROUTING)
        '2 = DEPRECRATED
        '3 = R4M TRAFFIC ENGINE
        '4 = DEPRECATED
        '5 = DEPRECATED
        '6 = TRUCKING
        <DataMember(Name:="dm", EmitDefaultValue:=False)> _
        Public Property DM As System.Nullable(Of Integer)

        'directions method
        '1 = DEFAULT (R4M PROPRIETARY INTERNAL NAVIGATION SYSTEM)
        '2 = DEPRECATED
        '3 = TRUCKING
        '4 = DEPRECATED
        <DataMember(Name:="dirm", EmitDefaultValue:=False)> _
        Public Property Dirm As System.Nullable(Of Integer)

        <DataMember(Name:="parts", EmitDefaultValue:=False)> _
        Public Property Parts As System.Nullable(Of Integer)

        <DataMember(Name:="parts_min", EmitDefaultValue:=False)>
        Public Property PartsMin As Integer?

        'deprecated 
        <DataMember(Name:="device_id", EmitDefaultValue:=False)> _
        Public Property DeviceID As Object

        'the type of device making this request
        'ENUM("web", "iphone", "ipad", "android_phone", "android_tablet")
        <DataMember(Name:="device_type", EmitDefaultValue:=False)> _
        Public Property DeviceType As String

        'for routes that have trucking directions enabled, directions generated
        'will ensure compliance so that road directions generated do not take the vehicle
        'where trailers are prohibited
        <DataMember(Name:="has_trailer", EmitDefaultValue:=False)> _
        Public Property HasTrailer As System.Nullable(Of Boolean)

        <DataMember(Name:="first_drive_then_wait_between_stops", EmitDefaultValue:=False)>
        Public Property FirstDriveThenWaitBetweenStops As Boolean?

        'for routes that have trucking directions enabled, directions generated
        'will ensure compliance so that road directions generated do not take the vehicle
        'on roads where the weight of the vehicle in tons exceeds this value
        <DataMember(Name:="trailer_weight_t", EmitDefaultValue:=False)> _
        Public Property TrailerWeightT As System.Nullable(Of Double)

        <DataMember(Name:="limited_weight_t", EmitDefaultValue:=False)> _
        Public Property LimitedWeightT As System.Nullable(Of Double)

        'for routes that have trucking directions enabled, directions generated
        'will ensure compliance so that road directions generated do not take the vehicle
        'where the weight per axle in tons exceeds this value
        <DataMember(Name:="weight_per_axle_t", EmitDefaultValue:=False)> _
        Public Property WeightPerAxleT As System.Nullable(Of Double)

        'for routes that have trucking directions enabled, directions generated
        'will ensure compliance of this maximum height of truck when generating road network driving directions
        <DataMember(Name:="truck_height", EmitDefaultValue:=False)> _
        Public Property TruckHeightMeters As System.Nullable(Of Integer)

        'for routes that have trucking directions enabled, directions generated
        'will ensure compliance of this width of the truck when generating road network driving directions
        <DataMember(Name:="truck_width", EmitDefaultValue:=False)> _
        Public Property TruckWidthMeters As System.Nullable(Of Integer)

        'for routes that have trucking directions enabled, directions generated
        'will ensure compliance of this length of the truck when generating road network driving directions
        <DataMember(Name:="truck_length", EmitDefaultValue:=False)> _
        Public Property TruckLengthMeters As System.Nullable(Of Integer)

        'the minimum number of stops permitted per created subroute
        <DataMember(Name:="min_tour_size", EmitDefaultValue:=False)> _
        Public Property MinTourSize As System.Nullable(Of Integer)

        'the maximum number of stops permitted per created subroute
        <DataMember(Name:="max_tour_size", EmitDefaultValue:=False)> _
        Public Property MaxTourSize As System.Nullable(Of Integer)

        'there are 3 types of optimization qualities that are optimizations goals
        '1 - Generate Optimized Routes As Quickly as Possible
        '2 - Generate Routes That Look Better On A Map
        '3 - Generate The Shortest And Quickest Possible Routes

        <DataMember(Name:="optimization_quality", EmitDefaultValue:=False)> _
        Public Property OptimizationQuality As System.Nullable(Of Integer)

        <DataMember(Name:="leftturn", EmitDefaultValue:=False)>
        Public Property LeftTurn As Integer?

        <DataMember(Name:="rightturn", EmitDefaultValue:=False)>
        Public Property RightTurn As Integer?

        <DataMember(Name:="uturn", EmitDefaultValue:=False)>
        Public Property Uturn As Integer?

    End Class
End Namespace
