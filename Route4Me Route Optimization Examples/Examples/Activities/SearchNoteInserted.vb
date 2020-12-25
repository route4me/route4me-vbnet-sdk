Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get activities with the event Note Inserted
        ''' </summary>
        Public Sub SearchNoteInserted()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            Dim routeId As String = SD10Stops_route_id

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim addressId As Integer = CInt(SD10Stops_route.Addresses(2).RouteDestinationId)

            Dim noteParams = New NoteParameters() With {
                .AddressId = addressId,
                .RouteId = routeId,
                .Latitude = SD10Stops_route.Addresses(2).Latitude,
                .Longitude = SD10Stops_route.Addresses(2).Longitude,
                .DeviceType = "web",
                .StrNoteContents = "Note example for Destination",
                .ActivityType = "dropoff"
            }

            Dim errorString0 As String = Nothing
            Dim addrssNote = route4Me.AddAddressNote(noteParams, errorString0)

            If addrssNote Is Nothing OrElse addrssNote.[GetType]() <> GetType(AddressNote) Then
                Console.WriteLine("Cannot add a note to the address." & Environment.NewLine & errorString0)
                RemoveTestOptimizations()
                Return
            End If

            Dim activityParameters = New ActivityParameters With {
                .ActivityType = "note-insert",
                .RouteId = routeId
            }

            Dim errorString As String = Nothing
            Dim activities As Activity() = route4Me.GetActivities(activityParameters, errorString)

            PrintExampleActivities(activities, errorString)
        End Sub
    End Class
End Namespace