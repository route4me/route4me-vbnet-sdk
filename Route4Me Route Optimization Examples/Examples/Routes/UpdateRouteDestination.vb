Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of updating many parameters 
        ''' simultaneously of the route destination by sending the HTTP parameters.
        ''' </summary>
        Public Sub UpdateRouteDestination()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            RunOptimizationSingleDriverRoute10Stops()

            OptimizationsToRemove = New List(Of String)() From {
                SD10Stops_optimization_problem_id
            }

            Dim routeDestionationId As Integer = CInt(SD10Stops_route.Addresses(1).RouteDestinationId)

            Dim CustomData = New Dictionary(Of String, String)()
            CustomData.Add("BatchId", "e7c672b1-a356-4a97-803e-97db88fdcf99")
            CustomData.Add("CustomerNumber", "2718500")
            CustomData.Add("DeliveryId", "2c71f6d9-c1aa-4672-a682-3e9f12badac9")
            CustomData.Add(
                "DeliveryInvoices",
                "<?xml version=""1.0"" encoding=""utf-16""?>" & vbCrLf & "<!DOCTYPE EXAMPLEDelivery SYSTEM ""EXAMPLEDelivery.dtd"">" & vbCrLf & "<ArrayOfRouteDeliveryInvoice>" & vbCrLf & "  <RouteDeliveryInvoice>" & vbCrLf & "    <InvoiceNumber>999999</InvoiceNumber>" & vbCrLf & "    <InventoryIds>" & vbCrLf & "      <string>1790908</string>" & vbCrLf & "    </InventoryIds>" & vbCrLf & "    <IsRA>false</IsRA>" & vbCrLf & "    <IsDT>false</IsDT>" & vbCrLf & "    <IsINC>true</IsINC>" & vbCrLf & "    <IsPO>false</IsPO>" & vbCrLf & "    <IsPOPickup>false</IsPOPickup>" & vbCrLf & "  </RouteDeliveryInvoice>" & vbCrLf & "</ArrayOfRouteDeliveryInvoice>"
                )
            CustomData.Add("DeliveryNotes", "")
            CustomData.Add("RouteId", "20191")

            Dim routeParameters = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id
            }

            Dim errorString As String = Nothing
            Dim dataObject As DataObjectRoute = route4Me.GetRoute(routeParameters, errorString)

            Dim oAddress As Address = dataObject.Addresses.
                    Where(Function(x) x.RouteDestinationId = routeDestionationId).
                    FirstOrDefault()

            Dim routeParams = New RouteParametersQuery() With {
                .RouteId = SD10Stops_route_id,
                .RouteDestinationId = oAddress.ContactId
            }

            oAddress.[Alias] = "Steele's - MONTICELLO"
            oAddress.Cost = 5
            oAddress.InvoiceNo = "945825"
            oAddress.CustomFields = New Dictionary(Of String, String) From {
                {"Test Custom Fields", "Test custom Data"}
            }

            ' Run the query
            errorString = ""
            Dim address As Address = route4Me.UpdateRouteDestination(oAddress, errorString)

            Console.WriteLine("")

            If address IsNot Nothing Then
                Console.WriteLine("UpdateRouteDestination executed successfully")
                Console.WriteLine("Alias {0}", address.[Alias])
                Console.WriteLine("Cost {0}", address.Cost)
                Console.WriteLine("InvoiceNo {0}", address.InvoiceNo)

                For Each kvpair As KeyValuePair(Of String, String) In address.CustomFields
                    Console.WriteLine(kvpair.Key & ": " & kvpair.Value)
                Next
            Else
                Console.WriteLine("UpdateRouteDestination error {0}", errorString)
            End If

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
