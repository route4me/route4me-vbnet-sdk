Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' The class for the request/response data structure to/from the endpoint vehicles/assign.
    ''' </summary>
    Public NotInheritable Class VehicleTemporary
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' A license plate of the vehicle.
        ''' </summary>
        <DataMember(Name:="vehicle_license_plate", EmitDefaultValue:=False)>
        Public Property VehicleLicensePlate As String

        ''' <summary>
        ''' Member ID assigned to the temporary vehicle.
        ''' </summary>
        <DataMember(Name:="assigned_member_id", EmitDefaultValue:=False)>
        Public Property AssignedMemberId As String

        ''' <summary>
        ''' An expiration date of the temporary vehicle.
        ''' </summary>
        <DataMember(Name:="expires_at", EmitDefaultValue:=False)>
        Public Property ExpiresAt As String

        ''' <summary>
        ''' If true, an assignment forced.
        ''' </summary>
        <DataMember(Name:="force-assignment", EmitDefaultValue:=False)>
        Public Property ForceAssignment As Boolean?

    End Class
End Namespace
