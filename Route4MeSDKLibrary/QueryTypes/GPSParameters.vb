Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Helper class, for setting GPS data
    ''' Used to create the suitable query string
    ''' See example in Route4MeExamples.SetGPSPosition()
    ''' </summary>
    Public NotInheritable Class GPSParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="format")>
        Public Property Format As String

        <HttpQueryMemberAttribute(Name:="member_id")>
        Public Property MemberId As Integer

        <HttpQueryMemberAttribute(Name:="route_id")>
        Public Property RouteId As String

        <HttpQueryMemberAttribute(Name:="tx_id")>
        Public Property TxId As String

        <HttpQueryMemberAttribute(Name:="vehicle_id")>
        Public Property VehicleId As Integer

        <HttpQueryMemberAttribute(Name:="course")>
        Public Property Course As Integer

        <HttpQueryMemberAttribute(Name:="speed")>
        Public Property Speed As Double

        <HttpQueryMemberAttribute(Name:="lat")>
        Public Property Latitude As Double

        <HttpQueryMemberAttribute(Name:="lng")>
        Public Property Longitude As Double

        <HttpQueryMemberAttribute(Name:="last_position")>
        Public Property last_position As Boolean

        <HttpQueryMemberAttribute(Name:="time_period")>
        Public Property TimePeriod As String

        <HttpQueryMemberAttribute(Name:="start_date")>
        Public Property StartDate As Long

        <HttpQueryMemberAttribute(Name:="end_date")>
        Public Property EndDate As Long

        <HttpQueryMemberAttribute(Name:="altitude", EmitDefaultValue:=False)>
        Public Property Altitude As Double

        <HttpQueryMemberAttribute(Name:="device_type")>
        Public Property DeviceType As String

        <HttpQueryMemberAttribute(Name:="device_guid")>
        Public Property DeviceGuid As String

        <HttpQueryMemberAttribute(Name:="device_timestamp", EmitDefaultValue:=False)>
        Public Property DeviceTimestamp As String

        <HttpQueryMemberAttribute(Name:="app_version", EmitDefaultValue:=False)>
        Public Property AppVersion As String

    End Class
End Namespace
