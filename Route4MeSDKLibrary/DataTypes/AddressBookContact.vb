Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class AddressBookContact
        Inherits GenericParameters
        <DataMember(Name:="address_id", EmitDefaultValue:=False)> _
        Public Property AddressId() As String
            Get
                Return m_AddressId
            End Get
            Set(value As String)
                m_AddressId = Value
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

        <DataMember(Name:="color", EmitDefaultValue:=False)> _
        Public Property Color() As String
            Get
                Return m_Color
            End Get
            Set(value As String)
                m_Color = Value
            End Set
        End Property
        Private m_Color As String
    End Class
End Namespace
