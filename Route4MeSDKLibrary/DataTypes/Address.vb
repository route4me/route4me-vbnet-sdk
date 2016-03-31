Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class Address
        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)> _
        Public Property RouteDestinationId() As System.Nullable(Of Integer)
            Get
                Return m_RouteDestinationId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_RouteDestinationId = Value
            End Set
        End Property
        Private m_RouteDestinationId As System.Nullable(Of Integer)

        <DataMember(Name:="alias", EmitDefaultValue:=False)> _
        Public Property [Alias]() As String
            Get
                Return m_Alias
            End Get
            Set(value As String)
                m_Alias = Value
            End Set
        End Property
        Private m_Alias As String

        'the id of the member inside the route4me system
        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId() As String
            Get
                Return m_MemberId
            End Get
            Set(value As String)
                m_MemberId = Value
            End Set
        End Property
        Private m_MemberId As String

        <DataMember(Name:="address")> _
        Public Property AddressString() As String
            Get
                Return m_AddressString
            End Get
            Set(value As String)
                m_AddressString = Value
            End Set
        End Property
        Private m_AddressString As String

        'designate this stop as a depot
        'a route may have multiple depots/points of origin
        <DataMember(Name:="is_depot", EmitDefaultValue:=False)> _
        Public Property IsDepot() As System.Nullable(Of Boolean)
            Get
                Return m_IsDepot
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_IsDepot = Value
            End Set
        End Property
        Private m_IsDepot As System.Nullable(Of Boolean)

        'the latitude of this address
        <DataMember(Name:="lat")> _
        Public Property Latitude() As Double
            Get
                Return m_Latitude
            End Get
            Set(value As Double)
                m_Latitude = Value
            End Set
        End Property
        Private m_Latitude As Double

        'the longitude of this address
        <DataMember(Name:="lng")> _
        Public Property Longitude() As Double
            Get
                Return m_Longitude
            End Get
            Set(value As Double)
                m_Longitude = Value
            End Set
        End Property
        Private m_Longitude As Double

        'the id of the route being viewed, modified, erased
        <DataMember(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId() As String
            Get
                Return m_RouteId
            End Get
            Set(value As String)
                m_RouteId = Value
            End Set
        End Property
        Private m_RouteId As String

        'if this route was duplicated from an existing route, this value would have the original route's id
        <DataMember(Name:="original_route_id", EmitDefaultValue:=False)> _
        Public Property OriginalRouteId() As String
            Get
                Return m_OriginalRouteId
            End Get
            Set(value As String)
                m_OriginalRouteId = Value
            End Set
        End Property
        Private m_OriginalRouteId As String

        'the id of the optimization request that was used to initially instantiate this route
        <DataMember(Name:="optimization_problem_id", EmitDefaultValue:=False)> _
        Public Property OptimizationProblemId() As String
            Get
                Return m_OptimizationProblemId
            End Get
            Set(value As String)
                m_OptimizationProblemId = Value
            End Set
        End Property
        Private m_OptimizationProblemId As String

        <DataMember(Name:="sequence_no", EmitDefaultValue:=False)> _
        Public Property SequenceNo() As System.Nullable(Of Integer)
            Get
                Return m_SequenceNo
            End Get
            Set(value As System.Nullable(Of Integer))
                m_SequenceNo = Value
            End Set
        End Property
        Private m_SequenceNo As System.Nullable(Of Integer)

        <DataMember(Name:="geocoded", EmitDefaultValue:=False)> _
        Public Property Geocoded() As System.Nullable(Of Boolean)
            Get
                Return m_Geocoded
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Geocoded = Value
            End Set
        End Property
        Private m_Geocoded As System.Nullable(Of Boolean)

        <DataMember(Name:="preferred_geocoding", EmitDefaultValue:=False)> _
        Public Property PreferredGeocoding() As System.Nullable(Of Integer)
            Get
                Return m_PreferredGeocoding
            End Get
            Set(value As System.Nullable(Of Integer))
                m_PreferredGeocoding = Value
            End Set
        End Property
        Private m_PreferredGeocoding As System.Nullable(Of Integer)

        <DataMember(Name:="failed_geocoding", EmitDefaultValue:=False)> _
        Public Property FailedGeocoding() As System.Nullable(Of Boolean)
            Get
                Return m_FailedGeocoding
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_FailedGeocoding = value
            End Set
        End Property
        Private m_FailedGeocoding As System.Nullable(Of Boolean)

        'when planning a route from the address book or using existing address book ids
        'pass the address book id (contact_id) for an address so that route4me can run
        'analytics on the address book addresses that were used to plan routes, and to find previous visits to 
        'favorite addresses
        <DataMember(Name:="contact_id", EmitDefaultValue:=False)> _
        Public Property ContactId() As System.Nullable(Of Integer)
            Get
                Return m_ContactId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_ContactId = Value
            End Set
        End Property
        Private m_ContactId As System.Nullable(Of Integer)


        'status flag to mark an address as visited (aka check in)
        <DataMember(Name:="is_visited", EmitDefaultValue:=False)> _
        Public Property IsVisited() As System.Nullable(Of Boolean)
            Get
                Return m_IsVisited
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_IsVisited = Value
            End Set
        End Property
        Private m_IsVisited As System.Nullable(Of Boolean)

        'status flag to mark an address as departed (aka check out)
        <DataMember(Name:="is_departed", EmitDefaultValue:=False)> _
        Public Property IsDeparted() As System.Nullable(Of Boolean)
            Get
                Return m_IsDeparted
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_IsDeparted = Value
            End Set
        End Property
        Private m_IsDeparted As System.Nullable(Of Boolean)

        'the last known visited timestamp of this address
        <DataMember(Name:="timestamp_last_visited", EmitDefaultValue:=False)> _
        Public Property TimestampLastVisited() As System.Nullable(Of UInteger)
            Get
                Return m_TimestampLastVisited
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_TimestampLastVisited = Value
            End Set
        End Property
        Private m_TimestampLastVisited As System.Nullable(Of UInteger)

        'the last known departed timestamp of this address
        <DataMember(Name:="timestamp_last_departed", EmitDefaultValue:=False)> _
        Public Property TimestampLastDeparted() As System.Nullable(Of UInteger)
            Get
                Return m_TimestampLastDeparted
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_TimestampLastDeparted = Value
            End Set
        End Property
        Private m_TimestampLastDeparted As System.Nullable(Of UInteger)

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="customer_po", EmitDefaultValue:=False)> _
        Public Property CustomerPo() As Object
            Get
                Return m_CustomerPo
            End Get
            Set(value As Object)
                m_CustomerPo = Value
            End Set
        End Property
        Private m_CustomerPo As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="invoice_no", EmitDefaultValue:=False)> _
        Public Property InvoiceNo() As Object
            Get
                Return m_InvoiceNo
            End Get
            Set(value As Object)
                m_InvoiceNo = Value
            End Set
        End Property
        Private m_InvoiceNo As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="reference_no", EmitDefaultValue:=False)> _
        Public Property ReferenceNo() As Object
            Get
                Return m_ReferenceNo
            End Get
            Set(value As Object)
                m_ReferenceNo = Value
            End Set
        End Property
        Private m_ReferenceNo As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        <DataMember(Name:="order_no", EmitDefaultValue:=False)> _
        Public Property OrderNo() As Object
            Get
                Return m_OrderNo
            End Get
            Set(value As Object)
                m_OrderNo = Value
            End Set
        End Property
        Private m_OrderNo As Object

        <DataMember(Name:="weight", EmitDefaultValue:=False)> _
        Public Property Weight() As Object
            Get
                Return m_Weight
            End Get
            Set(value As Object)
                m_Weight = Value
            End Set
        End Property
        Private m_Weight As Object

        <DataMember(Name:="cost", EmitDefaultValue:=False)> _
        Public Property Cost() As Object
            Get
                Return m_Cost
            End Get
            Set(value As Object)
                m_Cost = Value
            End Set
        End Property
        Private m_Cost As Object

        <DataMember(Name:="revenue", EmitDefaultValue:=False)> _
        Public Property Revenue() As Object
            Get
                Return m_Revenue
            End Get
            Set(value As Object)
                m_Revenue = Value
            End Set
        End Property
        Private m_Revenue As Object


        'the cubic volume that this destination/order/line-item consumes/contains
        'this is how much space it will take up on a vehicle
        <DataMember(Name:="cube", EmitDefaultValue:=False)> _
        Public Property Cube() As Object
            Get
                Return m_Cube
            End Get
            Set(value As Object)
                m_Cube = Value
            End Set
        End Property
        Private m_Cube As Object

        'the number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle
        <DataMember(Name:="pieces", EmitDefaultValue:=False)> _
        Public Property Pieces() As Object
            Get
                Return m_Pieces
            End Get
            Set(value As Object)
                m_Pieces = Value
            End Set
        End Property
        Private m_Pieces As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        'also used to email clients when vehicles are approaching (future capability)
        <DataMember(Name:="email", EmitDefaultValue:=False)> _
        Public Property Email() As Object
            Get
                Return m_Email
            End Get
            Set(value As Object)
                m_Email = Value
            End Set
        End Property
        Private m_Email As Object

        'pass-through data about this route destination
        'the data will be visible on the manifest, website, and mobile apps
        'also used to sms message clients when vehicles are approaching (future capability)
        <DataMember(Name:="phone", EmitDefaultValue:=False)> _
        Public Property Phone() As Object
            Get
                Return m_Phone
            End Get
            Set(value As Object)
                m_Phone = Value
            End Set
        End Property
        Private m_Phone As Object

        'the number of notes that are already associated with this address on the route
        <DataMember(Name:="destination_note_count", EmitDefaultValue:=False)> _
        Public Property DestinationNoteCount() As System.Nullable(Of Integer)
            Get
                Return m_DestinationNoteCount
            End Get
            Set(value As System.Nullable(Of Integer))
                m_DestinationNoteCount = Value
            End Set
        End Property
        Private m_DestinationNoteCount As System.Nullable(Of Integer)

        'server-side generated amount of km/miles that it will take to get to the next location on the route
        <DataMember(Name:="drive_time_to_next_destination", EmitDefaultValue:=False)> _
        Public Property DriveTimeToNextDestination() As System.Nullable(Of Integer)
            Get
                Return m_DriveTimeToNextDestination
            End Get
            Set(value As System.Nullable(Of Integer))
                m_DriveTimeToNextDestination = Value
            End Set
        End Property
        Private m_DriveTimeToNextDestination As System.Nullable(Of Integer)

        'server-side generated amount of seconds that it will take to get to the next location
        <DataMember(Name:="distance_to_next_destination", EmitDefaultValue:=False)> _
        Public Property DistanceToNextDestination() As System.Nullable(Of Double)
            Get
                Return m_DistanceToNextDestination
            End Get
            Set(value As System.Nullable(Of Double))
                m_DistanceToNextDestination = Value
            End Set
        End Property
        Private m_DistanceToNextDestination As System.Nullable(Of Double)


        'estimated time window start based on the optimization engine, after all the sequencing has been completed
        <DataMember(Name:="generated_time_window_start", EmitDefaultValue:=False)> _
        Public Property GeneratedTimeWindowStart() As System.Nullable(Of Integer)
            Get
                Return m_GeneratedTimeWindowStart
            End Get
            Set(value As System.Nullable(Of Integer))
                m_GeneratedTimeWindowStart = value
            End Set
        End Property
        Private m_GeneratedTimeWindowStart As System.Nullable(Of Integer)

        'estimated time window end based on the optimization engine, after all the sequencing has been completed
        <DataMember(Name:="generated_time_window_end", EmitDefaultValue:=False)> _
        Public Property GeneratedTimeWindowEnd() As System.Nullable(Of Integer)
            Get
                Return m_GeneratedTimeWindowEnd
            End Get
            Set(value As System.Nullable(Of Integer))
                m_GeneratedTimeWindowEnd = Value
            End Set
        End Property
        Private m_GeneratedTimeWindowEnd As System.Nullable(Of Integer)

        'the unique socket channel name which should be used to get real time alerts
        <DataMember(Name:="channel_name", EmitDefaultValue:=False)> _
        Public Property ChannelName() As String
            Get
                Return m_channel_name
            End Get
            Set(value As String)
                m_channel_name = value
            End Set
        End Property
        Private m_channel_name As String

        <DataMember(Name:="time_window_start", EmitDefaultValue:=False)> _
        Public Property TimeWindowStart() As System.Nullable(Of Integer)
            Get
                Return m_TimeWindowStart
            End Get
            Set(value As System.Nullable(Of Integer))
                m_TimeWindowStart = Value
            End Set
        End Property
        Private m_TimeWindowStart As System.Nullable(Of Integer)

        <DataMember(Name:="time_window_end", EmitDefaultValue:=False)> _
        Public Property TimeWindowEnd() As System.Nullable(Of Integer)
            Get
                Return m_TimeWindowEnd
            End Get
            Set(value As System.Nullable(Of Integer))
                m_TimeWindowEnd = Value
            End Set
        End Property
        Private m_TimeWindowEnd As System.Nullable(Of Integer)

        'the expected amount of time that will be spent at this address by the driver/user
        <DataMember(Name:="time", EmitDefaultValue:=False)> _
        Public Property Time() As System.Nullable(Of Integer)
            Get
                Return m_Time
            End Get
            Set(value As System.Nullable(Of Integer))
                m_Time = Value
            End Set
        End Property
        Private m_Time As System.Nullable(Of Integer)

        <DataMember(Name:="notes", EmitDefaultValue:=False)> _
        Public Property Notes() As AddressNote()
            Get
                Return m_Notes
            End Get
            Set(value As AddressNote())
                m_Notes = Value
            End Set
        End Property
        Private m_Notes As AddressNote()

        'if present, the priority will sequence addresses in all the optimal routes so that
        'higher priority addresses are general at the beginning of the route sequence
        '1 is the highest priority, 100000 is the lowest
        <DataMember(Name:="priority", EmitDefaultValue:=False)> _
        Public Property Priority() As System.Nullable(Of Integer)
            Get
                Return m_Priority
            End Get
            Set(value As System.Nullable(Of Integer))
                m_Priority = Value
            End Set
        End Property
        Private m_Priority As System.Nullable(Of Integer)

        'generate optimal routes and driving directions to this curbside lat
        <DataMember(Name:="curbside_lat")> _
        Public Property CurbsideLatitude() As System.Nullable(Of Double)
            Get
                Return m_CurbsideLatitude
            End Get
            Set(value As System.Nullable(Of Double))
                m_CurbsideLatitude = Value
            End Set
        End Property
        Private m_CurbsideLatitude As System.Nullable(Of Double)

        'generate optimal routes and driving directions to the curbside lang
        <DataMember(Name:="curbside_lng")> _
        Public Property CurbsideLongitude() As System.Nullable(Of Double)
            Get
                Return m_CurbsideLongitude
            End Get
            Set(value As System.Nullable(Of Double))
                m_CurbsideLongitude = Value
            End Set
        End Property
        Private m_CurbsideLongitude As System.Nullable(Of Double)

        <DataMember(Name:="time_window_start_2", EmitDefaultValue:=False)> _
        Public Property TimeWindowStart2() As System.Nullable(Of Integer)
            Get
                Return m_TimeWindowStart2
            End Get
            Set(value As System.Nullable(Of Integer))
                m_TimeWindowStart2 = Value
            End Set
        End Property
        Private m_TimeWindowStart2 As System.Nullable(Of Integer)

        <DataMember(Name:="time_window_end_2", EmitDefaultValue:=False)> _
        Public Property TimeWindowEnd2() As System.Nullable(Of Integer)
            Get
                Return m_TimeWindowEnd2
            End Get
            Set(value As System.Nullable(Of Integer))
                m_TimeWindowEnd2 = Value
            End Set
        End Property
        Private m_TimeWindowEnd2 As System.Nullable(Of Integer)

        <DataMember(Name:="custom_fields", EmitDefaultValue:=False)> _
        Public Property CustomFields() As Dictionary(Of String, String)
            Get
                Return m_CustomFields
            End Get
            Set(value As Dictionary(Of String, String))
                m_CustomFields = Value
            End Set
        End Property
        Private m_CustomFields As Dictionary(Of String, String)
    End Class
End Namespace