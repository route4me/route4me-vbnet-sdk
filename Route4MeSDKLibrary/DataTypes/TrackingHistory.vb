Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class TrackingHistory
        ' tracking data key names are shortened to reduce bandwidth usage (even with compression on) 

        ' speed at the time of the location transaction event
        <DataMember(Name:="s")> _
        Public Property Speed() As System.Nullable(Of Double)
            Get
                Return m_Speed
            End Get
            Set(value As System.Nullable(Of Double))
                m_Speed = Value
            End Set
        End Property
        Private m_Speed As System.Nullable(Of Double)

        ' latitude at the time of the location transaction event
        <DataMember(Name:="lt")> _
        Public Property Latitude() As System.Nullable(Of Double)
            Get
                Return m_Latitude
            End Get
            Set(value As System.Nullable(Of Double))
                m_Latitude = Value
            End Set
        End Property
        Private m_Latitude As System.Nullable(Of Double)

        ' longitude at the time of the location transaction event
        <DataMember(Name:="lg")> _
        Public Property Longitude() As System.Nullable(Of Double)
            Get
                Return m_Longitude
            End Get
            Set(value As System.Nullable(Of Double))
                m_Longitude = Value
            End Set
        End Property
        Private m_Longitude As System.Nullable(Of Double)

        ' direction/heading at the time of the location transaction event
        <DataMember(Name:="d")> _
        Public Property D() As String
            Get
                Return m_D
            End Get
            Set(value As String)
                m_D = Value
            End Set
        End Property
        Private m_D As String

        ' the original timestamp in unix timestamp format at the moment location transaction event
        <DataMember(Name:="ts")> _
        Public Property TimeStamp() As String
            Get
                Return m_TimeStamp
            End Get
            Set(value As String)
                m_TimeStamp = Value
            End Set
        End Property
        Private m_TimeStamp As String

        ' the original timestamp in a human readable timestamp format at the moment location transaction event
        <DataMember(Name:="ts_friendly")> _
        Public Property TimeStampFriendly() As String
            Get
                Return m_TimeStampFriendly
            End Get
            Set(value As String)
                m_TimeStampFriendly = Value
            End Set
        End Property
        Private m_TimeStampFriendly As String
    End Class
End Namespace
