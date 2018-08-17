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
        Public Property enabled As Boolean

        <DataMember(Name:="mode"), CustomValidation(GetType(PropertyValidation), "ValidateScheduleMode")> _
        Public Property mode As String

        <DataMember(Name:="daily", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property daily As schedule_daily

        <DataMember(Name:="weekly", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property weekly As schedule_weekly

        <DataMember(Name:="monthly", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property monthly As schedule_monthly

        <DataMember(Name:="annually", EmitDefaultValue:=False, IsRequired:=False)> _
        Public Property annually As schedule_annually

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

        Public Sub New(ByVal _every As Integer)
            every = _every
        End Sub

        Public Sub New()

        End Sub

        <DataMember(Name:="every")> _
        Public Property every As Integer

    End Class

    <DataContract> _
    Public Class schedule_weekly

        Public Sub New(ByVal _every As Integer, ByVal _weekdays As Integer())
            every = _every
            weekdays = _weekdays
        End Sub

        Public Sub New()

        End Sub

        <DataMember(Name:="every")> _
        Public Property every As Integer

        <DataMember(Name:="weekdays", EmitDefaultValue:=False), Range(1, 7, ErrorMessage:="Weekday must be between 1 and 7")> _
        Public Property weekdays As Integer()
    End Class

    <DataContract> _
    Public Class schedule_monthly_nth
        Public Sub New(Optional ByVal _n As Integer = 1, Optional ByVal _what As Integer = 1)
            n = _n
            what = _what
        End Sub

        Public Sub New()

        End Sub

        <DataMember(Name:="n", EmitDefaultValue:=False), CustomValidation(GetType(PropertyValidation), "ValidateMonthlyNthN")> _
        Public Property n As Integer

        <DataMember(Name:="what", EmitDefaultValue:=False), Range(1, 10, ErrorMessage:="Wrong value for the What Time parameter")> _
        Public Property what As Integer

    End Class

    <DataContract> _
    Public Class schedule_monthly

        Public Sub New(Optional ByVal _every As Integer = 1, Optional ByVal _mode As String = "dates", Optional ByVal _dates As Integer() = Nothing, Optional ByVal _nth As Dictionary(Of Integer, Integer) = Nothing)
            every = _every
            mode = _mode
            If _dates IsNot Nothing Then dates = _dates

            If _nth IsNot Nothing Then
                Dim _n As Integer = -1
                Dim _what As Integer = -1

                For Each kv1 As KeyValuePair(Of Integer, Integer) In _nth
                    _n = kv1.Key
                    _what = kv1.Value
                Next

                If _n <> -1 AndAlso _what <> -1 Then
                    Me.nth = New schedule_monthly_nth(_n, _what)
                End If
            End If
        End Sub

        <DataMember(Name:="every")> _
        Public Property every As Integer

        <DataMember(Name:="mode"), CustomValidation(GetType(PropertyValidation), "ValidateScheduleMonthlyMode")> _
        Public Property mode As String

        <DataMember(Name:="dates", EmitDefaultValue:=False), Range(1, 31, ErrorMessage:="Month day must be between 1 and 31")> _
        Public Property dates As Integer()

        <DataMember(Name:="nth", EmitDefaultValue:=False)> _
        Public Property nth As schedule_monthly_nth

    End Class

    <DataContract> _
    Public Class schedule_annually
        <DataMember(Name:="every")> _
        Public Property every As Integer

        <DataMember(Name:="use_nth")> _
        Public Property use_nth As Boolean

        <DataMember(Name:="months", EmitDefaultValue:=False), Range(1, 12, ErrorMessage:="Month number must be between 1 and 12")> _
        Public Property months As Integer()

        <DataMember(Name:="nth", EmitDefaultValue:=False)> _
        Public Property nth As schedule_monthly_nth

    End Class
End Namespace
