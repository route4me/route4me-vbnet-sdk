Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class GeocodingParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="addresses", EmitDefaultValue:=False)> _
        Public Property Addresses() As String
            Get
                Return m_Addresses
            End Get
            Set(value As String)
                m_Addresses = value
            End Set
        End Property
        Private m_Addresses As String

        <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)> _
        Public Property Format() As String
            Get
                Return m_Format
            End Get
            Set(value As String)
                m_Format = value
            End Set
        End Property
        Private m_Format As String

        <HttpQueryMemberAttribute(Name:="pk", EmitDefaultValue:=False)> _
        Public Property Pk() As Integer
            Get
                Return m_pk
            End Get
            Set(value As Integer)
                m_pk = value
            End Set
        End Property
        Private m_pk As Integer

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
        Public Property Offset() As Integer
            Get
                Return m_Offset
            End Get
            Set(value As Integer)
                m_Offset = value
            End Set
        End Property
        Private m_Offset As Integer

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
        Public Property Limit() As Integer
            Get
                Return m_Limit
            End Get
            Set(value As Integer)
                m_Limit = value
            End Set
        End Property
        Private m_Limit As Integer

        <HttpQueryMemberAttribute(Name:="zipcode", EmitDefaultValue:=False)> _
        Public Property Zipcode() As String
            Get
                Return m_Zipcode
            End Get
            Set(value As String)
                m_Zipcode = value
            End Set
        End Property
        Private m_Zipcode As String

        <HttpQueryMemberAttribute(Name:="housenumber", EmitDefaultValue:=False)> _
        Public Property Housenumber() As String
            Get
                Return m_Housenumber
            End Get
            Set(value As String)
                m_Housenumber = value
            End Set
        End Property
        Private m_Housenumber As String

    End Class
End Namespace