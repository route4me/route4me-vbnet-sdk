Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub CreateDriverReview()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim newDriverReview = New DriverReview() With {
                .TrackingNumber = "NDRK0M1V",
                .Rating = 4,
                .Review = "Test Review"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim driverReview = route4Me.CreateDriverReview(newDriverReview, resultResponse)

            PrintDriverReview(driverReview, resultResponse)
        End Sub
    End Class
End Namespace