Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response from execution of a vehicle order.
    ''' </summary>
    Public NotInheritable Class VehicleOrderResponse
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' Vehicle order ID
        ''' </summary>
        <DataMember(Name:="order_id", EmitDefaultValue:=False)>
        Public Property OrderId As Integer?

    End Class
End Namespace