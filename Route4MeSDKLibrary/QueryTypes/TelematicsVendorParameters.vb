Namespace Route4MeSDK.QueryTypes

    ''' <summary>
    ''' Parameters for requesting the telematics vendors.
    ''' </summary>
    Public NotInheritable Class TelematicsVendorParameters
        Inherits GenericParameters

        ''' <summary>
        ''' An unique ID of a telematics vendor.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vendor_id", EmitDefaultValue:=False)>
        Public Property vendorID As UInteger?

        ''' <summary>
        ''' If equal to 1, the vendor is integrated in the Route4Me system.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="is_integrated", EmitDefaultValue:=False)>
        Public Property isIntegrated As UInteger?

        ''' <summary>
        ''' Current page in the vendors collection
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="page", EmitDefaultValue:=False)>
        Public Property Page As UInteger?

        ''' <summary>
        ''' Vendors number per page
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="per_page", EmitDefaultValue:=False)>
        Public Property perPage As UInteger?

        ''' <summary>
        ''' The vendor's country
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="country", EmitDefaultValue:=False)>
        Public Property Country As String

        ''' <summary>
        ''' A vendor's feature
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="feature", EmitDefaultValue:=False)>
        Public Property Feature As String

        ''' <summary>
        ''' A query string
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="search", EmitDefaultValue:=False)>
        Public Property Search As String

        ''' <summary>
        ''' Comma-delimited list of the vendors IDs
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vendors", EmitDefaultValue:=False)>
        Public Property Vendors As String

        ''' <summary>
        ''' Owner of a telematicss connection.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberID As UInteger?

        ''' <summary>
        ''' Is a user real or virtual
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="is_virtual", EmitDefaultValue:=False)>
        Public Property isVirtual As UInteger?

        ''' <summary>
        ''' API key
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="api_key", EmitDefaultValue:=False)>
        Public Property ApiKey As String

        ''' <summary>
        ''' If true, remote credentials validated.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="validate_remote_credentials", EmitDefaultValue:=False)>
        Public Property ValidateRemoteCredentials As Boolean?

        ''' <summary>
        ''' API token.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="api_token", EmitDefaultValue:=False)>
        Public Property ApiToken As String

    End Class
End Namespace
