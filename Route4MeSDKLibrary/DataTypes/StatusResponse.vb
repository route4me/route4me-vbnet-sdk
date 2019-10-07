Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class StatusResponse
        Inherits GenericParameters

        <DataMember(Name:="status", EmitDefaultValue:=False)>
        Public Property Status As Boolean
    End Class
End Namespace
