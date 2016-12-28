Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberConfigurationDataRersponse
        Inherits GenericParameters

        <DataMember(Name:="config_key", EmitDefaultValue:=False)> _
        Public Property result() As String
            Get
                Return m_result
            End Get
            Set(value As String)
                m_result = value
            End Set
        End Property
        Private m_result As String

        <DataMember(Name:="data", EmitDefaultValue:=False)> _
        Public Property data() As MemberConfigurationData()
            Get
                Return m_data
            End Get
            Set(value As MemberConfigurationData())
                m_data = value
            End Set
        End Property
        Private m_data As MemberConfigurationData()

    End Class
End Namespace
