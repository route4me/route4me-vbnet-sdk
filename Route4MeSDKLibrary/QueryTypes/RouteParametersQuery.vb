Namespace Route4MeSDK.QueryTypes

    Public NotInheritable Class RouteParametersQuery
        Inherits GenericParameters
        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId() As String
            Get
                Return m_RouteId
            End Get
            Set(value As String)
                m_RouteId = Value
            End Set
        End Property
        Private m_RouteId As String

        <HttpQueryMemberAttribute(Name:="directions", EmitDefaultValue:=False)> _
        Public Property Directions() As System.Nullable(Of Boolean)
            Get
                Return m_Directions
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Directions = Value
            End Set
        End Property
        Private m_Directions As System.Nullable(Of Boolean)

        <HttpQueryMemberAttribute(Name:="route_path_output", EmitDefaultValue:=False)> _
        Public Property RoutePathOutput() As String
            Get
                Return m_RoutePathOutput
            End Get
            Set(value As String)
                m_RoutePathOutput = Value
            End Set
        End Property
        Private m_RoutePathOutput As String

        <HttpQueryMemberAttribute(Name:="device_tracking_history", EmitDefaultValue:=False)> _
        Public Property DeviceTrackingHistory() As System.Nullable(Of Boolean)
            Get
                Return m_DeviceTrackingHistory
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_DeviceTrackingHistory = Value
            End Set
        End Property
        Private m_DeviceTrackingHistory As System.Nullable(Of Boolean)

        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
        Public Property Limit() As System.Nullable(Of UInteger)
            Get
                Return m_Limit
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Limit = Value
            End Set
        End Property
        Private m_Limit As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
        Public Property Offset() As System.Nullable(Of UInteger)
            Get
                Return m_Offset
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Offset = Value
            End Set
        End Property
        Private m_Offset As System.Nullable(Of UInteger)

        <HttpQueryMemberAttribute(Name:="original", EmitDefaultValue:=False)> _
        Public Property Original() As System.Nullable(Of Boolean)
            Get
                Return m_Original
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Original = Value
            End Set
        End Property
        Private m_Original As System.Nullable(Of Boolean)

        <HttpQueryMemberAttribute(Name:="notes", EmitDefaultValue:=False)> _
        Public Property Notes() As System.Nullable(Of Boolean)
            Get
                Return m_Notes
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_Notes = Value
            End Set
        End Property
        Private m_Notes As System.Nullable(Of Boolean)
    End Class
End Namespace
