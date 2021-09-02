Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response from the get address book contacts request
    ''' </summary>
    <DataContract>
    Public NotInheritable Class AddressBookContactsResponse
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' An array of the visible addresses
        ''' </summary>
        <DataMember(Name:="fields", EmitDefaultValue:=False)>
        Public Property Fields As String()

        ''' <summary>
        ''' An array of the AddressBookContact type objects
        ''' </summary>
        <DataMember(Name:="results", EmitDefaultValue:=False)>
        Public Property Results As AddressBookContact()

        ''' <summary>
        ''' Total number of the returned address book contacts
        ''' </summary>
        <DataMember(Name:="total", EmitDefaultValue:=False)>
        Public Property Total As Integer?

    End Class
End Namespace