Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest
    Public Module Main
        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()

            Dim dataObject As DataObject = Nothing

            Dim dataObject1 As DataObject = examples.SingleDriverRoute10Stops()
            dataObject = dataObject1
            Dim routeId_SingleDriverRoute10Stops As String = If((dataObject IsNot Nothing AndAlso dataObject.Routes IsNot Nothing AndAlso dataObject.Routes.Length > 0), dataObject.Routes(0).RouteID, Nothing)

            Dim destinationIds As Integer() = examples.AddRouteDestinations(routeId_SingleDriverRoute10Stops)
            If destinationIds IsNot Nothing AndAlso destinationIds.Length > 0 Then
                examples.RemoveRouteDestination(routeId_SingleDriverRoute10Stops, destinationIds(0))
            End If

            If destinationIds IsNot Nothing AndAlso destinationIds.Length > 1 Then
                examples.RemoveAddressFromOptimization(dataObject1.OptimizationProblemId, destinationIds(1))
            End If

            Dim dataObject2 As DataObject = examples.SingleDriverRoundTrip()
            dataObject = dataObject2
            Dim routeId_SingleDriverRoundTrip As String = If((dataObject IsNot Nothing AndAlso dataObject.Routes IsNot Nothing AndAlso dataObject.Routes.Length > 0), dataObject.Routes(0).RouteID, Nothing)

            Dim routeIdToMoveTo As String = routeId_SingleDriverRoundTrip
            Dim routeDestinationIdToMove As Integer = If((dataObject1 IsNot Nothing AndAlso dataObject1.Routes IsNot Nothing AndAlso dataObject1.Routes.Length > 0 AndAlso dataObject1.Routes(0).Addresses.Length > 1 AndAlso dataObject1.Routes(0).Addresses(1).RouteDestinationId IsNot Nothing), dataObject1.Routes(0).Addresses(1).RouteDestinationId.Value, 0)
            Dim afterDestinationIdToMoveAfter As Integer = If((dataObject2 IsNot Nothing AndAlso dataObject2.Routes IsNot Nothing AndAlso dataObject2.Routes.Length > 0 AndAlso dataObject2.Routes(0).Addresses.Length > 1 AndAlso dataObject2.Routes(0).Addresses(0).RouteDestinationId IsNot Nothing), dataObject2.Routes(0).Addresses(0).RouteDestinationId.Value, 0)
            If routeIdToMoveTo IsNot Nothing AndAlso routeDestinationIdToMove <> 0 AndAlso afterDestinationIdToMoveAfter <> 0 Then
                examples.MoveDestinationToRoute(routeIdToMoveTo, routeDestinationIdToMove, afterDestinationIdToMoveAfter)
            Else
                Console.WriteLine("MoveDestinationToRoute not called. routeDestinationId = {0}, afterDestinationId = {1}.", routeDestinationIdToMove, afterDestinationIdToMoveAfter)
            End If

            Dim optimizationProblemID As String = examples.SingleDriverRoundTripGeneric()

            dataObject = examples.MultipleDepotMultipleDriver()
            Dim routeId_MultipleDepotMultipleDriver As String = If((dataObject IsNot Nothing AndAlso dataObject.Routes IsNot Nothing AndAlso dataObject.Routes.Length > 0), dataObject.Routes(0).RouteID, Nothing)

            dataObject = examples.MultipleDepotMultipleDriverTimeWindow()
            Dim routeId_MultipleDepotMultipleDriverTimeWindow As String = If((dataObject IsNot Nothing AndAlso dataObject.Routes IsNot Nothing AndAlso dataObject.Routes.Length > 0), dataObject.Routes(0).RouteID, Nothing)

            dataObject = examples.SingleDepotMultipleDriverNoTimeWindow()
            Dim routeId_SingleDepotMultipleDriverNoTimeWindow As String = If((dataObject IsNot Nothing AndAlso dataObject.Routes IsNot Nothing AndAlso dataObject.Routes.Length > 0), dataObject.Routes(0).RouteID, Nothing)

            dataObject = examples.MultipleDepotMultipleDriverWith24StopsTimeWindow()
            Dim routeId_MultipleDepotMultipleDriverWith24StopsTimeWindow As String = If((dataObject IsNot Nothing AndAlso dataObject.Routes IsNot Nothing AndAlso dataObject.Routes.Length > 0), dataObject.Routes(0).RouteID, Nothing)

            dataObject = examples.SingleDriverMultipleTimeWindows()
            Dim routeId_SingleDriverMultipleTimeWindows As String = If((dataObject IsNot Nothing AndAlso dataObject.Routes IsNot Nothing AndAlso dataObject.Routes.Length > 0), dataObject.Routes(0).RouteID, Nothing)

            If optimizationProblemID IsNot Nothing Then
                examples.GetOptimization(optimizationProblemID)
            Else
                Console.WriteLine("GetOptimization not called. optimizationProblemID == null.")
            End If

            examples.GetOptimizations()


            If optimizationProblemID IsNot Nothing Then
                examples.AddDestinationToOptimization(optimizationProblemID, True)
            Else
                Console.WriteLine("AddDestinationToOptimization not called. optimizationProblemID == null.")
            End If

            If optimizationProblemID IsNot Nothing Then
                examples.ReOptimization(optimizationProblemID)
            Else
                Console.WriteLine("ReOptimization not called. optimizationProblemID == null.")
            End If

            If routeId_SingleDriverRoute10Stops IsNot Nothing Then
                examples.GetRoute(routeId_SingleDriverRoute10Stops)
            Else
                Console.WriteLine("GetRoute not called. routeId_SingleDriverRoute10Stops == null.")
            End If

            If routeId_SingleDriverRoute10Stops IsNot Nothing Then
                examples.UpdateRoute(routeId_SingleDriverRoute10Stops)
                examples.ReoptimizeRoute(routeId_SingleDriverRoute10Stops)
                examples.GetRoute(routeId_SingleDriverRoute10Stops)
            Else
                Console.WriteLine("UpdateRoute, ReoptimizeRoute, GetRoute not called. routeId_SingleDriverRoute10Stops == null.")
            End If

            examples.GetRoutes()
            examples.GetUsers()

            examples.GetActivities()

            If routeIdToMoveTo IsNot Nothing AndAlso routeDestinationIdToMove <> 0 Then
                examples.GetAddress(routeIdToMoveTo, routeDestinationIdToMove)

                examples.AddAddressNote(routeIdToMoveTo, routeDestinationIdToMove)
                examples.GetAddressNotes(routeIdToMoveTo, routeDestinationIdToMove)
            Else
                Console.WriteLine("AddAddressNote, GetAddress, GetAddressNotes not called. routeIdToMoveTo == null || routeDestinationIdToMove == 0.")
            End If

            Dim routeId_DuplicateRoute As String = Nothing
            If routeId_SingleDriverRoute10Stops IsNot Nothing Then
                routeId_DuplicateRoute = examples.DuplicateRoute(routeId_SingleDriverRoute10Stops)
            Else
                Console.WriteLine("DuplicateRoute not called. routeId_SingleDriverRoute10Stops == null.")
            End If

            Dim routeIdsToDelete As New List(Of String)()
            If routeId_SingleDriverRoute10Stops IsNot Nothing Then
                routeIdsToDelete.Add(routeId_SingleDriverRoute10Stops)
            End If
            If routeId_SingleDriverRoundTrip IsNot Nothing Then
                routeIdsToDelete.Add(routeId_SingleDriverRoundTrip)
            End If
            If routeId_DuplicateRoute IsNot Nothing Then
                routeIdsToDelete.Add(routeId_DuplicateRoute)
            End If
            If routeId_MultipleDepotMultipleDriver IsNot Nothing Then
                routeIdsToDelete.Add(routeId_MultipleDepotMultipleDriver)
            End If
            If routeId_MultipleDepotMultipleDriverTimeWindow IsNot Nothing Then
                routeIdsToDelete.Add(routeId_MultipleDepotMultipleDriverTimeWindow)
            End If
            If routeId_SingleDepotMultipleDriverNoTimeWindow IsNot Nothing Then
                routeIdsToDelete.Add(routeId_SingleDepotMultipleDriverNoTimeWindow)
            End If
            If routeId_MultipleDepotMultipleDriverWith24StopsTimeWindow IsNot Nothing Then
                routeIdsToDelete.Add(routeId_MultipleDepotMultipleDriverWith24StopsTimeWindow)
            End If
            If routeId_SingleDriverMultipleTimeWindows IsNot Nothing Then
                routeIdsToDelete.Add(routeId_SingleDriverMultipleTimeWindows)
            End If

            If routeIdsToDelete.Count > 0 Then
                examples.DeleteRoutes(routeIdsToDelete.ToArray())
            Else
                Console.WriteLine("routeIdsToDelete.Count == 0. DeleteRoutes not called.")
            End If

            Dim contact1 As AddressBookContact = examples.AddAddressBookContact()
            Dim contact2 As AddressBookContact = examples.AddAddressBookContact()

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
            Dim order1 As Order = examples.AddOrder()
            Dim order2 As Order = examples.AddOrder()
            examples.GetOrders()
            If order1 IsNot Nothing Then
                order1.EXT_FIELD_last_name = "Updated " + (New Random()).[Next]().ToString()
                examples.UpdateOrder(order1)
            Else
                Console.WriteLine("order1 == null. UpdateOrder not called.")
            End If
            Dim orderIdsToRemove As New List(Of String)()
            If order1 IsNot Nothing Then
                orderIdsToRemove.Add(order1.order_id)
            End If
            If order2 IsNot Nothing Then
                orderIdsToRemove.Add(order2.order_id)
            End If
            examples.RemoveOrders(orderIdsToRemove.ToArray())

            examples.GenericExample()
            examples.GenericExampleShortcut()

            Console.WriteLine("Press any key")
            Console.ReadKey()
        End Sub
    End Module
End Namespace
