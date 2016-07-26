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
        Public Property OptimizationProblemId() As String
            Get
                Return m_OptimizationProblemId
            End Get
            Set(value As String)
                m_OptimizationProblemId = Value
            End Set
        End Property
        Private m_OptimizationProblemId As String

        <DataMember(Name:="state")> _
        Public Property State() As OptimizationState
            Get
                Return m_State
            End Get
            Set(value As OptimizationState)
                m_State = Value
            End Set
        End Property
        Private m_State As OptimizationState

        <DataMember(Name:="user_errors")> _
        Public Property UserErrors() As String()
            Get
                Return m_UserErrors
            End Get
            Set(value As String())
                m_UserErrors = Value
            End Set
        End Property
        Private m_UserErrors As String()

        <DataMember(Name:="sent_to_background")> _
        Public Property IsSentToBackground() As Boolean
            Get
                Return m_IsSentToBackground
            End Get
            Set(value As Boolean)
                m_IsSentToBackground = Value
            End Set
        End Property
        Private m_IsSentToBackground As Boolean

        <DataMember(Name:="parameters")> _
        Public Property Parameters() As RouteParameters
            Get
                Return m_Parameters
            End Get
            Set(value As RouteParameters)
                m_Parameters = Value
            End Set
        End Property
        Private m_Parameters As RouteParameters

        <DataMember(Name:="addresses")> _
        Public Property Addresses() As Address()
            Get
                Return m_Addresses
            End Get
            Set(value As Address())
                m_Addresses = Value
            End Set
        End Property
        Private m_Addresses As Address()

        <DataMember(Name:="routes")> _
        Public Property Routes() As DataObjectRoute()
            Get
                Return m_Routes
            End Get
            Set(value As DataObjectRoute())
                m_Routes = Value
            End Set
        End Property
        Private m_Routes As DataObjectRoute()

        <DataMember(Name:="links")> _
        Public Property Links() As Links
            Get
                Return m_Links
            End Get
            Set(value As Links)
                m_Links = value
            End Set
        End Property
        Private m_Links As Links

        <DataMember(Name:="tracking_history")> _
        Public Property TrackingHistory() As TrackingHistory()
            Get
                Return m_TrackingHistory
            End Get
            Set(value As TrackingHistory())
                m_TrackingHistory = value
            End Set
        End Property
        Private m_TrackingHistory As TrackingHistory()
    End Class
End Namespace