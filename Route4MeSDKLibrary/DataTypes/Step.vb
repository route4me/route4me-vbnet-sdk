Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Direction step parameters
    ''' </summary>
    <DataContract(Name:="step")> _
    Public NotInheritable Class Step1

        ''' <summary>
        ''' Directions of movement
        ''' </summary>
        <DataMember(Name:="directions")> _
        Public Property Directions() As String
            Get
                Return m_Directions
            End Get
            Set(value As String)
                m_Directions = value
            End Set
        End Property
        Private m_Directions As String

        ''' <summary>
        ''' Text for current direction
        ''' </summary>
        <DataMember(Name:="direction")> _
        Public Property Direction() As String
            Get
                Return m_Direction
            End Get
            Set(value As String)
                m_Direction = value
            End Set
        End Property
        Private m_Direction As String

        ''' <summary>
        ''' Step distance
        ''' </summary>
        <DataMember(Name:="distance")> _
        Public Property Distance() As System.Nullable(Of Double)
            Get
                Return m_Distance
            End Get
            Set(value As System.Nullable(Of Double))
                m_Distance = value
            End Set
        End Property
        Private m_Distance As System.Nullable(Of Double)

        ''' <summary>
        ''' Distance unit - 'mi' or 'km'
        ''' </summary>
        <DataMember(Name:="distance_unit")> _
        Public Property DistanceUnit() As String
            Get
                Return m_DistanceUnit
            End Get
            Set(value As String)
                m_DistanceUnit = value
            End Set
        End Property
        Private m_DistanceUnit As String

        ''' <summary>
        ''' Maneuver type
        ''' </summary>
        <DataMember(Name:="maneuverType")> _
        Public Property ManeuverType() As String
            Get
                Return m_ManeuverType
            End Get
            Set(value As String)
                m_ManeuverType = value
            End Set
        End Property
        Private m_ManeuverType As String

        ''' <summary>
        ''' Compass direction
        ''' </summary>
        <DataMember(Name:="compass_direction")> _
        Public Property CompassDirection() As String
            Get
                Return m_CompassDirection
            End Get
            Set(value As String)
                m_CompassDirection = value
            End Set
        End Property
        Private m_CompassDirection As String

        ''' <summary>
        ''' Maneuver point
        ''' </summary>
        <DataMember(Name:="maneuverPoint")> _
        Public Property ManeuverPoint() As GeoPoint
            Get
                Return m_ManeuverPoint
            End Get
            Set(value As GeoPoint)
                m_ManeuverPoint = value
            End Set
        End Property
        Private m_ManeuverPoint As GeoPoint

        ''' <summary>
        ''' Step duration (sec)
        ''' </summary>
        <DataMember(Name:="duration_sec")> _
        Public Property DurationSec() As System.Nullable(Of Integer)
            Get
                Return m_DurationSec
            End Get
            Set(value As System.Nullable(Of Integer))
                m_DurationSec = value
            End Set
        End Property
        Private m_DurationSec As System.Nullable(Of Integer)
    End Class
End Namespace
