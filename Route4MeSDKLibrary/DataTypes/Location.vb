Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Direction location parameters
    ''' </summary>
    <DataContract(Name:="location")> _
    Public NotInheritable Class Location
        ''' <summary>
        ''' Time (location)
        ''' </summary>
        <DataMember(Name:="time")> _
        Public Property Time() As Integer
            Get
                Return m_Time
            End Get
            Set(value As Integer)
                m_Time = value
            End Set
        End Property
        Private m_Time As Integer

        ''' <summary>
        ''' Current segment length
        ''' </summary>
        <DataMember(Name:="segment_distance")> _
        Public Property SegmentDistance() As Double
            Get
                Return m_SegmentDistance
            End Get
            Set(value As Double)
                m_SegmentDistance = value
            End Set
        End Property
        Private m_SegmentDistance As Double

        ''' <summary>
        ''' Direction name
        ''' </summary>
        <DataMember(Name:="name")> _
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String

        ''' <summary>
        ''' Start location name
        ''' </summary>
        <DataMember(Name:="start_location")> _
        Public Property StartLocation() As String
            Get
                Return m_StartLocation
            End Get
            Set(value As String)
                m_StartLocation = value
            End Set
        End Property
        Private m_StartLocation As String

        ''' <summary>
        ''' End location name
        ''' </summary>
        <DataMember(Name:="end_location")> _
        Public Property EndLocation() As String
            Get
                Return m_EndLocation
            End Get
            Set(value As String)
                m_EndLocation = value
            End Set
        End Property
        Private m_EndLocation As String

        ''' <summary>
        ''' Directions error message
        ''' </summary>
        <DataMember(Name:="directions_error")> _
        Public Property DircetionsError() As String
            Get
                Return m_DircetionsError
            End Get
            Set(value As String)
                m_DircetionsError = value
            End Set
        End Property
        Private m_DircetionsError As String

        ''' <summary>
        ''' Error code
        ''' </summary>
        <DataMember(Name:="error_code")> _
        Public Property ErrorCode() As Integer
            Get
                Return m_ErrorCode
            End Get
            Set(value As Integer)
                m_ErrorCode = value
            End Set
        End Property
        Private m_ErrorCode As Integer
    End Class
End Namespace
