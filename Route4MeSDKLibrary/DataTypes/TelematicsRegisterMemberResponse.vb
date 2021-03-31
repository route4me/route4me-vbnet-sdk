Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Response from the process of a telematics member registering.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class TelematicsRegisterMemberResponse
        ''' <summary>
        ''' API token
        ''' Use for the operations Get Telematics Connections, Register Telematics Connection
        ''' </summary>
        <DataMember(Name:="api_token", EmitDefaultValue:=False)>
        Public Property ApiToken As String

        ''' <summary>
        ''' When the registered member updated
        ''' </summary>
        <DataMember(Name:="updated_at", EmitDefaultValue:=False)>
        Public Property UpdatedAt As String

        ''' <summary>
        ''' When the registered member created
        ''' </summary>
        <DataMember(Name:="created_at", EmitDefaultValue:=False)>
        Public Property CreatedAt As String

        ''' <summary>
        ''' Telemetics member  ID
        ''' </summary>
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property Id As Integer?
    End Class
End Namespace