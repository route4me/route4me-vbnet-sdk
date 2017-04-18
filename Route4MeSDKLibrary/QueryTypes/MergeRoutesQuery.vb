Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization
Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Route parameters accepted by endpoints
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class MergeRoutesQuery
        Inherits GenericParameters
        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="route_ids", EmitDefaultValue:=False)> _
        Public Property RouteIds As String

        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="depot_address", EmitDefaultValue:=False)> _
        Public Property DepotAddress As String

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
