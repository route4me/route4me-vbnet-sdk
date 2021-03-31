Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Vehicle query parameters
    ''' </summary>
    Public NotInheritable Class VehicleParameters
        Inherits GenericParameters

        ''' <summary>
        ''' If true, returned vehicles array will be paginated
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
        ''' An array of the Vehicle IDs.
        ''' </summary>
        <DataMember(Name:="ids", EmitDefaultValue:=False)>
        Public Property VehicleIDs As String()

        ''' <summary>
        ''' Vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' Vehicle license plate
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="vehicle_license_plate", EmitDefaultValue:=False)>
        Public Property VehicleLicensePlate As String

    End Class
End Namespace