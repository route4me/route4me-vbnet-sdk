Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class AddressBookParameters
        Inherits GenericParameters
        <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)> _
        Public Property AddressId() As String
            Get
                Return m_AddressId
            End Get
            Set(value As String)
                m_AddressId = Value
            End Set
        End Property
        Private m_AddressId As String

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
        Public Property Limit() As System.Nullable(Of UInteger)
            Get
                Return m_Limit
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Limit = Value
            End Set
        End Property
        Private m_Limit As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
        Public Property Offset() As System.Nullable(Of UInteger)
            Get
                Return m_Offset
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Offset = Value
            End Set
        End Property
        Private m_Offset As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="start", EmitDefaultValue:=False)> _
        Public Property Start() As System.Nullable(Of UInteger)
            Get
                Return m_Start
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Start = Value
            End Set
        End Property
        Private m_Start As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)> _
        Public Property Query() As String
            Get
                Return m_Query
            End Get
            Set(value As String)
                m_Query = value
            End Set
        End Property
        Private m_Query As String

        <HttpQueryMemberAttribute(Name:="fields", EmitDefaultValue:=False)> _
        Public Property Fields() As String
            Get
                Return m_Fields
            End Get
            Set(value As String)
                m_Fields = Value
            End Set
        End Property
        Private m_Fields As String

        <HttpQueryMemberAttribute(Name:="display", EmitDefaultValue:=False)> _
        Public Property Display() As String
            Get
                Return m_Display
            End Get
            Set(value As String)
                m_Display = Value
            End Set
        End Property
        Private m_Display As String
    End Class
End Namespace
