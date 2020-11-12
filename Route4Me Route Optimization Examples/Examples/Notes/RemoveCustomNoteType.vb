Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove custom note type from the user account.
        ''' </summary>
        Public Sub RemoveCustomNoteType()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateCustomNoteType()

            Dim customNoteTypeId As Integer? = GetCustomNoteIdByName("To Do 5")

            If customNoteTypeId Is Nothing Then Return

            Dim errorString As String = Nothing
            Dim response = route4Me.removeCustomNoteType(CInt(customNoteTypeId), errorString)

            PrintExampleCustomNoteType(response, errorString)
        End Sub
    End Class
End Namespace
