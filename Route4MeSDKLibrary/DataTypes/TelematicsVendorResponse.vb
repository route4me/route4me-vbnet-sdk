Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class TelematicsVendorResponse
        <DataMember(Name:="vendor", EmitDefaultValue:=False)>
        Public Property Vendor As TelematicsVendor
    End Class
End Namespace
