Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get the address notes from the route address.
        ''' </summary>
        ''' <param name="routeId">Route ID</param>
        ''' <param name="routeDestinationId">Route destination ID</param>
        Public Sub GetAddressNotes(Optional ByVal routeId As String = Nothing,
                                   Optional ByVal routeDestinationId As Integer = Nothing)

            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim isInnerExample As Boolean = If(routeId Is Nothing, True, False)

            If isInnerExample Then CreateAddressNote(routeId, routeDestinationId)

            Dim noteParameters = New NoteParameters() With {
                .RouteId = routeId,
                .AddressId = CInt(routeDestinationId)
            }

            Dim errorString As String = Nothing
            Dim notes As AddressNote() = route4Me.GetAddressNotes(noteParameters, errorString)

            Console.WriteLine("")

            If notes IsNot Nothing Then
                Console.WriteLine("GetAddressNotes executed successfully, {0} notes returned", notes.Length)
                Console.WriteLine("")
            Else
                Console.WriteLine("GetAddressNotes error: {0}", errorString)
                Console.WriteLine("")
            End If

            If isInnerExample Then RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
