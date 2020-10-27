Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' URL query parameters for retrieving the schedule calendar.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class ScheduleCalendarQuery
        Inherits GenericParameters

        ''' <summary>
        ''' Start date to filter the schedules in the string format (e.g. 2020-10-27).
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="date_from_string", EmitDefaultValue:=False)>
        Public Property DateFromString As String

        ''' <summary>
        ''' End date to filter the schedules in the string format (e.g. 2020-10-30).
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="date_to_string", EmitDefaultValue:=False)>
        Public Property DateToString As String

        ''' <summary>
        ''' Member ID
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        Private _timezoneOffsetMinutes As Integer?

        ''' <summary>
        ''' Timezone offset (in minutes) (e.g. NYT: -4*60 = -480, Kiev: 3*60 = 180).
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="timezone_offset_minutes", EmitDefaultValue:=False)>
        Public Property TimezoneOffsetMinutes As Integer?
            Get
                Return -_timezoneOffsetMinutes
            End Get
            Set(ByVal value As Integer?)
                _timezoneOffsetMinutes = If(value IsNot Nothing, -value, Nothing)
            End Set
        End Property

        ''' <summary>
        ''' If true, the scheduled orders are included in the calendar.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="orders", EmitDefaultValue:=False)>
        Public Property ShowOrders As Boolean?

        ''' <summary>
        ''' If true, the scheduled address book contacts are included in the calendar.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="ab", EmitDefaultValue:=False)>
        Public Property ShowContacts As Boolean?

        ''' <summary>
        ''' If true, the scheduled routes are included in the calendar.
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="routes_count", EmitDefaultValue:=False)>
        Public Property RoutesCount As Boolean?
    End Class
End Namespace