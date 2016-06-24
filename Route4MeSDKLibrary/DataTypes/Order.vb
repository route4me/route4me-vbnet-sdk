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
        Public Property OrderId() As String
            Get
                Return m_OrderId
            End Get
            Set(value As String)
                m_OrderId = value
            End Set
        End Property
        Private m_OrderId As String

        ''' <summary>
        ''' Address 1 field. Required
        ''' </summary>
        <DataMember(Name:="address_1")> _
        Public Property Address1() As String
            Get
                Return m_Address1
            End Get
            Set(value As String)
                m_Address1 = value
            End Set
        End Property
        Private m_Address1 As String

        ''' <summary>
        ''' Address 2 field
        ''' </summary>
        <DataMember(Name:="address_2", EmitDefaultValue:=False)> _
        Public Property Address2() As String
            Get
                Return m_Address2
            End Get
            Set(value As String)
                m_Address2 = value
            End Set
        End Property
        Private m_Address2 As String

        ''' <summary>
        ''' Address Alias. Required
        ''' </summary>
        <DataMember(Name:="address_alias")> _
        Public Property AddressAlias() As String
            Get
                Return m_AddressAlias
            End Get
            Set(value As String)
                m_AddressAlias = value
            End Set
        End Property
        Private m_AddressAlias As String

        ''' <summary>
        ''' Geo latitude. Required
        ''' </summary>
        <DataMember(Name:="cached_lat")> _
        Public Property CachedLatitude() As Double
            Get
                Return m_CachedLatitude
            End Get
            Set(value As Double)
                m_CachedLatitude = value
            End Set
        End Property
        Private m_CachedLatitude As Double

        ''' <summary>
        ''' Geo longitude. Required
        ''' </summary>
        <DataMember(Name:="cached_lng")> _
        Public Property CachedLongitude() As Double
            Get
                Return m_CachedLongitude
            End Get
            Set(value As Double)
                m_CachedLongitude = value
            End Set
        End Property
        Private m_CachedLongitude As Double

        ''' <summary>
        ''' Generate optimal routes and driving directions to this curbside latitude
        ''' </summary>
        <DataMember(Name:="curbside_lat", EmitDefaultValue:=False)> _
        Public Property CurbsideLatitude() As System.Nullable(Of Double)
            Get
                Return m_CurbsideLatitude
            End Get
            Set(value As System.Nullable(Of Double))
                m_CurbsideLatitude = value
            End Set
        End Property
        Private m_CurbsideLatitude As System.Nullable(Of Double)

        ''' <summary>
        ''' Generate optimal routes and driving directions to the curbside langitude
        ''' </summary>
        <DataMember(Name:="curbside_lng", EmitDefaultValue:=False)> _
        Public Property CurbsideLongitude() As System.Nullable(Of Double)
            Get
                Return m_CurbsideLongitude
            End Get
            Set(value As System.Nullable(Of Double))
                m_CurbsideLongitude = value
            End Set
        End Property
        Private m_CurbsideLongitude As System.Nullable(Of Double)

        ''' <summary>
        ''' Address City
        ''' </summary>
        <DataMember(Name:="address_city", EmitDefaultValue:=False)> _
        Public Property AddressCity() As String
            Get
                Return m_AddressCity
            End Get
            Set(value As String)
                m_AddressCity = value
            End Set
        End Property
        Private m_AddressCity As String

        ''' <summary>
        ''' Address state ID
        ''' </summary>
        <DataMember(Name:="address_state_id", EmitDefaultValue:=False)> _
        Public Property AddressStateId() As String
            Get
                Return m_AddressStateId
            End Get
            Set(value As String)
                m_AddressStateId = value
            End Set
        End Property
        Private m_AddressStateId As String

        ''' <summary>
        ''' Address country ID
        ''' </summary>
        <DataMember(Name:="address_country_id", EmitDefaultValue:=False)> _
        Public Property AddressCountryId() As String
            Get
                Return m_AddressCountryId
            End Get
            Set(value As String)
                m_AddressCountryId = value
            End Set
        End Property
        Private m_AddressCountryId As String

        ''' <summary>
        ''' Address ZIP
        ''' </summary>
        <DataMember(Name:="address_zip", EmitDefaultValue:=False)> _
        Public Property AddressZIP() As String
            Get
                Return m_AddressZIP
            End Get
            Set(value As String)
                m_AddressZIP = value
            End Set
        End Property
        Private m_AddressZIP As String

        ''' <summary>
        ''' Order status ID
        ''' </summary>
        <DataMember(Name:="order_status_id", EmitDefaultValue:=False)> _
        Public Property OrderStatusId() As String
            Get
                Return m_OrderStatusId
            End Get
            Set(value As String)
                m_OrderStatusId = value
            End Set
        End Property
        Private m_OrderStatusId As String

        ''' <summary>
        ''' The id of the member inside the route4me system
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId() As String
            Get
                Return m_MemberId
            End Get
            Set(value As String)
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As String

        ''' <summary>
        ''' First name
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_first_name", EmitDefaultValue:=False)> _
        Public Property FirstName() As String
            Get
                Return m_FirstName
            End Get
            Set(value As String)
                m_FirstName = value
            End Set
        End Property
        Private m_FirstName As String

        ''' <summary>
        ''' Last name
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_last_name", EmitDefaultValue:=False)> _
        Public Property LastName() As String
            Get
                Return m_LastName
            End Get
            Set(value As String)
                m_LastName = value
            End Set
        End Property
        Private m_LastName As String

        ''' <summary>
        ''' Email
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_email", EmitDefaultValue:=False)> _
        Public Property Email() As String
            Get
                Return m_Email
            End Get
            Set(value As String)
                m_Email = value
            End Set
        End Property
        Private m_Email As String

        ''' <summary>
        ''' Phone number
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_phone", EmitDefaultValue:=False)> _
        Public Property Phone() As String
            Get
                Return m_Phone
            End Get
            Set(value As String)
                m_Phone = value
            End Set
        End Property
        Private m_Phone As String

        ''' <summary>
        ''' Custom data
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_custom_data", EmitDefaultValue:=False)> _
        Public Property CustomData() As List(Of Dictionary(Of String, String))
            Get
                Return m_CustomData
            End Get
            Set(value As List(Of Dictionary(Of String, String)))
                m_CustomData = value
            End Set
        End Property
        Private m_CustomData As List(Of Dictionary(Of String, String))

    End Class
End Namespace
