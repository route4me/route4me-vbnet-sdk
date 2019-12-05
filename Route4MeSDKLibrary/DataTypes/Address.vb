Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class Address
        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As Integer?

        <DataMember(Name:="alias", EmitDefaultValue:=False)> _
        Public Property [Alias] As String

        'the id of the member inside the route4me system
        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId As String

        <DataMember(Name:="first_name", EmitDefaultValue:=False)> _
        Public Property FirstName As String

        <DataMember(Name:="last_name", EmitDefaultValue:=False)> _
        Public Property LastName As String

        <DataMember(Name:="address")> _
        Public Property AddressString As String

        <DataMember(Name:="address_stop_type", EmitDefaultValue:=False)> _
        Public Property AddressStopType As String

        'designate this stop as a depot
        'a route may have multiple depots/points of origin
        <DataMember(Name:="is_depot", EmitDefaultValue:=False)>
        Public Property IsDepot As Boolean?

        'the latitude of this address
        <DataMember(Name:="lat")> _
        Public Property Latitude As Double

        'the longitude of this address
        <DataMember(Name:="lng")> _
        Public Property Longitude As Double

        'the id of the route being viewed, modified, erased
        <DataMember(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId As String

        'if this route was duplicated from an existing route, this value would have the original route's id
        <DataMember(Name:="original_route_id", EmitDefaultValue:=False)> _
        Public Property OriginalRouteId As String

        'the id of the optimization request that was used to initially instantiate this route
        <DataMember(Name:="optimization_problem_id", EmitDefaultValue:=False)> _
        Public Property OptimizationProblemId As String

        <DataMember(Name:="sequence_no", EmitDefaultValue:=False)>
        Public Property SequenceNo As Integer?

        <DataMember(Name:="geocoded", EmitDefaultValue:=False)>
        Public Property Geocoded As Boolean?

        <DataMember(Name:="preferred_geocoding", EmitDefaultValue:=False)>
        Public Property PreferredGeocoding As Integer?

        <DataMember(Name:="failed_geocoding", EmitDefaultValue:=False)>
        Public Property FailedGeocoding As Boolean?

        'when planning a route from the address book or using existing address book ids
        'pass the address book id (contact_id) for an address so that route4me can run
        'analytics on the address book addresses that were used to plan routes, and to find previous visits to 
        'favorite addresses
        <DataMember(Name:="contact_id", EmitDefaultValue:=False)>
        Public Property ContactId As Integer?

        'status flag to mark an address as visited (aka check in)
        <DataMember(Name:="is_visited", EmitDefaultValue:=False)>
        Public Property IsVisited As Boolean?

        'status flag to mark an address as departed (aka check out)
        <DataMember(Name:="is_departed", EmitDefaultValue:=False)>
        Public Property IsDeparted As Boolean?

        'the last known visited timestamp of this address
        <DataMember(Name:="timestamp_last_visited", EmitDefaultValue:=False)>
        Public Property TimestampLastVisited As Long?

        'the last known departed timestamp of this address
        <DataMember(Name:="timestamp_last_departed", EmitDefaultValue:=False)>
        Public Property TimestampLastDeparted As Long?

        <DataMember(Name:="group", EmitDefaultValue:=False)> _
        Public Property Group As String

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="customer_po", EmitDefaultValue:=False)> _
        Public Property CustomerPo As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="invoice_no", EmitDefaultValue:=False)> _
        Public Property InvoiceNo As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="reference_no", EmitDefaultValue:=False)> _
        Public Property ReferenceNo As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="order_no", EmitDefaultValue:=False)> _
        Public Property OrderNo As Object

        <DataMember(Name:="order_id", EmitDefaultValue:=False)>
        Public Property OrderId As Integer?

        <DataMember(Name:="weight", EmitDefaultValue:=False)> _
        Public Property Weight As Object

        <DataMember(Name:="cost", EmitDefaultValue:=False)> _
        Public Property Cost As Object

        <DataMember(Name:="revenue", EmitDefaultValue:=False)> _
        Public Property Revenue As Object

        'the cubic volume that this destination/order/line-item consumes/contains
        'this is how much space it will take up on a vehicle
        <DataMember(Name:="cube", EmitDefaultValue:=False)> _
        Public Property Cube As Object

        'the number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle
        <DataMember(Name:="pieces", EmitDefaultValue:=False)> _
        Public Property Pieces As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        'also used to email clients when vehicles are approaching (future capability)
        <DataMember(Name:="email", EmitDefaultValue:=False)> _
        Public Property Email As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        'also used to sms message clients when vehicles are approaching (future capability)
        <DataMember(Name:="phone", EmitDefaultValue:=False)> _
        Public Property Phone As Object

        'the number of notes that are already associated with this address on the route
        <DataMember(Name:="destination_note_count", EmitDefaultValue:=False)>
        Public Property DestinationNoteCount As Integer?

        'server-side generated amount of km/miles that it will take to get to the next location on the route
        <DataMember(Name:="drive_time_to_next_destination", EmitDefaultValue:=False)>
        Public Property DriveTimeToNextDestination As Long?

        'server-side generated amount of seconds that it will take to get to the next location
        <DataMember(Name:="distance_to_next_destination", EmitDefaultValue:=False)>
        Public Property DistanceToNextDestination As Double?

        'estimated time window start based on the optimization engine, after all the sequencing has been completed
        <DataMember(Name:="generated_time_window_start", EmitDefaultValue:=False)>
        Public Property GeneratedTimeWindowStart As Long?

        'estimated time window end based on the optimization engine, after all the sequencing has been completed
        <DataMember(Name:="generated_time_window_end", EmitDefaultValue:=False)>
        Public Property GeneratedTimeWindowEnd As Long?

        'the unique socket channel name which should be used to get real time alerts
        <DataMember(Name:="channel_name", EmitDefaultValue:=False)> _
        Public Property ChannelName As String

        <DataMember(Name:="time_window_start", EmitDefaultValue:=False)>
        Public Property TimeWindowStart As Long?

        <DataMember(Name:="time_window_end", EmitDefaultValue:=False)>
        Public Property TimeWindowEnd As Long?

        'the expected amount of time that will be spent at this address by the driver/user
        <DataMember(Name:="time", EmitDefaultValue:=False)>
        Public Property Time As Long?

        <DataMember(Name:="notes", EmitDefaultValue:=False)> _
        Public Property Notes As AddressNote()

        <DataMember(Name:="path_to_next", EmitDefaultValue:=False)> _
        Public Property PathToNext As GeoPoint()

        'if present, the priority will sequence addresses in all the optimal routes so that
        'higher priority addresses are general at the beginning of the route sequence
        '1 is the highest priority, 100000 is the lowest
        <DataMember(Name:="priority", EmitDefaultValue:=False)>
        Public Property Priority As Integer?

        'generate optimal routes and driving directions to this curbside lat
        <DataMember(Name:="curbside_lat")>
        Public Property CurbsideLatitude As Double?

        'generate optimal routes and driving directions to the curbside lang
        <DataMember(Name:="curbside_lng")>
        Public Property CurbsideLongitude As Double?

        <DataMember(Name:="time_window_start_2", EmitDefaultValue:=False)>
        Public Property TimeWindowStart2 As Long?

        <DataMember(Name:="time_window_end_2", EmitDefaultValue:=False)>
        Public Property TimeWindowEnd2 As Long?

        <DataMember(Name:="custom_fields", EmitDefaultValue:=False)>
        Public Property CustomFields As Dictionary(Of String, Object)

        <DataMember(Name:="tracking_number", EmitDefaultValue:=False)>
        Public Property tracking_number As String

        <DataMember(Name:="wait_time_to_next_destination", EmitDefaultValue:=False)>
        Public Property WaitTimeToNextDestination As Long?

        <DataMember(Name:="udu_distance_to_next_destination", EmitDefaultValue:=False)>
        Public Property UduDistanceToNextDestination As Double?

        <DataMember(Name:="manifest", EmitDefaultValue:=False)>
        Public Property Manifest As AddressManifest

    End Class
End Namespace