Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberConfigurationData
        Inherits GenericParameters

        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property member_id As Integer

        <DataMember(Name:="config_key", EmitDefaultValue:=False)> _
        Public Property config_key As String

        <DataMember(Name:="config_value", EmitDefaultValue:=False)> _
        Public Property config_value As String
    End Class
End Namespace
