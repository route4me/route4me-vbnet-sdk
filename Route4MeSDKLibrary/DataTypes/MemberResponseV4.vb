Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class MemberResponseV4

        <DataMember(Name:="HIDE_NONFUTURE_ROUTES", EmitDefaultValue:=False)> _
        Public Property HIDE_NONFUTURE_ROUTES() As String
            Get
                Return m_HIDE_NONFUTURE_ROUTES
            End Get
            Set(value As String)
                m_HIDE_NONFUTURE_ROUTES = value
            End Set
        End Property
        Private m_HIDE_NONFUTURE_ROUTES As String

        <DataMember(Name:="HIDE_ROUTED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_ROUTED_ADDRESSES() As String
            Get
                Return m_HIDE_ROUTED_ADDRESSES
            End Get
            Set(value As String)
                m_HIDE_ROUTED_ADDRESSES = value
            End Set
        End Property
        Private m_HIDE_ROUTED_ADDRESSES As String

        <DataMember(Name:="HIDE_VISITED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_VISITED_ADDRESSES() As String
            Get
                Return m_HIDE_VISITED_ADDRESSES
            End Get
            Set(value As String)
                m_HIDE_VISITED_ADDRESSES = value
            End Set
        End Property
        Private m_HIDE_VISITED_ADDRESSES As String

        <DataMember(Name:="member_id")> _
        Public Property member_id() As String
            Get
                Return m_member_id
            End Get
            Set(value As String)
                m_member_id = value
            End Set
        End Property
        Private m_member_id As String

        <DataMember(Name:="OWNER_MEMBER_ID")> _
        Public Property OWNER_MEMBER_ID() As String
            Get
                Return m_OWNER_MEMBER_ID
            End Get
            Set(value As String)
                m_OWNER_MEMBER_ID = value
            End Set
        End Property
        Private m_OWNER_MEMBER_ID As String

        <DataMember(Name:="READONLY_USER")> _
        Public Property READONLY_USER() As String
            Get
                Return m_READONLY_USER
            End Get
            Set(value As String)
                m_READONLY_USER = value
            End Set
        End Property
        Private m_READONLY_USER As String

        <DataMember(Name:="SHOW_ALL_DRIVERS")> _
        Public Property SHOW_ALL_DRIVERS() As String
            Get
                Return m_SHOW_ALL_DRIVERS
            End Get
            Set(value As String)
                m_SHOW_ALL_DRIVERS = value
            End Set
        End Property
        Private m_SHOW_ALL_DRIVERS As String

        <DataMember(Name:="SHOW_ALL_VEHICLES")> _
        Public Property SHOW_ALL_VEHICLES() As String
            Get
                Return m_SHOW_ALL_VEHICLES
            End Get
            Set(value As String)
                m_SHOW_ALL_VEHICLES = value
            End Set
        End Property
        Private m_SHOW_ALL_VEHICLES As String

        <DataMember(Name:="date_of_birth")> _
        Public Property date_of_birth() As String
            Get
                Return m_date_of_birth
            End Get
            Set(value As String)
                m_date_of_birth = value
            End Set
        End Property
        Private m_date_of_birth As String

        <DataMember(Name:="member_email")> _
        Public Property member_email() As String
            Get
                Return m_member_email
            End Get
            Set(value As String)
                m_member_email = value
            End Set
        End Property
        Private m_member_email As String

        <DataMember(Name:="member_first_name")> _
        Public Property member_first_name() As String
            Get
                Return m_member_first_name
            End Get
            Set(value As String)
                m_member_first_name = value
            End Set
        End Property
        Private m_member_first_name As String

        <DataMember(Name:="member_last_name")> _
        Public Property member_last_name() As String
            Get
                Return m_member_last_name
            End Get
            Set(value As String)
                m_member_last_name = value
            End Set
        End Property
        Private m_member_last_name As String

        <DataMember(Name:="member_phone")> _
        Public Property member_phone() As String
            Get
                Return m_member_phone
            End Get
            Set(value As String)
                m_member_phone = value
            End Set
        End Property
        Private m_member_phone As String

        <DataMember(Name:="member_picture")> _
        Public Property member_picture() As String
            Get
                Return m_member_picture
            End Get
            Set(value As String)
                m_member_picture = value
            End Set
        End Property
        Private m_member_picture As String

        <DataMember(Name:="member_type")> _
        Public Property member_type() As String
            Get
                Return m_member_type
            End Get
            Set(value As String)
                m_member_type = value
            End Set
        End Property
        Private m_member_type As String

        <DataMember(Name:="member_zipcode")> _
        Public Property member_zipcode() As String
            Get
                Return m_member_zipcode
            End Get
            Set(value As String)
                m_member_zipcode = value
            End Set
        End Property
        Private m_member_zipcode As String

        <DataMember(Name:="preferred_language")> _
        Public Property preferred_language() As String
            Get
                Return m_preferred_language
            End Get
            Set(value As String)
                m_preferred_language = value
            End Set
        End Property
        Private m_preferred_language As String

        <DataMember(Name:="preferred_units")> _
        Public Property preferred_units() As String
            Get
                Return m_preferred_units
            End Get
            Set(value As String)
                m_preferred_units = value
            End Set
        End Property
        Private m_preferred_units As String

        <DataMember(Name:="timezone")> _
        Public Property timezone() As String
            Get
                Return m_timezone
            End Get
            Set(value As String)
                m_timezone = value
            End Set
        End Property
        Private m_timezone As String

        <DataMember(Name:="user_reg_country_id")> _
        Public Property user_reg_country_id() As String
            Get
                Return m_user_reg_country_id
            End Get
            Set(value As String)
                m_user_reg_country_id = value
            End Set
        End Property
        Private m_user_reg_country_id As String

        <DataMember(Name:="user_reg_state_id")> _
        Public Property user_reg_state_id() As String
            Get
                Return m_user_reg_state_id
            End Get
            Set(value As String)
                m_user_reg_state_id = value
            End Set
        End Property
        Private m_user_reg_state_id As String

    End Class
End Namespace