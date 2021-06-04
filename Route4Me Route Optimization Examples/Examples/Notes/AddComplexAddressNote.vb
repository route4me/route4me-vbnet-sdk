Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Reflection
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add complex note to the specified route address.
        ''' </summary>
        Public Sub AddComplexAddressNote()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunSingleDriverRoundTrip()

            OptimizationsToRemove = New List(Of String)() From {
                SDRT_optimization_problem_id
            }

            Dim routeIdToMoveTo As String = SDRT_route_id
            Dim addressId As Integer = If((SDRT_route IsNot Nothing AndAlso SDRT_route.Addresses.Length > 1 AndAlso SDRT_route.Addresses(1).RouteDestinationId IsNot Nothing), SDRT_route.Addresses(1).RouteDestinationId.Value, 0)

            Dim lat As Double = If(SDRT_route.Addresses.Length > 1, SDRT_route.Addresses(1).Latitude, 33.132675170898)
            Dim lng As Double = If(SDRT_route.Addresses.Length > 1, SDRT_route.Addresses(1).Longitude, -83.244743347168)

            Dim errorString0 As String = Nothing
            Dim customNotesResponse = route4Me.getAllCustomNoteTypes(errorString0)

            Dim customNotes As Dictionary(Of String, String) = Nothing

            If customNotesResponse IsNot Nothing AndAlso customNotesResponse.[GetType]() = GetType(CustomNoteType()) Then
                Dim allCustomNotes = CType(customNotesResponse, CustomNoteType())

                If allCustomNotes.Length > 0 Then
                    customNotes = New Dictionary(Of String, String)() From {
                        {"custom_note_type[" & allCustomNotes(0).NoteCustomTypeID & "]", allCustomNotes(0).NoteCustomTypeValues(0)}
                    }
                End If
            End If

            Dim noteParameters = New NoteParameters() With {
                .RouteId = routeIdToMoveTo,
                .AddressId = addressId,
                .Latitude = lat,
                .Longitude = lng,
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .ActivityType = StatusUpdateType.DropOff.GetEnumDescription(),
                .StrNoteContents = "Test Note Contents " & DateTime.Now.ToString()
            }

            If customNotes IsNot Nothing Then noteParameters.CustomNoteTypes = customNotes

            Dim tempFilePath As String = Nothing

            Using stream As Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Route4Me_Route_Optimization_Examples.test.png")
                Dim tempFiles = New TempFileCollection()

                If True Then
                    tempFilePath = tempFiles.AddExtension("png")
                    Console.WriteLine(tempFilePath)

                    Using fileStream As Stream = File.OpenWrite(tempFilePath)
                        stream.CopyTo(fileStream)
                    End Using
                End If
            End Using

            noteParameters.StrFileName = tempFilePath

            Dim errorString As String = Nothing
            Dim note = route4Me.AddAddressNote(noteParameters, errorString)

            PrintExampleAddressNote(note, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
