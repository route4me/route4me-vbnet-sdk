Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Avoidance zone querry
    ''' </summary>
    Public NotInheritable Class AvoidanceZoneQuerry
        Inherits GenericParameters
        ''' <summary>
        ''' Device Id
        ''' </summary>
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
        <HttpQueryMemberAttribute(Name:="territory_id", EmitDefaultValue:=False)> _
        Public Property TerritoryId() As String
            Get
                Return m_TerritoryId
            End Get
            Set(value As String)
                m_TerritoryId = Value
            End Set
        End Property
        Private m_TerritoryId As String
    End Class
End Namespace
