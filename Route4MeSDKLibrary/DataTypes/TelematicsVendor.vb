Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Telematics vendor's data structure.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class TelematicsVendor
        ''' <summary>
        ''' Unique ID of a telematics vendor.
        ''' </summary>
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As String

        ''' <summary>
        ''' Vendor name.
        ''' </summary>
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Vendor slug.
        ''' </summary>
        <DataMember(Name:="slug", EmitDefaultValue:=False)>
        Public Property Slug As String

        ''' <summary>
        ''' Vendor description.
        ''' </summary>
        <DataMember(Name:="description", EmitDefaultValue:=False)>
        Public Property Description As String

        ''' <summary>
        ''' URL to the telematics vendor's logo.
        ''' </summary>
        <DataMember(Name:="logo_url", EmitDefaultValue:=False)>
        Public Property LogoURL As String

        ''' <summary>
        ''' Website URL of a telematics vendor.
        ''' </summary>
        <DataMember(Name:="website_url", EmitDefaultValue:=False)>
        Public Property WebsiteURL As String

        ''' <summary>
        ''' API URL of a telematics vendor.
        ''' </summary>
        <DataMember(Name:="api_docs_url", EmitDefaultValue:=False)>
        Public Property ApiDocsURL As String

        ''' <summary>
        ''' Whether, the vendor is or not integrated into the Route4Me system.
        ''' </summary>
        <DataMember(Name:="is_integrated", EmitDefaultValue:=False)>
        Public Property IsIntegrated As String

        ''' <summary>
        ''' Vendors size.
        ''' <para>Accepted values</para>
        ''' <value>global, regional, local. </value>
        ''' </summary>
        <DataMember(Name:="size", EmitDefaultValue:=False)>
        Public Property Size As String

        ''' <summary>
        ''' An array the vendor features. See <see cref="TelematicsVendorFeature"/>.
        ''' </summary>
        <DataMember(Name:="features", EmitDefaultValue:=False)>
        Public Property Features As TelematicsVendorFeature()

        ''' <summary>
        ''' An array of the countries, the vendor is operating. See <see cref="Country"/>.
        ''' </summary>
        <DataMember(Name:="countries", EmitDefaultValue:=False)>
        Public Property Countries As Country()
    End Class
End Namespace
