Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetDriverReviewById()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim allQueryParameters = New DriverReviewParameters() With {
                .Page = 0,
                .PerPage = 2
            }
            Dim resultResponse As ResultResponse = Nothing
            Dim reviews = route4Me.GetDriverReviewList(allQueryParameters, resultResponse)

            If (If(reviews?.Data?.Length, 0)) < 1 Then
                Console.WriteLine("Cannot retrive driver reviews")
                Return
            End If

            Dim queryParameters = New DriverReviewParameters() With {
                .RatingId = reviews.Data(0).RatingId
            }
            Dim review = route4Me.GetDriverReviewById(queryParameters, resultResponse)

            PrintDriverReview(review, resultResponse)
        End Sub
    End Class
End Namespace
