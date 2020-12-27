Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' Response for the user's authentication, registration, session validation process
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class MemberResponse

        ''' <summary>
        ''' Process status
        ''' </summary>
        <DataMember(Name:="status")>
        Public Property Status As Boolean?

        ''' <summary>
        ''' Geocoding service
        ''' </summary>
        <DataMember(Name:="geocoding_service")> _
        Public Property GeocodingService As String

        ''' <summary>
        ''' Session ID
        ''' </summary>
        <DataMember(Name:="session_id")>
        Public Property SessionId As String

        ''' <summary>
        ''' Session GUID
        ''' </summary>
        <DataMember(Name:="session_guid")> _
        Public Property SessionGuid As String

        ''' <summary>
        ''' Member ID
        ''' </summary>
        <DataMember(Name:="member_id")>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' User's API key
        ''' </summary>
        <DataMember(Name:="api_key")> _
        Public Property ApiKey As String

        ''' <summary>
        ''' Tracking TTL
        ''' </summary>
        <DataMember(Name:="tracking_ttl")>
        Public Property TrackingTtl As Integer?

        ''' <summary>
        ''' Geofence polygon shape. Available values: circle, poly, rect.
        ''' </summary>
        <DataMember(Name:="geofence_polygon_shape")> _
        Public Property GeofencePolygonShape As String

        ''' <summary>
        ''' Geofence polygon size
        ''' </summary>
        <DataMember(Name:="geofence_polygon_size")>
        Public Property GeofencePolygonSize As Integer?

        ''' <summary>
        ''' Geofence onsite trigger time (seconds)
        ''' </summary>
        <DataMember(Name:="geofence_time_onsite_trigger_secs")>
        Public Property GeofenceTimeOnsiteTriggerSecs As Integer?

        ''' <summary>
        ''' Geofence's minimum trigger speed
        ''' </summary>
        <DataMember(Name:="geofence_minimum_trigger_speed")>
        Public Property GeofenceMinimumTriggerSpeed As Integer?

        ''' <summary>
        ''' True if the subscription is past due
        ''' </summary>
        <DataMember(Name:="is_subscription_past_due")>
        Public Property IsSubscriptionPastDue As Boolean?

        ''' <summary>
        ''' If true, triggering of the visited and departed activities is enabled.
        ''' </summary>
        <DataMember(Name:="visited_departed_enabled")> _
        Public Property VisitedDepartedEnabled As String

        ''' <summary>
        ''' If true, long press is enabled
        ''' </summary>
        <DataMember(Name:="long_press_enabled")> _
        Public Property LongPressEnabled As String

        ''' <summary>
        ''' The account type ID
        ''' </summary>
        <DataMember(Name:="account_type_id")>
        Public Property AccountTypeId As Integer?

        ''' <summary>
        ''' Account type alias
        ''' </summary>
        <DataMember(Name:="account_type_alias")> _
        Public Property AccountTypeAlias As String

        ''' <summary>
        ''' Member type. Available values:
        ''' <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ''' <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER,</para>
        ''' <para>SUB_ACCOUNT_ANALYSTSUB_ACCOUNT_VENDORSUB_ACCOUNT_CUSTOMER_SERVICE</para>
        ''' </summary>
        <DataMember(Name:="member_type")> _
        Public Property MemberType As String

        ''' <summary>
        ''' Maximum allowed number of the stops per route.
        ''' </summary>
        <DataMember(Name:="max_stops_per_route")>
        Public Property MaxStopsPerRoute As Integer?

        ''' <summary>
        ''' Maximum allowed number of the generated routes
        ''' </summary>
        <DataMember(Name:="max_routes")>
        Public Property MaxRoutes As Integer?

        ''' <summary>
        ''' Number of the planned routes by the user
        ''' </summary>
        <DataMember(Name:="routes_planned")>
        Public Property RoutesPlanned As Integer?

        ''' <summary>
        ''' Preferred units (mi, km)
        ''' </summary>
        <DataMember(Name:="preferred_units")> _
        Public Property PreferredUnits As String

        ''' <summary>
        ''' Preferred language (en, fr)
        ''' </summary>
        <DataMember(Name:="preferred_language")> _
        Public Property PreferredLanguage As String

        ''' <summary>
        ''' If true, routed addresses will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_ROUTED_ADDRESSES")> _
        Public Property HideRoutedAddresses As String

        ''' <summary>
        ''' If true, visited addresses will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_VISITED_ADDRESSES")> _
        Public Property HideVisitedAddresses As String

        ''' <summary>
        ''' If true, nonfuture routes will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_NONFUTURE_ROUTES")> _
        Public Property HideNonfutureAddresses As String

        ''' <summary>
        ''' 
        ''' </summary>
        <DataMember(Name:="READONLY_USER")> _
        Public Property ReadonlyUser As String

        ''' <summary>
        ''' Time in seconds. A user will be logged out after been inactive during specified by this parameter seconds.
        ''' </summary>
        <DataMember(Name:="auto_logout_ts")>
        Public Property AutoLogoutTs As Integer?

    End Class
End Namespace