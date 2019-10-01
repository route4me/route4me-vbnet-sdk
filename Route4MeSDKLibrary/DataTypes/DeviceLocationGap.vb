Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationGap
        <DataMember(Name:="distance")>
        Public Property Distance As Decimal?

        <DataMember(Name:="duration")>
        Public Property Duration As Decimal?

        <DataMember(Name:="geometry")>
        Public Property Geometry As String
    End Class
End Namespace
