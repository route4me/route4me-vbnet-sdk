Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System
Imports System.Collections.Generic
Namespace Route4MeSDKTest
    Public Module Main
        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()

            ' ======== MErge Routes ===========================
            Dim RouteIds As String = "0E0F64689F772586042D0F3F4BFBEFA2,9E8D60B196743D6872E9D899E1BDE753"
            Dim DepotAddress As String = "455 S 4th St, Louisville, KY 40202"
            Dim RemoveOrigin As String = "False"
            Dim Latitude As String = "38.251698"
            Dim Longitude As String = "85.757308'"

            Dim params As New Dictionary(Of String, String)
            params.Add("route_ids", RouteIds)
            params.Add("depot_address", DepotAddress)
            params.Add("remove_origin", RemoveOrigin)
            params.Add("depot_lat", Latitude)
            params.Add("depot_lng", Longitude)
            examples.MergeRoutes(params)
            '======================================================================

            ' ======== Resequence Route Destination ===========================
            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'Dim RouteDestinationId = 174405640

            'Dim CustomData As New Dictionary(Of String, String)
            'CustomData.Add("animal", "tiger")
            'CustomData.Add("bird", "canary")
            'examples.UpdateRouteCustomData(RouteId, RouteDestinationId, CustomData)
            '======================================================================

            ' ======== Resequence Route Destination ===========================
            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'Dim email As String = "oleg.guchi@gmail.com"
            'examples.RouteSharing(RouteId, email)
            '======================================================================

            ' ======== Resequence Route Destination ===========================
            'Dim RouteId As String = "CA902292134DBC134EAF8363426BD247"
            'examples.ResequenceReoptimizeRoute(RouteId)
            '======================================================================

            ' ======== Resequence Route Destination ===========================
            'Dim RouteId As String = "F0C842829D8799067F9BF7A495076335"
            'Dim RouteDestinationId = 174389214
            'examples.ResequenceRouteDestination(RouteId, RouteDestinationId)
            '======================================================================

            ' ======== Get Route's Directions ===========================
            'Dim RouteId As String = "5C15E83A4BE005BCD1537955D28D51D7"

            'examples.GetRouteDirections(RouteId)
            '======================================================================

            ' ======== Get Route's Path Points ===========================
            'Dim RouteId As String = "5C15E83A4BE005BCD1537955D28D51D7"

            'examples.GetRoutePathPoints(RouteId)
            '======================================================================

            ' ======== Insert Address Into Route's Optimal Position ===========================
            'Dim RouteId As String = "5C15E83A4BE005BCD1537955D28D51D7"

            'examples.InsertAddressIntoRouteOptimzalPostion(RouteId)
            '======================================================================

            ' ======== Remove Existing Optimization ===========================
            'Dim OptimizationProblemIds As String() = {"D2EAF1916DCC33A903BBCBF23364980D"}

            'examples.RemoveOptimization(OptimizationProblemIds)
            '======================================================================

            ' ======== Remove Address From Optimization ===========================
            'Dim OptimizationProblemId As String = "F678E11289BBEA44D5BEC41BB22B3FB4"
            'Dim RouteDestinationId As Integer = 174785127

            'examples.RemoveAddressFromOptimization(OptimizationProblemId, RouteDestinationId)
            '======================================================================
            Console.WriteLine("Press any key")
            Console.ReadKey()
        End Sub
    End Module
End Namespace
