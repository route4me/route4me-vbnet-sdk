' See video tutorial here: http://support.route4me.com/route-planning-help.php?id=manual0:tutorial2:chapter2:subchapter2

Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Function MultipleDepotMultipleDriverTimeWindow() As DataObject
            ' Create the manager with the api key
            Dim route4Me As New Route4MeManager(ActualApiKey)

            ' Prepare the addresses
            '#Region "Addresses"








            '#End Region
            Dim addresses As Address() = New Address() {New Address() With { _
                .AddressString = "455 S 4th St, Louisville, KY 40202", _
                .IsDepot = True, _
                .Latitude = 38.251698, _
                .Longitude = -85.757308, _
                .Time = 300, _
                .TimeWindowStart = 28800, _
                .TimeWindowEnd = 30477 _
            }, New Address() With { _
                .AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214", _
                .Latitude = 38.141598, _
                .Longitude = -85.793846, _
                .Time = 300, _
                .TimeWindowStart = 30477, _
                .TimeWindowEnd = 33406 _
            }, New Address() With { _
                .AddressString = "1407 א53MCCOY, Louisville, KY, 40215", _
                .Latitude = 38.202496, _
                .Longitude = -85.786514, _
                .Time = 300, _
                .TimeWindowStart = 33406, _
                .TimeWindowEnd = 36228 _
            }, New Address() With { _
                .AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215", _
                .Latitude = 38.178844, _
                .Longitude = -85.774864, _
                .Time = 300, _
                .TimeWindowStart = 36228, _
                .TimeWindowEnd = 37518 _
            }, New Address() With { _
                .AddressString = "730 CECIL AVENUE, Louisville, KY, 40211", _
                .Latitude = 38.248684, _
                .Longitude = -85.821121, _
                .Time = 300, _
                .TimeWindowStart = 37518, _
                .TimeWindowEnd = 39550 _
            }, New Address() With { _
                .AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211", _
                .Latitude = 38.251923, _
                .Longitude = -85.800034, _
                .Time = 300, _
                .TimeWindowStart = 39550, _
                .TimeWindowEnd = 41348 _
            }, _
                New Address() With { _
                .AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216", _
                .Latitude = 38.176067, _
                .Longitude = -85.824638, _
                .Time = 300, _
                .TimeWindowStart = 41348, _
                .TimeWindowEnd = 42261 _
            }, New Address() With { _
                .AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215", _
                .Latitude = 38.179806, _
                .Longitude = -85.775558, _
                .Time = 300, _
                .TimeWindowStart = 42261, _
                .TimeWindowEnd = 45195 _
            }, New Address() With { _
                .AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212", _
                .Latitude = 38.259335, _
                .Longitude = -85.815094, _
                .Time = 300, _
                .TimeWindowStart = 45195, _
                .TimeWindowEnd = 46549 _
            }, New Address() With { _
                .AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215", _
                .Latitude = 38.179253, _
                .Longitude = -85.785118, _
                .Time = 300, _
                .TimeWindowStart = 46549, _
                .TimeWindowEnd = 47353 _
            }, New Address() With { _
                .AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214", _
                .Latitude = 38.162472, _
                .Longitude = -85.792854, _
                .Time = 300, _
                .TimeWindowStart = 47353, _
                .TimeWindowEnd = 50924 _
            }, New Address() With { _
                .AddressString = "1661 W HILL ST, Louisville, KY, 40210", _
                .Latitude = 38.229584, _
                .Longitude = -85.783966, _
                .Time = 300, _
                .TimeWindowStart = 50924, _
                .TimeWindowEnd = 51392 _
            }, _
                New Address() With { _
                .AddressString = "3222 KINGSWOOD WAY, Louisville, KY, 40216", _
                .Latitude = 38.210606, _
                .Longitude = -85.822594, _
                .Time = 300, _
                .TimeWindowStart = 51392, _
                .TimeWindowEnd = 52451 _
            }, New Address() With { _
                .AddressString = "1922 PALATKA RD, Louisville, KY, 40214", _
                .Latitude = 38.153767, _
                .Longitude = -85.796783, _
                .Time = 300, _
                .TimeWindowStart = 52451, _
                .TimeWindowEnd = 55631 _
            }, New Address() With { _
                .AddressString = "1314 SOUTH 26TH STREET, Louisville, KY, 40210", _
                .Latitude = 38.235847, _
                .Longitude = -85.796852, _
                .Time = 300, _
                .TimeWindowStart = 55631, _
                .TimeWindowEnd = 58516 _
            }, New Address() With { _
                .AddressString = "2135 MCCLOSKEY AVENUE, Louisville, KY, 40210", _
                .Latitude = 38.218662, _
                .Longitude = -85.789032, _
                .Time = 300, _
                .TimeWindowStart = 58516, _
                .TimeWindowEnd = 61080 _
            }, New Address() With { _
                .AddressString = "1409 PHYLLIS AVE, Louisville, KY, 40215", _
                .Latitude = 38.206154, _
                .Longitude = -85.781387, _
                .Time = 100, _
                .TimeWindowStart = 61080, _
                .TimeWindowEnd = 61504 _
            }, New Address() With { _
                .AddressString = "4504 SUNFLOWER AVE, Louisville, KY, 40216", _
                .Latitude = 38.187511, _
                .Longitude = -85.839149, _
                .Time = 300, _
                .TimeWindowStart = 61504, _
                .TimeWindowEnd = 62061 _
            }, _
                New Address() With { _
                .AddressString = "2512 GREENWOOD AVE, Louisville, KY, 40210", _
                .Latitude = 38.241405, _
                .Longitude = -85.795059, _
                .Time = 300, _
                .TimeWindowStart = 62061, _
                .TimeWindowEnd = 65012 _
            }, New Address() With { _
                .AddressString = "5500 WILKE FARM AVE, Louisville, KY, 40216", _
                .Latitude = 38.166065, _
                .Longitude = -85.863319, _
                .Time = 300, _
                .TimeWindowStart = 65012, _
                .TimeWindowEnd = 67541 _
            }, New Address() With { _
                .AddressString = "3640 LENTZ AVE, Louisville, KY, 40215", _
                .Latitude = 38.193283, _
                .Longitude = -85.786201, _
                .Time = 300, _
                .TimeWindowStart = 67541, _
                .TimeWindowEnd = 69120 _
            }, New Address() With { _
                .AddressString = "1020 BLUEGRASS AVE, Louisville, KY, 40215", _
                .Latitude = 38.17952, _
                .Longitude = -85.780037, _
                .Time = 300, _
                .TimeWindowStart = 69120, _
                .TimeWindowEnd = 70572 _
            }, New Address() With { _
                .AddressString = "123 NORTH 40TH ST, Louisville, KY, 40212", _
                .Latitude = 38.26498, _
                .Longitude = -85.814156, _
                .Time = 300, _
                .TimeWindowStart = 70572, _
                .TimeWindowEnd = 73177 _
            }, New Address() With { _
                .AddressString = "7315 ST ANDREWS WOODS CIRCLE UNIT 104, Louisville, KY, 40214", _
                .Latitude = 38.151072, _
                .Longitude = -85.802867, _
                .Time = 300, _
                .TimeWindowStart = 73177, _
                .TimeWindowEnd = 75231 _
            }, _
                New Address() With { _
                .AddressString = "3210 POPLAR VIEW DR, Louisville, KY, 40216", _
                .Latitude = 38.182594, _
                .Longitude = -85.849937, _
                .Time = 300, _
                .TimeWindowStart = 75231, _
                .TimeWindowEnd = 77663 _
            }, New Address() With { _
                .AddressString = "4519 LOUANE WAY, Louisville, KY, 40216", _
                .Latitude = 38.1754, _
                .Longitude = -85.811447, _
                .Time = 300, _
                .TimeWindowStart = 77663, _
                .TimeWindowEnd = 79796 _
            }, New Address() With { _
                .AddressString = "6812 MANSLICK RD, Louisville, KY, 40214", _
                .Latitude = 38.161839, _
                .Longitude = -85.798279, _
                .Time = 300, _
                .TimeWindowStart = 79796, _
                .TimeWindowEnd = 80813 _
            }, New Address() With { _
                .AddressString = "1524 HUNTOON AVENUE, Louisville, KY, 40215", _
                .Latitude = 38.172031, _
                .Longitude = -85.788353, _
                .Time = 300, _
                .TimeWindowStart = 80813, _
                .TimeWindowEnd = 83956 _
            }, New Address() With { _
                .AddressString = "1307 LARCHMONT AVE, Louisville, KY, 40215", _
                .Latitude = 38.209663, _
                .Longitude = -85.779816, _
                .Time = 300, _
                .TimeWindowStart = 83956, _
                .TimeWindowEnd = 84365 _
            }, New Address() With { _
                .AddressString = "434 N 26TH STREET #2, Louisville, KY, 40212", _
                .Latitude = 38.26844, _
                .Longitude = -85.791962, _
                .Time = 300, _
                .TimeWindowStart = 84365, _
                .TimeWindowEnd = 85367 _
            }, _
                New Address() With { _
                .AddressString = "678 WESTLAWN ST, Louisville, KY, 40211", _
                .Latitude = 38.250397, _
                .Longitude = -85.80629, _
                .Time = 300, _
                .TimeWindowStart = 85367, _
                .TimeWindowEnd = 86400 _
            }, New Address() With { _
                .AddressString = "2308 W BROADWAY, Louisville, KY, 40211", _
                .Latitude = 38.248882, _
                .Longitude = -85.790421, _
                .Time = 300, _
                .TimeWindowStart = 86400, _
                .TimeWindowEnd = 88703 _
            }, New Address() With { _
                .AddressString = "2332 WOODLAND AVE, Louisville, KY, 40210", _
                .Latitude = 38.233579, _
                .Longitude = -85.794257, _
                .Time = 300, _
                .TimeWindowStart = 88703, _
                .TimeWindowEnd = 89320 _
            }, New Address() With { _
                .AddressString = "1706 WEST ST. CATHERINE, Louisville, KY, 40210", _
                .Latitude = 38.239697, _
                .Longitude = -85.783928, _
                .Time = 300, _
                .TimeWindowStart = 89320, _
                .TimeWindowEnd = 90054 _
            }, New Address() With { _
                .AddressString = "1699 WATHEN LN, Louisville, KY, 40216", _
                .Latitude = 38.216465, _
                .Longitude = -85.792397, _
                .Time = 300, _
                .TimeWindowStart = 90054, _
                .TimeWindowEnd = 91150 _
            }, New Address() With { _
                .AddressString = "2416 SUNSHINE WAY, Louisville, KY, 40216", _
                .Latitude = 38.186245, _
                .Longitude = -85.831787, _
                .Time = 300, _
                .TimeWindowStart = 91150, _
                .TimeWindowEnd = 91915 _
            }, _
                New Address() With { _
                .AddressString = "6925 MANSLICK RD, Louisville, KY, 40214", _
                .Latitude = 38.158466, _
                .Longitude = -85.798355, _
                .Time = 300, _
                .TimeWindowStart = 91915, _
                .TimeWindowEnd = 93407 _
            }, New Address() With { _
                .AddressString = "2707 7TH ST, Louisville, KY, 40215", _
                .Latitude = 38.212438, _
                .Longitude = -85.785082, _
                .Time = 300, _
                .TimeWindowStart = 93407, _
                .TimeWindowEnd = 95992 _
            }, New Address() With { _
                .AddressString = "2014 KENDALL LN, Louisville, KY, 40216", _
                .Latitude = 38.179394, _
                .Longitude = -85.826668, _
                .Time = 300, _
                .TimeWindowStart = 95992, _
                .TimeWindowEnd = 99307 _
            }, New Address() With { _
                .AddressString = "612 N 39TH ST, Louisville, KY, 40212", _
                .Latitude = 38.273354, _
                .Longitude = -85.812012, _
                .Time = 300, _
                .TimeWindowStart = 99307, _
                .TimeWindowEnd = 102906 _
            }, New Address() With { _
                .AddressString = "2215 ROWAN ST, Louisville, KY, 40212", _
                .Latitude = 38.261703, _
                .Longitude = -85.786781, _
                .Time = 300, _
                .TimeWindowStart = 102906, _
                .TimeWindowEnd = 106021 _
            }, New Address() With { _
                .AddressString = "1826 W. KENTUCKY ST, Louisville, KY, 40210", _
                .Latitude = 38.241611, _
                .Longitude = -85.78653, _
                .Time = 300, _
                .TimeWindowStart = 106021, _
                .TimeWindowEnd = 107276 _
            }, _
                New Address() With { _
                .AddressString = "1810 GREGG AVE, Louisville, KY, 40210", _
                .Latitude = 38.224716, _
                .Longitude = -85.796211, _
                .Time = 300, _
                .TimeWindowStart = 107276, _
                .TimeWindowEnd = 107948 _
            }, New Address() With { _
                .AddressString = "4103 BURRRELL DRIVE, Louisville, KY, 40216", _
                .Latitude = 38.191753, _
                .Longitude = -85.825836, _
                .Time = 300, _
                .TimeWindowStart = 107948, _
                .TimeWindowEnd = 108414 _
            }, New Address() With { _
                .AddressString = "359 SOUTHWESTERN PKWY, Louisville, KY, 40212", _
                .Latitude = 38.259903, _
                .Longitude = -85.823463, _
                .Time = 200, _
                .TimeWindowStart = 108414, _
                .TimeWindowEnd = 108685 _
            }, New Address() With { _
                .AddressString = "2407 W CHESTNUT ST, Louisville, KY, 40211", _
                .Latitude = 38.252781, _
                .Longitude = -85.792109, _
                .Time = 300, _
                .TimeWindowStart = 108685, _
                .TimeWindowEnd = 110109 _
            }, New Address() With { _
                .AddressString = "225 S 22ND ST, Louisville, KY, 40212", _
                .Latitude = 38.257616, _
                .Longitude = -85.786658, _
                .Time = 300, _
                .TimeWindowStart = 110109, _
                .TimeWindowEnd = 111375 _
            }, New Address() With { _
                .AddressString = "1404 MCCOY AVE, Louisville, KY, 40215", _
                .Latitude = 38.202122, _
                .Longitude = -85.786072, _
                .Time = 300, _
                .TimeWindowStart = 111375, _
                .TimeWindowEnd = 112120 _
            }, _
                New Address() With { _
                .AddressString = "117 FOUNT LANDING CT, Louisville, KY, 40212", _
                .Latitude = 38.270061, _
                .Longitude = -85.799438, _
                .Time = 300, _
                .TimeWindowStart = 112120, _
                .TimeWindowEnd = 114095 _
            }, New Address() With { _
                .AddressString = "5504 SHOREWOOD DRIVE, Louisville, KY, 40214", _
                .Latitude = 38.145851, _
                .Longitude = -85.7798, _
                .Time = 300, _
                .TimeWindowStart = 114095, _
                .TimeWindowEnd = 115743 _
            }, New Address() With { _
                .AddressString = "1406 CENTRAL AVE, Louisville, KY, 40208", _
                .Latitude = 38.211025, _
                .Longitude = -85.780251, _
                .Time = 300, _
                .TimeWindowStart = 115743, _
                .TimeWindowEnd = 117716 _
            }, New Address() With { _
                .AddressString = "901 W WHITNEY AVE, Louisville, KY, 40215", _
                .Latitude = 38.194115, _
                .Longitude = -85.77494, _
                .Time = 300, _
                .TimeWindowStart = 117716, _
                .TimeWindowEnd = 119078 _
            }, New Address() With { _
                .AddressString = "2109 SCHAFFNER AVE, Louisville, KY, 40210", _
                .Latitude = 38.219699, _
                .Longitude = -85.779363, _
                .Time = 300, _
                .TimeWindowStart = 119078, _
                .TimeWindowEnd = 121147 _
            }, New Address() With { _
                .AddressString = "2906 DIXIE HWY, Louisville, KY, 40216", _
                .Latitude = 38.209278, _
                .Longitude = -85.798653, _
                .Time = 300, _
                .TimeWindowStart = 121147, _
                .TimeWindowEnd = 124281 _
            }, _
                New Address() With { _
                .AddressString = "814 WWHITNEY AVE, Louisville, KY, 40215", _
                .Latitude = 38.193596, _
                .Longitude = -85.773521, _
                .Time = 300, _
                .TimeWindowStart = 124281, _
                .TimeWindowEnd = 124675 _
            }, New Address() With { _
                .AddressString = "1610 ALGONQUIN PWKY, Louisville, KY, 40210", _
                .Latitude = 38.222153, _
                .Longitude = -85.784187, _
                .Time = 300, _
                .TimeWindowStart = 124675, _
                .TimeWindowEnd = 127148 _
            }, New Address() With { _
                .AddressString = "3524 WHEELER AVE, Louisville, KY, 40215", _
                .Latitude = 38.195293, _
                .Longitude = -85.788643, _
                .Time = 300, _
                .TimeWindowStart = 127148, _
                .TimeWindowEnd = 130667 _
            }, New Address() With { _
                .AddressString = "5009 NEW CUT RD, Louisville, KY, 40214", _
                .Latitude = 38.165905, _
                .Longitude = -85.779701, _
                .Time = 300, _
                .TimeWindowStart = 130667, _
                .TimeWindowEnd = 131980 _
            }, New Address() With { _
                .AddressString = "3122 ELLIOTT AVE, Louisville, KY, 40211", _
                .Latitude = 38.251213, _
                .Longitude = -85.804199, _
                .Time = 300, _
                .TimeWindowStart = 131980, _
                .TimeWindowEnd = 134402 _
            }, New Address() With { _
                .AddressString = "911 GAGEL AVE, Louisville, KY, 40216", _
                .Latitude = 38.173512, _
                .Longitude = -85.807854, _
                .Time = 300, _
                .TimeWindowStart = 134402, _
                .TimeWindowEnd = 136787 _
            }, _
                New Address() With { _
                .AddressString = "4020 GARLAND AVE #lOOA, Louisville, KY, 40211", _
                .Latitude = 38.246181, _
                .Longitude = -85.818901, _
                .Time = 300, _
                .TimeWindowStart = 136787, _
                .TimeWindowEnd = 138073 _
            }, New Address() With { _
                .AddressString = "5231 MT HOLYOKE DR, Louisville, KY, 40216", _
                .Latitude = 38.169369, _
                .Longitude = -85.85704, _
                .Time = 300, _
                .TimeWindowStart = 138073, _
                .TimeWindowEnd = 141407 _
            }, New Address() With { _
                .AddressString = "1339 28TH S #2, Louisville, KY, 40211", _
                .Latitude = 38.235275, _
                .Longitude = -85.800156, _
                .Time = 300, _
                .TimeWindowStart = 141407, _
                .TimeWindowEnd = 143561 _
            }, New Address() With { _
                .AddressString = "836 S 36TH ST, Louisville, KY, 40211", _
                .Latitude = 38.24651, _
                .Longitude = -85.811234, _
                .Time = 300, _
                .TimeWindowStart = 143561, _
                .TimeWindowEnd = 145941 _
            }, New Address() With { _
                .AddressString = "2132 DUNCAN STREET, Louisville, KY, 40212", _
                .Latitude = 38.262135, _
                .Longitude = -85.785172, _
                .Time = 300, _
                .TimeWindowStart = 145941, _
                .TimeWindowEnd = 148296 _
            }, New Address() With { _
                .AddressString = "3529 WHEELER AVE, Louisville, KY, 40215", _
                .Latitude = 38.195057, _
                .Longitude = -85.787949, _
                .Time = 300, _
                .TimeWindowStart = 148296, _
                .TimeWindowEnd = 150177 _
            }, _
                New Address() With { _
                .AddressString = "2829 DE MEL #11, Louisville, KY, 40214", _
                .Latitude = 38.171662, _
                .Longitude = -85.807271, _
                .Time = 300, _
                .TimeWindowStart = 150177, _
                .TimeWindowEnd = 150981 _
            }, New Address() With { _
                .AddressString = "1325 EARL AVENUE, Louisville, KY, 40215", _
                .Latitude = 38.204556, _
                .Longitude = -85.781555, _
                .Time = 300, _
                .TimeWindowStart = 150981, _
                .TimeWindowEnd = 151854 _
            }, New Address() With { _
                .AddressString = "3632 MANSLICK RD #10, Louisville, KY, 40215", _
                .Latitude = 38.193542, _
                .Longitude = -85.801147, _
                .Time = 300, _
                .TimeWindowStart = 151854, _
                .TimeWindowEnd = 152613 _
            }, New Address() With { _
                .AddressString = "637 S 41ST ST, Louisville, KY, 40211", _
                .Latitude = 38.253632, _
                .Longitude = -85.81897, _
                .Time = 300, _
                .TimeWindowStart = 152613, _
                .TimeWindowEnd = 156131 _
            }, New Address() With { _
                .AddressString = "3420 VIRGINIA AVENUE, Louisville, KY, 40211", _
                .Latitude = 38.238693, _
                .Longitude = -85.811386, _
                .Time = 300, _
                .TimeWindowStart = 156131, _
                .TimeWindowEnd = 157212 _
            }, New Address() With { _
                .AddressString = "3501 MALIBU CT APT 6, Louisville, KY, 40216", _
                .Latitude = 38.166481, _
                .Longitude = -85.825928, _
                .Time = 300, _
                .TimeWindowStart = 157212, _
                .TimeWindowEnd = 158655 _
            }, _
                New Address() With { _
                .AddressString = "4912 DIXIE HWY, Louisville, KY, 40216", _
                .Latitude = 38.170728, _
                .Longitude = -85.826817, _
                .Time = 300, _
                .TimeWindowStart = 158655, _
                .TimeWindowEnd = 159145 _
            }, New Address() With { _
                .AddressString = "7720 DINGLEDELL RD, Louisville, KY, 40214", _
                .Latitude = 38.162472, _
                .Longitude = -85.792854, _
                .Time = 300, _
                .TimeWindowStart = 159145, _
                .TimeWindowEnd = 161831 _
            }, New Address() With { _
                .AddressString = "2123 RATCLIFFE AVE, Louisville, KY, 40210", _
                .Latitude = 38.21978, _
                .Longitude = -85.797615, _
                .Time = 300, _
                .TimeWindowStart = 161831, _
                .TimeWindowEnd = 163705 _
            }, New Address() With { _
                .AddressString = "1321 OAKWOOD AVE, Louisville, KY, 40215", _
                .Latitude = 38.17704, _
                .Longitude = -85.783829, _
                .Time = 300, _
                .TimeWindowStart = 163705, _
                .TimeWindowEnd = 164953 _
            }, New Address() With { _
                .AddressString = "2223 WEST KENTUCKY STREET, Louisville, KY, 40210", _
                .Latitude = 38.242516, _
                .Longitude = -85.790695, _
                .Time = 300, _
                .TimeWindowStart = 164953, _
                .TimeWindowEnd = 166189 _
            }, New Address() With { _
                .AddressString = "8025 GLIMMER WAY #3308, Louisville, KY, 40214", _
                .Latitude = 38.131981, _
                .Longitude = -85.77935, _
                .Time = 300, _
                .TimeWindowStart = 166189, _
                .TimeWindowEnd = 166640 _
            }, _
                New Address() With { _
                .AddressString = "1155 S 28TH ST, Louisville, KY, 40211", _
                .Latitude = 38.238621, _
                .Longitude = -85.799911, _
                .Time = 300, _
                .TimeWindowStart = 166640, _
                .TimeWindowEnd = 168147 _
            }, New Address() With { _
                .AddressString = "840 IROQUOIS AVE, Louisville, KY, 40214", _
                .Latitude = 38.166355, _
                .Longitude = -85.779396, _
                .Time = 300, _
                .TimeWindowStart = 168147, _
                .TimeWindowEnd = 170385 _
            }, New Address() With { _
                .AddressString = "5573 BRUCE AVE, Louisville, KY, 40214", _
                .Latitude = 38.145222, _
                .Longitude = -85.779205, _
                .Time = 300, _
                .TimeWindowStart = 170385, _
                .TimeWindowEnd = 171096 _
            }, New Address() With { _
                .AddressString = "1727 GALLAGHER, Louisville, KY, 40210", _
                .Latitude = 38.239334, _
                .Longitude = -85.784882, _
                .Time = 300, _
                .TimeWindowStart = 171096, _
                .TimeWindowEnd = 171951 _
            }, New Address() With { _
                .AddressString = "1309 CATALPA ST APT 204, Louisville, KY, 40211", _
                .Latitude = 38.236524, _
                .Longitude = -85.801619, _
                .Time = 300, _
                .TimeWindowStart = 171951, _
                .TimeWindowEnd = 172393 _
            }, New Address() With { _
                .AddressString = "1330 ALGONQUIN PKWY, Louisville, KY, 40208", _
                .Latitude = 38.219846, _
                .Longitude = -85.777344, _
                .Time = 300, _
                .TimeWindowStart = 172393, _
                .TimeWindowEnd = 175337 _
            }, _
                New Address() With { _
                .AddressString = "823 SUTCLIFFE, Louisville, KY, 40211", _
                .Latitude = 38.246956, _
                .Longitude = -85.811569, _
                .Time = 300, _
                .TimeWindowStart = 175337, _
                .TimeWindowEnd = 176867 _
            }, New Address() With { _
                .AddressString = "4405 CHURCHMAN AVENUE #2, Louisville, KY, 40215", _
                .Latitude = 38.177768, _
                .Longitude = -85.792545, _
                .Time = 300, _
                .TimeWindowStart = 176867, _
                .TimeWindowEnd = 178051 _
            }, New Address() With { _
                .AddressString = "3211 DUMESNIL ST #1, Louisville, KY, 40211", _
                .Latitude = 38.237789, _
                .Longitude = -85.807968, _
                .Time = 300, _
                .TimeWindowStart = 178051, _
                .TimeWindowEnd = 179083 _
            }, New Address() With { _
                .AddressString = "3904 WEWOKA AVE, Louisville, KY, 40212", _
                .Latitude = 38.270367, _
                .Longitude = -85.813118, _
                .Time = 300, _
                .TimeWindowStart = 179083, _
                .TimeWindowEnd = 181543 _
            }, New Address() With { _
                .AddressString = "660 SO. 42ND STREET, Louisville, KY, 40211", _
                .Latitude = 38.252865, _
                .Longitude = -85.822624, _
                .Time = 300, _
                .TimeWindowStart = 181543, _
                .TimeWindowEnd = 184193 _
            }, New Address() With { _
                .AddressString = "3619  LENTZ  AVE, Louisville, KY, 40215", _
                .Latitude = 38.193249, _
                .Longitude = -85.785492, _
                .Time = 300, _
                .TimeWindowStart = 184193, _
                .TimeWindowEnd = 185853 _
            }, _
                New Address() With { _
                .AddressString = "4305  STOLTZ  CT, Louisville, KY, 40215", _
                .Latitude = 38.178707, _
                .Longitude = -85.787292, _
                .Time = 300, _
                .TimeWindowStart = 185853, _
                .TimeWindowEnd = 187252 _
            }}

            ' Set parameters


            Dim parameters As New RouteParameters() With { _
                .AlgorithmType = AlgorithmType.CVRP_TW_MD, _
                .RouteName = "Multiple Depot, Multiple Driver, Time Window", _
                .StoreRoute = False, _
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.[Date].AddDays(1)), _
                .RouteTime = 60 * 60 * 7, _
                .RT = True, _
                .RouteMaxDuration = 86400 * 3, _
                .VehicleCapacity = "99", _
                .VehicleMaxDistanceMI = "99999", _
                .Optimize = EnumHelper.GetEnumDescription(Optimize.Time), _
                .DistanceUnit = EnumHelper.GetEnumDescription(DistanceUnit.MI), _
                .DeviceType = EnumHelper.GetEnumDescription(DeviceType.Web), _
                .TravelMode = EnumHelper.GetEnumDescription(TravelMode.Driving), _
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
            PrintExampleOptimizationResult("MultipleDepotMultipleDriverTimeWindow", dataObject, errorString)

            Return dataObject
        End Function
    End Class
End Namespace
