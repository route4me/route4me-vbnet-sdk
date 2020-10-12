Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Main data object data-structure
    ''' See https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4
    ''' </summary>
    <DataContract>
    Public Class DataObject
        Inherits DataObjectBase

        <DataMember(Name:="state")> _
        Public Property State As OptimizationState

        <DataMember(Name:="user_errors")>
        Public Property UserErrors As String()

        <DataMember(Name:="optimization_errors")>
        Public Property OptimizationErrors As String()

        <DataMember(Name:="sent_to_background")> _
        Public Property IsSentToBackground As Boolean

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