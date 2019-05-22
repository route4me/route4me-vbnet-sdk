Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization
Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberParametersV4
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property member_id() As System.Nullable(Of Integer)
            Get
                Return m_member_id
            End Get
            Set(value As System.Nullable(Of Integer))
                m_member_id = value
            End Set
        End Property
        Private m_member_id As System.Nullable(Of Integer)

        <DataMember(Name:="HIDE_ROUTED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_ROUTED_ADDRESSES() As Boolean
            Get
                Return m_HIDE_ROUTED_ADDRESSES
            End Get
            Set(value As Boolean)
                m_HIDE_ROUTED_ADDRESSES = value
            End Set
        End Property
        Private m_HIDE_ROUTED_ADDRESSES As Boolean

        <DataMember(Name:="HIDE_VISITED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_VISITED_ADDRESSES() As Boolean
            Get
                Return m_HIDE_VISITED_ADDRESSES
            End Get
            Set(value As Boolean)
                m_HIDE_VISITED_ADDRESSES = value
            End Set
        End Property
        Private m_HIDE_VISITED_ADDRESSES As Boolean

        <DataMember(Name:="READONLY_USER", EmitDefaultValue:=False)> _
        Public Property READONLY_USER() As Boolean
            Get
                Return m_READONLY_USER
            End Get
            Set(value As Boolean)
                m_READONLY_USER = value
            End Set
        End Property
        Private m_READONLY_USER As Boolean

        <DataMember(Name:="HIDE_NONFUTURE_ROUTES", EmitDefaultValue:=False)> _
        Public Property HIDE_NONFUTURE_ROUTES() As Boolean
            Get
                Return m_HIDE_NONFUTURE_ROUTES
            End Get
            Set(value As Boolean)
                m_HIDE_NONFUTURE_ROUTES = value
            End Set
        End Property
        Private m_HIDE_NONFUTURE_ROUTES As Boolean

        <DataMember(Name:="SHOW_ALL_VEHICLES", EmitDefaultValue:=False)> _
        Public Property SHOW_ALL_VEHICLES() As Boolean
            Get
                Return m_SHOW_ALL_VEHICLES
            End Get
            Set(value As Boolean)
                m_SHOW_ALL_VEHICLES = value
            End Set
        End Property
        Private m_SHOW_ALL_VEHICLES As Boolean

        <DataMember(Name:="SHOW_ALL_DRIVERS", EmitDefaultValue:=False)> _
        Public Property SHOW_ALL_DRIVERS() As Boolean
            Get
                Return m_SHOW_ALL_DRIVERS
            End Get
            Set(value As Boolean)
                m_SHOW_ALL_DRIVERS = value
            End Set
        End Property
        Private m_SHOW_ALL_DRIVERS As Boolean

        <DataMember(Name:="member_phone", EmitDefaultValue:=False)> _
        Public Property member_phone() As String
            Get
                Return m_member_phone
            End Get
            Set(value As String)
                m_member_phone = value
            End Set
        End Property
        Private m_member_phone As String

        <DataMember(Name:="member_zipcode", EmitDefaultValue:=False)> _
        Public Property member_zipcode() As String
            Get
                Return m_member_zipcode
            End Get
            Set(value As String)
                m_member_zipcode = value
            End Set
        End Property
        Private m_member_zipcode As String

        <HttpQueryMemberAttribute(Name:="route_count", EmitDefaultValue:=False)> _
        Public Property route_count() As System.Nullable(Of Integer)
            Get
                Return m_route_count
            End Get
            Set(value As System.Nullable(Of Integer))
                m_route_count = value
            End Set
        End Property
        Private m_route_count As System.Nullable(Of Integer)

        <DataMember(Name:="member_email", EmitDefaultValue:=False)> _
        Public Property member_email() As String
            Get
                Return m_member_email
            End Get
            Set(value As String)
                m_member_email = value
            End Set
        End Property
        Private m_member_email As String

        <DataMember(Name:="member_type", EmitDefaultValue:=False)> _
        Public Property member_type() As String
            Get
                Return m_member_type
            End Get
            Set(value As String)
                m_member_type = value
            End Set
        End Property
        Private m_member_type As String

        <DataMember(Name:="date_of_birth", EmitDefaultValue:=False)> _
        Public Property date_of_birth() As String
            Get
                Return m_date_of_birth
            End Get
            Set(value As String)
                m_date_of_birth = value
            End Set
        End Property
        Private m_date_of_birth As String

        <DataMember(Name:="member_first_name", EmitDefaultValue:=False)> _
        Public Property member_first_name() As String
            Get
                Return m_member_first_name
            End Get
            Set(value As String)
                m_member_first_name = value
            End Set
        End Property
        Private m_member_first_name As String

        <DataMember(Name:="member_password", EmitDefaultValue:=False)> _
        Public Property member_password() As String
            Get
                Return m_member_password
            End Get
            Set(value As String)
                m_member_password = value
            End Set
        End Property
        Private m_member_password As String

        <DataMember(Name:="member_last_name", EmitDefaultValue:=False)> _
        Public Property member_last_name() As String
            Get
                Return m_member_last_name
            End Get
            Set(value As String)
                m_member_last_name = value
            End Set
        End Property
        Private m_member_last_name As String

        <DataMember(Name:="custom_data", EmitDefaultValue:=False)> _
        Public Property custom_data() As Object
            Get
                Return _custom_data
            End Get
            Set(value As Object)
                If value.[GetType]().ToString() = "System.Collections.Generic.Dictionary" Then
                    _custom_data = value
                Else
                    _custom_data = Nothing
                End If
            End Set
        End Property
        Private _custom_data As Object

    End Class
End Namespace

