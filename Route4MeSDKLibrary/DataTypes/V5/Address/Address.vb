Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.ComponentModel

Namespace Route4MeSDK.DataTypes.V5
    ''' <summary>
    ''' Route destination class.
    ''' <para>See the JSON schema at <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/Address.dtd"/> </para>
    ''' </summary>
    <DataContract>
    Public NotInheritable Class Address
        Inherits GenericParameters

        ''' <summary>
        ''' Route destination ID
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As Integer?

        ''' <summary>
        ''' Route alias
        ''' </summary>
        <DataMember(Name:="alias", EmitDefaultValue:=False)>
        Public Property [Alias] As String

        ''' <summary>
        ''' Member ID
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' First name
        ''' </summary>
        <DataMember(Name:="first_name", EmitDefaultValue:=False)>
        Public Property FirstName As String

        ''' <summary>
        ''' Last name
        ''' </summary>
        <DataMember(Name:="last_name", EmitDefaultValue:=False)>
        Public Property LastName As String

        ''' <summary>
        ''' Route destination address
        ''' </summary>
        <DataMember(Name:="address")>
        Public Property AddressString As String

        ''' <summary>
        ''' Route address stop type
        ''' </summary>
        <DataMember(Name:="address_stop_type")>
        Public Property AddressStopType As String

        ''' <summary>
        ''' Designate this stop as a depot.
        ''' A route may have multiple depots/points of origin.
        ''' </summary>
        <DataMember(Name:="is_depot", EmitDefaultValue:=False)>
        Public Property IsDepot As Boolean?

        ''' <summary>
        ''' Timeframe violation state
        ''' </summary>
        <DataMember(Name:="timeframe_violation_state", EmitDefaultValue:=False)>
        Public Property TimeframeViolationState As Integer?

        ''' <summary>
        ''' Timeframe violation time
        ''' </summary>
        <DataMember(Name:="timeframe_violation_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TimeframeViolationTime As Long?

        ''' <summary>
        ''' Timeframe violation rate
        ''' </summary>
        <DataMember(Name:="timeframe_violation_rate", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TimeframeViolationRate As Double?

        ''' <summary>
        ''' The latitude of this address
        ''' </summary>
        <DataMember(Name:="lat")>
        Public Property Latitude As Double

        ''' <summary>
        ''' The longitude of this address
        ''' </summary>
        <DataMember(Name:="lng")>
        Public Property Longitude As Double

        ''' <summary>
        ''' The ID of the route being viewed, modified, or erased.
        ''' </summary>
        <DataMember(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' If this route was duplicated from an existing route, this value would have the original route's ID.
        ''' </summary>
        <DataMember(Name:="original_route_id", EmitDefaultValue:=False)>
        Public Property OriginalRouteId As String

        ''' <summary>
        ''' Route name of a depot address.
        ''' </summary>
        <DataMember(Name:="route_name", EmitDefaultValue:=False)>
        Public Property RouteName As String

        ''' <summary>
        ''' The ID of the optimization request that was used to initially instantiate this route.
        ''' </summary>
        <DataMember(Name:="optimization_problem_id", EmitDefaultValue:=False)>
        Public Property OptimizationProblemId As String

        ''' <summary>
        ''' The destination's sequence number in the route.
        ''' </summary>
        <DataMember(Name:="sequence_no", EmitDefaultValue:=False)>
        Public Property SequenceNo As Integer?

        ''' <summary>
        ''' True if the address is geocoded.
        ''' </summary>
        <DataMember(Name:="geocoded", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property Geocoded As Boolean?

        ''' <summary>
        ''' The preferred geocoding number.
        ''' </summary>
        <DataMember(Name:="preferred_geocoding", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property PreferredGeocoding As Integer?

        ''' <summary>
        ''' True if geocoding failed.
        ''' </summary>
        <DataMember(Name:="failed_geocoding", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property FailedGeocoding As Boolean?

        ''' <summary>
        ''' An array containing Geocoding objects.
        ''' </summary>
        <DataMember(Name:="geocodings", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property Geocodings As Geocoding()

        ''' <summary>
        ''' When planning a route from the address book or using existing address book IDs, 
        ''' pass the address book ID (contact_id) for an address so that Route4Me can run
        ''' analytics on the address book addresses that were used to plan routes, And to find previous visits to 
        ''' favorite addresses.
        ''' </summary>
        <DataMember(Name:="contact_id", EmitDefaultValue:=False)>
        Public Property ContactId As Integer?

        ''' <summary>
        ''' The status flag to mark an address as visited (aka check in).
        ''' </summary>
        <DataMember(Name:="is_visited", EmitDefaultValue:=False)>
        Public Property IsVisited As Boolean?

        ''' <summary>
        ''' The status flag to mark an address as departed (aka check out).
        ''' </summary>
        <DataMember(Name:="is_departed", EmitDefaultValue:=False)>
        Public Property IsDeparted As Boolean?

        ''' <summary>
        ''' The last known visited timestamp of this address.
        ''' </summary>
        <DataMember(Name:="timestamp_last_visited", EmitDefaultValue:=False)>
        Public Property TimestampLastVisited As Long?

        ''' <summary>
        ''' The last known departed timestamp of this address.
        ''' </summary>
        <DataMember(Name:="timestamp_last_departed", EmitDefaultValue:=False)>
        Public Property TimestampLastDeparted As Long?

        ''' <summary>
        ''' Visited address latitude
        ''' </summary>
        <DataMember(Name:="visited_lat")>
        Public Property VisitedLatitude As Double?

        ''' <summary>
        ''' Visited address longitude
        ''' </summary>
        <DataMember(Name:="visited_lng")>
        Public Property VisitedLongitude As Double?

        ''' <summary>
        ''' Departed address latitude
        ''' </summary>
        <DataMember(Name:="departed_lat")>
        Public Property DepartedLatitude As Double?

        ''' <summary>
        ''' Departed address longitude
        ''' </summary>
        <DataMember(Name:="departed_lng")>
        Public Property DepartedLongitude As Double?

        ''' <summary>
        ''' The address group
        ''' </summary>
        <DataMember(Name:="group", EmitDefaultValue:=False)>
        Public Property Group As String

        ''' <summary>
        ''' Pass-through data about this route destination.
        ''' The data will be visible on the manifest, website, And mobile apps.
        ''' </summary>
        <DataMember(Name:="customer_po", EmitDefaultValue:=False)>
        Public Property CustomerPo As String

        ''' <summary>
        ''' Pass-through data about this route destination.
        ''' The data will be visible on the manifest, website, And mobile apps.
        ''' </summary>
        <DataMember(Name:="invoice_no", EmitDefaultValue:=False)>
        Public Property InvoiceNo As String

        ''' <summary>
        ''' Pass-through data about this route destination.
        ''' The data will be visible on the manifest, website, And mobile apps.
        ''' </summary>
        <DataMember(Name:="reference_no", EmitDefaultValue:=False)>
        Public Property ReferenceNo As String

        ''' <summary>
        ''' Pass-through data about this route destination.
        ''' The data will be visible on the manifest, website, And mobile apps.
        ''' </summary>
        <DataMember(Name:="order_no", EmitDefaultValue:=False)>
        Public Property OrderNo As String

        ''' <summary>
        ''' The address order ID
        ''' </summary>
        <DataMember(Name:="order_id", EmitDefaultValue:=False)>
        Public Property OrderId As Integer?

        ''' <summary>
        ''' The address cargo weight
        ''' </summary>
        <DataMember(Name:="weight", EmitDefaultValue:=False)>
        Public Property Weight As Double?

        ''' <summary>
        ''' The address cost
        ''' </summary>
        <DataMember(Name:="cost", EmitDefaultValue:=False)>
        Public Property Cost As Double?

        ''' <summary>
        ''' The address revenue
        ''' </summary>
        <DataMember(Name:="revenue", EmitDefaultValue:=False)>
        Public Property Revenue As Double?

        ''' <summary>
        ''' The cubic volume that this destination/order/line-item consumes/contains.
        ''' This Is how much space it will take up on a vehicle.
        ''' </summary>
        <DataMember(Name:="cube", EmitDefaultValue:=False)>
        Public Property Cube As Double?

        ''' <summary>
        ''' The number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle.
        ''' </summary>
        <DataMember(Name:="pieces", EmitDefaultValue:=False)>
        Public Property Pieces As Integer?

        ''' <summary>
        ''' Pass-through data about this route destination.
        ''' The data will be visible on the manifest, website, And mobile apps.
        ''' Also used to email clients when vehicles are approaching (future capability).
        ''' </summary>
        <DataMember(Name:="email", EmitDefaultValue:=False)>
        Public Property Email As String

        ''' <summary>
        ''' Pass-through data about this route destination.
        ''' The data will be visible on the manifest, website, And mobile apps.
        ''' Also used to send SMS messages to clients when vehicles are approaching (future capability).
        ''' </summary>
        <DataMember(Name:="phone", EmitDefaultValue:=False)>
        Public Property Phone As String

        ''' <summary>
        ''' The number of notes that are already associated with this address on the route.
        ''' </summary>
        <DataMember(Name:="destination_note_count", EmitDefaultValue:=False)>
        Public Property DestinationNoteCount As Integer?

        ''' <summary>
        ''' Server-side generated amount of km/miles that it will take to get to the next location on the route.
        ''' </summary>
        <DataMember(Name:="drive_time_to_next_destination", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property DriveTimeToNextDestination As Long?

        ''' <summary>
        ''' Abnormal traffic time to next destination.
        ''' </summary>
        <DataMember(Name:="abnormal_traffic_time_to_next_destination", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property AbnormalTrafficTimeToNextDestination As Long?

        ''' <summary>
        ''' Uncongested time to next destination.
        ''' </summary>
        <DataMember(Name:="uncongested_time_to_next_destination", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property UncongestedTimeToNextDestination As Long?

        ''' <summary>
        ''' Traffic time to next destination.
        ''' </summary>
        <DataMember(Name:="traffic_time_to_next_destination", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TrafficTimeToNextDestination As Long?

        ''' <summary>
        ''' Server-side generated amount of seconds that it will take to get to the next location.
        ''' </summary>
        <DataMember(Name:="distance_to_next_destination", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property DistanceToNextDestination As Double?

        ''' <summary>
        ''' UDU distance to next destination.
        ''' </summary>
        <DataMember(Name:="udu_distance_to_next_destination", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property UduDistanceToNextDestination As Double?

        ''' <summary>
        ''' Generated time window start.
        ''' </summary>
        <DataMember(Name:="generated_time_window_start", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property GeneratedTimeWindowStart As Long?

        ''' <summary>
        ''' Estimated time window end based on the optimization engine, after all the sequencing has been completed.
        ''' </summary>
        <DataMember(Name:="generated_time_window_end", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property GeneratedTimeWindowEnd As Long?

        ''' <summary>
        ''' The unique socket channel name which should be used to get real time alerts.
        ''' </summary>
        <DataMember(Name:="channel_name", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ChannelName As String

        ''' <summary>
        ''' The address time window start.
        ''' </summary>
        <DataMember(Name:="time_window_start", EmitDefaultValue:=False)>
        Public Property TimeWindowStart As Long?

        ''' <summary>
        ''' The address time window end.
        ''' </summary>
        <DataMember(Name:="time_window_end", EmitDefaultValue:=False)>
        Public Property TimeWindowEnd As Long?

        ''' <summary>
        ''' The address time window start 2.
        ''' </summary>
        <DataMember(Name:="time_window_start_2", EmitDefaultValue:=False)>
        Public Property TimeWindowStart2 As Long?

        ''' <summary>
        ''' The address time window end 2.
        ''' </summary>
        <DataMember(Name:="time_window_end_2", EmitDefaultValue:=False)>
        Public Property TimeWindowEnd2 As Long?

        ''' <summary>
        ''' Geofence detected visited timestamp
        ''' </summary>
        <DataMember(Name:="geofence_detected_visited_timestamp", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property geofence_detected_visited_timestamp As Long?

        ''' <summary>
        ''' Geofence detected departed timestamp
        ''' </summary>
        <DataMember(Name:="geofence_detected_departed_timestamp", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property geofence_detected_departed_timestamp As Long?

        ''' <summary>
        ''' Geofence detected service time
        ''' </summary>
        <DataMember(Name:="geofence_detected_service_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property geofence_detected_service_time As Long?

        ''' <summary>
        ''' Geofence detected visited latitude
        ''' </summary>
        <DataMember(Name:="geofence_detected_visited_lat", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property geofence_detected_visited_lat As Double?

        ''' <summary>
        ''' Geofence detected visited longitude
        ''' </summary>
        <DataMember(Name:="geofence_detected_visited_lng", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property geofence_detected_visited_lng As Double?

        ''' <summary>
        ''' Geofence detected departed latitude
        ''' </summary>
        <DataMember(Name:="geofence_detected_departed_lat", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property geofence_detected_departed_lat As Double?

        ''' <summary>
        ''' Geofence detected departed longitude
        ''' </summary>
        <DataMember(Name:="geofence_detected_departed_lng", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property geofence_detected_departed_lng As Double?

        ''' <summary>
        ''' The expected amount of time that will be spent at this address by the driver/user.
        ''' </summary>
        <DataMember(Name:="time", EmitDefaultValue:=False)>
        Public Property Time As Long?

        ''' <summary>
        ''' The address notes
        ''' </summary>
        <DataMember(Name:="notes", EmitDefaultValue:=False)>
        Public Property Notes As AddressNote()

        ''' <summary>
        ''' The route path point.
        ''' </summary>
        <DataMember(Name:="path_to_next", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property PathToNext As GeoPoint()

        ''' <summary>
        ''' If present, the priority will sequence addresses in all the optimal routes so that
        ''' higher priority addresses are general at the beginning of the route sequence.
        ''' 1 Is the highest priority, 100000 Is the lowest.
        ''' </summary>
        <DataMember(Name:="priority", EmitDefaultValue:=False)>
        Public Property Priority As Integer?

        ''' <summary>
        ''' Curbside latitude.
        ''' Generate optimal routes And driving directions to this curbside latitude.
        ''' </summary>
        <DataMember(Name:="curbside_lat")>
        Public Property CurbsideLatitude As Double?

        ''' <summary>
        ''' Curbside longitude.
        ''' Generate optimal routes And driving directions to the curbside longitude.
        ''' </summary>
        <DataMember(Name:="curbside_lng")>
        Public Property CurbsideLongitude As Double?

        ''' <summary>
        ''' The address custom fields.
        ''' </summary>
        <DataMember(Name:="custom_fields", EmitDefaultValue:=False)>
        Public Property CustomFields As Dictionary(Of String, String)

        ''' <summary>
        ''' he address custom fields in JSON format.
        ''' </summary>
        <DataMember(Name:="custom_fields_str_json", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property CustomFieldsStrJson As String

        ''' <summary>
        ''' The custom fields configuration.
        ''' </summary>
        <DataMember(Name:="custom_fields_config", EmitDefaultValue:=False)>
        Public Property CustomFieldsConfig As String()

        ''' <summary>
        ''' The custom fields configuration in JSON format.
        ''' </summary>
        <DataMember(Name:="custom_fields_config_str_json", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property CustomFieldsConfigStrJson As String

        ''' <summary>
        ''' System-wide unique code, which permits end-users (recipients) to track the status of their order.
        ''' </summary>
        <DataMember(Name:="tracking_number", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property tracking_number As String

        ''' <summary>
        ''' Wait time to next destination.
        ''' </summary>
        <DataMember(Name:="wait_time_to_next_destination", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property WaitTimeToNextDestination As Long?

        ''' <summary>
        ''' Manifest of a route address.
        ''' </summary>
        <DataMember(Name:="manifest", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property Manifest As AddressManifest

        ''' <summary>
        ''' Alias of a pickup point.
        ''' </summary>
        <DataMember(Name:="pickup", EmitDefaultValue:=False)>
        Public Property Pickup As String

        ''' <summary>
        ''' lias of the paired pickup point.
        ''' </summary>
        <DataMember(Name:="dropoff", EmitDefaultValue:=False)>
        Public Property Dropoff As String

        ''' <summary>
        ''' If equal to 1, the pickup and dropoff addresses are joint 
        ''' (one by one despite the regular pickup-dropoff addresses 
        ''' when it's possible to have multiple pickup addresses with one dropoff address).
        ''' </summary>
        <DataMember(Name:="joint", EmitDefaultValue:=False)>
        Public Property Joint As Integer?

        ''' <summary>
        ''' Bundle count
        ''' </summary>
        <DataMember(Name:="bundle_count", EmitDefaultValue:=True)>
        <DefaultValue(0)>
        <[ReadOnly](True)>
        Public Property BundleCount As Integer

        ''' <summary>
        ''' Bundle items
        ''' </summary>
        <DataMember(Name:="bundle_items", EmitDefaultValue:=True)>
        <[ReadOnly](True)>
        Public Property BundleItems As BundledItemResponse()

        ''' <summary>
        ''' List of the order inventories
        ''' </summary>
        <DataMember(Name:="order_inventory", EmitDefaultValue:=True)>
        <[ReadOnly](True)>
        Public Property OrderInventory As V5.OrderInventory()

        ''' <summary>
        ''' The driver tags specified in a team member's custom data.
        ''' (e.g. "driver skills" 
        ''' ["Class A CDL", "Class B CDL", "Forklift", "Skid Steer Loader", "Independent Contractor"]
        ''' </summary>
        <DataMember(Name:="tags", EmitDefaultValue:=False)>
        <DefaultValue(False)>
        Public Property Tags As String()

    End Class
End Namespace

