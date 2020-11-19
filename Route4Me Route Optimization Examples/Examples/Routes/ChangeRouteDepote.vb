Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Change a route depot.
        ''' </summary>
        Public Sub ChangeRouteDepote()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunSingleDriverRoundTrip()

            OptimizationsToRemove = New List(Of String)() From {
                SDRT_optimization_problem_id
            }

            Dim routeId As String = SDRT_route_id
            Dim initialRoute = R4MeUtils.ObjectDeepClone(Of DataObjectRoute)(SDRT_route)

            SDRT_route.Addresses(0).IsDepot = False
            Dim addressId0 As Integer? = SDRT_route.Addresses(0).RouteDestinationId
            SDRT_route.Addresses(0).[Alias] = addressId0.ToString()
            initialRoute.Addresses(0).[Alias] = addressId0.ToString()

            SDRT_route.Addresses(1).IsDepot = True
            Dim addressId1 As Integer? = SDRT_route.Addresses(1).RouteDestinationId
            SDRT_route.Addresses(1).[Alias] = addressId1.ToString()
            initialRoute.Addresses(1).[Alias] = addressId1.ToString()

            Dim errorString As String = Nothing
            Dim dataObject = route4Me.UpdateRoute(SDRT_route, initialRoute, errorString)

            PrintExampleRouteResult(dataObject, errorString)

            Dim address0 = dataObject.Addresses.
                Where(Function(x) x.[Alias] = addressId0.ToString()).
                FirstOrDefault()

            PrintExampleDestination(address0)

            Dim address1 = dataObject.Addresses.
                Where(Function(x) x.[Alias] = addressId1.ToString()).
                FirstOrDefault()

            PrintExampleDestination(address1)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace