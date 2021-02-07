Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes.V5

    ''' <summary>
    ''' Route filter parameters.
    ''' </summary>
    Public NotInheritable Class RouteFilterParameters
        Inherits GenericParameters

        <DataMember(Name:="query", EmitDefaultValue:=False)>
        Public Property Query As String

        <DataMember(Name:="filters", EmitDefaultValue:=False)>
        Public Property Filters As RouteFilterParametersFilters

        <DataMember(Name:="directions", EmitDefaultValue:=False)>
        Public Property Directions As Boolean?

        <DataMember(Name:="notes", EmitDefaultValue:=False)>
        Public Property Notes As Boolean?

        <DataMember(Name:="page", EmitDefaultValue:=False)>
        Public Property Page As Integer?

        <DataMember(Name:="per_page", EmitDefaultValue:=False)>
        Public Property PerPage As Integer?

        <DataMember(Name:="order_by", EmitDefaultValue:=False)>
        Public Property OrderBy As List(Of String())

        <DataMember(Name:="timezone", EmitDefaultValue:=False)>
        Public Property Timezone As String

    End Class

    Public Class RouteFilterParametersFilters
        Inherits GenericParameters

        ''' <summary>
        ''' An array of the scheduled dates.
        ''' </summary>
        <DataMember(Name:="schedule_date", EmitDefaultValue:=False)>
        Public Property ScheduleDate As String()
    End Class

End Namespace

