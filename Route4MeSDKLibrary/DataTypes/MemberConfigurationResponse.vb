Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class MemberConfigurationResponse

        <DataMember(Name:="result")> _
        Public Property result As String

        <DataMember(Name:="affected")> _
        Public Property affected As Integer
    End Class
End Namespace
