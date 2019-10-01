Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationHistoryResponse
        <DataMember(Name:="data")>
        Public Property data As TrackingHistory()

        <DataMember(Name:="mmd")>
        Public Property Mmd As DeviceLocationMmd
    End Class
End Namespace
