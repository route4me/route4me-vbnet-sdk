Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes


    <DataContract> _
    Public NotInheritable Class OptimizationParameters
        Inherits GenericParameters
        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)> _
        Public Property OptimizationProblemID() As String
            Get
                Return m_OptimizationProblemID
            End Get
            Set(value As String)
                m_OptimizationProblemID = Value
            End Set
        End Property
        Private m_OptimizationProblemID As String

        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="reoptimize", EmitDefaultValue:=False)> _
        Public Property ReOptimize() As System.Nullable(Of Boolean)
            Get
                Return m_ReOptimize
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_ReOptimize = Value
            End Set
        End Property
        Private m_ReOptimize As System.Nullable(Of Boolean)

        ''' <summary>
        ''' If true will be redirected
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)> _
        Public Property Redirect() As System.Nullable(Of Boolean)
            Get
                Return m_Redirect
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Redirect = value
            End Set
        End Property
        Private m_Redirect As System.Nullable(Of Boolean)

        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="show_directions", EmitDefaultValue:=False)> _
        Public Property ShowDirections() As System.Nullable(Of Boolean)
            Get
                Return m_ShowDirections
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_ShowDirections = Value
            End Set
        End Property
        Private m_ShowDirections As System.Nullable(Of Boolean)

        <DataMember(Name:="parameters", EmitDefaultValue:=False)> _
        Public Property Parameters() As RouteParameters
            Get
                Return m_Parameters
            End Get
            Set(value As RouteParameters)
                m_Parameters = Value
            End Set
        End Property
        Private m_Parameters As RouteParameters

        <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
        Public Property Addresses() As Address()
            Get
                Return m_Addresses
            End Get
            Set(value As Address())
                m_Addresses = Value
            End Set
        End Property
        Private m_Addresses As Address()
    End Class
End Namespace