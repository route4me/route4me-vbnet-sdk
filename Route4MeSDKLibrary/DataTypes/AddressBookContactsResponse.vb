Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class AddressBookContactsResponse
        Inherits GenericParameters
        <DataMember(Name:="results", EmitDefaultValue:=False)> _
        Public Property results As AddressBookContact()

        <DataMember(Name:="total", EmitDefaultValue:=False)> _
        Public Property total As System.Nullable(Of Integer)
    End Class
End Namespace
