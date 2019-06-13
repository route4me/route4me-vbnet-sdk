Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class Country
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As String
        <DataMember(Name:="country_code", EmitDefaultValue:=False)>
        Public Property countryCcode As String
        <DataMember(Name:="country_name", EmitDefaultValue:=False)>
        Public Property countryName As String
    End Class
End Namespace
