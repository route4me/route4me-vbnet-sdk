Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' The class for the custom note of a route destination.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class AddressCustomNote
        ''' <summary>
        ''' A unique ID (40 chars) of a custom note entry.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="note_custom_entry_id", EmitDefaultValue:=False)>
        Public Property NoteCustomEntryID As String

        ''' <summary>
        ''' The custom note ID.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="note_id", EmitDefaultValue:=False)>
        Public Property NoteID As String

        ''' <summary>
        ''' The custom note type ID.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="note_custom_type_id", EmitDefaultValue:=False)>
        Public Property NoteCustomTypeID As String

        ''' <summary>
        ''' The custom note value.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="note_custom_value")>
        Public Property NoteCustomValue As String

        ''' <summary>
        ''' The custom note type.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="note_custom_type")>
        Public Property NoteCustomType As String
    End Class
End Namespace
