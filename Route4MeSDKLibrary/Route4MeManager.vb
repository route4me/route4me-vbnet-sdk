Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System
Imports System.Collections
Imports System.Web.Http
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Reflection

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

        Private parseWithNewtonJson As Boolean
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

        Public Function RunAsyncOptimization(ByVal optimizationParameters As OptimizationParameters, ByRef errorString As String) As DataObject
            Dim result As Task(Of Tuple(Of DataObject, String)) = GetJsonObjectFromAPIAsync(Of DataObject)(optimizationParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Post, False)
            result.Wait()
            errorString = ""
            If result.IsFaulted OrElse result.IsCanceled Then errorString = result.Result.Item2
            Return result.Result.Item1
        End Function

        Public Function GetOptimization(optimizationParameters As OptimizationParameters, ByRef errorString As String) As DataObject
            Dim result = GetJsonObjectFromAPI(Of DataObject)(optimizationParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.[Get], errorString)

            Return result
        End Function

        ''' <summary>
        ''' </summary>
        <DataContract>
        Private NotInheritable Class DataObjectOptimizations
            <DataMember(Name:="optimizations")>
            Public Property Optimizations() As DataObject()
                Get
                    Return m_Optimizations
                End Get
                Set(value As DataObject())
                    m_Optimizations = value
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

        <DataContract>
        Private NotInheritable Class RemoveOptimizationResponse
            <DataMember(Name:="status")>
            Public Property Status() As [Boolean]
                Get
                    Return m_Status
                End Get
                Set(value As [Boolean])
                    m_Status = value
                End Set
            End Property
            Private m_Status As [Boolean]

            <DataMember(Name:="removed")>
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

        <DataContract>
        Private NotInheritable Class RemoveOptimizationRequest
            Inherits GenericParameters
            <DataMember(Name:="optimization_problem_ids", EmitDefaultValue:=False)>
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
            Dim request As New RemoveOptimizationRequest() With {
                .OptimizationProblemIDs = optimizationProblemIDs
            }

            Dim response As RemoveOptimizationResponse = GetJsonObjectFromAPI(Of RemoveOptimizationResponse)(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Delete, False, errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract>
        Private NotInheritable Class RemoveDestinationFromOptimizationResponse
            <DataMember(Name:="deleted")>
            Public Property Deleted As [Boolean]

            <DataMember(Name:="route_destination_id")>
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

        Private Function RemoveDuplicatedAddressesFromRoute(ByVal route As DataObjectRoute) As DataObjectRoute
            Dim lsAddress = New List(Of Address)()

            For Each addr1 In route.Addresses
                If Not lsAddress.Contains(addr1) AndAlso lsAddress.Where(Function(x) x.RouteDestinationId = addr1.RouteDestinationId).FirstOrDefault() Is Nothing Then lsAddress.Add(addr1)
            Next

            route.Addresses = lsAddress.ToArray()
            Return route
        End Function

        'Public Function UpdateRoute(ByVal route As DataObjectRoute, ByRef errorString As String) As DataObjectRoute

        '    Dim routeParameters = New RouteParametersQuery() With {
        '        .RouteId = route.RouteID,
        '        .ApprovedForExecution = route.ApprovedForExecution,
        '        .Parameters = route.Parameters,
        '        .Addresses = route.Addresses
        '    }

        '    routeParameters.PrepareForSerialization()
        '    Dim result = GetJsonObjectFromAPI(Of DataObjectRoute)(routeParameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, errorString)

        '    Return result
        'End Function

        Public Function UpdateRoute(ByVal route As DataObjectRoute, ByVal initialRoute As DataObjectRoute, ByRef errorString As String) As DataObjectRoute
            errorString = ""
            parseWithNewtonJson = True

            If initialRoute Is Nothing Then
                errorString = "An initial route should be specified"
                Return Nothing
            End If

            route = RemoveDuplicatedAddressesFromRoute(route)
            initialRoute = RemoveDuplicatedAddressesFromRoute(initialRoute)

#Region "ApprovedForExecution"
            Dim approvedForExecution As String = ""

            If initialRoute.ApprovedForExecution <> route.ApprovedForExecution Then
                approvedForExecution = String.Concat("{""approved_for_execution"": ", If(route.ApprovedForExecution, "true", "false"), "}")
                Dim genParams = New RouteParametersQuery() With {
            .RouteId = initialRoute.RouteID
        }
                Dim content = New StringContent(approvedForExecution, System.Text.Encoding.UTF8, "application/json")
                initialRoute = GetJsonObjectFromAPI(Of DataObjectRoute)(genParams, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, content, errorString)
                If initialRoute Is Nothing Then Return Nothing
            End If

#End Region

#Region "Resequence if sequence was changed"
            Dim resequenceJson As String = ""

            For Each addr1 In initialRoute.Addresses
                Dim addr = route.Addresses.Where(Function(x) x.RouteDestinationId = addr1.RouteDestinationId).FirstOrDefault()

                If addr1 IsNot Nothing AndAlso (addr.SequenceNo <> addr1.SequenceNo OrElse addr.IsDepot <> addr1.IsDepot) Then
                    resequenceJson += "{""route_destination_id"":" & addr1.RouteDestinationId

                    If addr.SequenceNo <> addr1.SequenceNo Then
                        resequenceJson += "," & """sequence_no"":" & addr.SequenceNo
                    ElseIf addr.IsDepot <> addr1.IsDepot Then
                        resequenceJson += "," & """is_depot"":" & addr.IsDepot.ToString().ToLower()
                    End If

                    resequenceJson += "},"
                End If
            Next

            If resequenceJson.Length > 10 Then
                resequenceJson = resequenceJson.TrimEnd(","c)
                resequenceJson = "{""addresses"": [" & resequenceJson & "]}"
                Dim genParams = New RouteParametersQuery() With {
            .RouteId = initialRoute.RouteID
        }
                Dim content = New StringContent(resequenceJson, System.Text.Encoding.UTF8, "application/json")
                initialRoute = GetJsonObjectFromAPI(Of DataObjectRoute)(genParams, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, content, errorString)
                If initialRoute Is Nothing Then Return Nothing
            End If
#End Region

#Region "Update Route Parameters"
            Dim errorString0 As String = Nothing

            If (If(route?.Parameters, Nothing)) IsNot Nothing Then
                Dim updatableRouteParametersProperties = R4MeUtils.GetPropertiesWithDifferentValues(route.Parameters, initialRoute.Parameters, errorString)

                If updatableRouteParametersProperties IsNot Nothing AndAlso updatableRouteParametersProperties.Count > 0 Then
                    Dim dynamicRouteProperties = New Route4MeDynamicClass()
                    dynamicRouteProperties.CopyPropertiesFromClass(route.Parameters, updatableRouteParametersProperties, errorString0)
                    Dim routeParamsJsonString = R4MeUtils.SerializeObjectToJson(dynamicRouteProperties.DynamicProperties, True)
                    routeParamsJsonString = String.Concat("{""parameters"":", routeParamsJsonString, "}")
                    Dim genParams = New RouteParametersQuery() With {
                .RouteId = initialRoute.RouteID
            }
                    Dim content = New StringContent(routeParamsJsonString, System.Text.Encoding.UTF8, "application/json")
                    initialRoute = GetJsonObjectFromAPI(Of DataObjectRoute)(genParams, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, content, errorString)
                End If
            End If

            If initialRoute Is Nothing Then Return Nothing
#End Region

#Region "Update Route Addresses"
            Dim lsUpdatedAddresses = New List(Of Address)()
            Dim errorString3 As String = Nothing

            If (If(route?.Addresses, Nothing)) IsNot Nothing AndAlso route.Addresses.Length > 0 Then

                For Each address In route.Addresses
                    Dim initialAddress = initialRoute.Addresses.Where(Function(x) x.RouteDestinationId = address.RouteDestinationId).FirstOrDefault()

                    If initialAddress Is Nothing Then
                        initialAddress = initialRoute.Addresses.Where(Function(x) x.AddressString = address.AddressString).FirstOrDefault()
                    End If

                    If initialAddress Is Nothing Then Continue For
                    Dim updatableAddressProperties = R4MeUtils.GetPropertiesWithDifferentValues(address, initialAddress, errorString)
                    If updatableAddressProperties.Contains("IsDepot") Then updatableAddressProperties.Remove("IsDepot")
                    If updatableAddressProperties.Contains("SequenceNo") Then updatableAddressProperties.Remove("SequenceNo")
                    If updatableAddressProperties.Contains("OptimizationProblemId") AndAlso updatableAddressProperties.Count = 1 Then updatableAddressProperties.Remove("OptimizationProblemId")

                    If updatableAddressProperties IsNot Nothing AndAlso updatableAddressProperties.Count > 0 Then
                        Dim dynamicAddressProperties = New Route4MeDynamicClass()
                        dynamicAddressProperties.CopyPropertiesFromClass(address, updatableAddressProperties, errorString0)
                        Dim addressParamsJsonString = R4MeUtils.SerializeObjectToJson(dynamicAddressProperties.DynamicProperties, True)
                        Dim genParams = New RouteParametersQuery() With {
                    .RouteId = initialRoute.RouteID,
                    .RouteDestinationId = address.RouteDestinationId
                }
                        Dim content = New StringContent(addressParamsJsonString, System.Text.Encoding.UTF8, "application/json")
                        Dim updatedAddress = GetJsonObjectFromAPI(Of Address)(genParams, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, content, errorString)
                        updatedAddress.IsDepot = initialAddress.IsDepot
                        updatedAddress.SequenceNo = initialAddress.SequenceNo

                        If (If(address?.Notes?.Length, -1)) > 0 Then
                            Dim addressNotes = New List(Of AddressNote)()

                            For Each note1 As AddressNote In address.Notes

                                If (If(note1?.NoteId, Nothing)) = -1 Then
                                    Dim noteParameters = New NoteParameters() With {
                                .RouteId = initialRoute.RouteID,
                                .AddressId = If(updatedAddress.RouteDestinationId IsNot Nothing, CInt(updatedAddress.RouteDestinationId), note1.RouteDestinationId),
                                .Latitude = note1.Latitude,
                                .Longitude = note1.Longitude
                            }
                                    If note1.ActivityType IsNot Nothing Then noteParameters.ActivityType = note1.ActivityType
                                    If note1.DeviceType IsNot Nothing Then noteParameters.DeviceType = note1.DeviceType
                                    Dim noteContent As String = If(note1.Contents IsNot Nothing, note1.Contents, Nothing)
                                    If noteContent IsNot Nothing Then noteParameters.StrNoteContents = noteContent
                                    Dim customNotes As Dictionary(Of String, String) = Nothing

                                    If (If(note1?.CustomTypes?.Length, -1)) > 0 Then
                                        customNotes = New Dictionary(Of String, String)()

                                        For Each customNote As AddressCustomNote In note1.CustomTypes
                                            customNotes.Add("custom_note_type[" & customNote.NoteCustomTypeID & "]", customNote.NoteCustomValue)
                                        Next
                                    End If

                                    If customNotes IsNot Nothing Then noteParameters.CustomNoteTypes = customNotes
                                    If note1.UploadUrl IsNot Nothing Then noteParameters.StrFileName = note1.UploadUrl
                                    Dim addedNote As AddressNote = AddAddressNote(noteParameters, errorString3)
                                    If errorString3 <> "" Then errorString += vbLf & "Note Adding Error: " & errorString3
                                    If addedNote IsNot Nothing Then addressNotes.Add(addedNote)
                                Else
                                    addressNotes.Add(note1)
                                End If
                            Next

                            address.Notes = addressNotes.ToArray()
                            updatedAddress.Notes = addressNotes.ToArray()
                        End If

                        If updatedAddress IsNot Nothing AndAlso updatedAddress.[GetType]() = GetType(Address) Then
                            Dim addressIndex As Integer = Array.IndexOf(initialRoute.Addresses, initialAddress)
                            If addressIndex > -1 Then initialRoute.Addresses(addressIndex) = updatedAddress
                        End If
                    End If
                Next
            End If
#End Region

            Return initialRoute
        End Function

        <DataContract>
        Private NotInheritable Class UpdateRouteCustomDataRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
            Public Property RouteDestinationId() As System.Nullable(Of Integer)
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As System.Nullable(Of Integer)

            <DataMember(Name:="custom_fields", EmitDefaultValue:=False)>
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
            Dim request As New UpdateRouteCustomDataRequest With {
                .RouteId = routeParameters.RouteId,
                .RouteDestinationId = routeParameters.RouteDestinationId,
                .CustomFields = customData
            }

            Dim result = GetJsonObjectFromAPI(Of Address)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put, errorString)

            Return result
        End Function

        ''' <summary>
        ''' The request parameters for the updating process of a route destination
        ''' </summary>
        <DataContract>
        Private NotInheritable Class UpdateRouteDestinationRequest
            Inherits Address

            ''' <summary>
            ''' A route ID to be updated
            ''' </summary>
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Overloads Property RouteId As String

            ''' <summary>
            ''' A optimization ID to be updated
            ''' </summary>
            <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)>
            Public Overloads Property OptimizationProblemId As String

            ''' <summary>
            ''' A route destination ID to be updated
            ''' </summary>
            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
            Public Overloads Property RouteDestinationId As Integer?

        End Class

        Public Function UpdateRouteDestination(addressParameters As Address, ByRef errorString As String) As Address
            Dim request As New UpdateRouteDestinationRequest() With {
                .RouteId = addressParameters.RouteId,
                .RouteDestinationId = addressParameters.RouteDestinationId
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

        ''' <summary>
        ''' Updates an optimization destination
        ''' </summary>
        ''' <param name="addressParameters">Contains an address object</param>
        ''' <param name="errorString">Returned error string in case of the processs failing</param>
        ''' <returns>The updated address</returns>
        Public Function UpdateOptimizationDestination(ByVal addressParameters As Address, ByRef errorString As String) As Address
            Dim request = New UpdateRouteDestinationRequest With {
                .OptimizationProblemId = addressParameters.OptimizationProblemId
            }

            For Each propInfo In GetType(Address).GetProperties()
                propInfo.SetValue(request, propInfo.GetValue(addressParameters))
            Next

            Dim dataObject = GetJsonObjectFromAPI(Of DataObject)(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, errorString)

            Return If(dataObject?.Addresses?.Where(Function(x) x.RouteDestinationId = addressParameters.RouteDestinationId).FirstOrDefault(), Nothing)
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

            Dim response As StatusResponse = GetJsonObjectFromAPI(Of StatusResponse)(roParames, R4MEInfrastructureSettings.MergeRoutes, HttpMethodType.Post, httpContent, errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ResequenceReoptimizeRoute(roParames As Dictionary(Of String, String), ByRef errorString As String) As Boolean
            Dim request As New RouteParametersQuery With {
               .RouteId = roParames.Item("route_id"),
               .DisableOptimization = Val(roParames.Item("route_id")),
               .Optimize = roParames.Item("optimize")
            }

            Dim response As StatusResponse = GetJsonObjectFromAPI(Of StatusResponse)(request, R4MEInfrastructureSettings.RouteReoptimize, HttpMethodType.[Get], errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract()>
        Private NotInheritable Class ManuallyResequenceRouteRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId As String

            <DataMember(Name:="addresses")>
            Public Property Addresses As AddressInfo()
        End Class

        <DataContract>
        Class AddressInfo
            Inherits GenericParameters

            <DataMember(Name:="route_destination_id")>
            Public Property DestinationId As Integer

            <DataMember(Name:="sequence_no")>
            Public Property SequenceNo As Integer

            <DataMember(Name:="is_depot")>
            Public Property IsDepot As Boolean
        End Class

        Public Function ManuallyResequenceRoute(ByVal rParams As RouteParametersQuery, ByVal addresses As Address(), ByRef errorString As String) As DataObjectRoute
            Dim request As ManuallyResequenceRouteRequest = New ManuallyResequenceRouteRequest() With {
                .RouteId = rParams.RouteId
            }

            Dim lsAddresses As List(Of AddressInfo) = New List(Of AddressInfo)()
            Dim iMaxSequenceNumber As Integer = 0

            For Each address In addresses
                Dim aInfo As AddressInfo = New AddressInfo() With {
                    .DestinationId = If(address.RouteDestinationId IsNot Nothing, CInt(address.RouteDestinationId), -1),
                    .SequenceNo = If(address.SequenceNo IsNot Nothing, CInt(address.SequenceNo), iMaxSequenceNumber)
                }

                lsAddresses.Add(aInfo)
                iMaxSequenceNumber += 1
            Next

            request.Addresses = lsAddresses.ToArray()
            Dim route1 As DataObjectRoute = GetJsonObjectFromAPI(Of DataObjectRoute)(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, errorString)

            Return route1
        End Function

        Public Function RouteSharing(roParames As RouteParametersQuery, Email As String, ByRef errorString As String) As Boolean
            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            keyValues.Add(New KeyValuePair(Of String, String)("recipient_email", Email))
            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As StatusResponse = GetJsonObjectFromAPI(Of StatusResponse)(roParames, R4MEInfrastructureSettings.RouteSharing, HttpMethodType.Post, httpContent, errorString)

            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract>
        Private NotInheritable Class ResequenceRouteDestinationRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
            Public Property RouteDestinationId() As System.Nullable(Of Integer)
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As System.Nullable(Of Integer)

            <DataMember(Name:="addresses", EmitDefaultValue:=False)>
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
            Dim request As New ResequenceRouteDestinationRequest With {
               .RouteId = routeParames.RouteId,
               .RouteDestinationId = routeParames.RouteDestinationId,
               .Addresses = addresses
            }
            '.RouteId = routeParames.RouteId _
            '.RouteDestinationId = routeParames.RouteDestinationId
            '}

            Dim response As RouteResponse = GetJsonObjectFromAPI(Of RouteResponse)(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, False, errorString)

            Return response
        End Function

        <DataContract>
        Private NotInheritable Class DuplicateRouteResponse
            <DataMember(Name:="optimization_problem_id")>
            Public Property OptimizationProblemId() As String
                Get
                    Return m_OptimizationProblemId
                End Get
                Set(value As String)
                    m_OptimizationProblemId = value
                End Set
            End Property
            Private m_OptimizationProblemId As String

            <DataMember(Name:="success")>
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

            ' Redirect to page or return json for none
            queryParameters.ParametersCollection("to") = "none"

            Dim response As DuplicateRouteResponse = GetJsonObjectFromAPI(Of DuplicateRouteResponse)(queryParameters, R4MEInfrastructureSettings.DuplicateRoute, HttpMethodType.[Get], errorString)

            'If response IsNot Nothing AndAlso response.Success Then
            '    Dim optimizationProblemId As String = response.OptimizationProblemId
            '    If optimizationProblemId IsNot Nothing Then
            '        routeId = Me.GetRouteId(optimizationProblemId, errorString)
            '    End If
            'End If

            Dim routeId = If(response IsNot Nothing AndAlso response.Success,
                    If(response.OptimizationProblemId IsNot Nothing,
                    response.OptimizationProblemId, Nothing),
                Nothing)

            Return routeId
        End Function

        <DataContract>
        Private NotInheritable Class DeleteRouteResponse
            <DataMember(Name:="deleted")>
            Public Property Deleted() As [Boolean]
                Get
                    Return m_Deleted
                End Get
                Set(value As [Boolean])
                    m_Deleted = value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="errors")>
            Public Property Errors() As List(Of [String])
                Get
                    Return m_Errors
                End Get
                Set(value As List(Of [String]))
                    m_Errors = value
                End Set
            End Property
            Private m_Errors As List(Of [String])

            <DataMember(Name:="route_id")>
            Public Property routeId() As String
                Get
                    Return m_routeId
                End Get
                Set(value As String)
                    m_routeId = value
                End Set
            End Property
            Private m_routeId As String

            <DataMember(Name:="route_ids")>
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

        ''' <summary>
        ''' Get schedule calendar from the user's account.
        ''' </summary>
        ''' <param name="scheduleCalendarParams">Query parameters</param>
        ''' <param name="errorString">Error string</param>
        ''' <returns>Schedule calendar of the member</returns>
        Public Function GetScheduleCalendar(ByVal scheduleCalendarParams As ScheduleCalendarQuery, ByRef errorString As String) As ScheduleCalendarResponse
            Dim response = GetJsonObjectFromAPI(Of ScheduleCalendarResponse)(scheduleCalendarParams, R4MEInfrastructureSettings.ScheduleCalendar, HttpMethodType.[Get], errorString)

            Return response
        End Function

#End Region

#Region "Tracking"

        Public Function GetLastLocation(parameters As GenericParameters, ByRef errorString As String) As DataObjectRoute
            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute)(parameters, R4MEInfrastructureSettings.RouteHost, HttpMethodType.[Get], False, errorString)

            Return result
        End Function

        Public Function SetGPS(gpsParameters As GPSParameters, ByRef errorString As String) As String
            Dim result = GetJsonObjectFromAPI(Of String)(gpsParameters, R4MEInfrastructureSettings.SetGpsHost, HttpMethodType.[Get], True, errorString)

            Return result
        End Function

        <DataContract>
        Private NotInheritable Class FindAssetRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="tracking", EmitDefaultValue:=False)>
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

            Dim request As New FindAssetRequest With {
               .Tracking = tracking
            }

            Dim result As FindAssetResponse = GetJsonObjectFromAPI(Of FindAssetResponse)(request, R4MEInfrastructureSettings.AssetTracking, HttpMethodType.[Get], False, errorString)

            Return result
        End Function

        ''' <summary>
        ''' Get user locations
        ''' </summary>
        ''' <param name="parameters">Query parameters</param>
        ''' <param name="errorString">Error string</param>
        ''' <returns>An array of the user locations</returns>
        Public Function GetUserLocations(ByVal parameters As GenericParameters, ByRef errorString As String) As UserLocation()
            Dim userLocations = GetJsonObjectFromAPI(Of UserLocation())(parameters, R4MEInfrastructureSettings.UserLocation, HttpMethodType.[Get], False, errorString)

            Return userLocations
        End Function

#End Region

#Region "Users"
        <DataContract>
        Public NotInheritable Class GetUsersResponse
            <DataMember(Name:="results")>
            Public Property results As MemberResponseV4()
        End Class

        Public Function GetUsers(ByVal parameters As GenericParameters, ByRef errorString As String) As GetUsersResponse
            Dim result = GetJsonObjectFromAPI(Of GetUsersResponse)(parameters, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.[Get], errorString)

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

        <DataContract>
        Private NotInheritable Class ValidateSessionRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="session_guid", EmitDefaultValue:=False)>
            Public Property SessionGuid() As String
                Get
                    Return m_SessionGuid
                End Get
                Set(value As String)
                    m_SessionGuid = value
                End Set
            End Property
            Private m_SessionGuid As String

            <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
            Public Property MemberId() As System.Nullable(Of Integer)
                Get
                    Return m_MemberId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_MemberId = value
                End Set
            End Property
            Private m_MemberId As System.Nullable(Of Integer)

            <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)>
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
            Dim request As ValidateSessionRequest = New ValidateSessionRequest() With {
                .SessionGuid = memParams.SessionGuid,
                .MemberId = memParams.MemberId,
                .Format = memParams.Format
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

        <DataContract>
        Public NotInheritable Class USerDeleteResponse
            <DataMember(Name:="status")>
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

        Public Function CreateNewConfigurationKey(ByVal confParams As MemberConfigurationParameters(), ByRef errorString As String) As MemberConfigurationResponse
            Dim genParams As GenericParameters = New GenericParameters()
            Dim httpContent = New StringContent(fastJSON.JSON.ToJSON(confParams), System.Text.Encoding.UTF8, "application/json")

            Dim response = GetJsonObjectFromAPI(Of MemberConfigurationResponse)(genParams, R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Post, httpContent, errorString)

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

        <DataContract>
        Private NotInheritable Class GetConfigurationDataRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="config_key", EmitDefaultValue:=False)>
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

        Public Function GetConfigurationData(confParams As MemberConfigurationParameters, ByRef errorString As String) As MemberConfigurationDataResponse
            Dim mParams As GetConfigurationDataRequest
            mParams = New GetConfigurationDataRequest()
            If Not confParams Is Nothing Then
                mParams.config_key = confParams.config_key
            End If

            Dim response As MemberConfigurationDataResponse _
            = GetJsonObjectFromAPI(Of MemberConfigurationDataResponse)(
                mParams,
                R4MEInfrastructureSettings.UserConfiguration,
                HttpMethodType.Get,
                errorString
              )

            Return response

        End Function


        Public Function GetMemberCapabilities(ByRef errorString As String) As MemberCapabilities
            Dim parameters = New GenericParameters()

            Dim result = GetJsonObjectFromAPI(Of MemberCapabilities)(parameters, R4MEInfrastructureSettings.MemberCapabilities, HttpMethodType.[Get], errorString)

            Return result
        End Function

        ''' <summary>
        ''' Check if the member with the actualApiKey has commercial member capability.
        ''' </summary>
        ''' <param name="actualApiKey">Actual API key</param>
        ''' <param name="demoApiKey">Demo API key</param>
        ''' <param name="errorString">Error message text</param>
        ''' <returns>True, if the member has commercial capability</returns>
        Public Function MemberHasCommercialCapability(ByVal actualApiKey As String, ByVal demoApiKey As String, ByRef errorString As String) As Boolean
            Try
                Dim memberCapabilities = Me.GetMemberCapabilities(errorString)

                If actualApiKey = demoApiKey OrElse memberCapabilities Is Nothing Then Return False

                Dim commercialSubscription = memberCapabilities.[GetType]().GetProperties().Where(Function(x) x.Name = "Commercial").FirstOrDefault()

                If commercialSubscription Is Nothing Then Return False

                Return True
            Catch ex As Exception
                errorString = ex.Message

                Return False
            End Try
        End Function

#End Region

#Region "Address Notes"

        Public Function GetAddressNotes(noteParameters As NoteParameters, ByRef errorString As String) As AddressNote()
            Dim addressParameters As New AddressParameters() With {
                .RouteId = noteParameters.RouteId,
                .RouteDestinationId = noteParameters.AddressId,
                .Notes = True
            }
            Dim address As Address = Me.GetAddress(addressParameters, errorString)
            If address IsNot Nothing Then
                Return address.Notes
            Else
                Return Nothing
            End If
        End Function

        <DataContract>
        Private NotInheritable Class AddAddressNoteResponse
            <DataMember(Name:="status")>
            Public Property Status As Boolean

            <DataMember(Name:="note")>
            Public Property Note As AddressNote
        End Class

        Public Function AddAddressNote(ByVal noteParameters As NoteParameters, ByVal noteContents As String, ByRef errorString As String) As AddressNote
            Return Me.AddAddressNote(noteParameters, noteContents, Nothing, errorString)
        End Function

        ''' <summary>
        ''' The method offers ability to send a complex note at once,
        ''' with text content, uploading file, custom notes.
        ''' </summary>
        ''' <param name="noteParameters">The note parameters of the type NoteParameters
        ''' Note: contains form data elemets too</param>
        ''' <param name="errorString">Error string</param>
        ''' <returns>Created address note</returns>
        Public Function AddAddressNote(ByVal noteParameters As NoteParameters, ByRef errorString As String) As AddressNote
            Dim attachmentFileStream As FileStream = Nothing
            Dim attachmentStreamContent As StreamContent = Nothing
            Dim multipartFormDataContent = New MultipartFormDataContent()

            If noteParameters.StrFileName IsNot Nothing Then
                attachmentFileStream = File.OpenRead(noteParameters.StrFileName)
                attachmentStreamContent = New StreamContent(attachmentFileStream)
                multipartFormDataContent.Add(attachmentStreamContent, "strFilename", Path.GetFileName(noteParameters.StrFileName))
            End If

            multipartFormDataContent.Add(New StringContent(noteParameters.ActivityType), "strUpdateType")
            multipartFormDataContent.Add(New StringContent(noteParameters.StrNoteContents), "strNoteContents")

            If noteParameters.CustomNoteTypes IsNot Nothing AndAlso noteParameters.CustomNoteTypes.Count > 0 Then

                For Each customNote As KeyValuePair(Of String, String) In noteParameters.CustomNoteTypes
                    multipartFormDataContent.Add(New StringContent(customNote.Value), customNote.Key)
                Next
            End If

            Dim httpContent As HttpContent = multipartFormDataContent
            Dim response = GetJsonObjectFromAPI(Of AddAddressNoteResponse)(noteParameters, R4MEInfrastructureSettings.AddRouteNotesHost, HttpMethodType.Post, httpContent, errorString)
            If attachmentStreamContent IsNot Nothing Then attachmentStreamContent.Dispose()
            If attachmentFileStream IsNot Nothing Then attachmentFileStream.Dispose()
            If response IsNot Nothing AndAlso response.Note Is Nothing AndAlso response.Status = False Then errorString = "Note not added"
            Return If((response IsNot Nothing), (If(response.Note IsNot Nothing, response.Note, Nothing)), Nothing)
        End Function

        Public Function AddAddressNote(ByVal noteParameters As NoteParameters, ByVal noteContents As String, ByVal attachmentFilePath As String, ByRef errorString As String) As AddressNote
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
                Dim multipartFormDataContent As MultipartFormDataContent = New MultipartFormDataContent()
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

        Public Function addCustomNoteToRoute(ByVal noteParameters As NoteParameters, ByVal customNotes As Dictionary(Of String, String), ByRef errorString As String) As Object
            Dim keyValues = New List(Of KeyValuePair(Of String, String))()

            For Each kv1 As KeyValuePair(Of String, String) In customNotes
                keyValues.Add(New KeyValuePair(Of String, String)(kv1.Key, kv1.Value))
            Next

            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)
            Dim response As AddAddressNoteResponse = GetJsonObjectFromAPI(Of AddAddressNoteResponse)(noteParameters, R4MEInfrastructureSettings.AddRouteNotesHost, HttpMethodType.Post, httpContent, errorString)
            If response Is Nothing Then Return errorString
            If response.[GetType]() <> GetType(AddAddressNoteResponse) Then Return "Can not add custom note to the route"

            If response.Status Then
                Return response.Note
            Else
                Return "Can not add custom note to the route"
            End If
        End Function

        <DataContract>
        Private NotInheritable Class AddCustomNoteTypeRequest
            Inherits GenericParameters

            <DataMember(Name:="type", EmitDefaultValue:=False)>
            Public Property Type As String

            <DataMember(Name:="values", EmitDefaultValue:=False)>
            Public Property Values As String()
        End Class

        <DataContract>
        Private NotInheritable Class AddCustomNoteTypeResponse
            <DataMember(Name:="result")>
            Public Property Result As String

            <DataMember(Name:="affected")>
            Public Property Affected As Integer
        End Class

        Public Function AddCustomNoteType(ByVal customType As String, ByVal values As String(), ByRef errorString As String) As Object
            Dim request As AddCustomNoteTypeRequest = New AddCustomNoteTypeRequest() With {
                .Type = customType,
                .Values = values
            }
            Dim response As AddCustomNoteTypeResponse = GetJsonObjectFromAPI(Of AddCustomNoteTypeResponse)(request, R4MEInfrastructureSettings.CustomNoteType, HttpMethodType.Post, errorString)

            If response IsNot Nothing Then
                Return If(response.Result = "OK", response.Affected, -1)
            Else
                Return errorString
            End If
        End Function

        <DataContract>
        Private NotInheritable Class getAllCustomNoteTypesRequest
            Inherits GenericParameters
        End Class

        Public Function getAllCustomNoteTypes(ByRef errorString As String) As Object
            Dim request As getAllCustomNoteTypesRequest = New getAllCustomNoteTypesRequest()
            Dim response As CustomNoteType() = GetJsonObjectFromAPI(Of CustomNoteType())(request, R4MEInfrastructureSettings.CustomNoteType, HttpMethodType.[Get], errorString)

            If response IsNot Nothing Then
                Return response
            Else
                Return errorString
            End If
        End Function

        <DataContract>
        Private NotInheritable Class removeCustomNoteTypeRequest
            Inherits GenericParameters

            <DataMember(Name:="id", EmitDefaultValue:=False)>
            Public Property id As Integer
        End Class

        Public Function removeCustomNoteType(ByVal customNoteId As Integer, ByRef errorString As String) As Object
            Dim request As removeCustomNoteTypeRequest = New removeCustomNoteTypeRequest() With {
                .id = customNoteId
            }
            Dim response As AddCustomNoteTypeResponse = GetJsonObjectFromAPI(Of AddCustomNoteTypeResponse)(request, R4MEInfrastructureSettings.CustomNoteType, HttpMethodType.Delete, errorString)

            If response IsNot Nothing Then
                Return If(response.Result = "OK", response.Affected, -1)
            Else
                Return errorString
            End If
        End Function

#End Region

#Region "Activities"

        <DataContract>
        Private NotInheritable Class GetActivitiesResponse
            <DataMember(Name:="results")>
            Public Property Results As Activity()

            <DataMember(Name:="total")>
            Public Property Total As UInteger

        End Class

        Public Function GetActivityFeed(activityParameters As ActivityParameters, ByRef errorString As String) As Activity()
            Dim response As GetActivitiesResponse = GetJsonObjectFromAPI(Of GetActivitiesResponse)(activityParameters, R4MEInfrastructureSettings.ActivityFeed, HttpMethodType.[Get], errorString)
            Dim result As Activity() = Nothing

            If response IsNot Nothing Then
                result = response.Results
            End If

            Return result
        End Function

        Public Function GetActivities(activityParameters As ActivityParameters, ByRef errorString As String) As Activity()
            Dim response As GetActivitiesResponse = GetJsonObjectFromAPI(Of GetActivitiesResponse)(activityParameters, R4MEInfrastructureSettings.GetActivitiesHost, HttpMethodType.[Get], errorString)
            Dim result As Activity() = Nothing
            If response IsNot Nothing Then
                result = response.Results
            End If
            Return result
        End Function

        <DataContract>
        Private NotInheritable Class LogCustomActivityResponse
            <DataMember(Name:="status")>
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

        ''' <summary>
        ''' The request parameters for the route destination adding process.
        ''' </summary>
        <DataContract>
        Private NotInheritable Class AddRouteDestinationRequest
            Inherits GenericParameters

            ''' <summary>
            ''' The route ID
            ''' </summary>
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId As String

            ''' <summary>
            ''' The optimization ID
            ''' </summary>
            <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)>
            Public Property OptimizationProblemId As String

            ''' <summary>
            ''' The array of the Address type objects
            ''' </summary>
            <DataMember(Name:="addresses", EmitDefaultValue:=False)>
            Public Property Addresses As Address()

            ''' <summary>
            ''' If true, an address will be inserted at optimal position 
            ''' of a route
            ''' </summary>
            <DataMember(Name:="optimal_position", EmitDefaultValue:=True)>
            Public Property OptimalPosition As Boolean

        End Class

        ''' <summary>
        ''' Adds the address(es) into a route.
        ''' </summary>
        ''' <param name="routeId">The route ID</param>
        ''' <param name="addresses">Valid array of the Address type objects.</param>
        ''' <param name="errorString">out: Error as string</param>
        ''' <returns>An array of the IDs of added addresses</returns>
        Public Function AddRouteDestinations(routeId As String, addresses As Address(), ByRef errorString As String) As Integer()
            Dim request As New AddRouteDestinationRequest() With {
                .RouteId = routeId,
                .Addresses = addresses
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

        ''' <summary>
		''' Adds address(es) into a route.
		''' </summary>
		''' <param name="routeId"> The route ID </param>
		''' <param name="addresses"> Valid array of the Address type objects. </param>
		''' <param name="optimalPosition"> If true, an address will be inserted at optimal position of a route </param>
		''' <param name="errorString"> out: Error as string </param>
		''' <returns> An array of the IDs of added addresses </returns>
        Public Function AddRouteDestinations(ByVal routeId As String,
                                            ByVal addresses As Address(),
                                            ByVal optimalPosition As Boolean,
                                            ByRef errorString As String) As Integer()

            Dim request = New AddRouteDestinationRequest() With {
                .RouteId = routeId,
                .Addresses = addresses,
                .OptimalPosition = optimalPosition
            }

            Dim response = GetJsonObjectFromAPI(Of DataObject)(
                request,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, errorString)

            Dim arrDestinationIds = New List(Of Integer)()

            If response IsNot Nothing And response.Addresses IsNot Nothing Then
                addresses.ToList().ForEach(
                    Sub(addressNew)
                        response.Addresses.Where(
                        Function(addressResp) _
                                addressResp.AddressString = addressNew.AddressString And
                                Math.Abs(addressResp.Latitude - addressNew.Latitude) < 0.0001 And
                                Math.Abs(addressResp.Longitude - addressNew.Longitude) < 0.0001 And
                                    addressResp.RouteDestinationId IsNot Nothing
                            ).
                            ToList().ForEach(
                            Sub(addrResp)
                                arrDestinationIds.Add(CType(addrResp.RouteDestinationId, Integer))
                            End Sub)

                    End Sub)
            End If

            Return arrDestinationIds.ToArray()
        End Function

        Public Function AddOptimizationDestinations(ByVal optimizationId As String, ByVal addresses As Address(), ByRef errorString As String) As Integer?()
            Dim request = New AddRouteDestinationRequest() With {
                .OptimizationProblemId = optimizationId,
                .Addresses = addresses
            }

            Dim addressesList = addresses.[Select](Function(x) x.AddressString).ToList()

            Dim dataObject = GetJsonObjectFromAPI(Of DataObject)(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, errorString)

            Return If(dataObject?.Addresses?.Where(Function(x) addressesList.Contains(x.AddressString))?.[Select](Function(y) y.RouteDestinationId).ToArray(), Nothing)
        End Function


        <DataContract>
        Private NotInheritable Class MarkAddressAsMarkedAsDepartedRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
            Public Property RouteDestinationId() As System.Nullable(Of Integer)
                Get
                    Return m_RouteDestinationId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_RouteDestinationId = value
                End Set
            End Property
            Private m_RouteDestinationId As System.Nullable(Of Integer)

            <IgnoreDataMember>
            <DataMember(Name:="is_departed")>
            Public Property IsDeparted() As Boolean
                Get
                    Return m_IsDeparted
                End Get
                Set(value As Boolean)
                    m_IsDeparted = value
                End Set
            End Property
            Private m_IsDeparted As Boolean

            <IgnoreDataMember>
            <DataMember(Name:="is_visited")>
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
            Dim request As New MarkAddressAsMarkedAsDepartedRequest With {
                .RouteId = aParams.RouteId,
                .RouteDestinationId = aParams.RouteDestinationId,
                .IsDeparted = aParams.IsDeparted
            }

            Dim response As Address = GetJsonObjectFromAPI(Of Address)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.[Put], errorString)

            Return response
        End Function

        Public Function MarkAddressAsMarkedAsVisited(aParams As AddressParameters, ByRef errorString As String) As Address
            Dim request As New MarkAddressAsMarkedAsDepartedRequest With {
                .RouteId = aParams.RouteId,
                .RouteDestinationId = aParams.RouteDestinationId,
                .IsVisited = aParams.IsVisited
            }

            Dim response As Address = GetJsonObjectFromAPI(Of Address)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.[Put], errorString)

            Return response
        End Function

        <DataContract>
        Private NotInheritable Class MarkAddressDepartedRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)>
            Public Property AddressId() As System.Nullable(Of Integer)
                Get
                    Return m_AddressId
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_AddressId = value
                End Set
            End Property
            Private m_AddressId As System.Nullable(Of Integer)

            <IgnoreDataMember>
            <HttpQueryMemberAttribute(Name:="is_departed", EmitDefaultValue:=False)>
            Public Property IsDeparted() As Boolean
                Get
                    Return m_IsDeparted
                End Get
                Set(value As Boolean)
                    m_IsDeparted = value
                End Set
            End Property
            Private m_IsDeparted As Boolean

            <IgnoreDataMember>
            <HttpQueryMemberAttribute(Name:="is_visited", EmitDefaultValue:=False)>
            Public Property IsVisited() As Boolean
                Get
                    Return m_IsVisited
                End Get
                Set(value As Boolean)
                    m_IsVisited = value
                End Set
            End Property
            Private m_IsVisited As Boolean

            <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
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

        <DataContract>
        Private NotInheritable Class MarkAddressDepartedResponse
            <DataMember(Name:="status")>
            Public Property Status() As [Boolean]
                Get
                    Return m_Status
                End Get
                Set(value As [Boolean])
                    m_Status = value
                End Set
            End Property
            Private m_Status As [Boolean]

            <DataMember(Name:="error")>
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
            Dim request As New MarkAddressDepartedRequest With {
                .RouteId = aParams.RouteId,
                .AddressId = aParams.AddressId,
                .IsDeparted = aParams.IsDeparted,
                .MemberId = 1
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
            Dim request As New MarkAddressDepartedRequest With {
                .RouteId = aParams.RouteId,
                .AddressId = aParams.AddressId,
                .IsVisited = aParams.IsVisited,
                .MemberId = 1
            }

            Dim response As String = GetJsonObjectFromAPI(Of String)(request, R4MEInfrastructureSettings.MarkAddressVisited, HttpMethodType.[Get], errorString)
            Dim iResponse As Integer = 0
            If Integer.TryParse(response.ToString(), iResponse) Then
                iResponse = Convert.ToInt32(response)
            End If
            Return iResponse
        End Function

        <DataContract>
        Private NotInheritable Class InsertAddressIntoRouteOptimalPositionRequest
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <DataMember(Name:="addresses", EmitDefaultValue:=False)>
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()

            <DataMember(Name:="optimal_position", EmitDefaultValue:=False)>
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
            Dim request As New InsertAddressIntoRouteOptimalPositionRequest() With {
                .RouteId = routeId,
                .Addresses = addresses,
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

        <DataContract>
        Private NotInheritable Class RemoveRouteDestinationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
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

        <DataContract>
        Private NotInheritable Class RemoveRouteDestinationResponse
            <DataMember(Name:="deleted")>
            Public Property Deleted() As [Boolean]
                Get
                    Return m_Deleted
                End Get
                Set(value As [Boolean])
                    m_Deleted = value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="route_destination_id")>
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
            Dim request As New RemoveRouteDestinationRequest() With {
                .RouteId = routeId,
                .RouteDestinationId = destinationId
            }
            Dim response As RemoveRouteDestinationResponse = GetJsonObjectFromAPI(Of RemoveRouteDestinationResponse)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Deleted Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract>
        Private NotInheritable Class RemoveAddressFromOptimizationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)>
            Public Property OptimizationProblemId() As String
                Get
                    Return m_OptimizationProblemId
                End Get
                Set(value As String)
                    m_OptimizationProblemId = value
                End Set
            End Property
            Private m_OptimizationProblemId As String

            <HttpQueryMemberAttribute(Name:="route_destination_id", EmitDefaultValue:=False)>
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

        <DataContract>
        Private NotInheritable Class RemoveAddressFromOptimizationResponse
            <DataMember(Name:="deleted")>
            Public Property Deleted() As [Boolean]
                Get
                    Return m_Deleted
                End Get
                Set(value As [Boolean])
                    m_Deleted = value
                End Set
            End Property
            Private m_Deleted As [Boolean]

            <DataMember(Name:="route_destination_id")>
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
            Dim request As New RemoveAddressFromOptimizationRequest() With {
                .OptimizationProblemId = optiimizationId,
                .RouteDestinationId = destinationId
            }
            Dim response As RemoveAddressFromOptimizationResponse = GetJsonObjectFromAPI(Of RemoveAddressFromOptimizationResponse)(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Deleted Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract>
        Private NotInheritable Class MoveDestinationToRouteResponse
            <DataMember(Name:="success")>
            Public Property Success() As [Boolean]
                Get
                    Return m_Success
                End Get
                Set(value As [Boolean])
                    m_Success = value
                End Set
            End Property
            Private m_Success As [Boolean]

            <DataMember(Name:="error")>
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

        <DataContract>
        Public NotInheritable Class GetAddressBookContactsResponse
            <DataMember(Name:="results")>
            Public Property Results() As AddressBookContact()
                Get
                    Return m_Results
                End Get
                Set(value As AddressBookContact())
                    m_Results = value
                End Set
            End Property
            Private m_Results As AddressBookContact()

            <DataMember(Name:="total")>
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

        ''' <summary>
        ''' The request parameters for the address book locations searching process.
        ''' </summary>
        <DataContract>
        Private NotInheritable Class SearchAddressBookLocationRequest
            Inherits GenericParameters

            ' <value>Comma-delimited list of the contact IDs</value>
            <HttpQueryMemberAttribute(Name:="address_id", EmitDefaultValue:=False)>
            Public Property AddressId As String

            ' <value>The query text</value>
            <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)>
            Public Property Query As String

            ' <value>The comma-delimited list of the fields</value>
            <HttpQueryMemberAttribute(Name:="fields", EmitDefaultValue:=False)>
            Public Property Fields As String

            ' <value>Search starting position</value>
            <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
            Public Property Offset As Integer?

            ' <value>Search starting position</value>
            <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
            Public Property Limit As Integer?

        End Class

        <DataContract>
        Public NotInheritable Class SearchAddressBookLocationResponse
            <DataMember(Name:="results")>
            Public Property Results() As List(Of String())
                Get
                    Return m_Results
                End Get
                Set(value As List(Of String()))
                    m_Results = value
                End Set
            End Property
            Private m_Results As List(Of String())

            <DataMember(Name:="total")>
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = value
                End Set
            End Property
            Private m_Total As UInteger

            <DataMember(Name:="fields")>
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

        ''' <summary>
        ''' Searches for the address book locations 
        ''' </summary>
        ''' <param name="addressBookParameters">An AddressParameters type object as the input parameter</param>
        ''' <param name="errorString">out: Error as string</param>
        ''' <returns>List of the selected fields values</returns>
        Public Function SearchAddressBookLocation(ByVal addressBookParameters As AddressBookParameters, ByRef contactsFromObjects As List(Of AddressBookContact), ByRef errorString As String) As SearchAddressBookLocationResponse

            If addressBookParameters.Fields Is Nothing Then
                errorString = "Fields property should be specified."
                contactsFromObjects = Nothing
                Return Nothing
            End If

            Dim request = New SearchAddressBookLocationRequest()
            contactsFromObjects = New List(Of AddressBookContact)()

            If addressBookParameters.AddressId IsNot Nothing Then request.AddressId = addressBookParameters.AddressId

            If addressBookParameters.Query IsNot Nothing Then request.Query = addressBookParameters.Query

            request.Fields = addressBookParameters.Fields

            If addressBookParameters.Offset IsNot Nothing Then request.Offset = If(addressBookParameters.Offset >= 0, CInt(addressBookParameters.Offset), 0)
            If addressBookParameters.Limit IsNot Nothing Then request.Limit = If(addressBookParameters.Limit >= 0, CInt(addressBookParameters.Limit), 0)

            parseWithNewtonJson = True

            Dim errorString0 As String = Nothing
            Dim response = GetJsonObjectFromAPI(Of SearchAddressBookLocationResponse)(request, R4MEInfrastructureSettings.AddressBook, HttpMethodType.[Get], errorString0)
            Dim errorString1 As String = Nothing, errorString2 As String = Nothing, errorString3 As String = Nothing

            If response IsNot Nothing AndAlso response.Total > 0 Then
                Dim orderedPropertyNames = R4MeUtils.OrderPropertiesByPosition(Of AddressBookContact)(response.Fields.ToList(), errorString)

                For Each contactObjects As Object() In response.Results
                    Dim contactFromObject = New AddressBookContact()

                    For Each propertyName In orderedPropertyNames
                        Dim value = contactObjects(orderedPropertyNames.IndexOf(propertyName))
                        Dim valueType = If(value IsNot Nothing, value.[GetType]().Name, "")
                        Dim propInfo As PropertyInfo = GetType(AddressBookContact).GetProperty(propertyName)

                        Select Case propertyName
                            Case "address_custom_data"
                                Dim customData = R4MeUtils.ToObject(Of Dictionary(Of String, String))(value, errorString1)

                                If errorString1 = "" Then
                                    propInfo.SetValue(contactFromObject, customData)
                                Else
                                    propInfo.SetValue(contactFromObject, New Dictionary(Of String, String)() From {
                                {"<WRONG DATA>", "<WRONG DATA>"}
                            })
                                End If

                            Case "schedule"
                                Dim schedules = R4MeUtils.ToObject(Of Schedule())(value, errorString2)

                                If errorString2 = "" Then
                                    propInfo.SetValue(contactFromObject, schedules)
                                Else
                                    propInfo.SetValue(contactFromObject, Nothing)
                                End If

                            Case "schedule_blacklist"
                                Dim scheduleBlackList = R4MeUtils.ToObject(Of String())(value, errorString3)

                                If errorString3 = "" Then
                                    propInfo.SetValue(contactFromObject, scheduleBlackList)
                                Else
                                    propInfo.SetValue(contactFromObject, New String() {"<WRONG DATA>"})
                                End If

                            Case Else
                                Dim convertedValue = If(valueType <> "", R4MeUtils.ConvertObjectToPropertyType(value, propInfo), value)
                                propInfo.SetValue(contactFromObject, convertedValue)
                        End Select
                    Next

                    contactsFromObjects.Add(contactFromObject)
                Next
            Else
                errorString = errorString0
            End If

            Return response
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

        ''' <summary>
        ''' Updates a contact by comparing initial And modified contact objects And
        ''' by updating only modified proeprties of a contact.
        ''' </summary>
        ''' <param name="contact">A address book contact object as input (modified Or created virtual contact)</param>
        ''' <param name="initialContact">An initial address book contact</param>
        ''' <param name="errorString">Error string</param>
        ''' <returns>Updated address book contact</returns>
        Public Function UpdateAddressBookContact(ByVal contact As AddressBookContact, ByVal initialContact As AddressBookContact, ByRef errorString As String) As AddressBookContact
            errorString = ""
            parseWithNewtonJson = True

            If (initialContact Is Nothing) Or (initialContact Is contact) Then
                errorString = "The initial and modified contacts should not be null"
                Return Nothing
            End If

            Dim updatableContactProperties = R4MeUtils.GetPropertiesWithDifferentValues(contact, initialContact, errorString)
            updatableContactProperties.Add("address_id")
            Dim errorString0 As String = Nothing

            If updatableContactProperties IsNot Nothing AndAlso updatableContactProperties.Count > 0 Then
                Dim dynamicContactProperties = New Route4MeDynamicClass()

                dynamicContactProperties.CopyPropertiesFromClass(contact, updatableContactProperties, errorString0)

                Dim contactParamsJsonString = R4MeUtils.SerializeObjectToJson(dynamicContactProperties.DynamicProperties, True)
                Dim genParams = New GenericParameters()
                Dim content = New StringContent(contactParamsJsonString, System.Text.Encoding.UTF8, "application/json")
                Dim response = GetJsonObjectFromAPI(Of AddressBookContact)(genParams, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Put, content, errorString)

                Return response
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Updates an address book contact.
        ''' Used in case fo sending specified, limited number of the Contact parameters.
        ''' </summary>
        ''' <param name="contact">Address Book Contact</param>
        ''' <param name="updatableProperties">List of the properties which should be updated - 
        ''' despite are they null Or Not</param>
        ''' <param name="errorString">Error strings</param>
        ''' <returns>Address book contact</returns>
        Public Function UpdateAddressBookContact(ByVal contact As AddressBookContact, ByVal updatableProperties As List(Of String), ByRef errorString As String) As AddressBookContact
            parseWithNewtonJson = True

            Dim myDynamicClass = New Route4MeDynamicClass()

            Dim errorString0 As String = Nothing
            myDynamicClass.CopyPropertiesFromClass(contact, updatableProperties, errorString0)

            Dim jsonString = fastJSON.JSON.ToJSON(myDynamicClass.DynamicProperties)

            Dim genParams = New GenericParameters()

            Dim content = New StringContent(jsonString, System.Text.Encoding.UTF8, "application/json")

            Dim response = GetJsonObjectFromAPI(Of AddressBookContact)(genParams, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Put, content, errorString)

            Return response
        End Function

        <DataContract>
        Private NotInheritable Class RemoveAddressBookContactsRequest
            Inherits GenericParameters
            <DataMember(Name:="address_ids", EmitDefaultValue:=False)>
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

        <DataContract>
        Private NotInheritable Class RemoveAddressBookContactsResponse
            <DataMember(Name:="status")>
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
            Dim request As New RemoveAddressBookContactsRequest() With {
                .AddressIds = addressIds
            }
            Dim response As RemoveAddressBookContactsResponse = GetJsonObjectFromAPI(Of RemoveAddressBookContactsResponse)(request, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region "Address Book Groups"
        Public Function GetAddressBookGroups(ByVal addressBookGroupParameters As AddressBookGroupParameters, ByRef errorString As String) As AddressBookGroup()
            Dim response = GetJsonObjectFromAPI(Of AddressBookGroup())(addressBookGroupParameters, R4MEInfrastructureSettings.AddressBookGroup, HttpMethodType.[Get], errorString)
            Return response
        End Function

        Public Function GetAddressBookGroup(ByVal addressBookGroupParameters As AddressBookGroupParameters, ByRef errorString As String) As AddressBookGroup
            addressBookGroupParameters.PrepareForSerialization()
            Dim response = GetJsonObjectFromAPI(Of AddressBookGroup)(addressBookGroupParameters, R4MEInfrastructureSettings.AddressBookGroup, HttpMethodType.[Get], errorString)
            Return response
        End Function

        Public Function GetAddressBookContactsByGroup(ByVal addressBookGroupParameters As AddressBookGroupParameters, ByRef errorString As String) As AddressBookContactsResponse
            addressBookGroupParameters.PrepareForSerialization()

            Dim response = GetJsonObjectFromAPI(Of AddressBookContactsResponse) _
                (
                    addressBookGroupParameters,
                    R4MEInfrastructureSettings.AddressBookGroupSearch,
                    HttpMethodType.Post, errorString
                )

            Return response
        End Function

        Public Function SearchAddressBookContactsByFilter(ByVal addressBookGroupParameters As AddressBookGroupParameters, ByRef errorString As String) As AddressBookContactsResponse
            addressBookGroupParameters.PrepareForSerialization()
            Dim response = GetJsonObjectFromAPI(Of AddressBookContactsResponse)(addressBookGroupParameters, R4MEInfrastructureSettings.AddressBook, HttpMethodType.Post, errorString)
            Return response
        End Function

        Public Function AddAddressBookGroup(ByVal group As AddressBookGroup, ByRef errorString As String) As AddressBookGroup
            group.PrepareForSerialization()
            Dim result As AddressBookGroup = GetJsonObjectFromAPI(Of AddressBookGroup)(group, R4MEInfrastructureSettings.AddressBookGroup, HttpMethodType.Post, errorString)
            Return result
        End Function

        Public Function UpdateAddressBookGroup(ByVal group As AddressBookGroup, ByRef errorString As String) As AddressBookGroup
            group.PrepareForSerialization()
            Dim result As AddressBookGroup = GetJsonObjectFromAPI(Of AddressBookGroup)(group, R4MEInfrastructureSettings.AddressBookGroup, HttpMethodType.Put, errorString)
            Return result
        End Function

        Public Function RemoveAddressBookGroup(ByVal groupID As AddressBookGroupParameters, ByRef errorString As String) As StatusResponse
            groupID.PrepareForSerialization()
            Dim result As StatusResponse = GetJsonObjectFromAPI(Of StatusResponse)(groupID, R4MEInfrastructureSettings.AddressBookGroup, HttpMethodType.Delete, errorString)
            Return result
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

        <DataContract>
        Private NotInheritable Class DeleteAvoidanceZoneResponse
            <DataMember(Name:="status")>
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

        <DataContract>
        Private NotInheritable Class RemoveTerritoryResponse
            <DataMember(Name:="status")>
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

        Public Function GetOrderByID(orderQuery As OrderParameters, ByRef errorString As String) As Order
            'Dim ids As String() = orderQuery.order_id.Split(","c)
            'If ids.Length = 1 Then
            '    orderQuery.order_id = orderQuery.order_id + "," + orderQuery.order_id
            'End If
            Dim response As Order = GetJsonObjectFromAPI(Of Order)(orderQuery, R4MEInfrastructureSettings.Order, HttpMethodType.[Get], errorString)

            Return response
        End Function

        ''' <summary>
        ''' Searches for the orders.
        ''' </summary>
        ''' <param name="orderQuery">The OrderParameters type object as the query parameters</param>
        ''' <param name="errorString">out: Error as string</param>
        ''' <returns>List of the Order type objects</returns>
        Public Function SearchOrders(ByVal orderQuery As OrderParameters, ByRef errorString As String) As Object
            Dim showFields As Boolean = If((If(orderQuery?.fields?.Length, 0)) < 1, False, True)

            If showFields Then
                Return GetJsonObjectFromAPI(Of SearchOrdersResponse)(
                    orderQuery,
                    R4MEInfrastructureSettings.Order,
                    HttpMethodType.[Get],
                    errorString
                  )
            Else
                Return GetJsonObjectFromAPI(Of GetOrdersResponse)(
                    orderQuery,
                    R4MEInfrastructureSettings.Order,
                    HttpMethodType.[Get],
                    errorString
                  )
            End If
        End Function

        ''' <summary>
        ''' Filter for the orders filtering
        ''' </summary>
        ''' <param name="orderFilter">>The OrderFilterParameters object as a HTTP request payload</param>
        ''' <param name="errorString">out: Error as string</param>
        ''' <returns>Array of the Order type objects</returns>
        Public Function FilterOrders(ByVal orderFilter As OrderFilterParameters,
                                     ByRef errorString As String) As Order()

            Dim response As GetOrdersResponse = GetJsonObjectFromAPI(Of GetOrdersResponse)(
                orderFilter,
                R4MEInfrastructureSettings.Order,
                HttpMethodType.Post,
                errorString
             )

            Return If((response IsNot Nothing), response.Results, Nothing)
        End Function

        <DataContract>
        Private NotInheritable Class SearchOrdersByCustomFieldsResponse
            <DataMember(Name:="results")>
            Public Property Results() As List(Of Integer())
                Get
                    Return m_Results
                End Get
                Set(value As List(Of Integer()))
                    m_Results = value
                End Set
            End Property
            Private m_Results As List(Of Integer())

            <DataMember(Name:="total")>
            Public Property Total() As UInteger
                Get
                    Return m_Total
                End Get
                Set(value As UInteger)
                    m_Total = value
                End Set
            End Property
            Private m_Total As UInteger

            <DataMember(Name:="fields")>
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

        ''' <summary>
        ''' The response for the orders getting process.
        ''' </summary>
        <DataContract>
        Public NotInheritable Class GetOrdersResponse
            ''' An arrary of the Order type objects
            <DataMember(Name:="results")>
            Public Property Results As Order()

            ''' <value>Number of the returned orders</value>
            <DataMember(Name:="total")>
            Public Property Total As UInteger

            ''' <value>Selected order fields to show</value>
            <DataMember(Name:="fields", EmitDefaultValue:=False)>
            Public Property Fields As String()
        End Class

        ''' <summary>
        ''' The response from the orders searching process (contains specified fields).
        ''' </summary>
        <DataContract>
        Public NotInheritable Class SearchOrdersResponse
            ''' <value>An arrary of the objects.
            ''' The item type: Object[]</value>
            <DataMember(Name:="results")>
            Public Property Results As IList(Of Object())

            ''' <value>Number of the returned orders</value>
            <DataMember(Name:="total")>
            Public Property Total As UInteger

            ''' <value>Selected order fields to show</value>
            <DataMember(Name:="fields", EmitDefaultValue:=False)>
            Public Property Fields As String()
        End Class


        ''' <summary>
        ''' Get Orders
        ''' </summary>
        ''' <param name="ordersQuery"> Parameters for request </param>
        ''' <param name="total"> out: Total number of orders </param>
        ''' <param name="errorString"> out: Error as string </param>
        ''' <returns> Order object list </returns>
        Public Function GetOrders(ordersQuery As OrderParameters,
                                  ByRef total As UInteger,
                                  ByRef errorString As String) As Order()
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

        <DataContract>
        Private NotInheritable Class RemoveOrdersRequest
            Inherits GenericParameters
            <DataMember(Name:="order_ids", EmitDefaultValue:=False)>
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

        <DataContract>
        Private NotInheritable Class RemoveOrdersResponse
            <DataMember(Name:="status")>
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
            Dim request As New RemoveOrdersRequest() With {
                    .OrderIds = orderIds
            }
            Dim response As RemoveOrdersResponse = GetJsonObjectFromAPI(Of RemoveOrdersResponse)(request, R4MEInfrastructureSettings.Order, HttpMethodType.Delete, errorString)
            If response IsNot Nothing AndAlso response.Status Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataContract>
        Private NotInheritable Class AddOrdersToRouteRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="route_id", EmitDefaultValue:=False)>
            Public Property RouteId() As String
                Get
                    Return m_RouteId
                End Get
                Set(value As String)
                    m_RouteId = value
                End Set
            End Property
            Private m_RouteId As String

            <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)>
            Public Property Redirect() As System.Nullable(Of Integer)
                Get
                    Return m_Redirect
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Redirect = value
                End Set
            End Property
            Private m_Redirect As System.Nullable(Of Integer)

            <DataMember(Name:="addresses", EmitDefaultValue:=False)>
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()

            <DataMember(Name:="parameters", EmitDefaultValue:=False)>
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
            Dim request As New AddOrdersToRouteRequest With {
                .RouteId = rQueryParams.RouteId,
                .Redirect = rQueryParams.Redirect,
                .Addresses = addresses,
                .Parameters = rParams
            }

            Dim response As RouteResponse = GetJsonObjectFromAPI(Of RouteResponse)(request, R4MEInfrastructureSettings.RouteHost, HttpMethodType.Put, False, errorString)

            Return response
        End Function

        <DataContract>
        Private NotInheritable Class AddOrdersToOptimizationRequest
            Inherits GenericParameters
            <HttpQueryMemberAttribute(Name:="optimization_problem_id", EmitDefaultValue:=False)>
            Public Property OptimizationProblemId() As String
                Get
                    Return m_OptimizationProblemId
                End Get
                Set(value As String)
                    m_OptimizationProblemId = value
                End Set
            End Property
            Private m_OptimizationProblemId As String

            <HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)>
            Public Property Redirect() As System.Nullable(Of Integer)
                Get
                    Return m_Redirect
                End Get
                Set(value As System.Nullable(Of Integer))
                    m_Redirect = value
                End Set
            End Property
            Private m_Redirect As System.Nullable(Of Integer)

            <DataMember(Name:="addresses", EmitDefaultValue:=False)>
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = value
                End Set
            End Property
            Private m_Addresses As Address()

            <DataMember(Name:="parameters", EmitDefaultValue:=False)>
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
            Dim request As New AddOrdersToOptimizationRequest With {
                .OptimizationProblemId = rQueryParams.OptimizationProblemID,
                .Redirect = rQueryParams.Redirect,
                .Addresses = addresses,
                .Parameters = rParams
            }

            Dim response As DataObject = GetJsonObjectFromAPI(Of DataObject)(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, False, errorString)

            Return response
        End Function
#End Region

#Region "Order Custom User Fields"
        Public Function GetOrderCustomUserFields(ByRef errorString As String) As OrderCustomField()
            Dim genParams = New GenericParameters()
            Dim response As OrderCustomField() = GetJsonObjectFromAPI(Of OrderCustomField())(genParams, R4MEInfrastructureSettings.OrderCustomField, HttpMethodType.[Get], errorString)
            Return response
        End Function

        Public Function CreateOrderCustomUserField(ByVal orderCustomUserField As OrderCustomFieldParameters, ByRef errorString As String) As OrderCustomFieldCreateResponse
            Return GetJsonObjectFromAPI(Of OrderCustomFieldCreateResponse)(orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField, HttpMethodType.Post, False, errorString)
        End Function

        Public Function RemoveOrderCustomUserField(ByVal orderCustomUserField As OrderCustomFieldParameters, ByRef errorString As String) As OrderCustomFieldCreateResponse
            Return GetJsonObjectFromAPI(Of OrderCustomFieldCreateResponse)(orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField, HttpMethodType.Delete, False, errorString)
        End Function

        Public Function UpdateOrderCustomUserField(ByVal orderCustomUserFieldParams As OrderCustomFieldParameters, ByRef errorString As String) As OrderCustomFieldCreateResponse
            orderCustomUserFieldParams.PrepareForSerialization()
            Dim orderCustomField = GetJsonObjectFromAPI(Of OrderCustomFieldCreateResponse)(orderCustomUserFieldParams, R4MEInfrastructureSettings.OrderCustomField, HttpMethodType.Put, False, errorString)
            Return orderCustomField
        End Function
#End Region

#Region "Geocoding"
        ''' <summary>
        ''' The request parameters for the geocoding process.
        ''' </summary>
        <DataContract>
        Private NotInheritable Class GeocodingRequest
            Inherits GenericParameters

            ''' <value>The list of the addresses delimited by the symbol '|'</value>
            <HttpQueryMemberAttribute(Name:="addresses", EmitDefaultValue:=False)>
            Public Property Addresses As String

            ''' <value>The response format (son, xml)</value>
            <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)>
            Public Property Format As String

        End Class

        ''' <summary>
        ''' </summary>
        <DataContract>
        Private NotInheritable Class RapidStreetResponse
            <DataMember(Name:="zipcode")>
            Public Property Zipcode As String

            <DataMember(Name:="street_name")>
            Public Property StreetName As String
        End Class


        Public Function Geocoding(geoParams As GeocodingParameters, ByRef errorString As String) As String
            Dim request As New GeocodingRequest With {
                .Addresses = geoParams.Addresses,
                .Format = geoParams.Format
            }

            Dim response As String = GetXmlObjectFromAPI(Of String)(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post, DirectCast(Nothing, HttpContent), False, errorString)

            Return response.ToString()
        End Function

        Public Function BatchGeocoding(ByVal geoParams As GeocodingParameters, ByRef errorString As String) As String
            Dim request As New GeocodingRequest

            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            keyValues.Add(New KeyValuePair(Of String, String)("strExportFormat", geoParams.ExportFormat))
            keyValues.Add(New KeyValuePair(Of String, String)("addresses", geoParams.Addresses))

            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)

            Dim response As String = GetJsonObjectFromAPI(Of String)(
                request,
                R4MEInfrastructureSettings.Geocoder,
                HttpMethodType.Post,
                httpContent, True,
                errorString)

            Return response.ToString()
        End Function

        Public Function BatchGeocodingAsync(ByVal geoParams As GeocodingParameters, ByRef errorString As String) As String
            Dim request As New GeocodingRequest

            Dim keyValues = New List(Of KeyValuePair(Of String, String))()
            keyValues.Add(New KeyValuePair(Of String, String)("strExportFormat", geoParams.ExportFormat))
            keyValues.Add(New KeyValuePair(Of String, String)("addresses", geoParams.Addresses))

            Dim httpContent As HttpContent = New FormUrlEncodedContent(keyValues)
            Dim result As Task(Of Tuple(Of String, String)) = GetJsonObjectFromAPIAsync(Of String)(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post, httpContent, True)

            result.Wait()

            errorString = ""
            If result.IsFaulted OrElse result.IsCanceled Then errorString = result.Result.Item2

            Return result.Result.Item1
        End Function

        ''' <summary>
        ''' The response from the addresses uploading process to temporary storage.
        ''' </summary>
        <DataContract>
        Public NotInheritable Class uploadAddressesToTemporaryStorageResponse
            Inherits GenericParameters

            ''' <value>The optimization problem ID</value>
            <DataMember(Name:="optimization_problem_id", IsRequired:=False)>
            Public Property optimization_problem_id As String

            ''' <value>The temporary addresses storage ID</value>
            <DataMember(Name:="temporary_addresses_storage_id", IsRequired:=False)>
            Public Property temporary_addresses_storage_id As String

            ''' <value>Number of the uploaded addresses</value>
            <DataMember(Name:="address_count", IsRequired:=False)>
            Public Property address_count As UInteger

            ''' <value>Status of the process: true, false</value>
            <DataMember(Name:="status", IsRequired:=False)>
            Public Property status As Boolean
        End Class

        Public Function uploadAddressesToTemporaryStorage(ByVal jsonAddresses As String, ByRef errorString As String) As uploadAddressesToTemporaryStorageResponse
            Dim request As New GeocodingRequest

            Dim content As HttpContent = New StringContent(jsonAddresses)

            content.Headers.ContentType = New MediaTypeHeaderValue("application/json")

            Dim result As Tuple(Of uploadAddressesToTemporaryStorageResponse, String) =
                GetJsonObjectFromAPIAsync(Of uploadAddressesToTemporaryStorageResponse)(
                request,
                R4MEInfrastructureSettings.FastGeocoder,
                HttpMethodType.Post,
                content,
                False).GetAwaiter().GetResult()

            Thread.SpinWait(5000)

            errorString = result.Item2

            Return result.Item1
        End Function

        Public Function RapidStreetData(geoParams As GeocodingParameters, ByRef errorString As String) As ArrayList
            Dim request As New GeocodingRequest With {
                .Addresses = geoParams.Addresses,
                .Format = geoParams.Format
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
            Dim request As New GeocodingRequest With {
                .Addresses = geoParams.Addresses,
                .Format = geoParams.Format
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
            Dim request As New GeocodingRequest With {
                .Addresses = geoParams.Addresses,
                .Format = geoParams.Format
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
        Public Function GetVehicles(ByVal vehParams As VehicleParameters,
                                    ByRef errorString As String) As VehiclesPaginated

            Dim response As VehiclesPaginated = GetJsonObjectFromAPI(Of VehiclesPaginated)(
                vehParams,
                R4MEInfrastructureSettings.Vehicle_V4,
                HttpMethodType.[Get],
                errorString
            )

            Return response
        End Function

        Public Function CreateVehicle(ByVal vehicle As VehicleV4Parameters,
                                      ByRef errorString As String) As VehicleV4CreateResponse

            Dim newVehicle As VehicleV4CreateResponse = GetJsonObjectFromAPI(
                Of VehicleV4CreateResponse)(
                vehicle,
                R4MEInfrastructureSettings.Vehicle_V4_API,
                HttpMethodType.Post,
                errorString)

            Return newVehicle
        End Function

        Public Function GetVehicle(ByVal vehParams As VehicleParameters,
                                   ByRef errorString As String) As VehicleV4Response

            Dim response As VehicleV4Response = GetJsonObjectFromAPI(
                Of VehicleV4Response)(
                vehParams,
                R4MEInfrastructureSettings.Vehicle_V4,
                HttpMethodType.[Get],
                errorString)

            Return response
        End Function

        Public Function updateVehicle(ByVal vehParams As VehicleV4Parameters,
                                      ByVal vehicleId As String,
                                      ByRef errorString As String) As VehicleV4Response

            Dim response As VehicleV4Response = GetJsonObjectFromAPI(
                Of VehicleV4Response)(
                vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 & "/" + vehicleId,
                HttpMethodType.Put,
                errorString)

            Return response
        End Function

        Public Function deleteVehicle(ByVal vehParams As VehicleV4Parameters,
                                      ByRef errorString As String) As VehicleV4Response
            Dim response As VehicleV4Response = GetJsonObjectFromAPI(
                Of VehicleV4Response)(
                vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 & "/" + vehParams.VehicleId,
                HttpMethodType.Delete,
                errorString)

            Return response
        End Function

#End Region

#Region "Telematics Vendors"
        Public Function GetAllTelematicsVendors(ByVal vendorParams As TelematicsVendorParameters,
                                                ByRef errorString As String) As TelematicsVendorsResponse

            Dim response As TelematicsVendorsResponse = GetJsonObjectFromAPI(
                Of TelematicsVendorsResponse)(
                vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost, HttpMethodType.[Get],
                errorString)

            Return response
        End Function

        Public Function GetTelematicsVendor(ByVal vendorParams As TelematicsVendorParameters,
                                            ByRef errorString As String) As TelematicsVendorResponse

            Dim response As TelematicsVendorResponse = GetJsonObjectFromAPI(
                Of TelematicsVendorResponse)(
                vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.[Get],
                errorString)

            Return response
        End Function

        Public Function SearchTelematicsVendors(ByVal vendorParams As TelematicsVendorParameters,
                                                ByRef errorString As String) As TelematicsVendorsResponse

            Dim response As TelematicsVendorsResponse = GetJsonObjectFromAPI(
                Of TelematicsVendorsResponse)(
                vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.[Get],
                errorString)

            Return response
        End Function
#End Region

#Region "Generic Methods"

        Public Function GetStringResponseFromAPI(optimizationParameters As GenericParameters,
                                                 url As String, httpMethod As HttpMethodType,
                                                 ByRef errorMessage As String) As String

            Dim result As String = GetJsonObjectFromAPI(Of String)(
                optimizationParameters,
                url, httpMethod,
                True,
                errorMessage)

            Return result
        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(
                                                           optimizationParameters As GenericParameters,
                                                           url As String,
                                                           httpMethod As HttpMethodType,
                                                           ByRef errorMessage As String) As T

            Dim result As T = GetJsonObjectFromAPI(Of T)(
                optimizationParameters,
                url,
                httpMethod,
                False,
                errorMessage)

            Return result
        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(
                                                           optimizationParameters As GenericParameters,
                                                           url As String,
                                                           httpMethod As HttpMethodType,
                                                           httpContent As HttpContent,
                                                           ByRef errorMessage As String) As T

            Dim result As T = GetJsonObjectFromAPI(Of T)(
                optimizationParameters,
                url,
                httpMethod,
                httpContent,
                False,
                errorMessage)

            Return result
        End Function

        Private Function GetJsonObjectFromAPI(Of T As Class)(
                                                            optimizationParameters As GenericParameters,
                                                            url As String,
                                                            httpMethod As HttpMethodType,
                                                            isString As Boolean,
                                                            ByRef errorMessage As String) As T

            Dim result As T = GetJsonObjectFromAPI(Of T)(
                optimizationParameters,
                url,
                httpMethod,
                DirectCast(Nothing, HttpContent),
                isString,
                errorMessage)

            Return result
        End Function

        Private Async Function GetJsonObjectFromAPIAsync(Of T As Class)(
                                                                       ByVal optimizationParameters As GenericParameters,
                                                                       ByVal url As String,
                                                                       ByVal httpMethod As HttpMethodType,
                                                                       ByVal isString As Boolean) As Task(Of Tuple(Of T, String))

            Return Await Task.Run(
                Function()
                    Dim result As Task(Of Tuple(Of T, String)) = GetJsonObjectFromAPIAsync(Of T)(
                                                                    optimizationParameters,
                                                                    url,
                                                                    httpMethod,
                                                                    CType(Nothing, HttpContent), isString)

                    Return result
                End Function)
        End Function

        Private Async Function GetJsonObjectFromAPIAsync(Of T As Class)(ByVal optimizationParameters As GenericParameters,
                                                                        ByVal url As String, ByVal httpMethod As HttpMethodType,
                                                                        ByVal httpContent As HttpContent,
                                                                        ByVal isString As Boolean) As Task(Of Tuple(Of T, String))

            Dim result As T = Nothing
            Dim errorMessage As String = String.Empty

            Try

                Using httpClient As HttpClient = CreateAsyncHttpClient(url)
                    Dim parametersURI As String = optimizationParameters.Serialize(m_ApiKey)

                    Select Case httpMethod
                        Case HttpMethodType.[Get]
                            Dim response = Await httpClient.GetStreamAsync(parametersURI)
                            result = If(isString, TryCast(response.ReadString(), T), response.ReadObject(Of T)())
                            Exit Select
                        Case HttpMethodType.Post, HttpMethodType.Put, HttpMethodType.Delete
                            Dim isPut As Boolean = httpMethod = HttpMethodType.Put
                            Dim isDelete As Boolean = httpMethod = HttpMethodType.Delete
                            Dim content As HttpContent = Nothing

                            If httpContent IsNot Nothing Then
                                content = httpContent
                            Else
                                Dim jsonString As String = R4MeUtils.SerializeObjectToJson(optimizationParameters)
                                content = New StringContent(jsonString)
                                content.Headers.ContentType = New MediaTypeHeaderValue("application/json")
                            End If

                            Dim response As HttpResponseMessage = Nothing

                            If isPut Then
                                response = Await httpClient.PutAsync(parametersURI, content)
                            ElseIf isDelete Then
                                Dim request As HttpRequestMessage = New HttpRequestMessage With {
                                    .Content = content,
                                    .Method = System.Net.Http.HttpMethod.Delete,
                                    .RequestUri = New Uri(parametersURI, UriKind.Relative)
                                }
                                response = Await httpClient.SendAsync(request)
                            Else
                                Dim request = New HttpRequestMessage()
                                response = Await httpClient.PostAsync(parametersURI, content).ConfigureAwait(True)
                            End If

                            If TypeOf response.Content Is StreamContent Then
                                Dim streamTask = Await (CType(response.Content, StreamContent)).ReadAsStreamAsync()
                                result = If(isString, TryCast(streamTask.ReadString(), T), streamTask.ReadObject(Of T)())
                            Else
                                Dim streamTask = Await (CType(response.Content, StreamContent)).ReadAsStreamAsync()
                                Dim errorResponse As ErrorResponse = Nothing

                                Try
                                    errorResponse = streamTask.ReadObject(Of ErrorResponse)()
                                Catch
                                    errorResponse = Nothing
                                End Try

                                If errorResponse IsNot Nothing AndAlso errorResponse.Errors IsNot Nothing AndAlso errorResponse.Errors.Count > 0 Then

                                    For Each [error] As String In errorResponse.Errors
                                        If errorMessage.Length > 0 Then errorMessage += "; "
                                        errorMessage += [error]
                                    Next
                                Else
                                    Dim responseStream = Await response.Content.ReadAsStringAsync()
                                    Dim responseString As String = responseStream
                                    If responseString IsNot Nothing Then errorMessage = "Response: " & responseString
                                End If
                            End If

                            Exit Select
                    End Select
                End Using

            Catch e As HttpListenerException
                'errorMessage = If(TypeOf e Is AggregateException, e.InnerException.Message, e.Message)
                errorMessage = e.Message & " --- " & errorMessage
                result = Nothing
            Catch e As Exception
                errorMessage = If(TypeOf e Is AggregateException, e.InnerException.Message, e.Message)
                result = Nothing
            End Try

            Return New Tuple(Of T, String)(result, errorMessage)
        End Function

        Private Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As GenericParameters,
                                                             ByVal url As String,
                                                             ByVal httpMethod As HttpMethodType,
                                                             ByVal httpContent As HttpContent,
                                                             ByVal isString As Boolean,
                                                             ByRef errorMessage As String) As T
            Dim result As T = Nothing
            errorMessage = String.Empty

            Try

                Using httpClient As HttpClient = CreateHttpClient(url)
                    Dim parametersURI As String = optimizationParameters.Serialize(m_ApiKey)

                    Select Case httpMethod
                        Case HttpMethodType.[Get]
                            Dim response = httpClient.GetStreamAsync(parametersURI)
                            response.Wait()

                            If response.IsCompleted Then
                                result = If(isString, TryCast(response.Result.ReadString(), T), response.Result.ReadObject(Of T)())
                            End If

                            Exit Select
                        Case HttpMethodType.Post, HttpMethodType.Put, HttpMethodType.Delete
                            Dim isPut As Boolean = httpMethod = HttpMethodType.Put
                            Dim isDelete As Boolean = httpMethod = HttpMethodType.Delete
                            Dim content As HttpContent = Nothing

                            If httpContent IsNot Nothing Then
                                content = httpContent
                            Else
                                Dim jsonString As String = R4MeUtils.SerializeObjectToJson(optimizationParameters)
                                content = New StringContent(jsonString)
                                content.Headers.ContentType = New MediaTypeHeaderValue("application/json")
                            End If

                            Dim response As Task(Of HttpResponseMessage) = Nothing

                            If isPut Then
                                response = httpClient.PutAsync(parametersURI, content)
                            ElseIf isDelete Then
                                Dim request As HttpRequestMessage = New HttpRequestMessage With {
                                    .Content = content,
                                    .Method = System.Net.Http.HttpMethod.Delete,
                                    .RequestUri = New Uri(parametersURI, UriKind.Relative)
                                }

                                response = httpClient.SendAsync(request)
                            Else
                                Dim cts = New CancellationTokenSource()
                                cts.CancelAfter(1000 * 60 * 5)
                                Dim request = New HttpRequestMessage()
                                response = httpClient.PostAsync(parametersURI, content, cts.Token)
                            End If

                            response.Wait()

                            If response.IsCompleted AndAlso response.Result.IsSuccessStatusCode AndAlso TypeOf response.Result.Content Is StreamContent Then
                                Dim streamTask = (CType(response.Result.Content, StreamContent)).ReadAsStreamAsync()
                                streamTask.Wait()

                                If streamTask.IsCompleted Then
                                    result = If(isString, TryCast(streamTask.Result.ReadString(), T), streamTask.Result.ReadObject(Of T)())
                                End If
                            Else
                                Dim streamTask = (CType(response.Result.Content, StreamContent)).ReadAsStreamAsync()
                                streamTask.Wait()
                                Dim errorResponse As ErrorResponse = Nothing

                                Try
                                    errorResponse = streamTask.Result.ReadObject(Of ErrorResponse)()
                                Catch
                                    errorResponse = Nothing
                                End Try

                                If errorResponse IsNot Nothing AndAlso errorResponse.Errors IsNot Nothing AndAlso errorResponse.Errors.Count > 0 Then

                                    For Each [error] As String In errorResponse.Errors
                                        If errorMessage.Length > 0 Then errorMessage += "; "
                                        errorMessage += [error]
                                    Next
                                Else
                                    Dim responseStream = response.Result.Content.ReadAsStringAsync()
                                    responseStream.Wait()
                                    Dim responseString As String = responseStream.Result
                                    If responseString IsNot Nothing Then errorMessage = "Response: " & responseString
                                End If
                            End If

                            Exit Select
                    End Select
                End Using

            Catch e As HttpListenerException
                'errorMessage = If(TypeOf e Is AggregateException, e.InnerException.Message, e.Message)
                errorMessage = e.Message & " --- " & errorMessage
                result = Nothing
            Catch e As Exception
                errorMessage = If(TypeOf e Is AggregateException, e.InnerException.Message, e.Message)
                result = Nothing
            End Try

            Return result
        End Function

        Private Function GetXmlObjectFromAPI(Of T As Class)(optimizationParameters As GenericParameters,
                                                            url As String,
                                                            httpMethod__1 As HttpMethodType,
                                                            httpContent As HttpContent,
                                                            isString As Boolean,
                                                            ByRef errorMessage As String) As String
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
                                    Dim request As New HttpRequestMessage() With {
                                        .Content = content,
                                        .Method = HttpMethod.Delete,
                                        .RequestUri = New Uri(parametersURI, UriKind.Relative)
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

        Private Function CreateAsyncHttpClient(ByVal url As String) As HttpClient
            Dim handler = New HttpClientHandler() With {
                .AllowAutoRedirect = False
            }
            Dim supprotsAutoRdirect = handler.SupportsAutomaticDecompression
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or CType(768, SecurityProtocolType) Or CType(3072, SecurityProtocolType)
            Dim result As HttpClient = New HttpClient(handler) With {
                .BaseAddress = New Uri(url)
            }
            result.Timeout = m_DefaultTimeOut
            result.DefaultRequestHeaders.Accept.Clear()
            result.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            Return result
        End Function

        Private Function CreateHttpClient(url As String) As HttpClient
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or CType(768, SecurityProtocolType) Or CType(3072, SecurityProtocolType)

            Dim result As New HttpClient() With {
                .BaseAddress = New Uri(url)
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
