Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="session_guid", EmitDefaultValue:=False)>
        Public Property SessionGuid As String

        <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)>
        Public Property hFormat As String

        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        <HttpQueryMemberAttribute(Name:="plan", EmitDefaultValue:=False)>
        Public Property Plan As String

        <HttpQueryMemberAttribute(Name:="member_type", EmitDefaultValue:=False)>
        Public Property MemberType As Integer?

        <DataMember(Name:="strEmail", EmitDefaultValue:=False)>
        Public Property StrEmail As String

        <DataMember(Name:="strPassword", EmitDefaultValue:=False)>
        Public Property StrPassword As String

        <DataMember(Name:="format", EmitDefaultValue:=False)>
        Public Property Format As String

        <DataMember(Name:="strIndustry", EmitDefaultValue:=False)>
        Public Property StrIndustry As String

        <DataMember(Name:="strFirstName", EmitDefaultValue:=False)>
        Public Property StrFirstName As String

        <DataMember(Name:="strLastName", EmitDefaultValue:=False)>
        Public Property StrLastName As String

        <DataMember(Name:="chkTerms", EmitDefaultValue:=False)>
        Public Property ChkTerms As Integer?

        <DataMember(Name:="device_type", EmitDefaultValue:=False)>
        Public Property DeviceType As String

        <DataMember(Name:="strPassword_1", EmitDefaultValue:=False)>
        Public Property StrPassword_1 As String

        <DataMember(Name:="strPassword_2", EmitDefaultValue:=False)>
        Public Property StrPassword_2 As String

    End Class
End Namespace
