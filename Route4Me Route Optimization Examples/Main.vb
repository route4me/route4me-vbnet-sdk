Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System
Imports System.Collections.Generic
Namespace Route4MeSDKTest
    Public Module Main
        Public Sub Main()
            Dim examples = New Route4MeSDKTest.Examples.Route4MeExamples()

            ' ======== Get Route's Directions ===========================
            Dim RouteId As String = "5C15E83A4BE005BCD1537955D28D51D7"

            examples.GetRouteDirections(RouteId)
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
