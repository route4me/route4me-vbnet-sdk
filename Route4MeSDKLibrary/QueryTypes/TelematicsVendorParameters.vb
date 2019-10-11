Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class TelematicsVendorParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="vendor_id", EmitDefaultValue:=False)>
        Public Property vendorID As UInteger?

        <HttpQueryMemberAttribute(Name:="is_integrated", EmitDefaultValue:=False)>
        Public Property isIntegrated As UInteger?

        <HttpQueryMemberAttribute(Name:="page", EmitDefaultValue:=False)>
        Public Property Page As UInteger?

        <HttpQueryMemberAttribute(Name:="per_page", EmitDefaultValue:=False)>
        Public Property perPage As UInteger?

        <HttpQueryMemberAttribute(Name:="country", EmitDefaultValue:=False)>
        Public Property Country As String

        <HttpQueryMemberAttribute(Name:="feature", EmitDefaultValue:=False)>
        Public Property Feature As String

        <HttpQueryMemberAttribute(Name:="search", EmitDefaultValue:=False)>
        Public Property Search As String

        <HttpQueryMemberAttribute(Name:="vendors", EmitDefaultValue:=False)>
        Public Property Vendors As String
    End Class
End Namespace
