Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationTracePoint
        <DataMember(Name:="alternatives_count")>
        Public Property AlternativesCount As Integer?

        <DataMember(Name:="waypoint_index")>
        Public Property WaypointIndex As Integer?

        <DataMember(Name:="location")>
        Public Property Location As Integer()

        <DataMember(Name:="name")>
        Public Property Name As String

        <DataMember(Name:="distance")>
        Public Property Distance As Decimal?

        <DataMember(Name:="matchings_index")>
        Public Property MatchingsIndex As Integer?
    End Class
End Namespace
