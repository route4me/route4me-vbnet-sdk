Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class TelematicsVendorsSearchResponse
        <DataMember(Name:="vendors", EmitDefaultValue:=False)>
        Public Property Vendors As TelematicsVendor()
    End Class
End Namespace
