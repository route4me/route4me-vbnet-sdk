Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes

    <DataContract> _
    Public NotInheritable Class OptimizationParameters
        Inherits GenericParameters
        ' Don't serialize as JSON
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)>
        Public Property OptimizationProblemID As String

        ' Don't serialize as JSON
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="reoptimize", EmitDefaultValue:=False)>
        Public Property ReOptimize As Boolean?

        ''' <summary>
        ''' The number of existing optimizations that should be returned per response when looking at a list of all the optimizations.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As UInteger?

        ''' <summary>
        ''' The page number for optimization listing pagination. Increment the offset by the limit number to move to the next page.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As UInteger?

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="start_date", EmitDefaultValue:=False)>
        Public Property StartDate As String

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="end_date", EmitDefaultValue:=False)>
        Public Property EndDate As String

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)>
        Public Property Query As String

        ''' <summary>
        ''' The optimization state: 
        ''' New = 0,
        ''' Initial = 1, 
        ''' MatrixProcessing = 2, 
        ''' Optimizing = 3, 
        ''' Optimized = 4, 
        ''' Error = 5, 
        ''' ComputingDirections = 6,
        ''' InQueue = 7
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="state", EmitDefaultValue:=False)>
        Public Property State As UInteger?

        ''' <summary>
        ''' If true, the response contains only optimization_problem_id
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="id_only", EmitDefaultValue:=False)>
        Public Property IdOnly As Boolean?

        ''' <summary>
        ''' If true will be redirected
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)>
        Public Property Redirect() As Boolean?

        ' Don't serialize as JSON
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="show_directions", EmitDefaultValue:=False)>
        Public Property ShowDirections() As Boolean?

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="optimized_callback_url", EmitDefaultValue:=False)>
        Public Property OptimizedCallbackURL As String

        <DataMember(Name:="parameters", EmitDefaultValue:=False)>
        Public Property Parameters As RouteParameters

        ''' <summary>
        ''' Array of the route addresses
        ''' </summary>
        <DataMember(Name:="addresses", EmitDefaultValue:=False)>
        Public Property Addresses As Address()

        ''' <summary>
        ''' Array of the depots
        ''' </summary>
        <DataMember(Name:="depots", EmitDefaultValue:=False)>
        Public Property Depots As Address()

        ''' <summary>
        ''' The order territories containing addresses for an optimization process.
        ''' </summary>
        <DataMember(Name:="order_territories")>
        Public Property OrderTerritories As OrderTerritories

    End Class
End Namespace