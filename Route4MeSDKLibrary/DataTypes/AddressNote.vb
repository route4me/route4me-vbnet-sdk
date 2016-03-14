Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class AddressNote
        <DataMember(Name:="note_id", EmitDefaultValue:=False)> _
        Public Property NoteId() As Integer
            Get
                Return m_NoteId
            End Get
            Set(value As Integer)
                m_NoteId = Value
            End Set
        End Property
        Private m_NoteId As Integer

        <DataMember(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId() As String
            Get
                Return m_RouteId
            End Get
            Set(value As String)
                m_RouteId = Value
            End Set
        End Property
        Private m_RouteId As String

        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)> _
        Public Property RouteDestinationId() As Integer
            Get
                Return m_RouteDestinationId
            End Get
            Set(value As Integer)
                m_RouteDestinationId = Value
            End Set
        End Property
        Private m_RouteDestinationId As Integer

        <DataMember(Name:="upload_id")> _
        Public Property UploadId() As String
            Get
                Return m_UploadId
            End Get
            Set(value As String)
                m_UploadId = Value
            End Set
        End Property
        Private m_UploadId As String

        <DataMember(Name:="ts_added", EmitDefaultValue:=False)> _
        Public Property TimestampAdded() As System.Nullable(Of UInteger)
            Get
                Return m_TimestampAdded
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_TimestampAdded = Value
            End Set
        End Property
        Private m_TimestampAdded As System.Nullable(Of UInteger)

        <DataMember(Name:="lat")> _
        Public Property Latitude() As Double
            Get
                Return m_Latitude
            End Get
            Set(value As Double)
                m_Latitude = Value
            End Set
        End Property
        Private m_Latitude As Double

        <DataMember(Name:="lng")> _
        Public Property Longitude() As Double
            Get
                Return m_Longitude
            End Get
            Set(value As Double)
                m_Longitude = Value
            End Set
        End Property
        Private m_Longitude As Double

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

        <DataMember(Name:="contents")> _
        Public Property Contents() As String
            Get
                Return m_Contents
            End Get
            Set(value As String)
                m_Contents = Value
            End Set
        End Property
        Private m_Contents As String

        <DataMember(Name:="upload_type")> _
        Public Property UploadType() As String
            Get
                Return m_UploadType
            End Get
            Set(value As String)
                m_UploadType = Value
            End Set
        End Property
        Private m_UploadType As String

        <DataMember(Name:="upload_url")> _
        Public Property UploadUrl() As String
            Get
                Return m_UploadUrl
            End Get
            Set(value As String)
                m_UploadUrl = Value
            End Set
        End Property
        Private m_UploadUrl As String

        <DataMember(Name:="upload_extension")> _
        Public Property UploadExtension() As String
            Get
                Return m_UploadExtension
            End Get
            Set(value As String)
                m_UploadExtension = Value
            End Set
        End Property
        Private m_UploadExtension As String

        <DataMember(Name:="device_type")> _
        Public Property DeviceType() As String
            Get
                Return m_DeviceType
            End Get
            Set(value As String)
                m_DeviceType = Value
            End Set
        End Property
        Private m_DeviceType As String
    End Class
End Namespace
