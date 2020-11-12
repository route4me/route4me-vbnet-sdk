Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add an address note to the user account.
        ''' </summary>
        Public Sub AddAddressNote()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunSingleDriverRoundTrip()

            OptimizationsToRemove = New List(Of String)() From {
                SDRT_optimization_problem_id
            }

            Dim routeIdToMoveTo As String = SDRT_route_id

            Dim addressId As Integer = CInt(SDRT_route.Addresses(1).RouteDestinationId)

            Dim lat As Double = If(SDRT_route.Addresses.Length > 1, SDRT_route.Addresses(1).Latitude, 33.132675170898)
            Dim lng As Double = If(SDRT_route.Addresses.Length > 1, SDRT_route.Addresses(1).Longitude, -83.244743347168)

            Dim noteParameters = New NoteParameters() With {
                .RouteId = routeIdToMoveTo,
                .AddressId = addressId,
                .Latitude = lat,
                .Longitude = lng,
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .ActivityType = StatusUpdateType.DropOff.GetEnumDescription()
            }

            Dim contents As String = "Test Note Contents " & DateTime.Now.ToString()

            Dim errorString As String = Nothing
            Dim note = route4Me.AddAddressNote(noteParameters, contents, errorString)

            PrintExampleAddressNote(note, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
