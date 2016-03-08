Imports Route4MeSDK.DataTypes
Imports System
Imports System.Collections.Generic
Namespace Route4MeSDKTest
    Public Module Module1

        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()
            Dim contact1 As AddressBookContact = examples.AddAddressBookContact()
            Dim contact2 As AddressBookContact = examples.AddAddressBookContact()

            Dim dataObject As DataObject = Nothing

            Dim dataObject1 As DataObject = examples.SingleDriverRoute10Stops()

            Console.WriteLine("contact1 and contact2 were added")

            Dim dataObject2 As DataObject = examples.SingleDriverRoundTrip()
            DataObject = dataObject2

            Dim routeId_SingleDriverRoundTrip As String = If((DataObject IsNot Nothing AndAlso DataObject.Routes IsNot Nothing AndAlso DataObject.Routes.Length > 0), DataObject.Routes(0).RouteID, Nothing)

            Dim routeIdToMoveTo As String = routeId_SingleDriverRoundTrip

            Dim routeDestinationIdToMove As Integer = If((dataObject1 IsNot Nothing AndAlso dataObject1.Routes IsNot Nothing AndAlso dataObject1.Routes.Length > 0 AndAlso dataObject1.Routes(0).Addresses.Length > 1 AndAlso dataObject1.Routes(0).Addresses(1).RouteDestinationId IsNot Nothing), dataObject1.Routes(0).Addresses(1).RouteDestinationId.Value, 0)

            If routeIdToMoveTo IsNot Nothing AndAlso routeDestinationIdToMove <> 0 Then
                examples.GetAddress(routeIdToMoveTo, routeDestinationIdToMove)

                examples.AddAddressNote(routeIdToMoveTo, routeDestinationIdToMove)
                examples.GetAddressNotes(routeIdToMoveTo, routeDestinationIdToMove)
            Else
                System.Console.WriteLine("AddAddressNote, GetAddress, GetAddressNotes not called. routeIdToMoveTo == null || routeDestinationIdToMove == 0.")
            End If
        End Sub

    End Module
End Namespace
