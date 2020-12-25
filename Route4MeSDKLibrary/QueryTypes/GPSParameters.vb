Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Helper class, for setting GPS data
    ''' Used to create the suitable query string
    ''' See example in Route4MeExamples.SetGPSPosition()
    ''' </summary>
    Public NotInheritable Class GPSParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Response format.
        ''' <para>Available values: <value>json, xml</value></para>
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)>
        Public Property Format As String

        ''' <summary>
        ''' Unique ID of a member.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Unique ID of a route the device assigned to.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        ''' <summary>
        ''' Unique ID of a GPS points group.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="tx_id", EmitDefaultValue:=False)>
        Public Property TxId As String

        ''' <summary>
        ''' Unique ID of a vehicle.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' The direction in degrees in which the vehicle is heading
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="course", EmitDefaultValue:=False)>
        Public Property Course As Integer?

        ''' <summary>
        ''' Vehicle speed.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="speed", EmitDefaultValue:=False)>
        Public Property Speed As Double?

        ''' <summary>
        ''' Latitude of a device position.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="lat", EmitDefaultValue:=False)>
        Public Property Latitude As Double?

        ''' <summary>
        ''' Longitude of a device position.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="lng", EmitDefaultValue:=False)>
        Public Property Longitude As Double?

        ''' <summary>
        ''' If true, returns a response with a last known position of a device.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="last_position", EmitDefaultValue:=False)>
        Public Property last_position As Boolean?

        ''' <summary>
        ''' If equal to 'custom' a time filter will work.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="time_period", EmitDefaultValue:=False)>
        Public Property TimePeriod As String

        ''' <summary>
        ''' Start of a time filter.
        ''' <remarks><para>Time format: UNIX timestamp.</para>
        ''' <para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="start_date", EmitDefaultValue:=False)>
        Public Property StartDate As Long?

        ''' <summary>
        ''' End of a time filter.
        ''' <remarks><para>Time format: UNIX timestamp.</para>
        ''' <para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="end_date", EmitDefaultValue:=False)>
        Public Property EndDate As Long?

        ''' <summary>
        ''' Altitude of a device position.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="altitude", EmitDefaultValue:=False)>
        Public Property Altitude As Double?

        ''' <summary>
        ''' Device type.
        ''' <para>Available values </para>
        ''' <value>'web', 'iphone', 'ipad', 'android phone', 'android tablet' etc</value>
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="device_type", EmitDefaultValue:=False)>
        Public Property DeviceType As String

        ''' <summary>
        ''' Globally unique identifier of a device.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="device_guid", EmitDefaultValue:=False)>
        Public Property DeviceGuid As String

        ''' <summary>
        ''' The timestamp on the local (remote relative to the server) device.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="device_timestamp", EmitDefaultValue:=False)>
        Public Property DeviceTimestamp As String

        ''' <summary>
        ''' The version of the app submitting the data.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="app_version", EmitDefaultValue:=False)>
        Public Property AppVersion As String

    End Class
End Namespace
