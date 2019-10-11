Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class GeocodingParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="addresses", EmitDefaultValue:=False)>
        Public Property Addresses As String

        <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)>
        Public Property Format As String

        <HttpQueryMemberAttribute(Name:="pk", EmitDefaultValue:=False)>
        Public Property Pk As Integer

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As Integer

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As Integer

        <HttpQueryMemberAttribute(Name:="zipcode", EmitDefaultValue:=False)>
        Public Property Zipcode As String

        <HttpQueryMemberAttribute(Name:="housenumber", EmitDefaultValue:=False)> _
        Public Property Housenumber() As String

    End Class
End Namespace