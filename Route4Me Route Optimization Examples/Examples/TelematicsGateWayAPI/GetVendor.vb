Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub GetVendor()
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim errorString As String = Nothing
            Dim vendors = route4Me.GetAllTelematicsVendors(New TelematicsVendorParameters(), errorString)

            Dim randomNumber As Integer = (New Random()).[Next](0, vendors.Vendors.Length - 1)

            Dim randomVendorID As String = vendors.Vendors(randomNumber).ID

            Dim vendorParameters = New TelematicsVendorParameters() With {
                .vendorID = Convert.ToUInt32(randomVendorID)
            }

            Dim vendor = route4Me.GetTelematicsVendor(vendorParameters, errorString)

            PrintExampleTelematicsVendor(vendor, errorString)
        End Sub
    End Class
End Namespace