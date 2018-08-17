Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class MemberResponse

        <DataMember(Name:="status")> _
        Public Property Status As System.Nullable(Of Boolean)

        <DataMember(Name:="geocoding_service")> _
        Public Property GeocodingService As String

        <DataMember(Name:="session_id")> _
        Public Property SessionId As System.Nullable(Of Integer)

        <DataMember(Name:="session_guid")> _
        Public Property SessionGuid As String

        <DataMember(Name:="member_id")> _
        Public Property MemberId As System.Nullable(Of Integer)

        <DataMember(Name:="api_key")> _
        Public Property ApiKey As String

        <DataMember(Name:="tracking_ttl")> _
        Public Property TrackingTtl As System.Nullable(Of Integer)

        <DataMember(Name:="geofence_polygon_shape")> _
        Public Property GeofencePolygonShape As String

        <DataMember(Name:="geofence_polygon_size")> _
        Public Property GeofencePolygonSize As System.Nullable(Of Integer)

        <DataMember(Name:="geofence_time_onsite_trigger_secs")> _
        Public Property GeofenceTimeOnsiteTriggerSecs As System.Nullable(Of Integer)

        <DataMember(Name:="geofence_minimum_trigger_speed")> _
        Public Property GeofenceMinimumTriggerSpeed As System.Nullable(Of Integer)

        <DataMember(Name:="is_subscription_past_due")> _
        Public Property IsSubscriptionPastDue As System.Nullable(Of Boolean)

        <DataMember(Name:="visited_departed_enabled")> _
        Public Property VisitedDepartedEnabled As String

        <DataMember(Name:="long_press_enabled")> _
        Public Property LongPressEnabled As String

        <DataMember(Name:="account_type_id")> _
        Public Property AccountTypeId As System.Nullable(Of Integer)

        <DataMember(Name:="account_type_alias")> _
        Public Property AccountTypeAlias As String

        <DataMember(Name:="member_type")> _
        Public Property MemberType As String

        <DataMember(Name:="max_stops_per_route")> _
        Public Property MaxStopsPerRoute As System.Nullable(Of Integer)

        <DataMember(Name:="max_routes")> _
        Public Property MaxRoutes As System.Nullable(Of Integer)

        <DataMember(Name:="routes_planned")> _
        Public Property RoutesPlanned As System.Nullable(Of Integer)

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

        <DataMember(Name:="auto_logout_ts")> _
        Public Property AutoLogoutTs As System.Nullable(Of Integer)
    End Class
End Namespace