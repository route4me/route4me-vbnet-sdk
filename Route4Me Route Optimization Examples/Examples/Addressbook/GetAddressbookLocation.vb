Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetAddressbookLocation()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            Dim addressBookParameters As New AddressBookParameters() With { _
                .Query = "david", _
                .Offset = 0, _
                .Limit = 20 _
            }

            ' Run the query
            Dim total As UInteger
            Dim errorString As String = ""
            Dim contacts As AddressBookContact() = route4Me.GetAddressBookLocation(addressBookParameters, total, errorString)

            Console.WriteLine("")

            If contacts IsNot Nothing Then
                Console.WriteLine("GetAddressbookLocation executed successfully, {0} contacts returned, total = {1}", contacts.Length, total)

                Console.WriteLine("")
            Else
                Console.WriteLine("GetAddressbookLocation error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace

