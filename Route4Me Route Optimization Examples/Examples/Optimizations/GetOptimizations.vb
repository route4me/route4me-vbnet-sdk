﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get limited number of the optimizations.
        ''' </summary>
        Public Sub GetOptimizations()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim queryParameters = New RouteParametersQuery() With {
                .Limit = 10,
                .Offset = 5
            }

            Dim errorString As String = Nothing
            Dim dataObjects As DataObject() = route4Me.GetOptimizations(queryParameters, errorString)

            PrintExampleOptimizationResult(dataObjects, errorString)
        End Sub
    End Class
End Namespace
