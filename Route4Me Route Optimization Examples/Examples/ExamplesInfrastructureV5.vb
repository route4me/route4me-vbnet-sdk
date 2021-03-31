Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManagerV5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public membersToRemove As List(Of TeamResponse) = New List(Of TeamResponse)()
        Private Const testPassword As String = "pSw1_2_3_4@"
        Private testRatingId As String

        Private Function GetTestEmail() As String
            Return "test" & DateTime.Now.ToString("yyMMddHHmmss") & "+evgenysoloshenko@gmail.com"
        End Function

        Private Sub PrintTeamMembers(ByVal result As Object, ByVal resultResponse As ResultResponse)
            Console.WriteLine("")

            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            If result IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")

                If result.[GetType]() = GetType(TeamResponse) Then

                    Dim member = CType(result, TeamResponse)
                    Console.WriteLine(
                        "Member: {0}",
                        member.MemberFirstName & " " + member.MemberLastName)

                ElseIf result.[GetType]() = GetType(TeamResponse()) Then
                    Dim members = CType(result, TeamResponse())

                    For Each member In members
                        Console.WriteLine(
                            "Member: {0}",
                            member.MemberFirstName & " " + member.MemberLastName)
                    Next
                Else
                    Console.WriteLine(testName & ": unknown response type")
                End If
            Else
                PrintFailResponse(resultResponse, testName)
            End If
        End Sub

        Private Sub PrintFailResponse(ByVal resultResponse As ResultResponse, ByVal testName As String)
            Console.WriteLine(testName & " failed:")
            Console.WriteLine("Status: {0}", If(resultResponse?.Status.ToString(), ""))
            Console.WriteLine("Status code: {0}", If(resultResponse?.Code.ToString(), ""))
            Console.WriteLine("Exit code: {0}", If(resultResponse?.ExitCode.ToString(), ""))

            If (If(resultResponse?.Messages?.Count, 0)) > 0 Then

                For Each message In resultResponse.Messages

                    If message.Key IsNot Nothing AndAlso (If(message.Value?.Length, 0)) > 0 Then
                        Console.WriteLine("")
                        Console.WriteLine((If(message.Key, "")) & ":")

                        For Each msg In message.Value
                            Console.WriteLine("    " & msg)
                        Next
                    End If
                Next
            End If
        End Sub

        Private Function GetRandomTeamMember() As TeamResponse
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim errorResponse As ResultResponse = Nothing

            Dim members = route4Me.GetTeamMembers(errorResponse)

            If members Is Nothing Then Return Nothing

            Dim rnd As Random = New Random()
            Dim i As Integer = rnd.[Next](0, members.Count() - 1)

            Return members(i)
        End Function

        Private Function GetOwnerMemberId() As Integer?
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim errorResponse As ResultResponse = Nothing

            Dim members = route4Me.GetTeamMembers(errorResponse)
            Dim memberParams = New TeamRequest()
            Dim ownerMemberId = If(members.Where(Function(x) memberParams.GetMemberType(x.MemberType) = MemberTypes.AccountOwner).FirstOrDefault()?.MemberId, Nothing)

            If ownerMemberId Is Nothing Then
                Console.WriteLine("Cannot retrieve the team owner - cannot create a member.")
            End If

            Return ownerMemberId
        End Function

        Private Sub CreateTestTeamMember()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)
            Dim ownerId As Integer? = GetOwnerMemberId()

            If ownerId Is Nothing Then Return

            Dim newMemberParameters = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "John",
                .MemberLastName = "Doe",
                .MemberCompany = "Test Member To Remove",
                .MemberEmail = GetTestEmail(),
                .OwnerMemberId = CInt(ownerId)
            }

            newMemberParameters.SetMemberType(MemberTypes.Driver)

            Dim resultResponse As ResultResponse = Nothing
            Dim member As TeamResponse = route4Me.CreateTeamMember(newMemberParameters, resultResponse)

            If member IsNot Nothing AndAlso member.[GetType]() = GetType(TeamResponse) Then membersToRemove.Add(member)
        End Sub

        Private Sub RemoveTestTeamMembers()
            If (If(membersToRemove?.Count, 0)) < 1 Then Return

            Dim route4Me = New Route4MeManagerV5(ActualApiKey)
            Dim resultResponse As ResultResponse = Nothing

            For Each member In membersToRemove
                Dim memberParams = New MemberQueryParameters() With {
                    .UserId = member.MemberId.ToString()
                }

                Dim removedMember = route4Me.RemoveTeamMember(memberParams, resultResponse)

                Console.WriteLine(
                    If(If(removedMember?.MemberEmail?.Contains(".deleted"), False),
                        String.Format("A test member {0} removed succsessfully", removedMember.MemberId),
                        String.Format("Cannot remove a test member {0}", removedMember.MemberId)))
            Next
        End Sub

        Private Sub PrintDriverReview(ByVal result As Object, ByVal resultResponse As ResultResponse)
            Console.WriteLine("")

            Dim testName As String = (New StackTrace()).GetFrame(1).GetMethod().Name
            testName = If(testName IsNot Nothing, testName, "")

            If result IsNot Nothing Then
                Console.WriteLine(testName & " executed successfully")

                If result.[GetType]() = GetType(DriverReview) Then
                    Dim review = CType(result, DriverReview)
                    Console.WriteLine(
                        "Tracking number: {0}, Rating: {1}, Review: {2}",
                        review.TrackingNumber, review.Rating, review.Review)
                ElseIf result.[GetType]() = GetType(DriverReviewsResponse) Then
                    Dim reviewResponse = CType(result, DriverReviewsResponse)

                    For Each review In reviewResponse.Data
                        Console.WriteLine(
                            "Tracking number: {0}, Rating: {1}, Review: {2}",
                            review.TrackingNumber, review.Rating, review.Review)
                    Next
                Else
                    Console.WriteLine("Unexcepted response type")
                End If
            Else
                PrintFailResponse(resultResponse, testName)
            End If
        End Sub

        Private Sub CreateTestDriverReview()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim newDriverReview = New DriverReview() With {
                .TrackingNumber = "NDRK0M1V",
                .Rating = 4,
                .Review = "Test Review"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim driverReview = route4Me.CreateDriverReview(newDriverReview, resultResponse)

            PrintDriverReview(driverReview, resultResponse)

            testRatingId = If(driverReview?.RatingId, Nothing)
        End Sub

        Private Function GetLastDriverReview() As DriverReview
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim queryParameters = New DriverReviewParameters() With {
                .Page = 0,
                .PerPage = 20
            }


            Dim resultResponse As ResultResponse = Nothing
            Dim reviewList = route4Me.GetDriverReviewList(queryParameters, resultResponse)

            Return If((If(reviewList?.Data?.Length, 0)) > 0, reviewList.Data(reviewList.Data.Length - 1), Nothing)
        End Function
    End Class
End Namespace
