Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class AddressBookGroupFilter
        <DataMember(Name:="condition", EmitDefaultValue:=False)>
        Public Property Condition As String

        <DataMember(Name:="rules", EmitDefaultValue:=False)>
        Public Property Rules As AddressBookGroupRule()

        Public Sub New()
        End Sub

        Public Sub New(ByVal _condition As String, ByVal _rules As AddressBookGroupRule())
            Condition = _condition
            Rules = _rules
        End Sub
    End Class
End Namespace

