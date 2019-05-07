Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public Class AddressForGeocoding
        Inherits GenericParameters

        <DataMember(Name:="rows", EmitDefaultValue:=False)>
        Public Property Rows As AddressField()
    End Class

    <DataContract>
    Public Class AddressField
        <DataMember(Name:="ADDRESS", EmitDefaultValue:=False)>
        Public Property Address As String
        <DataMember(Name:="CITY", EmitDefaultValue:=False)>
        Public Property City As String
        <DataMember(Name:="STATE", EmitDefaultValue:=False)>
        Public Property State As String
        <DataMember(Name:="ZIP", EmitDefaultValue:=False)>
        Public Property Zip As String
    End Class
End Namespace
