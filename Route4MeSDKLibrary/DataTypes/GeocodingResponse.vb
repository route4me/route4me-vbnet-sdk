Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Response from the address geocoding process
    ''' </summary>
    <DataContract>
    Public NotInheritable Class GeocodingResponse

        ''' <summary>
        ''' Address name
        ''' </summary>
        <DataMember(Name:="address", EmitDefaultValue:=False)>
        Public Property Address As String

        ''' <summary>
        ''' Latitude
        ''' </summary>
        <DataMember(Name:="lat", EmitDefaultValue:=False)>
        Public Property Lat As Double

        ''' <summary>
        ''' Longitude
        ''' </summary>
        <DataMember(Name:="lng", EmitDefaultValue:=False)>
        Public Property Lng As Double

        ''' <summary>
        ''' The address geocoding type. Available values:
        ''' <para>street_address, premise, locality, political,</para>
        ''' <para>postal_code, administrative_area_level_2, </para>
        ''' <para>political, political, administrative_area_level_1,</para>
        ''' <para>political, country, political</para>
        ''' </summary>
        <DataMember(Name:="type", EmitDefaultValue:=False)>
        Public Property Type As String

        ''' <summary>
        ''' Confidence level in the address geocoding.
        ''' <para>Available values: high, medium, low</para>
        ''' </summary>
        <DataMember(Name:="confidence", EmitDefaultValue:=False)>
        Public Property Confidence As String

        ''' <summary>
        ''' Original address string
        ''' </summary>
        <DataMember(Name:="original", EmitDefaultValue:=False)>
        Public Property Original As String

        ''' <summary>
        ''' Normalized address
        ''' </summary>
        <DataMember(Name:="normalized", EmitDefaultValue:=False)>
        Public Property Normalized As String

    End Class
End Namespace
