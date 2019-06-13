Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class TelematicsVendorsResponse
        <DataMember(Name:="vendors", EmitDefaultValue:=False)>
        Public Property Vendors As TelematicsVendors()
    End Class
End Namespace
