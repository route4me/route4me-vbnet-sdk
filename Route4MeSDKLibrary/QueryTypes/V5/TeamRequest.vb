Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
'Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Data structure for team management request tasks.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class TeamRequest
        Inherits GenericParameters

        ''' <summary>
        ''' A new passowrd of the user to loggin
        ''' </summary>
        <DataMember(Name:="new_password", EmitDefaultValue:=False)>
        Public Property NewPassword As String

        ''' <summary>
        ''' An URL to a member picture file. e.g: /uploads/cc6aba1a0e68ea429c51e3f9cb12e1ac/profile_c96135b77f6fc42be64cd98e0c21d341.jpg
        ''' Or as base64 string: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD//2Q=="
        ''' </summary>
        <DataMember(Name:="new_member_picture", EmitDefaultValue:=False)>
        Public Property NewMemberPicture As String

        ''' <summary>
        ''' User's first name
        ''' </summary>
        <DataMember(Name:="member_first_name", EmitDefaultValue:=False)>
        Public Property MemberFirstName As String

        ''' <summary>
        ''' User's last name
        ''' </summary>
        <DataMember(Name:="member_last_name", EmitDefaultValue:=False)>
        Public Property MemberLastName As String

        ''' <summary>
        ''' User's email
        ''' </summary>
        <DataMember(Name:="member_email", EmitDefaultValue:=False)>
        Public Property MemberEmail As String

        ''' <summary>
        ''' Member's company name
        ''' </summary>
        <DataMember(Name:="member_company", EmitDefaultValue:=False)>
        Public Property MemberCompany As String

        ''' <summary>
        ''' Member type. Available values:
        ''' <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ''' <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER</para>
        ''' </summary>
        <DataMember(Name:="member_type", EmitDefaultValue:=False)>
        Public Property MemberType As String

        ''' <summary>
        ''' The user's account owner ID
        ''' </summary>
        <DataMember(Name:="OWNER_MEMBER_ID", EmitDefaultValue:=False)>
        Public Property OwnerMemberId As Integer?

        ''' <summary>
        ''' User's phone number
        ''' </summary>
        <DataMember(Name:="member_phone", EmitDefaultValue:=False)>
        Public Property MemberPhone As String

        ''' <summary>
        ''' Birthdate of the user.
        ''' </summary>
        <DataMember(Name:="date_of_birth", EmitDefaultValue:=False)>
        Public Property DateOfBirth As String

        ''' <summary>
        ''' Registration state ID of a user
        ''' </summary>
        <DataMember(Name:="user_reg_state_id", EmitDefaultValue:=False)>
        Public Property UserRegStateId As String

        ''' <summary>
        ''' Registration country ID of a user
        ''' </summary>
        <DataMember(Name:="user_reg_country_id", EmitDefaultValue:=False)>
        Public Property UserRegCountryId As String

        ''' <summary>
        ''' Hourly rate of a user
        ''' </summary>
        <DataMember(Name:="DriverHourlyRate", EmitDefaultValue:=False)>
        Public Property DriverHourlyRate As Double?

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
        <DataMember(Name:="READONLY_USER", EmitDefaultValue:=False)>
        Public Property ReadOnlyUser As Boolean = False

        ''' <summary>
        ''' If true, the global address book contacts 
        ''' are visible in a user account.
        ''' </summary>
        <DataMember(Name:="SHOW_SUSR_ADDR", EmitDefaultValue:=False)>
        Public Property ShowGlobalAddresses As Boolean = False

        ''' <summary>
        ''' If true, the global orders are visible in a user account.
        ''' </summary>
        <DataMember(Name:="SHOW_SUSR_ORDERS", EmitDefaultValue:=False)>
        Public Property ShowGlobalOrders As Boolean = True

        ''' <summary>
        ''' If true, all drivers are visible to the user.
        ''' </summary>
        <DataMember(Name:="SHOW_ALL_DRIVERS", EmitDefaultValue:=False)>
        Public Property ShowAllDrivers As Boolean = False

        ''' <summary>
        ''' If true, all vehicles are visible to the user.
        ''' </summary>
        <DataMember(Name:="SHOW_ALL_VEHICLES", EmitDefaultValue:=False)>
        Public Property ShowAllVehicles As Boolean = False

        ''' <summary>
        ''' Display maximum_routes number of future days.
        ''' </summary>
        <DataMember(Name:="display_max_routes_future_days", EmitDefaultValue:=False)>
        Public Property DisplayMaxRoutesFutureDays As Integer?

        ''' <summary>
        ''' Vendor ID
        ''' </summary>
        <DataMember(Name:="vendor_id", EmitDefaultValue:=False)>
        Public Property VendorId As String

        ''' <summary>
        ''' Driving rate of a user.
        ''' </summary>
        <DataMember(Name:="driving_rate", EmitDefaultValue:=False)>
        Public Property DrivingRate As Double?

        ''' <summary>
        ''' Working rate of a user.
        ''' </summary>
        <DataMember(Name:="working_rate", EmitDefaultValue:=False)>
        Public Property WorkingRate As Double?

        ''' <summary>
        ''' Mile rate of a user.
        ''' </summary>
        <DataMember(Name:="mile_rate", EmitDefaultValue:=False)>
        Public Property MileRate As Double?

        ''' <summary>
        ''' Mile rate of a user.
        ''' </summary>
        <DataMember(Name:="idling_rate", EmitDefaultValue:=False)>
        Public Property IdlingRate As Double?

        ''' <summary>
        ''' Member's location timezone
        ''' </summary>
        <DataMember(Name:="timezone", EmitDefaultValue:=False)>
        Public Property Timezone As String


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



        Public Function ValidateMemberCreateRequest(<Out> ByRef errorString As String) As Boolean
            Dim isValid As Boolean = True
            errorString = ""

            If Me.MemberFirstName Is Nothing Then
                errorString += "Member first name is empty"
                isValid = False
            End If

            If Me.MemberLastName Is Nothing Then
                errorString += (If(errorString IsNot Nothing, Environment.NewLine, "")) & "Member last name is empty"
                isValid = False
            End If

            If Me.MemberType Is Nothing Then
                errorString = (If(errorString IsNot Nothing, Environment.NewLine, "")) & "Member type is empty"
                isValid = False
            Else

                If Me.GetMemberType(Me.MemberType) Is Nothing Then
                    errorString = (If(errorString IsNot Nothing, Environment.NewLine, "")) & "Member type " & Me.MemberType & "is not valid"
                    isValid = False
                End If
            End If

            If Me.MemberEmail Is Nothing Then
                errorString += (If(errorString IsNot Nothing, Environment.NewLine, "")) & "Member email is empty"
                isValid = False
            End If

            If Me.NewPassword Is Nothing Then
                errorString += (If(errorString IsNot Nothing, Environment.NewLine, "")) & "Member password is empty"
                isValid = False
            End If

            If Me.OwnerMemberId Is Nothing Then
                errorString += (If(errorString IsNot Nothing, Environment.NewLine, "")) & "Member owner ID is empty"
                isValid = False
            End If

            Return isValid
        End Function

        ''' <summary>
        ''' Set a member type by MemberTypes enum type. 
        ''' </summary>
        ''' <param name="memberType">MemberTypes enum type value</param>
        Public Sub SetMemberType(ByVal memberType As MemberTypes)
            memberType = memberType.GetEnumDescription()
        End Sub


        ''' <summary>
        ''' 
        ''' </summary>
        Public Function GetMemberType(ByVal memberType As String) As MemberTypes?
            For Each i As Integer In [Enum].GetValues(GetType(MemberTypes))

                If (CType(i, MemberTypes)).GetEnumDescription().Equals(memberType) Then
                    Return CType(i, MemberTypes)
                End If
            Next

            Return Nothing
        End Function
    End Class
End Namespace
