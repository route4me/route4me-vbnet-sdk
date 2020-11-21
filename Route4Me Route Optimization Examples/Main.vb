Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System
Imports System.Collections.Generic

Namespace Route4MeSDKTest
    Public Module Main
        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()

            examples.SingleDriverRoute10Stops()
            examples.AddRouteDestinations()
            examples.RemoveRouteDestination()
            examples.RemoveDestinationFromOptimization()
            examples.SingleDriverRoundTrip()
            examples.MoveDestinationToRoute()
            examples.SingleDriverRoundTripGeneric()
            examples.MultipleDepotMultipleDriver()
            examples.MultipleDepotMultipleDriverTimeWindow()
            examples.SingleDepotMultipleDriverNoTimeWindow()
            examples.MultipleDepotMultipleDriverWith24StopsTimeWindow()
            examples.SingleDriverMultipleTimeWindows()
            examples.GetOptimization()
            examples.GetOptimizations()
            examples.AddDestinationToOptimization()
            examples.ReOptimization()

            examples.UpdateRoute()
            examples.ReoptimizeRoute()
            examples.GetRoute()
            examples.GetRoutes()
            examples.DuplicateRoute()

            examples.GetUsers()

            examples.GetActivities()

            examples.GetAddress()

            examples.AddAddressNote()
            examples.GetAddressNotes()

            examples.DeleteRoutes()

            Dim contact1 As AddressBookContact = examples.contact1
            Dim contact2 As AddressBookContact = examples.contact2

            examples.CreateTestContacts()

            Console.WriteLine("contact1 and contact2 were added")

            examples.GetAddressBookContacts()
            If contact1 IsNot Nothing Then
                contact1.last_name = "Updated " + (New Random()).[Next]().ToString()
                examples.UpdateAddressBookContact(contact1)
            Else
                Console.WriteLine("contact1 == null. UpdateAddressBookContact not called.")
            End If
            Dim addressIdsToRemove As New List(Of String)()
            If contact1 IsNot Nothing Then
                addressIdsToRemove.Add(contact1.address_id)
            End If
            If contact2 IsNot Nothing Then
                addressIdsToRemove.Add(contact2.address_id)
            End If
            examples.RemoveAddressBookContacts(addressIdsToRemove.ToArray())

            ' Avoidance Zones
            Dim territoryId As String = examples.AddAvoidanceZone()
            examples.GetAvoidanceZones()
            If territoryId IsNot Nothing Then
                examples.GetAvoidanceZone(territoryId)
            Else
                Console.WriteLine("GetAvoidanceZone not called. territoryId == null.")
            End If
            If territoryId IsNot Nothing Then
                examples.UpdateAvoidanceZone(territoryId)
            Else
                Console.WriteLine("UpdateAvoidanceZone not called. territoryId == null.")
            End If
            If territoryId IsNot Nothing Then
                examples.DeleteAvoidanceZone(territoryId)
            Else
                Console.WriteLine("DeleteAvoidanceZone not called. territoryId == null.")
            End If

            'disabled by default, not necessary for optimization tests
            'not all accounts are capable of storing gps data
            'if (routeId_SingleDriverRoute10Stops != null)
            '{
            '  examples.SetGPSPosition(routeId_SingleDriverRoute10Stops);
            '  examples.TrackDeviceLastLocationHistory(routeId_SingleDriverRoute10Stops);
            '}
            'else
            '{
            '  System.Console.WriteLine("SetGPSPosition, TrackDeviceLastLocationHistory not called. routeId_SingleDriverRoute10Stops == null.");
            '}

            ' Orders
            examples.AddOrder()
            examples.UpdateOrder()
            examples.GetOrders()
            examples.RemoveOrders()

            examples.GenericExample()
            examples.GenericExampleShortcut()

            Console.WriteLine("Press any key")
            Console.ReadKey()
        End Sub
    End Module
End Namespace
