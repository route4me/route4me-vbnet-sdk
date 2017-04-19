Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class AddressBookContact
        Inherits GenericParameters
        <DataMember(Name:="territory_name", EmitDefaultValue:=False)> _
        Public Property territory_name As String

        <DataMember(Name:="created_timestamp", EmitDefaultValue:=False)> _
        Public Property created_timestamp As Integer

        <DataMember(Name:="address_id", EmitDefaultValue:=False)> _
        Public Property address_id As System.Nullable(Of Integer)

        <DataMember(Name:="address_1")> _
        Public Property address_1 As String

        <DataMember(Name:="address_2", EmitDefaultValue:=False)> _
        Public Property address_2 As String

        <DataMember(Name:="address_alias", EmitDefaultValue:=False)> _
        Public Property address_alias As String

        <DataMember(Name:="address_group", EmitDefaultValue:=False)> _
        Public Property address_group As String

        <DataMember(Name:="first_name", EmitDefaultValue:=False)> _
        Public Property first_name As String

        <DataMember(Name:="last_name", EmitDefaultValue:=False)> _
        Public Property last_name As String

        <DataMember(Name:="address_email", EmitDefaultValue:=False)> _
        Public Property address_email As String

        <DataMember(Name:="address_phone_number", EmitDefaultValue:=False)> _
        Public Property address_phone_number As String

        <DataMember(Name:="cached_lat")> _
        Public Property cached_lat As Double

        <DataMember(Name:="cached_lng")> _
        Public Property cached_lng As Double

        <DataMember(Name:="curbside_lat")> _
        Public Property curbside_lat As System.Nullable(Of Double)

        <DataMember(Name:="curbside_lng")> _
        Public Property curbside_lng As System.Nullable(Of Double)

        <DataMember(Name:="address_city", EmitDefaultValue:=False)> _
        Public Property address_city As String

        <DataMember(Name:="address_state_id", EmitDefaultValue:=False)> _
        Public Property address_state_id As String

        <DataMember(Name:="address_country_id", EmitDefaultValue:=False)> _
        Public Property address_country_id As String

        <DataMember(Name:="address_zip", EmitDefaultValue:=False)> _
        Public Property address_zip As String

        <DataMember(Name:="address_custom_data", EmitDefaultValue:=False)> _
        Public Property address_custom_data() As Object
            Get
                Return _address_custom_data
            End Get
            Set(value As Object)
                If value.[GetType]().ToString() = "System.Collections.Generic.Dictionary" Then
                    _address_custom_data = value
                Else
                    _address_custom_data = Nothing
                End If
            End Set
        End Property
        Private _address_custom_data As Object

        <DataMember(Name:="schedule", EmitDefaultValue:=False)> _
        Public Property schedule As IList(Of Schedule)

        <DataMember(Name:="schedule_blacklist", EmitDefaultValue:=False)> _
        Public Property schedule_blacklist As String()

        <DataMember(Name:="service_time", EmitDefaultValue:=False)> _
        Public Property service_time As System.Nullable(Of Integer)

        <DataMember(Name:="color", EmitDefaultValue:=False)> _
        Public Property color As String

        <DataMember(Name:="address_icon", EmitDefaultValue:=False)> _
        Public Property address_icon As String

    End Class
End Namespace