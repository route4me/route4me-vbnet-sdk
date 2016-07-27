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
        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId() As String
            Get
                Return m_RouteId
            End Get
            Set(value As String)
                m_RouteId = Value
            End Set
        End Property
        Private m_RouteId As String

        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
        Public Property RouteDestinationId() As System.Nullable(Of Integer)
            Get
                Return m_RouteDestinationId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_RouteDestinationId = value
            End Set
        End Property
        Private m_RouteDestinationId As System.Nullable(Of Integer)

        ''' <summary>
        '''  	Pass True to return directions and the route path
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="directions", EmitDefaultValue:=False)> _
        Public Property Directions() As System.Nullable(Of Boolean)
            Get
                Return m_Directions
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Directions = Value
            End Set
        End Property
        Private m_Directions As System.Nullable(Of Boolean)

        ''' <summary>
        ''' "None" - no path output. "Points" - points path output
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="route_path_output", EmitDefaultValue:=False)> _
        Public Property RoutePathOutput() As String
            Get
                Return m_RoutePathOutput
            End Get
            Set(value As String)
                m_RoutePathOutput = Value
            End Set
        End Property
        Private m_RoutePathOutput As String

        ''' <summary>
        ''' Output route tracking data in response
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="device_tracking_history", EmitDefaultValue:=False)> _
        Public Property DeviceTrackingHistory() As System.Nullable(Of Boolean)
            Get
                Return m_DeviceTrackingHistory
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_DeviceTrackingHistory = Value
            End Set
        End Property
        Private m_DeviceTrackingHistory As System.Nullable(Of Boolean)

        ''' <summary>
        ''' The number of existing routes that should be returned per response when looking at a list of all the routes.
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
        Public Property Limit() As System.Nullable(Of UInteger)
            Get
                Return m_Limit
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Limit = Value
            End Set
        End Property
        Private m_Limit As System.Nullable(Of UInteger)

        ''' <summary>
        ''' The page number for route listing pagination. Increment the offset by the limit number to move to the next page.
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
        Public Property Offset() As System.Nullable(Of UInteger)
            Get
                Return m_Offset
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Offset = Value
            End Set
        End Property
        Private m_Offset As System.Nullable(Of UInteger)

        ''' <summary>
        ''' Output addresses and directions in the original optimization request sequence. This is to allow us to compare routes before & after optimization.
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="original", EmitDefaultValue:=False)> _
        Public Property Original() As System.Nullable(Of Boolean)
            Get
                Return m_Original
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Original = Value
            End Set
        End Property
        Private m_Original As System.Nullable(Of Boolean)

        ''' <summary>
        ''' Output route and stop-specific notes. The notes will have timestamps, note types, and geospatial information if available
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="notes", EmitDefaultValue:=False)> _
        Public Property Notes() As System.Nullable(Of Boolean)
            Get
                Return m_Notes
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Notes = Value
            End Set
        End Property
        Private m_Notes As System.Nullable(Of Boolean)

        ''' <summary>
        ''' Search query
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)> _
        Public Property Query() As String
            Get
                Return m_Query
            End Get
            Set(value As String)
                m_Query = Value
            End Set
        End Property
        Private m_Query As String

        ''' <summary>
        ''' Updating a route supports the reoptimize=1 parameter, which reoptimizes only that route. Also supports the parameters from GET.
        ''' </summary>
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
        ''' For /api.v3/route/reoptimize_2.php endpoint. If equal to 0, optimization is enabled
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="disable_optimization", EmitDefaultValue:=False)> _
        Public Property DisableOptimization() As System.Nullable(Of Boolean)
            Get
                Return m_DisableOptimization
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_DisableOptimization = value
            End Set
        End Property
        Private m_DisableOptimization As System.Nullable(Of Boolean)

        ''' <summary>
        ''' For /api.v3/route/reoptimize_2.php endpoint. "Distance", "Time"
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="optimize", EmitDefaultValue:=False)> _
        Public Property Optimize() As String
            Get
                Return m_Optimize
            End Get
            Set(value As String)
                m_Optimize = value
            End Set
        End Property
        Private m_Optimize As String

        ''' <summary>
        ''' By sending recompute_directions=1 we request that the route directions be recomputed (note that this does happen automatically if certain properties of the route are updated, such as stop sequence_no changes or round-tripness)
        ''' </summary>
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="recompute_directions", EmitDefaultValue:=False)> _
        Public Property RecomputeDirections() As System.Nullable(Of Boolean)
            Get
                Return m_RecomputeDirections
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_RecomputeDirections = Value
            End Set
        End Property
        Private m_RecomputeDirections As System.Nullable(Of Boolean)

        ''' <summary>
        ''' Route Parameters to update.
        ''' (After a PUT there is no guarantee that the route_destination_id values are preserved! It may create copies resulting in new destination IDs, especially when dealing with multiple depots.)
        ''' </summary>
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
    End Class
End Namespace