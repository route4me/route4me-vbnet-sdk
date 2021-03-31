Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5.TelematicsPlatform
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSdkV5UnitTest.Route4MeSdkV5UnitTest.V5
Imports Xunit
Imports Xunit.Abstractions

Namespace Route4MeSdkV5UnitTest.TelematicsPlatformApi
    Public Class ConnectionsApiTests
        Implements IDisposable

        Shared c_ApiKey As String = ApiKeys.actualApiKey
        Shared lsTelematicsConnetions As List(Of Connection)

        Public Sub New(ByVal output As ITestOutputHelper)
            lsTelematicsConnetions = New List(Of Connection)()

            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim connectionParams = New ConnectionParameters() With {
                .Vendor = TelematicsVendorType.Tomtom.GetEnumDescription(),
                .VendorId = 154,
                .Name = "Connection to GeoTab",
                .Host = "telematics.tomtom.com/en_au/webfleet/",
                .ApiKey = "11111111111111111111111111111111",
                .AccountId = "SDS545454SASWEWA21DFFD54FGPPP456",
                .UserName = "John Doe",
                .Password = "11111",
                .VehiclePositionRefreshRate = 30,
                .ValidateRemoteCredentials = False,
                .IsEnabled = True,
                .Metadata = "string"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim telematicsConnetion = route4Me.RegisterTelematicsConnection(
                connectionParams,
                resultResponse)

            Assert.NotNull(telematicsConnetion)
            Assert.IsType(Of Connection)(telematicsConnetion)

            lsTelematicsConnetions.Add(telematicsConnetion)
        End Sub

        <Fact>
        Public Sub GetTelematicsConnectionsTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetTelematicsConnections(resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Connection())(result)
        End Sub

        <Fact>
        Public Sub GetTelematicsConnectionByTokenTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim connectionParams = New ConnectionParameters() With {
                .ConnectionToken = lsTelematicsConnetions(0).ConnectionToken
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.GetTelematicsConnectionByToken(connectionParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Connection)(result)
        End Sub

        <Fact>
        Public Sub RegisterTelematicsConnectionTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim connectionParams = New ConnectionParameters() With {
                .Vendor = TelematicsVendorType.Geotab.GetEnumDescription(),
                .VendorId = 154,
                .Name = "Connection to GeoTab",
                .Host = "https://www.geotab.com",
                .ApiKey = "11111111111111111111111111111111",
                .AccountId = "SDS545454SASWEWA21DFFD54FGPPP456",
                .UserName = "John Doe",
                .Password = "11111",
                .VehiclePositionRefreshRate = 30,
                .ValidateRemoteCredentials = False,
                .IsEnabled = True,
                .Metadata = "string"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.RegisterTelematicsConnection(connectionParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Connection)(result)

            lsTelematicsConnetions.Add(result)
        End Sub

        <Fact>
        Public Sub DeleteTelematicsConnectionTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim connectionParams = New ConnectionParameters() With {
                .ConnectionToken = lsTelematicsConnetions(lsTelematicsConnetions.Count - 1).ConnectionToken
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.DeleteTelematicsConnection(connectionParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Connection)(result)

            lsTelematicsConnetions.RemoveAt(lsTelematicsConnetions.Count - 1)
        End Sub

        <Fact>
        Public Sub UpdateTelematicsConnectionTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim connectionParams = New ConnectionParameters() With {
                .ConnectionToken = lsTelematicsConnetions(lsTelematicsConnetions.Count - 1).ConnectionToken,
                .Vendor = TelematicsVendorType.Geotab.GetEnumDescription(),
                .VendorId = 154,
                .Name = "Connection to GeoTab",
                .ApiKey = "11111111111111111111111111111111",
                .AccountId = "SDS545454SASWEWA21DFFD54FGPPP456",
                .UserName = "John Doe",
                .Password = "11111",
                .VehiclePositionRefreshRate = 60,
                .ValidateRemoteCredentials = False,
                .IsEnabled = True,
                .Metadata = "string"
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim result = route4Me.RegisterTelematicsConnection(connectionParams, resultResponse)

            Assert.NotNull(result)
            Assert.IsType(Of Connection)(result)
        End Sub

        Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
            If lsTelematicsConnetions.Count < 1 Then Return

            Dim route4Me = New Route4MeManagerV5(c_ApiKey)
            Dim resultResponse As ResultResponse = Nothing

            For Each connectionToken As Connection In lsTelematicsConnetions
                Dim connectionParams = New ConnectionParameters() With {
                    .ConnectionToken = connectionToken.ConnectionToken
                }

                Dim result = route4Me.DeleteTelematicsConnection(connectionParams, resultResponse)

                Assert.NotNull(result)
                Assert.IsType(Of Connection)(result)
            Next

            lsTelematicsConnetions = New List(Of Connection)()
        End Sub
    End Class
End Namespace
