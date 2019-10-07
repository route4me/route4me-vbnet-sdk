Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class AddressBookGroup
        Inherits GenericParameters

        <DataMember(Name:="group_id", EmitDefaultValue:=False)>
        Public Property groupID As String

        <DataMember(Name:="group_name", EmitDefaultValue:=False)>
        Public Property groupName As String

        <DataMember(Name:="group_color", EmitDefaultValue:=False)>
        Public Property groupColor As String

        <DataMember(Name:="group_icon", EmitDefaultValue:=False)>
        Public Property groupIcon As String

        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property memberId As String

        <DataMember(Name:="filter", EmitDefaultValue:=False)>
        Public Property Filter As AddressBookGroupFilter
    End Class
End Namespace
