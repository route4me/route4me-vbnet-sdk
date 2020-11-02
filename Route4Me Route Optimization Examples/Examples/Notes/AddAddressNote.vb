Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddAddressNote(routeId As String, addressId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim noteParameters As New NoteParameters() With { _
                .RouteId = routeId, _
                .AddressId = addressId, _
                .Latitude = 33.132675170898, _
                .Longitude = -83.244743347168, _
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web), _
                .ActivityType = EnumHelper.GetEnumDescription(StatusUpdateType.DropOff) _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim contents As String = "Test Note Contents " + DateTime.Now.ToString()
            Dim note As AddressNote = route4Me.AddAddressNote(noteParameters, contents, errorString)

            Console.WriteLine("")

            If note IsNot Nothing Then
                Console.WriteLine("AddAddressNote executed successfully")

                Console.WriteLine("Note ID: {0}", note.NoteId)
            Else
                Console.WriteLine("AddAddressNote error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
