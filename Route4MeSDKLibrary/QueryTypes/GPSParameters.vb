Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Helper class, for setting GPS data
    ''' Used to create the suitable query string
    ''' See example in Route4MeExamples.SetGPSPosition()
    ''' </summary>
    Public NotInheritable Class GPSParameters
        Inherits GenericParameters
        <HttpQueryMemberAttribute(Name:="format")> _
        Public Property Format() As String
            Get
                Return m_Format
            End Get
            Set(value As String)
                m_Format = Value
            End Set
        End Property
        Private m_Format As String

        <HttpQueryMemberAttribute(Name:="member_id")> _
        Public Property MemberId() As Integer
            Get
                Return m_MemberId
            End Get
            Set(value As Integer)
                m_MemberId = Value
            End Set
        End Property
        Private m_MemberId As Integer

        <HttpQueryMemberAttribute(Name:="route_id")> _
        Public Property RouteId() As String
            Get
                Return m_RouteId
            End Get
            Set(value As String)
                m_RouteId = Value
            End Set
        End Property
        Private m_RouteId As String

        <HttpQueryMemberAttribute(Name:="tx_id")> _
        Public Property TxId() As String
            Get
                Return m_TxId
            End Get
            Set(value As String)
                m_TxId = Value
            End Set
        End Property
        Private m_TxId As String

        <HttpQueryMemberAttribute(Name:="vehicle_id")> _
        Public Property VehicleId() As Integer
            Get
                Return m_VehicleId
            End Get
            Set(value As Integer)
                m_VehicleId = Value
            End Set
        End Property
        Private m_VehicleId As Integer

        <HttpQueryMemberAttribute(Name:="course")> _
        Public Property Course() As Integer
            Get
                Return m_Course
            End Get
            Set(value As Integer)
                m_Course = Value
            End Set
        End Property
        Private m_Course As Integer

        <HttpQueryMemberAttribute(Name:="speed")> _
        Public Property Speed() As Double
            Get
                Return m_Speed
            End Get
            Set(value As Double)
                m_Speed = Value
            End Set
        End Property
        Private m_Speed As Double

        <HttpQueryMemberAttribute(Name:="lat")> _
        Public Property Latitude() As Double
            Get
                Return m_Latitude
            End Get
            Set(value As Double)
                m_Latitude = Value
            End Set
        End Property
        Private m_Latitude As Double

        <HttpQueryMemberAttribute(Name:="lng")> _
        Public Property Longitude() As Double
            Get
                Return m_Longitude
            End Get
            Set(value As Double)
                m_Longitude = Value
            End Set
        End Property
        Private m_Longitude As Double

        <HttpQueryMemberAttribute(Name:="last_position")> _
        Public Property last_position() As Boolean
            Get
                Return m_last_position
            End Get
            Set(value As Boolean)
                m_last_position = value
            End Set
        End Property
        Private m_last_position As Boolean

        <HttpQueryMemberAttribute(Name:="time_period")> _
        Public Property time_period() As String
            Get
                Return m_time_period
            End Get
            Set(value As String)
                m_time_period = value
            End Set
        End Property
        Private m_time_period As String

        <HttpQueryMemberAttribute(Name:="start_date")> _
        Public Property start_date() As Integer
            Get
                Return m_start_date
            End Get
            Set(value As Integer)
                m_start_date = value
            End Set
        End Property
        Private m_start_date As Integer

        <HttpQueryMemberAttribute(Name:="end_date")> _
        Public Property end_date() As Integer
            Get
                Return m_end_date
            End Get
            Set(value As Integer)
                m_end_date = value
            End Set
        End Property
        Private m_end_date As Integer

        <HttpQueryMemberAttribute(Name:="altitude", EmitDefaultValue:=False)> _
        Public Property Altitude() As Double
            Get
                Return m_Altitude
            End Get
            Set(value As Double)
                m_Altitude = Value
            End Set
        End Property
        Private m_Altitude As Double

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

        <HttpQueryMemberAttribute(Name:="device_guid")> _
        Public Property DeviceGuid() As String
            Get
                Return m_DeviceGuid
            End Get
            Set(value As String)
                m_DeviceGuid = Value
            End Set
        End Property
        Private m_DeviceGuid As String

        <HttpQueryMemberAttribute(Name:="device_timestamp", EmitDefaultValue:=False)> _
        Public Property DeviceTimestamp() As String
            Get
                Return m_DeviceTimestamp
            End Get
            Set(value As String)
                m_DeviceTimestamp = Value
            End Set
        End Property
        Private m_DeviceTimestamp As String

        <HttpQueryMemberAttribute(Name:="app_version", EmitDefaultValue:=False)> _
        Public Property AppVersion() As String
            Get
                Return m_AppVersion
            End Get
            Set(value As String)
                m_AppVersion = Value
            End Set
        End Property
        Private m_AppVersion As String
    End Class
End Namespace
