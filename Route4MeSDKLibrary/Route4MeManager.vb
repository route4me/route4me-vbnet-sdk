﻿Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
'Imports Route4MeSDKLibrary.DataTypes
Imports System.Collections.Generic
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Threading.Tasks

Namespace Route4MeSDK
    ''' <summary>
    ''' This class encapsulates the Route4Me REST API
    ''' 1. Create an instance of Route4MeManager with the api_key
    ''' 1. Shortcut methods: Use shortcuts methods (for example Route4MeManager.GetOptimization()) to access the most popular functionality.
    '''    See examples Route4MeExamples.GetOptimization(), Route4MeExamples.SingleDriverRoundTrip()
    ''' 2. Generic methods: Use generic methods (for example Route4MeManager.GetJsonObjectFromAPI() or Route4MeManager.GetStringResponseFromAPI())
    '''    to access any availaible functionality.
    '''    See examples Route4MeExamples.GenericExample(), Route4MeExamples.SingleDriverRoundTripGeneric()
    ''' </summary>
    Public NotInheritable Class Route4MeManager
#Region "Fields"

        Private ReadOnly m_ApiKey As String
        Private ReadOnly m_DefaultTimeOut As New TimeSpan(TimeSpan.TicksPerMinute * 30)
        ' Default timeout - 30 minutes
        'private bool m_isTestMode = false;
#End Region

#Region "Methods"

#Region "Constructors"

        Public Sub New(apiKey As String)
            m_ApiKey = apiKey
        End Sub

#End Region

#Region "Route4Me Shortcut Methods"

#Region "Optimizations"

        Public Function RunOptimization(optimizationParameters As OptimizationParameters, ByRef errorString As String) As DataObject
            Dim result = GetJsonObjectFromAPI(Of DataObject)(optimizationParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Post, False, errorString)

            Return result
        End Function

        Public Function GetOptimization(optimizationParameters As OptimizationParameters, ByRef errorString As String) As DataObject
            Dim result = GetJsonObjectFromAPI(Of DataObject)(optimizationParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.[Get], errorString)

            Return result
        End Function

        ''' <summary>
        ''' </summary>
        <DataContract> _
        Private NotInheritable Class DataObjectOptimizations
            <DataMember(Name:="optimizations")> _
            Public Property Optimizations() As DataObject()
                Get
                    Return m_Optimizations
                End Get
                Set(value As DataObject())
                    m_Optimizations = Value
                End Set
            End Property
            Private m_Optimizations As DataObject()
        End Class

        Public Function GetOptimizations(queryParameters As RouteParametersQuery, ByRef errorString As String) As DataObject()
            Dim dataObjectOptimizations As DataObjectOptimizations = GetJsonObjectFromAPI(Of DataObjectOptimizations)(queryParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.[Get], errorString)
            Dim result As DataObject() = Nothing
            If dataObjectOptimizations IsNot Nothing Then
                result = dataObjectOptimizations.Optimizations
            End If
            Return result
        End Function

        Public Function UpdateOptimization(optimizationParameters As OptimizationParameters, ByRef errorString As String) As DataObject
            Dim result = GetJsonObjectFromAPI(Of DataObject)(optimizationParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, False, errorString)

            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class RemoveOptimizationResponse
            <DataMember(Name:="status")> _
            Public Property Status() As [Boolean]
                Get
                    Return m_Status
                End Get
                Set(value As [Boolean])
                    m_Status = value
                End Set
            End Property
            Private m_Status As [Boolean]

            <DataMember(Name:="removed")> _
            Public Property Removed() As Integer
                Get
                    Return m_Removed
                End Get
                Set(value As Integer)
                    m_Removed = value
                End Set
            End Property
            Private m_Removed As Integer
        End Class

        <DataContract> _
        Private NotInheritable Class RemoveOptimizationRequest
            Inherits GenericParameters
            <DataMember(Name:="optimization_problem_ids", EmitDefaultValue:=False)> _
            Public Property OptimizationProblemIDs() As String()
                Get
                    Return m_OptimizationProblemIDs
                End Get
                Set(value As String())
                    m_OptimizationProblemIDs = value
                End Set
            End Property
            Private m_OptimizationProblemIDs As String()
        End Class

        Public Function RemoveOptimization(optimizationProblemIDs As String(), ByRef errorString As String) As Boolean
            Dim request As New RemoveOptimizationRequest() With { _
                .OptimizationProblemIDs = optimizationProblemIDs _
            }

            Dim response As RemoveOptimizationResponse = GetJsonObjectFromAPI(Of RemoveOptimizationResponse)(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Delete, False, errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract> _
        Private NotInheritable Class RemoveDestinationFromOptimizationResponse
            <DataMember(Name:="deleted")> _
            Public Property Deleted As [Boolean]

            <DataMember(Name:="route_destination_id")> _
            Public Property RouteDestinationId As Integer

        End Class

        Public Function RemoveDestinationFromOptimization(optimizationId As String, destinationId As Integer, ByRef errorString As String) As Boolean
            Dim genericParameters As New GenericParameters()
            genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationId)
            genericParameters.ParametersCollection.Add("route_destination_id", destinationId.ToString())
            Dim response As RemoveDestinationFromOptimizationResponse = GetJsonObjectFromAPI(Of RemoveDestinationFromOptimizationResponse)(genericParameters, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Deleted Then
                Return True
            Else
                Return False
            End If
        End Function
#End Region

#Region "Hybrid Optimization"
        Public Function GetOHybridptimization(hybridOptimizationParameters As HybridOptimizationParameters, ByRef errorString As String) As DataObject
            Dim result = GetJsonObjectFromAPI(Of DataObject)(hybridOptimizationParameters, R4MEInfrastructureSettings.HybridOptimization, HttpMethodType.[Get], errorString)

            Return result
        End Function
#End Region

#Region "Routes"

        Public Function GetRoute(routeParameters As RouteParametersQuery, ByRef errorString As String) As DataObjectRoute
            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute)(routeParameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.[Get], errorString)

            Return result
        End Function

        Public Function GetRouteDirections(routeParameters As RouteParametersQuery, ByRef errorString As String) As RouteResponse
            Dim result = GetJsonObjectFromAPI(Of RouteResponse)(routeParameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.[Get], errorString)

            Return result
        End Function

        Public Function GetRoutes(routeParameters As RouteParametersQuery, ByRef errorString As String) As DataObjectRoute()
            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute())(routeParameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.[Get], errorString)

            Return result
        End Function

        Public Function GetRouteId(optimizationProblemId As String, ByRef errorString As String) As String
            Dim genericParameters As New GenericParameters()
            genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationProblemId)
            genericParameters.ParametersCollection.Add("wait_for_final_state", "1")
            Dim response As DataObject = GetJsonObjectFromAPI(Of DataObject)(genericParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.[Get], errorString)
            If response IsNot Nothing AndAlso response.Routes IsNot Nothing AndAlso response.Routes.Length > 0 Then
                Dim routeId As String = response.Routes(0).RouteID
                Return routeId
            End If
            Return Nothing
        End Function

        Public Function UpdateRoute(routeParameters As RouteParametersQuery, ByRef errorString As String) As DataObjectRoute
            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute)(routeParameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, errorString)

            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class UpdateRouteCustomDataRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
            Public Property RouteDestinationId() As System.Nullable(Of Integer)
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As System.Nullable(Of Integer)

            <DataMember(Name:="custom_fields", EmitDefaultValue:=False)> _
            Public Property CustomFields() As Dictionary(Of String, String)
                Get
                    Return m_CustomFields
                End Get
                Set(value As Dictionary(Of String, String))
                    m_CustomFields = value
                End Set
            End Property
            Private m_CustomFields As Dictionary(Of String, String)
        End Class

        Public Function UpdateRouteCustomData(routeParameters As RouteParametersQuery, customData As Dictionary(Of String, String), errorString As String) As Address
            Dim request As New UpdateRouteCustomDataRequest With { _
                .RouteId = routeParameters.RouteId, _
                .RouteDestinationId = routeParameters.RouteDestinationId, _
                .CustomFields = customData
            }

            Dim result = GetJsonObjectFromAPI(Of Address)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, errorString)

            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class UpdateRouteDestinationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
            Public Property RouteDestinationId As System.Nullable(Of Integer)

            <DataMember(Name:="alias", EmitDefaultValue:=False)> _
            Public Property [Alias] As String

            <DataMember(Name:="first_name", EmitDefaultValue:=False)> _
            Public Property FirstName() As String
                Get
                    Return m_FirstName
                End Get
                Set(value As String)
                    m_FirstName = value
                End Set
            End Property
            Private m_FirstName As String

            <DataMember(Name:="last_name", EmitDefaultValue:=False)> _
            Public Property LastName() As String
                Get
                    Return m_LastName
                End Get
                Set(value As String)
                    m_LastName = value
                End Set
            End Property
            Private m_LastName As String

            <DataMember(Name:="address", EmitDefaultValue:=False)> _
            Public Property AddressString() As String
                Get
                    Return m_AddressString
                End Get
                Set(value As String)
                    m_AddressString = value
                End Set
            End Property
            Private m_AddressString As String

            <DataMember(Name:="address_stop_type", EmitDefaultValue:=False)> _
            Public Property AddressStopType As String

            <DataMember(Name:="is_depot", EmitDefaultValue:=False)> _
            Public Property IsDepot() As System.Nullable(Of Boolean)
                Get
                    Return m_IsDepot
                End Get
                Set(value As System.Nullable(Of Boolean))
                    m_IsDepot = value
                End Set
            End Property
            Private m_IsDepot As System.Nullable(Of Boolean)

            'the latitude of this address
            <DataMember(Name:="lat", EmitDefaultValue:=False)> _
            Public Property Latitude As System.Nullable(Of Double)

            'the longitude of this address
            <DataMember(Name:="lng", EmitDefaultValue:=False)> _
            Public Property Longitude() As System.Nullable(Of Double)
                Get
                    Return m_Longitude
                End Get
                Set(value As System.Nullable(Of Double))
                    m_Longitude = value
                End Set
            End Property
            Private m_Longitude As System.Nullable(Of Double)

            <DataMember(Name:="sequence_no", EmitDefaultValue:=False)> _
            Public Property SequenceNo() As System.Nullable(Of Integer)
                Get
                    Return m_SequenceNo
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_SequenceNo = value
                End Set
            End Property
            Private m_SequenceNo As System.Nullable(Of Integer)

            'status flag to mark an address as visited (aka check in)
            <DataMember(Name:="is_visited", EmitDefaultValue:=False)> _
            Public Property IsVisited() As System.Nullable(Of Boolean)
                Get
                    Return m_IsVisited
                End Get
                Set(value As System.Nullable(Of Boolean))
                    m_IsVisited = value
                End Set
            End Property
            Private m_IsVisited As System.Nullable(Of Boolean)

            'status flag to mark an address as departed (aka check out)
            <DataMember(Name:="is_departed", EmitDefaultValue:=False)> _
            Public Property IsDeparted() As System.Nullable(Of Boolean)
                Get
                    Return m_IsDeparted
                End Get
                Set(value As System.Nullable(Of Boolean))
                    m_IsDeparted = value
                End Set
            End Property
            Private m_IsDeparted As System.Nullable(Of Boolean)

            'the last known visited timestamp of this address
            <DataMember(Name:="timestamp_last_visited", EmitDefaultValue:=False)> _
            Public Property TimestampLastVisited() As System.Nullable(Of UInteger)
                Get
                    Return m_TimestampLastVisited
                End Get
                Set(value As System.Nullable(Of UInteger))
                    m_TimestampLastVisited = value
                End Set
            End Property
            Private m_TimestampLastVisited As System.Nullable(Of UInteger)

            'the last known departed timestamp of this address
            <DataMember(Name:="timestamp_last_departed", EmitDefaultValue:=False)> _
            Public Property TimestampLastDeparted() As System.Nullable(Of UInteger)
                Get
                    Return m_TimestampLastDeparted
                End Get
                Set(value As System.Nullable(Of UInteger))
                    m_TimestampLastDeparted = value
                End Set
            End Property
            Private m_TimestampLastDeparted As System.Nullable(Of UInteger)

            <DataMember(Name:="group", EmitDefaultValue:=False)> _
            Public Property Group As String

            'pass-through data about this route destination
            'the data will be visible on the manifest, website, and mobile apps
            <DataMember(Name:="customer_po", EmitDefaultValue:=False)> _
            Public Property CustomerPo() As Object
                Get
                    Return m_CustomerPo
                End Get
                Set(value As Object)
                    m_CustomerPo = value
                End Set
            End Property
            Private m_CustomerPo As Object

            'pass-through data about this route destination
            'the data will be visible on the manifest, website, and mobile apps
            <DataMember(Name:="invoice_no", EmitDefaultValue:=False)> _
            Public Property InvoiceNo() As Object
                Get
                    Return m_InvoiceNo
                End Get
                Set(value As Object)
                    m_InvoiceNo = value
                End Set
            End Property
            Private m_InvoiceNo As Object

            'pass-through data about this route destination
            'the data will be visible on the manifest, website, and mobile apps
            <DataMember(Name:="reference_no", EmitDefaultValue:=False)> _
            Public Property ReferenceNo() As Object
                Get
                    Return m_ReferenceNo
                End Get
                Set(value As Object)
                    m_ReferenceNo = value
                End Set
            End Property
            Private m_ReferenceNo As Object

            'pass-through data about this route destination
            'the data will be visible on the manifest, website, and mobile apps
            <DataMember(Name:="order_no", EmitDefaultValue:=False)> _
            Public Property OrderNo() As Object
                Get
                    Return m_OrderNo
                End Get
                Set(value As Object)
                    m_OrderNo = value
                End Set
            End Property
            Private m_OrderNo As Object

            <DataMember(Name:="order_id", EmitDefaultValue:=False)> _
            Public Property OrderId() As System.Nullable(Of Integer)
                Get
                    Return m_OrderId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_OrderId = value
                End Set
            End Property
            Private m_OrderId As System.Nullable(Of Integer)

            <DataMember(Name:="weight", EmitDefaultValue:=False)> _
            Public Property Weight() As Object
                Get
                    Return m_Weight
                End Get
                Set(value As Object)
                    m_Weight = value
                End Set
            End Property
            Private m_Weight As Object

            <DataMember(Name:="cost", EmitDefaultValue:=False)> _
            Public Property Cost() As Object
                Get
                    Return m_Cost
                End Get
                Set(value As Object)
                    m_Cost = value
                End Set
            End Property
            Private m_Cost As Object

            <DataMember(Name:="revenue", EmitDefaultValue:=False)> _
            Public Property Revenue() As Object
                Get
                    Return m_Revenue
                End Get
                Set(value As Object)
                    m_Revenue = value
                End Set
            End Property
            Private m_Revenue As Object

            'the cubic volume that this destination/order/line-item consumes/contains
            'this is how much space it will take up on a vehicle
            <DataMember(Name:="cube", EmitDefaultValue:=False)> _
            Public Property Cube() As Object
                Get
                    Return m_Cube
                End Get
                Set(value As Object)
                    m_Cube = value
                End Set
            End Property
            Private m_Cube As Object

            'the number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle
            <DataMember(Name:="pieces", EmitDefaultValue:=False)> _
            Public Property Pieces() As Object
                Get
                    Return m_Pieces
                End Get
                Set(value As Object)
                    m_Pieces = value
                End Set
            End Property
            Private m_Pieces As Object

            <DataMember(Name:="email", EmitDefaultValue:=False)> _
            Public Property Email() As String
                Get
                    Return m_Email
                End Get
                Set(value As String)
                    m_Email = value
                End Set
            End Property
            Private m_Email As String

            <DataMember(Name:="phone", EmitDefaultValue:=False)> _
            Public Property Phone() As String
                Get
                    Return m_Phone
                End Get
                Set(value As String)
                    m_Phone = value
                End Set
            End Property
            Private m_Phone As String

            <DataMember(Name:="time_window_start", EmitDefaultValue:=False)> _
            Public Property TimeWindowStart() As System.Nullable(Of Integer)
                Get
                    Return m_TimeWindowStart
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_TimeWindowStart = value
                End Set
            End Property
            Private m_TimeWindowStart As System.Nullable(Of Integer)

            <DataMember(Name:="time_window_end", EmitDefaultValue:=False)> _
            Public Property TimeWindowEnd() As System.Nullable(Of Integer)
                Get
                    Return m_TimeWindowEnd
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_TimeWindowEnd = value
                End Set
            End Property
            Private m_TimeWindowEnd As System.Nullable(Of Integer)

            'the expected amount of time that will be spent at this address by the driver/user
            <DataMember(Name:="time", EmitDefaultValue:=False)> _
            Public Property Time() As System.Nullable(Of Integer)
                Get
                    Return m_Time
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Time = value
                End Set
            End Property
            Private m_Time As System.Nullable(Of Integer)

            <DataMember(Name:="notes", EmitDefaultValue:=False)> _
            Public Property Notes() As AddressNote()
                Get
                    Return m_Notes
                End Get
                Set(value As AddressNote())
                    m_Notes = value
                End Set
            End Property
            Private m_Notes As AddressNote()

            'if present, the priority will sequence addresses in all the optimal routes so that
            'higher priority addresses are general at the beginning of the route sequence
            '1 is the highest priority, 100000 is the lowest
            <DataMember(Name:="priority", EmitDefaultValue:=False)> _
            Public Property Priority() As System.Nullable(Of Integer)
                Get
                    Return m_Priority
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Priority = value
                End Set
            End Property
            Private m_Priority As System.Nullable(Of Integer)

            'generate optimal routes and driving directions to this curbside lat
            <DataMember(Name:="curbside_lat", EmitDefaultValue:=False)> _
            Public Property CurbsideLatitude() As System.Nullable(Of Double)
                Get
                    Return m_CurbsideLatitude
                End Get
                Set(value As System.Nullable(Of Double))
                    m_CurbsideLatitude = value
                End Set
            End Property
            Private m_CurbsideLatitude As System.Nullable(Of Double)

            'generate optimal routes and driving directions to the curbside lang
            <DataMember(Name:="curbside_lng", EmitDefaultValue:=False)> _
            Public Property CurbsideLongitude() As System.Nullable(Of Double)
                Get
                    Return m_CurbsideLongitude
                End Get
                Set(value As System.Nullable(Of Double))
                    m_CurbsideLongitude = value
                End Set
            End Property
            Private m_CurbsideLongitude As System.Nullable(Of Double)

            <DataMember(Name:="time_window_start_2", EmitDefaultValue:=False)> _
            Public Property TimeWindowStart2() As System.Nullable(Of Integer)
                Get
                    Return m_TimeWindowStart2
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_TimeWindowStart2 = value
                End Set
            End Property
            Private m_TimeWindowStart2 As System.Nullable(Of Integer)

            <DataMember(Name:="time_window_end_2", EmitDefaultValue:=False)> _
            Public Property TimeWindowEnd2() As System.Nullable(Of Integer)
                Get
                    Return m_TimeWindowEnd2
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_TimeWindowEnd2 = value
                End Set
            End Property
            Private m_TimeWindowEnd2 As System.Nullable(Of Integer)

            <DataMember(Name:="custom_fields", EmitDefaultValue:=False)> _
            Public Property CustomFields() As Dictionary(Of String, String)
                Get
                    Return m_CustomFields
                End Get
                Set(value As Dictionary(Of String, String))
                    m_CustomFields = value
                End Set
            End Property
            Private m_CustomFields As Dictionary(Of String, String)
        End Class

        Public Function UpdateRouteDestination(addressParameters As Address, ByRef errorString As String) As Address
            Dim request As New UpdateRouteDestinationRequest() With { _
                .RouteId = addressParameters.RouteId, _
                .RouteDestinationId = addressParameters.RouteDestinationId _
            }

            If addressParameters.[Alias] IsNot Nothing Then
                request.[Alias] = addressParameters.[Alias]
            End If
            If addressParameters.FirstName IsNot Nothing Then
                request.FirstName = addressParameters.FirstName
            End If
            If addressParameters.LastName IsNot Nothing Then
                request.LastName = addressParameters.LastName
            End If
            If addressParameters.AddressString IsNot Nothing Then
                request.AddressString = addressParameters.AddressString
            End If
            If addressParameters.AddressStopType IsNot Nothing Then
                request.AddressStopType = addressParameters.AddressStopType
            End If
            If addressParameters.IsDepot IsNot Nothing Then
                request.IsDepot = addressParameters.IsDepot
            End If
            If addressParameters.Latitude <> Nothing Then
                request.Latitude = addressParameters.Latitude
            End If
            If addressParameters.Longitude <> Nothing Then
                request.Longitude = addressParameters.Longitude
            End If

            If addressParameters.SequenceNo IsNot Nothing Then
                request.SequenceNo = addressParameters.SequenceNo
            End If
            If addressParameters.IsVisited IsNot Nothing Then
                request.IsVisited = addressParameters.IsVisited
            End If
            If addressParameters.IsDeparted IsNot Nothing Then
                request.IsDeparted = addressParameters.IsDeparted
            End If
            If addressParameters.TimestampLastVisited IsNot Nothing Then
                request.TimestampLastVisited = addressParameters.TimestampLastVisited
            End If
            If addressParameters.TimestampLastDeparted IsNot Nothing Then
                request.TimestampLastDeparted = addressParameters.TimestampLastDeparted
            End If
            If addressParameters.Group IsNot Nothing Then
                request.Group = addressParameters.Group
            End If
            If addressParameters.CustomerPo IsNot Nothing Then
                request.CustomerPo = addressParameters.CustomerPo
            End If
            If addressParameters.InvoiceNo IsNot Nothing Then
                request.InvoiceNo = addressParameters.InvoiceNo
            End If
            If addressParameters.ReferenceNo IsNot Nothing Then
                request.ReferenceNo = addressParameters.ReferenceNo
            End If
            If addressParameters.OrderNo IsNot Nothing Then
                request.OrderNo = addressParameters.OrderNo
            End If
            If addressParameters.OrderId IsNot Nothing Then
                request.OrderId = addressParameters.OrderId
            End If
            If addressParameters.Weight IsNot Nothing Then
                request.Weight = addressParameters.Weight
            End If
            If addressParameters.Cost IsNot Nothing Then
                request.Cost = addressParameters.Cost
            End If
            If addressParameters.Revenue IsNot Nothing Then
                request.Revenue = addressParameters.Revenue
            End If
            If addressParameters.Cube IsNot Nothing Then
                request.Cube = addressParameters.Cube
            End If
            If addressParameters.Pieces IsNot Nothing Then
                request.Pieces = addressParameters.Pieces
            End If
            If addressParameters.Phone IsNot Nothing Then
                request.Phone = addressParameters.Phone.ToString()
            End If

            If addressParameters.TimeWindowStart IsNot Nothing Then
                request.TimeWindowStart = addressParameters.TimeWindowStart
            End If
            If addressParameters.TimeWindowEnd IsNot Nothing Then
                request.TimeWindowEnd = addressParameters.TimeWindowEnd
            End If
            If addressParameters.Notes IsNot Nothing Then
                request.Notes = addressParameters.Notes
            End If
            If addressParameters.Priority IsNot Nothing Then
                request.Priority = addressParameters.Priority
            End If
            If addressParameters.CurbsideLatitude IsNot Nothing Then
                request.CurbsideLatitude = addressParameters.CurbsideLatitude
            End If
            If addressParameters.CurbsideLongitude IsNot Nothing Then
                request.CurbsideLongitude = addressParameters.CurbsideLongitude
            End If
            If addressParameters.TimeWindowStart2 IsNot Nothing Then
                request.TimeWindowStart2 = addressParameters.TimeWindowStart2
            End If
            If addressParameters.TimeWindowEnd2 IsNot Nothing Then
                request.TimeWindowEnd2 = addressParameters.TimeWindowEnd2
            End If
            If addressParameters.CustomFields IsNot Nothing Then
                request.CustomFields = addressParameters.CustomFields
            End If

            Dim result = GetJsonObjectFromAPI(Of Address)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, errorString)

            Return result
        End Function

        Public Function MergeRoutes(mergeRoutesParameters As MergeRoutesQuery, ByRef errorString As String) As Boolean
            Dim roParames As New GenericParameters()

            Dim keyValues As New List(Of KeyValuePair(Of String, String))()

            keyValues.Add(New KeyValuePair(Of String, String)("route_ids", mergeRoutesParameters.RouteIds))
            keyValues.Add(New KeyValuePair(Of String, String)("depot_address", mergeRoutesParameters.DepotAddress))
            keyValues.Add(New KeyValuePair(Of String, String)("remove_origin", mergeRoutesParameters.RemoveOrigin.ToString()))
            keyValues.Add(New KeyValuePair(Of String, String)("depot_lat", mergeRoutesParameters.DepotLat.ToString()))
            keyValues.Add(New KeyValuePair(Of String, String)("depot_lng", mergeRoutesParameters.DepotLng.ToString()))

            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As ResequenceReoptimizeRouteResponse = GetJsonObjectFromAPI(Of ResequenceReoptimizeRouteResponse)(roParames, R4MEInfrastructureSettings.MergeRoutes, HttpMethodType.Post, httpContent, errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract> _
        Private NotInheritable Class ResequenceReoptimizeRouteResponse
            <DataMember(Name:="status")> _
            Public Property Status() As [Boolean]
                Get
                    Return m_Status
                End Get
                Set(value As [Boolean])
                    m_Status = value
                End Set
            End Property
            Private m_Status As [Boolean]
        End Class

        Public Function ResequenceReoptimizeRoute(roParames As Dictionary(Of String, String), ByRef errorString As String) As Boolean
            Dim request As New RouteParametersQuery With { _
               .RouteId = roParames.Item("route_id"), _
               .DisableOptimization = Val(roParames.Item("route_id")), _
               .Optimize = roParames.Item("optimize")
            }

            Dim response As ResequenceReoptimizeRouteResponse = GetJsonObjectFromAPI(Of ResequenceReoptimizeRouteResponse)(request, R4MEInfrastructureSettings.RouteReoptimize, HttpMethodType.[Get], errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function RouteSharing(roParames As RouteParametersQuery, Email As String, ByRef errorString As String) As Boolean
            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            keyValues.Add(New KeyValuePair(Of String, String)("recipient_email", Email))
            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As ResequenceReoptimizeRouteResponse = GetJsonObjectFromAPI(Of ResequenceReoptimizeRouteResponse)(roParames, R4MEInfrastructureSettings.RouteSharing, HttpMethodType.Post, httpContent, errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract> _
        Private NotInheritable Class ResequenceRouteDestinationRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
            Public Property RouteDestinationId() As System.Nullable(Of Integer)
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As System.Nullable(Of Integer)

            <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()

        End Class

        Public Function ResequenceRouteDestination(routeParames As RouteParametersQuery, addresses As Address(), ByRef errorString As String) As RouteResponse
            Dim request As New ResequenceRouteDestinationRequest With { _
               .RouteId = routeParames.RouteId, _
               .RouteDestinationId = routeParames.RouteDestinationId, _
               .Addresses = addresses
            }
            '.RouteId = routeParames.RouteId _
            '.RouteDestinationId = routeParames.RouteDestinationId
            '}

            Dim response As RouteResponse = GetJsonObjectFromAPI(Of RouteResponse)(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, False, errorString)

            Return response
        End Function

        <DataContract> _
        Private NotInheritable Class DuplicateRouteResponse
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

            <DataMember(Name:="success")> _
            Public Property Success() As [Boolean]
                Get
                    Return m_Success
                End Get
                Set(value As [Boolean])
                    m_Success = value
                End Set
            End Property
            Private m_Success As [Boolean]
        End Class

        Public Function DuplicateRoute(queryParameters As RouteParametersQuery, ByRef errorString As String) As String
            'if (queryParameters.ParametersCollection["to"] == null)
            '  queryParameters.ParametersCollection.Add("to", "none");
            ' Redirect to page or return json for none
            queryParameters.ParametersCollection("to") = "none"
            Dim response As DuplicateRouteResponse = GetJsonObjectFromAPI(Of DuplicateRouteResponse)(queryParameters, R4MEInfrastructureSettings.DuplicateRoute, HttpMethodType.[Get], errorString)
            Dim routeId As String = Nothing
            If response IsNot Nothing AndAlso response.Success Then
                Dim optimizationProblemId As String = response.OptimizationProblemId
                If optimizationProblemId IsNot Nothing Then
                    routeId = Me.GetRouteId(optimizationProblemId, errorString)
                End If
            End If
            Return routeId
        End Function

        <DataContract> _
        Private NotInheritable Class DeleteRouteResponse
            <DataMember(Name:="deleted")> _
            Public Property Deleted() As [Boolean]
                Get
                    Return m_Deleted
                End Get
                Set(value As [Boolean])
                    m_Deleted = value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="errors")> _
            Public Property Errors() As List(Of [String])
                Get
                    Return m_Errors
                End Get
                Set(value As List(Of [String]))
                    m_Errors = value
                End Set
            End Property
            Private m_Errors As List(Of [String])

            <DataMember(Name:="route_id")> _
            Public Property routeId() As String
                Get
                    Return m_routeId
                End Get
                Set(value As String)
                    m_routeId = value
                End Set
            End Property
            Private m_routeId As String

            <DataMember(Name:="route_ids")> _
            Public Property routeIds() As String()
                Get
                    Return m_routeIds
                End Get
                Set(value As String())
                    m_routeIds = value
                End Set
            End Property
            Private m_routeIds As String()
        End Class

        Public Function DeleteRoutes(routeIds As String(), ByRef errorString As String) As String()
            Dim str_route_ids As String = ""
            For Each routeId As String In routeIds
                If str_route_ids.Length > 0 Then
                    str_route_ids += ","
                End If
                str_route_ids += routeId
            Next
            Dim genericParameters As New GenericParameters()
            genericParameters.ParametersCollection.Add("route_id", str_route_ids)
            Dim response As DeleteRouteResponse = GetJsonObjectFromAPI(Of DeleteRouteResponse)(genericParameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Delete, errorString)
            Dim deletedRouteIds As String() = Nothing
            If response IsNot Nothing Then
                deletedRouteIds = response.routeIds
            End If
            Return deletedRouteIds
        End Function

#End Region

#Region "Tracking"

        Public Function GetLastLocation(parameters As GenericParameters, ByRef errorString As String) As DataObject
            Dim result = GetJsonObjectFromAPI(Of DataObject)(parameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.[Get], False, errorString)

            Return result
        End Function

        Public Function SetGPS(gpsParameters As GPSParameters, ByRef errorString As String) As String
            Dim result = GetJsonObjectFromAPI(Of String)(gpsParameters, R4MEInfrastructureSettings.SetGpsHost, HttpMethodType.[Get], True, errorString)

            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class FindAssetRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="tracking", EmitDefaultValue:=False)> _
            Public Property Tracking() As String
                Get
                    Return m_Tracking
                End Get
                Set(value As String)
                    m_Tracking = value
                End Set
            End Property
            Private m_Tracking As String

        End Class

        Public Function FindAsset(tracking As String, ByRef errorString As String) As FindAssetResponse

            Dim request As New FindAssetRequest With { _
               .Tracking = tracking _
            }

            Dim result As FindAssetResponse = GetJsonObjectFromAPI(Of FindAssetResponse)(request, R4MEInfrastructureSettings.AssetTracking, HttpMethodType.[Get], False, errorString)

            Return result
        End Function

#End Region

#Region "Users"

        Public Function GetUsers(parameters As GenericParameters, ByRef errorString As String) As User()
            Dim result = GetJsonObjectFromAPI(Of User())(parameters, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.[Get], errorString)

            Return result
        End Function

        Public Function UserAuthentication(memParams As MemberParameters, ByRef errorString As String) As MemberResponse

            Dim roParams As New MemberParameters()

            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            keyValues.Add(New KeyValuePair(Of String, String)("strEmail", memParams.StrEmail))
            keyValues.Add(New KeyValuePair(Of String, String)("strPassword", memParams.StrPassword))
            keyValues.Add(New KeyValuePair(Of String, String)("format", memParams.Format))

            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As MemberResponse = GetJsonObjectFromAPI(Of MemberResponse)(roParams, R4MEInfrastructureSettings.UserAuthentication, HttpMethodType.Post, httpContent, errorString)

            Return response
        End Function

        <DataContract> _
        Private NotInheritable Class ValidateSessionRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="session_guid", EmitDefaultValue:=False)> _
            Public Property SessionGuid() As String
                Get
                    Return m_SessionGuid
                End Get
                Set(value As String)
                    m_SessionGuid = value
                End Set
            End Property
            Private m_SessionGuid As String

            <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)> _
            Public Property MemberId() As System.Nullable(Of Integer)
                Get
                    Return m_MemberId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_MemberId = value
                End Set
            End Property
            Private m_MemberId As System.Nullable(Of Integer)

            <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)> _
            Public Property Format() As String
                Get
                    Return m_Format
                End Get
                Set(value As String)
                    m_Format = value
                End Set
            End Property
            Private m_Format As String

        End Class

        Public Function ValidateSession(memParams As MemberParameters, ByRef errorString As String) As MemberResponse
            Dim request As ValidateSessionRequest = New ValidateSessionRequest() With { _
                .SessionGuid = memParams.SessionGuid, _
                .MemberId = memParams.MemberId, _
                .Format = memParams.Format _
            }

            Dim result As MemberResponse = GetJsonObjectFromAPI(Of MemberResponse)(request, R4MEInfrastructureSettings.ValidateSession, HttpMethodType.[Get], False, errorString)

            Return result

        End Function

        Public Function UserRegistration(memParams As MemberParameters, ByRef errorString As String) As MemberResponse

            Dim roParams As MemberParameters = New MemberParameters()
            roParams.Plan = memParams.Plan
            roParams.MemberType = memParams.MemberType

            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            keyValues.Add(New KeyValuePair(Of String, String)("strIndustry", memParams.StrIndustry))
            keyValues.Add(New KeyValuePair(Of String, String)("strFirstName", memParams.StrFirstName))
            keyValues.Add(New KeyValuePair(Of String, String)("strLastName", memParams.StrLastName))
            keyValues.Add(New KeyValuePair(Of String, String)("strEmail", memParams.StrEmail))
            keyValues.Add(New KeyValuePair(Of String, String)("format", memParams.Format))
            keyValues.Add(New KeyValuePair(Of String, String)("chkTerms", memParams.ChkTerms))
            keyValues.Add(New KeyValuePair(Of String, String)("device_type", memParams.DeviceType))
            keyValues.Add(New KeyValuePair(Of String, String)("strPassword_1", memParams.StrPassword_1))
            keyValues.Add(New KeyValuePair(Of String, String)("strPassword_2", memParams.StrPassword_2))

            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As MemberResponse = GetJsonObjectFromAPI(Of MemberResponse)(roParams, R4MEInfrastructureSettings.UserRegistration, HttpMethodType.Post, httpContent, errorString)

            Return response

        End Function

        Public Function GetUserById(memParams As MemberParametersV4, ByRef errorString As String) As MemberResponseV4
            Dim response As MemberResponseV4 = GetJsonObjectFromAPI(Of MemberResponseV4)(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Get, errorString)
            Return response
        End Function

        Public Function CreateUser(memParams As MemberParametersV4, ByRef errorString As String) As MemberResponseV4

            Dim response As MemberResponseV4 = GetJsonObjectFromAPI(Of MemberResponseV4)(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Post, errorString)
            Return response

        End Function

        Public Function UserUpdate(memParams As MemberParametersV4, ByRef errorString As String) As MemberResponseV4
            Dim response As MemberResponseV4 = GetJsonObjectFromAPI(Of MemberResponseV4)(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Put, errorString)
            Return response
        End Function

        <DataContract> _
        Public NotInheritable Class USerDeleteResponse
            <DataMember(Name:="status")> _
            Public Property Status() As Boolean
                Get
                    Return m_Status
                End Get
                Set(value As Boolean)
                    m_Status = value
                End Set
            End Property
            Private m_Status As Boolean
        End Class

        Public Function UserDelete(memParams As MemberParametersV4, ByRef errorString As String) As Boolean
            Dim response As USerDeleteResponse = GetJsonObjectFromAPI(Of USerDeleteResponse)(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Delete, errorString)

            If response Is Nothing Then
                Return False
            End If

            If response.Status Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Function CreateNewConfigurationKey(confParams As MemberConfigurationParameters, ByRef errorString As String) As MemberConfigurationResponse
            Dim response As MemberConfigurationResponse = GetJsonObjectFromAPI(Of MemberConfigurationResponse)(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Post, errorString)

            Return response

        End Function

        Public Function RemoveConfigurationKey(confParams As MemberConfigurationParameters, ByRef errorString As String) As MemberConfigurationResponse
            Dim response As MemberConfigurationResponse = GetJsonObjectFromAPI(Of MemberConfigurationResponse)(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Delete, errorString)

            Return response

        End Function

        Public Function UpdateConfigurationKey(confParams As MemberConfigurationParameters, ByRef errorString As String) As MemberConfigurationResponse
            Dim response As MemberConfigurationResponse = GetJsonObjectFromAPI(Of MemberConfigurationResponse)(confParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Put, errorString)

            Return response

        End Function

        <DataContract> _
        Private NotInheritable Class GetConfigurationDataRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="config_key", EmitDefaultValue:=False)> _
            Public Property config_key() As String
                Get
                    Return m_config_key
                End Get
                Set(value As String)
                    m_config_key = value
                End Set
            End Property
            Private m_config_key As String

        End Class

        Public Function GetConfigurationData(confParams As MemberConfigurationParameters, ByRef errorString As String) As MemberConfigurationDataRersponse
            Dim mParams As GetConfigurationDataRequest
            mParams = New GetConfigurationDataRequest()
            If Not confParams Is Nothing Then
                mParams.config_key = confParams.config_key
            End If

            Dim response As MemberConfigurationDataRersponse = GetJsonObjectFromAPI(Of MemberConfigurationDataRersponse)(mParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Get, errorString)

            Return response

        End Function

#End Region

#Region "Address Notes"

        Public Function GetAddressNotes(noteParameters As NoteParameters, ByRef errorString As String) As AddressNote()
            Dim addressParameters As New AddressParameters() With { _
                .RouteId = noteParameters.RouteId, _
                .RouteDestinationId = noteParameters.AddressId, _
                .Notes = True _
            }
            Dim address As Address = Me.GetAddress(addressParameters, errorString)
            If address IsNot Nothing Then
                Return address.Notes
            Else
                Return Nothing
            End If
        End Function

        <DataContract> _
        Private NotInheritable Class AddAddressNoteResponse
            <DataMember(Name:="status")> _
            Public Property Status() As Boolean
                Get
                    Return m_Status
                End Get
                Set(value As Boolean)
                    m_Status = value
                End Set
            End Property
            Private m_Status As Boolean

            <DataMember(Name:="note")> _
            Public Property Note() As AddressNote
                Get
                    Return m_Note
                End Get
                Set(value As AddressNote)
                    m_Note = value
                End Set
            End Property
            Private m_Note As AddressNote
        End Class

        Public Function AddAddressNote(noteParameters As NoteParameters, noteContents As String, ByRef errorString As String) As AddressNote
            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            Dim strUpdateType = "unclassified"
            If noteParameters.ActivityType IsNot Nothing AndAlso noteParameters.ActivityType.Length > 0 Then
                strUpdateType = noteParameters.ActivityType
            End If
            keyValues.Add(New KeyValuePair(Of String, String)("strUpdateType", strUpdateType))
            keyValues.Add(New KeyValuePair(Of String, String)("strNoteContents", noteContents))
            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As AddAddressNoteResponse = GetJsonObjectFromAPI(Of AddAddressNoteResponse)(noteParameters, R4MEInfrastructureSettings.AddRouteNotesHost, HttpMethodType.Post, httpContent, errorString)
            If response IsNot Nothing Then
                If response.Note IsNot Nothing Then
                    Return response.Note
                Else
                    If response.Status = False Then
                        errorString = "Note not added"
                    End If
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function

        Public Function AddAddressNote(noteParameters As NoteParameters, noteContents As String, attachmentFilePath As String, ByRef errorString As String) As AddressNote
            Dim strUpdateType = "unclassified"
            If noteParameters.ActivityType IsNot Nothing AndAlso noteParameters.ActivityType.Length > 0 Then
                strUpdateType = noteParameters.ActivityType
            End If
            Dim httpContent As HttpContent = Nothing
            Dim attachmentFileStream As FileStream = Nothing
            Dim attachmentStreamContent As StreamContent = Nothing
            If attachmentFilePath IsNot Nothing Then
                attachmentFileStream = File.OpenRead(attachmentFilePath)
                attachmentStreamContent = New StreamContent(attachmentFileStream)
                Dim multipartFormDataContent As New MultipartFormDataContent()
                multipartFormDataContent.Add(attachmentStreamContent, "strFilename", Path.GetFileName(attachmentFilePath))
                multipartFormDataContent.Add(New StringContent(strUpdateType), "strUpdateType")
                multipartFormDataContent.Add(New StringContent(noteContents), "strNoteContents")
                httpContent = multipartFormDataContent
            Else
                Dim keyValues = New List(Of KeyValuePair(Of String, String))()
                keyValues.Add(New KeyValuePair(Of String, String)("strUpdateType", strUpdateType))
                keyValues.Add(New KeyValuePair(Of String, String)("strNoteContents", noteContents))
                httpContent = New FormUrlEncodedContent(keyValues)
            End If
            Dim response As AddAddressNoteResponse = GetJsonObjectFromAPI(Of AddAddressNoteResponse)(noteParameters, R4MEInfrastructureSettings.AddRouteNotesHost, HttpMethodType.Post, httpContent, errorString)
            If attachmentStreamContent IsNot Nothing Then
                attachmentStreamContent.Dispose()
            End If
            If attachmentFileStream IsNot Nothing Then
                attachmentFileStream.Dispose()
            End If
            If response IsNot Nothing Then
                If response.Note IsNot Nothing Then
                    Return response.Note
                Else
                    If response.Status = False Then
                        errorString = "Note not added"
                    End If
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function

#End Region

#Region "Activities"

        <DataContract> _
        Private NotInheritable Class GetActivitiesResponse
            <DataMember(Name:="results")> _
            Public Property Results As Activity()

            <DataMember(Name:="total")> _
            Public Property Total As UInteger

        End Class

        Public Function GetActivityFeed(activityParameters As ActivityParameters, ByRef errorString As String) As Activity()
            Dim response As GetActivitiesResponse = GetJsonObjectFromAPI(Of GetActivitiesResponse)(activityParameters, R4MEInfrastructureSettings.GetActivitiesHost, HttpMethodType.[Get], errorString)
            Dim result As Activity() = Nothing
            If response IsNot Nothing Then
                result = response.Results
            End If
            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class LogCustomActivityResponse
            <DataMember(Name:="status")> _
            Public Property Status As Boolean

        End Class

        ''' <summary>
        ''' Create User Activity. Send custom message to Activity Stream.
        ''' </summary>
        ''' <param name="activity"> Input Activity object to add </param>
        ''' <param name="errorString"> Error string </param>
        ''' <returns> True/False </returns>
        Public Function LogCustomActivity(activity As Activity, ByRef errorString As String) As Boolean
            activity.PrepareForSerialization()
            Dim response As LogCustomActivityResponse = GetJsonObjectFromAPI(Of LogCustomActivityResponse)(activity, R4MEInfrastructureSettings.ActivityFeed, HttpMethodType.Post, errorString)
            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region "Destinations"

        Public Function GetAddress(addressParameters As AddressParameters, ByRef errorString As String) As Address
            Dim result = GetJsonObjectFromAPI(Of Address)(addressParameters, R4MEInfrastructureSettings.GetAddress, HttpMethodType.[Get], errorString)

            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class AddRouteDestinationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()
        End Class

        Public Function AddRouteDestinations(routeId As String, addresses As Address(), ByRef errorString As String) As Integer()
            Dim request As New AddRouteDestinationRequest() With { _
                .RouteId = routeId, _
                .Addresses = addresses _
            }
            Dim response As DataObject = GetJsonObjectFromAPI(Of DataObject)(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, errorString)
            Dim destinationIds As Integer() = Nothing
            If response IsNot Nothing AndAlso response.Addresses IsNot Nothing Then
                Dim arrDestinationIds As New List(Of Integer)()
                For Each addressNew As Address In addresses
                    Dim destinationId As Integer = 0
                    For Each addressResp As Address In response.Addresses
                        If addressResp.AddressString = addressNew.AddressString AndAlso addressResp.Latitude = addressNew.Latitude AndAlso addressResp.Longitude = addressNew.Longitude AndAlso addressResp.RouteDestinationId IsNot Nothing Then
                            destinationId = CInt(addressResp.RouteDestinationId)
                        End If
                    Next
                    arrDestinationIds.Add(destinationId)
                Next
                destinationIds = arrDestinationIds.ToArray()
            End If
            Return destinationIds
        End Function

        <DataContract> _
        Private NotInheritable Class MarkAddressAsMarkedAsDepartedRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
            Public Property RouteDestinationId() As System.Nullable(Of Integer)
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As System.Nullable(Of Integer)

            <IgnoreDataMember> _
            <DataMember(Name:="is_departed")> _
            Public Property IsDeparted() As Boolean
                Get
                    Return m_IsDeparted
                End Get
                Set(value As Boolean)
                    m_IsDeparted = value
                End Set
            End Property
            Private m_IsDeparted As Boolean

            <IgnoreDataMember> _
            <DataMember(Name:="is_visited")> _
            Public Property IsVisited() As Boolean
                Get
                    Return m_IsVisited
                End Get
                Set(value As Boolean)
                    m_IsVisited = value
                End Set
            End Property
            Private m_IsVisited As Boolean
        End Class

        Public Function MarkAddressAsMarkedAsDeparted(aParams As AddressParameters, ByRef errorString As String) As Address
            Dim request As New MarkAddressAsMarkedAsDepartedRequest With { _
                .RouteId = aParams.RouteId, _
                .RouteDestinationId = aParams.RouteDestinationId, _
                .IsDeparted = aParams.IsDeparted _
            }

            Dim response As Address = GetJsonObjectFromAPI(Of Address)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.[Put], errorString)

            Return response
        End Function

        Public Function MarkAddressAsMarkedAsVisited(aParams As AddressParameters, ByRef errorString As String) As Address
            Dim request As New MarkAddressAsMarkedAsDepartedRequest With { _
                .RouteId = aParams.RouteId, _
                .RouteDestinationId = aParams.RouteDestinationId, _
                .IsVisited = aParams.IsVisited _
            }

            Dim response As Address = GetJsonObjectFromAPI(Of Address)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.[Put], errorString)

            Return response
        End Function

        <DataContract> _
        Private NotInheritable Class MarkAddressDepartedRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)> _
            Public Property AddressId() As System.Nullable(Of Integer)
                Get
                    Return m_AddressId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_AddressId = value
                End Set
            End Property
            Private m_AddressId As System.Nullable(Of Integer)

            <IgnoreDataMember> _
            <HttpQueryMemberAttribute(Name:="is_departed", EmitDefaultValue:=False)> _
            Public Property IsDeparted() As Boolean
                Get
                    Return m_IsDeparted
                End Get
                Set(value As Boolean)
                    m_IsDeparted = value
                End Set
            End Property
            Private m_IsDeparted As Boolean

            <IgnoreDataMember> _
            <HttpQueryMemberAttribute(Name:="is_visited", EmitDefaultValue:=False)> _
            Public Property IsVisited() As Boolean
                Get
                    Return m_IsVisited
                End Get
                Set(value As Boolean)
                    m_IsVisited = value
                End Set
            End Property
            Private m_IsVisited As Boolean

            <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)> _
            Public Property MemberId() As System.Nullable(Of Integer)
                Get
                    Return m_MemberId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_MemberId = value
                End Set
            End Property
            Private m_MemberId As System.Nullable(Of Integer)
        End Class

        <DataContract> _
        Private NotInheritable Class MarkAddressDepartedResponse
            <DataMember(Name:="status")> _
            Public Property Status() As [Boolean]
                Get
                    Return m_Status
                End Get
                Set(value As [Boolean])
                    m_Status = value
                End Set
            End Property
            Private m_Status As [Boolean]

            <DataMember(Name:="error")> _
            Public Property [error]() As String
                Get
                    Return m_error
                End Get
                Set(value As String)
                    m_error = value
                End Set
            End Property
            Private m_error As String
        End Class

        Public Function MarkAddressDeparted(aParams As AddressParameters, ByRef errorString As String) As Integer
            Dim request As New MarkAddressDepartedRequest With { _
                .RouteId = aParams.RouteId, _
                .AddressId = aParams.AddressId, _
                .IsDeparted = aParams.IsDeparted, _
                .MemberId = 1 _
            }

            Dim response As MarkAddressDepartedResponse = GetJsonObjectFromAPI(Of MarkAddressDepartedResponse)(request, R4MEInfrastructureSettings.MarkAddressDeparted, HttpMethodType.[Get], errorString)

            If response IsNot Nothing Then
                If response.Status Then
                    Return 1
                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        End Function

        Public Function MarkAddressVisited(aParams As AddressParameters, ByRef errorString As String) As Integer
            Dim request As New MarkAddressDepartedRequest With { _
                .RouteId = aParams.RouteId, _
                .AddressId = aParams.AddressId, _
                .IsVisited = aParams.IsVisited, _
                .MemberId = 1 _
            }

            Dim response As String = GetJsonObjectFromAPI(Of String)(request, R4MEInfrastructureSettings.MarkAddressVisited, HttpMethodType.[Get], errorString)
            Dim iResponse As Integer = 0
            If Integer.TryParse(response.ToString(), iResponse) Then
                iResponse = Convert.ToInt32(response)
            End If
            Return iResponse
        End Function

        <DataContract> _
        Private NotInheritable Class InsertAddressIntoRouteOptimalPositionRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()

            <DataMember(Name:="optimal_position", EmitDefaultValue:=False)> _
            Public Property OptimalPosition() As Boolean
                Get
                    Return m_OptimalPosition
                End Get
                Set(value As Boolean)
                    m_OptimalPosition = value
                End Set
            End Property
            Private m_OptimalPosition As String
        End Class

        Public Function InsertAddressIntoRouteOptimalPosition(routeId As String, addresses As Address(), optimalPosition As Boolean, ByRef errorString As String) As Integer()
            Dim request As New InsertAddressIntoRouteOptimalPositionRequest() With { _
                .RouteId = routeId, _
                .Addresses = addresses, _
                .OptimalPosition = optimalPosition
            }

            Dim response As DataObject = GetJsonObjectFromAPI(Of DataObject)(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, errorString)
            Dim destinationIds As Integer() = Nothing
            If response IsNot Nothing AndAlso response.Addresses IsNot Nothing Then
                Dim arrDestinationIds As New List(Of Integer)()
                For Each addressNew As Address In addresses
                    Dim destinationId As Integer = 0
                    For Each addressResp As Address In response.Addresses
                        If addressResp.AddressString = addressNew.AddressString AndAlso addressResp.Latitude = addressNew.Latitude AndAlso addressResp.Longitude = addressNew.Longitude AndAlso addressResp.RouteDestinationId IsNot Nothing Then
                            destinationId = CInt(addressResp.RouteDestinationId)
                        End If
                    Next
                    arrDestinationIds.Add(destinationId)
                Next
                destinationIds = arrDestinationIds.ToArray()
            End If
            Return destinationIds

        End Function

        <DataContract> _
        Private NotInheritable Class RemoveRouteDestinationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
            Public Property RouteDestinationId() As Integer
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As Integer)
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As Integer
        End Class

        <DataContract> _
        Private NotInheritable Class RemoveRouteDestinationResponse
            <DataMember(Name:="deleted")> _
            Public Property Deleted() As [Boolean]
                Get
                    Return m_Deleted
                End Get
                Set(value As [Boolean])
                    m_Deleted = value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="route_destination_id")> _
            Public Property RouteDestinationId() As Integer
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As Integer)
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As Integer
        End Class

        Public Function RemoveRouteDestination(routeId As String, destinationId As Integer, ByRef errorString As String) As Boolean
            Dim request As New RemoveRouteDestinationRequest() With { _
                .RouteId = routeId, _
                .RouteDestinationId = destinationId _
            }
            Dim response As RemoveRouteDestinationResponse = GetJsonObjectFromAPI(Of RemoveRouteDestinationResponse)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Deleted Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract> _
        Private NotInheritable Class RemoveAddressFromOptimizationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)> _
            Public Property OptimizationProblemId() As String
                Get
                    Return m_OptimizationProblemId
                End Get
                Set(value As String)
                    m_OptimizationProblemId = value
                End Set
            End Property
            Private m_OptimizationProblemId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
            Public Property RouteDestinationId() As Integer
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As Integer)
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As Integer
        End Class

        <DataContract> _
        Private NotInheritable Class RemoveAddressFromOptimizationResponse
            <DataMember(Name:="deleted")> _
            Public Property Deleted() As [Boolean]
                Get
                    Return m_Deleted
                End Get
                Set(value As [Boolean])
                    m_Deleted = value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="route_destination_id")> _
            Public Property RouteDestinationId() As Integer
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As Integer)
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As Integer
        End Class

        Public Function RemoveAddressFromOptimization(optiimizationId As String, destinationId As Integer, ByRef errorString As String) As Boolean
            Dim request As New RemoveAddressFromOptimizationRequest() With { _
                .OptimizationProblemId = optiimizationId, _
                .RouteDestinationId = destinationId _
            }
            Dim response As RemoveAddressFromOptimizationResponse = GetJsonObjectFromAPI(Of RemoveAddressFromOptimizationResponse)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Deleted Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract> _
        Private NotInheritable Class MoveDestinationToRouteResponse
            <DataMember(Name:="success")> _
            Public Property Success() As [Boolean]
                Get
                    Return m_Success
                End Get
                Set(value As [Boolean])
                    m_Success = value
                End Set
            End Property
            Private m_Success As [Boolean]

            <DataMember(Name:="error")> _
            Public Property [error]() As String
                Get
                    Return m_error
                End Get
                Set(value As String)
                    m_error = value
                End Set
            End Property
            Private m_error As String
        End Class

        Public Function MoveDestinationToRoute(toRouteId As String, routeDestinationId As Integer, afterDestinationId As Integer, ByRef errorString As String) As Boolean
            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            keyValues.Add(New KeyValuePair(Of String, String)("to_route_id", toRouteId))
            keyValues.Add(New KeyValuePair(Of String, String)("route_destination_id", Convert.ToString(routeDestinationId)))
            keyValues.Add(New KeyValuePair(Of String, String)("after_destination_id", Convert.ToString(afterDestinationId)))
            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As MoveDestinationToRouteResponse = GetJsonObjectFromAPI(Of MoveDestinationToRouteResponse)(New GenericParameters(), R4MEInfrastructureSettings.MoveRouteDestination, HttpMethodType.Post, httpContent, errorString)
            If response IsNot Nothing Then
                If Not response.Success AndAlso response.[error] IsNot Nothing Then
                    errorString = response.[error]
                End If
                Return response.Success
            End If
            Return False
        End Function

#End Region

#Region "Address Book"

        <DataContract> _
        Private NotInheritable Class GetAddressBookContactsResponse
            <DataMember(Name:="results")> _
            Public Property Results() As AddressBookContact()
                Get
                    Return m_Results
                End Get
                Set(value As AddressBookContact())
                    m_Results = value
                End Set
            End Property
            Private m_Results As AddressBookContact()

            <DataMember(Name:="total")> _
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = value
                End Set
            End Property
            Private m_Total As UInteger
        End Class

        Public Function GetAddressBookContacts(addressBookParameters As AddressBookParameters, ByRef total As UInteger, ByRef errorString As String) As AddressBookContact()
            total = 0
            Dim response = GetJsonObjectFromAPI(Of GetAddressBookContactsResponse)(addressBookParameters, R4MEInfrastructureSettings.AddressBook, HttpMethodType.[Get], errorString)
            Dim result As AddressBookContact() = Nothing
            If response IsNot Nothing Then
                result = response.Results
                total = response.Total
            End If
            Return result
        End Function

        Public Function GetAddressBookLocation(addressBookParameters As AddressBookParameters, ByRef total As UInteger, ByRef errorString As String) As AddressBookContact()
            total = 0

            Dim response = GetJsonObjectFromAPI(Of GetAddressBookContactsResponse)(addressBookParameters, R4MEInfrastructureSettings.AddressBook, HttpMethodType.[Get], errorString)
            Dim result As AddressBookContact() = Nothing
            If response IsNot Nothing Then
                result = response.Results
                total = response.Total
            End If
            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class SearchAddressBookLocationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)> _
            Public Property Query() As String
                Get
                    Return m_Query
                End Get
                Set(value As String)
                    m_Query = value
                End Set
            End Property
            Private m_Query As String

            <HttpQueryMemberAttribute(Name:="fields", EmitDefaultValue:=False)> _
            Public Property Fields() As String
                Get
                    Return m_Fields
                End Get
                Set(value As String)
                    m_Fields = value
                End Set
            End Property
            Private m_Fields As String

            <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
            Public Property Offset() As System.Nullable(Of Integer)
                Get
                    Return m_Offset
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Offset = value
                End Set
            End Property
            Private m_Offset As System.Nullable(Of Integer)

            <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
            Public Property Limit() As System.Nullable(Of Integer)
                Get
                    Return m_Limit
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Limit = value
                End Set
            End Property
            Private m_Limit As System.Nullable(Of Integer)

        End Class

        <DataContract> _
        Private NotInheritable Class SearchAddressBookLocationResponse
            <DataMember(Name:="results")> _
            Public Property Results() As List(Of String())
                Get
                    Return m_Results
                End Get
                Set(value As List(Of String()))
                    m_Results = value
                End Set
            End Property
            Private m_Results As List(Of String())

            <DataMember(Name:="total")> _
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = value
                End Set
            End Property
            Private m_Total As UInteger

            <DataMember(Name:="fields")> _
            Public Property Fields() As String()
                Get
                    Return m_Fields
                End Get
                Set(value As String())
                    m_Fields = value
                End Set
            End Property
            Private m_Fields As String()
        End Class

        Public Function SearchAddressBookLocation(addressBookParameters As AddressBookParameters, ByRef total As UInteger, ByRef errorString As String) As List(Of String())
            total = 0

            Dim request As SearchAddressBookLocationRequest = New SearchAddressBookLocationRequest() With { _
                .Query = addressBookParameters.Query, _
                .Fields = addressBookParameters.Fields, _
                .Offset = addressBookParameters.Offset, _
                .Limit = addressBookParameters.Limit _
            }

            Dim response = GetJsonObjectFromAPI(Of SearchAddressBookLocationResponse)(request, R4MEInfrastructureSettings.AddressBook, HttpMethodType.[Get], errorString)
            Dim result As List(Of String()) = Nothing
            If response IsNot Nothing Then
                result = response.Results
                total = response.Total
            End If
            Return result
        End Function

        Public Function AddAddressBookContact(contact As AddressBookContact, ByRef errorString As String) As AddressBookContact
            contact.PrepareForSerialization()
            Dim result As AddressBookContact = GetJsonObjectFromAPI(Of AddressBookContact)(contact, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Post, errorString)
            Return result
        End Function


        Public Function UpdateAddressBookContact(contact As AddressBookContact, ByRef errorString As String) As AddressBookContact
            contact.PrepareForSerialization()
            Dim result As AddressBookContact = GetJsonObjectFromAPI(Of AddressBookContact)(contact, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Put, errorString)
            Return result
        End Function


        <DataContract> _
        Private NotInheritable Class RemoveAddressBookContactsRequest
            Inherits GenericParameters
            <DataMember(Name:="address_ids", EmitDefaultValue:=False)> _
            Public Property AddressIds() As String()
                Get
                    Return m_AddressIds
                End Get
                Set(value As String())
                    m_AddressIds = value
                End Set
            End Property
            Private m_AddressIds As String()
        End Class

        <DataContract> _
        Private NotInheritable Class RemoveAddressBookContactsResponse
            <DataMember(Name:="status")> _
            Public Property Status() As Boolean
                Get
                    Return m_Status
                End Get
                Set(value As Boolean)
                    m_Status = value
                End Set
            End Property
            Private m_Status As Boolean
        End Class

        Public Function RemoveAddressBookContacts(addressIds As String(), ByRef errorString As String) As Boolean
            Dim request As New RemoveAddressBookContactsRequest() With { _
                .AddressIds = addressIds _
            }
            Dim response As RemoveAddressBookContactsResponse = GetJsonObjectFromAPI(Of RemoveAddressBookContactsResponse)(request, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region "Avoidance Zones"

        ''' <summary>
        ''' Create avoidance zone
        ''' </summary>
        ''' <param name="avoidanceZoneParameters"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Avoidance zone Object </returns>
        Public Function AddAvoidanceZone(avoidanceZoneParameters As AvoidanceZoneParameters, ByRef errorString As String) As AvoidanceZone
            Dim avoidanceZone As AvoidanceZone = GetJsonObjectFromAPI(Of AvoidanceZone)(avoidanceZoneParameters, R4MEInfrastructureSettings.Avoidance, HttpMethodType.Post, errorString)
            Return avoidanceZone
        End Function

        ''' <summary>
        ''' Get avoidance zones
        ''' </summary>
        ''' <param name="avoidanceZoneQuery"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Avoidance zone Object list </returns>
        Public Function GetAvoidanceZones(avoidanceZoneQuery As AvoidanceZoneQuery, ByRef errorString As String) As AvoidanceZone()
            Dim avoidanceZones As AvoidanceZone() = GetJsonObjectFromAPI(Of AvoidanceZone())(avoidanceZoneQuery, R4MEInfrastructureSettings.Avoidance, HttpMethodType.[Get], errorString)
            Return avoidanceZones
        End Function

        ''' <summary>
        ''' Get avoidance zone by parameters (territory id, device id)
        ''' </summary>
        ''' <param name="avoidanceZoneQuery"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Avoidance zone Object </returns>
        Public Function GetAvoidanceZone(avoidanceZoneQuery As AvoidanceZoneQuery, ByRef errorString As String) As AvoidanceZone
            Dim avoidanceZone As AvoidanceZone = GetJsonObjectFromAPI(Of AvoidanceZone)(avoidanceZoneQuery, R4MEInfrastructureSettings.Avoidance, HttpMethodType.[Get], errorString)
            Return avoidanceZone
        End Function

        ''' <summary>
        ''' Update avoidance zone (by territory id, device id)
        ''' </summary>
        ''' <param name="avoidanceZoneParameters"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Avoidance zone Object </returns>
        Public Function UpdateAvoidanceZone(avoidanceZoneParameters As AvoidanceZoneParameters, ByRef errorString As String) As AvoidanceZone
            Dim avoidanceZone As AvoidanceZone = GetJsonObjectFromAPI(Of AvoidanceZone)(avoidanceZoneParameters, R4MEInfrastructureSettings.Avoidance, HttpMethodType.Put, errorString)
            Return avoidanceZone
        End Function

        <DataContract> _
        Private NotInheritable Class DeleteAvoidanceZoneResponse
            <DataMember(Name:="status")> _
            Public Property status As [Boolean]

        End Class
        ''' <summary>
        ''' Delete avoidance zone (by territory id, device id)
        ''' </summary>
        ''' <param name="avoidanceZoneQuery"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Result status true/false </returns>
        Public Function DeleteAvoidanceZone(avoidanceZoneQuery As AvoidanceZoneQuery, ByRef errorString As String) As Boolean
            Dim Result = GetJsonObjectFromAPI(Of DeleteAvoidanceZoneResponse)(avoidanceZoneQuery, R4MEInfrastructureSettings.Avoidance, HttpMethodType.Delete, errorString)
            Return Result.status
        End Function

#End Region

#Region "Territories"

        ''' <summary>
        ''' Create territory
        ''' </summary>
        ''' <param name="avoidanceZoneParameters"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Territory Object </returns>
        Public Function CreateTerritory(avoidanceZoneParameters As AvoidanceZoneParameters, ByRef errorString As String) As TerritoryZone
            Dim avoidanceZone As TerritoryZone = GetJsonObjectFromAPI(Of TerritoryZone)(avoidanceZoneParameters, R4MEInfrastructureSettings.Territory, HttpMethodType.Post, errorString)
            Return avoidanceZone
        End Function

        ''' <summary>
        ''' Get territory by parameters (territory id, device id, addresses)
        ''' </summary>
        ''' <param name="territoryQuery"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Territory zone Object </returns>
        Public Function GetTerritory(territoryQuery As TerritoryQuery, ByRef errorString As String) As TerritoryZone
            Dim territory As TerritoryZone = GetJsonObjectFromAPI(Of TerritoryZone)(territoryQuery, R4MEInfrastructureSettings.Territory, HttpMethodType.[Get], errorString)
            Return territory
        End Function

        ''' <summary>
        ''' Get territories by parameters
        ''' </summary>
        ''' <param name="avoidanceZoneQuery"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Territory zone Objects </returns>
        Public Function GetTerritories(avoidanceZoneQuery As AvoidanceZoneQuery, ByRef errorString As String) As AvoidanceZone()
            Dim territories As AvoidanceZone() = GetJsonObjectFromAPI(Of AvoidanceZone())(avoidanceZoneQuery, R4MEInfrastructureSettings.Territory, HttpMethodType.[Get], errorString)
            Return territories
        End Function

        <DataContract> _
        Private NotInheritable Class RemoveTerritoryResponse
            <DataMember(Name:="status")> _
            Public Property status As [Boolean]

        End Class
        ''' <summary>
        ''' Remove Territory (by territory id, device id)
        ''' </summary>
        ''' <param name="territoryQuery"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Result status true/false </returns>
        Public Function RemoveTerritory(territoryQuery As TerritoryQuery, ByRef errorString As String) As Boolean
            Dim result = GetJsonObjectFromAPI(Of RemoveTerritoryResponse)(territoryQuery, R4MEInfrastructureSettings.Territory, HttpMethodType.Delete, errorString)
            Return result.status
        End Function

        ''' <summary>
        ''' Update Territory (by territory id, device id)
        ''' </summary>
        ''' <param name="tereritoryParameters"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Territory Object </returns>
        Public Function UpdateTerritory(tereritoryParameters As AvoidanceZoneParameters, ByRef errorString As String) As AvoidanceZone
            Dim territory As AvoidanceZone = GetJsonObjectFromAPI(Of AvoidanceZone)(tereritoryParameters, R4MEInfrastructureSettings.Territory, HttpMethodType.Put, errorString)
            Return territory
        End Function

#End Region

#Region "Orders"

        Public Function GetOrderByID(orderQuery As OrderParameters, ByRef errorString As String) As Order()
            Dim ids As String() = orderQuery.order_id.Split(","c)
            If ids.Length = 1 Then
                orderQuery.order_id = orderQuery.order_id + "," + orderQuery.order_id
            End If
            Dim response As GetOrdersResponse = GetJsonObjectFromAPI(Of GetOrdersResponse)(orderQuery, R4MEInfrastructureSettings.Order, HttpMethodType.[Get], errorString)

            Return response.Results
        End Function

        Public Function SearchOrders(orderQuery As OrderParameters, ByRef errorString As String) As Order()
            Dim response As GetOrdersResponse = GetJsonObjectFromAPI(Of GetOrdersResponse)(orderQuery, R4MEInfrastructureSettings.Order, HttpMethodType.[Get], errorString)

            Dim result As Order() = Nothing
            If response IsNot Nothing Then
                result = response.Results
            End If
            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class SearchOrdersByCustomFieldsResponse
            <DataMember(Name:="results")> _
            Public Property Results() As List(Of Integer())
                Get
                    Return m_Results
                End Get
                Set(value As List(Of Integer()))
                    m_Results = value
                End Set
            End Property
            Private m_Results As List(Of Integer())

            <DataMember(Name:="total")> _
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = value
                End Set
            End Property
            Private m_Total As UInteger

            <DataMember(Name:="fields")> _
            Public Property Fields() As String()
                Get
                    Return m_Fields
                End Get
                Set(value As String())
                    m_Fields = value
                End Set
            End Property
            Private m_Fields As String()

        End Class

        Public Function SearchOrdersByCustomFields(orderQuery As OrderParameters, ByRef errorString As String) As List(Of Integer())
            Dim response As SearchOrdersByCustomFieldsResponse = GetJsonObjectFromAPI(Of SearchOrdersByCustomFieldsResponse)(orderQuery, R4MEInfrastructureSettings.Order, HttpMethodType.[Get], errorString)

            Dim result As List(Of Integer()) = New List(Of Integer())
            If response IsNot Nothing Then
                result = response.Results
            End If
            Return result
        End Function

        <DataContract> _
        Private NotInheritable Class GetOrdersResponse
            <DataMember(Name:="results")> _
            Public Property Results() As Order()
                Get
                    Return m_Results
                End Get
                Set(value As Order())
                    m_Results = value
                End Set
            End Property
            Private m_Results As Order()

            <DataMember(Name:="total")> _
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = value
                End Set
            End Property
            Private m_Total As UInteger
        End Class

        ''' <summary>
        ''' Get Orders
        ''' </summary>
        ''' <param name="ordersQuery"> Parameters for request </param>
        ''' <param name="total"> out: Total number of orders </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Order object list </returns>
        Public Function GetOrders(ordersQuery As OrderParameters, ByRef total As UInteger, ByRef errorString As String) As Order()
            total = 0
            Dim response As GetOrdersResponse = GetJsonObjectFromAPI(Of GetOrdersResponse)(ordersQuery, R4MEInfrastructureSettings.Order, HttpMethodType.[Get], errorString)
            Dim result As Order() = Nothing
            If response IsNot Nothing Then
                result = response.Results
                total = response.Total
            End If
            Return result
        End Function

        ''' <summary>
        ''' Create Order
        ''' </summary>
        ''' <param name="order"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Order object </returns>
        Public Function AddOrder(order As Order, ByRef errorString As String) As Order
            order.PrepareForSerialization()
            Dim resultOrder As Order = GetJsonObjectFromAPI(Of Order)(order, R4MEInfrastructureSettings.Order, HttpMethodType.Post, errorString)
            Return resultOrder
        End Function

        ''' <summary>
        ''' Update Order
        ''' </summary>
        ''' <param name="order"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Order Object </returns>
        Public Function UpdateOrder(order As Order, ByRef errorString As String) As Order
            order.PrepareForSerialization()
            Dim resultOrder As Order = GetJsonObjectFromAPI(Of Order)(order, R4MEInfrastructureSettings.Order, HttpMethodType.Put, errorString)
            Return resultOrder
        End Function

        <DataContract> _
        Private NotInheritable Class RemoveOrdersRequest
            Inherits GenericParameters
            <DataMember(Name:="order_ids", EmitDefaultValue:=False)> _
            Public Property OrderIds() As String()
                Get
                    Return m_OrderIds
                End Get
                Set(value As String())
                    m_OrderIds = value
                End Set
            End Property
            Private m_OrderIds As String()
        End Class

        <DataContract> _
        Private NotInheritable Class RemoveOrdersResponse
            <DataMember(Name:="status")> _
            Public Property Status() As Boolean
                Get
                    Return m_Status
                End Get
                Set(value As Boolean)
                    m_Status = value
                End Set
            End Property
            Private m_Status As Boolean
        End Class

        ''' <summary>
        ''' Remove Orders
        ''' </summary>
        ''' <param name="orderIds"> Order IDs </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Result status true/false </returns>
        Public Function RemoveOrders(orderIds As String(), ByRef errorString As String) As Boolean
            Dim request As New RemoveOrdersRequest() With { _
                    .OrderIds = orderIds _
            }
            Dim response As RemoveOrdersResponse = GetJsonObjectFromAPI(Of RemoveOrdersResponse)(request, R4MEInfrastructureSettings.Order, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract> _
        Private NotInheritable Class AddOrdersToRouteRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)> _
            Public Property Redirect() As System.Nullable(Of Integer)
                Get
                    Return m_Redirect
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Redirect = value
                End Set
            End Property
            Private m_Redirect As System.Nullable(Of Integer)

            <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()

            <DataMember(Name:="parameters", EmitDefaultValue:=False)> _
            Public Property Parameters() As RouteParameters
                Get
                    Return m_Parameters
                End Get
                Set(value As RouteParameters)
                    m_Parameters = value
                End Set
            End Property
            Private m_Parameters As RouteParameters

        End Class

        Public Function AddOrdersToRoute(rQueryParams As RouteParametersQuery, addresses As Address(), rParams As RouteParameters, ByRef errorString As String) As RouteResponse
            Dim request As New AddOrdersToRouteRequest With { _
                .RouteId = rQueryParams.RouteId, _
                .Redirect = rQueryParams.Redirect, _
                .Addresses = addresses, _
                .Parameters = rParams _
            }

            Dim response As RouteResponse = GetJsonObjectFromAPI(Of RouteResponse)(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, False, errorString)

            Return response
        End Function

        <DataContract> _
        Private NotInheritable Class AddOrdersToOptimizationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)> _
            Public Property OptimizationProblemId() As String
                Get
                    Return m_OptimizationProblemId
                End Get
                Set(value As String)
                    m_OptimizationProblemId = value
                End Set
            End Property
            Private m_OptimizationProblemId As String

            <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)> _
            Public Property Redirect() As System.Nullable(Of Integer)
                Get
                    Return m_Redirect
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Redirect = value
                End Set
            End Property
            Private m_Redirect As System.Nullable(Of Integer)

            <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()

            <DataMember(Name:="parameters", EmitDefaultValue:=False)> _
            Public Property Parameters() As RouteParameters
                Get
                    Return m_Parameters
                End Get
                Set(value As RouteParameters)
                    m_Parameters = value
                End Set
            End Property
            Private m_Parameters As RouteParameters

        End Class

        Public Function AddOrdersToOptimization(rQueryParams As OptimizationParameters, addresses As Address(), rParams As RouteParameters, ByRef errorString As String) As DataObject
            Dim request As New AddOrdersToOptimizationRequest With { _
                .OptimizationProblemId = rQueryParams.OptimizationProblemID, _
                .Redirect = rQueryParams.Redirect, _
                .Addresses = addresses, _
                .Parameters = rParams _
            }

            Dim response As DataObject = GetJsonObjectFromAPI(Of DataObject)(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, False, errorString)

            Return response
        End Function
#End Region

#Region "Geocoding"

        <DataContract> _
        Private NotInheritable Class GeocodingRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="addresses", EmitDefaultValue:=False)> _
            Public Property Addresses() As String
                Get
                    Return m_Addresses
                End Get
                Set(value As String)
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As String

            <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)> _
            Public Property Format() As String
                Get
                    Return m_Format
                End Get
                Set(value As String)
                    m_Format = value
                End Set
            End Property
            Private m_Format As String

        End Class

        ''' <summary>
        ''' </summary>
        <DataContract> _
        Private NotInheritable Class RapidStreetResponse
            <DataMember(Name:="zipcode")> _
            Public Property Zipcode() As String
                Get
                    Return m_Zipcode
                End Get
                Set(value As String)
                    m_Zipcode = value
                End Set
            End Property
            Private m_Zipcode As String

            <DataMember(Name:="street_name")> _
            Public Property StreetName() As String
                Get
                    Return m_StreetName
                End Get
                Set(value As String)
                    m_StreetName = value
                End Set
            End Property
            Private m_StreetName As String
        End Class

        Public Function Geocoding(geoParams As GeocodingParameters, ByRef errorString As String) As String
            Dim request As New GeocodingRequest With { _
                .Addresses = geoParams.Addresses, _
                .Format = geoParams.Format _
            }

            Dim response As String = GetXmlObjectFromAPI(Of String)(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post, DirectCast(Nothing, HttpContent), False, errorString)

            Return response.ToString()
        End Function

        Public Function RapidStreetData(geoParams As GeocodingParameters, ByRef errorString As String) As ArrayList
            Dim request As New GeocodingRequest With { _
                .Addresses = geoParams.Addresses, _
                .Format = geoParams.Format _
            }
            Dim url As String = R4MEInfrastructureSettings.RapidStreetData

            Dim result As New ArrayList

            If geoParams.Pk > 0 Then
                url = url & "/" & geoParams.Pk & "/"
                Dim response As RapidStreetResponse = GetJsonObjectFromAPI(Of RapidStreetResponse)(request, url, HttpMethodType.Get, DirectCast(Nothing, HttpContent), False, errorString)
                Dim dresult As Dictionary(Of String, String) = New Dictionary(Of String, String)()
                If Not response Is Nothing Then
                    dresult("zipcode") = response.Zipcode
                    dresult("street_name") = response.StreetName
                    result.Add(dresult)
                End If
            Else
                If geoParams.Offset > 0 Or geoParams.Limit > 0 Then
                    url = url & "/" & geoParams.Offset & "/" & geoParams.Limit & "/"
                End If
                Dim response As RapidStreetResponse() = GetJsonObjectFromAPI(Of RapidStreetResponse())(request, url, HttpMethodType.Get, DirectCast(Nothing, HttpContent), False, errorString)
                If Not response Is Nothing Then
                    For Each resp1 In response
                        Dim dresult As Dictionary(Of String, String) = New Dictionary(Of String, String)()
                        dresult("zipcode") = resp1.Zipcode
                        dresult("street_name") = resp1.StreetName
                        result.Add(dresult)
                    Next
                End If
            End If

            Return result
        End Function

        Public Function RapidStreetZipcode(geoParams As GeocodingParameters, ByRef errorString As String) As ArrayList
            Dim request As New GeocodingRequest With { _
                .Addresses = geoParams.Addresses, _
                .Format = geoParams.Format _
            }
            Dim url As String = R4MEInfrastructureSettings.RapidStreetZipcode

            Dim result As New ArrayList

            If geoParams.Zipcode IsNot Nothing Then
                url = url & "/" & geoParams.Zipcode & "/"
            Else
                Return result
            End If
            If geoParams.Offset > 0 Or geoParams.Limit > 0 Then
                url = url & geoParams.Offset & "/" & geoParams.Limit & "/"
            End If

            Dim response As RapidStreetResponse() = GetJsonObjectFromAPI(Of RapidStreetResponse())(request, url, HttpMethodType.Get, DirectCast(Nothing, HttpContent), False, errorString)
            If Not response Is Nothing Then
                For Each resp1 In response
                    Dim dresult As Dictionary(Of String, String) = New Dictionary(Of String, String)()
                    dresult("zipcode") = resp1.Zipcode
                    dresult("street_name") = resp1.StreetName
                    result.Add(dresult)
                Next
            End If

            Return result
        End Function

        Public Function RapidStreetService(geoParams As GeocodingParameters, ByRef errorString As String) As ArrayList
            Dim request As New GeocodingRequest With { _
                .Addresses = geoParams.Addresses, _
                .Format = geoParams.Format _
            }
            Dim url As String = R4MEInfrastructureSettings.RapidStreetService

            Dim result As New ArrayList

            If geoParams.Zipcode IsNot Nothing Then
                url = url & "/" & geoParams.Zipcode & "/"
            Else
                Return result
            End If
            If geoParams.Housenumber IsNot Nothing Then
                url = url & geoParams.Housenumber & "/"
            Else
                Return result
            End If
            If geoParams.Offset > 0 Or geoParams.Limit > 0 Then
                url = url & geoParams.Offset & "/" & geoParams.Limit & "/"
            End If

            Dim response As RapidStreetResponse() = GetJsonObjectFromAPI(Of RapidStreetResponse())(request, url, HttpMethodType.Get, DirectCast(Nothing, HttpContent), False, errorString)
            If Not response Is Nothing Then
                For Each resp1 In response
                    Dim dresult As Dictionary(Of String, String) = New Dictionary(Of String, String)()
                    dresult("zipcode") = resp1.Zipcode
                    dresult("street_name") = resp1.StreetName
                    result.Add(dresult)
                Next
            End If

            Return result
        End Function

#End Region

#Region "Vehicles"

        ''' <summary>
        ''' Get Vehicles
        ''' </summary>
        ''' <param name="vehParams"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Vehicle object list </returns>
        Public Function GetVehicles(vehParams As VehicleParameters, ByRef errorString As String) As VehicleResponse()

            Dim response As VehicleResponse() = GetJsonObjectFromAPI(Of VehicleResponse())(vehParams, R4MEInfrastructureSettings.ViewVehicles, HttpMethodType.[Get], errorString)

            Return response

        End Function

#End Region

#Region "Generic Methods"

        Public Function GetStringResponseFromAPI(optimizationParameters As GenericParameters, url As String, httpMethod As HttpMethodType, ByRef errorMessage As String) As String
            Dim result As String = GetJsonObjectFromAPI(Of String)(optimizationParameters, url, httpMethod, True, errorMessage)

            Return result
        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(optimizationParameters As GenericParameters, url As String, httpMethod As HttpMethodType, ByRef errorMessage As String) As T
            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, False, errorMessage)

            Return result
        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(optimizationParameters As GenericParameters, url As String, httpMethod As HttpMethodType, httpContent As HttpContent, ByRef errorMessage As String) As T
            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, httpContent, False, errorMessage)

            Return result
        End Function

        Private Function GetJsonObjectFromAPI(Of T As Class)(optimizationParameters As GenericParameters, url As String, httpMethod As HttpMethodType, isString As Boolean, ByRef errorMessage As String) As T
            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, DirectCast(Nothing, HttpContent), isString, errorMessage)

            Return result
        End Function

        Private Function GetJsonObjectFromAPI(Of T As Class)(optimizationParameters As GenericParameters, url As String, httpMethod__1 As HttpMethodType, httpContent As HttpContent, isString As Boolean, ByRef errorMessage As String) As T
            Dim result As T = Nothing
            errorMessage = String.Empty

            Try
                Using httpClient As HttpClient = CreateHttpClient(url)
                    ' Get the parameters
                    Dim parametersURI As String = optimizationParameters.Serialize(m_ApiKey)

                    Select Case httpMethod__1
                        Case HttpMethodType.[Get]
                            If True Then
                                Dim response = httpClient.GetStreamAsync(parametersURI)
                                response.Wait()

                                If response.IsCompleted Then
                                    'var test = m_isTestMode ? response.Result.ReadString() : null;
                                    result = If(isString, response.Result.ReadString(), response.Result.ReadObject(Of T)())
                                End If

                                Exit Select
                            End If
                        Case HttpMethodType.FileUpload
                            If True Then
                                Dim myWebClient As New System.Net.WebClient()
                                Dim uriString As String
                                Dim filename As String
                                uriString = "https://www.route4me.com/actions/addRouteNotes.php?api_key=11111111111111111111111111111111&route_id=4728372005DE97EF9E4205852D690E34&address_id=182302891&dev_lat=29.452769&dev_lng=-95.845939&device_type=web&strUpdateType=ANY_FILE"
                                filename = "notes.csv"
                                Dim uri As System.Uri = New Uri(uriString)


                                myWebClient.UploadFileAsync(uri, "POST", filename)

                                'Console.WriteLine(ControlChars.Cr & "Response Received.The contents of the file uploaded are: " & _
                                'ControlChars.Cr & "{0}", System.Text.Encoding.ASCII.GetString(responseArray))

                            End If
                        Case HttpMethodType.Post, HttpMethodType.Put, HttpMethodType.Delete
                            If True Then
                                Dim isPut As Boolean = httpMethod__1 = HttpMethodType.Put
                                Dim isDelete As Boolean = httpMethod__1 = HttpMethodType.Delete
                                Dim content As HttpContent = Nothing
                                If httpContent IsNot Nothing Then
                                    content = httpContent
                                Else
                                    Dim jsonString As String = R4MeUtils.SerializeObjectToJson(optimizationParameters)
                                    content = New StringContent(jsonString)
                                End If

                                Dim response As Task(Of HttpResponseMessage) = Nothing
                                If isPut Then
                                    response = httpClient.PutAsync(parametersURI, content)
                                ElseIf isDelete Then
                                    Dim request As New HttpRequestMessage() With { _
                                        .Content = content, _
                                        .Method = HttpMethod.Delete, _
                                        .RequestUri = New Uri(parametersURI, UriKind.Relative) _
                                    }
                                    response = httpClient.SendAsync(request)
                                Else
                                    'Dim request As New HttpRequestMessage() With { _
                                    '    .Content = content, _
                                    '    .Method = HttpMethod.Post, _
                                    '    .RequestUri = New Uri(parametersURI, UriKind.Relative) _
                                    '}
                                    'request.Headers.Add("User-agent", "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko")
                                    'response = httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead)

                                    response = httpClient.PostAsync(parametersURI, content)

                                End If

                                ' Wait for response
                                response.Wait()

                                ' Check if successful
                                If response.IsCompleted AndAlso response.Result.IsSuccessStatusCode AndAlso TypeOf response.Result.Content Is StreamContent Then
                                    Dim streamTask = DirectCast(response.Result.Content, StreamContent).ReadAsStreamAsync()
                                    streamTask.Wait()

                                    If streamTask.IsCompleted Then
                                        'var test = m_isTestMode ? streamTask.Result.ReadString() : null;
                                        'Dim test As VariantType = streamTask.Result.ReadString()
                                        result = If(isString, TryCast(streamTask.Result.ReadString(), T), streamTask.Result.ReadObject(Of T)())
                                    End If
                                Else
                                    Dim streamTask = DirectCast(response.Result.Content, StreamContent).ReadAsStreamAsync()
                                    streamTask.Wait()
                                    Dim errorResponse As ErrorResponse = Nothing
                                    Try
                                        errorResponse = streamTask.Result.ReadObject(Of ErrorResponse)()
                                    Catch
                                        ' (Exception e)
                                        errorResponse = Nothing
                                    End Try
                                    If errorResponse IsNot Nothing AndAlso errorResponse.Errors IsNot Nothing AndAlso errorResponse.Errors.Count > 0 Then
                                        For Each [error] As [String] In errorResponse.Errors
                                            If errorMessage.Length > 0 Then
                                                errorMessage += "; "
                                            End If
                                            errorMessage += [error]
                                        Next
                                    Else
                                        Dim responseStream = response.Result.Content.ReadAsStringAsync()
                                        responseStream.Wait()
                                        Dim responseString As [String] = responseStream.Result
                                        If responseString IsNot Nothing Then
                                            errorMessage = "Response: " + responseString
                                        End If
                                    End If
                                End If

                                Exit Select
                            End If
                    End Select
                End Using
            Catch e As Exception
                errorMessage = If(TypeOf e Is AggregateException, e.InnerException.Message, e.Message)
                result = Nothing
            End Try

            Return result
        End Function

        Private Function GetXmlObjectFromAPI(Of T As Class)(optimizationParameters As GenericParameters, url As String, httpMethod__1 As HttpMethodType, httpContent As HttpContent, isString As Boolean, ByRef errorMessage As String) As String
            Dim result As String = String.Empty
            errorMessage = String.Empty

            Try
                Using httpClient As HttpClient = CreateHttpClient(url)
                    ' Get the parameters
                    Dim parametersURI As String = optimizationParameters.Serialize(m_ApiKey)

                    Select Case httpMethod__1
                        Case HttpMethodType.[Get]
                            If True Then
                                Dim response = httpClient.GetStreamAsync(parametersURI)
                                response.Wait()

                                If response.IsCompleted Then
                                    'var test = m_isTestMode ? response.Result.ReadString() : null;
                                    result = If(isString, response.Result.ReadString(), response.Result.ReadObject(Of T)())
                                End If

                                Exit Select
                            End If
                        Case HttpMethodType.Post, HttpMethodType.Put, HttpMethodType.Delete
                            If True Then
                                Dim isPut As Boolean = httpMethod__1 = HttpMethodType.Put
                                Dim isDelete As Boolean = httpMethod__1 = HttpMethodType.Delete
                                Dim content As HttpContent = Nothing
                                If httpContent IsNot Nothing Then
                                    content = httpContent
                                Else
                                    Dim jsonString As String = R4MeUtils.SerializeObjectToJson(optimizationParameters)
                                    content = New StringContent(jsonString)
                                End If

                                Dim response As Task(Of HttpResponseMessage) = Nothing
                                If isPut Then
                                    response = httpClient.PutAsync(parametersURI, content)
                                ElseIf isDelete Then
                                    Dim request As New HttpRequestMessage() With { _
                                        .Content = content, _
                                        .Method = HttpMethod.Delete, _
                                        .RequestUri = New Uri(parametersURI, UriKind.Relative) _
                                    }
                                    response = httpClient.SendAsync(request)
                                Else
                                    response = httpClient.PostAsync(parametersURI, content)
                                End If

                                ' Wait for response
                                response.Wait()

                                ' Check if successful
                                If response.IsCompleted AndAlso response.Result.IsSuccessStatusCode AndAlso TypeOf response.Result.Content Is StreamContent Then
                                    Dim streamTask = DirectCast(response.Result.Content, StreamContent).ReadAsStreamAsync()
                                    streamTask.Wait()

                                    If streamTask.IsCompleted Then
                                        'var test = m_isTestMode ? streamTask.Result.ReadString() : null;
                                        'var test = streamTask.Result.ReadString();
                                        result = streamTask.Result.ReadString()
                                        'result = If(isString, TryCast(streamTask.Result.ReadString(), XmlDocument), streamTask.Result.ReadObject(Of XmlDocument)())
                                    End If
                                Else
                                    Dim streamTask = DirectCast(response.Result.Content, StreamContent).ReadAsStreamAsync()
                                    streamTask.Wait()
                                    Dim errorResponse As ErrorResponse = Nothing
                                    Try
                                        errorResponse = streamTask.Result.ReadObject(Of ErrorResponse)()
                                    Catch
                                        ' (Exception e)
                                        errorResponse = Nothing
                                    End Try
                                    If errorResponse IsNot Nothing AndAlso errorResponse.Errors IsNot Nothing AndAlso errorResponse.Errors.Count > 0 Then
                                        For Each [error] As [String] In errorResponse.Errors
                                            If errorMessage.Length > 0 Then
                                                errorMessage += "; "
                                            End If
                                            errorMessage += [error]
                                        Next
                                    Else
                                        Dim responseStream = response.Result.Content.ReadAsStringAsync()
                                        responseStream.Wait()
                                        Dim responseString As [String] = responseStream.Result
                                        If responseString IsNot Nothing Then
                                            errorMessage = "Response: " + responseString
                                        End If
                                    End If
                                End If

                                Exit Select
                            End If
                    End Select
                End Using
            Catch e As Exception
                errorMessage = If(TypeOf e Is AggregateException, e.InnerException.Message, e.Message)
                result = Nothing
            End Try

            Return result
        End Function

        Private Function CreateHttpClient(url As String) As HttpClient
            Dim result As New HttpClient() With { _
                .BaseAddress = New Uri(url) _
            }

            result.Timeout = m_DefaultTimeOut
            result.DefaultRequestHeaders.Accept.Clear()
            result.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Return result
        End Function

#End Region
#End Region
#End Region
    End Class
End Namespace
