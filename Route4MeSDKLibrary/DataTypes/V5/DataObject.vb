Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Main data object data-structure
    ''' See <see cref="https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4"/>  
    ''' </summary>
    <DataContract>
    Public Class DataObject
        Inherits DataObjectBase

        ''' <summary>
        ''' An optimization problem state. See <see cref="OptimizationState"/>
        ''' </summary>
        <DataMember(Name:="state")>
        Public Property State As OptimizationState

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
        ''' If true it means the solution was not returned 
        ''' (it is being computed in the background)
        ''' </summary>
        <DataMember(Name:="sent_to_background")>
        Public Property IsSentToBackground As Boolean

        ''' <summary>
        ''' An Unix Timestamp the Optimization Problem was scheduled for
        ''' </summary>
        <DataMember(Name:="scheduled_for", EmitDefaultValue:=False)>
        Public Property ScheduledFor As Long?

        ''' <summary>
        ''' An array ot the DataObjectRoute type objects. See <see cref="DataObjectRoute"/>
        ''' <para>The routes included in the optimization problem</para>
        ''' </summary>
        <DataMember(Name:="routes")>
        Public Property Routes As DataObjectRoute()

        ''' <summary>
        ''' Total number of the addresses included in the optimization
        ''' </summary>
        <DataMember(Name:="total_addresses", EmitDefaultValue:=False)>
        Public Property TotalAddresses As Integer?

    End Class

End Namespace

