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
        <DataMember(Name:="time")>
        Public Property Time As Long?

        ''' <summary>
        ''' Current segment length
        ''' </summary>
        <DataMember(Name:="segment_distance")> _
        Public Property SegmentDistance As Double

        ''' <summary>
        ''' Direction name
        ''' </summary>
        <DataMember(Name:="name")> _
        Public Property Name As String

        ''' <summary>
        ''' Start location name
        ''' </summary>
        <DataMember(Name:="start_location")> _
        Public Property StartLocation As String

        ''' <summary>
        ''' End location name
        ''' </summary>
        <DataMember(Name:="end_location")> _
        Public Property EndLocation As String

        ''' <summary>
        ''' Directions error message
        ''' </summary>
        <DataMember(Name:="directions_error")> _
        Public Property DircetionsError As String

        ''' <summary>
        ''' Error code
        ''' </summary>
        <DataMember(Name:="error_code")> _
        Public Property ErrorCode As Integer
    End Class
End Namespace
