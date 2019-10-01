Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationLeg
        <DataMember(Name:="geometry")>
        Public Property Geometry As String

        <DataMember(Name:="steps")>
        Public Property Steps As Object()

        <DataMember(Name:="distance")>
        Public Property Distance As Decimal?

        <DataMember(Name:="duration")>
        Public Property Duration As Decimal?

        <DataMember(Name:="summary")>
        Public Property Summary As String

        <DataMember(Name:="weight")>
        Public Property Weight As Decimal?
    End Class
End Namespace
