Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Available values: "all", "routed", "unrouted"
    ''' </summary>
    <DataContract>
    Public NotInheritable Class OrderFilterParameters
        Inherits GenericParameters

        <DataMember(Name:="filter")>
        Public Property Filter As FilterDetails
    End Class

    <DataContract>
    Public Class FilterDetails
        Inherits GenericParameters

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
        Public Property Scheduled_for_YYMMDD As String()

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
    End Class
End Namespace
