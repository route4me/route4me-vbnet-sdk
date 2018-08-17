Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Main data object data-structure
    ''' See https://www.assembla.com/spaces/route4me_api/wiki/Optimization_Problem_V4
    ''' </summary>
    <DataContract> _
    <KnownType(GetType(DataObjectRoute))> _
    Public Class DataObject
        <DataMember(Name:="optimization_problem_id")> _
        Public Property OptimizationProblemId As String

        <DataMember(Name:="state")> _
        Public Property State As OptimizationState

        <DataMember(Name:="user_errors")> _
        Public Property UserErrors As String()

        <DataMember(Name:="sent_to_background")> _
        Public Property IsSentToBackground As Boolean

        <DataMember(Name:="parameters")> _
        Public Property Parameters As RouteParameters

        <DataMember(Name:="addresses")> _
        Public Property Addresses As Address()

        <DataMember(Name:="routes")> _
        Public Property Routes As DataObjectRoute()

        <DataMember(Name:="links")> _
        Public Property Links As Links

        <DataMember(Name:="tracking_history")> _
        Public Property TrackingHistory As TrackingHistory()
    End Class
End Namespace