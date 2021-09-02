Imports System.ComponentModel.DataAnnotations
Imports System.Text.RegularExpressions

Namespace Route4MeSDK
    ''' <summary>
    ''' Validation of the class properties
    ''' </summary>
    Public NotInheritable Class PropertyValidation
        Private Shared CountryIDs As String() = {
            "US", "GB", "CA", "NL", "AU", "MX", "AF", "AL", "DZ", "AS", "AD", "AO", "AI", "AQ", "AG", "AR", "AM", "AW", "AT",
            "AZ", "BS", "BH", "BD", "BB", "BY", "BE", "BZ", "BJ", "BM", "BT", "BO", "BA", "BW", "BV", "BR", "BN", "BG", "BF",
            "BI", "KH", "CM", "CV", "KY", "TD", "CL", "CN", "CX", "CO", "KM", "CG", "CK", "CR", "HR", "CU", "CY", "CZ", "CD",
            "DK", "DJ", "DM", "DO", "TP", "EC", "EG", "SV", "GQ", "ER", "EE", "ET", "FK", "FO", "FJ", "FI", "FR", "GF", "PF",
            "GA", "GM", "GE", "DE", "GH", "GI", "GR", "GL", "GD", "GP", "GU", "GT", "GN", "GW", "GY", "HT", "HN", "HK", "HU",
            "IS", "IN", "ID", "IR", "IQ", "IE", "IL", "IT", "JM", "JP", "JO", "KZ", "KE", "KI", "KR", "KW", "KG", "LA", "LV",
            "LB", "LS", "LR", "LY", "LI", "LT", "LU", "MO", "MG", "MW", "MY", "MV", "ML", "MT", "MH", "MQ", "MR", "MU", "YT",
            "FM", "MD", "MC", "MN", "MS", "MA", "MZ", "MM", "NA", "NR", "NP", "AN", "NC", "NZ", "NI", "NE", "NG", "NU", "NF",
            "KP", "NO", "OM", "PK", "PW", "PA", "PG", "PY", "PE", "PH", "PN", "PL", "PT", "PR", "QA", "RE", "RO", "RU", "RW",
            "KN", "LC", "WS", "SM", "SA", "SN", "SC", "SL", "SG", "SK", "SI", "SB", "SO", "ZA", "ES", "LK", "SH", "PM", "SD",
            "SR", "SZ", "SE", "CH", "SY", "TW", "TJ", "TZ", "TH", "TG", "TK", "TO", "TT", "TN", "TR", "TM", "TC", "TV", "UG",
            "UA", "AE", "UM", "UY", "UZ", "VU", "VA", "VE", "VN", "VG", "VI", "WF", "YE", "YU", "ZM", "ZW"}

        Private Shared UsaStateIDs As String() = {
            "AK", "AL", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS",
            "KY", "LA", "ME", "MH", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND",
            "OH", "OK", "OR", "PW", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VI", "VA", "WA", "WV", "WI", "WY",
            "AE", "CA", "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT"}

        Private Shared AddressStopTypes As String() = {
            "DELIVERY", "PICKUP", "BREAK", "MEETUP", "SERVICE", "VISIT", "DRIVEBY"}

        Public Shared Function ValidateMonthlyNthN(ByVal N As Integer) As ValidationResult
            Dim nList As Integer() = {1, 2, 3, 4, 5, -1}

            If Array.IndexOf(nList, N) >= 0 Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult("The selected option is not available for this type of the schedule.")
            End If
        End Function

        Public Shared Function ValidateScheduleMode(ByVal sMode As String) As ValidationResult
            Dim sList As String() = {"daily", "weekly", "monthly", "annually"}

            If Array.IndexOf(sList, sMode) >= 0 Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult("The selected option is not available for this type of the schedule.")
            End If
        End Function

        Public Shared Function ValidateScheduleMonthlyMode(ByVal sMode As String) As ValidationResult
            Dim sList As String() = {"dates", "nth"}

            If Array.IndexOf(sList, sMode) >= 0 Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult("The selected option is not available for this type of the schedule.")
            End If
        End Function

        Public Shared Function ValidateEpochTime(ByVal value As Object) As ValidationResult
            Dim iTime As Integer = 0

            If Integer.TryParse(value.ToString(), iTime) AndAlso value.[GetType]().ToString() <> "System.String" Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult("The property time can not have the value " & value.ToString())
            End If
        End Function

        Public Shared Function ValidateOverrideAddresses(ByVal value As Object) As ValidationResult
            Try
                Dim oaddr As Route4MeSDK.DataTypes.OverrideAddresses = CType(value, Route4MeSDK.DataTypes.OverrideAddresses)
                Return ValidationResult.Success
            Catch ex As Exception
                Console.WriteLine("Validation of the override_addresses's value failed. " & ex.Message)
                Return New ValidationResult("The property override_addresses can not have the value " & value.ToString())
            End Try
        End Function

        Public Shared Function ValidateLatitude(ByVal value As Object) As ValidationResult
            If value Is Nothing Then Return Nothing
            Dim ___ As Double = Nothing

            Try

                If Double.TryParse(value.ToString(), ___) Then
                    Dim lat As Double = Convert.ToDouble(value)
                    Return If(
                        lat >= -90 AndAlso lat <= 90,
                        ValidationResult.Success,
                        New ValidationResult("The latitude value is wrong: " & (If(value?.ToString(), "null")))
                        )
                Else
                    Return New ValidationResult("The latitude value is wrong: " & (If(value?.ToString(), "null")))
                End If

            Catch ex As Exception
                Return New ValidationResult("The latitude value is wrong: " & (If(value?.ToString(), "null")) & $". Exception: {ex.Message}")
            End Try
        End Function

        Public Shared Function ValidateLongitude(ByVal value As Object) As ValidationResult
            If value Is Nothing Then Return Nothing
            Dim ___ As Double = Nothing

            Try

                If Double.TryParse(value.ToString(), ___) Then
                    Dim lng As Double = Convert.ToDouble(value)
                    Return If(lng >= -180 AndAlso lng <= 180, ValidationResult.Success, New ValidationResult("The longitude value is wrong: " & (If(value?.ToString(), "null"))))
                Else
                    Return New ValidationResult("The longitude value is wrong: " & (If(value?.ToString(), "null")))
                End If

            Catch ex As Exception
                Return New ValidationResult("The longitude value is wrong: " & (If(value?.ToString(), "null")) & $". Exception: {ex.Message}")
            End Try
        End Function

        Public Shared Function ValidateTimeWindow(ByVal value As Object) As ValidationResult
            If value Is Nothing Then Return Nothing
            Dim ___ As Long = Nothing

            Try

                If Long.TryParse(value.ToString(), ___) Then
                    Dim tw As Long = Convert.ToInt64(value)
                    Return If(
                        tw >= 0 AndAlso tw <= 86400,
                        ValidationResult.Success,
                        New ValidationResult("The time window value is wrong: " & (If(value?.ToString(), "null")))
                        )
                Else
                    Return New ValidationResult("The time window value is wrong: " & (If(value?.ToString(), "null")))
                End If

            Catch ex As Exception
                Return New ValidationResult("The time window value is wrong: " & (If(value?.ToString(), "null")) & $". Exception: {ex.Message}")
            End Try
        End Function

        Public Shared Function ValidateCsvTimeWindow(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            Dim pattern As String = "^[0-1]{0,1}[0-9]{1}\:[0-5]{1}[0-9]{1}$"
            Dim regex As Regex = New Regex(pattern)
            Dim match As Match = regex.Match(value.ToString())

            Return If(
                match.Success,
                ValidationResult.Success,
                New ValidationResult("The CSV time window value is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateServiceTime(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            Dim ___ As Long = Nothing

            If Not Long.TryParse(value.ToString(), ___) Then
                Return New ValidationResult("The csv service time must be integer: " & value.ToString())
            End If

            Dim servtime As Integer = Convert.ToInt32(value)

            Return If(
                servtime >= 0 AndAlso servtime < 28800,
                ValidationResult.Success,
                New ValidationResult("The CSV service time value is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateCsvServiceTime(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(Integer) Then
                Return New ValidationResult("The csv service time must be integer: " & value.ToString())
            End If

            Dim servtime As Integer = Convert.ToInt32(value)
            Return If(
                servtime >= 0 AndAlso servtime < 480,
                ValidationResult.Success,
                New ValidationResult("The CSV service time value is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateCountryId(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The country ID must be a string: " & value.ToString())
            End If

            Dim v As String = value.ToString().ToUpper()
            Return If(
                CountryIDs.Contains(v),
                ValidationResult.Success,
                New ValidationResult("The country ID value is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidationStateId(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The state ID must be a string: " & value.ToString())
            End If

            Dim v As String = value.ToString().ToUpper()
            Return If(
                UsaStateIDs.Contains(v),
                ValidationResult.Success,
                New ValidationResult("The state ID value is not USA state ID: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateEmail(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The email must be a string: " & value.ToString())
            End If

            Dim regex As Regex = New Regex("^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" & "@" & "((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")
            Dim match As Match = regex.Match(value.ToString())

            Return If(
                match.Success,
                ValidationResult.Success,
                New ValidationResult("The email is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidatePhone(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            Dim pattern As String = "^(\+\s{0,1}){0,1}(\(\d{1,3}\)|\d{1,3}){1}[ |-]{1}(\d{2,4}[ |.|-]{1})(\d{2,5}[ |.|-]{0,1}){1}(\d{2,5}[ |.|-]{0,1}){0,1}(\d{2,5}[ |.|-]{0,1}){0,1}$"

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The phone number must be a string: " & value.ToString())
            End If

            Dim regex As Regex = New Regex(pattern)
            Dim match As Match = regex.Match(value.ToString())

            Return If(
                match.Success,
                ValidationResult.Success,
                New ValidationResult("The phone number is wrong: " & value.ToString()))
        End Function

        Public Shared Function ValidateZipCode(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then Return New ValidationResult("The zip code must be a string: " & value.ToString())

            Dim regex As Regex = New Regex("^\d{4,5}(-\d{4})?$")
            Dim match As Match = regex.Match(value.ToString())

            Return If(
                match.Success,
                ValidationResult.Success,
                New ValidationResult("The zip code is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateAddressStopType(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The address stop type must be a string: " & value.ToString())
            End If

            Dim v As String = value.ToString().ToUpper()

            Return If(
                AddressStopTypes.Contains(v),
                ValidationResult.Success,
                New ValidationResult("The address stop type value is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateFirstName(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The first name must be a string: " & value.ToString())
            End If

            Return If(
                value.ToString().Length <= 25,
                ValidationResult.Success,
                New ValidationResult("The first name length should be < 26: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateLastName(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The last name must be a string: " & value.ToString())
            End If

            Return If(
                value.ToString().Length <= 35,
                ValidationResult.Success,
                New ValidationResult("The last name length should be < 26: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateColorValue(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            If value.[GetType]() <> GetType(String) Then
                Return New ValidationResult("The color must be a string: " & value.ToString())
            End If

            Dim pattern As String = "^[a-fA-F0-9]{6}$"
            Dim regex As Regex = New Regex(pattern)
            Dim match As Match = regex.Match(value.ToString())

            Return If(
                match.Success,
                ValidationResult.Success,
                New ValidationResult("The color value is wrong: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateCustomData(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            Try
                Dim val = R4MeUtils.ReadObjectNew(Of Dictionary(Of String, String))(value.ToString())
                Return If(
                    val IsNot Nothing,
                    ValidationResult.Success,
                    New ValidationResult("The custom data is wrong: " & value.ToString())
                    )
            Catch ex As Exception
                Return New ValidationResult("The custom data is wrong: " & value.ToString() & $". Exception: {ex.Message}")
            End Try
        End Function

        Public Shared Function ValidateIsNumber(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            Dim ___ As Double = Nothing
            Return If(
                Double.TryParse(value.ToString(), ___),
                ValidationResult.Success,
                New ValidationResult("The value is not number: " & value.ToString())
                )
        End Function

        Public Shared Function ValidateIsBoolean(ByVal value As Object) As ValidationResult
            If (If(value?.ToString().Length, 0)) < 1 Then Return Nothing

            Dim ___ As Boolean = Nothing
            Return If(
                Boolean.TryParse(value.ToString(), ___),
                ValidationResult.Success,
                New ValidationResult("The value is not boolean: " & value.ToString())
                )
        End Function
    End Class
End Namespace
