Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Vehicle search parameters.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleSearchParameters
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' An array of the vehicle IDs.
        ''' </summary>
        <DataMember(Name:="vehicle_ids", EmitDefaultValue:=False)>
        Public Property VehicleIDs As String()

        ''' <summary>
        ''' Latitude of a vehicle position.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <DataMember(Name:="lat", EmitDefaultValue:=False)>
        Public Property Latitude As Double?

        ''' <summary>
        ''' Longitude of a vehicle position.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <DataMember(Name:="lng", EmitDefaultValue:=False)>
        Public Property Longitude As Double?

    End Class
End Namespace