Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' Device tracking history data structure
    ''' </summary>
    <DataContract>
    Public NotInheritable Class TrackingHistory

        ''' <summary>
        ''' Speed at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="s", EmitDefaultValue:=False)>
        Public Property Speed As String

        ''' <summary>
        ''' Speed unit ('mph', 'kph')
        ''' </summary>
        <DataMember(Name:="su", EmitDefaultValue:=False)>
        Public Property SpeedUnit As String

        ''' <summary>
        ''' Latitude at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="lt", EmitDefaultValue:=False)>
        Public Property Latitude As String

        ''' <summary>
        ''' Member ID.
        ''' </summary>
        <DataMember(Name:="m", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Longitude at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="lg", EmitDefaultValue:=False)>
        Public Property Longitude As String

        ''' <summary>
        ''' Direction/Heading at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="d", EmitDefaultValue:=False)>
        Public Property D As Integer?

        ''' <summary>
        ''' The original timestamp in unix timestamp format at the moment location transaction event.
        ''' </summary>
        <DataMember(Name:="ts", EmitDefaultValue:=False)>
        Public Property TimeStamp As String

        ''' <summary>
        ''' The original timestamp in a human readable timestamp format at the moment location transaction event.
        ''' </summary>
        <DataMember(Name:="ts_friendly", EmitDefaultValue:=False)>
        Public Property TimeStampFriendly As String

        ''' <summary>
        ''' Package src (e.g. 'R4M').
        ''' </summary>
        <DataMember(Name:="src", EmitDefaultValue:=False)>
        Public Property Src As String

    End Class
End Namespace
