Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' This example demonstares how to use the API in a generic way, not bounded to the proposed Route4MeManager shortcucts
        ''' For the same functionality using shortcuts check the Route4MeExamples.GenericExampleShortcut()
        ''' </summary>
        Public Sub GenericExample()
            Const uri As String = R4MEInfrastructureSettings.MainHost + "/api.v4/route.php"

            'the api key of the account
            'the api key must have hierarchical ownership of the route being viewed (api key can't view routes of others)
            Const myApiKey As String = "11111111111111111111111111111111"

            Dim route4Me As New Route4MeManager(myApiKey)

            Dim genericParameters As New GenericParameters()

            'number of records per page
            genericParameters.ParametersCollection.Add("limit", "10")

            'the page offset starting at zero
            genericParameters.ParametersCollection.Add("Offset", "5")

            Dim errorMessage As String
            Dim dataObjects As DataObjectRoute() = route4Me.GetJsonObjectFromAPI(Of DataObjectRoute())(genericParameters, uri, HttpMethodType.[Get], errorMessage)

            Console.WriteLine("")

            If dataObjects IsNot Nothing Then
                Console.WriteLine("GenericExample executed successfully, {0} routes returned", dataObjects.Length)
                Console.WriteLine("")

                For Each dataObject As DataObjectRoute In dataObjects
                    Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                    Console.WriteLine("RouteID: {0}", dataObject.RouteID)
                    Console.WriteLine("")
                Next
            Else
                Console.WriteLine("GenericExample error {0}", errorMessage)
            End If
        End Sub

        Public Sub GenericExampleShortcut()
            Dim route4Me As New Route4MeManager(c_ApiKey)

            Dim routeQueryParameters As New RouteParametersQuery() With { _
                .Limit = 10, _
                .Offset = 5 _
            }

            Dim errorMessage As String = ""
            Dim dataObjects As DataObjectRoute() = route4Me.GetRoutes(routeQueryParameters, errorMessage)

            If dataObjects IsNot Nothing Then
                Console.WriteLine("GenericExampleShortcut executed successfully, {0} routes returned", dataObjects.Length)
                Console.WriteLine("")

                For Each dataObject As DataObjectRoute In dataObjects
                    Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                    Console.WriteLine("RouteID: {0}", dataObject.RouteID)
                    Console.WriteLine("")
                Next
            Else
                Console.WriteLine("GenericExampleShortcut error {0}", errorMessage)
            End If
        End Sub
    End Class
End Namespace