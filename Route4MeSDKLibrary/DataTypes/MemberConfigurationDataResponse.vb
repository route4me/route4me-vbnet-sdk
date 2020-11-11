Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberConfigurationDataResponse
        Inherits GenericParameters

        <DataMember(Name:="config_key", EmitDefaultValue:=False)>
        Public Property result As String

        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property data As MemberConfigurationData()

    End Class
End Namespace
