Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class AddressBookContact
        Inherits GenericParameters
        <DataMember(Name:="territory_name", EmitDefaultValue:=False)> _
        Public Property territory_name() As String
            Get
                Return m_territory_name
            End Get
            Set(value As String)
                m_territory_name = Value
            End Set
        End Property
        Private m_territory_name As String

        <DataMember(Name:="created_timestamp", EmitDefaultValue:=False)> _
        Public Property created_timestamp() As Integer
            Get
                Return m_created_timestamp
            End Get
            Set(value As Integer)
                m_created_timestamp = Value
            End Set
        End Property
        Private m_created_timestamp As Integer

        <DataMember(Name:="address_id", EmitDefaultValue:=False)> _
        Public Property address_id() As System.Nullable(Of Integer)
            Get
                Return m_address_id
            End Get
            Set(value As System.Nullable(Of Integer))
                m_address_id = Value
            End Set
        End Property
        Private m_address_id As System.Nullable(Of Integer)

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

        <DataMember(Name:="address_alias", EmitDefaultValue:=False)> _
        Public Property address_alias() As String
            Get
                Return m_address_alias
            End Get
            Set(value As String)
                m_address_alias = Value
            End Set
        End Property
        Private m_address_alias As String

        <DataMember(Name:="address_group", EmitDefaultValue:=False)> _
        Public Property address_group() As String
            Get
                Return m_address_group
            End Get
            Set(value As String)
                m_address_group = Value
            End Set
        End Property
        Private m_address_group As String

        <DataMember(Name:="first_name", EmitDefaultValue:=False)> _
        Public Property first_name() As String
            Get
                Return m_first_name
            End Get
            Set(value As String)
                m_first_name = Value
            End Set
        End Property
        Private m_first_name As String

        <DataMember(Name:="last_name", EmitDefaultValue:=False)> _
        Public Property last_name() As String
            Get
                Return m_last_name
            End Get
            Set(value As String)
                m_last_name = Value
            End Set
        End Property
        Private m_last_name As String

        <DataMember(Name:="address_email", EmitDefaultValue:=False)> _
        Public Property address_email() As String
            Get
                Return m_address_email
            End Get
            Set(value As String)
                m_address_email = Value
            End Set
        End Property
        Private m_address_email As String

        <DataMember(Name:="address_phone_number", EmitDefaultValue:=False)> _
        Public Property address_phone_number() As String
            Get
                Return m_address_phone_number
            End Get
            Set(value As String)
                m_address_phone_number = Value
            End Set
        End Property
        Private m_address_phone_number As String

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

        <DataMember(Name:="curbside_lat")> _
        Public Property curbside_lat() As System.Nullable(Of Double)
            Get
                Return m_curbside_lat
            End Get
            Set(value As System.Nullable(Of Double))
                m_curbside_lat = Value
            End Set
        End Property
        Private m_curbside_lat As System.Nullable(Of Double)

        <DataMember(Name:="curbside_lng")> _
        Public Property curbside_lng() As System.Nullable(Of Double)
            Get
                Return m_curbside_lng
            End Get
            Set(value As System.Nullable(Of Double))
                m_curbside_lng = Value
            End Set
        End Property
        Private m_curbside_lng As System.Nullable(Of Double)

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

        <DataMember(Name:="address_custom_data", EmitDefaultValue:=False)> _
        Public Property address_custom_data() As Object
            Get
                Return _address_custom_data
            End Get
            Set(value As Object)
                If value.[GetType]().ToString() = "System.Collections.Generic.Dictionary" Then
                    _address_custom_data = value
                Else
                    _address_custom_data = Nothing
                End If
            End Set
        End Property
        Private _address_custom_data As Object

        <DataMember(Name:="schedule", EmitDefaultValue:=False)> _
        Public Property schedule() As IList(Of Schedule)
            Get
                Return m_schedule
            End Get
            Set(value As IList(Of Schedule))
                m_schedule = Value
            End Set
        End Property
        Private m_schedule As IList(Of Schedule)

        <DataMember(Name:="schedule_blacklist", EmitDefaultValue:=False)> _
        Public Property schedule_blacklist() As String()
            Get
                Return m_schedule_blacklist
            End Get
            Set(value As String())
                m_schedule_blacklist = Value
            End Set
        End Property
        Private m_schedule_blacklist As String()

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

        <DataMember(Name:="color", EmitDefaultValue:=False)> _
        Public Property color() As String
            Get
                Return m_color
            End Get
            Set(value As String)
                m_color = Value
            End Set
        End Property
        Private m_color As String

        <DataMember(Name:="address_icon", EmitDefaultValue:=False)> _
        Public Property address_icon() As String
            Get
                Return m_address_icon
            End Get
            Set(value As String)
                m_address_icon = Value
            End Set
        End Property
        Private m_address_icon As String
    End Class
End Namespace