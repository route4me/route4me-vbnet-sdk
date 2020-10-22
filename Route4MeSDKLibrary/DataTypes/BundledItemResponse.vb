Imports System.ComponentModel
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Bundled item data structure
    ''' </summary>
    <DataContract>
    Public Class BundledItemResponse
        ''' <summary>
        ''' Summary cube value of the bundled addresses
        ''' </summary>
        <DataMember(Name:="cube", EmitDefaultValue:=True)>
        <DefaultValue(0)>
        <[ReadOnly](True)>
        Public Property Cube As Double

        ''' <summary>
        ''' Summary revenue value of the bundled addresses
        ''' </summary>
        <DataMember(Name:="revenue", EmitDefaultValue:=True)>
        <DefaultValue(0)>
        <[ReadOnly](True)>
        Public Property Revenue As Double

        ''' <summary>
        ''' Summary pieces value of the bundled addresses
        ''' </summary>
        <DataMember(Name:="pieces", EmitDefaultValue:=True)>
        <DefaultValue(0)>
        <[ReadOnly](True)>
        Public Property Pieces As Integer

        ''' <summary>
        ''' Summary weight value of the bundled addresses
        ''' </summary>
        <DataMember(Name:="weight", EmitDefaultValue:=True)>
        <DefaultValue(0)>
        <[ReadOnly](True)>
        Public Property Weight As Double

        ''' <summary>
        ''' Summary cost value of the bundled addresses
        ''' </summary>
        <DataMember(Name:="cost", EmitDefaultValue:=True)>
        <DefaultValue(0)>
        <[ReadOnly](True)>
        Public Property Cost As Double

        ''' <summary>
        ''' Service time of the bundled addresses
        ''' </summary>
        <DataMember(Name:="service_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ServiceTime As Integer?

        ''' <summary>
        ''' Time window start of the bundled addresses
        ''' </summary>
        <DataMember(Name:="time_window_start", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TimeWindowStart As Integer?

        ''' <summary>
        ''' Time window emd of the bundled addresses
        ''' </summary>
        <DataMember(Name:="time_window_end", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TimeWindowEnd As Integer?

        ''' <summary>
        ''' TO DO: Adjust description
        ''' </summary>
        <DataMember(Name:="custom_data", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property CustomData As Object

        ''' <summary>
        ''' Array of the IDs of the bundeld addresses.
        ''' </summary>
        <DataMember(Name:="addresses_id", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property AddressesId As Integer()
    End Class
End Namespace
