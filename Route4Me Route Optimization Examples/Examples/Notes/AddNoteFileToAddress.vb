Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.IO
Imports System.Reflection
Imports System.CodeDom.Compiler
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub AddAddressNoteWithFile(routeId As String, addressId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim noteParameters As New NoteParameters() With { _
                .RouteId = routeId, _
                .AddressId = addressId, _
                .Latitude = 33.132675170898, _
                .Longitude = -83.244743347168, _
                .DeviceType = DataTypes.DeviceType.Web, _
                .ActivityType = StatusUpdateType.DropOff _
            }

            Dim tempFilePath As String = Nothing
            Using stream As Stream = File.Open("test.png", FileMode.Open)
                Dim tempFiles = New TempFileCollection()
                If True Then
                    tempFilePath = tempFiles.AddExtension("png")
                    System.Console.WriteLine(tempFilePath)
                    Using fileStream As Stream = File.OpenWrite(tempFilePath)
                        stream.CopyTo(fileStream)
                    End Using
                End If
            End Using

            ' Run the query
            Dim errorString As String = ""
            Dim contents As String = "Test Note Contents with Attachment " + DateTime.Now.ToString()
            Dim note As AddressNote = route4Me.AddAddressNote(noteParameters, contents, tempFilePath, errorString)

            Console.WriteLine("")

            If note IsNot Nothing Then
                Console.WriteLine("AddAddressNoteWithFile executed successfully")

                Console.WriteLine("Note ID: {0}", note.NoteId)
            Else
                Console.WriteLine("AddAddressNoteWithFile error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace