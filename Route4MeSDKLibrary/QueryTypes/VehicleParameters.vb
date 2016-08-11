Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization
Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class VehicleParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Limit per page, if you use 0 you will get all records
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
        Public Property Limit() As System.Nullable(Of UInteger)
            Get
                Return m_Limit
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Limit = value
            End Set
        End Property
        Private m_Limit As System.Nullable(Of UInteger)

        ''' <summary>
        ''' Offset
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
        Public Property Offset() As System.Nullable(Of UInteger)
            Get
                Return m_Offset
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Offset = value
            End Set
        End Property
        Private m_Offset As System.Nullable(Of UInteger)

        ''' <summary>
        ''' Vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)> _
        Public Property VehicleId() As String
            Get
                Return m_VehicleId
            End Get
            Set(value As String)
                m_VehicleId = value
            End Set
        End Property
        Private m_VehicleId As String

    End Class
End Namespace