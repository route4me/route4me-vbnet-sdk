Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class OrdersResponse
        Inherits GenericParameters
        <DataMember(Name:="results", EmitDefaultValue:=False)>
        Public Property results As Order()

        <DataMember(Name:="total", EmitDefaultValue:=False)>
        Public Property total As Integer?

    End Class
End Namespace
