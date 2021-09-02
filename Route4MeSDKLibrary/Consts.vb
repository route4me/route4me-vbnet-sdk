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

        Public Const MainHost As String = "https://api.route4me.com"
        Public Const ApiHost As String = MainHost + "/api.v4/optimization_problem.php"
        Public Const ShowRouteHost As String = MainHost + "/route4me.php"
        Public Const RouteHost As String = MainHost + "/api.v4/route.php"
        Public Const SetGpsHost As String = MainHost + "/track/set.php"

        'Public Const GetUsersHost As String = MainHost + "/api/member/view_users.php" 'old endpoint version
        Public Const GetUsersHost As String = MainHost + "/api.v4/user.php"
        Public Const UserAuthentication As String = MainHost + "/actions/authenticate.php"
        Public Const ValidateSession As String = MainHost + "/datafeed/session/validate_session.php"
        Public Const UserRegistration As String = MainHost + "/actions/register_action.php"
        Public Const UserConfiguration As String = MainHost + "/api.v4/configuration-settings.php"

        Public Const AddRouteNotesHost As String = MainHost + "/actions/addRouteNotes.php"

        Public Const GetActivitiesHost As String = MainHost + "/api/get_activities.php"
        Public Const ActivityFeed As String = MainHost + "/api.v4/activity_feed.php"
        Public Const UserLocation As String = MainHost + "/api/track/view_user_locations.php"

        Public Const GetAddress As String = MainHost + "/api.v4/address.php"
        Public Const DuplicateRoute As String = MainHost + "/actions/duplicate_route.php"
        Public Const MoveRouteDestination As String = MainHost + "/actions/route/move_route_destination.php"

        Public Const AddressBook As String = MainHost + "/api.v4/address_book.php"
        Public Const AddressBookGroup As String = MainHost + "/api.v4/address_book_group.php"
        Public Const AddressBookGroupSearch As String = MainHost + "/api/address_book/get_search_group_addresses.php"

        Public Const Avoidance As String = MainHost + "/api.v4/avoidance.php"
        Public Const Territory As String = MainHost + "/api.v4/territory.php"
        Public Const Order As String = MainHost + "/api.v4/order.php"
        Public Const OrderCustomField As String = MainHost + "/api.v4/order_custom_user_fields.php"
        Public Const RouteReoptimize As String = MainHost + "/api.v3/route/reoptimize_2.php"
        Public Const RouteSharing As String = MainHost + "/actions/route/share_route.php"
        Public Const MergeRoutes As String = MainHost + "/actions/merge_routes.php"
        Public Const MarkAddressDeparted As String = MainHost + "/api/route/mark_address_departed.php"
        Public Const MarkAddressVisited As String = MainHost + "/actions/address/update_address_visited.php"

        Public Const Geocoder As String = MainHost + "/api/geocoder.php"
        Public Const FastGeocoder As String = MainHost + "/actions/upload/json-geocode.php"
        Public Const SaveGeocodedAddresses As String = MainHost & "/api/address_book/save_geocoded.php"
        Public Const r4meValidator As String = "https://validator.route4me.com:443/"
        Public Const RapidStreetData As String = "https://rapid.route4me.com/street_data"
        Public Const RapidStreetZipcode As String = "https://rapid.route4me.com/street_data/zipcode"
        Public Const RapidStreetService As String = "https://rapid.route4me.com/street_data/service"

        'Public Const AssetTracking As String = MainHost + "/api/asset/find_route.php"
        Public Const AssetTracking As String = MainHost + "/api.v4/status.php"
        Public Const DeviceLocation As String = MainHost + "/api/track/get_device_location.php"

        Public Const ViewVehicles As String = MainHost + "/api/vehicles/view_vehicles.php"
        Public Const Vehicle_V4 As String = "https://wh.route4me.com/modules/api/vehicles"
        Public Const Vehicle_V4_API As String = MainHost + "/api.v4/vehicle.php"

        Public Const HybridOptimization As String = MainHost + "/api.v4/hybrid_date_optimization.php"

        Public Const CustomNoteType As String = MainHost + "/api.v4/note_custom_types.php"

        Public Const TelematicsVendorsHost As String = "https://telematics.route4me.com/api/vendors.php"
        Public Const TelematicsRegisterHost As String = MainHost & "/api.v4/telematics/register.php"
        Public Const TelematicsConnection As String = MainHost & "/api.v4/telematics/connections.php"
        Public Const TelematicsVendorsInfo As String = MainHost & "/api.v4/telematics/vendors.php"

        Public Const MemberCapabilities As String = MainHost + "/api/member/capabilities.php"

        Public Const ScheduleCalendar As String = MainHost + "/api/schedule_calendar_data.php"
#End Region
    End Class

    Module R4MEInfrastructureSettingsV5
        Public Const ApiVersion As String = "5"

        Public Const MainHost As String = "https://wh.route4me.com/modules/api/v5.0"
        Public Const MainHostWeb As String = "https://wh.route4me.com/modules/webapi/v5.0"

        Public Const Routes As String = MainHost + "/routes"
        Public Const RoutesDuplicate As String = MainHost + "/routes/duplicate"
        Public Const RoutesMerge As String = MainHost + "/routes/merge"
        Public Const RoutesPaginate As String = MainHost + "/routes/paginate"
        Public Const RoutesFallbackPaginate As String = MainHost + "/routes/fallback/paginate"
        Public Const RoutesFallbackDatatable As String = MainHost + "/routes/fallback/datatable"
        Public Const RoutesFallback As String = MainHost + "/routes/fallback"
        Public Const RoutesReindexCallback As String = MainHost + "/routes/reindex-callback"
        Public Const RoutesDatatable As String = MainHost + "/routes/datatable"
        Public Const RoutesDatatableConfig As String = MainHost + "/routes/datatable/config"
        Public Const RoutesDatatableConfigFallback As String = MainHost + "/routes/fallback/datatable/config"

        Public Const TeamUsers As String = MainHost + "/team/users"

        Public Const TeamUsersBulkCreate As String = MainHost + "/team/bulk-insert"

        Public Const DriverReview As String = MainHost + "/driver-reviews"

        Public Const AccountProfile As String = MainHost & "/profile-api"

#Region "Vehicles"

        Public Const Vehicles As String = MainHost & "/vehicles"
        Public Const VehicleTemporary As String = MainHost & "/vehicles/assign"
        Public Const VehicleExecuteOrder As String = MainHost & "/vehicles/execute"
        Public Const VehicleLocation As String = MainHost & "/vehicles/location"
        Public Const VehicleProfiles As String = MainHost & "/vehicle-profiles"
        Public Const VehicleLicense As String = MainHost & "/vehicles/license"
        Public Const VehicleSearch As String = MainHost & "/vehicles/search"

#End Region

#Region "Telematics Platform"

        Public Const StagingHost As String = "/api"  ' Temporary unavailable
        Public Const TelematicsConnection As String = StagingHost & "/connections"
        Public Const TelematicsConnectionVehicles As String = StagingHost & "/connections/{connection_token}/vehicles"
        Public Const TelematicsAccessToken As String = StagingHost & "/access-tokens"
        Public Const TelematicsAccessTokenSchedules As String = StagingHost & "/access-token-schedules"
        Public Const TelematicsAccessTokenScheduleItems As String = StagingHost & "/access-token-schedules/{schedule_id}/items"
        Public Const TelematicsVehicleGroups As String = StagingHost & "/vehicle-groups"
        Public Const TelematicsVehicleGroupsRelation As String = StagingHost & "/vehicle-groups/{vehicle_group_id}/{relation}"
        Public Const TelematicsVehiclesRelation As String = StagingHost & "/vehicles/{vehicle_id}/{relation}"
        Public Const TelematicsInfoMembers As String = StagingHost & "/info/members"
        Public Const TelematicsInfoVehicles As String = StagingHost & "/info/vehicles"
        Public Const TelematicsInfoVehicle As String = StagingHost & "/info/vehicle/{vehicle_id}/track"
        Public Const TelematicsInfoModules As String = StagingHost & "/info/members"
        Public Const TelematicsAddresses As String = StagingHost & "/addresses"
        Public Const TelematicsErrors As String = StagingHost & "/errors"
        Public Const TelematicsCustomerNotifications As String = StagingHost & "​/customers​/{customer_id}​/notifications​"
        Public Const TelematicsCustomers As String = StagingHost & "/customers"
        Public Const TelematicsCustomerId As String = StagingHost & "/customers/{customer_id}"
        Public Const TelematicsNotificationScheduleItems As String = StagingHost & "/notification-schedules/{notification_schedule_id}/items"
        Public Const TelematicsNotificationSchedules As String = StagingHost & "/notification-schedules"
        Public Const TelematicsNotificationScheduleId As String = StagingHost & "/notification-schedules/{schedule_id}"
        Public Const TelematicsOneTimeNotifications As String = StagingHost & "​/one-time-notifications"
        Public Const TelematicsMember As String = StagingHost
        Public Const TelematicsMemberModules As String = StagingHost & "​/user-activated-modules"
        Public Const TelematicsMemberModuleId As String = StagingHost & "​/user-activated-modules/{module_id}"
        Public Const TelematicsMemberModuleVehicles As String = StagingHost & "​​/user-activated-modules​/{module_id}​/vehicles"
        Public Const TelematicsMemberModuleVehicleId As String = StagingHost & "​​​/user-activated-modules​/{module_id}​/vehicles​/{vehicle_id}"
        Public Const TelematicsVendors As String = StagingHost & "​/vendors"
        Public Const TelematicsVendorId As String = StagingHost & "​​/vendors​/{vendor_id}"
#End Region

#Region "Address Book Contacts"
        Public Const ContactHost As String = MainHost & "/address-book"
        Public Const ContactsGetAll As String = ContactHost & "/addresses/index/all"
        Public Const ContactsGetAllPaginated As String = ContactHost & "/addresses/index/pagination"
        Public Const ContactsGetClusters As String = ContactHost & "/addresses/index/clustering"
        Public Const ContactsFind As String = ContactHost & "/addresses/show"
        Public Const ContactsAddNew As String = ContactHost & "/addresses"
        Public Const ContactsAddMultiple As String = ContactHost & "/addresses/batch-create"
        Public Const ContactsUpdateById As String = ContactHost & "/addresses/{address_id}"
        Public Const ContactsUpdateMultiple As String = ContactHost & "/addresses/batch-update"
        Public Const ContactsUpdateByAreas As String = ContactHost & "/addresses/update-by-areas"
        Public Const ContactsDeleteMultiple As String = ContactHost & "/addresses/delete"
        Public Const ContactsDeleteByAreas As String = ContactHost & "/addresses/delete-by-areas"
        Public Const ContactsGetCustomFields As String = ContactHost & "/addresses/custom-fields"
        Public Const ContactsGetDepots As String = ContactHost & "/addresses/depots"
        Public Const ContactsReindexCallback As String = ContactHost & "/reindex-callback"
        Public Const ContactsExport As String = ContactHost & "/addresses/export"
        Public Const ContactsExportByAreas As String = ContactHost & "/addresses/export-by-areas"
        Public Const ContactsExportByAreaIds As String = ContactHost & "/addresses/export-by-area-ids"
        Public Const ContactsGetAsyncJobStatus As String = ContactHost & "/addresses/job-tracker/status/{job_id}"
        Public Const ContactsGetAsyncJobResult As String = ContactHost & "/addresses/job-tracker/result/{job_id}"
#End Region

    End Module

End Namespace
