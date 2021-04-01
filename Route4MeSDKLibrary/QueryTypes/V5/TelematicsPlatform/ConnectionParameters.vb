Imports System

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' The telematics connection query parameters.
    ''' Used for create, update, get connectin(s).
    ''' </summary>
    Public NotInheritable Class ConnectionParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Telemetics connection type. See "Enum.TelematicsVendorType".
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vendor", EmitDefaultValue:=False)>
        Public Property Vendor As String

        ''' <summary>
        ''' Telemetics connection type ID
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vendor_id", EmitDefaultValue:=False)>
        Public Property VendorId As Integer?

        ''' <summary>
        ''' Telemetics connection name
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Telematics connection access host.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="host", EmitDefaultValue:=False)>
        Public Property Host As String

        ''' <summary>
        ''' Telematics connection access api_key.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="api_key", EmitDefaultValue:=False)>
        Public Property ApiKey As String

        ''' <summary>
        ''' Telematics connection access account_id.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="account_id", EmitDefaultValue:=False)>
        Public Property AccountId As String

        ''' <summary>
        ''' Telematics connection access username
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="username", EmitDefaultValue:=False)>
        Public Property UserName As String

        ''' <summary>
        ''' Telematics connection access password.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="password", EmitDefaultValue:=False)>
        Public Property Password As String

        ''' <summary>
        ''' Vehicle tracking interval in seconds (default value 60).
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vehicle_position_refresh_rate", EmitDefaultValue:=False)>
        Public Property VehiclePositionRefreshRate As Integer?

        ''' <summary>
        ''' Validate connections credentials.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="validate_remote_credentials", EmitDefaultValue:=False)>
        Public Property ValidateRemoteCredentials As Boolean?

        ''' <summary>
        ''' Disable/enable vehicle tracking.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="is_enabled", EmitDefaultValue:=False)>
        Public Property IsEnabled As Boolean?

        ''' <summary>
        ''' Metadata
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="metadata", EmitDefaultValue:=False)>
        Public Property Metadata As String

        ''' <summary>
        ''' Telematics connection access token.
        ''' Required to show specified connection.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="connection_token", EmitDefaultValue:=False)>
        Public Property ConnectionToken As String

    End Class
End Namespace
