Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class AddressBookContact
        Inherits GenericParameters

        <DataMember(Name:="created_timestamp", EmitDefaultValue:=False)> _
        Public Property CreatedTimestamp() As System.Nullable(Of Integer)
            Get
                Return m_CreatedTimestamp
            End Get
            Set(value As System.Nullable(Of Integer))
                m_CreatedTimestamp = value
            End Set
        End Property
        Private m_CreatedTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="address_id", EmitDefaultValue:=False)> _
Public Property AddressId() As String
            Get
                Return m_AddressId
            End Get
            Set(value As String)
                m_AddressId = value
            End Set
        End Property
        Private m_AddressId As String

        <DataMember(Name:="address_group", EmitDefaultValue:=False)> _
        Public Property AddressGroup() As String
            Get
                Return m_AddressGroup
            End Get
            Set(value As String)
                m_AddressGroup = Value
            End Set
        End Property
        Private m_AddressGroup As String

        <DataMember(Name:="address_alias", EmitDefaultValue:=False)> _
        Public Property AddressAlias() As String
            Get
                Return m_AddressAlias
            End Get
            Set(value As String)
                m_AddressAlias = Value
            End Set
        End Property
        Private m_AddressAlias As String

        <DataMember(Name:="address_1")> _
        Public Property Address1() As String
            Get
                Return m_Address1
            End Get
            Set(value As String)
                m_Address1 = Value
            End Set
        End Property
        Private m_Address1 As String

        <DataMember(Name:="address_2", EmitDefaultValue:=False)> _
        Public Property Address2() As String
            Get
                Return m_Address2
            End Get
            Set(value As String)
                m_Address2 = Value
            End Set
        End Property
        Private m_Address2 As String

        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId() As System.Nullable(Of Integer)
            Get
                Return m_MemberId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As System.Nullable(Of Integer)

        <DataMember(Name:="first_name", EmitDefaultValue:=False)> _
        Public Property FirstName() As String
            Get
                Return m_FirstName
            End Get
            Set(value As String)
                m_FirstName = Value
            End Set
        End Property
        Private m_FirstName As String

        <DataMember(Name:="last_name", EmitDefaultValue:=False)> _
        Public Property LastName() As String
            Get
                Return m_LastName
            End Get
            Set(value As String)
                m_LastName = Value
            End Set
        End Property
        Private m_LastName As String

        <DataMember(Name:="address_email", EmitDefaultValue:=False)> _
        Public Property AddressEmail() As String
            Get
                Return m_AddressEmail
            End Get
            Set(value As String)
                m_AddressEmail = Value
            End Set
        End Property
        Private m_AddressEmail As String

        <DataMember(Name:="address_phone_number", EmitDefaultValue:=False)> _
        Public Property AddressPhoneNumber() As String
            Get
                Return m_AddressPhoneNumber
            End Get
            Set(value As String)
                m_AddressPhoneNumber = Value
            End Set
        End Property
        Private m_AddressPhoneNumber As String

        <DataMember(Name:="address_city", EmitDefaultValue:=False)> _
        Public Property AddressCity() As String
            Get
                Return m_AddressCity
            End Get
            Set(value As String)
                m_AddressCity = Value
            End Set
        End Property
        Private m_AddressCity As String

        <DataMember(Name:="address_state_id", EmitDefaultValue:=False)> _
        Public Property AddressStateId() As String
            Get
                Return m_AddressStateId
            End Get
            Set(value As String)
                m_AddressStateId = Value
            End Set
        End Property
        Private m_AddressStateId As String

        <DataMember(Name:="address_country_id", EmitDefaultValue:=False)> _
        Public Property AddressCountryId() As String
            Get
                Return m_AddressCountryId
            End Get
            Set(value As String)
                m_AddressCountryId = Value
            End Set
        End Property
        Private m_AddressCountryId As String

        <DataMember(Name:="address_zip", EmitDefaultValue:=False)> _
        Public Property AddressZip() As String
            Get
                Return m_AddressZip
            End Get
            Set(value As String)
                m_AddressZip = Value
            End Set
        End Property
        Private m_AddressZip As String

        <DataMember(Name:="cached_lat")> _
        Public Property CachedLat() As Double
            Get
                Return m_CachedLat
            End Get
            Set(value As Double)
                m_CachedLat = Value
            End Set
        End Property
        Private m_CachedLat As Double

        <DataMember(Name:="cached_lng")> _
        Public Property CachedLng() As Double
            Get
                Return m_CachedLng
            End Get
            Set(value As Double)
                m_CachedLng = Value
            End Set
        End Property
        Private m_CachedLng As Double

        <DataMember(Name:="curbside_lat")> _
        Public Property CurbsideLat() As Double
            Get
                Return m_CurbsideLat
            End Get
            Set(value As Double)
                m_CurbsideLat = value
            End Set
        End Property
        Private m_CurbsideLat As Double

        <DataMember(Name:="curbside_lng")> _
        Public Property CurbsideLng() As Double
            Get
                Return m_CurbsideLng
            End Get
            Set(value As Double)
                m_CurbsideLng = value
            End Set
        End Property
        Private m_CurbsideLng As Double

        <DataMember(Name:="address_custom_data", EmitDefaultValue:=False)> _
        Public Property AddressCustomData() As Dictionary(Of String, String)()
            Get
                Return m_AddressCustomData
            End Get
            Set(value As Dictionary(Of String, String)())
                m_AddressCustomData = value
            End Set
        End Property
        Private m_AddressCustomData As Dictionary(Of String, String)()

        <DataMember(Name:="in_route_count", EmitDefaultValue:=False)> _
        Public Property InRouteCount() As System.Nullable(Of Integer)
            Get
                Return m_InRouteCount
            End Get
            Set(value As System.Nullable(Of Integer))
                m_InRouteCount = value
            End Set
        End Property
        Private m_InRouteCount As System.Nullable(Of Integer)

        <DataMember(Name:="last_visited_timestamp", EmitDefaultValue:=False)> _
        Public Property LastVisitedTimestamp() As System.Nullable(Of Integer)
            Get
                Return m_LastVisitedTimestamp
            End Get
            Set(value As System.Nullable(Of Integer))
                m_LastVisitedTimestamp = value
            End Set
        End Property
        Private m_LastVisitedTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="last_routed_timestamp", EmitDefaultValue:=False)> _
        Public Property LastRoutedTimestamp() As System.Nullable(Of Integer)
            Get
                Return m_LastRoutedTimestamp
            End Get
            Set(value As System.Nullable(Of Integer))
                m_LastRoutedTimestamp = value
            End Set
        End Property
        Private m_LastRoutedTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="local_time_window_start", EmitDefaultValue:=False)> _
        Public Property LocalTimeWindowStart() As System.Nullable(Of Integer)
            Get
                Return m_LocalTimeWindowStart
            End Get
            Set(value As System.Nullable(Of Integer))
                m_LocalTimeWindowStart = value
            End Set
        End Property
        Private m_LocalTimeWindowStart As System.Nullable(Of Integer)

        <DataMember(Name:="local_time_window_end", EmitDefaultValue:=False)> _
        Public Property LocalTimeWindowEnd() As System.Nullable(Of Integer)
            Get
                Return m_LocalTimeWindowEnd
            End Get
            Set(value As System.Nullable(Of Integer))
                m_LocalTimeWindowEnd = value
            End Set
        End Property
        Private m_LocalTimeWindowEnd As System.Nullable(Of Integer)

        <DataMember(Name:="local_time_window_start_2", EmitDefaultValue:=False)> _
        Public Property LocalTimeWindowStart2() As System.Nullable(Of Integer)
            Get
                Return m_LocalTimeWindowStart2
            End Get
            Set(value As System.Nullable(Of Integer))
                m_LocalTimeWindowStart2 = value
            End Set
        End Property
        Private m_LocalTimeWindowStart2 As System.Nullable(Of Integer)

        <DataMember(Name:="local_time_window_end_2", EmitDefaultValue:=False)> _
        Public Property LocalTimeWindowEnd2() As System.Nullable(Of Integer)
            Get
                Return m_LocalTimeWindowEnd2
            End Get
            Set(value As System.Nullable(Of Integer))
                m_LocalTimeWindowEnd2 = value
            End Set
        End Property
        Private m_LocalTimeWindowEnd2 As System.Nullable(Of Integer)

        <DataMember(Name:="service_time", EmitDefaultValue:=False)> _
        Public Property ServiceTime() As System.Nullable(Of Integer)
            Get
                Return m_ServiceTime
            End Get
            Set(value As System.Nullable(Of Integer))
                m_ServiceTime = value
            End Set
        End Property
        Private m_ServiceTime As System.Nullable(Of Integer)

        <DataMember(Name:="local_timezone_string", EmitDefaultValue:=False)> _
        Public Property LocalTimezoneString() As String
            Get
                Return m_LocalTimezoneString
            End Get
            Set(value As String)
                m_LocalTimezoneString = value
            End Set
        End Property
        Private m_LocalTimezoneString As String

        <DataMember(Name:="color", EmitDefaultValue:=False)> _
        Public Property Color() As String
            Get
                Return m_Color
            End Get
            Set(value As String)
                m_Color = value
            End Set
        End Property
        Private m_Color As String

        <DataMember(Name:="address_icon", EmitDefaultValue:=False)> _
        Public Property AddressIcon() As String
            Get
                Return m_AddressIcon
            End Get
            Set(value As String)
                m_AddressIcon = value
            End Set
        End Property
        Private m_AddressIcon As String

    End Class
End Namespace
