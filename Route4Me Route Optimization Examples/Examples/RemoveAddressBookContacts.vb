Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub RemoveAddressBookContacts(addressIds As String())
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim removed As Boolean = route4Me.RemoveAddressBookContacts(addressIds, errorString)

            Console.WriteLine("")

            If removed Then
                Console.WriteLine("RemoveAddressBookContacts executed successfully, {0} contacts deleted", addressIds.Length)
            Else
                Console.WriteLine("RemoveAddressBookContacts error: {0}", errorString)
            End If

        End Sub
    End Class
End Namespace
