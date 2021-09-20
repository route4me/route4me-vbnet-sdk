Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Available values: "all", "routed", "unrouted"
    ''' </summary>
    <DataContract>
    Public NotInheritable Class OrderFilterParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Limit per page, if you use 0 you will get all records
        ''' </summary>
        <DataMember(Name:="limit")>
        Public Property Limit As UInteger?

        ''' <summary>
        ''' Offset.
        ''' </summary>
        <DataMember(Name:="offset")>
        Public Property Offset As UInteger?

        <DataMember(Name:="filter")>
        Public Property Filter As FilterDetails
    End Class

    <DataContract>
    Public Class FilterDetails
        Inherits GenericParameters

        ''' <summary>
        ''' A query text for the orders searching.
        ''' </summary>
        <DataMember(Name:="query")>
        Public Property Query As String

        ''' <summary>
        ''' Available values: "all", "routed", "unrouted"
        ''' </summary>
        <DataMember(Name:="display")>
        Public Property Display As String

        ''' <summary>
        ''' Start and end dates for filter the orders by schedule.
        ''' e.g. ["2019-06-01", "2019-06-18"]
        ''' </summary>
        <DataMember(Name:="scheduled_for_YYMMDD")>
        Public Property Scheduled_for_YYYYMMDD As String()

        ''' <summary>
        ''' An array of the tracking numbers to filter the orders by tracking numbers.
        ''' </summary>
        <DataMember(Name:="tracking_numbers")>
        Public Property TrackingNumbers As String()

    End Class
End Namespace
