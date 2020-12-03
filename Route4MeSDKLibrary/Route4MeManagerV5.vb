Imports Route4MeSDK.DataTypes.V5
Imports Route4MeSDK.QueryTypes.V5
Imports Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.DataTypes
Imports System
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
Imports Route4MeSDK.DataTypes
Imports System.Runtime.InteropServices
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.System.Net.Http
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDK
    Public NotInheritable Class Route4MeManagerV5
        Private ReadOnly m_ApiKey As String
        Private ReadOnly m_DefaultTimeOut As TimeSpan = New TimeSpan(TimeSpan.TicksPerMinute * 30)
        Private parseWithNewtonJson As Boolean

        Public Sub New(ByVal apiKey As String)
            m_ApiKey = apiKey
            parseWithNewtonJson = False
        End Sub

        <DataContract()>
        Public NotInheritable Class MemberQueryParameters
            Inherits GenericParameters

            <HttpQueryMemberAttribute(Name:="user_id", EmitDefaultValue:=False)>
            Public Property UserId As String
        End Class

        <DataContract>
        Private NotInheritable Class BulkMembersRequest
            Inherits GenericParameters

            <DataMember(Name:="users")>
            Public Property Users As TeamRequest()
        End Class

        Public Function GetTeamMembers(<Out> ByRef failResponse As ResultResponse) As TeamResponse()
            Dim parameters = New GenericParameters()
            Dim result = GetJsonObjectFromAPI(Of TeamResponse())(parameters, R4MEInfrastructureSettingsV5.TeamUsers, HttpMethodType.[Get], failResponse)
            Return result
        End Function

        Public Function GetTeamMemberById(ByVal parameters As MemberQueryParameters, <Out> ByRef resultResponse As ResultResponse) As TeamResponse
            If (If(parameters?.UserId, Nothing)) Is Nothing Then
                resultResponse = New ResultResponse() With {
                    .Status = False,
                    .Messages = New Dictionary(Of String, String())() From {
                        {"Error", New String() {"The UserId parameter is not specified"}}
                    }
                }
                Return Nothing
            End If

            Dim result = GetJsonObjectFromAPI(Of TeamResponse)(parameters, R4MEInfrastructureSettingsV5.TeamUsers & "/" & parameters.UserId, HttpMethodType.[Get], resultResponse)
            Return result
        End Function

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

            Return GetJsonObjectFromAPI(Of TeamResponse)(memberParams, R4MEInfrastructureSettingsV5.TeamUsers, HttpMethodType.Post, resultResponse)
        End Function

        Public Function BulkCreateTeamMembers(ByVal membersParams As TeamRequest(), <Out> ByRef resultResponse As ResultResponse) As ResultResponse
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
            Dim result = GetJsonObjectFromAPI(Of ResultResponse)(newMemberParams, R4MEInfrastructureSettingsV5.TeamUsersBulkCreate, HttpMethodType.Post, resultResponse)
            Return result
        End Function

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

            Dim response = GetJsonObjectFromAPI(Of TeamResponse)(parameters, R4MEInfrastructureSettingsV5.TeamUsers & "/" & parameters.UserId, HttpMethodType.Delete, resultResponse)
            Return response
        End Function

        Public Function UpdateTeamMember(ByVal queryParameters As MemberQueryParameters, ByVal requestPayload As TeamRequest, <Out> ByRef resultResponse As ResultResponse) As TeamResponse
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

            Dim response = GetJsonObjectFromAPI(Of TeamResponse)(requestPayload, R4MEInfrastructureSettingsV5.TeamUsers & "/" & queryParameters.UserId, HttpMethodType.Patch, resultResponse)
            Return response
        End Function

        Public Function AddSkillsToDriver(ByVal queryParameters As MemberQueryParameters, ByVal skills As String(), <Out> ByRef resultResponse As ResultResponse) As TeamResponse
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
            Dim response = GetJsonObjectFromAPI(Of TeamResponse)(requestPayload, R4MEInfrastructureSettingsV5.TeamUsers & "/" & queryParameters.UserId, HttpMethodType.Patch, resultResponse)
            Return response
        End Function

        Public Function GetDriverReviewList(ByVal parameters As DriverReviewParameters, <Out> ByRef resultResponse As ResultResponse) As DriverReviewsResponse
            parseWithNewtonJson = True
            Dim result = GetJsonObjectFromAPI(Of DriverReviewsResponse)(parameters, R4MEInfrastructureSettingsV5.DriverReview, HttpMethodType.[Get], resultResponse)
            Return result
        End Function

        Public Function GetDriverReviewById(ByVal parameters As DriverReviewParameters, <Out> ByRef resultResponse As ResultResponse) As DriverReview
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
            Dim result = GetJsonObjectFromAPI(Of DriverReview)(parameters, R4MEInfrastructureSettingsV5.DriverReview & "/" + parameters.RatingId, HttpMethodType.[Get], resultResponse)
            Return result
        End Function

        Public Function CreateDriverReview(ByVal driverReview As DriverReview, <Out> ByRef resultResponse As ResultResponse) As DriverReview
            Return GetJsonObjectFromAPI(Of DriverReview)(driverReview, R4MEInfrastructureSettingsV5.DriverReview, HttpMethodType.Post, resultResponse)
        End Function

        Public Function UpdateDriverReview(ByVal driverReview As DriverReview, ByVal method As HttpMethodType, <Out> ByRef resultResponse As ResultResponse) As DriverReview
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

            Return GetJsonObjectFromAPI(Of DriverReview)(driverReview, R4MEInfrastructureSettingsV5.DriverReview & "/" + driverReview.RatingId, method, resultResponse)
        End Function

        Public Function GetStringResponseFromAPI(ByVal optimizationParameters As GenericParameters, ByVal url As String, ByVal httpMethod As HttpMethodType, <Out> ByRef resultResponse As ResultResponse) As String
            Dim result As String = GetJsonObjectFromAPI(Of String)(optimizationParameters, url, httpMethod, True, resultResponse)
            Return result
        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As GenericParameters, ByVal url As String, ByVal httpMethod As HttpMethodType, <Out> ByRef resultResponse As ResultResponse) As T
            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, False, resultResponse)
            Return result
        End Function

        Public Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As GenericParameters, ByVal url As String, ByVal httpMethod As HttpMethodType, ByVal httpContent As HttpContent, <Out> ByRef resultResponse As ResultResponse) As T
            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, httpContent, False, resultResponse)
            Return result
        End Function

        Private Function GetJsonObjectFromAPI(Of T As Class)(ByVal optimizationParameters As GenericParameters, ByVal url As String, ByVal httpMethod As HttpMethodType, ByVal isString As Boolean, <Out> ByRef resultResponse As ResultResponse) As T
            Dim result As T = GetJsonObjectFromAPI(Of T)(optimizationParameters, url, httpMethod, CType(Nothing, HttpContent), isString, resultResponse)
            Return result
        End Function

        Private Async Function GetJsonObjectFromAPIAsync(Of T As Class)(ByVal optimizationParameters As GenericParameters, ByVal url As String, ByVal httpMethod As HttpMethodType, ByVal isString As Boolean) As Task(Of Tuple(Of T, ResultResponse))
            Return Await Task.Run(Function()
                                      Dim result As Task(Of Tuple(Of T, ResultResponse)) = GetJsonObjectFromAPIAsync(Of T)(optimizationParameters, url, httpMethod, CType(Nothing, HttpContent), isString)
                                      Return result
                                  End Function)
        End Function

        Private Async Function GetJsonObjectFromAPIAsync(Of T As Class)(ByVal optimizationParameters As GenericParameters, ByVal url As String, ByVal _httpMethod As HttpMethodType, ByVal httpContent As HttpContent, ByVal isString As Boolean) As Task(Of Tuple(Of T, ResultResponse))
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
                                Dim jsonString As String = R4MeUtils.SerializeObjectToJson(optimizationParameters)
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

        Private Function GetJsonObjectFromAPI(Of T As Class)(
                                                            ByVal optimizationParameters As GenericParameters,
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
                                    result = If(parseWithNewtonJson, response.Result.ReadObjectNew(Of T)(), response.Result.ReadObject(Of T)())
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

        Private Function GetXmlObjectFromAPI(Of T As Class)(ByVal optimizationParameters As GenericParameters, ByVal url As String, ByVal httpMethod__1 As HttpMethodType, ByVal httpContent As HttpContent, ByVal isString As Boolean, <Out> ByRef resultResponse As ResultResponse) As String
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
                                    result = If(isString, TryCast(response.Result.ReadString(), String), response.Result.ReadObject(Of String)())
                                End If
                            End If

                        Case HttpMethodType.Post

                            If True Then
                                Dim response = httpClient.GetStreamAsync(parametersURI)
                                response.Wait()

                                If response.IsCompleted Then
                                    result = If(isString, TryCast(response.Result.ReadString(), String), response.Result.ReadObject(Of String)())
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
                                    Dim errorResponse As ErrorResponse = Nothing
                                    Dim errorMessageContent As Task(Of String) = If(response.Result.Content.[GetType]() <> GetType(StreamContent), CSharpImpl.__Assign(errorMessageContent, response.Result.Content.ReadAsStringAsync()), Nothing)

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

