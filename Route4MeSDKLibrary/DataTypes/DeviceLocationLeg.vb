Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationLeg
        <DataMember(Name:="geometry")>
        Public Property Geometry As String

        <DataMember(Name:="steps")>
        Public Property Steps As Object()

        <DataMember(Name:="distance")>
        Public Property Distance As Double?

        <DataMember(Name:="duration")>
        Public Property Duration As Double?

        <DataMember(Name:="summary")>
        Public Property Summary As String

        <DataMember(Name:="weight")>
        Public Property Weight As Double?
    End Class
End Namespace
