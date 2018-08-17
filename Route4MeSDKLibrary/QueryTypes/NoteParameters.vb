Namespace Route4MeSDK.QueryTypes

    Public NotInheritable Class NoteParameters
        Inherits GenericParameters
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId As String

        <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)> _
        Public Property AddressId As Integer

        <HttpQueryMemberAttribute(Name:="dev_lat")> _
        Public Property Latitude As Double

        <HttpQueryMemberAttribute(Name:="dev_lng")> _
        Public Property Longitude As Double

        <HttpQueryMemberAttribute(Name:="device_type")> _
        Public Property DeviceType As String

        <HttpQueryMemberAttribute(Name:="strUpdateType")> _
        Public Property ActivityType As String

        <HttpQueryMemberAttribute(Name:="format")> _
        Public Property Format As String

        '[HttpQueryMemberAttribute(Name = "strNoteContents", EmitDefaultValue = false)]
        'public string StrNoteContents { get; set; }
    End Class
End Namespace
