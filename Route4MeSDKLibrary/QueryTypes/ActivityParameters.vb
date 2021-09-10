Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Parameters for the activity feed request.
    ''' </summary>
    Public NotInheritable Class ActivityParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Unique ID of a route.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' Unique ID of a device attached to a vehicle.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="device_id", EmitDefaultValue:=False)>
        Public Property DeviceID As String

        ''' <summary>
        ''' Unique ID of a member.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Limit the number of records in response.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As UInteger?

        ''' <summary>
        ''' Only records from that offset will be considered.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As UInteger?

        ''' <summary>
        ''' If equal to 'true' the response will include team activities.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="team", EmitDefaultValue:=False)>
        Public Property team As String

        ''' <summary>
        ''' Start of the time filter.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="start", EmitDefaultValue:=False)>
        Public Property Start() As Long?

        ''' <summary>
        ''' End of the time filter.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="end", EmitDefaultValue:=False)>
        Public Property [End]() As Long?

        ''' <summary>
        ''' If specified, the response will include only activities of the specified type.
        ''' <para>Query parameter.</para>
        ''' <para>Available values:</para>
        ''' <para>'', 'area-removed', 'area-added', 'area-updated', 'delete-destination', </para>
        ''' <para>'insert-destination', 'destination-out-sequence', 'driver-arrived-early', 'driver-arrived-late', </para>
        ''' <para>'driver-arrived-on-time', 'geofence-left', 'geofence-entered', 'mark-destination-departed', </para>
        ''' <para>'mark-destination-visited', 'member-created', 'member-deleted', 'member-modified', </para>
        ''' <para>'move-destination', 'note-insert', 'route-delete', 'route-optimized', </para>
        ''' <para>'route-owner-changed', 'route-duplicate', 'update-destinations', 'user_message'</para>
        ''' <para>'order-created', 'order-updated', 'order-deleted', 'unapproved-to-execute', 'route-update'</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="activity_type", EmitDefaultValue:=False)>
        Public Property ActivityType As String

        ''' <summary>
        ''' Activity message.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="activity_message", EmitDefaultValue:=False)>
        Public Property ActivityMessage As String

    End Class
End Namespace