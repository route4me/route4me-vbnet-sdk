Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Telematics vendors list item data structure.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class TelematicsVendors
        ''' <summary>
        ''' Vendor ID
        ''' </summary>
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As String

        ''' <summary>
        ''' Vendor name
        ''' </summary>
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Vendor slug
        ''' </summary>
        <DataMember(Name:="slug", EmitDefaultValue:=False)>
        Public Property Slug As String

        ''' <summary>
        ''' Description of the telematics vendor
        ''' </summary>
        <DataMember(Name:="description", EmitDefaultValue:=False)>
        Public Property Description As String

        ''' <summary>
        ''' URL to the telematics vendor's logo.
        ''' </summary>
        <DataMember(Name:="logo_url", EmitDefaultValue:=False)>
        Public Property logoURL As String

        ''' <summary>
        ''' URL to the telematics vendor's website
        ''' </summary>
        <DataMember(Name:="website_url", EmitDefaultValue:=False)>
        Public Property WebsiteURL As String

        ''' <summary>
        ''' URL to the telematics vendor's website.
        ''' </summary>
        <DataMember(Name:="api_docs_url", EmitDefaultValue:=False)>
        Public Property ApiDocsURL As String

        ''' <summary>
        ''' Whether, the vendor is or not integrated into the Route4Me system.
        ''' </summary>
        <DataMember(Name:="is_integrated", EmitDefaultValue:=False)>
        Public Property isIntegrated As String

        ''' <summary>
        ''' Telematics vendor size (e.g. 'global', regional', 'local')
        ''' </summary>
        <DataMember(Name:="size", EmitDefaultValue:=False)>
        Public Property Size As String
    End Class
End Namespace
