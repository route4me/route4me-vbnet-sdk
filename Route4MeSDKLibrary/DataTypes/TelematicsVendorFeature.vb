Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class TelematicsVendorFeature
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As String
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String
        <DataMember(Name:="slug", EmitDefaultValue:=False)>
        Public Property Slug As String
        <DataMember(Name:="feature_group", EmitDefaultValue:=False)>
        Public Property featureGroup As String
    End Class
End Namespace
