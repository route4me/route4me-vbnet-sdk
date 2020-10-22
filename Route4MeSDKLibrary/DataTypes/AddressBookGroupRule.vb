Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class AddressBookGroupRule
        <DataMember(Name:="id", EmitDefaultValue:=False)>
        Public Property ID As String

        <DataMember(Name:="field", EmitDefaultValue:=False)>
        Public Property Field As String

        <DataMember(Name:="type", EmitDefaultValue:=False)>
        Public Property Type As String

        <DataMember(Name:="input", EmitDefaultValue:=False)>
        Public Property Input As String

        <DataMember(Name:="operator", EmitDefaultValue:=False)>
        Public Property [Operator] As String

        <DataMember(Name:="value", EmitDefaultValue:=False)>
        Public Property Value As Object

        Public Sub New()
        End Sub

        Public Sub New(ByVal _field As String, ByVal _operator As String, ByVal _value As String, ByVal Optional _type As String = Nothing, ByVal Optional _input As String = Nothing)
            Field = _field
            [Operator] = _operator
            Value = _value
            If _type IsNot Nothing Then Type = _type
            If _input IsNot Nothing Then Input = _input
        End Sub
    End Class
End Namespace

