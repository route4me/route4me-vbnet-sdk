Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Parameters for the address(es) geocoding request.
    ''' </summary>
    Public NotInheritable Class GeocodingParameters
        Inherits GenericParameters

        ''' <summary>
        ''' List of the addresses as a multiline text.
        ''' <remarks><para>The addresses are delimited with the newline character.</para>
        ''' <para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="addresses", EmitDefaultValue:=False)>
        Public Property Addresses As String

        ''' <summary>
        ''' Response format (xml, json).
        ''' Note: used in the forward And reverse geocodings as a url parameter.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)>
        Public Property Format As String

        ''' <summary>
        ''' Response export format.
        ''' <para>Availbale values: <value> json, xml, csv.</value></para>
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="strExportFormat", EmitDefaultValue:=False)>
        Public Property ExportFormat As String

        ''' <summary>
        ''' Rapis string data index.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="pk", EmitDefaultValue:=False)>
        Public Property Pk As Integer

        ''' <summary>
        ''' Only records from that offset will be considered.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As Integer

        ''' <summary>
        ''' Limit the number of records in response.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As Integer

        ''' <summary>
        ''' Zipcode.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="zipcode", EmitDefaultValue:=False)>
        Public Property Zipcode As String

        ''' <summary>
        ''' House number.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="housenumber", EmitDefaultValue:=False)>
        Public Property Housenumber() As String

    End Class
End Namespace