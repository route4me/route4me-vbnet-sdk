Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class UserLocation
        Inherits GenericParameters

        ''' <summary>
        ''' Member data.
        ''' </summary>
        <DataMember(Name:="member_data", EmitDefaultValue:=False)>
        Public Property MemberData As User

        ''' <summary>
        ''' User tracking.
        ''' </summary>
        <DataMember(Name:="tracking", EmitDefaultValue:=False)>
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
        <DataMember(Name:="position_lng", EmitDefaultValue:=False)>
        Public Property PositionLongitude As Double

        ''' <summary>
        ''' Position latitude
        ''' </summary>
        <DataMember(Name:="position_lat", EmitDefaultValue:=False)>
        Public Property PositionLatitude As Double

        ''' <summary>
        ''' Movement direction in the degrees (north = 0, clockwise).
        ''' </summary>
        <DataMember(Name:="direction", EmitDefaultValue:=False)>
        Public Property Direction As Integer?

        ''' <summary>
        ''' Data source name.
        ''' </summary>
        <DataMember(Name:="data_source_name", EmitDefaultValue:=False)>
        Public Property DataSourceName As String

        ''' <summary>
        ''' Activity timestamp (EPOCH).
        ''' </summary>
        <DataMember(Name:="activity_timestamp", EmitDefaultValue:=False)>
        Public Property ActivityTimestamp As Long?

        ''' <summary>
        ''' Device timestamp (EPOCH).
        ''' </summary>
        <DataMember(Name:="device_timestamp", EmitDefaultValue:=False)>
        Public Property DeviceTimestamp As Long?

        ''' <summary>
        ''' Route ID.
        ''' </summary>
        <DataMember(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' Device ID.
        ''' </summary>
        <DataMember(Name:="device_id", EmitDefaultValue:=False)>
        Public Property DeviceId As String

        ''' <summary>
        ''' Vehicle movement speed.
        ''' </summary>
        <DataMember(Name:="speed", EmitDefaultValue:=False)>
        Public Property Speed As Double?

        ''' <summary>
        ''' Calculated speed
        ''' </summary>
        <DataMember(Name:="calculated_speed", EmitDefaultValue:=False)>
        Public Property CalculatedSpeed As String

        ''' <summary>
        ''' Speed Accuracy
        ''' </summary>
        <DataMember(Name:="speed_accuracy", EmitDefaultValue:=False)>
        Public Property SpeedAccuracy As String

        ''' <summary>
        ''' Speed Unit
        ''' </summary>
        <DataMember(Name:="speed_unit", EmitDefaultValue:=False)>
        Public Property SpeedUnit As String

        ''' <summary>
        ''' Bearing
        ''' </summary>
        <DataMember(Name:="bearing", EmitDefaultValue:=False)>
        Public Property Bearing As Integer?

        ''' <summary>
        ''' Bearing accuracy
        ''' </summary>
        <DataMember(Name:="bearing_accuracy", EmitDefaultValue:=False)>
        Public Property BearingAccuracy As String

        ''' <summary>
        ''' Accuracy
        ''' </summary>
        <DataMember(Name:="accuracy", EmitDefaultValue:=False)>
        Public Property Accuracy As String

        ''' <summary>
        ''' Vehicle movement speed.
        ''' </summary>
        <DataMember(Name:="altitude", EmitDefaultValue:=False)>
        Public Property Altitude As Integer?

        ''' <summary>
        ''' Footsteps.
        ''' </summary>
        <DataMember(Name:="footsteps", EmitDefaultValue:=False)>
        Public Property Footsteps As Integer?

        ''' <summary>
        '''User email.
        ''' </summary>
        <DataMember(Name:="custom_data", EmitDefaultValue:=False)>
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
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' Day ID.
        ''' </summary>
        <DataMember(Name:="day_id", EmitDefaultValue:=False)>
        Public Property DayId As Integer?

        ''' <summary>
        ''' Device type.
        ''' </summary>
        <DataMember(Name:="device_type", EmitDefaultValue:=False)>
        Public Property DeviceType As String

        ''' <summary>
        ''' Unique ID of the member inside the Route4Me system.
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Root member ID.
        ''' </summary>
        <DataMember(Name:="root_member_id", EmitDefaultValue:=False)>
        Public Property RootMemberId As Integer?

        ''' <summary>
        ''' Activity timestamp friendly.
        ''' </summary>
        <DataMember(Name:="activity_timestamp_friendly", EmitDefaultValue:=False)>
        Public Property ActivityTimestampFriendly As String

        ''' <summary>
        ''' Timestamp of a last known location.
        ''' </summary>
        <DataMember(Name:="LAST_KNOWN", EmitDefaultValue:=False)>
        Public Property LastKnown As Long?
    End Class

End Namespace
