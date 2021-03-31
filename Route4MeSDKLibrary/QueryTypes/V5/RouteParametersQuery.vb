Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Route parameters accepted by endpoints
    ''' </summary>
    <DataContract>
    Public NotInheritable Class RouteParametersQuery
        Inherits GenericParameters

        ''' <summary>
        ''' Route Identifier
        ''' </summary>
        <IgnoreDataMember> ' Don't serialize as JSON
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' Pass True to return directions and the route path
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="directions", EmitDefaultValue:=False)>
        Public Property Directions As Boolean?

        ''' <summary>
        ''' "None" - no path output. "Points" - points path output
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="route_path_output", EmitDefaultValue:=False)>
        Public Property RoutePathOutput As String

        ''' <summary>
        ''' Output route tracking data in response
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="device_tracking_history", EmitDefaultValue:=False)>
        Public Property DeviceTrackingHistory As Boolean?

        ''' <summary>
        ''' The number of existing routes that should be returned per response 
        ''' when looking at a list of all the routes.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As UInteger?

        ''' <summary>
        ''' The page number for route listing pagination. 
        ''' Increment the offset by the limit number to move to the next page.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As UInteger?

        ''' <summary>
        ''' Start date
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="start_date", EmitDefaultValue:=False)>
        Public Property StartDate As String

        ''' <summary>
        ''' End date
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="end_date", EmitDefaultValue:=False)>
        Public Property EndDate As String

        ''' <summary>
        ''' Output addresses and directions in the original optimization request sequence. 
        ''' This Is to allow us to compare routes before and after optimization.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="original", EmitDefaultValue:=False)>
        Public Property Original As Boolean?

        ''' <summary>
        ''' Output route and stop-specific notes. The notes will have timestamps, 
        ''' note types, And geospatial information if available
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="notes", EmitDefaultValue:=False)>
        Public Property Notes As Boolean?

        ''' <summary>
        ''' If true, the order inventory info included in the response.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="order_inventory", EmitDefaultValue:=False)>
        Public Property OrderInventory As Boolean?

        ''' <summary>
        ''' If true, not visited destinations of an active route re-optimized (re-sequenced).
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="remaining", EmitDefaultValue:=False)>
        Public Property Remaining As Boolean?

        ''' <summary>
        ''' Search query
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)>
        Public Property Query As String

        ''' <summary>
        ''' Updating a route supports the reoptimize=1 parameter, 
        ''' which reoptimizes only that route. 
        ''' Also supports the parameters from GET.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="reoptimize", EmitDefaultValue:=False)>
        Public Property ReOptimize As Boolean?

        ''' <summary>
        ''' Whether disable or not a route optimization.
        ''' <para>Available values </para>
        ''' <value>true - disable a route optimization. false - Not disable a route optimization.</value>
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="disable_optimization", EmitDefaultValue:=False)>
        Public Property DisableOptimization As Boolean?

        ''' <summary>
        ''' The driving directions will be generated biased for this selection. This has no impact on route sequencing.
        ''' <para>Available values </para>
        ''' <value>'Distance', 'Time', 'timeWithTraffic'.</value>
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="optimize", EmitDefaultValue:=False)>
        Public Property Optimize As String

        ''' <summary>
        ''' By sending recompute_directions=1 we request that the route directions 
        ''' be recomputed (note that this does happen automatically if certain properties 
        ''' of the route are updated, such as stop sequence_no changes Or round-tripness)
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="recompute_directions", EmitDefaultValue:=False)>
        Public Property RecomputeDirections As Boolean?

        ''' <summary>
        ''' 
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="response_format", EmitDefaultValue:=False)>
        Public Property ResponseFormat As String

        ''' <summary>
        ''' 
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As Integer?

        ''' <summary>
        ''' If true, will be redirected
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)>
        Public Property Redirect As Boolean?

        ''' <summary>
        ''' If true, the address bundling info is included into route response.
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="bundling_items", EmitDefaultValue:=False)>
        Public Property BundlingItems As Boolean?

        ''' <summary>
        ''' 
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="page", EmitDefaultValue:=False)>
        Public Property Page As Integer?

        ''' <summary>
        ''' 
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="per_page", EmitDefaultValue:=False)>
        Public Property PerPage As Integer?

        ''' <summary>
        ''' Route Parameters to update.
        ''' (After a PUT there Is no guarantee that the route_destination_id values are preserved! 
        ''' It may create copies resulting in New destination IDs, especially when dealing with multiple depots.)
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

        ''' <summary>
        ''' If true, the route is approved for execution.
        ''' </summary>
        <DataMember(Name:="approved_for_execution", EmitDefaultValue:=False)>
        Public Property ApprovedForExecution As Boolean

        ''' <summary>
        ''' If true, the route will be unlinked from the master optimization.
        ''' </summary>
        <DataMember(Name:="unlink_from_master_optimization", EmitDefaultValue:=False)>
        Public Property UnlinkFromMasterOptimization As Boolean

    End Class

End Namespace

