Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Subcalss of the Address class. See <see cref="Address.Geocodings"/>
    ''' </summary>
    <DataContract>
    Public NotInheritable Class Geocoding
        ''' <summary>
        ''' A unique identifier for the geocoding
        ''' </summary>
        <DataMember(Name:="key", EmitDefaultValue:=False)>
        Public Property Key As String

        ''' <summary>
        ''' Specific description of the geocoding result
        ''' </summary>
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Boundary box
        ''' </summary>
        <DataMember(Name:="bbox", EmitDefaultValue:=False)>
        Public Property Bbox As Double()

        ''' <summary>
        ''' The latitude of the geocoded address
        ''' </summary>
        <DataMember(Name:="lat", EmitDefaultValue:=False)>
        Public Property Latitude As Double

        ''' <summary>
        ''' The longitude of the geocoded address
        ''' </summary>
        <DataMember(Name:="lng", EmitDefaultValue:=False)>
        Public Property Longtude As Double

        ''' <summary>
        ''' Confidance level in the address geocoding:
        ''' <para>high, medium, low</para>
        ''' </summary>
        <DataMember(Name:="confidence", EmitDefaultValue:=False)>
        Public Property Confidence As String

        ''' <summary>
        ''' The postal code of the geocoded address
        ''' </summary>
        <DataMember(Name:="postalCode", EmitDefaultValue:=False)>
        Public Property PostalCode As String

        ''' <summary>
        ''' Country region
        ''' </summary>
        <DataMember(Name:="countryRegion", EmitDefaultValue:=False)>
        Public Property CountryRegion As String

        ''' <summary>
        ''' The address curbside coordinates
        ''' </summary>
        <DataMember(Name:="curbside_coordinates", EmitDefaultValue:=False)>
        Public Property CurbsideCoordinates As GeoPoint

        ''' <summary>
        ''' The address without number
        ''' </summary>
        <DataMember(Name:="address_without_number", EmitDefaultValue:=False)>
        Public Property AaddressWithoutNumber As String

        ''' <summary>
        ''' The place ID
        ''' </summary>
        <DataMember(Name:="place_id", EmitDefaultValue:=False)>
        Public Property PlaceID As String
    End Class
End Namespace

