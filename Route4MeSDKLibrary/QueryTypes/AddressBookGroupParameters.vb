Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    <DataContract>
    Public NotInheritable Class AddressBookGroupParameters
        Inherits GenericParameters

        <DataMember(Name:="group_id")>
        Public Property group_id As String

        <DataMember(Name:="fields")>
        Public Property Fields As String()

        <DataMember(Name:="offset")>
        Public Property Offset_ As Integer

        <DataMember(Name:="limit")>
        Public Property Limit_ As Integer

        <DataMember(Name:="filter")>
        Public Property filter As AddressBookGroupFilterParameter

        <HttpQueryMemberAttribute(Name:="group_id", EmitDefaultValue:=False)>
        Public Property GroupId As String

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As Integer

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As Integer
    End Class

    <DataContract>
    Public NotInheritable Class AddressBookGroupFilterParameter
        Inherits GenericParameters

        <DataMember(Name:="query")>
        Public Property Query As String

        <DataMember(Name:="display")>
        Public Property Display As String
    End Class
End Namespace