Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class Order
        Inherits GenericParameters

        ''' <summary>
        ''' Order ID.
        ''' </summary>
        <DataMember(Name:="order_id", EmitDefaultValue:=False)>
        Public Property order_id As Integer

        ''' <summary>
        ''' Address 1 field. Required
        ''' </summary>
        <DataMember(Name:="address_1")>
        Public Property address_1 As String

        ''' <summary>
        ''' Address 2 field
        ''' </summary>
        <DataMember(Name:="address_2", EmitDefaultValue:=False)>
        Public Property address_2 As String

        ''' <summary>
        ''' Geo latitude. Required
        ''' </summary>
        <DataMember(Name:="cached_lat")>
        Public Property cached_lat As Double

        ''' <summary>
        ''' Geo longitude. Required
        ''' </summary>
        <DataMember(Name:="cached_lng")>
        Public Property cached_lng As Double

        ''' <summary>
        ''' Generate optimal routes and driving directions to this curbside latitude
        ''' </summary>
        <DataMember(Name:="curbside_lat", EmitDefaultValue:=False)>
        Public Property curbside_lat As Double?

        ''' <summary>
        ''' Generate optimal routes and driving directions to the curbside langitude
        ''' </summary>
        <DataMember(Name:="curbside_lng", EmitDefaultValue:=False)>
        Public Property curbside_lng As Double?

        ''' <summary>
        ''' Scheduled day
        ''' </summary>
        <DataMember(Name:="day_scheduled_for_YYMMDD", EmitDefaultValue:=False)>
        Public Property day_scheduled_for_YYMMDD As String

        ''' <summary>
        ''' Address Alias. Required
        ''' </summary>
        <DataMember(Name:="address_alias")>
        Public Property address_alias As String

        ''' <summary>
        ''' Local time window start
        ''' </summary>
        <DataMember(Name:="local_time_window_start", EmitDefaultValue:=False)>
        Public Property local_time_window_start As System.Nullable(Of Integer)

        ''' <summary>
        ''' Local time window end
        ''' </summary>
        <DataMember(Name:="local_time_window_end", EmitDefaultValue:=False)>
        Public Property local_time_window_end As Integer?

        ''' <summary>
        ''' Second Local time window start
        ''' </summary>
        <DataMember(Name:="local_time_window_start_2", EmitDefaultValue:=False)>
        Public Property local_time_window_start_2 As Integer?

        ''' <summary>
        ''' Second local time window end
        ''' </summary>
        <DataMember(Name:="local_time_window_end_2", EmitDefaultValue:=False)>
        Public Property local_time_window_end_2 As Integer?

        ''' <summary>
        ''' Second time
        ''' </summary>
        <DataMember(Name:="service_time", EmitDefaultValue:=False)>
        Public Property service_time As Integer?

        ''' <summary>
        ''' Address City
        ''' </summary>
        <DataMember(Name:="address_city", EmitDefaultValue:=False)>
        Public Property address_city As String

        ''' <summary>
        ''' Address state ID
        ''' </summary>
        <DataMember(Name:="address_state_id", EmitDefaultValue:=False)>
        Public Property address_state_id As String

        ''' <summary>
        ''' Address country ID
        ''' </summary>
        <DataMember(Name:="address_country_id", EmitDefaultValue:=False)>
        Public Property address_country_id As String

        ''' <summary>
        ''' Address ZIP
        ''' </summary>
        <DataMember(Name:="address_zip", EmitDefaultValue:=False)>
        Public Property address_zip As String

        ''' <summary>
        ''' Order status ID
        ''' </summary>
        <DataMember(Name:="order_status_id", EmitDefaultValue:=False)>
        Public Property order_status_id As Integer

        ''' <summary>
        ''' The id of the member inside the route4me system
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property member_id As Integer

        ''' <summary>
        ''' First name
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_first_name", EmitDefaultValue:=False)>
        Public Property EXT_FIELD_first_name As String

        ''' <summary>
        ''' Last name
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_last_name", EmitDefaultValue:=False)>
        Public Property EXT_FIELD_last_name As String

        ''' <summary>
        ''' Email
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_email", EmitDefaultValue:=False)>
        Public Property EXT_FIELD_email As String

        ''' <summary>
        ''' Phone number
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_phone", EmitDefaultValue:=False)>
        Public Property EXT_FIELD_phone As String

        ''' <summary>
        ''' Custom data
        ''' </summary>
        <DataMember(Name:="EXT_FIELD_custom_data", EmitDefaultValue:=False)> _
        Public Property EXT_FIELD_custom_data() As Object
            Get
                Return _ext_field_custom_data
            End Get
            Set(value As Object)
                If value.[GetType]().ToString() = "System.Collections.Generic.Dictionary" Then
                    _ext_field_custom_data = value
                Else
                    _ext_field_custom_data = Nothing
                End If
            End Set
        End Property
        Private _ext_field_custom_data As Object

        ''' <summary>
        ''' Local timezone string
        ''' </summary>
        <DataMember(Name:="local_timezone_string", EmitDefaultValue:=False)>
        Public Property local_timezone_string As String

        ''' <summary>
        ''' Order icon
        ''' </summary>
        <DataMember(Name:="order_icon", EmitDefaultValue:=False)>
        Public Property order_icon As String

        ''' <summary>
        ''' Order icon
        ''' </summary>
        <DataMember(Name:="custom_user_fields", EmitDefaultValue:=False)>
        Public Property custom_user_fields As OrderCustomField()

    End Class
End Namespace