Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Class for the address bundling query
    ''' </summary>
    <DataContract>
    Public Class AddressBundling
        Inherits GenericParameters

        ''' <summary>
        ''' Address bundling mode
        ''' </summary>
        <DataMember(Name:="mode", EmitDefaultValue:=True)>
        <DefaultValue(AddressBundlingMode.Address)>
        <Range(CInt(AddressBundlingMode.Address), CInt(AddressBundlingMode.Coordinates))>
        Public Property Mode As AddressBundlingMode

        ''' <summary>
        ''' Address bundling mode parameters:
        ''' <para>If Mode=3, contains an array of the field names of the Address object</para>
        ''' <para>If Mode=4, contains an array of the custom fields of the Address object</para>
        ''' </summary>
        <DataMember(Name:="mode_params", EmitDefaultValue:=False)>
        Public Property ModeParams As String()

        ''' <summary>
        ''' Address bundling merge mode
        ''' </summary>
        <DataMember(Name:="merge_mode", EmitDefaultValue:=True)>
        <DefaultValue(AddressBundlingMergeMode.KeepAsSeparateDestinations)>
        <Range(CInt(AddressBundlingMergeMode.KeepAsSeparateDestinations), CInt(AddressBundlingMergeMode.MergeIntoSingleDestination))>
        Public Property MergeMode As AddressBundlingMergeMode

        ''' <summary>
        ''' Service time rules of the address bundling
        ''' </summary>
        <DataMember(Name:="service_time_rules", EmitDefaultValue:=False)>
        Public Property ServiceTimeRules As ServiceTimeRulesClass
    End Class

    ''' <summary>
    ''' Class for the address bundling service time rules
    ''' </summary>
    <DataContract>
    Public Class ServiceTimeRulesClass
        ''' <summary>
        ''' Mode of a first item of the bundled addresses.
        ''' </summary>
        <DataMember(Name:="first_item_mode", EmitDefaultValue:=True)>
        <DefaultValue(AddressBundlingFirstItemMode.KeepOriginal)>
        <Range(CInt(AddressBundlingFirstItemMode.KeepOriginal), CInt(AddressBundlingFirstItemMode.CustomTime))>
        Public Property FirstItemMode As AddressBundlingFirstItemMode

        ''' <summary>
        ''' First item mode parameters.
        ''' If FirstItemMode=AddressBundlingFirstItemMode.CustomTime, contains custom service time in seconds.
        ''' </summary>
        <DataMember(Name:="first_item_mode_params", EmitDefaultValue:=False)>
        Public Property FirstItemModeParams As Integer()

        ''' <summary>
        ''' Mode of the non-first items of the bundled addresses.
        ''' </summary>
        <DataMember(Name:="additional_items_mode", EmitDefaultValue:=True)>
        <DefaultValue(AddressBundlingAdditionalItemsMode.KeepOriginal)>
        <Range(CInt(AddressBundlingAdditionalItemsMode.KeepOriginal), CInt(AddressBundlingAdditionalItemsMode.InheritFromPrimary))>
        Public Property AdditionalItemsMode As AddressBundlingAdditionalItemsMode

        ''' <summary>
        ''' Additional items mode parameters:
        ''' <para>if AdditionalItemsMode=AddressBundlingAdditionalItemsMode.CustomTime, contains an array of the custom service times</para>
        ''' </summary>
        <DataMember(Name:="additional_items_mode_params", EmitDefaultValue:=False)>
        Public Property AdditionalItemsModeParams As Integer()
    End Class
End Namespace