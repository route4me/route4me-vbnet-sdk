Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' Response from the user endpoint (/api.v4/user.php) request
    ''' </summary>
    <DataContract>
    Public NotInheritable Class MemberResponseV4

        ''' <summary>
        ''' If true, the nonfuture routes will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_NONFUTURE_ROUTES", EmitDefaultValue:=False)>
        Public Property HIDE_NONFUTURE_ROUTES As String

        ''' <summary>
        ''' If true, the routed addresses will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_ROUTED_ADDRESSES", EmitDefaultValue:=False)>
        Public Property HIDE_ROUTED_ADDRESSES As String

        ''' <summary>
        ''' If true, the visited addresses will be hidden.
        ''' </summary>
        <DataMember(Name:="HIDE_VISITED_ADDRESSES", EmitDefaultValue:=False)>
        Public Property HIDE_VISITED_ADDRESSES As String

        ''' <summary>
        ''' The member ID
        ''' </summary>
        <DataMember(Name:="member_id")>
        Public Property member_id As String

        ''' <summary>
        ''' The user's account owner ID
        ''' </summary>
        <DataMember(Name:="OWNER_MEMBER_ID")>
        Public Property OWNER_MEMBER_ID As String

        ''' <summary>
        ''' If true, the user has read-only access type.
        ''' </summary>
        <DataMember(Name:="READONLY_USER")>
        Public Property READONLY_USER As String

        ''' <summary>
        ''' If true, all drivers are visible to the user.
        ''' </summary>
        <DataMember(Name:="SHOW_ALL_DRIVERS")>
        Public Property SHOW_ALL_DRIVERS As String

        ''' <summary>
        ''' If true, all vehicles are visible to the user.
        ''' </summary>
        <DataMember(Name:="SHOW_ALL_VEHICLES")>
        Public Property SHOW_ALL_VEHICLES As String

        ''' <summary>
        ''' Birthdate of the user.
        ''' </summary>
        <DataMember(Name:="date_of_birth")>
        Public Property date_of_birth As String

        ''' <summary>
        ''' User's email
        ''' </summary>
        <DataMember(Name:="member_email")>
        Public Property member_email As String

        ''' <summary>
        ''' User's first name
        ''' </summary>
        <DataMember(Name:="member_first_name")>
        Public Property member_first_name As String

        ''' <summary>
        ''' User's last name
        ''' </summary>
        <DataMember(Name:="member_last_name")>
        Public Property member_last_name As String

        ''' <summary>
        ''' User's phone number
        ''' </summary>
        <DataMember(Name:="member_phone")>
        Public Property member_phone As String

        ''' <summary>
        ''' A link to the user's picture
        ''' </summary>
        <DataMember(Name:="member_picture")>
        Public Property member_picture As String

        ''' <summary>
        ''' Member type. Available values:
        ''' <para>PRIMARY_ACCOUNT, SUB_ACCOUNT_ADMIN, SUB_ACCOUNT_REGIONAL_MANAGER,</para>
        ''' <para>SUB_ACCOUNT_DISPATCHER, SUB_ACCOUNT_PLANNER, SUB_ACCOUNT_DRIVER</para>
        ''' </summary>
        <DataMember(Name:="member_type")>
        Public Property member_type As String

        ''' <summary>
        ''' User zipcode
        ''' </summary>
        <DataMember(Name:="member_zipcode")>
        Public Property member_zipcode As String

        ''' <summary>
        ''' Preferred language (en, fr)
        ''' </summary>
        <DataMember(Name:="preferred_language")>
        Public Property preferred_language As String

        ''' <summary>
        ''' Preferred unit (mi, km)
        ''' </summary>
        <DataMember(Name:="preferred_units")>
        Public Property preferred_units As String

        ''' <summary>
        ''' Member's location timezone
        ''' </summary>
        <DataMember(Name:="timezone")>
        Public Property timezone As String

        ''' <summary>
        ''' Registration country ID of a user
        ''' </summary>
        <DataMember(Name:="user_reg_country_id")>
        Public Property user_reg_country_id As String

        ''' <summary>
        ''' Registration state ID of a user
        ''' </summary>
        <DataMember(Name:="user_reg_state_id")>
        Public Property user_reg_state_id As String

        ''' <summary>
        ''' Registration state ID of a user
        ''' </summary>
        <DataMember(Name:="level")>
        Public Property level As Integer?

        ''' <summary>
        ''' The user's custom data
        ''' </summary>
        <DataMember(Name:="custom_data", EmitDefaultValue:=False)>
        Public Property custom_data() As Dictionary(Of String, String)
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

            Set(value As Dictionary(Of String, String))
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
        ''' User API key.
        ''' </summary>
        <DataMember(Name:="api_key")>
        Public Property ApiKey As String
    End Class
End Namespace