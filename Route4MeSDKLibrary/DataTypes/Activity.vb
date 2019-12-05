Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class Activity
        Inherits GenericParameters
        ''' <summary>
        ''' Activity Id
        ''' </summary>
        <DataMember(Name:="activity_id", EmitDefaultValue:=False)> _
        Public Property ActivityId As String

        ''' <summary>
        ''' Activity type
        ''' </summary>
        <DataMember(Name:="activity_type", EmitDefaultValue:=False)> _
        Public Property ActivityType As String

        ''' <summary>
        ''' Activity timestamp
        ''' </summary>
        <DataMember(Name:="activity_timestamp", EmitDefaultValue:=False)>
        Public Property ActivityTimestamp As Long?

        ''' <summary>
        ''' Activity message
        ''' </summary>
        <DataMember(Name:="activity_message", EmitDefaultValue:=False)> _
        Public Property ActivityMessage As String

        ''' <summary>
        ''' Member Id
        ''' </summary>
        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId As String

        ''' <summary>
        ''' Route Id
        ''' </summary>
        <DataMember(Name:="route_id", EmitDefaultValue:=False)> _
        Public Property RouteId As String

        ''' <summary>
        ''' Destination Id
        ''' </summary>
        <DataMember(Name:="route_destination_id", EmitDefaultValue:=False)> _
        Public Property RouteDestinationId As String

    End Class
End Namespace

