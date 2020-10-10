Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes

    Public NotInheritable Class NoteParameters
        Inherits GenericParameters

        Public Sub New()
            Format = "json"
        End Sub

        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)>
        Public Property AddressId As Integer

        <HttpQueryMemberAttribute(Name:="dev_lat")>
        Public Property Latitude As Double

        <HttpQueryMemberAttribute(Name:="dev_lng")>
        Public Property Longitude As Double

        <HttpQueryMemberAttribute(Name:="device_type")>
        Public Property DeviceType As String

        <HttpQueryMemberAttribute(Name:="format")>
        Public Property Format As String

        ''' <summary>
        ''' An acitivity type related to the note.
        ''' API equivalent: strUpdateType.
        ''' </summary>
        Public Property ActivityType As String

        ''' <summary>
        ''' Text content of the note.
        ''' API equivalent: strNoteContents.
        ''' </summary>
        Public Property StrNoteContents As String

        ''' <summary>
        ''' A temporary filename of a prepared for uploading file.
        ''' API equivalent: strFileName.
        ''' </summary>
        Public Property StrFileName As String

        ''' <summary>
        ''' Form data parameter. 
        ''' Example item: "custom_note_type[412]": "do a service", 
        ''' where 412 Is "note_custom_type_id"
        ''' </summary>
        Public Property CustomNoteTypes As Dictionary(Of String, String)
    End Class
End Namespace
