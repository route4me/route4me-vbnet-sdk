Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class MemberResponse

        <DataMember(Name:="status")> _
        Public Property Status() As System.Nullable(Of Boolean)
            Get
                Return m_Status
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Status = value
            End Set
        End Property
        Private m_Status As System.Nullable(Of Boolean)

        <DataMember(Name:="geocoding_service")> _
        Public Property GeocodingService() As String
            Get
                Return m_GeocodingService
            End Get
            Set(value As String)
                m_GeocodingService = value
            End Set
        End Property
        Private m_GeocodingService As String

        <DataMember(Name:="session_id")> _
        Public Property SessionId() As System.Nullable(Of Integer)
            Get
                Return m_SessionId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_SessionId = value
            End Set
        End Property
        Private m_SessionId As System.Nullable(Of Integer)

        <DataMember(Name:="session_guid")> _
        Public Property SessionGuid() As String
            Get
                Return m_SessionGuid
            End Get
            Set(value As String)
                m_SessionGuid = value
            End Set
        End Property
        Private m_SessionGuid As String

        <DataMember(Name:="member_id")> _
        Public Property MemberId() As System.Nullable(Of Integer)
            Get
                Return m_MemberId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As System.Nullable(Of Integer)

        <DataMember(Name:="api_key")> _
        Public Property ApiKey() As String
            Get
                Return m_ApiKey
            End Get
            Set(value As String)
                m_ApiKey = value
            End Set
        End Property
        Private m_ApiKey As String

        <DataMember(Name:="tracking_ttl")> _
        Public Property TrackingTtl() As System.Nullable(Of Integer)
            Get
                Return m_TrackingTtl
            End Get
            Set(value As System.Nullable(Of Integer))
                m_TrackingTtl = value
            End Set
        End Property
        Private m_TrackingTtl As System.Nullable(Of Integer)

        <DataMember(Name:="geofence_polygon_shape")> _
        Public Property GeofencePolygonShape() As String
            Get
                Return m_GeofencePolygonShape
            End Get
            Set(value As String)
                m_GeofencePolygonShape = value
            End Set
        End Property
        Private m_GeofencePolygonShape As String

        <DataMember(Name:="geofence_polygon_size")> _
        Public Property GeofencePolygonSize() As System.Nullable(Of Integer)
            Get
                Return m_GeofencePolygonSize
            End Get
            Set(value As System.Nullable(Of Integer))
                m_GeofencePolygonSize = value
            End Set
        End Property
        Private m_GeofencePolygonSize As System.Nullable(Of Integer)

        <DataMember(Name:="geofence_time_onsite_trigger_secs")> _
        Public Property GeofenceTimeOnsiteTriggerSecs() As System.Nullable(Of Integer)
            Get
                Return m_GeofenceTimeOnsiteTriggerSecs
            End Get
            Set(value As System.Nullable(Of Integer))
                m_GeofenceTimeOnsiteTriggerSecs = value
            End Set
        End Property
        Private m_GeofenceTimeOnsiteTriggerSecs As System.Nullable(Of Integer)

        <DataMember(Name:="geofence_minimum_trigger_speed")> _
        Public Property GeofenceMinimumTriggerSpeed() As System.Nullable(Of Integer)
            Get
                Return m_GeofenceMinimumTriggerSpeed
            End Get
            Set(value As System.Nullable(Of Integer))
                m_GeofenceMinimumTriggerSpeed = value
            End Set
        End Property
        Private m_GeofenceMinimumTriggerSpeed As System.Nullable(Of Integer)

        <DataMember(Name:="is_subscription_past_due")> _
        Public Property IsSubscriptionPastDue() As System.Nullable(Of Boolean)
            Get
                Return m_IsSubscriptionPastDue
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_IsSubscriptionPastDue = value
            End Set
        End Property
        Private m_IsSubscriptionPastDue As System.Nullable(Of Boolean)

        <DataMember(Name:="visited_departed_enabled")> _
        Public Property VisitedDepartedEnabled() As String
            Get
                Return m_VisitedDepartedEnabled
            End Get
            Set(value As String)
                m_VisitedDepartedEnabled = value
            End Set
        End Property
        Private m_VisitedDepartedEnabled As String

        <DataMember(Name:="long_press_enabled")> _
        Public Property LongPressEnabled() As String
            Get
                Return m_LongPressEnabled
            End Get
            Set(value As String)
                m_LongPressEnabled = value
            End Set
        End Property
        Private m_LongPressEnabled As String

        <DataMember(Name:="account_type_id")> _
        Public Property AccountTypeId() As System.Nullable(Of Integer)
            Get
                Return m_AccountTypeId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_AccountTypeId = value
            End Set
        End Property
        Private m_AccountTypeId As System.Nullable(Of Integer)

        <DataMember(Name:="account_type_alias")> _
        Public Property AccountTypeAlias() As System.Nullable(Of Integer)
            Get
                Return m_AccountTypeAlias
            End Get
            Set(value As System.Nullable(Of Integer))
                m_AccountTypeAlias = value
            End Set
        End Property
        Private m_AccountTypeAlias As System.Nullable(Of Integer)

        <DataMember(Name:="member_type")> _
        Public Property MemberType() As String
            Get
                Return m_MemberType
            End Get
            Set(value As String)
                m_MemberType = value
            End Set
        End Property
        Private m_MemberType As String

        <DataMember(Name:="max_stops_per_route")> _
        Public Property MaxStopsPerRoute() As System.Nullable(Of Integer)
            Get
                Return m_MaxStopsPerRoute
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MaxStopsPerRoute = value
            End Set
        End Property
        Private m_MaxStopsPerRoute As System.Nullable(Of Integer)

        <DataMember(Name:="max_routes")> _
        Public Property MaxRoutes() As System.Nullable(Of Integer)
            Get
                Return m_MaxRoutes
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MaxRoutes = value
            End Set
        End Property
        Private m_MaxRoutes As System.Nullable(Of Integer)

        <DataMember(Name:="routes_planned")> _
        Public Property RoutesPlanned() As System.Nullable(Of Integer)
            Get
                Return m_RoutesPlanned
            End Get
            Set(value As System.Nullable(Of Integer))
                m_RoutesPlanned = value
            End Set
        End Property
        Private m_RoutesPlanned As System.Nullable(Of Integer)

        <DataMember(Name:="preferred_units")> _
        Public Property PreferredUnits() As String
            Get
                Return m_PreferredUnits
            End Get
            Set(value As String)
                m_PreferredUnits = value
            End Set
        End Property
        Private m_PreferredUnits As String

        <DataMember(Name:="preferred_language")> _
        Public Property PreferredLanguage() As String
            Get
                Return m_PreferredLanguage
            End Get
            Set(value As String)
                m_PreferredLanguage = value
            End Set
        End Property
        Private m_PreferredLanguage As String

        <DataMember(Name:="HIDE_ROUTED_ADDRESSES")> _
        Public Property HideRoutedAddresses() As String
            Get
                Return m_HideRoutedAddresses
            End Get
            Set(value As String)
                m_HideRoutedAddresses = value
            End Set
        End Property
        Private m_HideRoutedAddresses As String

        <DataMember(Name:="HIDE_VISITED_ADDRESSES")> _
        Public Property HideVisitedAddresses() As String
            Get
                Return m_HideVisitedAddresses
            End Get
            Set(value As String)
                m_HideVisitedAddresses = value
            End Set
        End Property
        Private m_HideVisitedAddresses As String

        <DataMember(Name:="HIDE_NONFUTURE_ROUTES")> _
        Public Property HideNonfutureAddresses() As String
            Get
                Return m_HideNonfutureAddresses
            End Get
            Set(value As String)
                m_HideNonfutureAddresses = value
            End Set
        End Property
        Private m_HideNonfutureAddresses As String

        <DataMember(Name:="READONLY_USER")> _
        Public Property ReadonlyUser() As String
            Get
                Return m_ReadonlyUser
            End Get
            Set(value As String)
                m_ReadonlyUser = value
            End Set
        End Property
        Private m_ReadonlyUser As String

        <DataMember(Name:="auto_logout_ts")> _
        Public Property AutoLogoutTs() As System.Nullable(Of Integer)
            Get
                Return m_AutoLogoutTs
            End Get
            Set(value As System.Nullable(Of Integer))
                m_AutoLogoutTs = value
            End Set
        End Property
        Private m_AutoLogoutTs As System.Nullable(Of Integer)

    End Class
End Namespace