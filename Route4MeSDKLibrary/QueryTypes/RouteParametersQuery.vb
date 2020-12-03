Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization
Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Route parameters accepted by endpoints
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class RouteParametersQuery
        Inherits GenericParameters

        ''' <summary>
        ''' Route Identifier
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId As String

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As Integer?

        ''' <summary>
        '''  	Pass True to return directions and the route path
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="directions", EmitDefaultValue:=False)>
        Public Property Directions As Boolean?

        ''' <summary>
        ''' "None" - no path output. "Points" - points path output
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="route_path_output", EmitDefaultValue:=False)> _
        Public Property RoutePathOutput As String

        ''' <summary>
        ''' Output route tracking data in response
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="device_tracking_history", EmitDefaultValue:=False)>
        Public Property DeviceTrackingHistory As Boolean?

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

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="start_date", EmitDefaultValue:=False)>
        Public Property StartDate As String

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="end_date", EmitDefaultValue:=False)>
        Public Property EndDate As String

        ''' <summary>
        ''' Output addresses and directions in the original optimization request sequence.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="original", EmitDefaultValue:=False)>
        Public Property Original As Boolean?

        ''' <summary>
        ''' Output route and stop-specific notes. The notes will have timestamps, note types, and geospatial information if available
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="notes", EmitDefaultValue:=False)>
        Public Property Notes As Boolean?

        ''' <summary>
        ''' Search query
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)> _
        Public Property Query As String

        ''' <summary>
        ''' Updating a route supports the reoptimize=1 parameter, which reoptimizes only that route. Also supports the parameters from GET.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="reoptimize", EmitDefaultValue:=False)>
        Public Property ReOptimize As Boolean?

        ''' <summary>
        ''' For /api.v3/route/reoptimize_2.php endpoint. If equal to 0, optimization is enabled
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="disable_optimization", EmitDefaultValue:=False)>
        Public Property DisableOptimization As Boolean?

        ''' <summary>
        ''' If true will be redirected
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)>
        Public Property Redirect As Boolean?

        ''' <summary>
        ''' If true, the address bundling info Is included into route response.
        ''' </summary>
        <IgnoreDataMember()>
        <HttpQueryMemberAttribute(Name:="bundling_items", EmitDefaultValue:=False)>
        Public Property BundlingItems As Boolean?

        ''' <summary>
        ''' For /api.v3/route/reoptimize_2.php endpoint. "Distance", "Time"
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="optimize", EmitDefaultValue:=False)> _
        Public Property Optimize As String

        ''' <summary>
        ''' For /actions/route/share_route.php endpoint. "json", "xml"
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="response_format", EmitDefaultValue:=False)> _
        Public Property ResponseFormat As String

        ''' <summary>
        ''' By sending recompute_directions=1 we request that the route directions be recomputed (note that this does happen automatically if certain properties of the route are updated, such as stop sequence_no changes or round-tripness)
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="recompute_directions", EmitDefaultValue:=False)>
        Public Property RecomputeDirections As Boolean?

        ''' <summary>
        ''' Route Parameters to update.
        ''' (After a PUT there is no guarantee that the route_destination_id values are preserved! It may create copies resulting in new destination IDs, especially when dealing with multiple depots.)
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
        ''' If true, the route is approved for execution..
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