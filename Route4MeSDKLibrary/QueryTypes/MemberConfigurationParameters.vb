Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberConfigurationParameters
        Inherits GenericParameters

        <DataMember(Name:="config_key", EmitDefaultValue:=False)>
        Public Property config_key As String

        <DataMember(Name:="config_value", EmitDefaultValue:=False)> _
        Public Property config_value() As String

    End Class
End Namespace
