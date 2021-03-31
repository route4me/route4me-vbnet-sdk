Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Request parameters for the vehicle profiles.
    ''' </summary>
    Public NotInheritable Class VehicleProfileParameters
        Inherits GenericParameters

        ''' <summary>
        ''' If true, returned vehicle profile is paginated.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="with_pagination", EmitDefaultValue:=False)>
        Public Property WithPagination As Boolean

        ''' <summary>
        ''' Current page number in the vehicles collection
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="page", EmitDefaultValue:=False)>
        Public Property Page As UInteger?

        ''' <summary>
        ''' Returned vehicles number per page
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="perPage", EmitDefaultValue:=False)>
        Public Property PerPage As UInteger?

        ''' <summary>
        ''' Vehicle profile ID
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="id", EmitDefaultValue:=False)>
        Public Property VehicleProfileId As Integer?

    End Class
End Namespace