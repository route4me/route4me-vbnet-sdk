Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' The response structure from the endpoint /connections
    ''' </summary>
    <DataContract>
    Public NotInheritable Class TelematicsConnection
        Inherits GenericParameters

        ''' <summary>
        ''' Telematics connection access account_id
        ''' </summary>
        <DataMember(Name:="account_id", EmitDefaultValue:=False)>
        Public Property AccountId As String

        ''' <summary>
        ''' Telematics connection access username
        ''' </summary>
        <DataMember(Name:="username", EmitDefaultValue:=False)>
        Public Property UserName As String

        ''' <summary>
        ''' Telematics connection access password
        ''' </summary>
        <DataMember(Name:="password", EmitDefaultValue:=False)>
        Public Property Password As String

        ''' <summary>
        ''' Telematics connection access host
        ''' </summary>
        <DataMember(Name:="host", EmitDefaultValue:=False)>
        Public Property Host As String

        ''' <summary>
        ''' Telematics connection access api_key
        ''' </summary>
        <DataMember(Name:="api_key", EmitDefaultValue:=False)>
        Public Property ApiKey As String

        ''' <summary>
        ''' Telemetics connection type ID
        ''' </summary>
        <DataMember(Name:="vendor_id", EmitDefaultValue:=False)>
        Public Property VendorId As Integer?

        ''' <summary>
        ''' elemetics connection name
        ''' </summary>
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Vehicle tracking interval in seconds
        ''' </summary>
        <DataMember(Name:="vehicle_position_refresh_rate", EmitDefaultValue:=False)>
        Public Property VehiclePositionRefreshRate As Integer?

        ''' <summary>
        ''' Maximum idle time
        ''' </summary>
        <DataMember(Name:="max_idle_time", EmitDefaultValue:=False)>
        Public Property MaxIdleTime As Integer?

        ''' <summary>
        ''' Disable/enable vehicle tracking
        ''' </summary>
        <DataMember(Name:="is_enabled", EmitDefaultValue:=False)>
        Public Property IsEnabled As Boolean?

        ''' <summary>
        ''' The last timestamp the vehicles reloaded
        ''' </summary>
        <DataMember(Name:="last_vehicles_reload", EmitDefaultValue:=False)>
        Public Property LastVehiclesReload As String

        ''' <summary>
        ''' The last timestamp the addresses reloaded
        ''' </summary>
        <DataMember(Name:="last_addresses_reload", EmitDefaultValue:=False)>
        Public Property LastAddressesReload As String

        ''' <summary>
        ''' The last timestamp the postions reloaded
        ''' </summary>
        <DataMember(Name:="last_position_reload", EmitDefaultValue:=False)>
        Public Property LastPositionReload As String

        ''' <summary>
        ''' Telematics connection access token
        ''' </summary>
        <DataMember(Name:="connection_token", EmitDefaultValue:=False)>
        Public Property ConnectionToken As String

        ''' <summary>
        ''' Connection user ID
        ''' </summary>
        <DataMember(Name:="user_id", EmitDefaultValue:=False)>
        Public Property UserId As Integer?

        ''' <summary>
        ''' When the connection updated
        ''' </summary>
        <DataMember(Name:="updated_at", EmitDefaultValue:=False)>
        Public Property UpdatedAt As String

        ''' <summary>
        ''' When the connection created
        ''' </summary>
        <DataMember(Name:="created_at", EmitDefaultValue:=False)>
        Public Property CreatedAt As String

        ''' <summary>
        ''' Telemetics connection ID
        ''' </summary>
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property Id As Integer?

        ''' <summary>
        ''' Metadata, custom key-value storage.
        ''' </summary>
        <DataMember(Name:="metadata", EmitDefaultValue:=False)>
        Public Property Metadata As Dictionary(Of String, String)

        ''' <summary>
        ''' Total vehicles count
        ''' </summary>
        <DataMember(Name:="total_vehicles_count", EmitDefaultValue:=False)>
        Public Property TotalVehiclesCount As Integer?

        ''' <summary>
        ''' Total addresses count
        ''' </summary>
        <DataMember(Name:="total_addresses_count", EmitDefaultValue:=False)>
        Public Property TotalAddressesCount As Integer?

        ''' <summary>
        ''' Syncronized vehicles count
        ''' </summary>
        <DataMember(Name:="synced_vehicles_count", EmitDefaultValue:=False)>
        Public Property SyncedVehiclesCount As Integer?

        ''' <summary>
        ''' Telemetics connection vendor <see cref="Enum.TelematicsVendorType" />
        ''' </summary>
        <DataMember(Name:="vendor", EmitDefaultValue:=False)>
        Public Property Vendor As String
    End Class
End Namespace