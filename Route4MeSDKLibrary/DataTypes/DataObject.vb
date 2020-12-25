Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Main data object data-structure
    ''' See https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4
    ''' </summary>
    <DataContract>
    Public Class DataObject
        Inherits DataObjectBase

        ''' <summary>
        ''' An optimization problem state.
        ''' </summary>
        <DataMember(Name:="state")>
        Public Property State As OptimizationState

        ''' <summary>
        ''' Smart optimization ID
        ''' </summary>
        <DataMember(Name:="smart_optimization_id")>
        Public Property SmartOptimizationId As String()

        ''' <summary>
        ''' An array of the user errors
        ''' </summary>
        <DataMember(Name:="user_errors")>
        Public Property UserErrors As String()

        ''' <summary>
        ''' An array of the optimization errors
        ''' </summary>
        <DataMember(Name:="optimization_errors")>
        Public Property OptimizationErrors As String()

        ''' <summary>
        ''' If true it means the solution was not returned (it is being computed in the background).
        ''' </summary>
        <DataMember(Name:="sent_to_background")> _
        Public Property IsSentToBackground As Boolean

        ''' <summary>
        ''' An array ot the DataObjectRoute type objects.
        ''' The routes included in the optimization problem.
        ''' </summary>
        <DataMember(Name:="routes")>
        Public Property Routes As DataObjectRoute()

        ''' <summary>
        ''' Total number of the addresses included in the optimization
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="total_addresses")>
        Public Property TotalAddresses As Integer?

        ''' <summary>
        ''' An Unix Timestamp the Optimization Problem was scheduled for
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="scheduled_for")>
        Public Property ScheduledFor As Long?
    End Class
End Namespace