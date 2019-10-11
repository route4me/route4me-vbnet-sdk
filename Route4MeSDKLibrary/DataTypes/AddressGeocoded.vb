Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class AddressGeocoded
        <DataMember(Name:="address", EmitDefaultValue:=False)>
        Public Property geocodedAddress As Address

        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer

        <DataMember(Name:="member_type_id", EmitDefaultValue:=False)>
        Public Property memberTypeID As Integer

        <DataMember(Name:="userLat")>
        Public Property userLatitude As Double?

        <DataMember(Name:="userLng")>
        Public Property userLongitude As Double?

        <DataMember(Name:="intUserIP", EmitDefaultValue:=False)>
        Public Property intUserIP As UInteger

        <DataMember(Name:="strGeocodingMethod", EmitDefaultValue:=False)>
        Public Property strGeocodingMethod As String

        <DataMember(Name:="getCurbside", EmitDefaultValue:=False)>
        Public Property getCurbside As Boolean

        <DataMember(Name:="priority", EmitDefaultValue:=False)>
        Public Property Priority As Integer
    End Class
End Namespace

