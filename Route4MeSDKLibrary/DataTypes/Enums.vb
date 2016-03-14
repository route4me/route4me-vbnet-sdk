Imports System.ComponentModel

Namespace Route4MeSDK.DataTypes

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
    End Enum


    Public Enum TravelMode As UInteger
        <Description("Driving")> _
        Driving

        <Description("Walking")> _
        Walking

        <Description("Trucking")> _
        Trucking
    End Enum


    Public Enum DistanceUnit As UInteger
        <Description("mi")> _
        MI

        <Description("km")> _
        KM
    End Enum
    Public Enum Avoid
        <Description("Highways")> _
        Highways

        <Description("Tolls")> _
        Tolls

        <Description("minimizeHighways")> _
        MinimizeHighways

        <Description("minimizeTolls")> _
        MinimizeTolls

        <Description("")> _
        None
    End Enum


    Public Enum Optimize As UInteger
        <Description("Distance")> _
        Distance

        <Description("Time")> _
        Time

        <Description("timeWithTraffic")> _
        TimeWithTraffic
    End Enum


    Public Enum Metric As UInteger
        Euclidean = 1
        'measures point to point distance as a straight line
        Manhattan = 2
        'measures point to point distance as taxicab geometry line
        Geodesic = 3
        'measures point to point distance approximating curvature of the earth
        Matrix = 4
        'measures point to point distance by traversing the actual road network
        Exact_2D = 5
        'measures point to point distance using 2d rectilinear distance
    End Enum

    Public Enum DeviceType
        <Description("web")> _
        Web

        <Description("iphone")> _
        IPhone

        <Description("ipad")> _
        IPad

        <Description("android_phone")> _
        AndroidPhone

        <Description("android_tablet")> _
        AndroidTablet
    End Enum


    Public Enum Format
        <Description("csv")> _
        Csv

        <Description("serialized")> _
        Serialized

        <Description("xml")> _
        Xml
    End Enum


    'an optimization problem can be at one state at any given time
    'every state change invokes a socket notification to the associated member id
    'every state change invokes a callback webhook event invocation if it was provided during the initial optimization
    Public Enum OptimizationState As UInteger
        Initial = 1
        MatrixProcessing = 2
        Optimizing = 3
        Optimized = 4
        [Error] = 5
        ComputingDirections = 6
    End Enum

    'if the actual polylines of the driving path between all the stops on the route should be returned
    Public Enum RoutePathOutput As UInteger
        <Description("None")> _
        None

        <Description("Points")> _
        Points
    End Enum

    Public Enum StatusUpdateType
        <Description("pickup")> _
        Pickup

        <Description("dropoff")> _
        DropOff

        <Description("noanswer")> _
        NoAnswer

        <Description("notfound")> _
        NotFound

        <Description("notpaid")> _
        NotPaid

        <Description("paid")> _
        Paid

        <Description("wrongdelivery")> _
        WrongDelivery

        <Description("wrongaddressrecipient")> _
        WrongAddressRecipient

        <Description("notpresent")> _
        NotPresent

        <Description("parts_missing")> _
        PartsMissing

        <Description("service_rendered")> _
        ServiceRendered

        <Description("follow_up")> _
        FollowUp

        <Description("left_information")> _
        LeftInformation

        <Description("spoke_with_decision_maker")> _
        SpokeWithDecisionMaker

        <Description("spoke_with_decision_influencer")> _
        SpokeWithDecisionInfluencer

        <Description("competitive_account")> _
        CompetitiveAccount

        <Description("scheduled_follow_up_meeting")> _
        ScheduledFollowUpMeeting

        <Description("scheduled_lunch")> _
        ScheduledLunch

        <Description("scheduled_product_demo")> _
        ScheduledProductDemo

        <Description("scheduled_clinical_demo")> _
        ScheduledClinicalDemo

        <Description("no_opportunity")> _
        NoOpportunity
    End Enum

    ''' <summary>
    ''' Territory type (circle, rectangle, polygon)
    ''' </summary>
    Public Enum TerritoryType As UInteger
        <Description("circle")> _
        Circle

        <Description("poly")> _
        Poly

        <Description("rect")> _
        Rect
    End Enum
End Namespace
