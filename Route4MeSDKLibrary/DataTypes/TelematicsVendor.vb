Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class TelematicsVendor
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As String
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String
        <DataMember(Name:="title", EmitDefaultValue:=False)>
        Public Property Title As String
        <DataMember(Name:="slug", EmitDefaultValue:=False)>
        Public Property Slug As String
        <DataMember(Name:="description", EmitDefaultValue:=False)>
        Public Property Description As String
        <DataMember(Name:="logo_url", EmitDefaultValue:=False)>
        Public Property logoURL As String
        <DataMember(Name:="website_url", EmitDefaultValue:=False)>
        Public Property websiteURL As String
        <DataMember(Name:="api_docs_url", EmitDefaultValue:=False)>
        Public Property apiDocsURL As String
        <DataMember(Name:="is_integrated", EmitDefaultValue:=False)>
        Public Property isIntegrated As String
        <DataMember(Name:="size", EmitDefaultValue:=False)>
        Public Property Size As String
        <DataMember(Name:="features", EmitDefaultValue:=False)>
        Public Property Features As TelematicsVendorFeature()
        <DataMember(Name:="countries", EmitDefaultValue:=False)>
        Public Property Countries As Country()
    End Class
End Namespace
