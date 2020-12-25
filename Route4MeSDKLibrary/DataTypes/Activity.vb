Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Activity Type Class
    ''' </summary>
    <DataContract>
    Public NotInheritable Class Activity
        Inherits GenericParameters
        ''' <summary>
        ''' Activity Id
        ''' </summary>
        <DataMember(Name:="activity_id", EmitDefaultValue:=False)>
        Public Property ActivityId As String

        ''' <summary>
        ''' Activity type
        ''' </summary>
        <DataMember(Name:="activity_type", EmitDefaultValue:=False)>
        Public Property ActivityType As String

        ''' <summary>
        ''' Activity timestamp
        ''' </summary>
        <DataMember(Name:="activity_timestamp", EmitDefaultValue:=False)>
        Public Property ActivityTimestamp As Long?

        ''' <summary>
        ''' Activity message
        ''' </summary>
        <DataMember(Name:="activity_message", EmitDefaultValue:=False)>
        Public Property ActivityMessage As String

        ''' <summary>
        ''' Member Id
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As String

        ''' <summary>
        ''' Parent route Id
        ''' </summary>
        <DataMember(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' Parent route name
        ''' </summary>
        <DataMember(Name:="route_name", EmitDefaultValue:=False)>
        Public Property RouteName As String

        ''' <summary>
        ''' Destination Id
        ''' </summary>
        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As String

        ''' <summary>
        ''' Note Id
        ''' </summary>
        <DataMember(Name:="note_id", EmitDefaultValue:=False)>
        Public Property NoteId As String

        ''' <summary>
        ''' Note type
        ''' </summary>
        <DataMember(Name:="note_type", EmitDefaultValue:=False)>
        Public Property NoteType As String

        ''' <summary>
        ''' Note contents
        ''' </summary>
        <DataMember(Name:="note_contents", EmitDefaultValue:=False)>
        Public Property NoteContents As String

        ''' <summary>
        ''' Note file
        ''' </summary>
        <DataMember(Name:="note_file", EmitDefaultValue:=False)>
        Public Property NoteFile As String

        ''' <summary>
        ''' Member-owner of the activity.
        ''' </summary>
        <DataMember(Name:="member", EmitDefaultValue:=False)>
        Public Property Member As ActivityMember

        ''' <summary>
        ''' A route destination name
        ''' </summary>
        <DataMember(Name:="destination_name", EmitDefaultValue:=False)>
        Public Property DestinationName As String

        ''' <summary>
        ''' A route destination alias.
        ''' </summary>
        <DataMember(Name:="destination_alias", EmitDefaultValue:=False)>
        Public Property DestinationAlias As String

    End Class
End Namespace

