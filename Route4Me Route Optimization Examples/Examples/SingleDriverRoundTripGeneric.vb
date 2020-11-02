' See video tutorial here: http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter1:subchapter1

Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System
Imports System.Runtime.Serialization
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
#Region "Types"

        ' Inherit from GenericParameters and add any JSON serializable content
        ' Marked with attribute [DataMember]
        <DataContract> _
        Private Class MyAddressAndParametersHolder
            Inherits GenericParameters
            <DataMember> _
            Public Property addresses() As Address()
                Get
                    Return m_addresses
                End Get
                Set(value As Address())
                    m_addresses = Value
                End Set
            End Property
            Private m_addresses As Address()
            ' Using the defined class "Address", can use user-defined class instead
            <DataMember> _
            Public Property parameters() As RouteParameters
                Get
                    Return m_parameters
                End Get
                Set(value As RouteParameters)
                    m_parameters = Value
                End Set
            End Property
            Private m_parameters As RouteParameters
            ' Using the defined "RouteParameters", can use user-defined class instead
        End Class

        ' Generic class for returned JSON holder
        <DataContract> _
        Private Class MyDataObjectGeneric
            <DataMember(Name:="optimization_problem_id")> _
            Public Property OptimizationProblemId() As String
                Get
                    Return m_OptimizationProblemId
                End Get
                Set(value As String)
                    m_OptimizationProblemId = Value
                End Set
            End Property
            Private m_OptimizationProblemId As String

            <DataMember(Name:="state")> _
            Public Property MyState() As Integer
                Get
                    Return m_MyState
                End Get
                Set(value As Integer)
                    m_MyState = Value
                End Set
            End Property
            Private m_MyState As Integer

            <DataMember(Name:="addresses")> _
            Public Property Addresses() As Address()
                Get
                    Return m_Addresses
                End Get
                Set(value As Address())
                    m_Addresses = Value
                End Set
            End Property
            Private m_Addresses As Address()
            ' Using the defined class "Address", can use user-defined class instead
            <DataMember(Name:="parameters")> _
            Public Property Parameters() As RouteParameters
                Get
                    Return m_Parameters
                End Get
                Set(value As RouteParameters)
                    m_Parameters = Value
                End Set
            End Property
            Private m_Parameters As RouteParameters
            ' Using the defined "RouteParameters", can use user-defined class instead
        End Class

#End Region

#Region "Methods"

        Public Function SingleDriverRoundTripGeneric() As String
            Const uri As String = R4MEInfrastructureSettings.MainHost + "/api.v4/optimization_problem.php"

            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Prepare the addresses
            ' Using the defined class, can use user-defined class instead
            '#Region "Addresses"

            '#End Region
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "754 5th Ave New York, NY 10019", _
                .[Alias] = "Bergdorf Goodman", _
                .IsDepot = True, _
                .Latitude = 40.7636197, _
                .Longitude = -73.9744388, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "717 5th Ave New York, NY 10022", _
                .[Alias] = "Giorgio Armani", _
                .Latitude = 40.7669692, _
                .Longitude = -73.9693864, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "888 Madison Ave New York, NY 10014", _
                .[Alias] = "Ralph Lauren Women's and Home", _
                .Latitude = 40.7715154, _
                .Longitude = -73.9669241, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "1011 Madison Ave New York, NY 10075", _
                .[Alias] = "Yigal Azrou'l", _
                .Latitude = 40.7772129, _
                .Longitude = -73.9669, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "440 Columbus Ave New York, NY 10024", _
                .[Alias] = "Frank Stella Clothier", _
                .Latitude = 40.7808364, _
                .Longitude = -73.9732729, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "324 Columbus Ave #1 New York, NY 10023", _
                .[Alias] = "Liana", _
                .Latitude = 40.7803123, _
                .Longitude = -73.9793079, _
                .Time = 0 _
            }, _
                New Address() With { _
                .AddressString = "110 W End Ave New York, NY 10023", _
                .[Alias] = "Toga Bike Shop", _
                .Latitude = 40.7753077, _
                .Longitude = -73.9861529, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "555 W 57th St New York, NY 10019", _
                .[Alias] = "BMW of Manhattan", _
                .Latitude = 40.7718005, _
                .Longitude = -73.9897716, _
                .Time = 0 _
            }, New Address() With { _
                .AddressString = "57 W 57th St New York, NY 10019", _
                .[Alias] = "Verizon Wireless", _
                .Latitude = 40.7558695, _
                .Longitude = -73.9862019, _
                .Time = 0 _
            }}

            ' Set parameters
            ' Using the defined class, can use user-defined class instead


            Dim parameters As New RouteParameters() With {
                .AlgorithmType = AlgorithmType.TSP,
                .RouteName = "Single Driver Round Trip",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = "1",
                .VehicleMaxDistanceMI = "10000",
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Distance),
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI),
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web),
                .TravelMode = EnumHelper.GetEnumDescription(TravelMode.Driving)
            }

            Dim myParameters As New MyAddressAndParametersHolder() With { _
                .addresses = addresses, _
                .parameters = parameters _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As MyDataObjectGeneric = route4Me.GetJsonObjectFromAPI(Of MyDataObjectGeneric)(myParameters, uri, HttpMethodType.Post, errorString)

            Console.WriteLine("")

            If dataObject IsNot Nothing Then
                Console.WriteLine("SingleDriverRoundTripGeneric executed successfully")
                Console.WriteLine("")

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId)
                Console.WriteLine("State: {0}", dataObject.MyState)
                Console.WriteLine("")

                For Each Address As Address In dataObject.Addresses
                    Console.WriteLine("Address: {0}", Address.AddressString)
                    Console.WriteLine("Route ID: {0}", Address.RouteId)
                Next
                
                Return dataObject.OptimizationProblemId
            Else
                Console.WriteLine("SingleDriverRoundTripGeneric error {0}", errorString)
                Return Nothing
            End If
        End Function

#End Region
    End Class
End Namespace
