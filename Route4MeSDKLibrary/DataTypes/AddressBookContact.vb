Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Address book contact class. 
    ''' <para>See the JSON schema at "https://github.com/route4me/route4me-json-schemas/blob/master/Addressbook_v4.dtd" </para>
    ''' <para>Note: 'contact' means 'address book contact' and 'address' means 'geographic address of the contact'.</para>
    ''' </summary>
    <DataContract>
    Public NotInheritable Class AddressBookContact
        Inherits GenericParameters
        ''' <summary>
        ''' A territory shape name the contact belongs.
        ''' </summary>
        <DataMember(Name:="territory_name", EmitDefaultValue:=False)>
        Public Property territory_name As String

        ''' <summary>
        ''' Time when the contact was created.
        ''' </summary>
        <DataMember(Name:="created_timestamp", EmitDefaultValue:=False)>
        Public Property created_timestamp As Long?

        ''' <summary>
        ''' Unique ID of the contact.
        ''' </summary>
        <DataMember(Name:="address_id", EmitDefaultValue:=False)>
        Public Property address_id As Integer?

        ''' <summary>
        ''' The geographic address of the contact.
        ''' </summary>
        <DataMember(Name:="address_1")>
        Public Property address_1 As String

        ''' <summary>
        ''' Second geographic address of the contact.
        ''' </summary>
        <DataMember(Name:="address_2", EmitDefaultValue:=False)>
        Public Property address_2 As String

        ''' <summary>
        ''' Unique ID of the member.
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property member_id As Integer?

        ''' <summary>
        ''' The contact's alias.
        ''' </summary>
        <DataMember(Name:="address_alias", EmitDefaultValue:=False)>
        Public Property address_alias As String

        ''' <summary>
        ''' A group the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_group", EmitDefaultValue:=False)>
        Public Property address_group As String

        ''' <summary>
        ''' The first name of the contact person.
        ''' </summary>
        <DataMember(Name:="first_name", EmitDefaultValue:=False)>
        Public Property first_name As String

        ''' <summary>
        ''' The last name of the contact person.
        ''' </summary>
        <DataMember(Name:="last_name", EmitDefaultValue:=False)>
        Public Property last_name As String

        ''' <summary>
        ''' Start of the contact's local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_start", EmitDefaultValue:=False)>
        Public Property local_time_window_start As Long?

        ''' <summary>
        ''' End of the contact's local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_end", EmitDefaultValue:=False)>
        Public Property local_time_window_end As Long?

        ''' <summary>
        ''' Start of the contact's second local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_start_2", EmitDefaultValue:=False)>
        Public Property local_time_window_start_2 As Long?

        ''' <summary>
        ''' End of the contact's second local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_end_2", EmitDefaultValue:=False)>
        Public Property local_time_window_end_2 As Long?

        ''' <summary>
        ''' The contact's email.
        ''' </summary>
        <DataMember(Name:="address_email", EmitDefaultValue:=False)>
        Public Property address_email As String

        ''' <summary>
        ''' The contact's phone number.
        ''' </summary>
        <DataMember(Name:="address_phone_number", EmitDefaultValue:=False)>
        Public Property address_phone_number As String

        ''' <summary>
        ''' A latitude of the contact's cached position.
        ''' </summary>
        <DataMember(Name:="cached_lat")>
        Public Property cached_lat As Double

        ''' <summary>
        ''' A longitude of the contact's cached position.
        ''' </summary>
        <DataMember(Name:="cached_lng")>
        Public Property cached_lng As Double

        ''' <summary>
        ''' A latitude of the contact's curbside.
        ''' </summary>
        <DataMember(Name:="curbside_lat")>
        Public Property curbside_lat As Double?

        ''' <summary>
        ''' A longitude of the contact's curbside.
        ''' </summary>
        <DataMember(Name:="curbside_lng")>
        Public Property curbside_lng As Double?

        ''' <summary>
        ''' A city the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_city", EmitDefaultValue:=False)>
        Public Property address_city As String

        ''' <summary>
        ''' The ID of the state the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_state_id", EmitDefaultValue:=False)>
        Public Property address_state_id As String

        ''' <summary>
        ''' The ID of the country the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_country_id", EmitDefaultValue:=False)>
        Public Property address_country_id As String

        ''' <summary>
        ''' The contact's ZIP code.
        ''' </summary>
        <DataMember(Name:="address_zip", EmitDefaultValue:=False)>
        Public Property address_zip As String

        ''' <summary>
        ''' An array of the contact's custom field-value pairs.
        ''' </summary>
        <DataMember(Name:="address_custom_data", EmitDefaultValue:=False)>
        Public Property address_custom_data As Dictionary(Of String, Object)

        ''' <summary>
        ''' An array of the contact's schedules.
        ''' </summary>
        <DataMember(Name:="schedule", EmitDefaultValue:=False)>
        Public Property schedule As IList(Of Schedule)

        ''' <summary>
        ''' The list of dates that should be omitted from the schedules.
        ''' </summary>
        <DataMember(Name:="schedule_blacklist", EmitDefaultValue:=False)>
        Public Property schedule_blacklist As String()

        ''' <summary>
        ''' Number of the routes containing the contact.
        ''' </summary>
        <DataMember(Name:="in_route_count", EmitDefaultValue:=False)>
        Public Property in_route_count As Integer?

        ''' <summary>
        ''' Number of the visits to the contact.
        ''' </summary>
        <DataMember(Name:="visited_count", EmitDefaultValue:=False)>
        Public Property visited_count As Integer?

        ''' <summary>
        ''' When the contact was last visited.
        ''' </summary>
        <DataMember(Name:="last_visited_timestamp", EmitDefaultValue:=False)>
        Public Property last_visited_timestamp As Long?

        ''' <summary>
        ''' When the contact was last routed.
        ''' </summary>
        <DataMember(Name:="last_routed_timestamp", EmitDefaultValue:=False)>
        Public Property last_routed_timestamp As Long?

        ''' <summary>
        ''' The service time at the contact's address.
        ''' </summary>
        <DataMember(Name:="service_time", EmitDefaultValue:=False)>
        Public Property service_time As Long?

        ''' <summary>
        ''' The contact's local timezone.
        ''' </summary>
        <DataMember(Name:="local_timezone_string", EmitDefaultValue:=False)>
        Public Property local_timezone_string As String

        ''' <summary>
        ''' The contact's color on the map.
        ''' </summary>
        <DataMember(Name:="color", EmitDefaultValue:=False)>
        Public Property color As String

        ''' <summary>
        ''' The contact's icon on the map.
        ''' </summary>
        <DataMember(Name:="address_icon", EmitDefaultValue:=False)>
        Public Property address_icon As String

        ''' <summary>
        ''' The contact's stop type.
        ''' </summary>
        <DataMember(Name:="address_stop_type")>
        Public Property AddressStopType As String

        ''' <summary>
        '''  The cubic volume of the contact's cargo.
        ''' </summary>
        <DataMember(Name:="address_cube", EmitDefaultValue:=False)>
        Public Property AddressCube As Double?

        ''' <summary>
        ''' The number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle.
        ''' </summary>
        <DataMember(Name:="address_pieces", EmitDefaultValue:=False)>
        Public Property AddressPieces As Integer?

        ''' <summary>
        ''' The reference number of the address.
        ''' </summary>
        <DataMember(Name:="address_reference_no", EmitDefaultValue:=False)>
        Public Property AddressReferenceNo As String

        ''' <summary>
        ''' The revenue from the contact.
        ''' </summary>
        <DataMember(Name:="address_revenue", EmitDefaultValue:=False)>
        Public Property AddressRevenue As Double?

        ''' <summary>
        ''' The weight of the contact's cargo.
        ''' </summary>
        <DataMember(Name:="address_weight", EmitDefaultValue:=False)>
        Public Property AddressWeight As Double?

        ''' <summary>
        ''' If present, the priority will sequence addresses in all the optimal routes so that 
        ''' higher priority addresses are general at the beginning of the route sequence. 
        ''' 1 is the highest priority, 100000 is the lowest.
        ''' </summary>
        <DataMember(Name:="address_priority", EmitDefaultValue:=False)>
        Public Property AddressPriority As Integer?

        ''' <summary>
        ''' The customer purchase order of the contact.
        ''' </summary>
        <DataMember(Name:="address_customer_po", EmitDefaultValue:=False)>
        Public Property AddressCustomerPo As String

        ''' <summary>
        ''' If true, a location assigned to a route.
        ''' </summary>
        <DataMember(Name:="is_assigned", EmitDefaultValue:=False)>
        Public Property IsAssigned As Boolean?

    End Class
End Namespace