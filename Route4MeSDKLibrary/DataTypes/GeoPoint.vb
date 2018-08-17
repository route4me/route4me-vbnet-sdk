Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class GeoPoint
        <DataMember(Name:="lat")> _
        Public Property Latitude As Double

        <DataMember(Name:="lng")> _
        Public Property Longitude As Double
    End Class
End Namespace
