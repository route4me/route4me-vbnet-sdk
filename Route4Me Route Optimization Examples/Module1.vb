Imports Route4MeSDK.DataTypes
Imports System
Imports System.Collections.Generic
Namespace Route4MeSDKTest
    Public Module Module1

        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()
            Dim contact1 As AddressBookContact = examples.AddAddressBookContact()
            Dim contact2 As AddressBookContact = examples.AddAddressBookContact()

            Console.WriteLine("contact1 and contact2 were added")
        End Sub

    End Module
End Namespace
