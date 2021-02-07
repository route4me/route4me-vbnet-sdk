Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' The response from the route delete process.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class RouteDuplicateResponse
        Inherits GenericParameters

        ''' <summary>
        ''' If true, the route duplicated successfully.
        ''' </summary>
        <DataMember(Name:="status", EmitDefaultValue:=False)>
        Public Property Status As Boolean

        ''' <summary>
        ''' If true, the route duplication process was asynchronous.
        ''' </summary>
        <DataMember(Name:="async", EmitDefaultValue:=False)>
        Public Property Async As Boolean?

        ''' <summary>
        ''' An array of the duplicated route IDs.
        ''' </summary>
        <DataMember(Name:="route_ids", EmitDefaultValue:=False)>
        Public Property RouteIDs As String()

    End Class

End Namespace

