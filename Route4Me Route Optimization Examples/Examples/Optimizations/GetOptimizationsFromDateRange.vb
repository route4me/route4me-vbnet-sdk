Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' Get optimizations scheduled to date from the specified date range.
        ''' </summary>
        Public Sub GetOptimizationsFromDateRange()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim today As DateTime = DateTime.Now
            Dim days3 As TimeSpan = New TimeSpan(3, 0, 0, 0)

            Dim queryParameters = New RouteParametersQuery() With {
                .StartDate = (today - days3).ToString("yyyy-MM-dd"),
                .EndDate = today.ToString("yyyy-MM-dd")
            }

            Dim errorString As String = Nothing
            Dim dataObjects = route4Me.GetOptimizations(queryParameters, errorString)

            PrintExampleOptimizationResult(dataObjects, errorString)
        End Sub
    End Class
End Namespace
