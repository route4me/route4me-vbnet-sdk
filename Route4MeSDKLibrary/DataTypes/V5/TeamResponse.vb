Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5
    ''' <summary>
    ''' Response from the user endpoint (/api/v5.0/team/users) request
    ''' </summary>
    <DataContract>
    Public NotInheritable Class TeamResponse

        ''' <summary>
        ''' The member ID
        ''' </summary>
        <DataMember(Name:="member_id")>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' The user's account owner ID
        ''' </summary>
        <DataMember(Name:="OWNER_MEMBER_ID")>
        Public Property OwnerMemberId As Integer?

        ''' <summary>
        ''' User's first name
        ''' </summary>
        <DataMember(Name:="member_first_name")>
        Public Property MemberFirstName As String

        ''' <summary>
        ''' User's last name
        ''' </summary>
        <DataMember(Name:="member_last_name")>
        Public Property MemberLastName As String

        ''' <summary>
        ''' User's email
        ''' </summary>
        <DataMember(Name:="member_email")>
        Public Property MemberEmail As String

        ''' <summary>
        ''' User's phone number
        ''' </summary>
        <DataMember(Name:="member_phone")>
        Public Property MemberPhone As String

        ''' <summary>
        ''' Member's company name
        ''' </summary>
        <DataMember(Name:="member_company", EmitDefaultValue:=False)>
        Public Property MemberCompany As String

        ''' <summary>
        ''' Birthdate of the user.
        ''' </summary>
        <DataMember(Name:="date_of_birth")>
        Public Property DateOfBirth As String

        ''' <summary>
        ''' Registration state ID of a user
        ''' </summary>
        <DataMember(Name:="user_reg_state_id")>
        Public Property UserRegStateId As String

        ''' <summary>
        ''' Registration country ID of a user
        ''' </summary>
        <DataMember(Name:="user_reg_country_id")>
        Public Property UserRegCountryId As String

        ''' <summary>
        ''' A link to the user's picture
        ''' </summary>
        <DataMember(Name:="member_picture")>
        Public Property MemberPicture As String

        ''' <summary>
        ''' Member type. Available values:
        ''' <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ''' <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER</para>
        ''' </summary>
        <DataMember(Name:="member_type")>
        Public Property MemberType As String

        ''' <summary>
        ''' If true, the routed addresses will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_ROUTED_ADDRESSES", EmitDefaultValue:=False)>
        Public Property HideRoutedAddresses As Boolean = False

        ''' <summary>
        ''' If true, the visited addresses will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_VISITED_ADDRESSES", EmitDefaultValue:=False)>
        Public Property HideVisitedAddresses As Boolean = False

        ''' <summary>
        ''' If true, the nonfuture routes will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_NONFUTURE_ROUTES", EmitDefaultValue:=False)>
        Public Property HideNonFutureRoutes As Boolean = False

        ''' <summary>
        ''' If true, the user has read-only access type.
        ''' </summary>
        <DataMember(Name:="READONLY_USER")>
        Public Property ReadOnlyUser As Boolean = False

        ''' <summary>
        ''' If true, the global address book contacts  
        ''' are visible in a user account.
        ''' </summary>
        <DataMember(Name:="SHOW_SUSR_ADDR")>
        Public Property ShowGlobalAddresses As Boolean = False

        ''' <summary>
        ''' If true, the global orders are visible in a user account.
        ''' </summary>
        <DataMember(Name:="SHOW_SUSR_ORDERS")>
        Public Property ShowGlobalOrders As Boolean = True

        ''' <summary>
        ''' If true, all drivers are visible to the user.
        ''' </summary>
        <DataMember(Name:="SHOW_ALL_DRIVERS")>
        Public Property ShowAllDrivers As Boolean = False

        ''' <summary>
        ''' If true, all vehicles are visible to the user.
        ''' </summary>
        <DataMember(Name:="SHOW_ALL_VEHICLES")>
        Public Property ShowAllVehicles As Boolean = False

        ''' <summary>
        ''' Allowed sub-member types in the user's account.
        ''' Available array item values:
        ''' "SUB_ACCOUNT_DRIVER", "SUB_ACCOUNT_DISPATCHER", "SUB_ACCOUNT_PLANNER",
        ''' "SUB_ACCOUNT_ANALYST", "SUB_ACCOUNT_ADMIN", "SUB_ACCOUNT_REGIONAL_MANAGER"
        ''' </summary>
        <DataMember(Name:="allowed_submember_types")>
        Public Property AllowedSubmemberTypes As String()

        ''' <summary>
        ''' If true, the user can edit the account data.
        ''' </summary>
        <DataMember(Name:="can_edit")>
        Public Property CanEdit As Boolean = False

        ''' <summary>
        ''' If true, the user can delete the account data.
        ''' </summary>
        <DataMember(Name:="can_delete")>
        Public Property CanDelete As Boolean = False

        ''' <summary>
        ''' The user's custom data
        ''' </summary>
        <DataMember(Name:="custom_data", EmitDefaultValue:=False)>
        Public Property CustomData As Dictionary(Of String, String)
            Get
                If _custom_data Is Nothing Then
                    Return Nothing
                Else
                    Dim v1 = CType(_custom_data, Dictionary(Of String, String))
                    Dim v2 As Dictionary(Of String, String) = New Dictionary(Of String, String)()

                    For Each kv1 As KeyValuePair(Of String, String) In v1

                        If kv1.Key IsNot Nothing Then

                            If kv1.Value IsNot Nothing Then
                                v2.Add(kv1.Key, kv1.Value.ToString())
                            Else
                                v2.Add(kv1.Key, "")
                            End If
                        Else
                            Continue For
                        End If
                    Next

                    Return v2
                End If
            End Get
            Set(ByVal value As Dictionary(Of String, String))

                If value Is Nothing Then
                    _custom_data = Nothing
                Else
                    Dim v1 = CType(value, Dictionary(Of String, String))
                    Dim v2 As Dictionary(Of String, String) = New Dictionary(Of String, String)()

                    For Each kv1 As KeyValuePair(Of String, String) In v1

                        If kv1.Key IsNot Nothing Then

                            If kv1.Value IsNot Nothing Then
                                v2.Add(kv1.Key, kv1.Value.ToString())
                            Else
                                v2.Add(kv1.Key, "")
                            End If
                        Else
                            Continue For
                        End If
                    Next

                    _custom_data = v2
                End If
            End Set
        End Property

        Private _custom_data As Dictionary(Of String, String)

        ''' <summary>
        ''' Hourly rate of a user
        ''' </summary>
        <DataMember(Name:="DriverHourlyRate")>
        Public Property DriverHourlyRate As Double?

        ''' <summary>
        ''' Vendor ID
        ''' </summary>
        <DataMember(Name:="vendor_id")>
        Public Property VendorId As String

        ''' <summary>
        ''' Driving rate of a user.
        ''' </summary>
        <DataMember(Name:="driving_rate")>
        Public Property DrivingRate As Double?

        ''' <summary>
        ''' Working rate of a user.
        ''' </summary>
        <DataMember(Name:="working_rate")>
        Public Property WorkingRate As Double?

        ''' <summary>
        ''' Mile rate of a user.
        ''' </summary>
        <DataMember(Name:="mile_rate")>
        Public Property MileRate As Double?

        ''' <summary>
        ''' Mile rate of a user.
        ''' </summary>
        <DataMember(Name:="idling_rate")>
        Public Property IdlingRate As Double?

        ''' <summary>
        ''' Display maximum_routes number of future days.
        ''' </summary>
        <DataMember(Name:="display_max_routes_future_days", EmitDefaultValue:=False)>
        Public Property DisplayMaxRoutesFutureDays As Integer?

        ''' <summary>
        ''' Member's location timezone
        ''' </summary>
        <DataMember(Name:="timezone")>
        Public Property Timezone As String

    End Class
End Namespace