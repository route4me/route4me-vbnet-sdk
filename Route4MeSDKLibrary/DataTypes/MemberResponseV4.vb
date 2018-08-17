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
    End Class
End Namespace