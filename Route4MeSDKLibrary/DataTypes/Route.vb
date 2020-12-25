Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' The order data structure
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class DataObjectRoute
        Inherits DataObjectBase

        ''' <summary>
        ''' The route ID
        ''' </summary>
        <DataMember(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteID As String

        ''' <summary>
        ''' User route rating [0, 5]. A null value means no rating was given.
        ''' Users can rate routes so that future optimizations take these ratings into account.
        ''' </summary>
        <DataMember(Name:="user_route_rating", EmitDefaultValue:=False)>
        Public Property UserRouteRating As Integer?

        ''' <summary>
        ''' The member ID
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' The member's email
        ''' </summary>
        <DataMember(Name:="member_email", EmitDefaultValue:=False)>
        Public Property MemberEmail As String

        ''' <summary>
        ''' The member's first name
        ''' </summary>
        <DataMember(Name:="member_first_name", EmitDefaultValue:=False)>
        Public Property MemberFirstName As String

        ''' <summary>
        ''' The member's last name
        ''' </summary>
        <DataMember(Name:="member_last_name", EmitDefaultValue:=False)>
        Public Property MemberLastName As String

        ''' <summary>
        ''' Channel name.
        ''' </summary>
        <DataMember(Name:="channel_name", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ChannelName As String

        ''' <summary>
        ''' URL to a member picture
        ''' </summary>
        <DataMember(Name:="member_picture", EmitDefaultValue:=False)>
        Public Property MemberPicture As String

        ''' <summary>
        ''' Member tracking subheadline
        ''' </summary>
        <DataMember(Name:="member_tracking_subheadline", EmitDefaultValue:=False)>
        Public Property MemberTrackingSubheadline As String

        ''' <summary>
        ''' If true, the order is approved for execution.
        ''' </summary>
        <DataMember(Name:="approved_for_execution")>
        Public Property ApprovedForExecution As Boolean

        ''' <summary>
        ''' Counter of the approved revisions
        ''' </summary>
        <DataMember(Name:="approved_revisions_counter", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ApprovedRevisionsCounter As Integer?

        ''' <summary>
        ''' Vehicle alias
        ''' </summary>
        <DataMember(Name:="vehicle_alias", EmitDefaultValue:=False)>
        Public Property VehicleAlias As String

        ''' <summary>
        ''' Driver alias
        ''' </summary>
        <DataMember(Name:="driver_alias", EmitDefaultValue:=False)>
        Public Property DriverAlias As String

        ''' <summary>
        ''' Cost of the route
        ''' </summary>
        <DataMember(Name:="route_cost", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property RouteCost As Double?

        ''' <summary>
        ''' Route weight
        ''' </summary>
        <DataMember(Name:="route_weight", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property RouteWeight As Double?

        ''' <summary>
        ''' Route total cubic volume of the carried cargo (vehicle capacity).
        ''' </summary>
        <DataMember(Name:="route_cube", EmitDefaultValue:=False)>
        Public Property RouteCube As Double?

        ''' <summary>
        ''' The total number of route pieces (vehicle pieces).
        ''' </summary>
        <DataMember(Name:="route_pieces", EmitDefaultValue:=False)>
        Public Property RoutePieces As Integer?

        ''' <summary>
        ''' Total route revenue
        ''' </summary>
        <DataMember(Name:="route_revenue", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property RouteRevenue As Double?

        ''' <summary>
        ''' Net revenue per distance unit
        ''' </summary>
        <DataMember(Name:="net_revenue_per_distance_unit", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property NetRevenuePerDistanceUnit As Double?

        ''' <summary>
        ''' Miles per gallon
        ''' </summary>
        <DataMember(Name:="mpg", EmitDefaultValue:=False)>
        Public Property mpg As String

        ''' <summary>
        ''' Total route's trip distance
        ''' </summary>
        <DataMember(Name:="trip_distance", EmitDefaultValue:=False)>
        Public Property TripDistance As Double?

        ''' <summary>
        ''' The UDU distance measurement unit for the route.
        ''' enum: ["mi", "km"]
        ''' </summary>
        ''' <remarks>km or mi, the route4me api will convert all measurements into these units</remarks>
        <DataMember(Name:="udu_distance_unit", EmitDefaultValue:=False)>
        Public Property UduDistanceUnit As String

        ''' <summary>
        ''' Total route's UDU trip distance
        ''' </summary>
        <DataMember(Name:="udu_trip_distance", EmitDefaultValue:=False)>
        Public Property UduTripDistance As Double?

        ''' <summary>
        ''' If true, route is unrouted.
        ''' </summary>
        <DataMember(Name:="is_unrouted", EmitDefaultValue:=False)>
        Public Property IsUnrouted As Boolean?

        ''' <summary>
        ''' Gas price
        ''' </summary>
        <DataMember(Name:="gas_price", EmitDefaultValue:=False)>
        Public Property GasPrice As Double?

        ''' <summary>
        ''' Total route duration (seconds).
        ''' </summary>
        <DataMember(Name:="route_duration_sec", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property RouteDurationSec As Long?

        ''' <summary>
        ''' Planned total route duration (seconds).
        ''' </summary>
        <DataMember(Name:="planned_total_route_duration", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property PlannedTotalRouteDuration As Long?

        ''' <summary>
        ''' Total wait time (seconds).
        ''' </summary>
        <DataMember(Name:="total_wait_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TotalWaitTime As Long?

        ''' <summary>
        ''' UDU Actual travel distance.
        ''' </summary>
        <DataMember(Name:="udu_actual_travel_distance", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property UduActualTravelDistance As Double?

        ''' <summary>
        ''' Actual travel distance.
        ''' </summary>
        <DataMember(Name:="actual_travel_distance", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ActualTravelDistance As Double?

        ''' <summary>
        ''' Actual travel time (seconds).
        ''' </summary>
        <DataMember(Name:="actual_travel_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ActualTravelTime As Long?

        ''' <summary>
        ''' Actual footsteps.
        ''' </summary>
        <DataMember(Name:="actual_footsteps", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ActualFootsteps As Integer?

        ''' <summary>
        ''' Working time.
        ''' </summary>
        <DataMember(Name:="working_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property WorkingTime As Long?

        ''' <summary>
        ''' Driving time.
        ''' </summary>
        <DataMember(Name:="driving_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property DrivingTime As Long?

        ''' <summary>
        ''' Idling time.
        ''' </summary>
        <DataMember(Name:="idling_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property IdlingTime As Long?

        ''' <summary>
        ''' Paying miles
        ''' </summary>
        <DataMember(Name:="paying_miles", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property PayingMiles As Double?

        ''' <summary>
        ''' Geofence polygon type.
        ''' enum: ["circle", "poly", "rect"]
        ''' </summary>
        <DataMember(Name:="geofence_polygon_type", EmitDefaultValue:=False)>
        Public Property GeofencePolygonType As String

        ''' <summary>
        ''' Geofence polygon size.
        ''' </summary>
        <DataMember(Name:="geofence_polygon_size", EmitDefaultValue:=False)>
        Public Property GeofencePolygonSize As Integer?

        ''' <summary>
        ''' Destination count.
        ''' </summary>
        <DataMember(Name:="destination_count", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property DestinationCount As Integer?

        ''' <summary>
        ''' Notes count in the route.
        ''' </summary>
        <DataMember(Name:="notes_count", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property NotesCount As Integer?

        ''' <summary>
        ''' Route notes
        ''' </summary>
        <DataMember(Name:="notes", EmitDefaultValue:=False)>
        Public Property Notes As AddressNote()

        ''' <summary>
        ''' A vehicle assigned to the route.
        ''' </summary>
        <DataMember(Name:="vehicle", EmitDefaultValue:=False)>
        Public Property Vehilce As VehicleV4Response

        ''' <summary>
        ''' Member config key-value pairs.
        ''' </summary>
        <DataMember(Name:="member_config_storage", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property MemberConfigStorage As Dictionary(Of String, String)

        ''' <summary>
        ''' Original route.
        ''' </summary>
        <DataMember(Name:="original_route", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property OriginalRoute As DataObjectRoute

        ''' <summary>
        ''' Bundled items
        ''' </summary>
        <DataMember(Name:="bundle_items", EmitDefaultValue:=True)>
        <[ReadOnly](True)>
        Public Property BundleItems As BundledItemResponse()

        ''' <summary>
        ''' Edge by edge turn-by-turn directions. See <see cref="Direction"/>
        ''' </summary>
        <DataMember(Name:="directions", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property Directions As Direction()

        ''' <summary>
        ''' Edge-wise path to be drawn on the map See <see cref="GeoPoint"/>
        ''' </summary>
        <DataMember(Name:="path", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property Path As GeoPoint()

        ''' <summary>
        ''' A collection of device tracking data with coordinates, speed, and timestamps.
        ''' </summary>
        <DataMember(Name:="tracking_history", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TrackingHistory As TrackingHistory()

    End Class
End Namespace
