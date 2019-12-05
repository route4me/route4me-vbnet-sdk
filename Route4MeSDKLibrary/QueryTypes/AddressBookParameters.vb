Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class AddressBookParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)>
        Public Property AddressId As String

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As UInteger?

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As UInteger?

        <HttpQueryMemberAttribute(Name:="start", EmitDefaultValue:=False)>
        Public Property Start As Long?

        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)>
        Public Property Query As String

        <DataMember(Name:="fields", EmitDefaultValue:=False)>
        Public Property Fields As String

        <HttpQueryMemberAttribute(Name:="display", EmitDefaultValue:=False)>
        Public Property Display As String

    End Class
End Namespace
