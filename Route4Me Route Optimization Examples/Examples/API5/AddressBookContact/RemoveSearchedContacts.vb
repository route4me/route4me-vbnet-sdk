Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub RemoveSearchedContacts()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)
            Dim addressBookParameters = New AddressBookParameters With {
                .Query = "ToDelete",
                .Offset = 0,
                .Limit = 500
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim response = route4Me.GetAddressBookContacts(addressBookParameters, resultResponse)

            Dim addressIDs As Integer() = response.Results.Where(Function(x) x.AddressId IsNot Nothing).[Select](Function(x) CInt(x.AddressId)).ToArray()
            Console.WriteLine("Length:", String.Join(",", addressIDs))

            Dim removed = route4Me.RemoveAddressBookContacts(addressIDs, resultResponse)
            Console.WriteLine(If(resultResponse Is Nothing, addressIDs.Length & " contacts removed from database", "Cannot remove " & addressIDs.Length & " contacts." & Environment.NewLine & "Exit code: " + (If(resultResponse?.ExitCode.ToString(), "")) + Environment.NewLine & "Code: " + (If(resultResponse?.Code.ToString(), "")) + Environment.NewLine & "Status: " + (If(resultResponse?.Status.ToString(), "")) + Environment.NewLine))

            If resultResponse IsNot Nothing Then

                For Each msg In resultResponse.Messages
                    Console.WriteLine(msg.Key & ": " & String.Join(", ", msg.Value) + Environment.NewLine)
                Next
            End If

            Console.WriteLine("=======================================")
        End Sub
    End Class
End Namespace