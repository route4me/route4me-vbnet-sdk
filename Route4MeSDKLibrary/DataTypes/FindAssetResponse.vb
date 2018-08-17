Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class FindAssetResponse
        <DataMember(Name:="tracking_number")> _
        Public Property TrackingNumber As String

        <DataMember(Name:="status_history")> _
        Public Property StatusHistory As String()

        <DataMember(Name:="locations")> _
        Public Property Locations As FindAssetResponseLocations()

        <DataMember(Name:="custom_data", EmitDefaultValue:=False)> _
        Public Property CustomData As Dictionary(Of String, String)

        <DataMember(Name:="arrival")> _
        Public Property Arrival As FindAssetResponseArrival()

        <DataMember(Name:="delivered", EmitDefaultValue:=False)> _
        Public Property Delivered As System.Nullable(Of Boolean)
    End Class

    <DataContract> _
    Public NotInheritable Class FindAssetResponseLocations
        <DataMember(Name:="lat")> _
        Public Property Latitude As Double

        <DataMember(Name:="lng")> _
        Public Property Longitude As Double

        <DataMember(Name:="icon")> _
        Public Property Icon As String
    End Class

    <DataContract> _
    Public NotInheritable Class FindAssetResponseArrival
        <DataMember(Name:="from_unix_timestamp")> _
        Public Property FromUnixTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="to_unix_timestamp")> _
        Public Property ToUnixTimestamp As System.Nullable(Of Integer)
    End Class
End Namespace