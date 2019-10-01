Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class FindAssetResponse
        <DataMember(Name:="tracking_number")>
        Public Property TrackingNumber As String

        <DataMember(Name:="large_logo_uri")>
        Public Property LargeLogoUri As String

        <DataMember(Name:="mobile_logo_uri")>
        Public Property MobileLogoUri As String

        <DataMember(Name:="map_color")>
        Public Property MapColor As String

        <DataMember(Name:="large_logo_alignment")>
        Public Property LargeLogoAlignment As String

        <DataMember(Name:="mobile_logo_alignment")>
        Public Property MobileLogoAlignment As String

        <DataMember(Name:="show_map_zoom_controls")>
        Public Property ShowMapZoomControls As Boolean?

        <DataMember(Name:="driver_phone")>
        Public Property DriverPhone As String

        <DataMember(Name:="route_started")>
        Public Property RouteStarted As Boolean?

        <DataMember(Name:="customer_service_phone")>
        Public Property CustomerServicePhone As String

        <DataMember(Name:="driver_name")>
        Public Property DriverName As String

        <DataMember(Name:="driver_picture")>
        Public Property DriverPicture As String

        <DataMember(Name:="tracking_page_subheadline")>
        Public Property TrackingPageSubheadline As String

        <DataMember(Name:="destination_address_1")>
        Public Property DestinationAddress1 As String

        <DataMember(Name:="destination_address_2")>
        Public Property DestinationAddress2 As String

        <DataMember(Name:="status_history")>
        Public Property StatusHistory As AssetStatusHistory()

        <DataMember(Name:="locations")>
        Public Property Locations As FindAssetResponseLocations()

        <DataMember(Name:="custom_data", EmitDefaultValue:=False)>
        Public Property CustomData As Dictionary(Of String, String)

        <DataMember(Name:="arrival")>
        Public Property Arrival As FindAssetResponseArrival()

        <DataMember(Name:="delivered", EmitDefaultValue:=False)>
        Public Property Delivered As Boolean?

        <DataMember(Name:="timestamp_geofence_visited")>
        Public Property TimestampGeofenceVisited As Integer?

        <DataMember(Name:="timestamp_last_visited")>
        Public Property TimestampLastVisited As Integer?
    End Class

    <DataContract> _
    Public NotInheritable Class FindAssetResponseLocations
        <DataMember(Name:="is_destination")>
        Public Property IsDestination As Boolean?

        <DataMember(Name:="lat")>
        Public Property Latitude As Double

        <DataMember(Name:="lng")>
        Public Property Longitude As Double

        <DataMember(Name:="icon")>
        Public Property Icon As String

        <DataMember(Name:="size")>
        Public Property Size As Integer?

        <DataMember(Name:="anchor")>
        Public Property Anchor As Integer()

        <DataMember(Name:="popupAnchor")>
        Public Property PopupAnchor As Integer()

        <DataMember(Name:="angle")>
        Public Property Angle As Integer?

        <DataMember(Name:="info")>
        Public Property Info As String
    End Class

    <DataContract>
    Public NotInheritable Class FindAssetResponseArrival
        <DataMember(Name:="from_unix_timestamp")>
        Public Property FromUnixTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="to_unix_timestamp")>
        Public Property ToUnixTimestamp As System.Nullable(Of Integer)
    End Class

    <DataContract()>
    Public NotInheritable Class AssetStatusHistory
        <DataMember(Name:="unix_timestamp")>
        Public Property UnixTimestamp As Integer?
        <DataMember(Name:="info")>
        Public Property Info As String
    End Class
End Namespace