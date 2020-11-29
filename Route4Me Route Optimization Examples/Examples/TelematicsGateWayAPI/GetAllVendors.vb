﻿Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of getting all telematics vendors.
        ''' </summary>
        Public Sub GetAllVendors()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            Dim vendorParameters = New TelematicsVendorParameters()

            ' Run the query
            Dim errorString As String = Nothing
            Dim vendors = route4Me.GetAllTelematicsVendors(vendorParameters, errorString)

            PrintExampleTelematicsVendor(vendors, errorString)
        End Sub
    End Class
End Namespace
