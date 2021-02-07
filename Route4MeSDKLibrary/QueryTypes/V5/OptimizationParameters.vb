Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Optimization request parameters.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class OptimizationParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Optimization problem ID
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)>
        Public Property OptimizationProblemID As String

        ''' <summary>
        ''' If true, the optimization will be reoptimized.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="reoptimize", EmitDefaultValue:=False)>
        Public Property ReOptimize As Boolean?

        ''' <summary>
        ''' If true will be redirected
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)>
        Public Property Redirect As Boolean?

        ''' <summary>
        ''' If true, the directions are shown in the optimization.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="show_directions", EmitDefaultValue:=False)>
        Public Property ShowDirections As Boolean?

        ''' <summary>
        ''' Optimization callback URL
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="optimized_callback_url", EmitDefaultValue:=False)>
        Public Property OptimizedCallbackURL As String

        ''' <summary>
        ''' The number of existing routes that should be returned per response when looking at a list of all the routes.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As UInteger?

        ''' <summary>
        ''' The page number for route listing pagination. Increment the offset by the limit number to move to the next page.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As UInteger?

        ''' <summary>
        ''' Route start date
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="start_date", EmitDefaultValue:=False)>
        Public Property StartDate As String

        ''' <summary>
        ''' Route end date
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="end_date", EmitDefaultValue:=False)>
        Public Property EndDate As String

        ''' <summary>
        ''' Optimization query string
        ''' </summary>
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
        ''' Route Parameters to update.
        ''' (After a PUT there Is no guarantee that the route_destination_id values 
        ''' are preserved! It may create copies resulting in New destination IDs, 
        ''' especially when dealing with multiple depots.)
        ''' </summary>
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

    End Class

End Namespace

