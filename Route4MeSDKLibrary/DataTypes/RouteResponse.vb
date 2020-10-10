Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class RouteResponse
        <DataMember(Name:="route_id")>
        Public Property RouteID As String

        <DataMember(Name:="optimization_problem_id")>
        Public Property OptimizationProblemId As String

        ''' <summary>
        ''' Route rating by user [0 - 5]
        ''' </summary>
        <DataMember(Name:="user_route_rating")>
        Public Property UserRouteRating As Integer?

        <DataMember(Name:="member_id")>
        Public Property MemberId As Integer?

        <DataMember(Name:="member_email")>
        Public Property MemberEmail As String

        <DataMember(Name:="member_first_name")>
        Public Property MemberFirstName As String

        <DataMember(Name:="member_last_name")>
        Public Property MemberLastName As String

        <DataMember(Name:="channel_name")>
        Public Property ChannelName As String

        <DataMember(Name:="member_picture", EmitDefaultValue:=False)>
        Public Property MemberPicture As String

        <DataMember(Name:="member_tracking_subheadline", EmitDefaultValue:=False)>
        Public Property MemberTrackingSubheadline As String

        <DataMember(Name:="approved_for_execution")>
        Public Property ApprovedForExecution As Boolean

        <DataMember(Name:="approved_revisions_counter", EmitDefaultValue:=False)>
        Public Property ApprovedRevisionsCounter As Integer?

        <DataMember(Name:="vehicle_alias")>
        Public Property VehicleAlias As String

        <DataMember(Name:="driver_alias")>
        Public Property DriverAlias As String

        <DataMember(Name:="trip_distance")>
        Public Property TripDistance As Double?

        <DataMember(Name:="udu_distance_unit", EmitDefaultValue:=False)>
        Public Property UduDistanceUnit As String

        <DataMember(Name:="udu_trip_distance", EmitDefaultValue:=False)>
        Public Property UduTripDistance As Double?

        <DataMember(Name:="is_unrouted")>
        Public Property IsUnrouted As Boolean

        <DataMember(Name:="route_cost")>
        Public Property RouteCost As Double?

        <DataMember(Name:="route_revenue")>
        Public Property RouteRevenue As Double?

        <DataMember(Name:="net_revenue_per_distance_unit")>
        Public Property NetRevenuePerDistanceUnit As Double?

        <DataMember(Name:="created_timestamp")>
        Public Property CreatedTimestamp As Long?

        <DataMember(Name:="mpg")>
        Public Property mpg As Double?

        <DataMember(Name:="gas_price")>
        Public Property GasPrice As Double?

        <DataMember(Name:="route_duration_sec")>
        Public Property RouteDurationSec As Long?

        <DataMember(Name:="planned_total_route_duration")>
        Public Property PlannedTotalRouteDuration As Long?

        <DataMember(Name:="total_wait_time", EmitDefaultValue:=False)>
        Public Property TotalWaitTime As Long?

        <DataMember(Name:="udu_actual_travel_distance", EmitDefaultValue:=False)>
        Public Property UduActualTravelDistance As Double?

        <DataMember(Name:="actual_travel_distance")>
        Public Property ActualTravelDistance As Double?

        <DataMember(Name:="actual_travel_time")>
        Public Property ActualTravelTime As Integer?

        <DataMember(Name:="actual_footsteps")>
        Public Property ActualFootSteps As Long?

        <DataMember(Name:="working_time")>
        Public Property WorkingTime As Long?

        <DataMember(Name:="driving_time")>
        Public Property DrivingTime As Long?

        <DataMember(Name:="idling_time")>
        Public Property IdlingTime As Long?

        <DataMember(Name:="paying_miles")>
        Public Property PayingMiles As Double?

        <DataMember(Name:="geofence_polygon_type")>
        Public Property GeofencePolygonType As String

        <DataMember(Name:="geofence_polygon_size")>
        Public Property GeofencePolygonSize As Integer?

        <DataMember(Name:="destination_count", EmitDefaultValue:=False)>
        Public Property DestinationCount As Integer?

        <DataMember(Name:="notes_count", EmitDefaultValue:=False)>
        Public Property NotesCount As Integer?

        <DataMember(Name:="parameters")>
        Public Property Parameters As RouteParameters

        <DataMember(Name:="addresses")>
        Public Property Addresses As Address()

        <DataMember(Name:="links")>
        Public Property Links As Links

        <DataMember(Name:="notes")>
        Public Property Notes As AddressNote()

        <DataMember(Name:="vehicle", EmitDefaultValue:=False)>
        Public Property Vehilce As VehicleV4Response

        <DataMember(Name:="member_config_storage", EmitDefaultValue:=False)>
        Public Property MemberConfigStorage As Dictionary(Of String, String)

        <DataMember(Name:="original_route", EmitDefaultValue:=False)>
        Public Property OriginalRoute As DataObjectRoute

        <DataMember(Name:="path")>
        Public Property Path As GeoPoint()

        <DataMember(Name:="directions")>
        Public Property Directions As Direction()

    End Class
End Namespace