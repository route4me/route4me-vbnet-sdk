Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationSummary
        <DataMember(Name:="total_distance")>
        Public Property TotalDistance As Double?

        <DataMember(Name:="total_duration")>
        Public Property TotalDuration As Double?

        <DataMember(Name:="matchings_distance")>
        Public Property MatchingsDistance As Double?

        <DataMember(Name:="matchings_duration")>
        Public Property MatchingsDuration As Double?

        <DataMember(Name:="gaps_distance")>
        Public Property GapsDistance As Double?

        <DataMember(Name:="gaps_duration")>
        Public Property GapsDuration As Double?
    End Class
End Namespace
