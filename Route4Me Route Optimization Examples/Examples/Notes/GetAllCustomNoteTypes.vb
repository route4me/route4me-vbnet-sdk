Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get all custom note types from the user account.
        ''' </summary>
        Public Sub GetAllCustomNoteTypes()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing
            Dim response = route4Me.getAllCustomNoteTypes(errorString)

            Console.WriteLine(
                If(
                    (response IsNot Nothing AndAlso response.[GetType]() = GetType(CustomNoteType())),
                    "Retrieved the custom note types: " & (CType(response, CustomNoteType())).Length.ToString(),
                    "Cannot retrieve custom note types")
                )
        End Sub
    End Class
End Namespace
