Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' The class for address note
    ''' </summary>
    <DataContract>
    Public NotInheritable Class AddressNote
        ''' <summary>
        ''' An unique ID of a note
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="note_id", EmitDefaultValue:=False)>
        Public Property NoteId As Integer

        ''' <summary>
        ''' The route ID
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' The route destination ID
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As Integer

        ''' <summary>
        ''' An unique ID of an uploaded file
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="upload_id")>
        Public Property UploadId As String

        ''' <summary>
        ''' When the note was added
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="ts_added", EmitDefaultValue:=False)>
        Public Property TimestampAdded As Long?

        ''' <summary>
        ''' The position latitude where the address note was added
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="lat")>
        Public Property Latitude As Double

        ''' <summary>
        ''' The position longitude where the address note was added
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="lng")>
        Public Property Longitude As Double

        ''' <summary>
        ''' The activity type
        ''' See available activity types here: "https://github.com/route4me/route4me-json-schemas/blob/master/Activity.dtd#L23"/>
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="activity_type")>
        Public Property ActivityType As String

        ''' <summary>
        ''' The note text contents
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="contents")>
        Public Property Contents As String

        ''' <summary>
        ''' An upload type of the note
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="upload_type")>
        Public Property UploadType As String

        ''' <summary>
        ''' An upload url - where a file-note was uploaded.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="upload_url")>
        Public Property UploadUrl As String

        ''' <summary>
        ''' An extension of the uploaded file.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="upload_extension")>
        Public Property UploadExtension As String

        ''' <summary>
        ''' The device a note was uploaded from
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="device_type")>
        Public Property DeviceType As String

        ''' <summary>
        ''' Array of the custom type notes 
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="custom_types")>
        Public Property CustomTypes As AddressCustomNote()
    End Class
End Namespace
