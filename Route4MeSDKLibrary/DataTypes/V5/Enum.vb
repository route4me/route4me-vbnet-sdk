Imports System.ComponentModel

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Enumeration of the algorithm types:
    ''' <para>TSP = 1, single depot, single driver route</para>
    ''' <para>VRP = 2, single depot, multiple driver, no constraints, no time windows, no capacities</para>
    ''' <para>CVRP_TW_SD = 3, single depot, multiple driver, capacitated, time windows</para>
    ''' <para>CVRP_TW_MD = 4, multiple depot, multiple driver, capacitated, time windows</para>
    ''' <para>TSP_TW = 5, single depot, single driver, time windows</para>
    ''' <para>TSP_TW_CR = 6, single depot, single driver, time windows, continuous optimization (minimal location shifting)</para>
    ''' <para>BBCVRP = 7, shifts addresses from one route to another over time on a recurring schedule</para>
    ''' <para>ALG_NONE = 100</para>
    ''' <para>ALG_LEGACY_DISTRIBUTED = 101</para>
    ''' </summary>
    Public Enum AlgorithmType
        TSP = 1
        'single depot, single driver route
        VRP = 2
        'single depot, multiple driver, no constraints, no time windows, no capacities
        CVRP_TW_SD = 3
        'single depot, multiple driver, capacitated, time windows
        CVRP_TW_MD = 4
        'multiple depot, multiple driver, capacitated, time windows
        TSP_TW = 5
        'single depot, single driver, time windows
        TSP_TW_CR = 6
        'single depot, single driver, time windows, continuous optimization (minimal location shifting)
        BBCVRP = 7
        'shifts addresses from one route to another over time on a recurring schedule
        ADVANCED_CVRP_TW = 9
        'optimization with advanced constraints
        ALG_NONE = 100
        ALG_LEGACY_DISTRIBUTED = 101
    End Enum

    ''' <summary>
    ''' Enumeration of the travel modes
    ''' </summary>
    Public Enum TravelMode As UInteger
        <Description("Driving")>
        Driving

        <Description("Walking")>
        Walking
    End Enum

    ''' <summary>
    ''' Enumeration of the distance units
    ''' </summary>
    Public Enum DistanceUnit As UInteger
        <Description("mi")>
        MI

        <Description("km")>
        KM
    End Enum

    ''' <summary>
    ''' Enumeration of the avoidance conditions
    ''' </summary>
    Public Enum Avoid
        <Description("")>
        None

        <Description("minimizeHighways")>
        MinimizeHighways

        <Description("minimizeTolls")>
        MinimizeTolls

        <Description("Highways")>
        Highways

        <Description("Tolls")>
        Tolls

        <Description("highways,tolls")>
        HighwaysTolls
    End Enum

    ''' <summary>
    ''' Enumeration of the optimization options
    ''' </summary>
    Public Enum Optimize As UInteger
        <Description("Distance")>
        Distance

        <Description("Time")>
        Time

        <Description("timeWithTraffic")>
        TimeWithTraffic
    End Enum

    ''' <summary>
    ''' Enumeration of the metric systems:
    ''' <para>Euclidean = 1, measures point to point distance as a straight line</para>
    ''' <para>Manhattan = 2, measures point to point distance as taxicab geometry line</para>
    ''' <para>Geodesic = 3, measures point to point distance approximating curvature of the earth</para>
    ''' <para>Matrix = 4, measures point to point distance by traversing the actual road network</para>
    ''' <para>Exact_2D = 5, measures point to point distance using 2d rectilinear distance</para>
    ''' </summary>
    Public Enum Metric As UInteger
        Euclidean = 1
        Manhattan = 2
        Geodesic = 3
        Matrix = 4
        Exact_2D = 5
    End Enum

    ''' <summary>
    ''' Enumeration of the input device types
    ''' </summary>
    Public Enum DeviceType
        <Description("web")>
        Web

        <Description("iphone")>
        IPhone

        <Description("ipad")>
        IPad

        <Description("android_phone")>
        AndroidPhone

        <Description("android_tablet")>
        AndroidTablet
    End Enum

    ''' <summary>
    ''' Enumeration of the response formats
    ''' </summary>
    Public Enum Format
        <Description("csv")>
        Csv

        <Description("serialized")>
        Serialized

        <Description("xml")>
        Xml

        <Description("json")>
        Json
    End Enum

    ''' <summary>
    ''' Enumeration of the optimization states.
    ''' <remark>
    ''' <para>An optimization problem can be at one state at any given time.</para>
    ''' <para>Every state change invokes a socket notification associated member ID.</para>
    ''' <para>Every state change invokes a callback webhook event invocation if it was provided during the initial optimization.</para>
    ''' </remark>
    ''' </summary>
    Public Enum OptimizationState As UInteger
        StateNew = 0
        Initial = 1
        MatrixProcessing = 2
        Optimizing = 3
        Optimized = 4
        [Error] = 5
        ComputingDirections = 6
        InQueue = 7
    End Enum

    ''' <summary>
    ''' Route path output.
    ''' <para>If the actual polylines of the driving path between all the stops on the route should be returned</para>
    ''' </summary>
    Public Enum RoutePathOutput As UInteger
        <Description("None")>
        None

        <Description("Points")>
        Points
    End Enum

    ''' <summary>
    ''' Enumeration of the route destination update types
    ''' </summary>
    Public Enum StatusUpdateType
        <Description("pickup")>
        Pickup

        <Description("dropoff")>
        DropOff

        <Description("noanswer")>
        NoAnswer

        <Description("notfound")>
        NotFound

        <Description("notpaid")>
        NotPaid

        <Description("paid")>
        Paid

        <Description("wrongdelivery")>
        WrongDelivery

        <Description("wrongaddressrecipient")>
        WrongAddressRecipient

        <Description("notpresent")>
        NotPresent

        <Description("parts_missing")>
        PartsMissing

        <Description("service_rendered")>
        ServiceRendered

        <Description("follow_up")>
        FollowUp

        <Description("left_information")>
        LeftInformation

        <Description("spoke_with_decision_maker")>
        SpokeWithDecisionMaker

        <Description("spoke_with_decision_influencer")>
        SpokeWithDecisionInfluencer
        <Description("competitive_account")>
        CompetitiveAccount

        <Description("scheduled_follow_up_meeting")>
        ScheduledFollowUpMeeting

        <Description("scheduled_lunch")>
        ScheduledLunch

        <Description("scheduled_product_demo")>
        ScheduledProductDemo

        <Description("scheduled_clinical_demo")>
        ScheduledClinicalDemo

        <Description("no_opportunity")>
        NoOpportunity
    End Enum

    ''' <summary>
    ''' Address stop type.
    ''' </summary>
    Public Enum AddressStopType
        <Description("DELIVERY")>
        Delivery

        <Description("PICKUP")>
        PickUp

        <Description("BREAK")>
        Break

        <Description("MEETUP")>
        MeetUp

        <Description("SERVICE")>
        Service

        <Description("VISIT")>
        Visit

        <Description("DRIVEBY")>
        DriverBy
    End Enum

    ''' <summary>
    ''' Enumeration of the territory types
    ''' </summary>
    Public Enum TerritoryType As UInteger
        <Description("circle")>
        Circle

        <Description("poly")>
        Poly

        <Description("rect")>
        Rect
    End Enum

    ''' <summary>
    ''' Enumeration of the address bundling mode:
    ''' <para>Address = 1, group locations by address</para>
    ''' <para>Coordinates = 2, group locations by coordinates</para>
    ''' <para>AddressId = 3, group locations by list of the address IDs</para>
    ''' <para>Address = 4, group locations by address custom fields</para>
    ''' </summary>
    Public Enum AddressBundlingMode As UInteger
        Address = 1
        Coordinates = 2
        AddressId = 3
        AddressCustomField = 4
    End Enum

    ''' <summary>
    ''' Enumeration of the destinations merge mode:
    ''' <para>KeepAsSeparateDestinations = 1, keep separate destinations in output</para>
    ''' <para>MergeIntoSingleDestination = 2, merge the bundled destinations in one destination in output</para>
    ''' </summary>
    Public Enum AddressBundlingMergeMode As UInteger
        KeepAsSeparateDestinations = 1
        MergeIntoSingleDestination = 2
    End Enum

    ''' <summary>
    ''' Enumeration of the service time first item mode:
    ''' <para>KeepOriginal = 1, keep original service time</para>
    ''' <para>CustomTime = 2, set custom time to service time</para>
    ''' </summary>
    Public Enum AddressBundlingFirstItemMode As UInteger
        KeepOriginal = 1
        CustomTime = 2
    End Enum

    ''' <summary>
    ''' Enumeration of the service time additional items mode:
    ''' <para>KeepOriginal = 1, preserve original address service time</para>
    ''' <para>CustomTime = 2, set custom times</para>
    ''' <para>InheritFromPrimary = 3, don't add service times</para>
    ''' </summary>
    Public Enum AddressBundlingAdditionalItemsMode As UInteger
        KeepOriginal = 1
        CustomTime = 2
        InheritFromPrimary = 3
    End Enum

    Public Enum MemberTypes
        <Description("PRIMARY_ACCOUNT")>
        AccountOwner

        <Description("SUB_ACCOUNT_ADMIN")>
        Administrator

        <Description("SUB_ACCOUNT_REGIONAL_MANAGER")>
        RegionalManager

        <Description("SUB_ACCOUNT_DISPATCHER")>
        Dispatcher

        <Description("SUB_ACCOUNT_PLANNER")>
        RoutePlanner

        <Description("PRIMARY_ACCOUNT")>
        PrimaryAccount

        <Description("SUB_ACCOUNT_DRIVER")>
        Driver

        <Description("SUB_ACCOUNT_ANALYST")>
        Analyst

        <Description("SUB_ACCOUNT_VENDOR")>
        Vendor
    End Enum
End Namespace
