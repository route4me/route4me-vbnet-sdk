Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5
    ''' <summary>
    ''' Account profile
    ''' </summary>
    <DataContract>
    Public NotInheritable Class AccountProfile
        ''' <summary>
        ''' Account profile email
        ''' </summary>
        <DataMember(Name:="email", EmitDefaultValue:=False)>
        Public Property Email As String

        ''' <summary>
        ''' Account member ID
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberIId As Integer?

        ''' <summary>
        ''' Account API key
        ''' </summary>
        <DataMember(Name:="api_key", EmitDefaultValue:=False)>
        Public Property ApiKey As String

        ''' <summary>
        ''' Account root member ID
        ''' </summary>
        <DataMember(Name:="root_member_id", EmitDefaultValue:=False)>
        Public Property RootMemberId As Integer?

        ''' <summary>
        ''' Prefered unnits of the account
        ''' </summary>
        <DataMember(Name:="preferred_units", EmitDefaultValue:=False)>
        Public Property PreferredUnits As String

    End Class
End Namespace
