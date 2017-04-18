Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    ''' <summary>
    ''' Update Route Custom Data
    ''' </summary>
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub MergeRoutes(params As MergeRoutesQuery)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim result As Boolean = route4Me.MergeRoutes(params, errorString)

            Console.WriteLine("")

            If result Then
                Console.WriteLine("MergeRoutes executed successfully")
            Else
                Console.WriteLine("MergeRoutes error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
