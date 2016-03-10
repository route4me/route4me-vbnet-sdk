Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub UpdateAddressBookContact(contact As AddressBookContact)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim updatedContact As AddressBookContact = route4Me.UpdateAddressBookContact(contact, errorString)

            Console.WriteLine("")

            If updatedContact IsNot Nothing Then
                Console.WriteLine("UpdateAddressBookContact executed successfully")
            Else
                Console.WriteLine("UpdateAddressBookContact error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
