Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes


    <DataContract> _
    Public NotInheritable Class DataObjectRoute
        Inherits DataObject
        <DataMember(Name:="route_id")> _
        Public Property RouteID() As String
            Get
                Return m_RouteID
            End Get
            Set(value As String)
                m_RouteID = Value
            End Set
        End Property
        Private m_RouteID As String

        <DataMember(Name:="member_id")> _
        Public Property MemberId() As String
            Get
                Return m_MemberId
            End Get
            Set(value As String)
                m_MemberId = Value
            End Set
        End Property
        Private m_MemberId As String

        <DataMember(Name:="member_email")> _
        Public Property MemberEmail() As String
            Get
                Return m_MemberEmail
            End Get
            Set(value As String)
                m_MemberEmail = Value
            End Set
        End Property
        Private m_MemberEmail As String

        <DataMember(Name:="vehicle_alias")> _
        Public Property VehicleAlias() As String
            Get
                Return m_VehicleAlias
            End Get
            Set(value As String)
                m_VehicleAlias = Value
            End Set
        End Property
        Private m_VehicleAlias As String

        <DataMember(Name:="driver_alias")> _
        Public Property DriverAlias() As String
            Get
                Return m_DriverAlias
            End Get
            Set(value As String)
                m_DriverAlias = Value
            End Set
        End Property
        Private m_DriverAlias As String

        <DataMember(Name:="route_cost")> _
        Public Property RouteCost() As System.Nullable(Of Double)
            Get
                Return m_RouteCost
            End Get
            Set(value As System.Nullable(Of Double))
                m_RouteCost = Value
            End Set
        End Property
        Private m_RouteCost As System.Nullable(Of Double)

        <DataMember(Name:="route_revenue")> _
        Public Property RouteRevenue() As System.Nullable(Of Double)
            Get
                Return m_RouteRevenue
            End Get
            Set(value As System.Nullable(Of Double))
                m_RouteRevenue = Value
            End Set
        End Property
        Private m_RouteRevenue As System.Nullable(Of Double)

        <DataMember(Name:="net_revenue_per_distance_unit")> _
        Public Property NetRevenuePerDistanceUnit() As System.Nullable(Of Double)
            Get
                Return m_NetRevenuePerDistanceUnit
            End Get
            Set(value As System.Nullable(Of Double))
                m_NetRevenuePerDistanceUnit = Value
            End Set
        End Property
        Private m_NetRevenuePerDistanceUnit As System.Nullable(Of Double)

        <DataMember(Name:="created_timestamp")> _
        Public Property CreatedTimestamp() As System.Nullable(Of Integer)
            Get
                Return m_CreatedTimestamp
            End Get
            Set(value As System.Nullable(Of Integer))
                m_CreatedTimestamp = Value
            End Set
        End Property
        Private m_CreatedTimestamp As System.Nullable(Of Integer)

        <DataMember(Name:="mpg")> _
        Public Property mpg() As String
            Get
                Return m_mpg
            End Get
            Set(value As String)
                m_mpg = Value
            End Set
        End Property
        Private m_mpg As String

        <DataMember(Name:="trip_distance")> _
        Public Property TripDistance() As System.Nullable(Of Double)
            Get
                Return m_TripDistance
            End Get
            Set(value As System.Nullable(Of Double))
                m_TripDistance = Value
            End Set
        End Property
        Private m_TripDistance As System.Nullable(Of Double)

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
                m_RouteDurationSec = Value
            End Set
        End Property
        Private m_RouteDurationSec As System.Nullable(Of Integer)

    End Class
End Namespace
