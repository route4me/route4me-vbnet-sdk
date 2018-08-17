Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberConfigurationDataRersponse
        Inherits GenericParameters

        <DataMember(Name:="config_key", EmitDefaultValue:=False)> _
        Public Property result As String

        <DataMember(Name:="data", EmitDefaultValue:=False)> _
        Public Property data As MemberConfigurationData()

    End Class
End Namespace
