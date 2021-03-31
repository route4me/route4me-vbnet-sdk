Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub UpdateDriverReview()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim existingReview = GetLastDriverReview()

            Dim driverReview = New DriverReview() With {
                .RatingId = existingReview.RatingId,
                .TrackingNumber = existingReview.TrackingNumber,
                .Rating = existingReview.Rating,
                .Review = "Updated Review"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim updatedDriverReview = route4Me.UpdateDriverReview(
                driverReview,
                HttpMethodType.Patch,
                resultResponse)

            PrintDriverReview(updatedDriverReview, resultResponse)
        End Sub
    End Class
End Namespace
