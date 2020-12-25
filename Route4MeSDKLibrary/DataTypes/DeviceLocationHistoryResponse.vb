Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationHistoryResponse

        ''' <summary>
        ''' The array of the TrackingHistory objects
        ''' </summary>
        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property data As TrackingHistory()

        <DataMember(Name:="mmd", EmitDefaultValue:=False)>
        Public Property Mmd As DeviceLocationMmd
    End Class
End Namespace
