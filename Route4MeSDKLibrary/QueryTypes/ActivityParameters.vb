Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class ActivityParameters
        Inherits GenericParameters
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId() As String
            Get
                Return m_RouteId
            End Get
            Set(value As String)
                m_RouteId = value
            End Set
        End Property
        Private m_RouteId As String

        <HttpQueryMemberAttribute(Name:="device_id", EmitDefaultValue:=False)> _
        Public Property DeviceID() As String
            Get
                Return m_DeviceID
            End Get
            Set(value As String)
                m_DeviceID = value
            End Set
        End Property
        Private m_DeviceID As String

        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId() As System.Nullable(Of Integer)
            Get
                Return m_MemberId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As System.Nullable(Of Integer)

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
        Public Property Limit() As System.Nullable(Of UInteger)
            Get
                Return m_Limit
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Limit = value
            End Set
        End Property
        Private m_Limit As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
        Public Property Offset() As System.Nullable(Of UInteger)
            Get
                Return m_Offset
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Offset = value
            End Set
        End Property
        Private m_Offset As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="start", EmitDefaultValue:=False)> _
        Public Property Start() As System.Nullable(Of UInteger)
            Get
                Return m_Start
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Start = value
            End Set
        End Property
        Private m_Start As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="end", EmitDefaultValue:=False)> _
        Public Property [End]() As System.Nullable(Of UInteger)
            Get
                Return m_End
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_End = value
            End Set
        End Property
        Private m_End As System.Nullable(Of UInteger)
    End Class
End Namespace