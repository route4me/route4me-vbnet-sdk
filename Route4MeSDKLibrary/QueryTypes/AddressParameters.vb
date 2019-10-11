Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class AddressParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteId As String

        <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
        Public Property RouteDestinationId As Integer

        <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)>
        Public Property AddressId As Integer

        <HttpQueryMemberAttribute(Name:="notes")>
        Public Property Notes As Boolean

        <HttpQueryMemberAttribute(Name:="is_departed")>
        Public Property IsDeparted As Boolean

        <HttpQueryMemberAttribute(Name:="is_visited")>
        Public Property IsVisited As Boolean

    End Class
End Namespace