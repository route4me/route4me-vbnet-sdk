Imports System.CodeDom.Compiler
Imports System.IO
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update route by sending a modified whole route object.
        ''' </summary>
        Public Sub UpdateWholeRoute()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeId As String = SD10Stops_route_id
            Dim initialRoute = R4MeUtils.ObjectDeepClone(Of DataObjectRoute)(SD10Stops_route)

#Region "Notes, Custom Type Notes, Note File Uploading"

            Dim errorString5 As String = Nothing
            Dim customNotesResponse = route4Me.getAllCustomNoteTypes(errorString5)

            Dim allCustomNotes = If(
                customNotesResponse IsNot Nothing AndAlso
                customNotesResponse.[GetType]() = GetType(CustomNoteType()),
                    CType(customNotesResponse, CustomNoteType()),
                    Nothing
            )

            Dim tempFilePath As String = Nothing

            Using stream As Stream = File.Open("test.png", FileMode.Open)
                Dim tempFiles = New TempFileCollection()

                If True Then
                    tempFilePath = tempFiles.AddExtension("png")

                    Console.WriteLine(tempFilePath)

                    Using fileStream As Stream = File.OpenWrite(tempFilePath)
                        stream.CopyTo(fileStream)
                    End Using
                End If
            End Using

            SD10Stops_route.Addresses(1).Notes = New AddressNote() {New AddressNote() With {
                .NoteId = -1,
                .RouteId = SD10Stops_route.RouteID,
                .Latitude = SD10Stops_route.Addresses(1).Latitude,
                .Longitude = SD10Stops_route.Addresses(1).Longitude,
                .ActivityType = "dropoff",
                .Contents = "C# SDK Test Content",
                .CustomTypes = If(
                                    allCustomNotes.Length > 0,
                                    New AddressCustomNote() {New AddressCustomNote() With {
                                        .NoteCustomTypeID = allCustomNotes(0).NoteCustomTypeID.ToString(),
                                        .NoteCustomValue = allCustomNotes(0).NoteCustomTypeValues(0)
                                    }},
                                    Nothing
                                ),
                .UploadUrl = tempFilePath
            }}

            Dim errorString0 As String = Nothing
            Dim updatedRoute0 = route4Me.UpdateRoute(
                                            SD10Stops_route,
                                            initialRoute,
                                            errorString0)

            If (If(updatedRoute0?.Addresses(1)?.Notes.Length, 0)) <> 1 Then
                Console.WriteLine("UpdateRouteTest failed: cannot create a note")
            End If

            If allCustomNotes.Length > 0 Then
                Assert.IsTrue(
                    updatedRoute0.Addresses(1).Notes(0).CustomTypes.Length = 1,
                    "UpdateRouteTest failed: cannot create a custom type note"
                )
            End If

            Assert.IsTrue(
                updatedRoute0.Addresses(1).Notes(0).UploadId.Length = 32,
                "UpdateRouteTest failed: cannot create a custom type note"
            )

#End Region

            SD10Stops_route.ApprovedForExecution = True
            SD10Stops_route.Parameters.RouteName += " Edited"
            SD10Stops_route.Parameters.Metric = Metric.Manhattan
            SD10Stops_route.Addresses(1).AddressString += " Edited"
            SD10Stops_route.Addresses(1).Group = "Example Group"
            SD10Stops_route.Addresses(1).CustomerPo = "CPO 456789"
            SD10Stops_route.Addresses(1).InvoiceNo = "INO 789654"
            SD10Stops_route.Addresses(1).ReferenceNo = "RNO 313264"
            SD10Stops_route.Addresses(1).OrderNo = "ONO 654878"

            SD10Stops_route.Addresses(1).Notes = New AddressNote() {New AddressNote() With {
                .RouteDestinationId = -1,
                .RouteId = SD10Stops_route.RouteID,
                .Latitude = SD10Stops_route.Addresses(1).Latitude,
                .Longitude = SD10Stops_route.Addresses(1).Longitude,
                .ActivityType = "dropoff",
                .Contents = "C# SDK Test Content"
            }}

            SD10Stops_route.Addresses(2).SequenceNo = 5

            Dim addressID = SD10Stops_route.Addresses(2).RouteDestinationId

            Dim errorString As String = Nothing
            Dim dataObject = route4Me.UpdateRoute(SD10Stops_route, initialRoute, errorString)

            Assert.IsTrue(
                dataObject.Addresses.
                Where(Function(x) x.RouteDestinationId = addressID).
                FirstOrDefault().
                SequenceNo = 5,
                "UpdateWholeRouteTest failed."
            )
            Assert.IsTrue(
                SD10Stops_route.ApprovedForExecution,
                "UpdateRouteTest failed, ApprovedForExecution cannot set to true"
            )
            Assert.IsNotNull(dataObject, "UpdateRouteTest failed. " & errorString)
            Assert.IsTrue(
                dataObject.Parameters.RouteName.Contains("Edited"),
                "UpdateRouteTest failed, the route name not changed."
            )
            Assert.IsTrue(
                dataObject.Addresses(1).AddressString.Contains("Edited"),
                "UpdateRouteTest failed, second address name not changed."
            )
            Assert.IsTrue(
                dataObject.Addresses(1).Group = "Example Group",
                "UpdateWholeRouteTest failed."
            )
            Assert.IsTrue(
                dataObject.Addresses(1).CustomerPo = "CPO 456789",
                "UpdateWholeRouteTest failed."
            )
            Assert.IsTrue(
                dataObject.Addresses(1).InvoiceNo = "INO 789654",
                "UpdateWholeRouteTest failed."
            )
            Assert.IsTrue(
                dataObject.Addresses(1).ReferenceNo = "RNO 313264",
                "UpdateWholeRouteTest failed."
            )
            Assert.IsTrue(
                dataObject.Addresses(1).OrderNo = "ONO 654878",
                "UpdateWholeRouteTest failed."
            )

            initialRoute = R4MeUtils.ObjectDeepClone(Of DataObjectRoute)(SD10Stops_route)

            SD10Stops_route.ApprovedForExecution = False
            SD10Stops_route.Addresses(1).Group = Nothing
            SD10Stops_route.Addresses(1).CustomerPo = Nothing
            SD10Stops_route.Addresses(1).InvoiceNo = Nothing
            SD10Stops_route.Addresses(1).ReferenceNo = Nothing
            SD10Stops_route.Addresses(1).OrderNo = Nothing

            dataObject = route4Me.UpdateRoute(SD10Stops_route, initialRoute, errorString)

            PrintExampleRouteResult(dataObject, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace