Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
#Region "Types"

        ' Inherit from GenericParameters and add any JSON serializable content
        ' Marked with attribute [DataMember]
        <DataContract> _
        Private Class MyAddressAndParametersHolder
            Inherits GenericParameters

            <DataMember>
            Public Property addresses As Address()

            ' Using the defined class "Address", can use user-defined class instead
            <DataMember>
            Public Property parameters As RouteParameters

            ' Using the defined "RouteParameters", can use user-defined class instead
        End Class

        ' Generic class for returned JSON holder
        <DataContract> _
        Private Class MyDataObjectGeneric
            <DataMember(Name:="optimization_problem_id")>
            Public Property OptimizationProblemId As String

            <DataMember(Name:="state")>
            Public Property MyState As Integer

            <DataMember(Name:="addresses")>
            Public Property Addresses As Address()

            ' Using the defined class "Address", can use user-defined class instead
            <DataMember(Name:="parameters")>
            Public Property Parameters As RouteParameters

            ' Using the defined "RouteParameters", can use user-defined class instead
        End Class

#End Region

#Region "Methods"

        ''' <summary>
        ''' This example demonstares how to use the API in a generic way, Not bounded to the proposed Route4MeManager shortcucts
        ''' For the same functionality using shortcuts check the Route4MeExamples.SingleDriverRoundTrip()
        ''' </summary>
        Public Sub SingleDriverRoundTripGeneric()
            Const uri As String = R4MEInfrastructureSettings.MainHost + "/api.v4/optimization_problem.php"

            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

#Region "Prepare the addresses"
            ' Using the defined class, can use user-defined class instead

            Dim myApiKey As String = DemoApiKey

            Dim addresses = New Address() {New Address() With {
                .AddressString = "754 5th Ave New York, NY 10019",
                .[Alias] = "Bergdorf Goodman",
                .IsDepot = True,
                .Latitude = 40.7636197,
                .Longitude = -73.9744388,
                .Time = 0
            }, New Address() With {
                .AddressString = "717 5th Ave New York, NY 10022",
                .[Alias] = "Giorgio Armani",
                .Latitude = 40.7669692,
                .Longitude = -73.9693864,
                .Time = 0
            }, New Address() With {
                .AddressString = "888 Madison Ave New York, NY 10014",
                .[Alias] = "Ralph Lauren Women's and Home",
                .Latitude = 40.7715154,
                .Longitude = -73.9669241,
                .Time = 0
            }, New Address() With {
                .AddressString = "1011 Madison Ave New York, NY 10075",
                .[Alias] = "Yigal Azrou'l",
                .Latitude = 40.7772129,
                .Longitude = -73.9669,
                .Time = 0
            }, New Address() With {
                .AddressString = "440 Columbus Ave New York, NY 10024",
                .[Alias] = "Frank Stella Clothier",
                .Latitude = 40.7808364,
                .Longitude = -73.9732729,
                .Time = 0
            }, New Address() With {
                .AddressString = "324 Columbus Ave #1 New York, NY 10023",
                .[Alias] = "Liana",
                .Latitude = 40.7803123,
                .Longitude = -73.9793079,
                .Time = 0
            }, New Address() With {
                .AddressString = "110 W End Ave New York, NY 10023",
                .[Alias] = "Toga Bike Shop",
                .Latitude = 40.7753077,
                .Longitude = -73.9861529,
                .Time = 0
            }, New Address() With {
                .AddressString = "555 W 57th St New York, NY 10019",
                .[Alias] = "BMW of Manhattan",
                .Latitude = 40.7718005,
                .Longitude = -73.9897716,
                .Time = 0
            }, New Address() With {
                .AddressString = "57 W 57th St New York, NY 10019",
                .[Alias] = "Verizon Wireless",
                .Latitude = 40.7558695,
                .Longitude = -73.9862019,
                .Time = 0
            }}

#End Region

            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Round Trip",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = 1,
                .VehicleMaxDistanceMI = 10000,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription()
            }

            Dim myParameters = New MyAddressAndParametersHolder() With {
                .addresses = addresses,
                .parameters = parameters
            }

            Dim errorString As String = Nothing
            Dim dataObject As MyDataObjectGeneric = route4Me.GetJsonObjectFromAPI(
                Of MyDataObjectGeneric)(myParameters, uri, HttpMethodType.Post, errorString
            )

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("SingleDriverRoundTripGeneric executed successfully")
                Console.WriteLine("")
                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("State: {0}", dataObject.MyState)
                Console.WriteLine("")

                dataObject.Addresses.ToList().ForEach(
                    Sub(address)
                        Console.WriteLine("Address: {0}", address.AddressString)
                        Console.WriteLine("Route ID: {0}", address.RouteId)
                    End Sub
                )
            Else
                Console.WriteLine("SingleDriverRoundTripGeneric error {0}", errorString)
            End If

            RemoveTestOptimizations()
        End Sub

#End Region
    End Class
End Namespace
