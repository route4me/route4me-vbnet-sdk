Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class TelematicsVendors
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As String
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String
        <DataMember(Name:="slug", EmitDefaultValue:=False)>
        Public Property Slug As String
        <DataMember(Name:="logo_url", EmitDefaultValue:=False)>
        Public Property logoURL As String
        <DataMember(Name:="is_integrated", EmitDefaultValue:=False)>
        Public Property isIntegrated As String
    End Class
End Namespace
