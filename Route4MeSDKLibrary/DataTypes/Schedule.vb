Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' A trip schedule to a location
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class Schedule

        Public Sub New()

        End Sub

        Public Sub New(ByVal sMode As String, ByVal blNth As Boolean)
            Select Case sMode
                Case "daily"
                    Me.daily = New schedule_daily()
                    Me.mode = "daily"
                    Exit Select
                Case "weekly"
                    Me.weekly = New schedule_weekly()
                    Me.mode = "weekly"
                    'this.weekly.weekdays = new int[]{};
                    Exit Select
                Case "monthly"
                    Me.monthly = New schedule_monthly()
                    Me.mode = "monthly"
                    If blNth Then
                        Me.monthly.nth = New schedule_monthly_nth()
                    End If
                    Exit Select
                Case "annually"
                    Me.annually = New schedule_annually()
                    Me.mode = "annually"
                    If blNth Then
                        Me.annually.nth = New schedule_monthly_nth()
                    End If
                    'this.annually.months = new int[] { }; 
                    Exit Select

            End Select
        End Sub

        <DataMember(Name:="enabled")> _
        Public Property enabled() As Boolean
            Get
                Return m_enabled
            End Get
            Set(value As Boolean)
                m_enabled = value
            End Set
        End Property
        Private m_enabled As Boolean

        <DataMember(Name:="mode"), CustomValidation(GetType(PropertyValidation), "ValidateScheduleMode")> _
        Public Property mode() As String
            Get
                Return m_mode
            End Get
            Set(value As String)
                m_mode = value
            End Set
        End Property
        Private m_mode As String

        <DataMember(Name:="daily", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property daily() As schedule_daily
            Get
                Return m_daily
            End Get
            Set(value As schedule_daily)
                m_daily = value
            End Set
        End Property
        Private m_daily As schedule_daily

        <DataMember(Name:="weekly", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property weekly() As schedule_weekly
            Get
                Return m_weekly
            End Get
            Set(value As schedule_weekly)
                m_weekly = value
            End Set
        End Property
        Private m_weekly As schedule_weekly

        <DataMember(Name:="monthly", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property monthly() As schedule_monthly
            Get
                Return m_monthly
            End Get
            Set(value As schedule_monthly)
                m_monthly = value
            End Set
        End Property
        Private m_monthly As schedule_monthly

        <DataMember(Name:="annually", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property annually() As schedule_annually
            Get
                Return m_annually
            End Get
            Set(value As schedule_annually)
                m_annually = value
            End Set
        End Property
        Private m_annually As schedule_annually

        Public Function ValidateScheduleMode(ScheduleMode As Object) As Boolean
            If ScheduleMode Is Nothing Then
                Return False
            End If
            If Array.IndexOf(New String() {"daily", "weekly", "monthly", "annually"}, ScheduleMode.ToString()) >= 0 Then
                Return True
            End If
            Return False
        End Function

        Public Function ValidateScheduleEnabled(ScheduleEnabled As Object) As Boolean
            Dim blValid As Boolean = False
            If Boolean.TryParse(ScheduleEnabled.ToString(), blValid) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ValidateScheduleUseNth(ScheduleUseNth As Object) As Boolean
            Dim blValid As Boolean = False
            If Boolean.TryParse(ScheduleUseNth.ToString(), blValid) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ValidateScheduleEvery(ScheduleEvery As Object) As Boolean
            Dim iEvery As Integer = -1
            If Integer.TryParse(ScheduleEvery.ToString(), iEvery) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ValidateScheduleWeekdays(Weekdays As Object) As Boolean
            If Weekdays Is Nothing Then
                Return False
            End If

            Dim blValid As Boolean = True

            Dim arWeekdays As String() = Weekdays.ToString().Split(","c)

            For Each weekday As String In arWeekdays
                Dim iWeekday As Integer = -1
                If Not Integer.TryParse(weekday, iWeekday) Then
                    blValid = False
                    Exit For
                End If

                iWeekday = Convert.ToInt32(weekday)
                If iWeekday > 7 OrElse iWeekday < 1 Then
                    blValid = False
                    Exit For
                End If
            Next

            Return blValid
        End Function

        Public Function ValidateScheduleMonthDays(ScheduleMonthDays As Object) As Boolean
            If ScheduleMonthDays Is Nothing Then
                Return False
            End If

            Dim blValid As Boolean = True

            Dim arMonthdays As String() = ScheduleMonthDays.ToString().Split(","c)

            For Each monthday As String In arMonthdays
                Dim iMonthday As Integer = -1
                If Not Integer.TryParse(monthday, iMonthday) Then
                    blValid = False
                    Exit For
                End If

                iMonthday = Convert.ToInt32(monthday)
                If iMonthday > 31 OrElse iMonthday < 1 Then
                    blValid = False
                    Exit For
                End If
            Next

            Return blValid
        End Function

        Public Function ValidateScheduleYearMonths(ScheduleYearMonths As Object) As Boolean
            If ScheduleYearMonths Is Nothing Then
                Return False
            End If

            Dim blValid As Boolean = True

            Dim arYearMonth As String() = ScheduleYearMonths.ToString().Split(","c)

            For Each yearmonth As String In arYearMonth
                Dim iYearmonth As Integer = -1
                If Not Integer.TryParse(yearmonth, iYearmonth) Then
                    blValid = False
                    Exit For
                End If

                iYearmonth = Convert.ToInt32(yearmonth)
                If iYearmonth > 12 OrElse iYearmonth < 1 Then
                    blValid = False
                    Exit For
                End If
            Next

            Return blValid
        End Function

        Public Function ValidateScheduleMonthlyMode(ScheduleMonthlyMode As Object) As Boolean
            If ScheduleMonthlyMode Is Nothing Then
                Return False
            End If
            If Array.IndexOf(New String() {"dates", "nth"}, ScheduleMonthlyMode.ToString()) >= 0 Then
                Return True
            End If
            Return False
        End Function

        Public Function ValidateScheduleNthN(ScheduleNthN As Object) As Boolean
            Dim [iN] As Integer = -1
            If Not Integer.TryParse(ScheduleNthN.ToString(), [iN]) Then
                Return False
            End If
            [iN] = Convert.ToInt32(ScheduleNthN)

            If Array.IndexOf(New Integer() {1, 2, 3, 4, 5, -1}, [iN]) < 0 Then
                Return False
            End If

            Return True
        End Function

        Public Function ValidateScheduleNthWhat(ScheduleNthWhat As Object) As Boolean
            Dim [iN] As Integer = -1
            If Not Integer.TryParse(ScheduleNthWhat.ToString(), [iN]) Then
                Return False
            End If
            [iN] = Convert.ToInt32(ScheduleNthWhat)

            If Array.IndexOf(New Integer() {1, 2, 3, 4, 5, 6, _
                7, 8, 9, 10}, [iN]) < 0 Then
                Return False
            End If

            Return True
        End Function

    End Class

    <DataContract> _
    Public Class schedule_daily
        <DataMember(Name:="every")> _
        Public Property every() As Integer
            Get
                Return m_every
            End Get
            Set(value As Integer)
                m_every = Value
            End Set
        End Property
        Private m_every As Integer
    End Class

    <DataContract> _
    Public Class schedule_weekly
        <DataMember(Name:="every")> _
        Public Property every() As Integer
            Get
                Return m_every
            End Get
            Set(value As Integer)
                m_every = Value
            End Set
        End Property
        Private m_every As Integer

        <DataMember(Name:="weekdays", EmitDefaultValue:=False), Range(1, 7, ErrorMessage:="Weekday must be between 1 and 7")> _
        Public Property weekdays() As Integer()
            Get
                Return m_weekdays
            End Get
            Set(value As Integer())
                m_weekdays = Value
            End Set
        End Property
        Private m_weekdays As Integer()
    End Class

    <DataContract> _
    Public Class schedule_monthly_nth
        <DataMember(Name:="n", EmitDefaultValue:=False), CustomValidation(GetType(PropertyValidation), "ValidateMonthlyNthN")> _
        Public Property n() As Integer
            Get
                Return m_n
            End Get
            Set(value As Integer)
                m_n = Value
            End Set
        End Property
        Private m_n As Integer

        <DataMember(Name:="what", EmitDefaultValue:=False), Range(1, 10, ErrorMessage:="Wrong value for the What Time parameter")> _
        Public Property what() As Integer
            Get
                Return m_what
            End Get
            Set(value As Integer)
                m_what = Value
            End Set
        End Property
        Private m_what As Integer
    End Class

    <DataContract> _
    Public Class schedule_monthly
        <DataMember(Name:="every")> _
        Public Property every() As Integer
            Get
                Return m_every
            End Get
            Set(value As Integer)
                m_every = Value
            End Set
        End Property
        Private m_every As Integer

        <DataMember(Name:="mode"), CustomValidation(GetType(PropertyValidation), "ValidateScheduleMonthlyMode")> _
        Public Property mode() As String
            Get
                Return m_mode
            End Get
            Set(value As String)
                m_mode = Value
            End Set
        End Property
        Private m_mode As String

        <DataMember(Name:="dates", EmitDefaultValue:=False), Range(1, 31, ErrorMessage:="Month day must be between 1 and 31")> _
        Public Property dates() As Integer()
            Get
                Return m_dates
            End Get
            Set(value As Integer())
                m_dates = Value
            End Set
        End Property
        Private m_dates As Integer()

        <DataMember(Name:="nth", EmitDefaultValue:=False)> _
        Public Property nth() As schedule_monthly_nth
            Get
                Return m_nth
            End Get
            Set(value As schedule_monthly_nth)
                m_nth = Value
            End Set
        End Property
        Private m_nth As schedule_monthly_nth
    End Class

    <DataContract> _
    Public Class schedule_annually
        <DataMember(Name:="every")> _
        Public Property every() As Integer
            Get
                Return m_every
            End Get
            Set(value As Integer)
                m_every = Value
            End Set
        End Property
        Private m_every As Integer

        <DataMember(Name:="use_nth")> _
        Public Property use_nth() As Boolean
            Get
                Return m_use_nth
            End Get
            Set(value As Boolean)
                m_use_nth = Value
            End Set
        End Property
        Private m_use_nth As Boolean

        <DataMember(Name:="months", EmitDefaultValue:=False), Range(1, 12, ErrorMessage:="Month number must be between 1 and 12")> _
        Public Property months() As Integer()
            Get
                Return m_months
            End Get
            Set(value As Integer())
                m_months = Value
            End Set
        End Property
        Private m_months As Integer()

        <DataMember(Name:="nth", EmitDefaultValue:=False)> _
        Public Property nth() As schedule_monthly_nth
            Get
                Return m_nth
            End Get
            Set(value As schedule_monthly_nth)
                m_nth = Value
            End Set
        End Property
        Private m_nth As schedule_monthly_nth
    End Class
End Namespace
