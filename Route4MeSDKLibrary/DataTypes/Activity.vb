Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class Activity
        <DataMember(Name:="activity_id")> _
        Public Property ActivityId() As String
            Get
                Return m_ActivityId
            End Get
            Set(value As String)
                m_ActivityId = Value
            End Set
        End Property
        Private m_ActivityId As String

        <DataMember(Name:="activity_type")> _
        Public Property ActivityType() As String
            Get
                Return m_ActivityType
            End Get
            Set(value As String)
                m_ActivityType = Value
            End Set
        End Property
        Private m_ActivityType As String

        <DataMember(Name:="activity_timestamp", EmitDefaultValue:=False)> _
        Public Property ActivityTimestamp() As System.Nullable(Of UInteger)
            Get
                Return m_ActivityTimestamp
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_ActivityTimestamp = Value
            End Set
        End Property
        Private m_ActivityTimestamp As System.Nullable(Of UInteger)

        <DataMember(Name:="activity_message")> _
        Public Property ActivityMessage() As String
            Get
                Return m_ActivityMessage
            End Get
            Set(value As String)
                m_ActivityMessage = Value
            End Set
        End Property
        Private m_ActivityMessage As String
    End Class
End Namespace

