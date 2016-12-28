Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberConfigurationData
        Inherits GenericParameters

        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property member_id() As Integer
            Get
                Return m_member_id
            End Get
            Set(value As Integer)
                m_member_id = value
            End Set
        End Property
        Private m_member_id As Integer

        <DataMember(Name:="config_key", EmitDefaultValue:=False)> _
        Public Property config_key() As String
            Get
                Return m_config_key
            End Get
            Set(value As String)
                m_config_key = value
            End Set
        End Property
        Private m_config_key As String

        <DataMember(Name:="config_value", EmitDefaultValue:=False)> _
        Public Property config_value() As String
            Get
                Return m_config_value
            End Get
            Set(value As String)
                m_config_value = value
            End Set
        End Property
        Private m_config_value As String

    End Class
End Namespace
