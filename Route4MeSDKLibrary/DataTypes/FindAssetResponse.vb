Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' Response from the asset finding request
    ''' </summary>
    <DataContract>
    Public NotInheritable Class FindAssetResponse

        ''' <summary>
        ''' Tracking number
        ''' </summary>
        <DataMember(Name:="tracking_number")>
        Public Property TrackingNumber As String

        ''' <summary>
        ''' A link to a large logo
        ''' </summary>
        <DataMember(Name:="large_logo_uri")>
        Public Property LargeLogoUri As String

        ''' <summary>
        ''' A link to a large logo (2x)
        ''' </summary>
        <DataMember(Name:="large_logo_uri_2x")>
        Public Property LargeLogoUri2x As String

        ''' <summary>
        ''' A link to a mobile logo
        ''' </summary>
        <DataMember(Name:="mobile_logo_uri")>
        Public Property MobileLogoUri As String

        ''' <summary>
        ''' A link to a mobile logo (2x)
        ''' </summary>
        <DataMember(Name:="mobile_logo_uri_2x")>
        Public Property MobileLogoUri2x As String

        ''' <summary>
        ''' The asset color on a map
        ''' </summary>
        <DataMember(Name:="map_color")>
        Public Property MapColor As String

        ''' <summary>
        ''' An alignment of a large logo
        ''' </summary>
        <DataMember(Name:="large_logo_alignment")>
        Public Property LargeLogoAlignment As String

        ''' <summary>
        ''' An alignment of a mobile logo
        ''' </summary>
        <DataMember(Name:="mobile_logo_alignment")>
        Public Property MobileLogoAlignment As String

        ''' <summary>
        ''' Show map zoom controls
        ''' </summary>
        <DataMember(Name:="show_map_zoom_controls")>
        Public Property ShowMapZoomControls As Boolean?

        ''' <summary>
        ''' Driver phone number
        ''' </summary>
        <DataMember(Name:="driver_phone")>
        Public Property DriverPhone As String

        ''' <summary>
        ''' Route started timestamp
        ''' </summary>
        <DataMember(Name:="route_started")>
        Public Property RouteStarted As Boolean?

        ''' <summary>
        ''' Customer service phone
        ''' </summary>
        <DataMember(Name:="customer_service_phone")>
        Public Property CustomerServicePhone As String

        ''' <summary>
        ''' If true, Covid19 warning hidden
        ''' </summary>
        <DataMember(Name:="hide_covid19_warning")>
        Public Property HideCovid19Warning As String

        ''' <summary>
        ''' Driver name
        ''' </summary>
        <DataMember(Name:="driver_name")>
        Public Property DriverName As String

        ''' <summary>
        ''' A link to a driver picture file
        ''' </summary>
        <DataMember(Name:="driver_picture")>
        Public Property DriverPicture As String

        ''' <summary>
        ''' A subheadline of a tracking page
        ''' </summary>
        <DataMember(Name:="tracking_page_subheadline")>
        Public Property TrackingPageSubheadline As String

        ''' <summary>
        ''' A first destination address
        ''' </summary>
        <DataMember(Name:="destination_address_1")>
        Public Property DestinationAddress1 As String

        ''' <summary>
        ''' A second destination address
        ''' </summary>
        <DataMember(Name:="destination_address_2")>
        Public Property DestinationAddress2 As String

        ''' <summary>
        ''' Asset status history
        ''' </summary>
        <DataMember(Name:="status_history")>
        Public Property StatusHistory As AssetStatusHistory()

        ''' <summary>
        ''' An array of the asset locations.
        ''' </summary>
        <DataMember(Name:="locations")>
        Public Property Locations As FindAssetResponseLocations()

        ''' <summary>
        ''' Custom data
        ''' </summary>
        <DataMember(Name:="custom_data", EmitDefaultValue:=False)>
        Public Property CustomData As Dictionary(Of String, String)

        ''' <summary>
        ''' Arrival time of the asset.
        ''' </summary>
        <DataMember(Name:="arrival")>
        Public Property Arrival As FindAssetResponseArrival()

        ''' <summary>
        ''' True if the asset was delivered.
        ''' </summary>
        <DataMember(Name:="delivered", EmitDefaultValue:=False)>
        Public Property Delivered As Boolean?

        ''' <summary>
        ''' UNIX timestamp when a geofence visited event was triggered.
        ''' </summary>
        <DataMember(Name:="timestamp_geofence_visited")>
        Public Property TimestampGeofenceVisited As Long?

        ''' <summary>
        ''' UNIX timestamp of a last visited event.
        ''' </summary>
        <DataMember(Name:="timestamp_last_visited")>
        Public Property TimestampLastVisited As Long?

    End Class

    ''' <summary>
    ''' The subclass of the FindAssetResponse class.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class FindAssetResponseLocations

        ''' <summary>
        ''' If true, current location is destination.
        ''' </summary>
        <DataMember(Name:="is_destination")>
        Public Property IsDestination As Boolean?

        ''' <summary>
        ''' Latitude.
        ''' </summary>
        <DataMember(Name:="lat")>
        Public Property Latitude As Double

        ''' <summary>
        ''' Longitude.
        ''' </summary>
        <DataMember(Name:="lng")>
        Public Property Longitude As Double

        ''' <summary>
        ''' The asset's location icon.
        ''' </summary>
        <DataMember(Name:="icon")>
        Public Property Icon As String

        ''' <summary>
        ''' Size of the icon.
        ''' </summary>
        <DataMember(Name:="size")>
        Public Property Size As Integer?

        ''' <summary>
        ''' A icon's acnhor position.
        ''' </summary>
        <DataMember(Name:="anchor")>
        Public Property Anchor As Integer()

        ''' <summary>
        ''' Popup position of an icon. 
        ''' </summary>
        <DataMember(Name:="popupAnchor")>
        Public Property PopupAnchor As Integer()

        ''' <summary>
        ''' Rotation angle.
        ''' </summary>
        <DataMember(Name:="angle")>
        Public Property Angle As Integer?

        ''' <summary>
        ''' Information about a shipped package at a specified location.
        ''' </summary>
        <DataMember(Name:="info")>
        Public Property Info As String

    End Class

    ''' <summary>
    ''' The subclass of the FindAssetResponse class.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class FindAssetResponseArrival

        ''' <summary>
        ''' Start of the arrival time
        ''' </summary>
        <DataMember(Name:="from_unix_timestamp")>
        Public Property FromUnixTimestamp As Long?

        ''' <summary>
        ''' End of the arrival time
        ''' </summary>
        <DataMember(Name:="to_unix_timestamp")>
        Public Property ToUnixTimestamp As Long?

    End Class

    ''' <summary>
    ''' The subclass of the FindAssetResponse class
    ''' </summary>
    <DataContract()>
    Public NotInheritable Class AssetStatusHistory

        ''' <summary>
        ''' Status getting timestamp
        ''' </summary>
        <DataMember(Name:="unix_timestamp")>
        Public Property UnixTimestamp As Long?

        ''' <summary>
        ''' nformation about a shipped package.
        ''' enum: ["Order Received", "Order Assigned to Route", "Packing", "Loaded to Vehicle", "Out for Delivery"]
        ''' </summary>
        <DataMember(Name:="info")>
        Public Property Info As String

    End Class
End Namespace