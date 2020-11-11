Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get an address book group
        ''' </summary>
        Public Sub GetAddressBookGroup()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateAddressBookGroup()

            Dim groupId As String = addressBookGroupsToRemove(addressBookGroupsToRemove.Count - 1)

            Dim addressBookGroupParameters = New AddressBookGroupParameters() With {
                .GroupId = groupId
            }

            Dim errorString As String = Nothing
            Dim addressBookGroup = route4Me.
                GetAddressBookGroup(addressBookGroupParameters, errorString)

            Console.WriteLine(
                If(
                    addressBookGroup Is Nothing AndAlso addressBookGroup.[GetType]() <> GetType(AddressBookGroup),
                    "Cannot retrieve the addres group " & groupId & Environment.NewLine & errorString,
                    "Retrieved the address book group " & groupId
                  )
             )

            RemoveAddressBookGroups()
        End Sub
    End Class
End Namespace