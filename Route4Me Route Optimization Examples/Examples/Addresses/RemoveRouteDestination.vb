﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub RemoveRouteDestination(routeId As String, destinationId As Integer)
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Run the query
            Dim errorString As String = ""
            Dim deleted As Boolean = route4Me.RemoveRouteDestination(routeId, destinationId, errorString)

            Console.WriteLine("")

            If deleted Then
                Console.WriteLine("RemoveRouteDestination executed successfully")

                Console.WriteLine("Destination ID: {0}", destinationId)
            Else
                Console.WriteLine("RemoveRouteDestination error: {0}", errorString)
            End If

        End Sub
    End Class
End Namespace