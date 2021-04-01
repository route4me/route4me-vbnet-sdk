Imports System

Namespace Route4MeSDK.QueryTypes

    ''' <summary>
    ''' Parameters for requesting the telematics connection.
    ''' </summary>
    Public NotInheritable Class TelematicsConnectionParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Unique accont ID of a telematics connection.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="account_id", EmitDefaultValue:=False)>
        Public Property AccountId As String

        ''' <summary>
        ''' User name
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="username", EmitDefaultValue:=False)>
        Public Property UserName As String

        ''' <summary>
        ''' Password
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="password", EmitDefaultValue:=False)>
        Public Property Password As String

        ''' <summary>
        ''' Connection provider host
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="host", EmitDefaultValue:=False)>
        Public Property Host As String

        ''' <summary>
        ''' An unique ID of a telematics vendor.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vendor_id", EmitDefaultValue:=False)>
        Public Property VendorID As UInteger?

        ''' <summary>
        ''' Telematics connection name
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Vehicle tracking interval in seconds
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vehicle_position_refresh_rate", EmitDefaultValue:=False)>
        Public Property VehiclePositionRefreshRate As Integer?

        ''' <summary>
        ''' Connection token
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="connection_token", EmitDefaultValue:=False)>
        Public Property ConnectionToken As String

        ''' <summary>
        ''' Connection user ID
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="user_id", EmitDefaultValue:=False)>
        Public Property UserId As Integer?

        ''' <summary>
        ''' Connection ID
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As Integer?

        ''' <summary>
        ''' Telemetics connection type. See cref="Enum.TelematicsVendorType".
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vendor", EmitDefaultValue:=False)>
        Public Property Vendor As String

        ''' <summary>
        ''' Validate connections credentials.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="validate_remote_credentials", EmitDefaultValue:=False)>
        Public Property ValidateRemoteCredentials As Boolean?

    End Class
End Namespace
