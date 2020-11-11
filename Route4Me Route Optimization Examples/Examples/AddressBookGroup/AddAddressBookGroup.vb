Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Add an address book group to the user's account.
        ''' </summary>
        Public Sub AddAddressBookGroup()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim addressBookGroupRule = New AddressBookGroupRule() With {
                .ID = "address_1",
                .Field = "address_1",
                .[Operator] = "not_equal",
                .Value = "qwerty123456"
            }

            Dim addressBookGroupFilter = New AddressBookGroupFilter() With {
                .Condition = "AND",
                .Rules = New AddressBookGroupRule() {addressBookGroupRule}
            }

            Dim addressBookGroupParameters = New AddressBookGroup() With {
                .groupName = "All Group",
                .groupColor = "92e1c0",
                .Filter = addressBookGroupFilter
            }

            Dim errorString As String = Nothing
            Dim addressBookGroup = route4Me.AddAddressBookGroup(addressBookGroupParameters, errorString)

            If addressBookGroup Is Nothing OrElse addressBookGroup.[GetType]() <> GetType(AddressBookGroup) Then
                Console.WriteLine("Cannot create an address book group." & Environment.NewLine & errorString)
                Return
            Else
                Console.WriteLine("Created an address book group " & addressBookGroup.groupID)
                AddAddressBookGroupToRemoveList(addressBookGroup.groupID)
            End If

            RemoveAddressBookGroups()
        End Sub
    End Class
End Namespace

