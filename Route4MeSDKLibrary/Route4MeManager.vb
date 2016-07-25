﻿Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
'Imports Route4MeSDKLibrary.DataTypes
Imports System.Collections.Generic
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Runtime.Serialization
Imports System.Text
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

#End Region

#Region "Routes"

        Public Function GetRoute(routeParameters As RouteParametersQuery, ByRef errorString As String) As DataObjectRoute
            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute)(routeParameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.[Get], errorString)

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
        Private NotInheritable Class DuplicateRouteResponse
            <DataMember(Name:="optimization_problem_id")> _
            Public Property OptimizationProblemId() As String
                Get
                    Return m_OptimizationProblemId
                End Get
                Set(value As String)
                    m_OptimizationProblemId = Value
                End Set
            End Property
            Private m_OptimizationProblemId As String

            <DataMember(Name:="success")> _
            Public Property Success() As [Boolean]
                Get
                    Return m_Success
                End Get
                Set(value As [Boolean])
                    m_Success = Value
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
                    m_Deleted = Value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="errors")> _
            Public Property Errors() As List(Of [String])
                Get
                    Return m_Errors
                End Get
                Set(value As List(Of [String]))
                    m_Errors = Value
                End Set
            End Property
            Private m_Errors As List(Of [String])

            <DataMember(Name:="route_id")> _
            Public Property routeId() As String
                Get
                    Return m_routeId
                End Get
                Set(value As String)
                    m_routeId = Value
                End Set
            End Property
            Private m_routeId As String

            <DataMember(Name:="route_ids")> _
            Public Property routeIds() As String()
                Get
                    Return m_routeIds
                End Get
                Set(value As String())
                    m_routeIds = Value
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

#End Region

#Region "Users"

        Public Function GetUsers(parameters As GenericParameters, ByRef errorString As String) As User()
            Dim result = GetJsonObjectFromAPI(Of User())(parameters, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.[Get], errorString)

            Return result
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
                    m_Status = Value
                End Set
            End Property
            Private m_Status As Boolean

            <DataMember(Name:="note")> _
            Public Property Note() As AddressNote
                Get
                    Return m_Note
                End Get
                Set(value As AddressNote)
                    m_Note = Value
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

#End Region

#Region "Activities"

        <DataContract> _
        Private NotInheritable Class GetActivitiesResponse
            <DataMember(Name:="results")> _
            Public Property Results() As Activity()
                Get
                    Return m_Results
                End Get
                Set(value As Activity())
                    m_Results = Value
                End Set
            End Property
            Private m_Results As Activity()

            <DataMember(Name:="total")> _
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = Value
                End Set
            End Property
            Private m_Total As UInteger
        End Class

        Public Function GetActivityFeed(activityParameters As ActivityParameters, ByRef errorString As String) As Activity()
            Dim response As GetActivitiesResponse = GetJsonObjectFromAPI(Of GetActivitiesResponse)(activityParameters, R4MEInfrastructureSettings.GetActivitiesHost, HttpMethodType.[Get], errorString)
            Dim result As Activity() = Nothing
            If response IsNot Nothing Then
                result = response.Results
            End If
            Return result
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
                    m_RouteId = Value
                End Set
            End Property
            Private m_RouteId As String

            <DataMember(Name:="addresses", EmitDefaultValue:=False)> _
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = Value
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
        Private NotInheritable Class RemoveRouteDestinationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)> _
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = Value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)> _
            Public Property RouteDestinationId() As Integer
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As Integer)
                    m_RouteDestinationId = Value
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
                    m_Deleted = Value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="route_destination_id")> _
            Public Property RouteDestinationId() As Integer
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As Integer)
                    m_RouteDestinationId = Value
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
                    m_Success = Value
                End Set
            End Property
            Private m_Success As [Boolean]

            <DataMember(Name:="error")> _
            Public Property [error]() As String
                Get
                    Return m_error
                End Get
                Set(value As String)
                    m_error = Value
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
                    m_Results = Value
                End Set
            End Property
            Private m_Results As AddressBookContact()

            <DataMember(Name:="total")> _
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = Value
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
                    m_AddressIds = Value
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
                    m_Status = Value
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

        ''' <summary>
        ''' Delete avoidance zone (by territory id, device id)
        ''' </summary>
        ''' <param name="avoidanceZoneQuery"> Parameters for request </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Result status true/false </returns>
        Public Function DeleteAvoidanceZone(avoidanceZoneQuery As AvoidanceZoneQuery, ByRef errorString As String) As Boolean
            GetJsonObjectFromAPI(Of AvoidanceZone)(avoidanceZoneQuery, R4MEInfrastructureSettings.Avoidance, HttpMethodType.Delete, errorString)
            Return errorString <> ""
        End Function

#End Region


#Region "Orders"

        <DataContract> _
        Private NotInheritable Class GetOrdersResponse
            <DataMember(Name:="results")> _
            Public Property Results() As Order()
                Get
                    Return m_Results
                End Get
                Set(value As Order())
                    m_Results = Value
                End Set
            End Property
            Private m_Results As Order()

            <DataMember(Name:="total")> _
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = Value
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
                    m_OrderIds = Value
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
                    m_Status = Value
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
