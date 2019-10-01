Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes

    <DataContract>
    Public NotInheritable Class OrderCustomFieldParameters
        Inherits GenericParameters

        <DataMember(Name:="order_custom_field_id", EmitDefaultValue:=False)>
        Public Property OrderCustomFieldId As Integer

        ''' <summary>
        ''' Custom order field name.
        ''' </summary>
        <DataMember(Name:="order_custom_field_name", EmitDefaultValue:=False, IsRequired:=False)>
        Public Property OrderCustomFieldName As String

        ''' <summary>
        ''' Custom order field label.
        ''' </summary>
        <DataMember(Name:="order_custom_field_label", EmitDefaultValue:=False, IsRequired:=False)>
        Public Property OrderCustomFieldLabel As String

        ''' <summary>
        ''' Custom order field type.
        ''' </summary>
        <DataMember(Name:="order_custom_field_type", EmitDefaultValue:=False, IsRequired:=False)>
        Public Property OrderCustomFieldType As String

        ''' <summary>
        ''' Account owner member ID.
        ''' </summary>
        <DataMember(Name:="root_owner_member_id", EmitDefaultValue:=False, IsRequired:=False)>
        Public Property RootOwnerMemberId As Integer

        ''' <summary>
        ''' Information about an order's custom field.
        ''' You can specify the propertiesof the different types in this property,
        ''' but the property "short_label" is reserved - it specifies custom field column header 
        ''' in the orders table in the page: https://route4me.com/orders
        ''' </summary>
        <DataMember(Name:="order_custom_field_type_info", EmitDefaultValue:=False, IsRequired:=False)>
        Public Property OrderCustomFieldTypeInfo As Dictionary(Of String, Object)
    End Class
End Namespace