Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class Links
        <DataMember(Name:="route")> _
        Public Property Route() As String
            Get
                Return m_Route
            End Get
            Set(value As String)
                m_Route = Value
            End Set
        End Property
        Private m_Route As String

        <DataMember(Name:="view")> _
        Public Property View() As String
            Get
                Return m_View
            End Get
            Set(value As String)
                m_View = Value
            End Set
        End Property
        Private m_View As String

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
    End Class
End Namespace
