﻿Imports System.Runtime.Serialization
Imports System.Collections.Generic
Imports System.Reflection

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class DataObjectRoute
        Inherits DataObjectBase

        <DataMember(Name:="route_id", EmitDefaultValue:=False)>
        Public Property RouteID As String

        <DataMember(Name:="user_route_rating", EmitDefaultValue:=False)>
        Public Property UserRouteRating As Integer?

        <DataMember(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        <DataMember(Name:="member_email", EmitDefaultValue:=False)>
        Public Property MemberEmail As String

        <DataMember(Name:="member_first_name", EmitDefaultValue:=False)>
        Public Property MemberFirstName As String

        <DataMember(Name:="member_last_name", EmitDefaultValue:=False)>
        Public Property MemberLastName As String

        <DataMember(Name:="channel_name", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ChannelName As String

        <DataMember(Name:="member_picture", EmitDefaultValue:=False)>
        Public Property MemberPicture As String

        <DataMember(Name:="member_tracking_subheadline", EmitDefaultValue:=False)>
        Public Property MemberTrackingSubheadline As String

        <DataMember(Name:="approved_for_execution")>
        Public Property ApprovedForExecution As Boolean

        <DataMember(Name:="approved_revisions_counter", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ApprovedRevisionsCounter As Integer?

        <DataMember(Name:="vehicle_alias", EmitDefaultValue:=False)>
        Public Property VehicleAlias As String

        <DataMember(Name:="driver_alias", EmitDefaultValue:=False)>
        Public Property DriverAlias As String

        <DataMember(Name:="route_cost", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property RouteCost As Double?

        <DataMember(Name:="route_revenue", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property RouteRevenue As Double?

        <DataMember(Name:="net_revenue_per_distance_unit", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property NetRevenuePerDistanceUnit As Double?

        <DataMember(Name:="mpg", EmitDefaultValue:=False)>
        Public Property mpg As String

        <DataMember(Name:="trip_distance", EmitDefaultValue:=False)>
        Public Property TripDistance As Double?

        <DataMember(Name:="udu_distance_unit", EmitDefaultValue:=False)>
        Public Property UduDistanceUnit As String

        <DataMember(Name:="udu_trip_distance", EmitDefaultValue:=False)>
        Public Property UduTripDistance As Double?

        <DataMember(Name:="is_unrouted", EmitDefaultValue:=False)>
        Public Property IsUnrouted As Boolean?

        <DataMember(Name:="gas_price", EmitDefaultValue:=False)>
        Public Property GasPrice As Double?

        <DataMember(Name:="route_duration_sec", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property RouteDurationSec As Long?

        <DataMember(Name:="planned_total_route_duration", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property PlannedTotalRouteDuration As Long?

        <DataMember(Name:="total_wait_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TotalWaitTime As Long?

        <DataMember(Name:="udu_actual_travel_distance", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property UduActualTravelDistance As Double?

        <DataMember(Name:="actual_travel_distance", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ActualTravelDistance As Double?

        <DataMember(Name:="actual_travel_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ActualTravelTime As Long?

        <DataMember(Name:="actual_footsteps", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property ActualFootsteps As Integer?

        <DataMember(Name:="working_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property WorkingTime As Long?

        <DataMember(Name:="driving_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property DrivingTime As Long?

        <DataMember(Name:="idling_time", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property IdlingTime As Long?

        <DataMember(Name:="paying_miles", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property PayingMiles As Double?

        <DataMember(Name:="geofence_polygon_type", EmitDefaultValue:=False)>
        Public Property GeofencePolygonType As String

        <DataMember(Name:="geofence_polygon_size", EmitDefaultValue:=False)>
        Public Property GeofencePolygonSize As Integer?

        <DataMember(Name:="destination_count", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property DestinationCount As Integer?

        <DataMember(Name:="notes_count", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property NotesCount As Integer?

        <DataMember(Name:="notes", EmitDefaultValue:=False)>
        Public Property Notes As AddressNote()

        <DataMember(Name:="vehicle", EmitDefaultValue:=False)>
        Public Property Vehilce As VehicleV4Response

        <DataMember(Name:="member_config_storage", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property MemberConfigStorage As Dictionary(Of String, String)

        <DataMember(Name:="original_route", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property OriginalRoute As DataObjectRoute

        ''' <summary>
        ''' Edge by edge turn-by-turn directions. See <see cref="Direction"/>
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="directions", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property Directions As Direction()

        ''' <summary>
        ''' Edge-wise path to be drawn on the map See <see cref="GeoPoint"/>
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="path", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property Path As GeoPoint()

        ''' <summary>
        ''' A collection of device tracking data with coordinates, speed, and timestamps.
        ''' </summary>
        ''' <returns></returns>
        <DataMember(Name:="tracking_history", EmitDefaultValue:=False)>
        <[ReadOnly](True)>
        Public Property TrackingHistory As TrackingHistory()
    End Class
End Namespace
