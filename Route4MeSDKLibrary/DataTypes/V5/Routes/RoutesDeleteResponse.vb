
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' The response from the endpoint R4MEInfrastructureSettingsV5.RoutesDuplicate.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class RoutesDeleteResponse
        Inherits GenericParameters

        ''' <summary>
        ''' If true, the route duplicated successfully.
        ''' </summary>
        <DataMember(Name:="deleted", EmitDefaultValue:=False)>
        Public Property Deleted As Boolean

        ''' <summary>
        ''' If true, the route duplication process was asynchronous.
        ''' </summary>
        <DataMember(Name:="async", EmitDefaultValue:=False)>
        Public Property Async As Boolean?

        ''' <summary>
        ''' Route ID
        ''' </summary>
        <DataMember(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' An array of the duplicated route IDs.
        ''' </summary>
        <DataMember(Name:="route_ids", EmitDefaultValue:=False)>
        Public Property RouteIDs As String()


    End Class

End Namespace
