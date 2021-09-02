Imports System.Runtime.Serialization
Imports Newtonsoft.Json.Linq

Namespace Route4MeSDK.DataTypes.V5
    ''' <summary>
    ''' Address book contact class. 
    ''' <para>See the JSON schema at <see cref="https://github.com/route4me/route4me-json-schemas/blob/master/API5/address-book.json>link</see> </para>
    ''' <para>Note: 'contact' means 'address book contact' and 'address' means 'geographic address of the contact'.</para>
    ''' </summary>
    <DataContract>
    Public NotInheritable Class AddressBookContact
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' A territory shape name the contact belongs.
        ''' </summary>
        <DataMember(Name:="territory_name", EmitDefaultValue:=False)>
        Public Property TerritoryName As String

        ''' <summary>
        ''' Time when the contact was created.
        ''' </summary>
        <DataMember(Name:="created_timestamp", EmitDefaultValue:=False)>
        Public Property CreatedTimestamp As Long

        ''' <summary>
        ''' Unique ID of the contact.
        ''' </summary>
        <DataMember(Name:="address_id", EmitDefaultValue:=False)>
        Public Property AddressId As Integer?

        ''' <summary>
        ''' The geographic address of the contact.
        ''' </summary>
        <DataMember(Name:="address_1")>
        Public Property Address1 As String

        ''' <summary>
        ''' Second geographic address of the contact.
        ''' </summary>
        <DataMember(Name:="address_2", EmitDefaultValue:=False)>
        Public Property Address2 As String

        ''' <summary>
        ''' Unique ID of the member.
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' The contact's alias.
        ''' </summary>
        <DataMember(Name:="address_alias", EmitDefaultValue:=False)>
        Public Property AddressAlias As String

        ''' <summary>
        ''' A group the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_group", EmitDefaultValue:=False)>
        Public Property AddressGroup As String

        ''' <summary>
        ''' The first name of the contact person.
        ''' </summary>
        <DataMember(Name:="first_name", EmitDefaultValue:=False)>
        Public Property FirstName As String

        ''' <summary>
        ''' The last name of the contact person.
        ''' </summary>
        <DataMember(Name:="last_name", EmitDefaultValue:=False)>
        Public Property LastName As String

        ''' <summary>
        ''' Start of the contact's local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_start", EmitDefaultValue:=False)>
        Public Property LocalTimeWindowStart As Long?

        ''' <summary>
        ''' End of the contact's local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_end", EmitDefaultValue:=False)>
        Public Property LocalTimeWindowEnd As Long?

        ''' <summary>
        ''' Start of the contact's second local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_start_2", EmitDefaultValue:=False)>
        Public Property LocalTimeWindowStart2 As Long?

        ''' <summary>
        ''' End of the contact's second local time window.
        ''' </summary>
        <DataMember(Name:="local_time_window_end_2", EmitDefaultValue:=False)>
        Public Property LocalTimeWindowEnd2 As Long?

        ''' <summary>
        ''' The contact's email.
        ''' </summary>
        <DataMember(Name:="address_email", EmitDefaultValue:=False)>
        Public Property AddressEmail As String

        ''' <summary>
        ''' The contact's phone number.
        ''' </summary>
        <DataMember(Name:="address_phone_number", EmitDefaultValue:=False)>
        Public Property AddressPhoneNumber As String

        ''' <summary>
        ''' A latitude of the contact's cached position.
        ''' </summary>
        <DataMember(Name:="cached_lat")>
        Public Property CachedLat As Double

        ''' <summary>
        ''' A longitude of the contact's cached position.
        ''' </summary>
        <DataMember(Name:="cached_lng")>
        Public Property CachedLng As Double

        ''' <summary>
        ''' A latitude of the contact's curbside.
        ''' </summary>
        <DataMember(Name:="curbside_lat")>
        Public Property CurbsideLat As Double?

        ''' <summary>
        ''' A longitude of the contact's curbside.
        ''' </summary>
        <DataMember(Name:="curbside_lng")>
        Public Property CurbsideLng As Double?

        ''' <summary>
        ''' A city the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_city", EmitDefaultValue:=False)>
        Public Property AddressCity As String

        ''' <summary>
        ''' The ID of the state the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_state_id", EmitDefaultValue:=False)>
        Public Property AddressStateId As String

        ''' <summary>
        ''' The ID of the country the contact belongs.
        ''' </summary>
        <DataMember(Name:="address_country_id", EmitDefaultValue:=False)>
        Public Property AddressCountryId As String

        ''' <summary>
        ''' The contact's ZIP code.
        ''' </summary>
        <DataMember(Name:="address_zip", EmitDefaultValue:=False)>
        Public Property AddressZip As String

        ''' <summary>
        ''' An array of the contact's custom field-value pairs.
        ''' </summary>
        <DataMember(Name:="address_custom_data", EmitDefaultValue:=False)>
        Public Property AddressCustomData As Object
            Get
                Return _address_custom_data
            End Get
            Set(ByVal value As Object)

                Try

                    If value Is Nothing OrElse (CObj(value)).[GetType]() = GetType(Array) Then
                        _address_custom_data = Nothing
                    Else

                        If (CObj(value)).[GetType]() = GetType(JObject) Then
                            _address_custom_data = (CType(value, JObject)).ToObject(Of Dictionary(Of String, String))()
                        ElseIf (CObj(value)).[GetType]() = GetType(Dictionary(Of String, String)) Then
                            If value Is Nothing OrElse (CObj(value)).[GetType]() <> GetType(Array) Then _address_custom_data = value
                        End If
                    End If

                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End Set
        End Property

        Private _address_custom_data As Object

        ''' <summary>
        ''' An array of the contact's schedules.
        ''' </summary>
        <DataMember(Name:="schedule", EmitDefaultValue:=False)>
        Public Property Schedule As IList(Of Schedule)

        ''' <summary>
        ''' The list of dates that should be omitted from the schedules.
        ''' </summary>
        <DataMember(Name:="schedule_blacklist", EmitDefaultValue:=False)>
        Public Property ScheduleBlacklist As String()

        ''' <summary>
        ''' Number of the routes containing the contact.
        ''' </summary>
        <DataMember(Name:="in_route_count", EmitDefaultValue:=False)>
        Public Property InRouteCount As Integer?

        ''' <summary>
        ''' Number of the visits to the contact.
        ''' </summary>
        <DataMember(Name:="visited_count", EmitDefaultValue:=False)>
        Public Property VisitedCount As Integer?

        ''' <summary>
        ''' When the contact was last visited.
        ''' </summary>
        <DataMember(Name:="last_visited_timestamp", EmitDefaultValue:=False)>
        Public Property LastVisitedTimestamp As Long?

        ''' <summary>
        ''' When the contact was last routed.
        ''' </summary>
        <DataMember(Name:="last_routed_timestamp", EmitDefaultValue:=False)>
        Public Property LastRoutedTimestamp As Long?

        ''' <summary>
        ''' The service time at the contact's address.
        ''' </summary>
        <DataMember(Name:="service_time", EmitDefaultValue:=False)>
        Public Property ServiceTime As Long?

        ''' <summary>
        ''' The contact's local timezone.
        ''' </summary>
        <DataMember(Name:="local_timezone_string", EmitDefaultValue:=False)>
        Public Property LocalTimezoneString As String

        ''' <summary>
        ''' The contact's color on the map.
        ''' </summary>
        <DataMember(Name:="color", EmitDefaultValue:=False)>
        Public Property Color As String

        ''' <summary>
        ''' The contact's icon on the map.
        ''' </summary>
        <DataMember(Name:="address_icon", EmitDefaultValue:=False)>
        Public Property AddressIcon As String

        ''' <summary>
        ''' The contact's stop type.
        ''' </summary>
        <DataMember(Name:="address_stop_type")>
        Public Property AddressStopType As String

        ''' <summary>
        ''' The cubic volume of the contact's cargo.
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
        ''' 1 Is the highest priority, 100000 Is the lowest.
        ''' </summary>
        <DataMember(Name:="address_priority", EmitDefaultValue:=False)>
        Public Property AddressPriority As Integer?

        ''' <summary>
        ''' The customer purchase order of the contact.
        ''' </summary>
        <DataMember(Name:="address_customer_po", EmitDefaultValue:=False)>
        Public Property AddressCustomerPo As String

        ''' <summary>
        ''' Assigned to the member
        ''' </summary>
        <DataMember(Name:="assigned_to", EmitDefaultValue:=False)>
        Public Property IsAssigned As ContactAssignedTo

        ''' <summary>
        ''' If true, the contact is eligigble for pickup.
        ''' </summary>
        <DataMember(Name:="eligible_pickup", EmitDefaultValue:=False)>
        Public Property EligiblePickup As Boolean?

        ''' <summary>
        ''' If true, the contact is eligigble for depot.
        ''' </summary>
        <DataMember(Name:="eligible_depot", EmitDefaultValue:=False)>
        Public Property EligibleDepot As Boolean?

        <OnSerializing()>
        Friend Sub OnSerializingMethod(ByVal context As StreamingContext)
            If _address_custom_data Is Nothing Then Return

            If _address_custom_data.[GetType]() <> GetType(Dictionary(Of String, String)) Then
                _address_custom_data = Nothing
            End If
        End Sub

    End Class

    ''' <summary>
    ''' Data structure of an assigned member.
    ''' </summary>
    <DataContract>
    Public Class ContactAssignedTo
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' Unique ID of the member.
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' The first name of the contact person.
        ''' </summary>
        <DataMember(Name:="member_first_name", EmitDefaultValue:=False)>
        Public Property MemberFirstName As String

        ''' <summary>
        ''' The last name of the contact person.
        ''' </summary>
        <DataMember(Name:="member_last_name", EmitDefaultValue:=False)>
        Public Property MemberLastName As String

        ''' <summary>
        ''' The contact's email.
        ''' </summary>
        <DataMember(Name:="member_email", EmitDefaultValue:=False)>
        Public Property MemberEmail As String

        ''' <summary>
        ''' Membership expiration date
        ''' </summary>
        <DataMember(Name:="until", EmitDefaultValue:=False)>
        Public Property Until As String
    End Class
End Namespace