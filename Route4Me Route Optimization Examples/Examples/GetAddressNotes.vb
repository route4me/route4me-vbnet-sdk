Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetAddressNotes(routeId As String, routeDestinationId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim noteParameters As New NoteParameters() With { _
                .RouteId = routeId, _
                .AddressId = routeDestinationId _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim notes As AddressNote() = route4Me.GetAddressNotes(noteParameters, errorString)

            Console.WriteLine("")

            If notes IsNot Nothing Then
                Console.WriteLine("GetAddressNotes executed successfully, {0} notes returned", notes.Length)
                Console.WriteLine("")
            Else
                Console.WriteLine("GetAddressNotes error: {0}", errorString)
                Console.WriteLine("")
            End If
        End Sub
    End Class
End Namespace
