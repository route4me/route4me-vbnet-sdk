Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response from the endpoint /vehicles/location
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleLocationResponse
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' An array of the vehicle locations
        ''' </summary>
        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property Data As VehicleLocationItem()
    End Class

    ''' <summary>
    ''' ehicle location data structure.
    ''' </summary>
    Public Class VehicleLocationItem
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' When a vehicle activity was detected.
        ''' </summary>
        <DataMember(Name:="activity_timestamp", EmitDefaultValue:=False)>
        Public Property ActivityTimestamp As Long?

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
