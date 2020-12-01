Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class MemberResponse

        <DataMember(Name:="status")>
        Public Property Status As Boolean?

        <DataMember(Name:="geocoding_service")> _
        Public Property GeocodingService As String

        <DataMember(Name:="session_id")>
        Public Property SessionId As String

        <DataMember(Name:="session_guid")> _
        Public Property SessionGuid As String

        <DataMember(Name:="member_id")>
        Public Property MemberId As Integer?

        <DataMember(Name:="api_key")> _
        Public Property ApiKey As String

        <DataMember(Name:="tracking_ttl")>
        Public Property TrackingTtl As Integer?

        <DataMember(Name:="geofence_polygon_shape")> _
        Public Property GeofencePolygonShape As String

        <DataMember(Name:="geofence_polygon_size")>
        Public Property GeofencePolygonSize As Integer?

        <DataMember(Name:="geofence_time_onsite_trigger_secs")>
        Public Property GeofenceTimeOnsiteTriggerSecs As Integer?

        <DataMember(Name:="geofence_minimum_trigger_speed")>
        Public Property GeofenceMinimumTriggerSpeed As Integer?

        <DataMember(Name:="is_subscription_past_due")>
        Public Property IsSubscriptionPastDue As Boolean?

        <DataMember(Name:="visited_departed_enabled")> _
        Public Property VisitedDepartedEnabled As String

        <DataMember(Name:="long_press_enabled")> _
        Public Property LongPressEnabled As String

        <DataMember(Name:="account_type_id")>
        Public Property AccountTypeId As Integer?

        <DataMember(Name:="account_type_alias")> _
        Public Property AccountTypeAlias As String

        <DataMember(Name:="member_type")> _
        Public Property MemberType As String

        <DataMember(Name:="max_stops_per_route")>
        Public Property MaxStopsPerRoute As Integer?

        <DataMember(Name:="max_routes")>
        Public Property MaxRoutes As Integer?

        <DataMember(Name:="routes_planned")>
        Public Property RoutesPlanned As Integer?

        <DataMember(Name:="preferred_units")> _
        Public Property PreferredUnits As String

        <DataMember(Name:="preferred_language")> _
        Public Property PreferredLanguage As String

        <DataMember(Name:="HIDE_ROUTED_ADDRESSES")> _
        Public Property HideRoutedAddresses As String

        <DataMember(Name:="HIDE_VISITED_ADDRESSES")> _
        Public Property HideVisitedAddresses As String

        <DataMember(Name:="HIDE_NONFUTURE_ROUTES")> _
        Public Property HideNonfutureAddresses As String

        <DataMember(Name:="READONLY_USER")> _
        Public Property ReadonlyUser As String

        <DataMember(Name:="auto_logout_ts")>
        Public Property AutoLogoutTs As Integer?
    End Class
End Namespace