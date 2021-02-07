Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Base class for the DataObject and DataObjectRoute classes.
    ''' </summary>
    <DataContract>
    <KnownType(GetType(DataObject))>
    <KnownType(GetType(DataObjectRoute))>
    Public Class DataObjectBase

        ''' <summary>
        ''' Optimization problem ID
        ''' </summary>
        <DataMember(Name:="optimization_problem_id")>
        Public Property OptimizationProblemId As String

        ''' <summary>
        ''' Smart Optimization ID
        ''' </summary>
        <DataMember(Name:="smart_optimization_id")>
        Public Property SmartOptimizationId As String

        ''' <summary>
        ''' When the optimization problem was created
        ''' </summary>
        <DataMember(Name:="created_timestamp", EmitDefaultValue:=False)>
        Public Property CreatedTimestamp As Long?

        ''' <summary>
        ''' Route Parameters. See <see cref="RouteParameters"/>
        ''' </summary>
        <DataMember(Name:="parameters")>
        Public Property Parameters As RouteParameters

        ''' <summary>
        ''' An array ot the Address type objects. See  <see cref="Address"/>
        ''' </summary>
        <DataMember(Name:="addresses")>
        Public Property Addresses As Address()

        ''' <summary>
        ''' The links to the GET operations for the optimization problem. See <see cref="Links"/>
        ''' </summary>
        <DataMember(Name:="links")>
        Public Property Links As Links

    End Class

End Namespace

