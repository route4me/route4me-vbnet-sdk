Imports System.ComponentModel.DataAnnotations

Namespace Route4MeSDK
    ''' <summary>
    ''' Validation of the class properties
    ''' </summary>
    Public NotInheritable Class PropertyValidation
        Private Sub New()
        End Sub
        Public Shared Function ValidateMonthlyNthN(N As Integer) As ValidationResult
            Dim nList As Integer() = {1, 2, 3, 4, 5, -1}
            ' Perform validation logic here and set isValid to true or false.

            If Array.IndexOf(nList, N) >= 0 Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult("The selected option is not available for this type of the schedule.")
            End If
        End Function

        Public Shared Function ValidateScheduleMode(sMode As String) As ValidationResult
            Dim sList As String() = {"daily", "weekly", "monthly", "annually"}
            ' Perform validation logic here and set isValid to true or false.

            If Array.IndexOf(sList, sMode) >= 0 Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult("The selected option is not available for this type of the schedule.")
            End If
        End Function

        Public Shared Function ValidateScheduleMonthlyMode(sMode As String) As ValidationResult
            Dim sList As String() = {"dates", "nth"}
            ' Perform validation logic here and set isValid to true or false.

            If Array.IndexOf(sList, sMode) >= 0 Then
                Return ValidationResult.Success
            Else
                Return New ValidationResult("The selected option is not available for this type of the schedule.")
            End If
        End Function
    End Class
End Namespace
