Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        ''' <summary>
        ''' The example refers to the process of creating an optimization 
        ''' with single-depot, multi-driver, no time windows options.
        ''' </summary>
        Public Sub SingleDepotMultipleDriverNoTimeWindow()
            ' Create the manager with the api key
            Dim route4Me = New Route4MeManager(ActualApiKey)

            ' Prepare the addresses
            Dim addresses = New Address() {New Address() With {
                .AddressString = "40 Mercer st, New York, NY",
                .IsDepot = True,
                .Latitude = 40.7213583,
                .Longitude = -74.0013082,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york, ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Manhatten Island NYC",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "503 W139 St, NY,NY",
                .Latitude = 40.7109062,
                .Longitude = -74.0091848,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "203 grand st, new york, ny",
                .Latitude = 40.718899,
                .Longitude = -73.996732,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "119 Church Street",
                .Latitude = 40.7137757,
                .Longitude = -74.0088238,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "broadway street, new york",
                .Latitude = 40.7191551,
                .Longitude = -74.0020849,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Ground Zero, Vesey-Liberty-Church-West Streets New York NY 10038",
                .Latitude = 40.7233126,
                .Longitude = -74.0116602,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "226 ilyssa way staten lsland ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "185 franklin st.",
                .Latitude = 40.7192099,
                .Longitude = -74.009767,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york city,",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "11 e. broaway 11038",
                .Latitude = 40.713206,
                .Longitude = -73.9974019,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Brooklyn Bridge, NY",
                .Latitude = 40.7053804,
                .Longitude = -73.9962503,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "World Trade Center Site, NY",
                .Latitude = 40.711498,
                .Longitude = -74.012299,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York Stock Exchange, NY",
                .Latitude = 40.7074242,
                .Longitude = -74.0116342,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Wall Street, NY",
                .Latitude = 40.7079825,
                .Longitude = -74.0079781,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Trinity Church, NY",
                .Latitude = 40.7081426,
                .Longitude = -74.0120511,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "World Financial Center, NY",
                .Latitude = 40.710475,
                .Longitude = -74.015493,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Federal Hall, NY",
                .Latitude = 40.7073034,
                .Longitude = -74.0102734,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Flatiron Building, NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "South Street Seaport, NY",
                .Latitude = 40.706921,
                .Longitude = -74.003638,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Rockefeller Center, NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "FAO Schwarz, NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Woolworth Building, NY",
                .Latitude = 40.7123903,
                .Longitude = -74.0083309,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Met Life Building, NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "SOHO/Tribeca, NY",
                .Latitude = 40.718565,
                .Longitude = -74.012017,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "MacyГўв‚¬в„ўs, NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "City Hall, NY, NY",
                .Latitude = 40.7127047,
                .Longitude = -74.0058663,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Macy&amp;acirc;в‚¬в„ўs, NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "1452 potter blvd bayshore ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "55 Church St. New York, NY",
                .Latitude = 40.711232,
                .Longitude = -74.010268,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "55 Church St, New York, NY",
                .Latitude = 40.711232,
                .Longitude = -74.010268,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "79 woodlawn dr revena ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "135 main st revena ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "250 greenwich st, new york, ny",
                .Latitude = 40.713159,
                .Longitude = -74.011889,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "79 grand, new york, ny",
                .Latitude = 40.7216958,
                .Longitude = -74.0024352,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "World trade center" & vbLf,
                .Latitude = 40.711626,
                .Longitude = -74.010714,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "World trade centern",
                .Latitude = 40.713291,
                .Longitude = -74.011835,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "391 broadway new york",
                .Latitude = 40.7183693,
                .Longitude = -74.00278,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Fletcher street",
                .Latitude = 40.7063954,
                .Longitude = -74.0056353,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "2 Plum LanenPlainview New York",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "50 Kennedy drivenPlainview New York",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "7 Crestwood DrivenPlainview New York",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "85 west street nyc",
                .Latitude = 40.709646,
                .Longitude = -74.014614,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York, New York",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "89 Reade St, New York City, New York 10013",
                .Latitude = 40.714297,
                .Longitude = -74.005966,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "100 white st",
                .Latitude = 40.7172477,
                .Longitude = -74.0014351,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "100 white st" & vbLf & "33040",
                .Latitude = 40.7172477,
                .Longitude = -74.0014351,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Canal st and mulberry",
                .Latitude = 40.717088,
                .Longitude = -73.9986025,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "91-83 111st st" & vbLf & "Richmond hills ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "122-09 liberty avenOzone park ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "80-16 101 avenOzone park ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "6302 woodhaven blvdnRego park ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "39-02 64th stnWoodside ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York City, NY,",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Pine st",
                .Latitude = 40.7069754,
                .Longitude = -74.0089557,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Wall st",
                .Latitude = 40.7079825,
                .Longitude = -74.0079781,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "32 avenue of the Americas, NY, NY",
                .Latitude = 40.720114,
                .Longitude = -74.005092,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "260 west broadway, NY, NY",
                .Latitude = 40.720621,
                .Longitude = -74.005567,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Long island, ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "27 Carley ave" & vbLf & "Huntington ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "17 west neck RdnHuntington ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "206 washington st",
                .Latitude = 40.7131577,
                .Longitude = -74.0126091,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Cipriani new york",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Byshnell Basin. NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "89 Reade St, New York, New York 10013",
                .Latitude = 40.714297,
                .Longitude = -74.005966,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "250 Greenwich St, New York, New York 10007",
                .Latitude = 40.7133,
                .Longitude = -74.012,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "64 Bowery, New York, New York 10013",
                .Latitude = 40.716554,
                .Longitude = -73.99627,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "142-156 Mulberry St, New York, New York 10013",
                .Latitude = 40.7192764,
                .Longitude = -73.9973096,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "80 Spring St, New York, New York 10012",
                .Latitude = 40.722659,
                .Longitude = -73.998182,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "182 Duane street ny",
                .Latitude = 40.7170879,
                .Longitude = -74.010121,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "182 Duane St, New York, New York 10013",
                .Latitude = 40.7170879,
                .Longitude = -74.010121,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "462 broome street nyc",
                .Latitude = 40.72258,
                .Longitude = -74.000898,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "117 mercer street nyc",
                .Latitude = 40.7239679,
                .Longitude = -73.9991585,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Lucca antiques" & vbLf & "182 Duane St, New York, New York 10013",
                .Latitude = 40.7167516,
                .Longitude = -74.0087482,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Room and board" & vbLf & "105 Wooster street nyc",
                .Latitude = 40.7229097,
                .Longitude = -74.0021852,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Lucca antiquesn182 Duane St, New York, New York 10013",
                .Latitude = 40.7167516,
                .Longitude = -74.0087482,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Room and boardn105 Wooster street nyc",
                .Latitude = 40.7229097,
                .Longitude = -74.0021852,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Lucca antiques 182 Duane st new York ny",
                .Latitude = 40.7170879,
                .Longitude = -74.010121,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Property" & vbLf & "14 Wooster street nyc",
                .Latitude = 40.7229097,
                .Longitude = -74.0021852,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "101 Crosby street nyc",
                .Latitude = 40.723573,
                .Longitude = -73.996954,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Room and board " & vbLf & "105 Wooster street nyc",
                .Latitude = 40.7229097,
                .Longitude = -74.0021852,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Propertyn14 Wooster street nyc",
                .Latitude = 40.7229097,
                .Longitude = -74.0021852,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Room and board n105 Wooster street nyc",
                .Latitude = 40.7229097,
                .Longitude = -74.0021852,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Mecox gardens" & vbLf & "926 Lexington nyc",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "25 sybil&apos;s crossing Kent lakes, ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "10149 ASHDALE LANE" & vbTab & "67" & vbTab & "67393253" & vbTab & vbTab & vbTab & "SANTEE" & vbTab & "CA" & vbTab & "92071" & vbTab & vbTab & "280501691" & vbTab & "67393253" & vbTab & "IFI" & vbTab & "280501691" & vbTab & "05-JUN-10" & vbTab & "67393253",
                .Latitude = 40.7143,
                .Longitude = -74.0067,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "193 Lakebridge Dr, Kings Paark, NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "219 west creek",
                .Latitude = 40.7198564,
                .Longitude = -74.0121098,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "14 North Moore Street" & vbLf & "New York, ny",
                .Latitude = 40.719697,
                .Longitude = -74.00661,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "14 North Moore StreetnNew York, ny",
                .Latitude = 40.719697,
                .Longitude = -74.00661,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "14 North Moore Street New York, ny",
                .Latitude = 40.719697,
                .Longitude = -74.00661,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "30-38 Fulton St, New York, New York 10038",
                .Latitude = 40.7077737,
                .Longitude = -74.0043299,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "73 Spring Street Ny NY",
                .Latitude = 40.7225378,
                .Longitude = -73.9976742,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "119 Mercer Street Ny NY",
                .Latitude = 40.724139,
                .Longitude = -73.999311,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "525 Broadway Ny NY",
                .Latitude = 40.723041,
                .Longitude = -73.999165,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Church St",
                .Latitude = 40.7154338,
                .Longitude = -74.007543,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "135 union stnWatertown ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "21101 coffeen stnWatertown ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "215 Washington stnWatertown ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "619 mill stnWatertown ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "3 canel st, new York, ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york city new york",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "50 grand street",
                .Latitude = 40.722578,
                .Longitude = -74.0038019,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Orient ferry, li ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Hilton hotel river head li ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "116 park pl",
                .Latitude = 40.7140565,
                .Longitude = -74.0110155,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "long islans new york",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "1 prospect pointe niagra falls ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York City" & vbTab & "NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "pink berry ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York City" & vbTab & " NY",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "10108",
                .Latitude = 40.7143,
                .Longitude = -74.0067,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Ann st",
                .Latitude = 40.7105937,
                .Longitude = -74.0073715,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Hok 620 ave of Americas new York ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Som 14 wall st nyc",
                .Latitude = 40.7076179,
                .Longitude = -74.010763,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York ,ny",
                .Latitude = 40.7142691,
                .Longitude = -74.0059729,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "52 prince st. 10012",
                .Latitude = 40.723584,
                .Longitude = -73.996117,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "451 broadway 10013",
                .Latitude = 40.7205177,
                .Longitude = -74.0009557,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Dover street",
                .Latitude = 40.7087886,
                .Longitude = -74.0008644,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Murray st",
                .Latitude = 40.7148929,
                .Longitude = -74.0113349,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "85 West St, New York, New York",
                .Latitude = 40.709646,
                .Longitude = -74.014614,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "NYC",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "64 trinity place, ny, ny",
                .Latitude = 40.7081649,
                .Longitude = -74.0127168,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "150 broadway ny ny",
                .Latitude = 40.709185,
                .Longitude = -74.010033,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Pinegrove Dude Ranch 31 cherrytown Rd Kerhinkson Ny",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Front street",
                .Latitude = 40.706399,
                .Longitude = -74.0045493,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "234 canal St new York, NY 10013",
                .Latitude = 40.717701,
                .Longitude = -73.999957,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "72 spring street, new york ny 10012",
                .Latitude = 40.7225093,
                .Longitude = -73.997654,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "150 spring street, new york, ny 10012",
                .Latitude = 40.7242393,
                .Longitude = -74.0014922,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "580 broadway street, new york, ny 10012",
                .Latitude = 40.724421,
                .Longitude = -73.997026,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "42 trinity place, new york, ny 10007",
                .Latitude = 40.7074,
                .Longitude = -74.013551,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "baco ny",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Micro Tel Inn Alburn New York",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "20 Cedar Close",
                .Latitude = 40.7068734,
                .Longitude = -74.0078613,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "South street",
                .Latitude = 40.7080184,
                .Longitude = -73.9999414,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "47 Lafayette street",
                .Latitude = 40.7159204,
                .Longitude = -74.0027332,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Newyork",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Ground Zero, NY",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "GROUND ZERO NY",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "33400 SE Harrison",
                .Latitude = 40.71884,
                .Longitude = -74.010333,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york, new york",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "8 Greene St, New York, 10013",
                .Latitude = 40.720616,
                .Longitude = -74.00276,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "226 w 44st new york city",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "s street seaport 11 fulton st new york city",
                .Latitude = 40.706915,
                .Longitude = -74.0033215,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "30 Rockefeller Plaza w 49th St New York City",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "30 Rockefeller Plaza 50th St New York City",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "S. Street Seaport 11 Fulton St. New York City",
                .Latitude = 40.706915,
                .Longitude = -74.0033215,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "30 rockefeller plaza w 49th st, new york city",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "30 rockefeller plaza 50th st, new york city",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "11 fulton st, new york city",
                .Latitude = 40.706915,
                .Longitude = -74.0033215,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york city ny",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Big apple",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Ny",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York new York",
                .Latitude = 40.7143528,
                .Longitude = -74.0059731,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "83-85 Chambers St, New York, New York 10007",
                .Latitude = 40.714813,
                .Longitude = -74.006889,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York",
                .Latitude = 40.7145502,
                .Longitude = -74.0071249,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "102 North End Ave NY, NY",
                .Latitude = 40.714798,
                .Longitude = -74.015969,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "57 Thompson St, New York, New York 10012",
                .Latitude = 40.72414,
                .Longitude = -74.003586,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york city",
                .Latitude = 40.71455,
                .Longitude = -74.007125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "nyc, ny",
                .Latitude = 40.7145502,
                .Longitude = -74.0071249,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York NY",
                .Latitude = 40.71455,
                .Longitude = -74.007125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "285 West Broadway New York, NY 10013",
                .Latitude = 40.720875,
                .Longitude = -74.004631,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "100 avenue of the americas New York, NY 10013",
                .Latitude = 40.723312,
                .Longitude = -74.004395,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "270 Lafeyette st New York, NY 10012",
                .Latitude = 40.723879,
                .Longitude = -73.996527,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "560 Broadway New York, NY 10012",
                .Latitude = 40.723854,
                .Longitude = -73.997498,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "42 Wooster St New York, NY 10013",
                .Latitude = 40.722386,
                .Longitude = -74.002422,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "42 Wooster StreetNew York, NY 10013-2230",
                .Latitude = 40.7223633,
                .Longitude = -74.002624,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "504 Broadway, New York, NY 10012",
                .Latitude = 40.7221444,
                .Longitude = -73.9992714,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "426 Broome Street, New York, NY 10013",
                .Latitude = 40.7213295,
                .Longitude = -73.9987121,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "City hall, nyc",
                .Latitude = 40.7122066,
                .Longitude = -74.0055026,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "South street seaport, nyc",
                .Latitude = 40.7069501,
                .Longitude = -74.0030848,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Ground zero, nyc",
                .Latitude = 40.711641,
                .Longitude = -74.012253,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Ground zero",
                .Latitude = 40.711641,
                .Longitude = -74.012253,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Mulberry and canal, NYC",
                .Latitude = 40.71709,
                .Longitude = -73.99859,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "World Trade Center, NYC",
                .Latitude = 40.711667,
                .Longitude = -74.0125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "South Street Seaport",
                .Latitude = 40.7069501,
                .Longitude = -74.0030848,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Wall Street and Nassau Street, NYC",
                .Latitude = 40.70714,
                .Longitude = -74.01069,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Trinity Church, NYC",
                .Latitude = 40.7081269,
                .Longitude = -74.0125691,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Federal Hall National Memorial",
                .Latitude = 40.7069515,
                .Longitude = -74.0101638,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Little Italy, NYC",
                .Latitude = 40.719692,
                .Longitude = -73.997765,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York, NY",
                .Latitude = 40.71455,
                .Longitude = -74.007125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York City, NY,",
                .Latitude = 40.71455,
                .Longitude = -74.007125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york,ny",
                .Latitude = 40.71455,
                .Longitude = -74.00713,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Odeon cinema",
                .Latitude = 40.71683,
                .Longitude = -74.00803,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York City",
                .Latitude = 40.71455,
                .Longitude = -74.00713,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "52 broadway, ny,ny 1004",
                .Latitude = 40.7065,
                .Longitude = -74.0123,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "52 broadway, ny,ny 10004",
                .Latitude = 40.7065,
                .Longitude = -74.0123,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "22 beaver st, ny,ny 10004",
                .Latitude = 40.70482,
                .Longitude = -74.01218,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "54 pine st,ny,ny 10005",
                .Latitude = 40.70686,
                .Longitude = -74.00849,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "114 liberty st, ny,ny 10006",
                .Latitude = 40.70977,
                .Longitude = -74.0122,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "215 canal st,ny,ny 10013",
                .Latitude = 40.71747,
                .Longitude = -73.99895,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york city ny",
                .Latitude = 40.71455,
                .Longitude = -74.00713,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "World Trade Center, New York, NY",
                .Latitude = 40.71167,
                .Longitude = -74.0125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Chinatown, New York, NY",
                .Latitude = 40.71596,
                .Longitude = -73.99741,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "101 murray street new york, ny",
                .Latitude = 40.71526,
                .Longitude = -74.01251,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "nyc",
                .Latitude = 40.71455,
                .Longitude = -74.00712,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "510 broadway new york",
                .Latitude = 40.72234,
                .Longitude = -73.999016,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "nyc",
                .Latitude = 40.7145502,
                .Longitude = -74.0071249,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Little Italy",
                .Latitude = 40.719692,
                .Longitude = -73.9977647,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "463 Broadway, New York, NY",
                .Latitude = 40.721059,
                .Longitude = -74.000688,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "222 West Broadway, New York, NY",
                .Latitude = 40.719352,
                .Longitude = -74.006417,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "270 Lafayette street new York new york",
                .Latitude = 40.723879,
                .Longitude = -73.996527,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "New York, NY USA",
                .Latitude = 40.71455,
                .Longitude = -74.007125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "97 Kenmare Street, New York, NY 10012",
                .Latitude = 40.721437,
                .Longitude = -73.996911,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "19 Beekman St, New York, New York 10038",
                .Latitude = 40.710754,
                .Longitude = -74.006287,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Soho",
                .Latitude = 40.7241404,
                .Longitude = -74.0020213,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Bergen, New York",
                .Latitude = 40.71455,
                .Longitude = -74.007125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "478 Broadway, NY, NY",
                .Latitude = 40.721336,
                .Longitude = -73.999771,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "555 broadway, ny, ny",
                .Latitude = 40.723883,
                .Longitude = -73.998296,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "375 West Broadway, NY, NY",
                .Latitude = 40.7235,
                .Longitude = -74.002602,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "35 howard st, NY, NY",
                .Latitude = 40.719524,
                .Longitude = -74.00103,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Pier 17 NYC",
                .Latitude = 40.706366,
                .Longitude = -74.002689,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "120 Liberty St NYC",
                .Latitude = 40.709774,
                .Longitude = -74.012451,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "80 White Street, NY, NY",
                .Latitude = 40.717834,
                .Longitude = -74.002052,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Manhattan, NY",
                .Latitude = 40.71443,
                .Longitude = -74.0061,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "22 read st, ny",
                .Latitude = 40.714201,
                .Longitude = -74.004491,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "130 Mulberry St, New York, NY 10013-5547",
                .Latitude = 40.718288,
                .Longitude = -73.997711,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "new york city, ny",
                .Latitude = 40.71455,
                .Longitude = -74.007125,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "10038",
                .Latitude = 40.7092119,
                .Longitude = -74.0033631,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "11 Wall St, New York, NY 10005-1905",
                .Latitude = 40.70729,
                .Longitude = -74.011201,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "89 Reade St, New York, New York 10007",
                .Latitude = 40.713456,
                .Longitude = -74.003499,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "265 Canal St, New York, NY 10013-6010",
                .Latitude = 40.718885,
                .Longitude = -74.0009,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "39 Broadway, New York, NY 10006-3003",
                .Latitude = 40.713345,
                .Longitude = -73.996132,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "25 beaver street new york ny",
                .Latitude = 40.705111,
                .Longitude = -74.012007,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "100 church street new york ny",
                .Latitude = 40.713043,
                .Longitude = -74.009637,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "69 Mercer St, New York, NY 10012-4440",
                .Latitude = 40.722649,
                .Longitude = -74.00061,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "111 Worth St, New York, NY 10013-4008",
                .Latitude = 40.715921,
                .Longitude = -74.00341,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "240-248 Broadway, New York, New York 10038",
                .Latitude = 40.712769,
                .Longitude = -74.007681,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "12 Maiden Ln, New York, NY 10038-4002",
                .Latitude = 40.709446,
                .Longitude = -74.009576,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "291 Broadway, New York, NY 10007-1814",
                .Latitude = 40.715,
                .Longitude = -74.006134,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "55 Liberty St, New York, NY 10005-1003",
                .Latitude = 40.708843,
                .Longitude = -74.009384,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "Brooklyn Bridge, NY",
                .Latitude = 40.706344,
                .Longitude = -73.997439,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "wall street",
                .Latitude = 40.7063889,
                .Longitude = -74.0094444,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "south street seaport, ny",
                .Latitude = 40.7069501,
                .Longitude = -74.0030848,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "little italy, ny",
                .Latitude = 40.719692,
                .Longitude = -73.9977647,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "47 Pine St, New York, NY 10005-1513",
                .Latitude = 40.706734,
                .Longitude = -74.008928,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "22 cortlandt street new york ny",
                .Latitude = 40.710082,
                .Longitude = -74.010251,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "105 reade street new york ny",
                .Latitude = 40.715633,
                .Longitude = -74.008522,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "2 lafayette street new york ny",
                .Latitude = 40.714031,
                .Longitude = -74.003891,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "53 crosby street new york ny",
                .Latitude = 40.721977,
                .Longitude = -73.998245,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "2 Lafayette St, New York, NY 10007-1307",
                .Latitude = 40.714031,
                .Longitude = -74.003891,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "105 Reade St, New York, NY 10013-3840",
                .Latitude = 40.715633,
                .Longitude = -74.008522,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "chinatown, ny",
                .Latitude = 40.7159556,
                .Longitude = -73.9974133,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "250 Broadway, New York, NY 10007-2516",
                .Latitude = 40.713018,
                .Longitude = -74.00747,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "156 William St, New York, NY 10038-2609",
                .Latitude = 40.709797,
                .Longitude = -74.005577,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "100 Church St, New York, NY 10007-2601",
                .Latitude = 40.713043,
                .Longitude = -74.009637,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }, New Address() With {
                .AddressString = "33 Beaver St, New York, NY 10004-2736",
                .Latitude = 40.705098,
                .Longitude = -74.01172,
                .Time = 0,
                .TimeWindowStart = Nothing,
                .TimeWindowEnd = Nothing
            }}

            ' Set parameters
            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.CVRP_TW_MD,
                .RouteName = "Single Depot, Multiple Driver, No Time Window",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RT = True,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = 20,
                .VehicleMaxDistanceMI = 99999,
                .Parts = 4,
                .Optimize = Optimize.Time.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription(),
                .Metric = Metric.Matrix
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }

            ' Run the query
            Dim errorString As String = Nothing
            Dim dataObject As DataObject = route4Me.RunOptimization(
                                                        optimizationParameters,
                                                        errorString)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            PrintExampleOptimizationResult(dataObject, errorString)

            RemoveTestOptimizations()
        End Sub
    End Class
End Namespace
