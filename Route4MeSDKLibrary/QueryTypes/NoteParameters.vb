Namespace Route4MeSDK.QueryTypes

    Public NotInheritable Class NoteParameters
        Inherits GenericParameters
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId() As String
            Get
                Return m_RouteId
            End Get
            Set(value As String)
                m_RouteId = Value
            End Set
        End Property
        Private m_RouteId As String

        <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)> _
        Public Property AddressId() As Integer
            Get
                Return m_AddressId
            End Get
            Set(value As Integer)
                m_AddressId = Value
            End Set
        End Property
        Private m_AddressId As Integer

        <HttpQueryMemberAttribute(Name:="dev_lat")> _
        Public Property Latitude() As Double
            Get
                Return m_Latitude
            End Get
            Set(value As Double)
                m_Latitude = Value
            End Set
        End Property
        Private m_Latitude As Double

        <HttpQueryMemberAttribute(Name:="dev_lng")> _
        Public Property Longitude() As Double
            Get
                Return m_Longitude
            End Get
            Set(value As Double)
                m_Longitude = Value
            End Set
        End Property
        Private m_Longitude As Double

        <HttpQueryMemberAttribute(Name:="device_type")> _
        Public Property DeviceType() As String
            Get
                Return m_DeviceType
            End Get
            Set(value As String)
                m_DeviceType = Value
            End Set
        End Property
        Private m_DeviceType As String

        <HttpQueryMemberAttribute(Name:="strUpdateType")> _
        Public Property ActivityType() As String
            Get
                Return m_ActivityType
            End Get
            Set(value As String)
                m_ActivityType = Value
            End Set
        End Property
        Private m_ActivityType As String

        '[HttpQueryMemberAttribute(Name = "strNoteContents", EmitDefaultValue = false)]
        'public string StrNoteContents { get; set; }
    End Class
End Namespace
