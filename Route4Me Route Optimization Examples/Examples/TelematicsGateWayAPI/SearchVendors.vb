Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of searching by query text. 
        ''' </summary>
        Public Sub SearchVendors()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim vendorParameters = New TelematicsVendorParameters() With {
                .isIntegrated = 1,
                .Search = "Fleet",
                .Page = 1,
                .perPage = 15
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim vendors = route4Me.SearchTelematicsVendors(vendorParameters, errorString)

            PrintExampleTelematicsVendor(vendors, errorString)
        End Sub
    End Class
End Namespace
