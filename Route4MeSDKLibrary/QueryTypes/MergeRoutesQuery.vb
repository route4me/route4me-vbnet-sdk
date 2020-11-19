Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Route parameters accepted by endpoints
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class MergeRoutesQuery
        Inherits GenericParameters
        ''' Don't serialize as JSON
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="route_ids", EmitDefaultValue:=False)>
        Public Property RouteIds As String

        ''' Where to merge routes (optional)
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="to_route_id", EmitDefaultValue:=False)>
        Public Property ToRouteId As String

        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="depot_address", EmitDefaultValue:=False)>
        Public Property DepotAddress As String

        ''' Depot ID
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As String

        ''' Comma-delimited list of the depot IDs.
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="route_destination_ids", EmitDefaultValue:=False)>
        Public Property RouteDestinationIDs As String

        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="remove_origin", EmitDefaultValue:=False)> _
        Public Property RemoveOrigin As Boolean

        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="depot_lat", EmitDefaultValue:=False)> _
        Public Property DepotLat As Double

        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="depot_lng", EmitDefaultValue:=False)> _
        Public Property DepotLng As Double

    End Class
End Namespace
