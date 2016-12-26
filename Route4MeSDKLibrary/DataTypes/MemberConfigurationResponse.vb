Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class MemberConfigurationResponse

        <DataMember(Name:="result")> _
        Public Property result() As String
            Get
                Return m_result
            End Get
            Set(value As String)
                m_result = value
            End Set
        End Property
        Private m_result As String

        <DataMember(Name:="affected")> _
        Public Property affected() As Integer
            Get
                Return m_affected
            End Get
            Set(value As Integer)
                m_affected = value
            End Set
        End Property
        Private m_affected As Integer

    End Class
End Namespace
