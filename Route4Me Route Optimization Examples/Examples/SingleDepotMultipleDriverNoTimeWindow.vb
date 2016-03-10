Imports Route4MeSDK
Imports Route4MeSDK.DataTypes
Imports Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function SingleDepotMultipleDriverNoTimeWindow() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(c_ApiKey)

            ' Prepare the addresses
            '#Region "Addresses"





            '125left



            '#End Region
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "40 Mercer st, New York, NY", _
                .IsDepot = True, _
                .Latitude = 40.7213583, _
                .Longitude = -74.0013082, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york, ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Manhatten Island NYC", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "503 W139 St, NY,NY", _
                .Latitude = 40.7109062, _
                .Longitude = -74.0091848, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "203 grand st, new york, ny", _
                .Latitude = 40.718899, _
                .Longitude = -73.996732, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "119 Church Street", _
                .Latitude = 40.7137757, _
                .Longitude = -74.0088238, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "new york ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "broadway street, new york", _
                .Latitude = 40.7191551, _
                .Longitude = -74.0020849, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Ground Zero, Vesey-Liberty-Church-West Streets New York NY 10038", _
                .Latitude = 40.7233126, _
                .Longitude = -74.0116602, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "226 ilyssa way staten lsland ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "185 franklin st.", _
                .Latitude = 40.7192099, _
                .Longitude = -74.009767, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "new york city,", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "11 e. broaway 11038", _
                .Latitude = 40.713206, _
                .Longitude = -73.9974019, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Brooklyn Bridge, NY", _
                .Latitude = 40.7053804, _
                .Longitude = -73.9962503, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "World Trade Center Site, NY", _
                .Latitude = 40.711498, _
                .Longitude = -74.012299, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York Stock Exchange, NY", _
                .Latitude = 40.7074242, _
                .Longitude = -74.0116342, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Wall Street, NY", _
                .Latitude = 40.7079825, _
                .Longitude = -74.0079781, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Trinity Church, NY", _
                .Latitude = 40.7081426, _
                .Longitude = -74.0120511, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "World Financial Center, NY", _
                .Latitude = 40.710475, _
                .Longitude = -74.015493, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Federal Hall, NY", _
                .Latitude = 40.7073034, _
                .Longitude = -74.0102734, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Flatiron Building, NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "South Street Seaport, NY", _
                .Latitude = 40.706921, _
                .Longitude = -74.003638, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Rockefeller Center, NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "FAO Schwarz, NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Woolworth Building, NY", _
                .Latitude = 40.7123903, _
                .Longitude = -74.0083309, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Met Life Building, NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "SOHO/Tribeca, NY", _
                .Latitude = 40.718565, _
                .Longitude = -74.012017, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "MacyГўв‚¬в„ўs, NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "City Hall, NY, NY", _
                .Latitude = 40.7127047, _
                .Longitude = -74.0058663, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Macy&amp;acirc;в‚¬в„ўs, NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "1452 potter blvd bayshore ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "55 Church St. New York, NY", _
                .Latitude = 40.711232, _
                .Longitude = -74.010268, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "55 Church St, New York, NY", _
                .Latitude = 40.711232, _
                .Longitude = -74.010268, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "79 woodlawn dr revena ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "135 main st revena ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "250 greenwich st, new york, ny", _
                .Latitude = 40.713159, _
                .Longitude = -74.011889, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "79 grand, new york, ny", _
                .Latitude = 40.7216958, _
                .Longitude = -74.0024352, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "World trade center" & vbLf, _
                .Latitude = 40.711626, _
                .Longitude = -74.010714, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "World trade centern", _
                .Latitude = 40.713291, _
                .Longitude = -74.011835, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "391 broadway new york", _
                .Latitude = 40.7183693, _
                .Longitude = -74.00278, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Fletcher street", _
                .Latitude = 40.7063954, _
                .Longitude = -74.0056353, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "2 Plum LanenPlainview New York", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "50 Kennedy drivenPlainview New York", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "7 Crestwood DrivenPlainview New York", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "85 west street nyc", _
                .Latitude = 40.709646, _
                .Longitude = -74.014614, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York, New York", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "89 Reade St, New York City, New York 10013", _
                .Latitude = 40.714297, _
                .Longitude = -74.005966, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "100 white st", _
                .Latitude = 40.7172477, _
                .Longitude = -74.0014351, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "100 white st" & vbLf & "33040", _
                .Latitude = 40.7172477, _
                .Longitude = -74.0014351, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Canal st and mulberry", _
                .Latitude = 40.717088, _
                .Longitude = -73.9986025, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "91-83 111st st" & vbLf & "Richmond hills ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "122-09 liberty avenOzone park ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "80-16 101 avenOzone park ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "6302 woodhaven blvdnRego park ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "39-02 64th stnWoodside ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York City, NY,", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Pine st", _
                .Latitude = 40.7069754, _
                .Longitude = -74.0089557, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Wall st", _
                .Latitude = 40.7079825, _
                .Longitude = -74.0079781, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "32 avenue of the Americas, NY, NY", _
                .Latitude = 40.720114, _
                .Longitude = -74.005092, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "260 west broadway, NY, NY", _
                .Latitude = 40.720621, _
                .Longitude = -74.005567, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Long island, ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "27 Carley ave" & vbLf & "Huntington ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "17 west neck RdnHuntington ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "206 washington st", _
                .Latitude = 40.7131577, _
                .Longitude = -74.0126091, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Cipriani new york", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Byshnell Basin. NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "89 Reade St, New York, New York 10013", _
                .Latitude = 40.714297, _
                .Longitude = -74.005966, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "250 Greenwich St, New York, New York 10007", _
                .Latitude = 40.7133, _
                .Longitude = -74.012, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "64 Bowery, New York, New York 10013", _
                .Latitude = 40.716554, _
                .Longitude = -73.99627, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "142-156 Mulberry St, New York, New York 10013", _
                .Latitude = 40.7192764, _
                .Longitude = -73.9973096, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "80 Spring St, New York, New York 10012", _
                .Latitude = 40.722659, _
                .Longitude = -73.998182, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "182 Duane street ny", _
                .Latitude = 40.7170879, _
                .Longitude = -74.010121, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "182 Duane St, New York, New York 10013", _
                .Latitude = 40.7170879, _
                .Longitude = -74.010121, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "462 broome street nyc", _
                .Latitude = 40.72258, _
                .Longitude = -74.000898, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "117 mercer street nyc", _
                .Latitude = 40.7239679, _
                .Longitude = -73.9991585, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Lucca antiques" & vbLf & "182 Duane St, New York, New York 10013", _
                .Latitude = 40.7167516, _
                .Longitude = -74.0087482, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Room and board" & vbLf & "105 Wooster street nyc", _
                .Latitude = 40.7229097, _
                .Longitude = -74.0021852, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Lucca antiquesn182 Duane St, New York, New York 10013", _
                .Latitude = 40.7167516, _
                .Longitude = -74.0087482, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Room and boardn105 Wooster street nyc", _
                .Latitude = 40.7229097, _
                .Longitude = -74.0021852, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Lucca antiques 182 Duane st new York ny", _
                .Latitude = 40.7170879, _
                .Longitude = -74.010121, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Property" & vbLf & "14 Wooster street nyc", _
                .Latitude = 40.7229097, _
                .Longitude = -74.0021852, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "101 Crosby street nyc", _
                .Latitude = 40.723573, _
                .Longitude = -73.996954, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Room and board " & vbLf & "105 Wooster street nyc", _
                .Latitude = 40.7229097, _
                .Longitude = -74.0021852, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Propertyn14 Wooster street nyc", _
                .Latitude = 40.7229097, _
                .Longitude = -74.0021852, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Room and board n105 Wooster street nyc", _
                .Latitude = 40.7229097, _
                .Longitude = -74.0021852, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Mecox gardens" & vbLf & "926 Lexington nyc", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "25 sybil&apos;s crossing Kent lakes, ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "10149 ASHDALE LANE" & vbTab & "67" & vbTab & "67393253" & vbTab & vbTab & vbTab & "SANTEE" & vbTab & "CA" & vbTab & "92071" & vbTab & vbTab & "280501691" & vbTab & "67393253" & vbTab & "IFI" & vbTab & "280501691" & vbTab & "05-JUN-10" & vbTab & "67393253", _
                .Latitude = 40.7143, _
                .Longitude = -74.0067, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "193 Lakebridge Dr, Kings Paark, NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "219 west creek", _
                .Latitude = 40.7198564, _
                .Longitude = -74.0121098, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "14 North Moore Street" & vbLf & "New York, ny", _
                .Latitude = 40.719697, _
                .Longitude = -74.00661, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "14 North Moore StreetnNew York, ny", _
                .Latitude = 40.719697, _
                .Longitude = -74.00661, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "14 North Moore Street New York, ny", _
                .Latitude = 40.719697, _
                .Longitude = -74.00661, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "30-38 Fulton St, New York, New York 10038", _
                .Latitude = 40.7077737, _
                .Longitude = -74.0043299, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "73 Spring Street Ny NY", _
                .Latitude = 40.7225378, _
                .Longitude = -73.9976742, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "119 Mercer Street Ny NY", _
                .Latitude = 40.724139, _
                .Longitude = -73.999311, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "525 Broadway Ny NY", _
                .Latitude = 40.723041, _
                .Longitude = -73.999165, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Church St", _
                .Latitude = 40.7154338, _
                .Longitude = -74.007543, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "135 union stnWatertown ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "21101 coffeen stnWatertown ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "215 Washington stnWatertown ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "619 mill stnWatertown ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "3 canel st, new York, ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york city new york", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "50 grand street", _
                .Latitude = 40.722578, _
                .Longitude = -74.0038019, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Orient ferry, li ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Hilton hotel river head li ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "116 park pl", _
                .Latitude = 40.7140565, _
                .Longitude = -74.0110155, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "long islans new york", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "1 prospect pointe niagra falls ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York City" & vbTab & "NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "pink berry ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York City" & vbTab & " NY", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "10108", _
                .Latitude = 40.7143, _
                .Longitude = -74.0067, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Ann st", _
                .Latitude = 40.7105937, _
                .Longitude = -74.0073715, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Hok 620 ave of Americas new York ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Som 14 wall st nyc", _
                .Latitude = 40.7076179, _
                .Longitude = -74.010763, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York ,ny", _
                .Latitude = 40.7142691, _
                .Longitude = -74.0059729, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "52 prince st. 10012", _
                .Latitude = 40.723584, _
                .Longitude = -73.996117, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "451 broadway 10013", _
                .Latitude = 40.7205177, _
                .Longitude = -74.0009557, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Dover street", _
                .Latitude = 40.7087886, _
                .Longitude = -74.0008644, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Murray st", _
                .Latitude = 40.7148929, _
                .Longitude = -74.0113349, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "85 West St, New York, New York", _
                .Latitude = 40.709646, _
                .Longitude = -74.014614, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "NYC", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "64 trinity place, ny, ny", _
                .Latitude = 40.7081649, _
                .Longitude = -74.0127168, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "150 broadway ny ny", _
                .Latitude = 40.709185, _
                .Longitude = -74.010033, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Pinegrove Dude Ranch 31 cherrytown Rd Kerhinkson Ny", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Front street", _
                .Latitude = 40.706399, _
                .Longitude = -74.0045493, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "234 canal St new York, NY 10013", _
                .Latitude = 40.717701, _
                .Longitude = -73.999957, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "72 spring street, new york ny 10012", _
                .Latitude = 40.7225093, _
                .Longitude = -73.997654, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "150 spring street, new york, ny 10012", _
                .Latitude = 40.7242393, _
                .Longitude = -74.0014922, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "580 broadway street, new york, ny 10012", _
                .Latitude = 40.724421, _
                .Longitude = -73.997026, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "42 trinity place, new york, ny 10007", _
                .Latitude = 40.7074, _
                .Longitude = -74.013551, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "baco ny", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Micro Tel Inn Alburn New York", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "20 Cedar Close", _
                .Latitude = 40.7068734, _
                .Longitude = -74.0078613, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "South street", _
                .Latitude = 40.7080184, _
                .Longitude = -73.9999414, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "47 Lafayette street", _
                .Latitude = 40.7159204, _
                .Longitude = -74.0027332, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Newyork", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Ground Zero, NY", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "GROUND ZERO NY", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "33400 SE Harrison", _
                .Latitude = 40.71884, _
                .Longitude = -74.010333, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york, new york", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "8 Greene St, New York, 10013", _
                .Latitude = 40.720616, _
                .Longitude = -74.00276, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "226 w 44st new york city", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "s street seaport 11 fulton st new york city", _
                .Latitude = 40.706915, _
                .Longitude = -74.0033215, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "30 Rockefeller Plaza w 49th St New York City", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "30 Rockefeller Plaza 50th St New York City", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "S. Street Seaport 11 Fulton St. New York City", _
                .Latitude = 40.706915, _
                .Longitude = -74.0033215, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "30 rockefeller plaza w 49th st, new york city", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "30 rockefeller plaza 50th st, new york city", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "11 fulton st, new york city", _
                .Latitude = 40.706915, _
                .Longitude = -74.0033215, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york city ny", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Big apple", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Ny", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "New York new York", _
                .Latitude = 40.7143528, _
                .Longitude = -74.0059731, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "83-85 Chambers St, New York, New York 10007", _
                .Latitude = 40.714813, _
                .Longitude = -74.006889, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York", _
                .Latitude = 40.7145502, _
                .Longitude = -74.0071249, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "102 North End Ave NY, NY", _
                .Latitude = 40.714798, _
                .Longitude = -74.015969, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "57 Thompson St, New York, New York 10012", _
                .Latitude = 40.72414, _
                .Longitude = -74.003586, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york city", _
                .Latitude = 40.71455, _
                .Longitude = -74.007125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "nyc, ny", _
                .Latitude = 40.7145502, _
                .Longitude = -74.0071249, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York NY", _
                .Latitude = 40.71455, _
                .Longitude = -74.007125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "285 West Broadway New York, NY 10013", _
                .Latitude = 40.720875, _
                .Longitude = -74.004631, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "100 avenue of the americas New York, NY 10013", _
                .Latitude = 40.723312, _
                .Longitude = -74.004395, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "270 Lafeyette st New York, NY 10012", _
                .Latitude = 40.723879, _
                .Longitude = -73.996527, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "560 Broadway New York, NY 10012", _
                .Latitude = 40.723854, _
                .Longitude = -73.997498, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "42 Wooster St New York, NY 10013", _
                .Latitude = 40.722386, _
                .Longitude = -74.002422, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "42 Wooster StreetNew York, NY 10013-2230", _
                .Latitude = 40.7223633, _
                .Longitude = -74.002624, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "504 Broadway, New York, NY 10012", _
                .Latitude = 40.7221444, _
                .Longitude = -73.9992714, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "426 Broome Street, New York, NY 10013", _
                .Latitude = 40.7213295, _
                .Longitude = -73.9987121, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "City hall, nyc", _
                .Latitude = 40.7122066, _
                .Longitude = -74.0055026, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "South street seaport, nyc", _
                .Latitude = 40.7069501, _
                .Longitude = -74.0030848, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Ground zero, nyc", _
                .Latitude = 40.711641, _
                .Longitude = -74.012253, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Ground zero", _
                .Latitude = 40.711641, _
                .Longitude = -74.012253, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Mulberry and canal, NYC", _
                .Latitude = 40.71709, _
                .Longitude = -73.99859, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "World Trade Center, NYC", _
                .Latitude = 40.711667, _
                .Longitude = -74.0125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "South Street Seaport", _
                .Latitude = 40.7069501, _
                .Longitude = -74.0030848, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Wall Street and Nassau Street, NYC", _
                .Latitude = 40.70714, _
                .Longitude = -74.01069, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Trinity Church, NYC", _
                .Latitude = 40.7081269, _
                .Longitude = -74.0125691, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Federal Hall National Memorial", _
                .Latitude = 40.7069515, _
                .Longitude = -74.0101638, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Little Italy, NYC", _
                .Latitude = 40.719692, _
                .Longitude = -73.997765, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York, NY", _
                .Latitude = 40.71455, _
                .Longitude = -74.007125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York City, NY,", _
                .Latitude = 40.71455, _
                .Longitude = -74.007125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york,ny", _
                .Latitude = 40.71455, _
                .Longitude = -74.00713, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Odeon cinema", _
                .Latitude = 40.71683, _
                .Longitude = -74.00803, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York City", _
                .Latitude = 40.71455, _
                .Longitude = -74.00713, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "52 broadway, ny,ny 1004", _
                .Latitude = 40.7065, _
                .Longitude = -74.0123, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "52 broadway, ny,ny 10004", _
                .Latitude = 40.7065, _
                .Longitude = -74.0123, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "22 beaver st, ny,ny 10004", _
                .Latitude = 40.70482, _
                .Longitude = -74.01218, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "54 pine st,ny,ny 10005", _
                .Latitude = 40.70686, _
                .Longitude = -74.00849, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "114 liberty st, ny,ny 10006", _
                .Latitude = 40.70977, _
                .Longitude = -74.0122, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "215 canal st,ny,ny 10013", _
                .Latitude = 40.71747, _
                .Longitude = -73.99895, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york city ny", _
                .Latitude = 40.71455, _
                .Longitude = -74.00713, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "World Trade Center, New York, NY", _
                .Latitude = 40.71167, _
                .Longitude = -74.0125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Chinatown, New York, NY", _
                .Latitude = 40.71596, _
                .Longitude = -73.99741, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "101 murray street new york, ny", _
                .Latitude = 40.71526, _
                .Longitude = -74.01251, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "nyc", _
                .Latitude = 40.71455, _
                .Longitude = -74.00712, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "510 broadway new york", _
                .Latitude = 40.72234, _
                .Longitude = -73.999016, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "nyc", _
                .Latitude = 40.7145502, _
                .Longitude = -74.0071249, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Little Italy", _
                .Latitude = 40.719692, _
                .Longitude = -73.9977647, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "463 Broadway, New York, NY", _
                .Latitude = 40.721059, _
                .Longitude = -74.000688, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "222 West Broadway, New York, NY", _
                .Latitude = 40.719352, _
                .Longitude = -74.006417, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "270 Lafayette street new York new york", _
                .Latitude = 40.723879, _
                .Longitude = -73.996527, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "New York, NY USA", _
                .Latitude = 40.71455, _
                .Longitude = -74.007125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "97 Kenmare Street, New York, NY 10012", _
                .Latitude = 40.721437, _
                .Longitude = -73.996911, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "19 Beekman St, New York, New York 10038", _
                .Latitude = 40.710754, _
                .Longitude = -74.006287, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Soho", _
                .Latitude = 40.7241404, _
                .Longitude = -74.0020213, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Bergen, New York", _
                .Latitude = 40.71455, _
                .Longitude = -74.007125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "478 Broadway, NY, NY", _
                .Latitude = 40.721336, _
                .Longitude = -73.999771, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "555 broadway, ny, ny", _
                .Latitude = 40.723883, _
                .Longitude = -73.998296, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "375 West Broadway, NY, NY", _
                .Latitude = 40.7235, _
                .Longitude = -74.002602, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "35 howard st, NY, NY", _
                .Latitude = 40.719524, _
                .Longitude = -74.00103, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Pier 17 NYC", _
                .Latitude = 40.706366, _
                .Longitude = -74.002689, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "120 Liberty St NYC", _
                .Latitude = 40.709774, _
                .Longitude = -74.012451, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "80 White Street, NY, NY", _
                .Latitude = 40.717834, _
                .Longitude = -74.002052, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "Manhattan, NY", _
                .Latitude = 40.71443, _
                .Longitude = -74.0061, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "22 read st, ny", _
                .Latitude = 40.714201, _
                .Longitude = -74.004491, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "130 Mulberry St, New York, NY 10013-5547", _
                .Latitude = 40.718288, _
                .Longitude = -73.997711, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "new york city, ny", _
                .Latitude = 40.71455, _
                .Longitude = -74.007125, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "10038", _
                .Latitude = 40.7092119, _
                .Longitude = -74.0033631, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "11 Wall St, New York, NY 10005-1905", _
                .Latitude = 40.70729, _
                .Longitude = -74.011201, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "89 Reade St, New York, New York 10007", _
                .Latitude = 40.713456, _
                .Longitude = -74.003499, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "265 Canal St, New York, NY 10013-6010", _
                .Latitude = 40.718885, _
                .Longitude = -74.0009, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "39 Broadway, New York, NY 10006-3003", _
                .Latitude = 40.713345, _
                .Longitude = -73.996132, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "25 beaver street new york ny", _
                .Latitude = 40.705111, _
                .Longitude = -74.012007, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "100 church street new york ny", _
                .Latitude = 40.713043, _
                .Longitude = -74.009637, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "69 Mercer St, New York, NY 10012-4440", _
                .Latitude = 40.722649, _
                .Longitude = -74.00061, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "111 Worth St, New York, NY 10013-4008", _
                .Latitude = 40.715921, _
                .Longitude = -74.00341, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "240-248 Broadway, New York, New York 10038", _
                .Latitude = 40.712769, _
                .Longitude = -74.007681, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "12 Maiden Ln, New York, NY 10038-4002", _
                .Latitude = 40.709446, _
                .Longitude = -74.009576, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "291 Broadway, New York, NY 10007-1814", _
                .Latitude = 40.715, _
                .Longitude = -74.006134, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "55 Liberty St, New York, NY 10005-1003", _
                .Latitude = 40.708843, _
                .Longitude = -74.009384, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "Brooklyn Bridge, NY", _
                .Latitude = 40.706344, _
                .Longitude = -73.997439, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "wall street", _
                .Latitude = 40.7063889, _
                .Longitude = -74.0094444, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "south street seaport, ny", _
                .Latitude = 40.7069501, _
                .Longitude = -74.0030848, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "little italy, ny", _
                .Latitude = 40.719692, _
                .Longitude = -73.9977647, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "47 Pine St, New York, NY 10005-1513", _
                .Latitude = 40.706734, _
                .Longitude = -74.008928, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "22 cortlandt street new york ny", _
                .Latitude = 40.710082, _
                .Longitude = -74.010251, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "105 reade street new york ny", _
                .Latitude = 40.715633, _
                .Longitude = -74.008522, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "2 lafayette street new york ny", _
                .Latitude = 40.714031, _
                .Longitude = -74.003891, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "53 crosby street new york ny", _
                .Latitude = 40.721977, _
                .Longitude = -73.998245, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "2 Lafayette St, New York, NY 10007-1307", _
                .Latitude = 40.714031, _
                .Longitude = -74.003891, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "105 Reade St, New York, NY 10013-3840", _
                .Latitude = 40.715633, _
                .Longitude = -74.008522, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "chinatown, ny", _
                .Latitude = 40.7159556, _
                .Longitude = -73.9974133, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, _
                New Address() With { _
                .AddressString = "250 Broadway, New York, NY 10007-2516", _
                .Latitude = 40.713018, _
                .Longitude = -74.00747, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "156 William St, New York, NY 10038-2609", _
                .Latitude = 40.709797, _
                .Longitude = -74.005577, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "100 Church St, New York, NY 10007-2601", _
                .Latitude = 40.713043, _
                .Longitude = -74.009637, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }, New Address() With { _
                .AddressString = "33 Beaver St, New York, NY 10004-2736", _
                .Latitude = 40.705098, _
                .Longitude = -74.01172, _
                .Time = 0, _
                .TimeWindowStart = Nothing, _
                .TimeWindowEnd = Nothing _
            }}

            ' Set parameters


            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
                .RouteName = "Single Depot, Multiple Driver, No Time Window", _
                .StoreRoute = False, _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 60 * 60 * 7, _
                .RT = True, _
                .RouteMaxDuration = 86400, _
                .VehicleCapacity = "20", _
                .VehicleMaxDistanceMI = "99999", _
                .Parts = 4, _
                .Optimize = Optimize.Time.Description(), _
                .DistanceUnit = DistanceUnit.MI.Description(), _
                .DeviceType = DeviceType.Web.Description(), _
                .TravelMode = TravelMode.Driving.Description(), _
                .Metric = Metric.Geodesic _
            }

            Dim optimizationParameters As New OptimizationParameters() With { _
                .Addresses = addresses, _
                .Parameters = parameters _
            }

            ' Run the query
            Dim errorString As String = ""
            Dim dataObject As DataObject = route4Me.RunOptimization(optimizationParameters, errorString)

            ' Output the result
            PrintExampleOptimizationResult("SingleDepotMultipleDriverNoTimeWindow", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
