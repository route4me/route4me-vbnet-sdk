Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of getting all user locations.
        ''' </summary>
        Public Sub GetAllUserLocations()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim genericParameters = New GenericParameters()

            Dim errorString As String = Nothing
            Dim userLocations = route4Me.GetUserLocations(genericParameters, errorString)

            If userLocations IsNot Nothing AndAlso userLocations.[GetType]() = GetType(UserLocation()) Then
                Console.WriteLine("GetAllUserLocations excuted successfully")

                If userLocations.Length > 0 Then

                    For Each userLocation In userLocations
                        Console.WriteLine(
                            "The member: {0} {1}",
                            userLocation.MemberData.MemberFirstName,
                            userLocation.MemberData.MemberLastName
                        )
                        Console.WriteLine(
                            "Location: {0}, {1}",
                            If(userLocation?.UserTracking?.PositionLatitude, Nothing),
                            If(userLocation?.UserTracking?.PositionLongitude, Nothing)
                        )
                    Next
                End If
            Else
                Console.WriteLine("GetAllUserLocations failed")
            End If
        End Sub
    End Class
End Namespace