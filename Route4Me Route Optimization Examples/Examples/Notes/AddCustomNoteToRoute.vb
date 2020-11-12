Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add custom note to the specified route
        ''' </summary>
        Public Sub AddCustomNoteToRoute()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunSingleDriverRoundTrip()

            OptimizationsToRemove = New List(Of String)() From {
                SDRT_optimization_problem_id
            }

            Dim noteParameters = New NoteParameters() With {
                .RouteId = SDRT_route.RouteID,
                .AddressId = If(
                    SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing,
                    CInt(SDRT_route.Addresses(1).RouteDestinationId),
                    0
                ),
                .Format = "json",
                .Latitude = SDRT_route.Addresses(1).Latitude,
                .Longitude = SDRT_route.Addresses(1).Longitude
            }

            Dim customNotes = New Dictionary(Of String, String)() From {
                {"custom_note_type[11]", "slippery"},
                {"custom_note_type[10]", "Backdoor"},
                {"strUpdateType", "dropoff"},
                {"strNoteContents", "test1111"}
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.addCustomNoteToRoute(noteParameters, customNotes, errorString)

            PrintExampleAddressNote(response, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
