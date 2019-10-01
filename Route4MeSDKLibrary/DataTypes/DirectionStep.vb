Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Direction step parameters
    ''' </summary>
    <DataContract(Name:="step")>
    Public NotInheritable Class DirectionStep

        ''' <summary>
        ''' Directions of movement
        ''' </summary>
        <DataMember(Name:="directions")>
        Public Property Directions As String

        ''' <summary>
        ''' Text for current direction
        ''' </summary>
        <DataMember(Name:="direction")>
        Public Property Direction As String

        ''' <summary>
        ''' Step distance
        ''' </summary>
        <DataMember(Name:="distance")>
        Public Property Distance As Double?

        ''' <summary>
        ''' Distance unit - 'mi' or 'km'
        ''' </summary>
        <DataMember(Name:="distance_unit")>
        Public Property DistanceUnit As String

        ''' <summary>
        ''' Maneuver type
        ''' </summary>
        <DataMember(Name:="maneuverType")>
        Public Property ManeuverType As String

        ''' <summary>
        ''' UDU Distance (UDU: User Distance Unit).
        ''' </summary>
        <DataMember(Name:="udu_distance")>
        Public Property UduDistance As String

        ''' <summary>
        ''' Compass direction
        ''' </summary>
        <DataMember(Name:="compass_direction")>
        Public Property CompassDirection As String

        ''' <summary>
        ''' Maneuver point
        ''' </summary>
        <DataMember(Name:="maneuverPoint")>
        Public Property ManeuverPoint As GeoPoint

        ''' <summary>
        ''' Step duration (sec)
        ''' </summary>
        <DataMember(Name:="duration_sec")>
        Public Property DurationSec As System.Nullable(Of Integer)

    End Class
End Namespace
