Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response from the endpoint /vehicles/{id}/track
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleTrackResponse
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' An array of the vehicle locations
        ''' </summary>
        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property Data As VehicleTrackItem()

    End Class

    ''' <summary>
    ''' Vehicle track data structure.
    ''' </summary>
    Public Class VehicleTrackItem
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' The member ID
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

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

        ''' <summary>
        ''' The geographic altitude
        ''' </summary>
        <DataMember(Name:="altitude", EmitDefaultValue:=False)>
        Public Property Altitude As Integer?

        ''' <summary>
        ''' Vehicle speed
        ''' </summary>
        <DataMember(Name:="speed", EmitDefaultValue:=False)>
        Public Property Speed As Integer?

        ''' <summary>
        ''' When a vehicle activity was detected.
        ''' </summary>
        <DataMember(Name:="timestamp", EmitDefaultValue:=False)>
        Public Property Timestamp As Long?

    End Class
End Namespace
