Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Order inventory class
    ''' </summary>
    <DataContract>
    Public Class OrderInventory
        Inherits GenericParameters

        ''' <summary>
        ''' Unique inventory ID
        ''' </summary>
        <DataMember(Name:="inventory_id", EmitDefaultValue:=False)>
        Public Property InventoryId As Integer?

        ''' <summary>
        ''' Unique order ID
        ''' </summary>
        <DataMember(Name:="order_id", EmitDefaultValue:=False)>
        Public Property OrderId As Integer?

        ''' <summary>
        ''' Order inventory name
        ''' </summary>
        <DataMember(Name:="name", EmitDefaultValue:=False)>
        Public Property Name As String

        ''' <summary>
        ''' Order inventory quantity
        ''' </summary>
        <DataMember(Name:="quantity", EmitDefaultValue:=False)>
        Public Property Quantity As Integer?

        ''' <summary>
        ''' Total weight of the order inventory.
        ''' </summary>
        <DataMember(Name:="total_weight", EmitDefaultValue:=False)>
        Public Property TotalWeight As Double?

        ''' <summary>
        ''' Total volume of the inventory.
        ''' </summary>
        <DataMember(Name:="total_volume", EmitDefaultValue:=False)>
        Public Property TotalVolume As Double?

        ''' <summary>
        ''' Total cost of the inventory.
        ''' </summary>
        <DataMember(Name:="total_cost", EmitDefaultValue:=False)>
        Public Property TotalCost As Double?

        ''' <summary>
        ''' Total price of the inventory.
        ''' </summary>
        <DataMember(Name:="total_price", EmitDefaultValue:=False)>
        Public Property TotalPrice As Double?

        ''' <summary>
        ''' When the inventory created.
        ''' </summary>
        <DataMember(Name:="created_at", EmitDefaultValue:=False)>
        Public Property CreatedAt As String

        ''' <summary>
        ''' When the inventory updated.
        ''' </summary>
        <DataMember(Name:="updated_at", EmitDefaultValue:=False)>
        Public Property UpdatedAt As String

    End Class
End Namespace

