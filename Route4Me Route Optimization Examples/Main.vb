Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System
Imports System.Collections.Generic
Namespace Route4MeSDKTest
    Public Module Main
        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()

            ' ======== Add Rectangular Avoidance Zone  ===========================
            examples.AddRectAvoidanceZone()
            '======================================================================

            ' ======== Add Polygon Avoidance Zone  ===========================
            'examples.AddPolygonAvoidanceZone()
            '======================================================================

            ' ======== Remove Territory  ===========================
            'examples.UpdateTerritory()
            '======================================================================

            ' ======== Remove Territory  ===========================
            'examples.RemoveTerritory()
            '======================================================================

            ' ======== Create Rectangular Territory  ===========================
            'examples.CreateRectTerritory()
            '======================================================================

            ' ======== Create Polygon Territory  ===========================
            'examples.CreatePolygonTerritory()
            '======================================================================

            ' ======== Get a Territories  ===========================
            'examples.GetTerritories()
            '======================================================================

            ' ======== Get a Territory  ===========================
            'examples.GetTerritory()
            '======================================================================

            ' ======== Create a Territory  ===========================
            'examples.CreateTerritory()
            '======================================================================

            ' ======== Search Locations By IDs  ===========================
            'examples.SearchLocationsByIDs()
            '======================================================================

            ' ======== Search Routed Locations  ===========================
            'examples.SearchRoutedLocations()
            '======================================================================

            ' ======== Get Addressbook Specified Fields Filtered by Text in Any Field  ===========================
            'examples.GetSpecifiedFieldsSearchText()
            '======================================================================

            ' ======== Get Addressbook Locations By Text In Any Field ===========================
            'examples.GetAddressbookLocation()
            '======================================================================

            ' ======== Get Vehicles ===========================
            'examples.GetVehicles()
            '======================================================================

            ' ======== Search Route Owner Changed ===========================
            'examples.SearchRouteOwnerChanged()
            '======================================================================

            ' ======== Search Note Inserted ===========================
            'examples.SearchNoteInserted()
            '======================================================================

            ' ======== Search Destination Marked As Departed ===========================
            'examples.SearchDestinationMarkedAsDeparted()
            '======================================================================

            ' ======== Search Area Destination Inserted ===========================
            'examples.SearchDestinationInserted()
            '======================================================================

            ' ======== Search Area Destination Deleted ===========================
            'examples.SearchDestinationDeleted()
            '======================================================================

            ' ======== Search Area Added Activiities ===========================
            'examples.SearchAreaAdded()
            '======================================================================

            ' ======== Log Specific Message ===========================
            'examples.LogSpecificMessage()
            '======================================================================

            ' ======== User Registratin ===========================
            'examples.UserRegistration()
            '======================================================================

            ' ======== Validate Session ===========================
            'examples.ValidateSession()
            '======================================================================

            ' ======== User Authentication ===========================
            'examples.UserAuthentication()
            '======================================================================

            ' ======== Find Asset (Asset Tracking) ===========================
            'examples.FindAsset()
            '======================================================================

            ' ======== Rapid Stret Service Limited ===========================
            'examples.RapidStreetServiceLimited()
            '======================================================================

            ' ======== Rapid Stret Service All ===========================
            'examples.RapidStreetServiceAll()
            '======================================================================

            ' ======== Rapid Stret Zipcode Limited ===========================
            'examples.RapidStreetZipcodeLimited()
            '======================================================================

            ' ======== Rapid Stret Zipcode All ===========================
            'examples.RapidStreetZipcodeAll()
            '======================================================================

            ' ======== Rapid Stret Data Limited ===========================
            'examples.RapidStreetDataLimited()
            '======================================================================

            ' ======== Rapid Stret Data All ===========================
            'examples.RapidStreetDataAll()
            '======================================================================

            ' ======== Rapid Stret Data Single ===========================
            'examples.RapidStreetDataSingle()
            '======================================================================

            ' ======== Reverse Geocoding ===========================
            'examples.ReverseGeocoding()
            '======================================================================

            ' ======== Forward Geocoding ===========================
            'Dim geoParams As New GeocodingParameters With { _
            '     .Addresses = "Los20%Angeles20%International20%Airport,20%CA", _
            '    .Format = "xml" _
            '}
            'examples.GeocodingForward(geoParams)
            '======================================================================

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
            '    .RouteId = "241466F15515D67D3F951E2DA38DE76D", _
            '    .AddressId = 167899269, _
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

            ' ======== Add Orders To an Optimization ===========================
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
            '======================================================================

            ' ======== Add Orders To a Route ===========================
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
            '======================================================================

            ' ======== Merge Routes ===========================
            'Dim RouteIds As String = "0E0F64689F772586042D0F3F4BFBEFA2,9E8D60B196743D6872E9D899E1BDE753"
            'Dim DepotAddress As String = "455 S 4th St, Louisville, KY 40202"
            'Dim RemoveOrigin As String = "0"
            'Dim Latitude As String = "38.251698"
            'Dim Longitude As String = "85.757308'"

            'Dim params As New Dictionary(Of String, String)
            'params.Add("route_ids", RouteIds)
            'params.Add("depot_address", DepotAddress)
            'params.Add("remove_origin", RemoveOrigin)
            'params.Add("depot_lat", Latitude)
            'params.Add("depot_lng", Longitude)
            'examples.MergeRoutes(params)
            '======================================================================

            ' ======== Add Custom Data to a Route ===========================
            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'Dim RouteDestinationId = 174405640

            'Dim CustomData As New Dictionary(Of String, String)
            'CustomData.Add("animal", "tiger")
            'CustomData.Add("bird", "canary")
            'examples.UpdateRouteCustomData(RouteId, RouteDestinationId, CustomData)
            '======================================================================

            ' ======== Route Sharing ===========================
            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'Dim email As String = "oleg.guchi@gmail.com"
            'examples.RouteSharing(RouteId, email)
            '======================================================================

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
            'Dim RouteId As String = "6D1622D3F794F3A804381C9451E5A239"

            'examples.GetRouteDirections(RouteId)
            '======================================================================

            ' ======== Get Route's Path Points ===========================
            'Dim RouteId As String = "6D1622D3F794F3A804381C9451E5A239"

            'examples.GetRoutePathPoints(RouteId)
            '======================================================================

            ' ======== Insert Address Into Route's Optimal Position ===========================
            'Dim RouteId As String = "6D1622D3F794F3A804381C9451E5A239"

            'examples.InsertAddressIntoRouteOptimzalPostion(RouteId)
            '======================================================================

            ' ======== Remove Existing Optimization ===========================
            'Dim OptimizationProblemIds As String() = {"D2EAF1916DCC33A903BBCBF23364980D"}

            'examples.RemoveOptimization(OptimizationProblemIds)
            '======================================================================

            ' ======== Remove Address From Optimization ===========================
            'Dim OptimizationProblemId As String = "F678E11289BBEA44D5BEC41BB22B3FB4"
            'Dim RouteDestinationId As Integer = 174785127

            'examples.RemoveAddressFromOptimization(OptimizationProblemId, RouteDestinationId)
            '======================================================================
            Console.WriteLine("Press any key")
            Console.ReadKey()
        End Sub
    End Module
End Namespace
