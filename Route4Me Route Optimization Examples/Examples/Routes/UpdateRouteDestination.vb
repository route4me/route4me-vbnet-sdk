Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub UpdateRouteDestination()
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager("11111111111111111111111111111111")

            ' The example refers to the process of updating many parameters simultaneously of the route destination by sending the HTTP parameters.

            Dim routeId As String = "824CA521E3A8DE9F1C684C8BAE90CF07"
            Dim routeDestionationId As Integer = 217393034

            Dim CustomData As New Dictionary(Of String, String)()
            CustomData.Add("BatchId", "e7c672b1-a356-4a97-803e-97db88fdcf99")
            CustomData.Add("CustomerNumber", "2718500")
            CustomData.Add("DeliveryId", "2c71f6d9-c1aa-4672-a682-3e9f12badac9")
            CustomData.Add("DeliveryInvoices", "<?xml version=""1.0"" encoding=""utf-16""?>" & vbCr & vbLf & "<!DOCTYPE EXAMPLEDelivery SYSTEM ""EXAMPLEDelivery.dtd"">" & vbCr & vbLf & "<ArrayOfRouteDeliveryInvoice>" & vbCr & vbLf & "  <RouteDeliveryInvoice>" & vbCr & vbLf & "    <InvoiceNumber>999999</InvoiceNumber>" & vbCr & vbLf & "    <InventoryIds>" & vbCr & vbLf & "      <string>1790908</string>" & vbCr & vbLf & "    </InventoryIds>" & vbCr & vbLf & "    <IsRA>false</IsRA>" & vbCr & vbLf & "    <IsDT>false</IsDT>" & vbCr & vbLf & "    <IsINC>true</IsINC>" & vbCr & vbLf & "    <IsPO>false</IsPO>" & vbCr & vbLf & "    <IsPOPickup>false</IsPOPickup>" & vbCr & vbLf & "  </RouteDeliveryInvoice>" & vbCr & vbLf & "</ArrayOfRouteDeliveryInvoice>")
            CustomData.Add("DeliveryNotes", "")
            CustomData.Add("RouteId", "20191")

            ' Run the query
            Dim routeParameters As New RouteParametersQuery() With { _
                .RouteId = routeId _
            }

            Dim errorString As String = ""
            Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

            For Each oAddress As Address In dataObject.Addresses
                If oAddress.RouteDestinationId = routeDestionationId Then
                    Dim routeParams As New RouteParametersQuery() With { _
                        .RouteId = routeId, _
                        .RouteDestinationId = oAddress.ContactId _
                    }
                    oAddress.[Alias] = "Steele's - MONTICELLO"
                    oAddress.Cost = 5
                    oAddress.InvoiceNo = 945825
                    ' etc fill the necessary address parameters

                    oAddress.CustomFields = R4MeUtils.ToObject(Of Dictionary(Of String, Object))(CustomData, errorString)

                    errorString = ""
                        Dim address As Address = route4Me.UpdateRouteDestination(oAddress, errorString)

                        Console.WriteLine("")

                        If address IsNot Nothing Then
                            Console.WriteLine("UpdateRouteDestination executed successfully")
                            Console.WriteLine("Alias {0}", address.[Alias])
                            Console.WriteLine("Cost {0}", address.Cost)
                            Console.WriteLine("InvoiceNo {0}", address.InvoiceNo)
                        For Each kvpair As KeyValuePair(Of String, Object) In address.CustomFields
                            Console.WriteLine(kvpair.Key & ": " & kvpair.Value)
                        Next
                    Else
                            Console.WriteLine("UpdateRouteDestination error {0}", errorString)
                        End If
                    End If
            Next
        End Sub
    End Class
End Namespace
