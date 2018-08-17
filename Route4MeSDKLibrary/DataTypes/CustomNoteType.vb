Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class CustomNoteType
        <DataMember(Name:="note_custom_type_id", EmitDefaultValue:=False)>
        Public Property NoteCustomTypeID As Integer

        <DataMember(Name:="note_custom_type", EmitDefaultValue:=False)>
        Public Property NoteCustomType As String

        <DataMember(Name:="root_owner_member_id", EmitDefaultValue:=False)>
        Public Property RootOwnerMemberID As Integer

        <DataMember(Name:="note_custom_type_values")>
        Public Property NoteCustomTypeValues As String()
    End Class
End Namespace
