Imports Route4MeSDKLibrary.Route4MeSDK

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add a custom note type to the user account.
        ''' </summary>
        Public Sub AddCustomNoteType()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim customType As String = "To Do 5"
            Dim customValues As String() = New String() _
            {
                "Pass a package 5",
                "Pickup package",
                "Do a service"
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.AddCustomNoteType(customType, customValues, errorString)

            PrintExampleCustomNoteType(response, errorString)

            CustomNoteTypesToRemove = New List(Of String)()

            If response IsNot Nothing AndAlso response.[GetType]() = GetType(Integer) Then
                CustomNoteTypesToRemove.Add("To Do 5")
            End If

            RemoveCustomNoteTypes()
        End Sub
    End Class
End Namespace
