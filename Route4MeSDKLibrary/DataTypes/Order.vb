Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class Order
        Inherits GenericParameters

        ''' <summary>
        ''' Order ID.
        ''' </summary>
        <DataMember(Name:="order_id", EmitDefaultValue:=False)> _
        Public Property order_id() As Integer
            Get
                Return m_order_id
            End Get
            Set(value As Integer)
                m_order_id = Value
            End Set
        End Property
        Private m_order_id As Integer

        ''' <summary>
        ''' Address 1 field. Required
        ''' </summary>
        <DataMember(Name:="address_1")> _
        Public Property address_1() As String
            Get
                Return m_address_1
            End Get
            Set(value As String)
                m_address_1 = Value
            End Set
        End Property
        Private m_address_1 As String

        ''' <summary>
        ''' Address 2 field
        ''' </summary>
        <DataMember(Name:="address_2", EmitDefaultValue:=False)> _
        Public Property address_2() As String
            Get
                Return m_address_2
            End Get
            Set(value As String)
                m_address_2 = Value
            End Set
        End Property
        Private m_address_2 As String

        ''' <summary>
        ''' Geo latitude. Required
        ''' </summary>
        <DataMember(Name:="cached_lat")> _
        Public Property cached_lat() As Double
            Get
                Return m_cached_lat
            End Get
            Set(value As Double)
                m_cached_lat = Value
            End Set
        End Property
        Private m_cached_lat As Double

        ''' <summary>
        ''' Geo longitude. Required
        ''' </summary>
        <DataMember(Name:="cached_lng")> _
        Public Property cached_lng() As Double
            Get
                Return m_cached_lng
            End Get
            Set(value As Double)
                m_cached_lng = Value
            End Set
        End Property
        Private m_cached_lng As Double

        ''' <summary>
        ''' Generate optimal routes and driving directions to this curbside latitude
        ''' </summary>
        <DataMember(Name:="curbside_lat", EmitDefaultValue:=False)> _
        Public Property curbside_lat() As System.Nullable(Of Double)
            Get
                Return m_curbside_lat
            End Get
            Set(value As System.Nullable(Of Double))
                m_curbside_lat = Value
            End Set
        End Property
        Private m_curbside_lat As System.Nullable(Of Double)

        ''' <summary>
        ''' Generate optimal routes and driving directions to the curbside langitude
        ''' </summary>
        <DataMember(Name:="curbside_lng", EmitDefaultValue:=False)> _
        Public Property curbside_lng() As System.Nullable(Of Double)
            Get
                Return m_curbside_lng
            End Get
            Set(value As System.Nullable(Of Double))
                m_curbside_lng = Value
            End Set
        End Property
        Private m_curbside_lng As System.Nullable(Of Double)

        ''' <summary>
        ''' Scheduled day
        ''' </summary>
        <DataMember(Name:="day_scheduled_for_YYMMDD", EmitDefaultValue:=False)> _
        Public Property day_scheduled_for_YYMMDD() As String
            Get
                Return m_day_scheduled_for_YYMMDD
            End Get
            Set(value As String)
                m_day_scheduled_for_YYMMDD = Value
            End Set
        End Property
        Private m_day_scheduled_for_YYMMDD As String

        ''' <summary>
        ''' Address Alias. Required
        ''' </summary>
        <DataMember(Name:="address_alias")> _
        Public Property address_alias() As String
            Get
                Return m_address_alias
            End Get
            Set(value As String)
                m_address_alias = Value
            End Set
        End Property
        Private m_address_alias As String

        ''' <summary>
        ''' Local time window start
        ''' </summary>
        <DataMember(Name:="local_time_window_start", EmitDefaultValue:=False)> _
        Public Property local_time_window_start() As System.Nullable(Of Integer)
            Get
                Return m_local_time_window_start
            End Get
            Set(value As System.Nullable(Of Integer))
                m_local_time_window_start = Value
            End Set
        End Property
        Private m_local_time_window_start As System.Nullable(Of Integer)

        ''' <summary>
        ''' Local time window end
        ''' </summary>
        <DataMember(Name:="local_time_window_end", EmitDefaultValue:=False)> _
        Public Property local_time_window_end() As System.Nullable(Of Integer)
            Get
                Return m_local_time_window_end
            End Get
            Set(value As System.Nullable(Of Integer))
                m_local_time_window_end = Value
            End Set
        End Property
        Private m_local_time_window_end As System.Nullable(Of Integer)

        ''' <summary>
        ''' Second Local time window start
        ''' </summary>
        <DataMember(Name:="local_time_window_start_2", EmitDefaultValue:=False)> _
        Public Property local_time_window_start_2() As System.Nullable(Of Integer)
            Get
                Return m_local_time_window_start_2
            End Get
            Set(value As System.Nullable(Of Integer))
                m_local_time_window_start_2 = Value
            End Set
        End Property
        Private m_local_time_window_start_2 As System.Nullable(Of Integer)

        ''' <summary>
        ''' Second local time window end
        ''' </summary>
        <DataMember(Name:="local_time_window_end_2", EmitDefaultValue:=False)> _
        Public Property local_time_window_end_2() As System.Nullable(Of Integer)
            Get
                Return m_local_time_window_end_2
            End Get
            Set(value As System.Nullable(Of Integer))
                m_local_time_window_end_2 = Value
            End Set
        End Property
        Private m_local_time_window_end_2 As System.Nullable(Of Integer)

        ''' <summary>
        ''' Second time
        ''' </summary>
        <DataMember(Name:="service_time", EmitDefaultValue:=False)> _
        Public Property service_time() As System.Nullable(Of Integer)
            Get
                Return m_service_time
            End Get
            Set(value As System.Nullable(Of Integer))
                m_service_time = Value
            End Set
        End Property
        Private m_service_time As System.Nullable(Of Integer)

        ''' <summary>
        ''' Address City
        ''' </summary>
        <DataMember(Name:="address_city", EmitDefaultValue:=False)> _
        Public Property address_city() As String
            Get
                Return m_address_city
            End Get
            Set(value As String)
                m_address_city = Value
            End Set
        End Property
        Private m_address_city As String

        ''' <summary>
        ''' Address state ID
        ''' </summary>
        <DataMember(Name:="address_state_id", EmitDefaultValue:=False)> _
        Public Property address_state_id() As String
            Get
                Return m_address_state_id
            End Get
            Set(value As String)
                m_address_state_id = Value
            End Set
        End Property
        Private m_address_state_id As String

        ''' <summary>
        ''' Address country ID
        ''' </summary>
        <DataMember(Name:="address_country_id", EmitDefaultValue:=False)> _
        Public Property address_country_id() As String
            Get
                Return m_address_country_id
            End Get
            Set(value As String)
                m_address_country_id = Value
            End Set
        End Property
        Private m_address_country_id As String

        ''' <summary>
        ''' Address ZIP
        ''' </summary>
        <DataMember(Name:="address_zip", EmitDefaultValue:=False)> _
        Public Property address_zip() As String
            Get
                Return m_address_zip
            End Get
            Set(value As String)
                m_address_zip = Value
            End Set
        End Property
        Private m_address_zip As String

        ''' <summary>
        ''' Order status ID
        ''' </summary>
        <DataMember(Name:="order_status_id", EmitDefaultValue:=False)> _
        Public Property order_status_id() As Integer
            Get
                Return m_order_status_id
            End Get
            Set(value As Integer)
                m_order_status_id = Value
            End Set
        End Property
        Private m_order_status_id As Integer

        ''' <summary>
        ''' The id of the member inside the route4me system
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property member_id() As Integer
            Get
                Return m_member_id
            End Get
            Set(value As Integer)
                m_member_id = Value
            End Set
        End Property
        Private m_member_id As Integer

        ''' <summary>
        ''' First name
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_first_name", EmitDefaultValue:=False)> _
        Public Property EXT_FIELD_first_name() As String
            Get
                Return m_EXT_FIELD_first_name
            End Get
            Set(value As String)
                m_EXT_FIELD_first_name = Value
            End Set
        End Property
        Private m_EXT_FIELD_first_name As String

        ''' <summary>
        ''' Last name
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_last_name", EmitDefaultValue:=False)> _
        Public Property EXT_FIELD_last_name() As String
            Get
                Return m_EXT_FIELD_last_name
            End Get
            Set(value As String)
                m_EXT_FIELD_last_name = Value
            End Set
        End Property
        Private m_EXT_FIELD_last_name As String

        ''' <summary>
        ''' Email
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_email", EmitDefaultValue:=False)> _
        Public Property EXT_FIELD_email() As String
            Get
                Return m_EXT_FIELD_email
            End Get
            Set(value As String)
                m_EXT_FIELD_email = Value
            End Set
        End Property
        Private m_EXT_FIELD_email As String

        ''' <summary>
        ''' Phone number
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_phone", EmitDefaultValue:=False)> _
        Public Property EXT_FIELD_phone() As String
            Get
                Return m_EXT_FIELD_phone
            End Get
            Set(value As String)
                m_EXT_FIELD_phone = Value
            End Set
        End Property
        Private m_EXT_FIELD_phone As String

        ''' <summary>
        ''' Custom data
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_custom_data", EmitDefaultValue:=False)> _
        Public Property EXT_FIELD_custom_data() As Object
            Get
                Return _ext_field_custom_data
            End Get
            Set(value As Object)
                If value.[GetType]().ToString() = "System.Collections.Generic.Dictionary" Then
                    _ext_field_custom_data = value
                Else
                    _ext_field_custom_data = Nothing
                End If
            End Set
        End Property
        Private _ext_field_custom_data As Object

        ''' <summary>
        ''' Local timezone string
        ''' </summary>
        <DataMember(Name:="local_timezone_string", EmitDefaultValue:=False)> _
        Public Property local_timezone_string() As String
            Get
                Return m_local_timezone_string
            End Get
            Set(value As String)
                m_local_timezone_string = Value
            End Set
        End Property
        Private m_local_timezone_string As String

        ''' <summary>
        ''' Order icon
        ''' </summary>
        <DataMember(Name:="order_icon", EmitDefaultValue:=False)> _
        Public Property order_icon() As String
            Get
                Return m_order_icon
            End Get
            Set(value As String)
                m_order_icon = Value
            End Set
        End Property
        Private m_order_icon As String

    End Class
End Namespace