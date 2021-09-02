Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes.V5
    ''' <summary>
    ''' Parameters for execution of a vehicle order.
    ''' </summary>
    <DataContract>
    Public Class AddressBookParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Unique ID of an address book contact.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)>
        Public Property AddressId As String

        ''' <summary>
        ''' Limit the number of records in response.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property Limit As UInteger?

        ''' <summary>
        ''' Only records from that offset will be considered.
        ''' <remarks><para>Query parameter.</para></remarks>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property Offset As UInteger?

        ''' <summary>
        ''' Query string for filtering of the address book contacts.
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)>
        Public Property Query As String

        ''' <summary>
        ''' An array of the fields.
        ''' <para><remarks>If specified, the response will contain only the values of the listed fields.</remarks></para>
        ''' <para>Query parameter.</para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="fields", EmitDefaultValue:=False)>
        Public Property Fields As String()

        ''' <summary>
        ''' Specifies which address book contacts to display.
        ''' <para>Available values are:
        ''' <value>'all', 'routed', 'unrouted'</value>
        ''' </para>
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="display", EmitDefaultValue:=False)>
        Public Property Display As String

    End Class
End Namespace