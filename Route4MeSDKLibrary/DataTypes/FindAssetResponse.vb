Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class FindAssetResponse
        <DataMember(Name:="tracking_number")> _
        Public Property TrackingNumber() As String
            Get
                Return m_TrackingNumber
            End Get
            Set(value As String)
                m_TrackingNumber = value
            End Set
        End Property
        Private m_TrackingNumber As String

        <DataMember(Name:="status_history")> _
        Public Property StatusHistory() As String()
            Get
                Return m_StatusHistory
            End Get
            Set(value As String())
                m_StatusHistory = value
            End Set
        End Property
        Private m_StatusHistory As String()

        <DataMember(Name:="locations")> _
        Public Property Locations() As FindAssetResponseLocations()
            Get
                Return m_Locations
            End Get
            Set(value As FindAssetResponseLocations())
                m_Locations = value
            End Set
        End Property
        Private m_Locations As FindAssetResponseLocations()

        <DataMember(Name:="custom_data", EmitDefaultValue:=False)> _
        Public Property CustomData() As Dictionary(Of String, String)
            Get
                Return m_CustomData
            End Get
            Set(value As Dictionary(Of String, String))
                m_CustomData = value
            End Set
        End Property
        Private m_CustomData As Dictionary(Of String, String)

        <DataMember(Name:="arrival")> _
        Public Property Arrival() As FindAssetResponseArrival()
            Get
                Return m_Arrival
            End Get
            Set(value As FindAssetResponseArrival())
                m_Arrival = value
            End Set
        End Property
        Private m_Arrival As FindAssetResponseArrival()

        <DataMember(Name:="delivered", EmitDefaultValue:=False)> _
        Public Property Delivered() As System.Nullable(Of Boolean)
            Get
                Return m_Delivered
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Delivered = value
            End Set
        End Property
        Private m_Delivered As System.Nullable(Of Boolean)

    End Class

    <DataContract> _
    Public NotInheritable Class FindAssetResponseLocations
        <DataMember(Name:="lat")> _
        Public Property Latitude() As Double
            Get
                Return m_Latitude
            End Get
            Set(value As Double)
                m_Latitude = value
            End Set
        End Property
        Private m_Latitude As Double

        <DataMember(Name:="lng")> _
        Public Property Longitude() As Double
            Get
                Return m_Longitude
            End Get
            Set(value As Double)
                m_Longitude = value
            End Set
        End Property
        Private m_Longitude As Double

        <DataMember(Name:="icon")> _
        Public Property Icon() As String
            Get
                Return m_Icon
            End Get
            Set(value As String)
                m_Icon = value
            End Set
        End Property
        Private m_Icon As String
    End Class

    <DataContract> _
    Public NotInheritable Class FindAssetResponseArrival
        <DataMember(Name:="from_unix_timestamp")> _
        Public Property FromUnixTimestamp() As System.Nullable(Of Integer)
            Get
                Return m_FromUnixTimestamp
            End Get
            Set(value As System.Nullable(Of Integer))
                m_FromUnixTimestamp = value
            End Set
        End Property
        Private m_FromUnixTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="to_unix_timestamp")> _
        Public Property ToUnixTimestamp() As System.Nullable(Of Integer)
            Get
                Return m_ToUnixTimestamp
            End Get
            Set(value As System.Nullable(Of Integer))
                m_ToUnixTimestamp = value
            End Set
        End Property
        Private m_ToUnixTimestamp As System.Nullable(Of Integer)
    End Class
End Namespace