Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract>
    Public NotInheritable Class TrackingHistory

        ''' <summary>
        ''' Speed at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="s")>
        Public Property Speed As String

        ''' <summary>
        ''' Latitude at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="lt")>
        Public Property Latitude As String

        ''' <summary>
        ''' Member ID.
        ''' </summary>
        <DataMember(Name:="m")>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Longitude at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="lg")>
        Public Property Longitude As String

        ''' <summary>
        ''' Direction/Heading at the time of the location transaction event.
        ''' </summary>
        <DataMember(Name:="d")>
        Public Property D As Integer?

        ''' <summary>
        ''' The original timestamp in unix timestamp format at the moment location transaction event.
        ''' </summary>
        <DataMember(Name:="ts")>
        Public Property TimeStamp As String

        ''' <summary>
        ''' The original timestamp in a human readable timestamp format at the moment location transaction event.
        ''' </summary>
        <DataMember(Name:="ts_friendly")>
        Public Property TimeStampFriendly As String

        ''' <summary>
        ''' Package src (e.g. 'R4M').
        ''' </summary>
        <DataMember(Name:="src")>
        Public Property Src As String

    End Class
End Namespace
