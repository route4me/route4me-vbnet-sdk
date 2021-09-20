Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSdkV5UnitTest.Route4MeSdkV5UnitTest.V5
Imports Xunit
Imports Xunit.Abstractions

Namespace Route4MeSdkV5UnitTest.AddressBookContactApi
    Public Class AddressBookContactApiTests
        Shared c_ApiKey As String = ApiKeys.actualApiKey
        Private ReadOnly _output As ITestOutputHelper
        Shared lsCreatedContacts As List(Of AddressBookContact)

        Public Sub New(ByVal output As ITestOutputHelper)
            If ApiKeys.actualApiKey = ApiKeys.demoApiKey Then
                Return
            End If

            _output = output

            Dim route4Me = New Route4MeManagerV5(c_ApiKey)
            lsCreatedContacts = New List(Of AddressBookContact)()

            Dim contactParams = New AddressBookContact() With {
                .FirstName = "Test FirstName " & (New Random()).[Next]().ToString(),
                .Address1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .CachedLat = 38.024654,
                .CachedLng = -77.338814,
                .AddressStopType = AddressStopType.PickUp.GetEnumDescription()
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim contact1 = route4Me.AddAddressBookContact(contactParams, resultResponse)

            lsCreatedContacts.Add(contact1)

            contactParams = New AddressBookContact() With {
                .FirstName = "Test FirstName " & (New Random()).[Next]().ToString(),
                .Address1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .CachedLat = 38.024664,
                .CachedLng = -77.338834,
                .AddressStopType = AddressStopType.PickUp.GetEnumDescription()
            }

            Dim contact2 = route4Me.AddAddressBookContact(contactParams, resultResponse)

            lsCreatedContacts.Add(contact2)

            contactParams = New AddressBookContact() With {
                .FirstName = "Test FirstName " & (New Random()).[Next]().ToString(),
                .Address1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .CachedLat = 38.024684,
                .CachedLng = -77.338854,
                .AddressStopType = AddressStopType.PickUp.GetEnumDescription()
            }

            Dim contact3 = route4Me.AddAddressBookContact(contactParams, resultResponse)
            lsCreatedContacts.Add(contact3)
        End Sub

        Public Sub Dispose()
            If ApiKeys.actualApiKey = ApiKeys.demoApiKey Then
                Return
            End If

            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim lsRemLocations = New List(Of String)()
            Dim resultResponse As ResultResponse = Nothing

            If lsCreatedContacts.Count > 0 Then
                Dim contactIDs = lsCreatedContacts.Where(Function(x) (x IsNot Nothing AndAlso x.AddressId IsNot Nothing)).[Select](Function(x) CInt((x.AddressId)))

                Dim removed As Boolean = route4Me.RemoveAddressBookContacts(contactIDs.ToArray(), resultResponse)

                Assert.Null(resultResponse)
            End If
        End Sub

        <IgnoreOnWindowsFactAttribute>
        Public Sub GetAddressBookContactsTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim addressBookParameters = New AddressBookParameters() With {
                .Limit = 1,
                .Offset = 16
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim response = route4Me.GetAddressBookContacts(addressBookParameters, resultResponse)

            Assert.IsType(Of AddressBookContactsResponse)(response)
        End Sub

        <IgnoreOnWindowsFactAttribute>
        Public Sub GetAddressBookContactsByIDsTest()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim addressId1 As Integer = CInt(lsCreatedContacts(lsCreatedContacts.Count - 1).AddressId)
            Dim addressId2 As Integer = CInt(lsCreatedContacts(lsCreatedContacts.Count - 2).AddressId)
            Dim addressIDs As Integer() = New Integer() {addressId1, addressId2}

            Dim resultResponse As ResultResponse = Nothing
            Dim response = route4Me.GetAddressBookContactsByIds(addressIDs, resultResponse)

            Assert.IsType(Of AddressBookContactsResponse)(response)
        End Sub

        <IgnoreOnWindowsFactAttribute>
        Public Sub DeleteAddressBookContacts()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim lsSize = lsCreatedContacts.Count - 1
            Dim addressIDs As Integer() = New Integer() {
                CInt(lsCreatedContacts(lsSize - 1).AddressId),
                CInt(lsCreatedContacts(lsSize - 2).AddressId)
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim response = route4Me.RemoveAddressBookContacts(addressIDs, resultResponse)

            Assert.Null(resultResponse)

            lsCreatedContacts.RemoveAt(lsSize - 1)
            lsCreatedContacts.RemoveAt(lsSize - 2)
        End Sub

        <IgnoreOnWindowsFactAttribute>
        Public Sub CreateAddressBookContact()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim contactParams = New AddressBookContact() With {
                .FirstName = "Test FirstName " & (New Random()).[Next]().ToString(),
                .Address1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                .CachedLat = 38.024654,
                .CachedLng = -77.338814,
                .AddressStopType = AddressStopType.PickUp.GetEnumDescription()
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim contact = route4Me.AddAddressBookContact(contactParams, resultResponse)

            Assert.IsType(Of AddressBookContact)(contact)

            lsCreatedContacts.Add(contact)
        End Sub

        <IgnoreOnWindowsFactAttribute>
        Public Sub BatchCreatingAddressBookContacts()
            Dim route4Me = New Route4MeManagerV5(c_ApiKey)

            Dim lsContacts = New List(Of AddressBookContact)() From {
                New AddressBookContact() With {
                    .FirstName = "Test FirstName " & (New Random()).[Next]().ToString(),
                    .Address1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                    .CachedLat = 38.024754,
                    .CachedLng = -77.338914,
                    .AddressStopType = AddressStopType.PickUp.GetEnumDescription()
                },
                New AddressBookContact() With {
                    .FirstName = "Test FirstName " & (New Random()).[Next]().ToString(),
                    .Address1 = "Test Address1 " & (New Random()).[Next]().ToString(),
                    .CachedLat = 38.024554,
                    .CachedLng = -77.338714,
                    .AddressStopType = AddressStopType.PickUp.GetEnumDescription()
                }
            }

            Dim mandatoryFields = New String() {
                R4MeUtils.GetPropertyName(Function() lsContacts(0).FirstName),
                R4MeUtils.GetPropertyName(Function() lsContacts(0).Address1),
                R4MeUtils.GetPropertyName(Function() lsContacts(0).CachedLat),
                R4MeUtils.GetPropertyName(Function() lsContacts(0).CachedLng),
                R4MeUtils.GetPropertyName(Function() lsContacts(0).AddressStopType)
            }

            Dim contactParams = New Route4MeManagerV5.BatchCreatingAddressBookContactsRequest() With {
                .Data = lsContacts.ToArray()
            }

            Dim resultResponse As ResultResponse = Nothing
            Dim response = route4Me.BatchCreateAdressBookContacts(contactParams, mandatoryFields, resultResponse)

            Assert.IsType(Of StatusResponse)(response)
            Assert.[True](response.status)
        End Sub

    End Class
End Namespace