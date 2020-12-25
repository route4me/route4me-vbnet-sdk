Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public Class ActivityMember
        Inherits GenericParameters

        ''' <summary>
        ''' Member ID
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' First name of an user created an activity.
        ''' </summary>
        <DataMember(Name:="member_first_name", EmitDefaultValue:=False)>
        Public Property MemberFirstName As String

        ''' <summary>
        ''' Last name of an user created an activity.
        ''' </summary>
        <DataMember(Name:="member_last_name", EmitDefaultValue:=False)>
        Public Property MemberLastName As String

        ''' <summary>
        ''' Email of an user created an activity.
        ''' </summary>
        <DataMember(Name:="member_email", EmitDefaultValue:=False)>
        Public Property MemberEmail As String
    End Class

End Namespace
