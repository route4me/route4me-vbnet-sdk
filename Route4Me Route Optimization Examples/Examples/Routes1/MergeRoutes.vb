﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    ''' <summary>
    ''' Update Route Custom Data
    ''' </summary>
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub MergeRoutes(params As Dictionary(Of String, String))
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObjectRoute = route4Me.MergeRoutes(params, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("UpdateRouteCustomData executed successfully")

                Console.WriteLine("Route ID: {0}", dataObject.RouteID)
                Console.WriteLine("Route state: {0}", dataObject.State)
            Else
                Console.WriteLine("UpdateRouteCustomData error: {0}", errorString)
            End If
        End Sub
    End Class
End Namespace
