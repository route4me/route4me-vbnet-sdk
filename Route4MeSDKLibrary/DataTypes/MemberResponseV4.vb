Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class MemberResponseV4

        <DataMember(Name:="HIDE_NONFUTURE_ROUTES", EmitDefaultValue:=False)> _
        Public Property HIDE_NONFUTURE_ROUTES As String

        <DataMember(Name:="HIDE_ROUTED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_ROUTED_ADDRESSES As String

        <DataMember(Name:="HIDE_VISITED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_VISITED_ADDRESSES As String

        <DataMember(Name:="member_id")> _
        Public Property member_id As String

        <DataMember(Name:="OWNER_MEMBER_ID")> _
        Public Property OWNER_MEMBER_ID As String

        <DataMember(Name:="READONLY_USER")> _
        Public Property READONLY_USER As String

        <DataMember(Name:="SHOW_ALL_DRIVERS")> _
        Public Property SHOW_ALL_DRIVERS As String

        <DataMember(Name:="SHOW_ALL_VEHICLES")> _
        Public Property SHOW_ALL_VEHICLES As String

        <DataMember(Name:="date_of_birth")> _
        Public Property date_of_birth As String

        <DataMember(Name:="member_email")> _
        Public Property member_email As String

        <DataMember(Name:="member_first_name")> _
        Public Property member_first_name As String

        <DataMember(Name:="member_last_name")> _
        Public Property member_last_name As String

        <DataMember(Name:="member_phone")> _
        Public Property member_phone As String

        <DataMember(Name:="member_picture")> _
        Public Property member_picture As String

        <DataMember(Name:="member_type")> _
        Public Property member_type As String

        <DataMember(Name:="member_zipcode")> _
        Public Property member_zipcode As String

        <DataMember(Name:="preferred_language")> _
        Public Property preferred_language As String

        <DataMember(Name:="preferred_units")> _
        Public Property preferred_units As String

        <DataMember(Name:="timezone")> _
        Public Property timezone As String

        <DataMember(Name:="user_reg_country_id")> _
        Public Property user_reg_country_id As String

        <DataMember(Name:="user_reg_state_id")> _
        Public Property user_reg_state_id As String

        <DataMember(Name:="custom_data", EmitDefaultValue:=False)> _
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

    End Class
End Namespace