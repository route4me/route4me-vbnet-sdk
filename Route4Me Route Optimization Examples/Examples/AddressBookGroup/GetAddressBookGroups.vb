Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get the address book groups
        ''' </summary>
        Public Sub GetAddressBookGroups()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim addressBookGroupParameters = New AddressBookGroupParameters() With {
                .Limit = 10,
                .Offset = 0
            }

            Dim errorString As String = Nothing
            Dim groups As AddressBookGroup() = route4Me.
                GetAddressBookGroups(addressBookGroupParameters, errorString)

            Console.WriteLine(
                If(
                    groups Is Nothing AndAlso groups.[GetType]() <> GetType(AddressBookGroup()),
                    "Cannot retrieve the addres groups." & Environment.NewLine & errorString,
                    "Retrieved the address book groups: " & groups.Length
                  )
            )

            RemoveAddressBookGroups()
        End Sub
    End Class
End Namespace
