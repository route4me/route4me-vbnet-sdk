Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationMmd
        <DataMember(Name:="matchings")>
        Public Property Matchings As DeviceLocationMatching()

        <DataMember(Name:="tracepoints")>
        Public Property Tracepoints As DeviceLocationTracePoint()

        <DataMember(Name:="gaps")>
        Public Property Gaps As DeviceLocationGap()

        <DataMember(Name:="summary")>
        Public Property Summary As DeviceLocationSummary
    End Class
End Namespace
