Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Remove specified address book group
        ''' </summary>
        Public Sub RemoveAddressBookGroup()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateAddressBookGroup()

            Dim groupId As String = addressBookGroupsToRemove(addressBookGroupsToRemove.Count - 1)

            Dim addressGroupParams = New AddressBookGroupParameters() With {
                .group_id = groupId
            }

            Dim errorString As String = Nothing
            Dim response = route4Me.RemoveAddressBookGroup(addressGroupParams, errorString)

            Console.WriteLine(
                If(
                    If(response?.Status, False),
                    "Removed the address book group " & groupId,
                    "Cannot remove the address book group " & groupId
                )
             )
        End Sub
    End Class
End Namespace
