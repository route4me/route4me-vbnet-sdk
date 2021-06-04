Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Reflection
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add a file note to the route address.
        ''' </summary>
        Public Sub AddAddressNoteWithFile()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim noteParameters = New NoteParameters() With {
                .RouteId = SD10Stops_route_id,
                .AddressId = CInt(SD10Stops_route.Addresses(1).RouteDestinationId),
                .Latitude = 33.132675170898,
                .Longitude = -83.244743347168,
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .ActivityType = StatusUpdateType.DropOff.GetEnumDescription()
            }

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

            Dim contents As String = "Test Note Contents with Attachment " & DateTime.Now.ToString()

            Dim errorString As String = Nothing
            Dim note As AddressNote = route4Me.AddAddressNote(
                    noteParameters,
                    contents,
                    tempFilePath,
                    errorString
                )

            PrintExampleAddressNote(note, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
