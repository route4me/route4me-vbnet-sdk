Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class ActivityParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        <HttpQueryMemberAttribute(Name:="device_id", EmitDefaultValue:=False)>
        Public Property DeviceID As String

        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As UInteger?

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As UInteger?

        <HttpQueryMemberAttribute(Name:="team", EmitDefaultValue:=False)>
        Public Property team As String

        <HttpQueryMemberAttribute(Name:="start", EmitDefaultValue:=False)>
        Public Property Start() As Long?

        <HttpQueryMemberAttribute(Name:="end", EmitDefaultValue:=False)>
        Public Property [End]() As Long?

        <HttpQueryMemberAttribute(Name:="activity_type", EmitDefaultValue:=False)>
        Public Property ActivityType As String

        <HttpQueryMemberAttribute(Name:="activity_message", EmitDefaultValue:=False)>
        Public Property ActivityMessage As String

    End Class
End Namespace