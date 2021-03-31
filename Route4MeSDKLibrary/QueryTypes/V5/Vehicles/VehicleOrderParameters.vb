Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Parameters for execution of a vehicle order.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleOrderParameters
        Inherits GenericParameters

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

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