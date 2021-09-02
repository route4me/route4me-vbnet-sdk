Imports System.Text
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Runtime.Serialization
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Runtime.InteropServices
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.System.Net.Http
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5.TelematicsPlatform

Namespace Route4MeSDK
    Public NotInheritable Class Route4MeManagerV5

        Private ReadOnly m_ApiKey As String
        Private ReadOnly m_DefaultTimeOut As TimeSpan = New TimeSpan(TimeSpan.TicksPerMinute * 30)
        Private parseWithNewtonJson As Boolean

        Private mandatoryFields As String()

        Public Sub New(ByVal apiKey As String)
            m_ApiKey = apiKey
            parseWithNewtonJson = False
        End Sub

#Region "Team Management"

        ''' <summary>
        ''' The request parameters for retrieving team members.
        ''' </summary>
        <DataContract()>
        Public NotInheritable Class MemberQueryParameters
            Inherits QueryTypes.GenericParameters

            <QueryTypes.HttpQueryMemberAttribute(Name:="user_id", EmitDefaultValue:=False)>
            Public Property UserId As String
        End Class

        ''' <summary>
        ''' The request class to bulk create the team members.
        ''' </summary>
        <DataContract>
        Private NotInheritable Class BulkMembersRequest
            Inherits QueryTypes.GenericParameters

            <DataMember(Name:="users")>
            Public Property Users As TeamRequest()
        End Class

        ''' <summary>
        ''' Retrieve all existing sub-users associated with the Member’s account.
        ''' </summary>
        ''' <param name="failResponse">Failing response</param>
        ''' <returns>An array of the TeamResponseV5 type objects</returns>
        Public Function GetTeamMembers(<Out> ByRef failResponse As ResultResponse) As TeamResponse()
            Dim parameters = New QueryTypes.GenericParameters()

            Dim result = GetJsonObjectFromAPI(Of TeamResponse())(
                parameters,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.[Get],
                failResponse)

            Return result
        End Function

        ''' <summary>
        ''' Retrieve a team member by the parameter UserId
        ''' </summary>
        ''' <param name="parameters">Query parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Retrieved team member</returns>
        Public Function GetTeamMemberById(ByVal parameters As MemberQueryParameters,
                                         <Out> ByRef resultResponse As ResultResponse) As TeamResponse

            If (If(parameters?.UserId, Nothing)) Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The UserId parameter is not specified"}}
                    }
                }
                Return Nothing
            End If

            Dim result = GetJsonObjectFromAPI(Of TeamResponse)(
                parameters,
                R4MEInfrastructureSettingsV5.TeamUsers & "/" & parameters.UserId,
                HttpMethodType.[Get],
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Creates new team member (sub-user) in the user's account
        ''' </summary>
        ''' <param name="memberParams">An object of the type MemberParametersV4</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Created team member</returns>
        Public Function CreateTeamMember(ByVal memberParams As TeamRequest, <Out> ByRef resultResponse As ResultResponse) As TeamResponse
            Dim error0 As String = Nothing

            If Not memberParams.ValidateMemberCreateRequest(error0) Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {error0}}
                    }
                }
                Return Nothing
            End If

            Return GetJsonObjectFromAPI(Of TeamResponse)(
                memberParams,
                R4MEInfrastructureSettingsV5.TeamUsers,
                HttpMethodType.Post,
                resultResponse)
        End Function

        ''' <summary>
        ''' Bulk create the team members. 
        ''' TO DO: there is no response from the function.
        ''' </summary>
        ''' <param name="membersParams">Member request parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns></returns>
        Public Function BulkCreateTeamMembers(ByVal membersParams As TeamRequest(),
                                              <Out> ByRef resultResponse As ResultResponse) As ResultResponse
            resultResponse = Nothing

            If (If(membersParams?.Length, 0)) < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The array of the user parameters is empty"}}
                    }
                }
                Return Nothing
            End If

            Dim error0 As String = Nothing

            For Each memberParams In membersParams

                If Not memberParams.ValidateMemberCreateRequest(error0) Then
                    resultResponse = New ResultResponse() With {
                        .Status = False,
                        .Messages = New Dictionary(Of String, String())() From {
                            {"Error", New String() {error0}}
                        }
                    }
                    Return Nothing
                End If
            Next

            Dim newMemberParams = New BulkMembersRequest() With {
                .Users = membersParams
            }

            Dim result = GetJsonObjectFromAPI(Of ResultResponse)(
                newMemberParams,
                R4MEInfrastructureSettingsV5.TeamUsersBulkCreate,
                HttpMethodType.Post,
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Removes a team member (sub-user) from the user's account.
        ''' </summary>
        ''' <param name="parameters">An object of the type MemberParametersV4 containg the parameter UserId</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Removed team member</returns>
        Public Function RemoveTeamMember(ByVal parameters As MemberQueryParameters, <Out> ByRef resultResponse As ResultResponse) As TeamResponse

            If (If(parameters?.UserId, Nothing)) Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The UserId parameter is not specified"}}
                    }
                }
                Return Nothing
            End If

            Dim response = GetJsonObjectFromAPI(Of TeamResponse)(
                parameters,
                R4MEInfrastructureSettingsV5.TeamUsers & "/" & parameters.UserId,
                HttpMethodType.Delete,
                resultResponse)

            Return response
        End Function

        ''' <summary>
        ''' Update a team member
        ''' </summary>
        ''' <param name="queryParameters">Member query parameters</param>
        ''' <param name="requestPayload">Member request parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Updated team member</returns>
        Public Function UpdateTeamMember(ByVal queryParameters As MemberQueryParameters,
                                         ByVal requestPayload As TeamRequest,
                                         <Out> ByRef resultResponse As ResultResponse) As TeamResponse

            If (If(queryParameters?.UserId, Nothing)) Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The UserId parameter is not specified"}}
                    }
                }
                Return Nothing
            End If

            If requestPayload Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The team request object is empty"}}
                    }
                }
                Return Nothing
            End If

            Dim response = GetJsonObjectFromAPI(Of TeamResponse)(
                requestPayload, R4MEInfrastructureSettingsV5.TeamUsers & "/" & queryParameters.UserId,
                HttpMethodType.Patch,
                resultResponse)

            Return response
        End Function

        ''' <summary>
        ''' Add an array of skills to the driver.
        ''' </summary>
        ''' <param name="queryParameters">Query parameters</param>
        ''' <param name="skills">An array of the driver skills</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Updated team member</returns>
        Public Function AddSkillsToDriver(ByVal queryParameters As MemberQueryParameters,
                                          ByVal skills As String(),
                                          <Out> ByRef resultResponse As ResultResponse) As TeamResponse

            If (If(queryParameters?.UserId, Nothing)) Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The UserId parameter is not specified"}}
                    }
                }
                Return Nothing
            End If

            If (If(skills?.Length, 0)) < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The driver skills array is empty."}}
                    }
                }
                Return Nothing
            End If

            Dim driverSkills = New Dictionary(Of String, String)()
            driverSkills.Add("driver_skills", String.Join(",", skills))
            Dim requestPayload = New TeamRequest() With {
                .CustomData = driverSkills
            }
            Dim response = GetJsonObjectFromAPI(Of TeamResponse)(
                requestPayload,
                R4MEInfrastructureSettingsV5.TeamUsers & "/" & queryParameters.UserId,
                HttpMethodType.Patch,
                resultResponse)

            Return response
        End Function

#End Region

#Region "Driver Review"

        ''' <summary>
        ''' Get list of the drive reviews.
        ''' </summary>
        ''' <param name="parameters">Query parmeters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>List of the driver reviews</returns>
        Public Function GetDriverReviewList(ByVal parameters As DriverReviewParameters,
                                            <Out> ByRef resultResponse As ResultResponse) As DriverReviewsResponse

            parseWithNewtonJson = True

            Dim result = GetJsonObjectFromAPI(Of DriverReviewsResponse)(
                parameters,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.[Get],
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Get driver review by ID
        ''' </summary>
        ''' <param name="parameters">Query parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Driver review</returns>
        Public Function GetDriverReviewById(ByVal parameters As DriverReviewParameters,
                                            <Out> ByRef resultResponse As ResultResponse) As DriverReview

            If (If(parameters?.RatingId, Nothing)) Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The RatingId parameter is not specified"}}
                    }
                }
                Return Nothing
            End If

            parseWithNewtonJson = True
            Dim result = GetJsonObjectFromAPI(Of DriverReview)(
                parameters,
                R4MEInfrastructureSettingsV5.DriverReview & "/" + parameters.RatingId,
                HttpMethodType.[Get],
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Upload driver review to the server.
        ''' </summary>
        ''' <param name="driverReview">Request payload</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Driver review</returns>
        Public Function CreateDriverReview(ByVal driverReview As DriverReview, <Out> ByRef resultResponse As ResultResponse) As DriverReview

            Return GetJsonObjectFromAPI(Of DriverReview)(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview,
                HttpMethodType.Post,
                resultResponse)

        End Function

        ''' <summary>
        ''' Update a driver review.
        ''' </summary>
        ''' <param name="driverReview">Request payload</param>
        ''' <param name="method">Http method</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Driver review</returns>
        Public Function UpdateDriverReview(ByVal driverReview As DriverReview,
                                           ByVal method As HttpMethodType,
                                           <Out> ByRef resultResponse As ResultResponse) As DriverReview

            If method <> HttpMethodType.Patch AndAlso method <> HttpMethodType.Put Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The parameter method has an incorect value."}}
                    }
                }
                Return Nothing
            End If

            If driverReview.RatingId Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The parameters doesn't contain parameter RatingId."}}
                    }
                }
                Return Nothing
            End If

            Return GetJsonObjectFromAPI(Of DriverReview)(
                driverReview,
                R4MEInfrastructureSettingsV5.DriverReview & "/" + driverReview.RatingId,
                method,
                resultResponse)
        End Function

#End Region

#Region "Address Book Contacts"

        ''' <summary>
        ''' Remove the address book contacts.
        ''' </summary>
        ''' <param name="contactIDs">The array of the address ID</param>
        ''' <param name="resultResponse">out: Error response</param>
        ''' <returns>If true the contacts were removed successfully</returns>
        Public Function RemoveAddressBookContacts(ByVal contactIDs As Integer(),
                                                  <Out> ByRef resultResponse As ResultResponse) As Boolean
            Dim request = New AddressBookContactsRequest() With {
                .AddressIds = contactIDs
            }
            Dim response = GetJsonObjectFromAPI(Of StatusResponse)(
                request,
                R4MEInfrastructureSettingsV5.ContactsDeleteMultiple,
                HttpMethodType.Delete,
                resultResponse)

            Return If(resultResponse Is Nothing, True, False)
        End Function


        Public Function GetAddressBookContacts(ByVal addressBookParameters As AddressBookParameters,
                                               <Out> ByRef resultResponse As ResultResponse) As AddressBookContactsResponse

            Dim response = GetJsonObjectFromAPI(Of AddressBookContactsResponse)(
                addressBookParameters,
                R4MEInfrastructureSettingsV5.ContactsGetAll,
                HttpMethodType.[Get],
                resultResponse)

            Return response
        End Function

        ''' <summary>
        ''' Get an address book contact by ID
        ''' </summary>
        ''' <param name="contactId">contact ID</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An AddressBookContact type object</returns>
        Public Function GetAddressBookContactById(ByVal contactId As Integer,
                                                  <Out> ByRef resultResponse As ResultResponse) As AddressBookContact

            Dim gparams = New Route4MeSDK.QueryTypes.GenericParameters()

            gparams.ParametersCollection.Add("address_id", contactId.ToString())

            Dim response = GetJsonObjectFromAPI(Of AddressBookContact)(
                gparams,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.[Get], resultResponse)

            Return response
        End Function

        ''' <summary>
        ''' The request parameter for the address book contacts removing process.
        ''' </summary>
        <DataContract>
        Public NotInheritable Class AddressBookContactsRequest
            Inherits QueryTypes.GenericParameters

            ''' The array of the address IDs
            <DataMember(Name:="address_ids", EmitDefaultValue:=False)>
            Public Property AddressIds As Integer()
        End Class

        ''' <summary>
        ''' Get address book contacts by sending an array of address IDs.
        ''' </summary>
        ''' <param name="contactIDs">An array of address IDs</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An AddressBookContactsResponse type object</returns>
        Public Function GetAddressBookContactsByIds(ByVal contactIDs As Integer(),
                                                    <Out> ByRef resultResponse As ResultResponse) As AddressBookContactsResponse

            Dim request = New AddressBookContactsRequest() With {
                .AddressIds = contactIDs
            }

            Dim response = GetJsonObjectFromAPI(Of AddressBookContactsResponse)(
                request,
                R4MEInfrastructureSettingsV5.ContactsFind,
                HttpMethodType.Post,
                resultResponse)

            Return response
        End Function

        ''' <summary>
        ''' Add an address book contact to database.
        ''' </summary>
        ''' <param name="contactParams">The contact parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Created address book contact</returns>
        Public Function AddAddressBookContact(ByVal contactParams As AddressBookContact,
                                              <Out> ByRef resultResponse As ResultResponse) As AddressBookContact

            parseWithNewtonJson = True
            contactParams.PrepareForSerialization()

            Return GetJsonObjectFromAPI(Of AddressBookContact)(
                contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddNew,
                HttpMethodType.Post,
                resultResponse)

        End Function

        ''' <summary>
        ''' The request parameter for the multiple address book contacts creating process.
        ''' </summary>
        <DataContract>
        Public NotInheritable Class BatchCreatingAddressBookContactsRequest
            Inherits QueryTypes.GenericParameters

            ''' The array of the address IDs
            <DataMember(Name:="data", EmitDefaultValue:=False)>
            Public Property Data As AddressBookContact()
        End Class

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="contactParams"></param>
        ''' <param name="mandatoryNullableFields"></param>
        ''' <param name="resultResponse"></param>
        ''' <returns></returns>
        Public Function BatchCreateAdressBookContacts(ByVal contactParams As BatchCreatingAddressBookContactsRequest,
                                                      ByVal mandatoryNullableFields As String(),
                                                      <Out> ByRef resultResponse As ResultResponse) As StatusResponse

            Me.mandatoryFields = mandatoryNullableFields
            contactParams.PrepareForSerialization()

            Return GetJsonObjectFromAPI(Of StatusResponse)(
                contactParams,
                R4MEInfrastructureSettingsV5.ContactsAddMultiple,
                HttpMethodType.Post,
                resultResponse)
        End Function

#End Region

#Region "Account Profile"
        ''' <summary>
        ''' Get account profile
        ''' </summary>
        ''' <param name="failResponse">Error response</param>
        ''' <returns>Account profile</returns>
        Public Function GetAccountProfile(<Out> ByRef failResponse As ResultResponse) As AccountProfile
            Dim parameters = New QueryTypes.GenericParameters()

            Dim result = GetJsonObjectFromAPI(Of AccountProfile)(
                parameters,
                R4MEInfrastructureSettingsV5.AccountProfile,
                HttpMethodType.[Get],
                failResponse)

            Return result
        End Function

        ''' <summary>
        ''' Get prefered unit of the account woner
        ''' </summary>
        ''' <param name="failResponse">Error response</param>
        ''' <returns>Prefered unit ('mi', 'km')</returns>
        Public Function GetAccountPreferedUnit(<Out> ByRef failResponse As ResultResponse) As String
            Dim accountProfile = Me.GetAccountProfile(failResponse)
            Dim ownerId = accountProfile.RootMemberId

            Dim r4me = New Route4MeManager(Me.m_ApiKey)

            Dim memPars = New QueryTypes.MemberParametersV4() With {
                .member_id = ownerId
            }

            Dim errorString As String = Nothing
            Dim user = r4me.GetUserById(memPars, errorString)

            Dim prefUnit As String = user.preferred_units

            Return prefUnit
        End Function

#End Region


#Region "Routes"

        ''' <summary>
        ''' Get all Routes of the User filtered by specifying the corresponding query parameters.
        ''' </summary>
        ''' <param name="routeParameters">Route query parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An array of the routes</returns>
        Public Function GetRoutes(ByVal routeParameters As RouteParametersQuery, <Out> ByRef resultResponse As ResultResponse) As DataObjectRoute()
            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute())(routeParameters, R4MEInfrastructureSettingsV5.Routes, HttpMethodType.[Get], resultResponse)
            Return result
        End Function

        ''' <summary>
        ''' Get all Routes of the User filtered by specifying the corresponding query parameters.
        ''' </summary>
        ''' <param name="routeParameters">Route query parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An array of the routes</returns>
        Public Function GetAllRoutesWithPagination(ByVal routeParameters As RouteParametersQuery, <Out> ByRef resultResponse As ResultResponse) As DataObjectRoute()
            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute())(routeParameters, R4MEInfrastructureSettingsV5.RoutesPaginate, HttpMethodType.[Get], resultResponse)
            Return result
        End Function

        ''' <summary>
        ''' Get all Routes of the User filtered by specifying the corresponding query parameters (without using ElasticSearch).
        ''' </summary>
        ''' <param name="routeParameters">Route query parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An array of the routes</returns>
        Public Function GetPaginatedRouteListWithoutElasticSearch(ByVal routeParameters As RouteParametersQuery,
                                                                  <Out> ByRef resultResponse As ResultResponse) As DataObjectRoute()

            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute())(
                routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackPaginate,
                HttpMethodType.[Get],
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Get route list using Elastic Search.
        ''' </summary>
        ''' <param name="routeFilterParameters">Route filter parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An array of the routes</returns>
        Public Function GetRouteDataTableWithElasticSearch(ByVal routeFilterParameters As RouteFilterParameters,
                                                           <Out> ByRef resultResponse As ResultResponse) As DataObjectRoute()

            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute())(
                routeFilterParameters,
                R4MEInfrastructureSettingsV5.RoutesFallbackDatatable,
                HttpMethodType.Post,
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Get route list without using Elastic Search.
        ''' </summary>
        ''' <param name="routeParameters">Route filter parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An array of the routes</returns>
        Public Function GetRouteListWithoutElasticSearch(ByVal routeParameters As RouteParametersQuery,
                                                         <Out> ByRef resultResponse As ResultResponse) As DataObjectRoute()

            Dim result = GetJsonObjectFromAPI(Of DataObjectRoute())(
                routeParameters,
                R4MEInfrastructureSettingsV5.RoutesFallback,
                HttpMethodType.[Get],
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Duplicate multiple Routes by sending a body payload with the array of the corresponding Route IDs.
        ''' </summary>
        ''' <param name="routeIDs">An array of the route ID</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>A response with status code</returns>
        Public Function DuplicateRoute(ByVal routeIDs As String(), <Out> ByRef resultResponse As ResultResponse) As RouteDuplicateResponse

            Dim duplicateParameter = New Dictionary(Of String, String())() From {
                {"duplicate_routes_id", routeIDs}
            }

            Dim duplicateParameterJsonString = R4MeUtils.SerializeObjectToJson(duplicateParameter, True)
            Dim content = New StringContent(duplicateParameterJsonString, Encoding.UTF8, "application/json")

            Dim genParams = New RouteParametersQuery()
            Dim result = GetJsonObjectFromAPI(Of RouteDuplicateResponse)(
                genParams,
                R4MEInfrastructureSettingsV5.RoutesDuplicate,
                HttpMethodType.Post,
                content,
                resultResponse)

            Return result

        End Function


        ''' <summary>
        ''' Delete multiple Routes by sending a comma-delimited list of the corresponding Route IDs as a query string.
        ''' </summary>
        ''' <param name="routeIds">An array of the route IDs</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>A response with status code</returns>
        Public Function DeleteRoutes(ByVal routeIds As String(), <Out> ByRef resultResponse As ResultResponse) As RoutesDeleteResponse
            Dim str_route_ids As String = ""

            For Each routeId As String In routeIds
                If str_route_ids.Length > 0 Then str_route_ids += ","
                str_route_ids += routeId
            Next

            Dim genericParameters = New QueryTypes.GenericParameters()
            genericParameters.ParametersCollection.Add("route_id", str_route_ids)

            Dim response = GetJsonObjectFromAPI(Of RoutesDeleteResponse)(
                genericParameters, R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Delete,
                resultResponse)

            Return response
        End Function

        ''' <summary>
        ''' Get the datatable configuration.
        ''' </summary>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>A response with status code</returns>
        Public Function GetRouteDataTableConfig(<Out> ByRef resultResponse As ResultResponse) As RouteDataTableConfigResponse

            Dim genericParameters = New QueryTypes.GenericParameters()

            Dim result = GetJsonObjectFromAPI(Of RouteDataTableConfigResponse)(
                genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfig,
                HttpMethodType.[Get],
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Get a datatable fallback configuration request.
        ''' </summary>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>RouteDataTableConfigResponse type object</returns>
        Public Function GetRouteDataTableFallbackConfig(<Out> ByRef resultResponse As ResultResponse) As RouteDataTableConfigResponse

            Dim genericParameters = New QueryTypes.GenericParameters()

            Dim result = GetJsonObjectFromAPI(Of RouteDataTableConfigResponse)(
                genericParameters,
                R4MEInfrastructureSettingsV5.RoutesDatatableConfigFallback,
                HttpMethodType.[Get],
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' You can update a route in two ways:
        ''' 1. Modify existing route And put in this function the route object as parameter.
        ''' 2. Create an empty route object And assign values to the parameters:
        ''' - RouteID;
        ''' - Parameters (optional);
        ''' - Addresses (Optional).
        ''' </summary>
        ''' <param name="route">Route object</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Updated route</returns>
        <Obsolete("Will be finished after implementing Route Destinations API")>
        Public Function UpdateRoute(ByVal route As DataObjectRoute, <Out> ByRef resultResponse As ResultResponse) As DataObjectRoute

            Dim routeQueryParams = New RouteParametersQuery()

            routeQueryParams.RouteId = route.RouteID

            If route.Parameters IsNot Nothing Then routeQueryParams.Parameters = route.Parameters

            If route.Addresses IsNot Nothing AndAlso route.Addresses.Length > 0 Then routeQueryParams.Addresses = route.Addresses

            Dim response = GetJsonObjectFromAPI(Of DataObjectRoute)(
                routeQueryParams,
                R4MEInfrastructureSettingsV5.Routes,
                HttpMethodType.Put,
                resultResponse)

            Return response
        End Function

#End Region

#Region "Optimizations"

        ''' <summary>
        ''' Generates optimized routes
        ''' </summary>
        ''' <param name="optimizationParameters">The input parameters for the routes optimization, which encapsulates:
        ''' the route parameters And the addresses. </param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Created optimization</returns>
        Public Function RunOptimization(ByVal optimizationParameters As OptimizationParameters,
                                        <Out> ByRef resultResponse As ResultResponse) As DataObject

            Dim result = GetJsonObjectFromAPI(Of DataObject)(optimizationParameters, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Post, False, resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' The response returned by the remove optimization command
        ''' </summary>
        <DataContract>
        Private NotInheritable Class RemoveOptimizationResponse

            ''' <value>True if an optimization was removed successfuly </value>
            <DataMember(Name:="status")>
            Public Property Status As Boolean

            ''' <value>The number of the removed optimizations </value>
            <DataMember(Name:="removed")>
            Public Property Removed As Integer
        End Class

        ''' <summary>
        ''' The request parameters for an optimization removing
        ''' </summary>
        <DataContract()>
        Private NotInheritable Class RemoveOptimizationRequest
            Inherits QueryTypes.GenericParameters

            ''' <value>If true will be redirected</value>
            <QueryTypes.HttpQueryMemberAttribute(Name:="redirect", EmitDefaultValue:=False)>
            Public Property redirect As Integer

            ''' <value>The array of the optimization problem IDs to be removed</value>
            <DataMember(Name:="optimization_problem_ids", EmitDefaultValue:=False)>
            Public Property optimization_problem_ids As String()
        End Class

        ''' <summary>
        ''' Remove an existing optimization belonging to an user.
        ''' </summary>
        ''' <param name="optimizationProblemIDs">Optimization Problem ID</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Result status (true/false)</returns>
        Public Function RemoveOptimization(ByVal optimizationProblemIDs As String(),
                                           <Out> ByRef resultResponse As ResultResponse) As Boolean

            Dim remParameters = New RemoveOptimizationRequest() With {
                .redirect = 0,
                .optimization_problem_ids = optimizationProblemIDs
            }

            Dim response = GetJsonObjectFromAPI(Of RemoveOptimizationResponse)(
                remParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Delete,
                resultResponse)

            If response IsNot Nothing Then
                If response.Status AndAlso response.Removed > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Function

#End Region

#Region "Vehicles"

        ''' <summary>
        ''' Creates a vehicle
        ''' </summary>
        ''' <param name="vehicle">The VehicleV4Parameters type object as the request payload</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>The created vehicle</returns>
        Public Function CreateVehicle(ByVal vehicle As Vehicle,
                                      <Out> ByRef resultResponse As ResultResponse) As Vehicle

            Return GetJsonObjectFromAPI(Of Vehicle)(
                vehicle,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Post, resultResponse)

        End Function

        ''' <summary>
        ''' Returns the VehiclesPaginated type object containing an array of the vehicles
        ''' </summary>
        ''' <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>An array of the vehicles</returns>
        Public Function GetPaginatedVehiclesList(ByVal vehicleParams As VehicleParameters,
                                                 <Out> ByRef resultResponse As ResultResponse) As Vehicle()

            Return GetJsonObjectFromAPI(Of Vehicle())(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.[Get],
                resultResponse)

        End Function

        ''' <summary>
        ''' Removes a vehicle from a user's account
        ''' </summary>
        ''' <param name="vehicleId">The VehicleParameters type object as the query parameters containing parameter VehicleId</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>The removed vehicle</returns>
        Public Function DeleteVehicle(ByVal vehicleId As String,
                                      <Out> ByRef resultResponse As ResultResponse) As Vehicle

            If (If(vehicleId?.Length, 0)) <> 32 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The vehicle ID is not specified"}}
                    }
                }

                Return Nothing
            End If

            Return GetJsonObjectFromAPI(Of Vehicle)(
                New QueryTypes.GenericParameters(),
                R4MEInfrastructureSettings.Vehicle_V4 & "/" & vehicleId,
                HttpMethodType.Delete,
                resultResponse)

        End Function

        ''' <summary>
        ''' Creates temporary vehicle in the database.
        ''' </summary>
        ''' <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>A result with an order ID</returns>
        Public Function CreateTemporaryVehicle(ByVal vehParams As VehicleTemporary,
                                               <Out> ByRef resultResponse As ResultResponse) As VehicleTemporary

            Dim result = GetJsonObjectFromAPI(Of VehicleTemporary)(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleTemporary,
                HttpMethodType.Post,
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Execute a vehicle order
        ''' </summary>
        ''' <param name="vehOrderParams">Vehicle order parameters</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>Created vehicle order</returns>
        Public Function ExecuteVehicleOrder(ByVal vehOrderParams As VehicleOrderParameters,
                                            <Out> ByRef resultResponse As ResultResponse) As VehicleOrderResponse

            Dim result = GetJsonObjectFromAPI(Of VehicleOrderResponse)(
                vehOrderParams,
                R4MEInfrastructureSettingsV5.VehicleExecuteOrder,
                HttpMethodType.Post,
                resultResponse)

            Return result
        End Function

        ''' <summary>
        ''' Get latest vehicle locations by specified vehicle IDs.
        ''' </summary>
        ''' <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>Data with vehicles</returns>
        Public Function GetVehicleLocations(ByVal vehParams As VehicleParameters,
                                            <Out> ByRef resultResponse As ResultResponse) As VehicleLocationResponse

            Dim result = GetJsonObjectFromAPI(Of VehicleLocationResponse)(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Get the Vehicle by specifying vehicle ID.
        ''' </summary>
        ''' <param name="vehicleId">Vehicle ID</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Vehicle object</returns>
        Public Function GetVehicleById(ByVal vehicleId As String,
                                       <Out> ByRef resultResponse As ResultResponse) As Vehicle

            Dim result = GetJsonObjectFromAPI(Of Vehicle)(
                New VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles & "/" & vehicleId,
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Get the Vehicle track by specifying vehicle ID.
        ''' </summary>
        ''' <param name="vehicleId">Vehicle ID</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Vehicle track object</returns>
        Public Function GetVehicleTrack(ByVal vehicleId As String,
                                        <Out> ByRef resultResponse As ResultResponse) As VehicleTrackResponse

            Dim result = GetJsonObjectFromAPI(Of VehicleTrackResponse)(
                New VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles & "/" & vehicleId & "/" & "track",
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Get paginated list of the vehicle profiles.
        ''' </summary>
        ''' <param name="profileParams">Vehicle profile request parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>The data including list of the vehicle profiles.</returns>
        Public Function GetVehicleProfiles(ByVal profileParams As VehicleProfileParameters,
                                           <Out> ByRef resultResponse As ResultResponse) As VehicleProfilesResponse

            Dim result = GetJsonObjectFromAPI(Of VehicleProfilesResponse)(
                profileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Create a vehicle profile.
        ''' </summary>
        ''' <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Created vehicle profile</returns>
        Public Function CreateVehicleProfile(ByVal vehicleProfileParams As VehicleProfile,
                                             <Out> ByRef resultResponse As ResultResponse) As VehicleProfile

            Dim result = GetJsonObjectFromAPI(Of VehicleProfile)(
                vehicleProfileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Post,
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Remove a vehicle profile from database.
        ''' TO DO: adjust response Structure.
        ''' </summary>
        ''' <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Removed vehicle profile</returns>
        Public Function DeleteVehicleProfile(ByVal vehicleProfileParams As VehicleProfileParameters,
                                             <Out> ByRef resultResponse As ResultResponse) As Object

            If (If(vehicleProfileParams?.VehicleProfileId, 0)) < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The vehicle ID is not specified"}}
                    }
                }

                Return Nothing

            End If

            Dim result = GetJsonObjectFromAPI(Of Object)(
                New QueryTypes.GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles & "/" + vehicleProfileParams.VehicleProfileId.ToString(),
                HttpMethodType.Delete,
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Get vehicle profile by ID.
        ''' </summary>
        ''' <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Vehicle profile</returns>
        Public Function GetVehicleProfileById(ByVal vehicleProfileParams As VehicleProfileParameters,
                                              <Out> ByRef resultResponse As ResultResponse) As VehicleProfile

            If (If(vehicleProfileParams?.VehicleProfileId, 0)) < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The vehicle ID is not specified"}}
                    }
                }

                Return Nothing

            End If

            Dim result = GetJsonObjectFromAPI(Of VehicleProfile)(
                New VehicleParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles & "/" & vehicleProfileParams.VehicleProfileId.ToString(),
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Get vehicle by license plate.
        ''' </summary>
        ''' <param name="vehicleParams">Vehicle parameter containing vehicle license plate</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Vehicle</returns>
        Public Function GetVehicleByLicensePlate(ByVal vehicleParams As VehicleParameters,
                                                 <Out> ByRef resultResponse As ResultResponse) As VehicleResponse

            If (If(vehicleParams?.VehicleLicensePlate?.Length, 0)) < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The vehicle license plate is not specified"}}
                    }
                }

                Return Nothing

            End If

            Dim result = GetJsonObjectFromAPI(Of VehicleResponse)(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Search vehicles by sending request body.
        ''' </summary>
        ''' <param name="searchParams">Search parameters</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>An array of the found vehicles</returns>
        <ObsoleteAttribute("This method is deprecated until resolving the response issue.")>
        Public Function SearchVehicles(ByVal searchParams As VehicleSearchParameters,
                                       <Out> ByRef resultResponse As ResultResponse) As Vehicle()

            Dim result = GetJsonObjectFromAPI(Of Vehicle())(
                searchParams,
                R4MEInfrastructureSettingsV5.VehicleSearch,
                HttpMethodType.Post,
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Update a vehicle
        ''' </summary>
        ''' <param name="vehicleParams">Vehicle body parameters</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>Updated vehicle</returns>
        Public Function UpdateVehicle(ByVal vehicleParams As Vehicle,
                                      <Out> ByRef resultResponse As ResultResponse) As Vehicle

            If (If(vehicleParams?.VehicleId?.Length, 0)) <> 32 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The vehicle ID is not specified"}}
                    }
                }

                Return Nothing

            End If

            Dim updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, False)
            Dim content = New StringContent(updateBodyJsonString, Encoding.UTF8, "application/json")

            Dim genParams = New RouteParametersQuery()
            Dim result = GetJsonObjectFromAPI(Of Vehicle)(
                genParams,
                R4MEInfrastructureSettingsV5.Vehicles & "/" + vehicleParams.VehicleId,
                HttpMethodType.Patch,
                content,
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Update a vehicle profile.
        ''' </summary>
        ''' <param name="profileParams">Vehicle profile object as body payload</param>
        ''' <param name="resultResponse">Failing response</param>
        ''' <returns>Updated vehicle profile</returns>
        Public Function UpdateVehicleProfile(ByVal profileParams As VehicleProfile,
                                             <Out> ByRef resultResponse As ResultResponse) As VehicleProfile

            If (If(profileParams?.VehicleProfileId, 0)) < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The vehicle profile ID is not specified"}}
                    }
                }

                Return Nothing

            End If

            Dim updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, False)
            Dim content = New StringContent(updateBodyJsonString, Encoding.UTF8, "application/json")

            Dim genParams = New RouteParametersQuery()
            Dim result = GetJsonObjectFromAPI(Of VehicleProfile)(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles & "/" & profileParams.VehicleProfileId.ToString(),
                HttpMethodType.Patch,
                content,
                resultResponse)

            Return result

        End Function

#End Region

#Region "Telematics Platform"

#Region "Connection"

        ''' <summary>
        ''' Get all registered telematics connections.
        ''' </summary>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>An array of the Connection type objects</returns>
        Public Function GetTelematicsConnections(<Out> ByRef resultResponse As ResultResponse) As Connection()
            Dim result = GetJsonObjectFromAPI(Of Connection())(
                New QueryTypes.GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Get a telematics connection by specified access token
        ''' </summary>
        ''' <param name="connectionParams">The telematics query paramaters 
        ''' as ConnectionPaameters type object</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>A connection type object</returns>
        Public Function GetTelematicsConnectionByToken(ByVal connectionParams As ConnectionParameters,
                                                       <Out> ByRef resultResponse As ResultResponse) As Connection

            Dim result = GetJsonObjectFromAPI(Of Connection)(
                New QueryTypes.GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection & "/" + connectionParams.ConnectionToken,
                HttpMethodType.[Get],
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Register a telematics connection.
        ''' </summary>
        ''' <param name="connectionParams">The telematics query paramaters 
        ''' as ConnectionPaameters type object</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>A connection type object</returns>
        Public Function RegisterTelematicsConnection(ByVal connectionParams As ConnectionParameters,
                                                     <Out> ByRef resultResponse As ResultResponse) As Connection

            Dim result = GetJsonObjectFromAPI(Of Connection)(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection,
                HttpMethodType.Post,
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Delete telematics connection account by specified access token.
        ''' </summary>
        ''' <param name="connectionParams">The telematics query paramaters 
        ''' as ConnectionPaameters type object</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>Removed teleamtics connection</returns>
        Public Function DeleteTelematicsConnection(ByVal connectionParams As ConnectionParameters,
                                                   <Out> ByRef resultResponse As ResultResponse) As Connection

            If (If(connectionParams?.ConnectionToken, "")).Length < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The connection token is not specified"}}
                    }
                }

                Return Nothing

            End If

            Dim result = GetJsonObjectFromAPI(Of Connection)(
                New QueryTypes.GenericParameters(),
                R4MEInfrastructureSettingsV5.TelematicsConnection & "/" + connectionParams.ConnectionToken,
                HttpMethodType.Delete,
                resultResponse)

            Return result

        End Function

        ''' <summary>
        ''' Update telemetics connection's account
        ''' </summary>
        ''' <param name="connectionParams">The telematics query paramaters 
        ''' as ConnectionPaameters type object</param>
        ''' <param name="resultResponse">Error response</param>
        ''' <returns>Updated teleamtics connection</returns>
        Public Function UpdateTelematicsConnection(ByVal connectionParams As ConnectionParameters,
                                                   <Out> ByRef resultResponse As ResultResponse) As Connection

            If (If(connectionParams?.ConnectionToken, "")).Length < 1 Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The connection token is not specified"}}
                    }
                }

                Return Nothing

            End If

            Dim result = GetJsonObjectFromAPI(Of Connection)(
                connectionParams,
                R4MEInfrastructureSettingsV5.TelematicsConnection & "/" + connectionParams.ConnectionToken,
                HttpMethodType.Put,
                resultResponse)

            Return result

        End Function

#End Region

#End Region

        Public Function GetStringResponseFromAPI(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                ByVal url As String,
                                                ByVal httpMethod As HttpMethodType,
                                                <Out> ByRef resultResponse As ResultResponse) As String

            Dim result As String = GetJsonObjectFromAPI(Of String)(optimizationParameters, url, httpMethod, True, resultResponse)
            Return result

        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                           ByVal url As String, ByVal httpMethod As HttpMethodType,
                                                           <Out> ByRef resultResponse As ResultResponse) As T

            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, False, resultResponse)
            Return result

        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                            ByVal url As String,
                                                            ByVal httpMethod As HttpMethodType,
                                                            ByVal httpContent As HttpContent,
                                                            <Out> ByRef resultResponse As ResultResponse) As T

            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, httpContent, False, resultResponse)
            Return result

        End Function

        Private Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                             ByVal url As String,
                                                             ByVal httpMethod As HttpMethodType,
                                                             ByVal isString As Boolean,
                                                             <Out> ByRef resultResponse As ResultResponse) As T

            Dim result As T = GetJsonObjectFromAPI(Of T)(
                optimizationParameters,
                url,
                httpMethod,
                CType(Nothing, HttpContent),
                isString,
                resultResponse)

            Return result
        End Function

        Private Async Function GetJsonObjectFromAPIAsync(Of T As Class)(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                                        ByVal url As String,
                                                                        ByVal httpMethod As HttpMethodType,
                                                                        ByVal isString As Boolean) As Task(Of Tuple(Of T, ResultResponse))

            Return Await Task.Run(Function()
                                      Dim result As Task(Of Tuple(Of T, ResultResponse)) = GetJsonObjectFromAPIAsync(Of T)(
                                      optimizationParameters,
                                      url,
                                      httpMethod,
                                      CType(Nothing, HttpContent),
                                      isString)
                                      Return result

                                  End Function)
        End Function

        Private Async Function GetJsonObjectFromAPIAsync(Of T As Class)(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                                        ByVal url As String,
                                                                        ByVal _httpMethod As HttpMethodType,
                                                                        ByVal httpContent As HttpContent,
                                                                        ByVal isString As Boolean) As Task(Of Tuple(Of T, ResultResponse))
            Dim result As T = Nothing
            Dim resultResponse As ResultResponse = Nothing

            Try
                Using httpClient As HttpClient = CreateAsyncHttpClient(url)
                    Dim parametersURI As String = optimizationParameters.Serialize(m_ApiKey)

                    Select Case _httpMethod
                        Case HttpMethodType.[Get]
                            Dim response = Await httpClient.GetStreamAsync(parametersURI)
                            result = If(isString, TryCast(response.ReadString(), T), response.ReadObject(Of T)())
                            Exit Select
                        Case HttpMethodType.Post, HttpMethodType.Put, HttpMethodType.Patch, HttpMethodType.Delete
                            Dim isPut As Boolean = _httpMethod = HttpMethodType.Put
                            Dim isPatch As Boolean = _httpMethod = HttpMethodType.Patch
                            Dim isDelete As Boolean = _httpMethod = HttpMethodType.Delete
                            Dim content As HttpContent = Nothing

                            If httpContent IsNot Nothing Then
                                content = httpContent
                            Else
                                Dim jsonString As String = If(
                                    (If(Me.mandatoryFields?.Length, 0)) > 0,
                                    R4MeUtils.SerializeObjectToJson(optimizationParameters, Me.mandatoryFields),
                                    R4MeUtils.SerializeObjectToJson(optimizationParameters))

                                content = New StringContent(jsonString)

                                content.Headers.ContentType = New MediaTypeHeaderValue("application/json")
                            End If

                            Dim response As HttpResponseMessage = Nothing

                            If isPut Then
                                response = Await httpClient.PutAsync(parametersURI, content)
                            ElseIf isPatch Then
                                content.Headers.ContentType = New MediaTypeHeaderValue("application/json-patch+json")
                                response = Await httpClient.PatchAsync(parametersURI, content)
                            ElseIf isDelete Then
                                Dim request = New HttpRequestMessage With {
                                    .Content = content,
                                    .Method = HttpMethod.Delete,
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
                            ElseIf response.Content.[GetType]().ToString().ToLower().Contains("httpconnectionresponsecontent") Then
                                Dim streamTask2 = response.Content.ReadAsStreamAsync()
                                streamTask2.Wait()

                                If streamTask2.IsCompleted Then
                                    Dim content2 As HttpContent = response.Content

                                    If isString Then
                                        result = TryCast(content2.ReadAsStreamAsync().Result.ReadString(), T)
                                    Else
                                        result = If(parseWithNewtonJson, content2.ReadAsStreamAsync().Result.ReadObjectNew(Of T)(), content2.ReadAsStreamAsync().Result.ReadObject(Of T)())
                                        parseWithNewtonJson = False
                                    End If
                                End If
                            Else
                                Dim streamTask = Await (CType(response.Content, StreamContent)).ReadAsStreamAsync()
                                Dim errorMessageContent As Task(Of String) = Nothing
                                If response.Content.[GetType]() <> GetType(StreamContent) Then errorMessageContent = response.Content.ReadAsStringAsync()

                                Try
                                    resultResponse = streamTask.ReadObject(Of ResultResponse)()
                                Catch
                                    resultResponse = Nothing
                                End Try

                                If resultResponse IsNot Nothing AndAlso resultResponse.Messages IsNot Nothing AndAlso resultResponse.Messages.Count > 0 Then
                                ElseIf errorMessageContent IsNot Nothing Then
                                    resultResponse = New ResultResponse() With {
                                        .Status = False,
                                        .Messages = New Dictionary(Of String, String())() From {
                                            {"ErrorMessageContent", New String() {errorMessageContent.Result}}
                                        }
                                    }
                                Else
                                    Dim responseStream = Await response.Content.ReadAsStringAsync()
                                    Dim responseString As String = responseStream
                                    resultResponse = New ResultResponse() With {
                                        .Status = False,
                                        .Messages = New Dictionary(Of String, String())() From {
                                            {"Response", New String() {responseString}}
                                        }
                                    }
                                End If
                            End If

                            Exit Select
                    End Select
                End Using

            Catch e As HttpListenerException
                resultResponse = New ResultResponse() With {
                    .Status = False
                }

                If e.Message IsNot Nothing Then
                    resultResponse.Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {e.Message}}
                    }
                End If

                If (If(e.InnerException?.Message, Nothing)) IsNot Nothing Then
                    If resultResponse.Messages Is Nothing Then
                        resultResponse.Messages = New Dictionary(Of String, String())()
                    End If

                    resultResponse.Messages.Add("Error", New String() {e.InnerException.Message})
                End If

                result = Nothing
            Catch e As Exception
                resultResponse = New ResultResponse() With {
                    .Status = False
                }

                If e.Message IsNot Nothing Then
                    resultResponse.Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {e.Message}}
                    }
                End If

                If (If(e.InnerException?.Message, Nothing)) IsNot Nothing Then
                    If resultResponse.Messages Is Nothing Then resultResponse.Messages = New Dictionary(Of String, String())()
                    resultResponse.Messages.Add("InnerException Error", New String() {e.InnerException.Message})
                End If

                result = Nothing
            End Try

            Return New Tuple(Of T, ResultResponse)(result, resultResponse)
        End Function

        Private Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                            ByVal url As String,
                                                            ByVal _httpMethod As HttpMethodType,
                                                            ByVal httpContent As HttpContent,
                                                            ByVal isString As Boolean,
                                                            <Out> ByRef resultResponse As ResultResponse) As T
            Dim result As T = Nothing
            resultResponse = Nothing

            Try

                Using httpClient As HttpClient = CreateHttpClient(url)
                    Dim parametersURI As String = optimizationParameters.Serialize(m_ApiKey)

                    Select Case _httpMethod
                        Case HttpMethodType.[Get]
                            Dim response = httpClient.GetStreamAsync(parametersURI)
                            response.Wait()

                            If response.IsCompleted Then

                                If isString Then
                                    result = TryCast(response.Result.ReadString(), T)
                                Else
                                    result = If(parseWithNewtonJson,
                                                response.Result.ReadObjectNew(Of T)(),
                                                response.Result.ReadObject(Of T)())

                                    parseWithNewtonJson = False
                                End If
                            End If

                            Exit Select
                        Case HttpMethodType.Post, HttpMethodType.Put, HttpMethodType.Patch, HttpMethodType.Delete
                            Dim isPut As Boolean = _httpMethod = HttpMethodType.Put
                            Dim isPatch As Boolean = _httpMethod = HttpMethodType.Patch
                            Dim isDelete As Boolean = _httpMethod = HttpMethodType.Delete
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
                            ElseIf isPatch Then
                                content.Headers.ContentType = New MediaTypeHeaderValue("application/json-patch+json")
                                response = httpClient.PatchAsync(parametersURI, content)
                            ElseIf isDelete Then
                                Dim request As HttpRequestMessage = New HttpRequestMessage With {
                                    .Content = content,
                                    .Method = HttpMethod.Delete,
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

                                    If isString Then
                                        result = TryCast(streamTask.Result.ReadString(), T)
                                    Else
                                        result = If(parseWithNewtonJson, streamTask.Result.ReadObjectNew(Of T)(), streamTask.Result.ReadObject(Of T)())
                                        parseWithNewtonJson = False
                                    End If
                                End If
                            ElseIf response.IsCompleted AndAlso response.Result.IsSuccessStatusCode AndAlso response.Result.Content.[GetType]().ToString().ToLower().Contains("httpconnectionresponsecontent") Then
                                Dim streamTask2 = response.Result.Content.ReadAsStreamAsync()
                                streamTask2.Wait()

                                If streamTask2.IsCompleted Then
                                    Dim content2 As HttpContent = response.Result.Content

                                    If isString Then
                                        result = TryCast(content2.ReadAsStreamAsync().Result.ReadString(), T)
                                    Else
                                        result = If(parseWithNewtonJson, content2.ReadAsStreamAsync().Result.ReadObjectNew(Of T)(), content2.ReadAsStreamAsync().Result.ReadObject(Of T)())
                                        parseWithNewtonJson = False
                                    End If
                                End If
                            Else
                                Dim streamTask As Task(Of Stream) = Nothing
                                Dim errorMessageContent As Task(Of String) = Nothing

                                If response.Result.Content.[GetType]() = GetType(StreamContent) Then
                                    streamTask = (CType(response.Result.Content, StreamContent)).ReadAsStreamAsync()
                                Else
                                    errorMessageContent = response.Result.Content.ReadAsStringAsync()
                                End If

                                streamTask?.Wait()
                                errorMessageContent?.Wait()

                                Try
                                    resultResponse = streamTask.Result.ReadObject(Of ResultResponse)()
                                Catch
                                    resultResponse = Nothing
                                End Try

                                If resultResponse IsNot Nothing AndAlso resultResponse.Messages IsNot Nothing AndAlso resultResponse.Messages.Count > 0 Then
                                ElseIf errorMessageContent IsNot Nothing Then
                                    resultResponse = New ResultResponse() With {
                                        .Status = False,
                                        .Messages = New Dictionary(Of String, String())() From {
                                            {"ErrorMessageContent", New String() {errorMessageContent.Result}}
                                        }
                                    }
                                Else
                                    Dim responseStream = response.Result.Content.ReadAsStringAsync()
                                    responseStream.Wait()
                                    Dim responseString As String = responseStream.Result
                                    resultResponse = New ResultResponse() With {
                                        .Status = False,
                                        .Messages = New Dictionary(Of String, String())() From {
                                            {"Response", New String() {responseString}}
                                        }
                                    }
                                End If
                            End If

                            Exit Select
                    End Select
                End Using

            Catch e As HttpListenerException
                resultResponse = New ResultResponse() With {
                    .Status = False
                }

                If e.Message IsNot Nothing Then
                    resultResponse.Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {e.Message}}
                    }
                End If

                If (If(e.InnerException?.Message, Nothing)) IsNot Nothing Then
                    If resultResponse.Messages Is Nothing Then
                        resultResponse.Messages = New Dictionary(Of String, String())()
                    End If

                    resultResponse.Messages.Add("Error", New String() {e.InnerException.Message})
                End If

                result = Nothing
            Catch e As Exception
                resultResponse = New ResultResponse() With {
                    .Status = False
                }

                If e.Message IsNot Nothing Then
                    resultResponse.Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {e.Message}}
                    }
                End If

                If (If(e.InnerException?.Message, Nothing)) IsNot Nothing Then
                    If resultResponse.Messages Is Nothing Then
                        resultResponse.Messages = New Dictionary(Of String, String())()
                    End If

                    resultResponse.Messages.Add("InnerException Error", New String() {e.InnerException.Message})
                End If

                result = Nothing
            End Try

            Return result
        End Function

        Private Function GetXmlObjectFromAPI(Of T As Class)(ByVal optimizationParameters As QueryTypes.GenericParameters,
                                                            ByVal url As String,
                                                            ByVal httpMethod__1 As HttpMethodType,
                                                            ByVal httpContent As HttpContent,
                                                            ByVal isString As Boolean,
                                                            <Out> ByRef resultResponse As ResultResponse) As String

            Dim result As String = String.Empty
            resultResponse = Nothing

            Try

                Using httpClient As HttpClient = CreateHttpClient(url)
                    Dim parametersURI As String = optimizationParameters.Serialize(m_ApiKey)

                    Select Case httpMethod__1
                        Case HttpMethodType.[Get]

                            If True Then
                                Dim response = httpClient.GetStreamAsync(parametersURI)
                                response.Wait()

                                If response.IsCompleted Then
                                    result = If(isString,
                                                TryCast(response.Result.ReadString(), String),
                                                response.Result.ReadObject(Of String)())
                                End If
                            End If

                        Case HttpMethodType.Post

                            If True Then
                                Dim response = httpClient.GetStreamAsync(parametersURI)
                                response.Wait()

                                If response.IsCompleted Then
                                    result = If(isString,
                                                TryCast(response.Result.ReadString(), String),
                                                response.Result.ReadObject(Of String)())
                                End If
                            End If

                        Case HttpMethodType.Put, HttpMethodType.Patch, HttpMethodType.Delete

                            If True Then
                                Dim isPut As Boolean = httpMethod__1 = HttpMethodType.Put
                                Dim isPatch As Boolean = httpMethod__1 = HttpMethodType.Patch
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
                                ElseIf isPatch Then
                                    content.Headers.ContentType = New MediaTypeHeaderValue("application/json-patch+json")
                                    response = httpClient.PatchAsync(parametersURI, content)
                                ElseIf isDelete Then
                                    Dim request As HttpRequestMessage = New HttpRequestMessage With {
                                        .Content = content,
                                        .Method = HttpMethod.Delete,
                                        .RequestUri = New Uri(parametersURI, UriKind.Relative)
                                    }
                                    response = httpClient.SendAsync(request)
                                Else
                                    response = httpClient.PostAsync(parametersURI, content)
                                End If

                                response.Wait()

                                If response.IsCompleted AndAlso response.Result.IsSuccessStatusCode AndAlso TypeOf response.Result.Content Is StreamContent Then
                                    Dim streamTask = (CType(response.Result.Content, StreamContent)).ReadAsStreamAsync()
                                    streamTask.Wait()

                                    If streamTask.IsCompleted Then
                                        result = streamTask.Result.ReadString()
                                    End If
                                Else
                                    Dim streamTask = (CType(response.Result.Content, StreamContent)).ReadAsStreamAsync()
                                    streamTask.Wait()

                                    Dim errorResponse As DataTypes.ErrorResponse = Nothing
                                    Dim errorMessageContent As Task(Of String) = If(response.Result.Content.[GetType]() <> GetType(StreamContent),
                                                                                    response.Result.Content.ReadAsStringAsync(),
                                                                                    Nothing)

                                    Try
                                        resultResponse = streamTask.Result.ReadObject(Of ResultResponse)()
                                    Catch
                                        resultResponse = Nothing
                                    End Try

                                    If errorResponse IsNot Nothing AndAlso errorResponse.Errors IsNot Nothing AndAlso errorResponse.Errors.Count > 0 Then
                                    ElseIf errorMessageContent IsNot Nothing Then
                                        resultResponse = New ResultResponse() With {
                                            .Status = False,
                                            .Messages = New Dictionary(Of String, String())() From {
                                                {"ErrorMessageContent", New String() {errorMessageContent.Result}}
                                            }
                                        }
                                    Else
                                        Dim responseStream = response.Result.Content.ReadAsStringAsync()
                                        responseStream.Wait()
                                        Dim responseString As String = responseStream.Result
                                        resultResponse = New ResultResponse() With {
                                            .Status = False,
                                            .Messages = New Dictionary(Of String, String())() From {
                                                {"Response", New String() {responseString}}
                                            }
                                        }
                                    End If
                                End If
                            End If
                    End Select
                End Using

            Catch e As HttpListenerException
                resultResponse = New ResultResponse() With {
                    .Status = False
                }

                If e.Message IsNot Nothing Then
                    resultResponse.Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {e.Message}}
                    }
                End If

                If (If(e.InnerException?.Message, Nothing)) IsNot Nothing Then
                    If resultResponse.Messages Is Nothing Then
                        resultResponse.Messages = New Dictionary(Of String, String())()
                    End If

                    resultResponse.Messages.Add("Error", New String() {e.InnerException.Message})
                End If

                result = Nothing
            Catch e As Exception
                resultResponse = New ResultResponse() With {
                    .Status = False
                }

                If e.Message IsNot Nothing Then
                    resultResponse.Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {e.Message}}
                    }
                End If

                If (If(e.InnerException?.Message, Nothing)) IsNot Nothing Then
                    If resultResponse.Messages Is Nothing Then
                        resultResponse.Messages = New Dictionary(Of String, String())()
                    End If

                    resultResponse.Messages.Add("InnerException Error", New String() {e.InnerException.Message})
                End If

                result = Nothing
            End Try

            Return result
        End Function

        Private Function CreateHttpClient(ByVal url As String) As HttpClient
            ServicePointManager.SecurityProtocol = CType(768, SecurityProtocolType) Or CType(3072, SecurityProtocolType) Or CType(12288, SecurityProtocolType)

            Dim result As HttpClient = New HttpClient() With {
                .BaseAddress = New Uri(url)
            }

            result.Timeout = m_DefaultTimeOut
            result.DefaultRequestHeaders.Accept.Clear()
            result.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Return result
        End Function

        Private Function CreateAsyncHttpClient(ByVal url As String) As HttpClient
            Dim handler = New HttpClientHandler() With {
                .AllowAutoRedirect = False
            }

            Dim supprotsAutoRdirect = handler.SupportsAutomaticDecompression
            ServicePointManager.SecurityProtocol = CType(768, SecurityProtocolType) Or CType(3072, SecurityProtocolType) Or CType(12288, SecurityProtocolType)

            Dim result As HttpClient = New HttpClient(handler) With {
                .BaseAddress = New Uri(url)
            }

            result.Timeout = m_DefaultTimeOut
            result.DefaultRequestHeaders.Accept.Clear()
            result.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Return result
        End Function

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace

