Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetDriverReviewList()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim queryParameters = New DriverReviewParameters() With {
                .Start = "2020-01-01",
                .[End] = "2030-01-01",
                .Page = 0,
                .PerPage = 20
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim reviewList = route4Me.GetDriverReviewList(queryParameters, resultResponse)

            PrintDriverReview(reviewList, resultResponse)
        End Sub
    End Class
End Namespace
