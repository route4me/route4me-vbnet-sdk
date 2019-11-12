Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Base class for the DataObject and DataObjectRoute classes
    ''' </summary>
    <DataContract>
    <KnownType(GetType(DataObject))>
    <KnownType(GetType(DataObjectRoute))>
    Public Class DataObjectBase
        <DataMember(Name:="optimization_problem_id")>
        Public Property OptimizationProblemId As String

        <DataMember(Name:="parameters")>
        Public Property Parameters As RouteParameters

        <DataMember(Name:="addresses")>
        Public Property Addresses As Address()

        <DataMember(Name:="links")>
        Public Property Links As Links

        ''' <summary>
        ''' When the optimization problem was created
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="created_timestamp")>
        Public Property CreatedTimestamp As Long?
    End Class
End Namespace
