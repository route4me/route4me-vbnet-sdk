Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get the address book contacts by specified address book group.
        ''' </summary>
        Public Sub GetAddressBookContactsByGroup()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateAddressBookGroup()

            Dim groupId As String = addressBookGroupsToRemove(addressBookGroupsToRemove.Count - 1)

            Dim addressBookGroupParameters = New AddressBookGroupParameters() With {
                .group_id = groupId,
                .Fields = New String() {"address_id"}
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.GetAddressBookContactsByGroup(addressBookGroupParameters, errorString)

            Console.WriteLine(
                    If(
                        (If(response?.results?.Length, 0)) < 1,
                        "Cannot retrieve contacts by group " & groupId & Environment.NewLine & errorString,
                        "Retrieved the contacts by group " & groupId & ": " & response.results.Length.ToString()
                    )
                )

            RemoveAddressBookGroups()
        End Sub
    End Class
End Namespace
