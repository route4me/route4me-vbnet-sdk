Imports Route4Me_Route_Optimization_Examples.Route4MeSDKTest.Examples
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System
Imports System.Collections.Generic

Namespace Route4MeSDKTest
    Public Module Main
        Public Sub Main()
            Dim examples = New Route4MeExamples()

            ' Available values for the variable executeOption
            ' "api4" - execute all the examples related to the API 4 
            ' "api5" - execute all the examples related to the API 5 
            ' a method name - execute a specifed example method (e.g. "GetTeamMemberById")
            Dim executeOption As String = "TrackDeviceLastLocationHistory"

            If executeOption.ToLower() = "api4" Then
#Region "API 4"
#Region "==== Optimizations ====="
                examples.GetOptimization()
                examples.GetOptimizationsByText()
                examples.GetOptimizationsFromDateRange()
                examples.OptimizationWithCallbackUrl()
                examples.GetOptimizations()
                examples.ReOptimization()
                examples.RemoveOptimization()
                examples.UpdateOptimizationDestination()
                examples.ExampleOptimization()
                examples.SingleDriverRoute7Stops()
                examples.SingleDriverRoundTripGeneric()
                'examples.HybridOptimizationFrom1000Orders()
                'examples.HybridOptimizationFrom1000Addresses()
                Dim dataobject = examples.AsyncMultipleDepotMultipleDriver().GetAwaiter().GetResult()
#End Region
#Region "==== Route Examples ===="
                examples.BundledAddresses()
                examples.GetScheduleCalendar()
                examples.MultipleDepotMultipleDriverFineTuning()
                examples.MultipleDepotMultipleDriver()
                examples.MultipleDepotMultipleDriverTimeWindow()
                examples.MultipleDepotMultipleDriverWith24StopsTimeWindow()
                examples.MultipleSeparateDepostMultipleDriver()
                examples.Route300Stops()
                examples.SingleDriverRoute10Stops()
                examples.RouteSlowdown()
                examples.SingleDriverRoundTrip()
                examples.SingleDepotMultipleDriverNoTimeWindow()
                examples.SingleDriverMultipleTimeWindows()
                examples.SingleDriverRoundTripGeneric()
#End Region
#Region "==== Route Addresses ====="
                examples.MoveDestinationToRoute()
                examples.AddDestinationToOptimization()
                examples.UpdateRouteDestination()
                examples.AddRouteDestinations()
                examples.GetAddress()
                examples.MarkAddressAsMarkedAsDeparted()
                examples.MarkAddressAsMarkedAsVisited()
                examples.MarkAddressDeparted()
                examples.MarkAddressVisited()
                examples.RemoveDestinationFromOptimization()
                examples.RemoveRouteDestination()
                'examples.ResequenceRouteDestinations()
                examples.AddRouteDestinationInSpecificPosition()
#End Region
#Region "==== Address Notes ===="
                examples.AddAddressNote()
                examples.AddAddressNoteWithFile()
                examples.AddComplexAddressNote()
                examples.AddCustomNoteToRoute()
                examples.AddCustomNoteType()
                examples.GetAddressNotes()
                examples.GetAllCustomNoteTypes()
                examples.RemoveCustomNoteType()
#End Region
#Region "==== Routes ===="
                examples.AssignMemberToRoute()
                examples.AssignVehicleToRoute()
                examples.ChangeRouteDepote()
                examples.DeleteRoutes()
                examples.DuplicateRoute()
                examples.GetRouteDirections()
                examples.GetRoutePathPoints()
                examples.GetRoute()
                examples.GetRoutesByIDs()
                examples.GetRoutesFromDateRange()
                examples.GetRoutes()
                examples.ReoptimizeRoute()
                examples.ResequenceReoptimizeRoute()
                'examples.ResequenceRouteDestinations()
                examples.RouteOriginParameter()
                examples.ShareRoute()
                examples.UnlinkRouteFromOptimization()
                examples.UpdateRouteAvoidanceZones()
                examples.UpdateRouteCustomData()
                examples.UpdateRoute()
                examples.UpdateRouteDestination()
                examples.UpdateWholeRoute()
                examples.SearchRoutesForText()
                examples.MergeRoutes()
#End Region
#Region "==== Activities ===="
                examples.GetActivities()
                examples.GetActivitiesByMember()
                examples.GetRouteTeamActivities()
                examples.GetLastActivities()
                examples.LogCustomActivity()
                examples.SearchAreaUpdated()
                examples.SearchAreaAdded()
                examples.SearchAreaRemoved()
                examples.SearchDestinationDeleted()
                examples.SearchDestinationInserted()
                examples.SearchDestinationMarkedAsDeparted()
                examples.SearchDestinationOutSequence()
                examples.SearchDestinationUpdated()
                examples.SearchDriverArrivedEarly()
                examples.SearchDriverArrivedLate()
                examples.SearchDriverArrivedOnTime()
                examples.SearchGeofenceEntered()
                examples.SearchGeofenceLeft()
                examples.SearchInsertDestinationAll()
                examples.SearchMarkDestinationDepartedAll()
                examples.SearchMarkDestinationVisited()
                examples.SearchMemberCreated()
                examples.SearchMemberDeleted()
                examples.SearchMemberModified()
                examples.SearchMoveDestination()
                examples.SearchNoteInserted()
                examples.SearchNoteInsertedAll()
                examples.SearchRouteDeleted()
                examples.SearchRouteOptimized()
                examples.SearchRouteOwnerChanged()

                examples.GetRouteTeamActivities()
                examples.SearchAreaAdded()
#End Region
#Region "==== Address Book Contacts ===="
                examples.AddAddressBookContact()
                examples.AddScheduledAddressBookContact()
                examples.GetAddressBookContacts()
                examples.SearchLocationsByText()

                examples.AddAddressBookContact()
                examples.UpdateAddressBookContact()
                examples.UpdateWholeAddressBookContact()

                examples.SearchRoutedLocations()
                examples.SearchLocationsByIDs()
                examples.RemoveAddressBookContacts()
                examples.GetSpecifiedFieldsSearchText()
#End Region
#Region "==== Address Book Groups ===="
                examples.AddAddressBookGroup()
                examples.GetAddressBookContactsByGroup()
                examples.GetAddressBookGroup()
                examples.GetAddressBookGroups()
                examples.RemoveAddressBookGroup()
                examples.SearchAddressBookContactsByFilter()
                examples.UpdateAddressBookGroup()
#End Region
#Region "==== User Configuration ===="
                examples.AddNewConfigurationKey()
                examples.AddConfigurationKeyArray()
                examples.GetAllConfigurationData()
                examples.GetSpecificConfigurationKeyData()
                examples.RemoveConfigurationKey()
                examples.UpdateConfigurationKey()
#End Region
#Region "==== Territories ===="
                examples.UpdateTerritory()
                examples.RemoveTerritory()
                examples.GetTerritory()
                examples.GetTerritories()
                examples.CreateRectTerritory()
                examples.CreatePolygonTerritory()
                examples.CreateTerritory()
#End Region
#Region "==== Avoidance Zones ===="
                examples.AddAvoidanceZone()
                examples.AddPolygonAvoidanceZone()
                examples.AddRectAvoidanceZone()
                examples.DeleteAvoidanceZone()
                examples.GetAvoidanceZone()
                examples.GetAvoidanceZones()
                examples.UpdateAvoidanceZone()
#End Region
#Region "==== Vehicles ===="
                examples.GetVehicles()
#End Region
#Region "==== Users ===="
                examples.ValidateSession()
                examples.UserRegistration()
                examples.UserAuthentication()
                examples.UpdateUser()
                examples.GetUserById()
                examples.DeleteUser()
                examples.CreateUser()
#End Region
#Region "==== Tracking ===="
                examples.FindAsset()
                examples.GetAllUserLocations()
                examples.GetDeviceHistoryTimeRange()
                examples.QueryUserLocations()
                examples.SetGPSPosition()
                examples.TrackDeviceLastLocationHistory()
#End Region
#Region "==== Geocoding ===="
                examples.GeocodingForward()
                examples.BatchGeocodingForward()
                examples.BatchGeocodingForwardAsync()
                examples.ReverseGeocoding()
                examples.uploadAndGeocodeLargeJsonFile()

                examples.RapidStreetDataAll()
                examples.RapidStreetDataLimited()
                examples.RapidStreetDataSingle()

                examples.RapidStreetServiceAll()
                examples.RapidStreetServiceLimited()

                examples.RapidStreetZipcodeAll()
                examples.RapidStreetZipcodeLimited()
#End Region
#Region "==== Orders ===="
                examples.AddOrder()
                examples.AddOrdersToOptimization()
                examples.AddOrdersToRoute()
                examples.AddScheduledOrder()
                examples.CreateOrderWithCustomField()
                examples.GetOrderByID()
                examples.GetOrdersByInsertedDate()
                examples.GetOrdersByScheduledDate()
                examples.GetOrdersByCustomFields()
                examples.GetOrdersByScheduleFilter()
                examples.GetOrdersBySpecifiedText()
                examples.RemoveOrders()
                examples.GetOrders()
                examples.UpdateOrder()
                examples.UpdateOrderWithCustomField()
#End Region
#Region "==== Custom User Order Fields ===="
                examples.CreateOrderCustomUserField()
                examples.GetOrderCustomUserFields()
                examples.RemoveOrderCustomUserField()
                examples.updateOrderCustomUserField()
#End Region
#Region "==== Telematics Vendors ===="
                examples.GetAllVendors()
                examples.GetVendor()
                examples.SearchVendors()
                examples.VendorsComparison()
#End Region
#End Region
            ElseIf executeOption.ToLower() = "api5" Then
#Region "API 5"

#Region "Team Management"
                'examples.GetTeamMembers()
                'examples.GetTeamMemberById()
                'examples.RemoveTeamMember()
                'examples.UpdateTeamMember()
                'examples.CreateTeamMember()
                'examples.BulkCreateTeamMembers()
                'examples.AddSkillsToDriver()
#End Region

#Region "Driver Rating"
                'examples.GetDriverReviewList()
                'examples.GetDriverReviewById()
                'examples.CreateDriverReview()
                'examples.UpdateDriverReview()
#End Region

#Region "Route Types"
                'examples.CreateOptimizationWithDriverSkills()
#End Region

#End Region
            Else
                Try
                    GetType(Route4MeExamples).GetMethod(executeOption).Invoke(examples, Nothing)
                Catch ex As Exception
                    Console.WriteLine(executeOption & " error: {0}", ex.Message)
                End Try
            End If

            System.Console.WriteLine("")
            System.Console.WriteLine("Press any key")
            System.Console.ReadKey()
        End Sub
    End Module
End Namespace
