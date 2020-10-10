Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class UserLocation
        Inherits GenericParameters

        ''' <summary>
        ''' Member data.
        ''' </summary>
        <DataMember(Name:="member_data")>
        Public Property MemberData As User

        ''' <summary>
        ''' User tracking.
        ''' </summary>
        <DataMember(Name:="tracking")>
        Public Property UserTracking As UserTracking

        ''' <summary>
        ''' If true, getting a location from the cache enabled.
        ''' </summary>
        <DataMember(Name:="from_cache", EmitDefaultValue:=False)>
        Public Property FromCache As Boolean?

    End Class

    Public Class UserTracking
        ''' <summary>
        ''' Position longitude
        ''' </summary>
        <DataMember(Name:="position_lng")>
        Public Property PositionLongitude As Double

        ''' <summary>
        ''' Position latitude
        ''' </summary>
        <DataMember(Name:="position_lat")>
        Public Property PositionLatitude As Double

        ''' <summary>
        ''' Movement direction in the degrees (north = 0, clockwise).
        ''' </summary>
        <DataMember(Name:="direction")>
        Public Property Direction As Integer?

        ''' <summary>
        ''' Data source name.
        ''' </summary>
        <DataMember(Name:="data_source_name")>
        Public Property DataSourceName As String

        ''' <summary>
        ''' Activity timestamp (EPOCH).
        ''' </summary>
        <DataMember(Name:="activity_timestamp")>
        Public Property ActivityTimestamp As Long?

        ''' <summary>
        ''' Device timestamp (EPOCH).
        ''' </summary>
        <DataMember(Name:="device_timestamp")>
        Public Property DeviceTimestamp As Long?

        ''' <summary>
        ''' Route ID.
        ''' </summary>
        <DataMember(Name:="route_id")>
        Public Property RouteId As String

        ''' <summary>
        ''' Device ID.
        ''' </summary>
        <DataMember(Name:="device_id")>
        Public Property DeviceId As String

        ''' <summary>
        ''' Vehicle movement speed.
        ''' </summary>
        <DataMember(Name:="speed")>
        Public Property Speed As Double?

        ''' <summary>
        ''' Vehicle movement speed.
        ''' </summary>
        <DataMember(Name:="altitude")>
        Public Property Altitude As Integer?

        ''' <summary>
        ''' Footsteps.
        ''' </summary>
        <DataMember(Name:="footsteps")>
        Public Property Footsteps As Integer?

        ''' <summary>
        '''User email.
        ''' </summary>
        <DataMember(Name:="custom_data")>
        Public Property CustomData As Dictionary(Of String, String)

        ''' <summary>
        ''' Device timezone (e.g. 'America/New_York').
        ''' </summary>
        <DataMember(Name:="device_timezone", EmitDefaultValue:=False)>
        Public Property DeviceTimezone As String

        ''' <summary>
        ''' Device timezone offset.
        ''' </summary>
        <DataMember(Name:="device_timezone_offset", EmitDefaultValue:=False)>
        Public Property DeviceTimezoneOffset As Integer?

        ''' <summary>
        ''' Vehicle ID.
        ''' </summary>
        <DataMember(Name:="vehicle_id_id")>
        Public Property VehicleId As String

        ''' <summary>
        ''' Day ID.
        ''' </summary>
        <DataMember(Name:="day_id", EmitDefaultValue:=False)>
        Public Property DayId As Integer?

        ''' <summary>
        ''' Device type.
        ''' </summary>
        <DataMember(Name:="device_type")>
        Public Property DeviceType As String

        ''' <summary>
        ''' Unique ID of the member inside the Route4Me system.
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Activity timestamp friendly.
        ''' </summary>
        <DataMember(Name:="activity_timestamp_friendly")>
        Public Property ActivityTimestampFriendly As String

        ''' <summary>
        ''' Timestamp of a last known location.
        ''' </summary>
        <DataMember(Name:="LAST_KNOWN", EmitDefaultValue:=False)>
        Public Property LastKnown As Long?
    End Class

End Namespace
