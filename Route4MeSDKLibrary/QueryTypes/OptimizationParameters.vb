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

        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="reoptimize", EmitDefaultValue:=False)> _
        Public Property ReOptimize() As System.Nullable(Of Boolean)

        ''' <summary>
        ''' If true will be redirected
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)> _
        Public Property Redirect() As System.Nullable(Of Boolean)

        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="show_directions", EmitDefaultValue:=False)> _
        Public Property ShowDirections() As System.Nullable(Of Boolean)

        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="optimized_callback_url", EmitDefaultValue:=False)> _
        Public Property OptimizedCallbackURL() As String

        <DataMember(Name:="parameters", EmitDefaultValue:=False)> _
        Public Property Parameters() As RouteParameters

        <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
        Public Property Addresses() As Address()

    End Class
End Namespace