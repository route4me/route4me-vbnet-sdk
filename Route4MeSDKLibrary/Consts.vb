Namespace Route4MeSDK
    ''' <summary>
    ''' Route4Me infrastructure settings
    ''' Api version 4 hosts constants
    ''' </summary>
    Public NotInheritable Class R4MEInfrastructureSettings
        Private Sub New()
        End Sub
#Region "Api V4"

        Public Const ApiVersion As String = "4"

        Public Const MainHost As String = "https://www.route4me.com"
        Public Const ApiHost As String = MainHost + "/api.v4/optimization_problem.php"
        Public Const ShowRouteHost As String = MainHost + "/route4me.php"
        Public Const RouteHost As String = MainHost + "/api.v4/route.php"
        Public Const SetGpsHost As String = MainHost + "/track/set.php"

        Public Const GetUsersHost As String = MainHost + "/api/member/view_users.php"
        Public Const UserAuthentication As String = MainHost + "/actions/authenticate.php"
        Public Const ValidateSession As String = MainHost + "/datafeed/session/validate_session.php"
        Public Const UserRegistration As String = MainHost + "/actions/register_action.php"

        Public Const AddRouteNotesHost As String = MainHost + "/actions/addRouteNotes.php"
        Public Const GetActivitiesHost As String = MainHost + "/api/get_activities.php"
        Public Const GetAddress As String = MainHost + "/api.v4/address.php"
        Public Const DuplicateRoute As String = MainHost + "/actions/duplicate_route.php"
        Public Const MoveRouteDestination As String = MainHost + "/actions/route/move_route_destination.php"
        Public Const AddressBook As String = MainHost + "/api.v4/address_book.php"
        Public Const Avoidance As String = MainHost + "/api.v4/avoidance.php"
        Public Const Order As String = MainHost + "/api.v4/order.php"
        Public Const RouteReoptimize As String = MainHost + "/api.v3/route/reoptimize_2.php"
        Public Const RouteSharing As String = MainHost + "/actions/route/share_route.php"
        Public Const MergeRoutes As String = MainHost + "/actions/merge_routes.php"
        Public Const MarkAddressDeparted As String = MainHost + "/api/route/mark_address_departed.php"
        Public Const MarkAddressVisited As String = MainHost + "/actions/address/update_address_visited.php"
        Public Const Geocoder As String = MainHost + "/api/geocoder.php"
        Public Const RapidStreetData As String = "https://rapid.route4me.com/street_data"
        Public Const RapidStreetZipcode As String = "https://rapid.route4me.com/street_data/zipcode"
        Public Const RapidStreetService As String = "https://rapid.route4me.com/street_data/service"
        Public Const AssetTracking As String = MainHost + "/api/asset/find_route.php"


#End Region
    End Class
End Namespace
