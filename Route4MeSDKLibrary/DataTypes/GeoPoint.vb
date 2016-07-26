Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class GeoPoint
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
    End Class
End Namespace
