Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' Address book group class.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class AddressBookGroup
        Inherits GenericParameters

        ''' <summary>
        ''' An unique ID of the group
        ''' </summary>
        <DataMember(Name:="group_id", EmitDefaultValue:=False)>
        Public Property groupID As String

        ''' <summary>
        ''' The group name
        ''' </summary>
        <DataMember(Name:="group_name", EmitDefaultValue:=False)>
        Public Property groupName As String

        ''' <summary>
        ''' The group color
        ''' </summary>
        <DataMember(Name:="group_color", EmitDefaultValue:=False)>
        Public Property groupColor As String

        ''' <summary>
        ''' The group icon
        ''' </summary>
        <DataMember(Name:="group_icon", EmitDefaultValue:=False)>
        Public Property groupIcon As String

        ''' <summary>
        ''' A member ID the group belongs
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property memberId As String

        ''' <summary>
        ''' The AddressBookGroupFilter type object as a group filter for the address book contacs.
        ''' </summary>
        <DataMember(Name:="filter", EmitDefaultValue:=False)>
        Public Property Filter As AddressBookGroupFilter

        ''' <summary>
        ''' If true, the address book group is valid.
        ''' </summary>
        <DataMember(Name:="valid", EmitDefaultValue:=False)>
        Public Property Valid As Boolean?

    End Class
End Namespace
