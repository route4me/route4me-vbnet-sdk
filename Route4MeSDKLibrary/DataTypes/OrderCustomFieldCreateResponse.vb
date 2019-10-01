Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' The class for the response from the creating/updating an order custom field.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class OrderCustomFieldCreateResponse
        ''' <summary>
        ''' If the order custom field created/update succsessfully, equals to 'OK'.
        ''' </summary>
        <DataMember(Name:="result", EmitDefaultValue:=False)>
        Public Property Result As String

        ''' <summary>
        ''' How many custom order fields were affected..
        ''' </summary>
        <DataMember(Name:="affected", EmitDefaultValue:=False)>
        Public Property Affected As Integer

        ''' <summary>
        ''' Created/updated custom order field.
        ''' </summary>
        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property Data As OrderCustomField

    End Class
End Namespace
