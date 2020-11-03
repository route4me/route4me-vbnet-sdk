Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System
Imports System.Collections.Generic

Namespace Route4MeSDKTest
    Public Module Main
        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()
            'MainFUll.MainFull()
            'Return

            '======== Update Route Destination  ============
            'examples.DeserializeDictionaryExample()
            '======================================================================

#Region "==== Member Configuration ===="
            'examples.GetSpecificConfigurationKeyData()
            'examples.GetAllConfigurationData()
            'examples.UpdateConfigurationKey()
            'examples.RemoveConfigurationKey()
            'examples.AddNewConfigurationKey()
#End Region

#Region "==== Tracking ===="
            'examples.GetDeviceHistoryTimeRange("814FB49CEA8188D134E9D4D4B8B0DAF7")
            'examples.FindAsset()

#End Region

#Region "===== Routes ===="
            'examples.SearchRoutesForText("Tbilisi")
            'examples.SingleDriverRoute_UTF8_strings()
            'examples.SingleDriverRoute7Stops()
            'examples.SingleDriverRoute10Stops()
            'examples.SearchRouteOwnerChanged()

            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'Dim RouteDestinationId = 174405640

            'Dim CustomData As New Dictionary(Of String, String)
            'CustomData.Add("animal", "tiger")
            'CustomData.Add("bird", "canary")
            'examples.UpdateRouteCustomData(RouteId, RouteDestinationId, CustomData)

            'examples.UpdateRouteDestination()

            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'Dim email As String = "oleg.guchi@gmail.com"
            'examples.RouteSharing(RouteId, email)

            ' ======== Resequence Reoptimize Route ===========================
            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'examples.ResequenceReoptimizeRoute(RouteId)
            '======================================================================

            ' ======== Resequence Route Destination ===========================
            'Dim RouteId As String = "F0C842829D8799067F9BF7A495076335"
            'Dim RouteDestinationId = 174389214
            'examples.ResequenceRouteDestination(RouteId, RouteDestinationId)
            '======================================================================

            ' ======== Get Route's Directions ===========================
            'Dim RouteId As String = "56E8F6BF949670F0C0BBAC00590FD116"

            'examples.GetRouteDirections(RouteId)
            '======================================================================

            ' ======== Get Route's Path Points ===========================
            'Dim RouteId As String = "6D1622D3F794F3A804381C9451E5A239"

            'examples.GetRoutePathPoints(RouteId)
            '======================================================================

            ' ======== Insert Address Into Route's Optimal Position ===========================
            'Dim RouteId As String = "6D1622D3F794F3A804381C9451E5A239"

            'examples.InsertAddressIntoRouteOptimalPosition(RouteId)
            '======================================================================
#End Region

#Region "==== Activities ===="
            'examples.GetRouteTeamActivities()
            'examples.GetActivitiesByMember()
            'examples.GetLastActivities()
            'examples.GetActivities()
            'examples.LogCustomActivity()
            'examples.SearchAreaUpdated()
            'examples.SearchAreaRemoved()
            'examples.SearchDestinationOutSequence()
            'examples.SearchDestinationUpdated()
            'examples.SearchDriverArrivedEarly()
            'examples.SearchDriverArrivedLate()
            'examples.SearchDriverArrivedOnTime()
            'examples.SearchGeofenceEntered()
            'examples.SearchGeofenceLeft()
            'examples.SearchInsertDestinationAll()
            'examples.SearchMarkDestinationDepartedAll()
            'examples.SearchMarkDestinationVisited()
            'examples.SearchMemberCreated()
            'examples.SearchMemberDeleted()
            'examples.SearchMemberModified()
            'examples.SearchMoveDestination()
            'examples.SearchRouteDeleted()
            'examples.SearchRouteOptimized()
            'examples.SearchAreaAdded()
#End Region

#Region "==== User ===="
            'examples.GetUserById()
            'examples.DeleteUser()
            'examples.UpdateUser()
            'examples.CreateUser()
            'examples.GetUsers()
            'examples.LogSpecificMessage()
            'examples.UserRegistration()
            'examples.ValidateSession()
            'examples.UserAuthentication()

#End Region

#Region "==== Avoidance Zone ===="
            'examples.AddRectAvoidanceZone()
            'examples.AddPolygonAvoidanceZone()
#End Region

#Region "===== Territory ===="
            'examples.UpdateTerritory()
            'examples.RemoveTerritory()
            'examples.CreateRectTerritory()
            'examples.CreatePolygonTerritory()
            'examples.GetTerritories()
            'examples.GetTerritory()
            'examples.CreateTerritory()
#End Region

#Region "==== Addressbook Locations ===="
            'examples.UpdateWholeAddressBookContact()
            'examples.UpdateAddressBookContact()
            'examples.SearchLocationsByText()
            'examples.SearchLocationsByIDs()
            'examples.RemoveAddressBookContacts()
            'examples.GetSpecifiedFieldsSearchText()
            'examples.GetAddressBookContacts()
            'examples.AddScheduledAddressBookContact()
            'examples.AddAddressBookContact()
            'examples.SearchRoutedLocations()
            'examples.GetAddressbookLocation()
#End Region

#Region "===== Get Vehicles ===="
            'examples.GetVehicles()
#End Region

#Region "==== Address Notes ===="
            'examples.SearchNoteInserted()
            'examples.SearchNoteInsertedAll()
            'examples.AddNoteFileToAddress("4728372005DE97EF9E4205852D690E34", 182302891)
#End Region

#Region "===== Geocoding ===="
            'examples.RapidStreetServiceLimited()
            'examples.RapidStreetServiceAll()
            'examples.RapidStreetZipcodeLimited()
            'examples.RapidStreetZipcodeAll()
            'examples.RapidStreetDataLimited()
            'examples.RapidStreetDataAll()
            'examples.RapidStreetDataSingle()
            'examples.ReverseGeocoding()

            'Dim geoParams As New GeocodingParameters With { _
            '     .Addresses = "Los20%Angeles20%International20%Airport,20%CA", _
            '    .Format = "xml" _
            '}
            'examples.GeocodingForward(geoParams)

#End Region

#Region "==== Mark Address ===="
            'examples.SearchDestinationMarkedAsDeparted()
            'examples.SearchDestinationInserted()
            'examples.SearchDestinationDeleted()

            ' ======== Mark Address As Visited ===========================
            'Dim aParams As New AddressParameters With { _
            '    .RouteId = "241466F15515D67D3F951E2DA38DE76D", _
            '    .AddressId = 167899269, _
            '    .IsVisited = True
            '}
            'examples.MarkAddressVisited(aParams)
            '======================================================================

            ' ======== Mark Address As Departed ===========================
            'Dim aParams As New AddressParameters With { _
            '    .RouteId = "DD376C7148E7FEE36CFABE2BD9978BDD", _
            '    .AddressId = 183045808, _
            '    .IsDeparted = True
            '}
            'examples.MarkAddressDeparted(aParams)
            '======================================================================

            ' ======== Mark Address As Marked As Visited ===========================
            'Dim aParams As New AddressParameters With { _
            '    .RouteId = "241466F15515D67D3F951E2DA38DE76D", _
            '    .RouteDestinationId = 167899269, _
            '    .IsVisited = True
            '}
            'examples.MarkAddressAsMarkedAsVisited(aParams)
            '======================================================================

            ' ======== Mark Address As Marked As Departed ===========================
            'Dim aParams As New AddressParameters With { _
            '    .RouteId = "241466F15515D67D3F951E2DA38DE76D", _
            '    .RouteDestinationId = 167899269, _
            '    .IsDeparted = True
            '}
            'examples.MarkAddressAsMarkedAsDeparted(aParams)
            '======================================================================
#End Region

#Region "==== Orders ===="
            ' ======== Search for Orders by Specified Text ===========================
            'Dim query As String = "Olman"
            'examples.GetOrdersBySpecifiedText(query)
            '======================================================================

            ' ======== Search for Orders by Custom Fields ===========================
            'Dim CustomFields As String = "order_id,member_id"
            'examples.GetOrdersByCustomFields(CustomFields)
            '======================================================================

            ' ======== Search for Orders by scheduled date ===========================
            'Dim ScheduledDate As String = ""
            'examples.GetOrderByScheduledDate(ScheduledDate)
            '======================================================================

            ' ======== Search for Orders by inserted date ===========================
            'Dim InsertedDate As String = ""
            'examples.GetOrderByInsertedDate(InsertedDate)
            '======================================================================

            ' ======== Retrieve Single Order by order_id ===========================
            'Dim OrderId As Integer = 216
            'examples.GetOrderByID(OrderId)
            '======================================================================
#End Region

#Region "===== Add Orders To an Optimization ====="
            'Dim rQueryParams As New OptimizationParameters
            'With rQueryParams
            '    .OptimizationProblemID = "7988378F70C533283BAD5024E6E37201"
            '    .Redirect = False
            'End With

            'Dim addresses As Address() = New Address() {New Address() With { _
            '    .AddressString = "273 Canal St, New York, NY 10013, USA", _
            '    .Latitude = 40.7191558, _
            '    .Longitude = -74.0011966, _
            '    .Alias = "", _
            '    .CurbsideLatitude = 40.7191558, _
            '    .CurbsideLongitude = -74.0011966, _
            '    .IsDepot = True _
            '}, New Address() With { _
            '    .AddressString = "106 Liberty St, New York, NY 10006, USA", _
            '    .Alias = "BK Restaurant #: 2446", _
            '    .Latitude = 40.709637, _
            '    .Longitude = -74.011912, _
            '    .CurbsideLatitude = 40.709637, _
            '    .CurbsideLongitude = -74.011912, _
            '    .Email = "", _
            '    .Phone = "(917) 338-1887", _
            '    .FirstName = "", _
            '    .LastName = "", _
            '    .CustomFields = New Dictionary(Of String, String) From {{"icon", Nothing}}, _
            '    .Time = 0, _
            '    .TimeWindowStart = 1472544000, _
            '    .TimeWindowEnd = 1472544300, _
            '    .OrderId = 7205705 _
            '}, New Address() With { _
            '    .AddressString = "325 Broadway, New York, NY 10007, USA", _
            '    .Alias = "BK Restaurant #: 20333", _
            '    .Latitude = 40.71615, _
            '    .Longitude = -74.00505, _
            '    .CurbsideLatitude = 40.71615, _
            '    .CurbsideLongitude = -74.00505, _
            '    .Email = "", _
            '    .Phone = "(212) 227-7535", _
            '    .FirstName = "", _
            '    .LastName = "", _
            '    .CustomFields = New Dictionary(Of String, String) From {{"icon", Nothing}}, _
            '    .Time = 0, _
            '    .TimeWindowStart = 1472545000, _
            '    .TimeWindowEnd = 1472545300, _
            '    .OrderId = 7205704 _
            '}, New Address() With { _
            '    .AddressString = "106 Fulton St, Farmingdale, NY 11735, USA", _
            '    .Alias = "BK Restaurant #: 17871", _
            '    .Latitude = 40.73073, _
            '    .Longitude = -73.459283, _
            '    .CurbsideLatitude = 40.73073, _
            '    .CurbsideLongitude = -73.459283, _
            '    .Email = "", _
            '    .Phone = "(212) 566-5132", _
            '    .FirstName = "", _
            '    .LastName = "", _
            '    .CustomFields = New Dictionary(Of String, String) From {{"icon", Nothing}}, _
            '    .Time = 0, _
            '    .TimeWindowStart = 1472546000, _
            '    .TimeWindowEnd = 1472546300, _
            '    .OrderId = 7205703 _
            '}}

            'Dim rParams As New RouteParameters

            'With rParams
            '    .RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)"
            '    .RouteDate = 1465948800
            '    .RouteTime = 14400
            '    .Optimize = "Time"
            '    .RouteType = "single"
            '    .AlgorithmType = 1
            '    .RT = False
            '    .LockLast = False
            '    .MemberId = 1
            '    .VehicleId = ""
            '    .DisableOptimization = False
            'End With

            'examples.AddOrdersToOptimization(rQueryParams, addresses, rParams)
#End Region

#Region "==== Add Orders To a Route ===="
            'Dim rQueryParams As New RouteParametersQuery
            'With rQueryParams
            '    .RouteId = "F0C842829D8799067F9BF7A495076335"
            '    .Redirect = False
            'End With

            'Dim addresses As Address() = New Address() {New Address() With { _
            '    .AddressString = "273 Canal St, New York, NY 10013, USA", _
            '    .Latitude = 40.7191558, _
            '    .Longitude = -74.0011966, _
            '    .Alias = "", _
            '    .CurbsideLatitude = 40.7191558, _
            '    .CurbsideLongitude = -74.0011966 _
            '}, New Address() With { _
            '    .AddressString = "106 Liberty St, New York, NY 10006, USA", _
            '    .Alias = "BK Restaurant #: 2446", _
            '    .Latitude = 40.709637, _
            '    .Longitude = -74.011912, _
            '    .CurbsideLatitude = 40.709637, _
            '    .CurbsideLongitude = -74.011912, _
            '    .Email = "", _
            '    .Phone = "(917) 338-1887", _
            '    .FirstName = "", _
            '    .LastName = "", _
            '    .CustomFields = New Dictionary(Of String, String) From {{"icon", Nothing}}, _
            '    .Time = 0, _
            '    .OrderId = 7205705 _
            '}, New Address() With { _
            '    .AddressString = "106 Fulton St, Farmingdale, NY 11735, USA", _
            '    .Alias = "BK Restaurant #: 17871", _
            '    .Latitude = 40.73073, _
            '    .Longitude = -73.459283, _
            '    .CurbsideLatitude = 40.73073, _
            '    .CurbsideLongitude = -73.459283, _
            '    .Email = "", _
            '    .Phone = "(212) 566-5132", _
            '    .FirstName = "", _
            '    .LastName = "", _
            '    .CustomFields = New Dictionary(Of String, String) From {{"icon", Nothing}}, _
            '    .Time = 0, _
            '    .OrderId = 7205703 _
            '}}

            'Dim rParams As New RouteParameters

            'With rParams
            '    .RouteName = "Wednesday 15th of June 2016 07:01 PM (+03:00)"
            '    .RouteDate = 1465948800
            '    .RouteTime = 14400
            '    .Optimize = "Time"
            '    .RouteType = "single"
            '    .AlgorithmType = 1
            '    .RT = False
            '    .LockLast = False
            '    .MemberId = 1
            '    .VehicleId = ""
            '    .DisableOptimization = False
            'End With

            'examples.AddOrdersToRoute(rQueryParams, addresses, rParams)
#End Region

#Region "==== Merge Routes ===="
            'Dim RouteIds As String = "56E8F6BF949670F0C0BBAC00590FD116,A6DAA07A7D4737723A9C85E7C3BA2351"
            'Dim DepotAddress As String = "11921 N Dickinson Dr, Fredericksburg, VA 22407, USA"
            'Dim RemoveOrigin As String = "0"
            'Dim Latitude As String = "38.285804"
            'Dim Longitude As String = "-77.555054"

            'Dim params As New Dictionary(Of String, String)
            'params.Add("route_ids", RouteIds)
            'params.Add("depot_address", DepotAddress)
            'params.Add("remove_origin", RemoveOrigin)
            'params.Add("depot_lat", Latitude)
            'params.Add("depot_lng", Longitude)
            'examples.MergeRoutes(params)
#End Region

#Region "==== Optimization ===="
            ' ======== Remove Existing Optimization ===========================

            'Dim optimizationProblemID As String = examples.SingleDriverRoundTripGeneric()

            'Dim OptimizationProblemIds As String() = {optimizationProblemID}

            'examples.RemoveOptimization(OptimizationProblemIds)
            '======================================================================

            ' ======== Remove Address From Optimization ===========================
            'Dim OptimizationProblemId As String = "F678E11289BBEA44D5BEC41BB22B3FB4"
            'Dim RouteDestinationId As Integer = 174785127

            'examples.RemoveAddressFromOptimization(OptimizationProblemId, RouteDestinationId)
            '======================================================================
#End Region
            Console.WriteLine("Press any key")
            Console.ReadKey()
        End Sub
    End Module
End Namespace
