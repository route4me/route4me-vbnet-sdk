﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Example refers to the process of searching for the specified text 
        ''' throughout all routes names belonging to the user's account.
        ''' </summary>
        Public Sub SearchRoutesForText()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops("Nonstandard route name")

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeParameters = New RouteParametersQuery() With {
                .Query = "Nonstandard"
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim dataObjects As DataObjectRoute() = route4Me.
                GetRoutes(routeParameters, errorString)

            PrintExampleRouteResult(dataObjects, errorString)

            RemoveTestOptimizations()

        End Sub
    End Class
End Namespace