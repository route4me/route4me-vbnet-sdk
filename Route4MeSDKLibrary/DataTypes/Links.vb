Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class Links
        <DataMember(Name:="route")> _
        Public Property Route As String

        <DataMember(Name:="view")> _
        Public Property View As String

        <DataMember(Name:="optimization_problem_id")> _
        Public Property OptimizationProblemId As String
    End Class
End Namespace
