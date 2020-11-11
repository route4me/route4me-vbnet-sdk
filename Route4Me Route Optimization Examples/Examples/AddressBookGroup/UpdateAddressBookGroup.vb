Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Update an address book group
        ''' </summary>
        Public Sub UpdateAddressBookGroup()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            CreateAddressBookGroup()

            Dim groupId As String = addressBookGroupsToRemove(addressBookGroupsToRemove.Count - 1)

            Dim addressBookGroupRule = New AddressBookGroupRule() With {
                .ID = "address_1",
                .Field = "address_1",
                .[Operator] = "not_equal",
                .Value = "qwerty1234567"
            }

            Dim addressBookGroupFilter = New AddressBookGroupFilter() With {
                .Condition = "AND",
                .Rules = New AddressBookGroupRule() {addressBookGroupRule}
            }

            Dim addressBookGroupParameters = New AddressBookGroup() With {
                .groupID = groupId,
                .groupColor = "cd74e6",
                .Filter = addressBookGroupFilter
            }

            Dim errorString As String = Nothing
            Dim addressBookGroup = route4Me.
                UpdateAddressBookGroup(addressBookGroupParameters, errorString)

            If addressBookGroup Is Nothing AndAlso addressBookGroup.[GetType]() <> GetType(AddressBookGroup) Then
                Console.WriteLine("Cannot update the address book group " & groupId)
            Else
                Console.WriteLine(
                    If(
                        addressBookGroup.groupColor = "cd74e6",
                        "Updated the color of the address book group " & groupId,
                        "Cannot update the color of the address book group " & groupId
                    )
                )
            End If

            RemoveAddressBookGroups()
        End Sub
    End Class
End Namespace