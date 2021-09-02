Imports System.IO
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example referes to the process of creating an optimization
        ''' with pickup/dropof and joint addresses.
        ''' </summary>
        Public Sub SingleDepotPickupDropoffJoint()
            ' Note: use an API key with permission for pickup/dropoff operations.
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim jsonFile As String = AppDomain.CurrentDomain.BaseDirectory & "\Data\JSON\pickupdropoff_request.json"

            Dim r As StreamReader = New StreamReader(jsonFile)
            Dim jsonString As String = r.ReadToEnd()

            Dim routeParamsFromJson = R4MeUtils.ReadObjectNew(Of DataObjectRoute)(jsonString)

            Dim optParams = New OptimizationParameters() With {
                .Parameters = routeParamsFromJson.Parameters,
                .Addresses = routeParamsFromJson.Addresses,
                .Depots = If(routeParamsFromJson.Addresses.Where(Function(x) x.IsDepot = True)?.ToArray(), Nothing)
            }

            Dim errorString As String = Nothing
            Dim dataObject = route4Me.RunOptimization(optParams, errorString)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            If dataObject Is Nothing AndAlso dataObject.[GetType]() <> GetType(DataObject) Then
                Console.WriteLine("SingleDepotPickupDropoff failed" & Environment.NewLine & "Cannot create the optimization. " + Environment.NewLine & errorString)
                Return
            End If

            If (If(dataObject?.Routes?.Length, 0)) < 1 Then
                Console.WriteLine("The optimization doesn't contain route")
                RemoveTestOptimizations()
                Return
            End If

            Dim routeId = dataObject.Routes(0).RouteID

            If (If(routeId?.Length, 0)) < 32 Then
                Console.WriteLine("The route ID is not valid")
                RemoveTestOptimizations()
                Return
            End If

            Dim routeQueryParameters = New RouteParametersQuery() With {
                .RouteId = routeId
            }

            Dim routePickDrop = route4Me.GetRoute(routeQueryParameters, errorString)

            PrintExampleRouteResult(routePickDrop, errorString)
            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace