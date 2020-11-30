Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class User
        'the id of the member inside the route4me system
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        <DataMember(Name:="account_type_id", EmitDefaultValue:=False)>
        Public Property AccountTypeId As Integer?

        <DataMember(Name:="member_type", EmitDefaultValue:=False)>
        Public Property MemberType As String

        <DataMember(Name:="member_first_name")>
        Public Property MemberFirstName As String

        <DataMember(Name:="member_last_name")>
        Public Property MemberLastName As String

        <DataMember(Name:="member_email")>
        Public Property MemberEmail As String

        <DataMember(Name:="phone_number")>
        Public Property PhoneNumber As String

        <DataMember(Name:="readonly_user", EmitDefaultValue:=False)>
        Public Property ReadonlyUser As Boolean?

        <DataMember(Name:="show_superuser_addresses", EmitDefaultValue:=False)>
        Public Property ShowSuperuserAddresses As Boolean?

    End Class
End Namespace