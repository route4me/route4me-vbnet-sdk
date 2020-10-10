Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class DeviceLocationMatching
        <DataMember(Name:="confidence")>
        Public Property Confidence As Double?

        <DataMember(Name:="geometry")>
        Public Property Geometry As String

        <DataMember(Name:="legs")>
        Public Property Legs As DeviceLocationLeg()

        <DataMember(Name:="distance")>
        Public Property Distance As Double?

        <DataMember(Name:="duration")>
        Public Property Duration As Double?

        <DataMember(Name:="weight_name")>
        Public Property WeightName As String

        <DataMember(Name:="weight")>
        Public Property Weight As Double?
    End Class
End Namespace
