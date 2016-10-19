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

        <HttpQueryMemberAttribute(Name:="team", EmitDefaultValue:=False)> _
        Public Property team() As String
            Get
                Return m_team
            End Get
            Set(value As String)
                m_team = value
            End Set
        End Property
        Private m_team As String

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

        <HttpQueryMemberAttribute(Name:="activity_type", EmitDefaultValue:=False)> _
        Public Property ActivityType() As String
            Get
                Return m_ActivityType
            End Get
            Set(value As String)
                m_ActivityType = value
            End Set
        End Property
        Private m_ActivityType As String

        <HttpQueryMemberAttribute(Name:="activity_message", EmitDefaultValue:=False)> _
        Public Property ActivityMessage() As String
            Get
                Return m_ActivityMessage
            End Get
            Set(value As String)
                m_ActivityMessage = value
            End Set
        End Property
        Private m_ActivityMessage As String
    End Class
End Namespace