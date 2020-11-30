Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of searching by query text the user location.
        ''' </summary>
        Public Sub QueryUserLocations()
            Dim route4Me = New Route4MeManager(ActualApiKey)

#Region "Retrieve All User Locations"

            Dim genericParameters = New GenericParameters()
            Dim errorString As String = Nothing
            Dim userLocations = route4Me.GetUserLocations(genericParameters, errorString)

            If userLocations Is Nothing Then
                Console.WriteLine("Cannot retrieve all user locations. " & errorString)
                Return
            End If

#End Region

#Region "Get First User's Email"

            Dim userLocation = userLocations.Where(Function(x) x.UserTracking IsNot Nothing).FirstOrDefault()
            Dim email As String = userLocation.MemberData.MemberEmail

#End Region

            ' Run query
            genericParameters.ParametersCollection.Add("query", email)
            Dim queriedUserLocations = route4Me.GetUserLocations(genericParameters, errorString)

            Console.WriteLine(If(
                              queriedUserLocations IsNot Nothing,
                              "QueryUserLocations executed successfully",
                              "QueryUserLocations failed"))

        End Sub
    End Class
End Namespace