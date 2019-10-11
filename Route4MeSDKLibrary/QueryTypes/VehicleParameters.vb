Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class VehicleParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="with_pagination", EmitDefaultValue:=False)>
        Public Property WithPagination As Boolean

        <HttpQueryMemberAttribute(Name:="page", EmitDefaultValue:=False)>
        Public Property Page As UInteger?

        <HttpQueryMemberAttribute(Name:="perPage", EmitDefaultValue:=False)>
        Public Property PerPage As UInteger?

        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String
    End Class
End Namespace