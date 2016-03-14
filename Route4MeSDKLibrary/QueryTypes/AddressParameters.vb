Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class AddressParameters
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

        <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
        Public Property RouteDestinationId() As Integer
            Get
                Return m_RouteDestinationId
            End Get
            Set(value As Integer)
                m_RouteDestinationId = Value
            End Set
        End Property
        Private m_RouteDestinationId As Integer

        <HttpQueryMemberAttribute(Name:="notes")> _
        Public Property Notes() As Boolean
            Get
                Return m_Notes
            End Get
            Set(value As Boolean)
                m_Notes = Value
            End Set
        End Property
        Private m_Notes As Boolean
    End Class
End Namespace