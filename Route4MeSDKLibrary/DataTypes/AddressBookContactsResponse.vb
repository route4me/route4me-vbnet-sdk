Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class AddressBookContactsResponse
        Inherits GenericParameters

        <DataMember(Name:="results", EmitDefaultValue:=False)>
        Public Property results As AddressBookContact()

        <DataMember(Name:="total", EmitDefaultValue:=False)>
        Public Property total As Integer?

        <DataMember(Name:="index_query", EmitDefaultValue:=False)>
        Public Property index_query As String

        <DataMember(Name:="fields", EmitDefaultValue:=False)>
        Public Property fields As String()
    End Class


End Namespace
