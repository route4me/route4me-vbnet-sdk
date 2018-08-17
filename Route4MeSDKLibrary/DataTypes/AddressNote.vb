Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class AddressNote
        <DataMember(Name:="note_id", EmitDefaultValue:=False)> _
        Public Property NoteId As Integer

        <DataMember(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId As String

        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)> _
        Public Property RouteDestinationId As Integer

        <DataMember(Name:="upload_id")> _
        Public Property UploadId As String

        <DataMember(Name:="ts_added", EmitDefaultValue:=False)> _
        Public Property TimestampAdded As System.Nullable(Of UInteger)

        <DataMember(Name:="lat")> _
        Public Property Latitude As Double

        <DataMember(Name:="lng")> _
        Public Property Longitude As Double

        <DataMember(Name:="activity_type")> _
        Public Property ActivityType As String

        <DataMember(Name:="contents")> _
        Public Property Contents As String

        <DataMember(Name:="upload_type")> _
        Public Property UploadType As String

        <DataMember(Name:="upload_url")> _
        Public Property UploadUrl As String

        <DataMember(Name:="upload_extension")> _
        Public Property UploadExtension As String

        <DataMember(Name:="device_type")> _
        Public Property DeviceType As String
    End Class
End Namespace
