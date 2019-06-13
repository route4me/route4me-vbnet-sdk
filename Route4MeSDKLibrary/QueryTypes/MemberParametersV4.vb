Imports System.Runtime.Serialization
Imports System.Collections.Generic

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberParametersV4
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property member_id As System.Nullable(Of Integer)

        <DataMember(Name:="HIDE_ROUTED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_ROUTED_ADDRESSES As Boolean

        <DataMember(Name:="HIDE_VISITED_ADDRESSES", EmitDefaultValue:=False)> _
        Public Property HIDE_VISITED_ADDRESSES As Boolean

        <DataMember(Name:="READONLY_USER", EmitDefaultValue:=False)> _
        Public Property READONLY_USER As Boolean

        <DataMember(Name:="HIDE_NONFUTURE_ROUTES", EmitDefaultValue:=False)> _
        Public Property HIDE_NONFUTURE_ROUTES As Boolean

        <DataMember(Name:="SHOW_ALL_VEHICLES", EmitDefaultValue:=False)> _
        Public Property SHOW_ALL_VEHICLES As Boolean

        <DataMember(Name:="SHOW_ALL_DRIVERS", EmitDefaultValue:=False)> _
        Public Property SHOW_ALL_DRIVERS As Boolean

        <DataMember(Name:="member_phone", EmitDefaultValue:=False)> _
        Public Property member_phone As String

        <DataMember(Name:="member_zipcode", EmitDefaultValue:=False)> _
        Public Property member_zipcode As String

        <HttpQueryMemberAttribute(Name:="route_count", EmitDefaultValue:=False)> _
        Public Property route_count As System.Nullable(Of Integer)

        <DataMember(Name:="member_email", EmitDefaultValue:=False)> _
        Public Property member_email As String

        <DataMember(Name:="member_type", EmitDefaultValue:=False)> _
        Public Property member_type As String

        <DataMember(Name:="date_of_birth", EmitDefaultValue:=False)> _
        Public Property date_of_birth As String

        <DataMember(Name:="member_first_name", EmitDefaultValue:=False)> _
        Public Property member_first_name As String

        <DataMember(Name:="member_password", EmitDefaultValue:=False)> _
        Public Property member_password As String

        <DataMember(Name:="member_last_name", EmitDefaultValue:=False)> _
        Public Property member_last_name As String

        <DataMember(Name:="custom_data", EmitDefaultValue:=False)> _
        Public Property custom_data() As Dictionary(Of String, Object)
            Get
                If _custom_data Is Nothing Then
                    Return Nothing
                Else
                    Dim v1 = CType(_custom_data, Dictionary(Of String, Object))
                    Dim v2 As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

                    For Each kv1 As KeyValuePair(Of String, Object) In v1

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

            Set(value As Dictionary(Of String, Object))
                If value Is Nothing Then
                    _custom_data = Nothing
                Else
                    Dim v1 = CType(value, Dictionary(Of String, Object))
                    Dim v2 As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

                    For Each kv1 As KeyValuePair(Of String, Object) In v1

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
        Private _custom_data As Dictionary(Of String, Object)

    End Class
End Namespace
