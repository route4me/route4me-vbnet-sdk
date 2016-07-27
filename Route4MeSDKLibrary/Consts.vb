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
        Public Const MergeRoutes As String = MainHost + "/actions/merge_routes.phpp"

#End Region
    End Class
End Namespace
