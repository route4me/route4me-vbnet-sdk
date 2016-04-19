Access Route4Me's logistics-as-a-service API using our VB.NET SDK
-------------------


### What does the Route4Me SDK permit me to do?
This SDK makes it easier for you use the Route4Me API, which creates optimally sequenced driving routes for many drivers.

### Who can use the Route4Me SDK (and API)?
The service is typically used by organizations who must route many drivers to many destinations. In addition to route optimization for new (future) routes, the API can also be used to analyze historical routes, and to distribute routes to field personnel.

### Who is prohibited from using the Route4Me SDK (and API)?
The Route4Me SDK and API cannot be resold or used in a product or system that competes directly with Route4Me. This means that developers cannot resell route optimization services to other businesses or developers. However, developers can integrate our route optimization SDK/API into their software applications. Developers and startups are also permitted to use our software for internal purposes (i.e. a same day delivery startup).


### How does the API/SDK Integration Work?
A Route4Me customer, integrator, or partner incorporates the Route4Me SDK or API into their code base.
Route4Me permits any paying subscriber to interact with every part of its system using it’s API.
The API is RESTful, which means that it’s web based and can be accessed by other programs and machines
The API/SDK should be used to automate the route planning process, or to generate many routes with minimal manual intervention



### Do optimized routes automatically appear inside my Route4Me account?
Every Route4Me SDK instance needs a unique API key. The API key can be retrieved inside your Route4Me.com account, inside the Settings tab called API. When a route is planned, it appears inside the corresponding Route4Me account. Because Route4Me web and mobile accounts are synchronized, the routes will appear in both environments at the same time.

### Can I test the SDK with other addresses without a valid API Key?
No. The sample API key only permits you to optimize routes with the sample address coordinates that are part of this SDK.

### Does the SDK have rate limits?
The number of requests you can make per second is limited by your current subscription plan. Typically, there are different rate limits for these core features:
Address Geocoding & Address Reverse Geocoding
Route Optimization & Management
Viewing a Route

### What is the recommended architecture for the Route4Me SDK?
There are two typical integration strategies that we recommend.  Using this SDK, you can make optimization requests and then the SDK polls the Route4Me API to detect state changes as the optimization progresses. Alternatively, you can provide a webhook/callback url, and the API will notify that callback URL every time there is a state change.

### I don't need route management or mobile capabilities. Is there a lower level Route4Me API just for the optimization engine?
Yes. Please contact support@route4me.com to learn about the low-level RESTful API.

### How fast is the route Route4Me Optimization Web Service?
Most routes having less than 200 destinations are optimized in 1 second or less.

### Can I disable optimization when planning routes?
Yes. You can send routes with optimization disabled if you want to conveniently see them on a map, or distribute them to your drivers in the order you prefer.

### Can the API be used for aerial vehicles such as drones or self-driving cars?
Yes. The API can accept lat/lng and an unlimited amount of per-address metadata. The metadata will be preserved as passthrough data by our API, so that the receiving device will have access to critical data when our API invokes a webhook callback to the device.

### Are all my optimized routes stored permanently stored in the Route4Me database?
Yes. All routes are permanently stored in the database and are no longer accessible to you after your subscription is terminated.


### Can I incorporate your API into my mobile application?
Route4Me’s route planning and optimization technology can only be added into applications that do not directly compete with Route4Me. 
This means the application’s primary capabilities must be unrelated to route optimization, route planning, or navigation.

### Can I pay you to develop a custom algorithm?
Yes

### Can I use your API and resell it to my customers?
White-labeling and private-labeling Route4Me is possible but the deal’s licensing terms vary considerably based on customer count, route count, and the level of support that Route4Me should provide to your customers.

### Does the API/SDK have TMS or EDI, or EDI translator capabilities?
Route4Me is currently working on these features but they are not currently available for sale.

### Can the API/SDK send notifications back to our system using callbacks, notifications, pushes, or webhooks?

Because Route4Me processes all routes asynchronously, Route4Me will conveniently notify the endpoint you specify as the route optimization job progresses through each state of the optimization. Every stage of the route optimization process has a unique stage id.

### Does the Route4Me API and SDK work in my country?
Route4Me.com, as well as all of Route4Me’s mobile applications use the Route4Me SDK’s and API.
Since Route4Me works globally, this means that all of Route4Me’s capabilities are available using the SDK’s in every country 


### Will the Route4Me API/SDK work in my program on the Mac, PC, or Linux?
Customers are encouraged to select their preferred operating system environment. The Route4Me API/SDK will function on any operating system that supports the preferred programming language of the customer. At this point in time, almost every supported SDK can run on any operating system.


### Does the Route4Me API/SDK require me to buy my own servers?
Route4Me has its own computing infrastructure that you can access using the API and SDKs. Customers typically have to run the SDK code on their own computers and/or servers to access this infrastructure.

### Does Route4Me have an on-premise solution?
Route4Me does not currently lease or sell servers, and does not have on-premise appliance solution. This would only be possible in exceptionally unique scenarios.


### Does the Route4Me API/SDK require me to have my own programmers?
The time required to integrate the SDK can be as little as 1 hour or may take several weeks, depending on the number of features being incorporated into the customer’s application and how much integration testing will be done by the client. A programmer’s involvement is almost always required to use Route4Me’s technology when accessing it through the API.


### Installation and Usage

1. Add reference to Route4MeSDKLibrary.dll
2. Use the class Route4MeSDK.Route4MeManager for accessing the Route4ME API
3. Use methods Route4MeManager.GetRoute(), Route4MeManager.UpdateOptimization() etc. to access the main functionality of Route4Me API.
4. Use generic methods Route4MeManager.GetStringResponseFromAPI() and Route4MeManager.GetJsonObjectFromAPI<T>() for accessing any Route4Me API functionally via custom defined classes (see example in Route4MeSDKTest.SingleDriverRoundTripGeneric.cs)

### Examples and Tests

1. See project "Route4Me Route Optimization Examples" (class Route4MeSDKTest.Examples) for some examples of using Route4MeSDKLibrary
2. See an example of creating a simple route below

### Creating a Simple Route

``` VB.NET
Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDriverRoundTrip() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the addresses
            '#Region "Addresses"  ' Note: For vb.net code region feature is supported starting from Visual Studio 2015
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
			'#End Region  
            ' Set parameters

            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.TSP, _
                .StoreRoute = False, _
                .RouteName = "Single Driver Round Trip", _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 60 * 60 * 7, _
                .RouteMaxDuration = 86400, _
                .VehicleCapacity = "1", _
                .VehicleMaxDistanceMI = "10000", _
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Distance), _
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI), _
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web), _
                .TravelMode = EnumHelper.GetEnumDescription(TravelMode.Driving) _
            }

            Dim optimizationParameters As New OptimizationParameters() With { _
                .Addresses = addresses, _
                .Parameters = parameters _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.RunOptimization(OptimizationParameters, errorString)

            ' Output the result
            PrintExampleOptimizationResult("SingleDriverRoundTrip", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
```

-------------------
##Optimization Problem Types

Route4Me provides solutions for different types of routes:

### Single depot, Single Driver

Single Depot, Single Driver is particular case of Optimization Problem of VRP (Vehicle Routing Problem), in which vehicle departs from the depot and visits a set of customers.

See video tutorial at [Single Driver, Single Depot](http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter1:subchapter1)

Required parameters: addresses, api_key, algorithm_types.

see detailed description of parameters and sample values [here] (https://www.route4me.com/api/demo/add_optimization_problem.php?sample=Single%20Depot,%20Single%20Driver)

### Single Driver Route 10 Stops

Single Depot, Single Driver and 10 stops is particular case of Optimization Problem, in which vehicle departs from the depot, visits 10 stops ends in the 10th stop.

Required parameters: addresses, api_key, algorithm_types.

see detailed description of parameters and sample values [here] (https://www.route4me.com/api/demo/add_optimization_problem.php?sample=Single%20Driver%20Route%2010%20Stops)

### Single Driver Round Trip

Single Driver Round Trip is particular case of Optimization Problem, in which vehicle departs from the depot, visits a set of customers and ends it's routes in the depot.

Required parameters: addresses, api_key, algorithm_types.

see detailed description of parameters and sample values [here] (https://www.route4me.com/api/demo/add_optimization_problem.php?sample=Single%20Driver%20Round%20Trip)

### Single Depot, Multiple Driver

Single Depot, Multiple Driver is particular case of Optimization Problem, in which multiple vehicles depart from the depot, visit a set of customers and end their routes in the depot.

See video tutorial at [Single Depot, Multiple Driver](http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter1:subchapter2)

Required parameters: addresses, api_key, algorithm_types.

see detailed description of parameters and sample values [here] (https://www.route4me.com/api/demo/add_optimization_problem.php?sample=Single%20Depot,%20Multiple%20Driver)

### Single Depot, Multiple Driver, Time window

Single Depot, Multiple Driver, Time window is particular case of Optimization Problem with time constraints, in which multiple vehicles depart from the depot, visit a set of customers and end their routes in the depot.

Required parameters: addresses, api_key, algorithm_types.

see detailed description of parameters and sample values [here] (https://www.route4me.com/api/demo/add_optimization_problem.php?sample=Single%20Depot,%20Multiple%20Driver,%20Time%20window)

### Multiple Depot, Multiple Driver

Multiple Depot, Multiple Driver is particular case of Optimization Problem, in which multiple vehicles depart from the multiple depot, visit a set of customers and end their routes in the depots.

See video tutorial at [Multiple Depot, Multiple](http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter2:subchapter1)

Required parameters: addresses, api_key, algorithm_types.

see detailed description of parameters and sample values [here] (https://www.route4me.com/api/demo/add_optimization_problem.php?sample=Multiple%20Depot,%20Multiple%20Driver)

### Multiple Depot, Multiple Driver, Time window

Multiple Depot, Multiple Driver, Time window is particular case of Optimization Problem  with time constraints, in which multiple vehicles depart from the multiple depot, visit a set of customers and end their routes in the depots.

See video tutorial at [Multiple Depot, Multiple Driver, Time window](http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter2:subchapter2)

Required parameters: addresses, api_key, algorithm_types.

see detailed description of parameters and sample values [here] (https://www.route4me.com/api/demo/add_optimization_problem.php?sample=Multiple%20Depot,%20Multiple%20Driver,%20Time%20window)

...

**See other interesting video tutorials about Different Types of Routes [here](http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2)**

-------------------