Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class RouteResponse
        <DataMember(Name:="route_id")> _
        Public Property RouteID() As String
            Get
                Return m_RouteID
            End Get
            Set(value As String)
                m_RouteID = value
            End Set
        End Property
        Private m_RouteID As String

        <DataMember(Name:="optimization_problem_id")> _
        Public Property OptimizationProblemId() As String
            Get
                Return m_OptimizationProblemId
            End Get
            Set(value As String)
                m_OptimizationProblemId = value
            End Set
        End Property
        Private m_OptimizationProblemId As String

        ''' <summary>
        ''' Route rating by user [0 - 5]
        ''' </summary>
        <DataMember(Name:="user_route_rating")> _
        Public Property UserRouteRating() As System.Nullable(Of Integer)
            Get
                Return m_UserRouteRating
            End Get
            Set(value As System.Nullable(Of Integer))
                m_UserRouteRating = value
            End Set
        End Property
        Private m_UserRouteRating As System.Nullable(Of Integer)

        <DataMember(Name:="member_id")> _
        Public Property MemberId() As System.Nullable(Of Integer)
            Get
                Return m_MemberId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As System.Nullable(Of Integer)

        <DataMember(Name:="member_email")> _
        Public Property MemberEmail() As String
            Get
                Return m_MemberEmail
            End Get
            Set(value As String)
                m_MemberEmail = value
            End Set
        End Property
        Private m_MemberEmail As String

        <DataMember(Name:="member_first_name")> _
        Public Property MemberFirstName() As String
            Get
                Return m_MemberFirstName
            End Get
            Set(value As String)
                m_MemberFirstName = value
            End Set
        End Property
        Private m_MemberFirstName As String

        <DataMember(Name:="member_last_name")> _
        Public Property MemberLastName() As String
            Get
                Return m_MemberLastName
            End Get
            Set(value As String)
                m_MemberLastName = value
            End Set
        End Property
        Private m_MemberLastName As String

        <DataMember(Name:="channel_name")> _
        Public Property ChannelName() As String
            Get
                Return m_ChannelName
            End Get
            Set(value As String)
                m_ChannelName = value
            End Set
        End Property
        Private m_ChannelName As String

        <DataMember(Name:="vehicle_alias")> _
        Public Property VehicleAlias() As String
            Get
                Return m_VehicleAlias
            End Get
            Set(value As String)
                m_VehicleAlias = value
            End Set
        End Property
        Private m_VehicleAlias As String

        <DataMember(Name:="driver_alias")> _
        Public Property DriverAlias() As String
            Get
                Return m_DriverAlias
            End Get
            Set(value As String)
                m_DriverAlias = value
            End Set
        End Property
        Private m_DriverAlias As String

        <DataMember(Name:="trip_distance")> _
        Public Property TripDistance() As System.Nullable(Of Double)
            Get
                Return m_TripDistance
            End Get
            Set(value As System.Nullable(Of Double))
                m_TripDistance = value
            End Set
        End Property
        Private m_TripDistance As System.Nullable(Of Double)

        <DataMember(Name:="is_unrouted")> _
        Public Property IsUnrouted() As Boolean
            Get
                Return m_IsUnrouted
            End Get
            Set(value As Boolean)
                m_IsUnrouted = value
            End Set
        End Property
        Private m_IsUnrouted As Boolean

        <DataMember(Name:="route_cost")> _
        Public Property RouteCost() As System.Nullable(Of Double)
            Get
                Return m_RouteCost
            End Get
            Set(value As System.Nullable(Of Double))
                m_RouteCost = value
            End Set
        End Property
        Private m_RouteCost As System.Nullable(Of Double)

        <DataMember(Name:="route_revenue")> _
        Public Property RouteRevenue() As System.Nullable(Of Double)
            Get
                Return m_RouteRevenue
            End Get
            Set(value As System.Nullable(Of Double))
                m_RouteRevenue = value
            End Set
        End Property
        Private m_RouteRevenue As System.Nullable(Of Double)

        <DataMember(Name:="net_revenue_per_distance_unit")> _
        Public Property NetRevenuePerDistanceUnit() As System.Nullable(Of Double)
            Get
                Return m_NetRevenuePerDistanceUnit
            End Get
            Set(value As System.Nullable(Of Double))
                m_NetRevenuePerDistanceUnit = value
            End Set
        End Property
        Private m_NetRevenuePerDistanceUnit As System.Nullable(Of Double)

        <DataMember(Name:="created_timestamp")> _
        Public Property CreatedTimestamp() As System.Nullable(Of Integer)
            Get
                Return m_CreatedTimestamp
            End Get
            Set(value As System.Nullable(Of Integer))
                m_CreatedTimestamp = value
            End Set
        End Property
        Private m_CreatedTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="mpg")> _
        Public Property mpg() As System.Nullable(Of Double)
            Get
                Return m_mpg
            End Get
            Set(value As System.Nullable(Of Double))
                m_mpg = value
            End Set
        End Property
        Private m_mpg As System.Nullable(Of Double)

        <DataMember(Name:="gas_price")> _
        Public Property GasPrice() As System.Nullable(Of Double)
            Get
                Return m_GasPrice
            End Get
            Set(value As System.Nullable(Of Double))
                m_GasPrice = value
            End Set
        End Property
        Private m_GasPrice As System.Nullable(Of Double)

        <DataMember(Name:="route_duration_sec")> _
        Public Property RouteDurationSec() As System.Nullable(Of Integer)
            Get
                Return m_RouteDurationSec
            End Get
            Set(value As System.Nullable(Of Integer))
                m_RouteDurationSec = value
            End Set
        End Property
        Private m_RouteDurationSec As System.Nullable(Of Integer)

        <DataMember(Name:="planned_total_route_duration")> _
        Public Property PlannedTotalRouteDuration() As System.Nullable(Of Integer)
            Get
                Return m_PlannedTotalRouteDuration
            End Get
            Set(value As System.Nullable(Of Integer))
                m_PlannedTotalRouteDuration = value
            End Set
        End Property
        Private m_PlannedTotalRouteDuration As System.Nullable(Of Integer)

        <DataMember(Name:="actual_travel_distance")> _
        Public Property ActualTravelDistance() As System.Nullable(Of Double)
            Get
                Return m_ActualTravelDistance
            End Get
            Set(value As System.Nullable(Of Double))
                m_ActualTravelDistance = value
            End Set
        End Property
        Private m_ActualTravelDistance As System.Nullable(Of Double)

        <DataMember(Name:="actual_travel_time")> _
        Public Property ActualTravelTime() As System.Nullable(Of Integer)
            Get
                Return m_ActualTravelTime
            End Get
            Set(value As System.Nullable(Of Integer))
                m_ActualTravelTime = value
            End Set
        End Property
        Private m_ActualTravelTime As System.Nullable(Of Integer)

        <DataMember(Name:="actual_footsteps")> _
        Public Property ActualFootSteps() As System.Nullable(Of Integer)
            Get
                Return m_ActualFootSteps
            End Get
            Set(value As System.Nullable(Of Integer))
                m_ActualFootSteps = value
            End Set
        End Property
        Private m_ActualFootSteps As System.Nullable(Of Integer)

        <DataMember(Name:="working_time")> _
        Public Property WorkingTime() As System.Nullable(Of Integer)
            Get
                Return m_WorkingTime
            End Get
            Set(value As System.Nullable(Of Integer))
                m_WorkingTime = value
            End Set
        End Property
        Private m_WorkingTime As System.Nullable(Of Integer)

        <DataMember(Name:="driving_time")> _
        Public Property DrivingTime() As System.Nullable(Of Integer)
            Get
                Return m_DrivingTime
            End Get
            Set(value As System.Nullable(Of Integer))
                m_DrivingTime = value
            End Set
        End Property
        Private m_DrivingTime As System.Nullable(Of Integer)

        <DataMember(Name:="idling_time")> _
        Public Property IdlingTime() As System.Nullable(Of Integer)
            Get
                Return m_IdlingTime
            End Get
            Set(value As System.Nullable(Of Integer))
                m_IdlingTime = value
            End Set
        End Property
        Private m_IdlingTime As System.Nullable(Of Integer)

        <DataMember(Name:="paying_miles")> _
        Public Property PayingMiles() As System.Nullable(Of Double)
            Get
                Return m_PayingMiles
            End Get
            Set(value As System.Nullable(Of Double))
                m_PayingMiles = value
            End Set
        End Property
        Private m_PayingMiles As System.Nullable(Of Double)

        <DataMember(Name:="geofence_polygon_type")> _
        Public Property GeofencePolygonType() As String
            Get
                Return m_GeofencePolygonType
            End Get
            Set(value As String)
                m_GeofencePolygonType = value
            End Set
        End Property
        Private m_GeofencePolygonType As String

        <DataMember(Name:="geofence_polygon_size")> _
        Public Property GeofencePolygonSize() As System.Nullable(Of Integer)
            Get
                Return m_GeofencePolygonSize
            End Get
            Set(value As System.Nullable(Of Integer))
                m_GeofencePolygonSize = value
            End Set
        End Property
        Private m_GeofencePolygonSize As System.Nullable(Of Integer)

        <DataMember(Name:="parameters")> _
        Public Property Parameters() As RouteParameters
            Get
                Return m_Parameters
            End Get
            Set(value As RouteParameters)
                m_Parameters = value
            End Set
        End Property
        Private m_Parameters As RouteParameters

        <DataMember(Name:="addresses")> _
        Public Property Addresses() As Address()
            Get
                Return m_Addresses
            End Get
            Set(value As Address())
                m_Addresses = value
            End Set
        End Property
        Private m_Addresses As Address()

        <DataMember(Name:="links")> _
        Public Property Links() As Links
            Get
                Return m_Links
            End Get
            Set(value As Links)
                m_Links = value
            End Set
        End Property
        Private m_Links As Links

        <DataMember(Name:="notes")> _
        Public Property Notes() As AddressNote()
            Get
                Return m_Notes
            End Get
            Set(value As AddressNote())
                m_Notes = value
            End Set
        End Property
        Private m_Notes As AddressNote()

        <DataMember(Name:="path")> _
        Public Property Path() As GeoPoint()
            Get
                Return m_Path
            End Get
            Set(value As GeoPoint())
                m_Path = value
            End Set
        End Property
        Private m_Path As GeoPoint()

        <DataMember(Name:="directions")> _
        Public Property Directions() As Direction()
            Get
                Return m_Directions
            End Get
            Set(value As Direction())
                m_Directions = value
            End Set
        End Property
        Private m_Directions As Direction()


    End Class
End Namespace