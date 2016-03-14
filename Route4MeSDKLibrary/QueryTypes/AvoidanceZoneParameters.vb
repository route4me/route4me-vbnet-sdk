Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Avoidance zone parameters
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class AvoidanceZoneParameters
        Inherits GenericParameters
        ''' <summary>
        ''' Device Id
        ''' </summary>
        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="device_id", EmitDefaultValue:=False)> _
        Public Property DeviceID() As String
            Get
                Return m_DeviceID
            End Get
            Set(value As String)
                m_DeviceID = Value
            End Set
        End Property
        Private m_DeviceID As String

        ''' <summary>
        ''' Territory Id
        ''' </summary>
        <DataMember(Name:="territory_id")> _
        Public Property TerritoryId() As String
            Get
                Return m_TerritoryId
            End Get
            Set(value As String)
                m_TerritoryId = Value
            End Set
        End Property
        Private m_TerritoryId As String

        ''' <summary>
        ''' Territory name
        ''' </summary>
        <DataMember(Name:="territory_name")> _
        Public Property TerritoryName() As String
            Get
                Return m_TerritoryName
            End Get
            Set(value As String)
                m_TerritoryName = Value
            End Set
        End Property
        Private m_TerritoryName As String

        ''' <summary>
        ''' Territory color
        ''' </summary>
        <DataMember(Name:="territory_color")> _
        Public Property TerritoryColor() As String
            Get
                Return m_TerritoryColor
            End Get
            Set(value As String)
                m_TerritoryColor = Value
            End Set
        End Property
        Private m_TerritoryColor As String

        ''' <summary>
        ''' Member Id
        ''' </summary>
        <DataMember(Name:="member_id")> _
        Public Property MemberId() As String
            Get
                Return m_MemberId
            End Get
            Set(value As String)
                m_MemberId = Value
            End Set
        End Property
        Private m_MemberId As String

        ''' <summary>
        ''' Territory parameters
        ''' </summary>
        <DataMember(Name:="territory")> _
        Public Property Territory() As Territory
            Get
                Return m_Territory
            End Get
            Set(value As Territory)
                m_Territory = Value
            End Set
        End Property
        Private m_Territory As Territory
    End Class
End Namespace
