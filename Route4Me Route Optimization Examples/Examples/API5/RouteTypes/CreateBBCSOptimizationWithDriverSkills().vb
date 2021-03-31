Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManagerV5
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5

Namespace Route4MeSDKTest.Examples

    Partial Public NotInheritable Class Route4MeExamples
        Public Sub CreateBBCSOptimizationWithDriverSkills()

            Dim route4Me = New Route4MeManagerV5(ActualApiKey)
            Dim r4metobbcsid As Dictionary(Of String, String) = New Dictionary(Of String, String)()

            Dim ownerId As Integer? = GetOwnerMemberId()
            If ownerId Is Nothing Then Return

            Dim newMemberParameters1 = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "Vaishali",
                .MemberLastName = "Deshpande",
                .MemberCompany = "Test Member 1 Created",
                .MemberEmail = GetTestEmail().Replace("+", "1+"),
                .OwnerMemberId = CInt(ownerId)
            }

            newMemberParameters1.SetMemberType(DataTypes.V5.MemberTypes.Driver)

            Dim route4MeV5 = New Route4MeManagerV5(ActualApiKey)

            Dim resultResponse As DataTypes.V5.ResultResponse = Nothing

            Dim member1 = route4MeV5.CreateTeamMember(newMemberParameters1, resultResponse)

            If member1 IsNot Nothing AndAlso member1.[GetType]() = GetType(DataTypes.V5.TeamResponse) Then membersToRemove.Add(member1)

            Dim queryParams1 = New MemberQueryParameters() With {
                .UserId = member1.MemberId.ToString()
            }
            Dim skills1 As String() = New String() {"MLT3PLUMB", "MLT3AUDIT"}

            Dim updatedMember1 = route4MeV5.AddSkillsToDriver(queryParams1, skills1, resultResponse)

            Dim newMemberParameters2 = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "Guru",
                .MemberLastName = "Deshpande",
                .MemberCompany = "Test Member 2 Created",
                .MemberEmail = GetTestEmail().Replace("+", "1+"),
                .OwnerMemberId = CInt(ownerId)
            }

            newMemberParameters2.SetMemberType(DataTypes.V5.MemberTypes.Driver)

            route4MeV5 = New Route4MeManagerV5(ActualApiKey)

            Dim member2 = route4MeV5.CreateTeamMember(newMemberParameters2, resultResponse)

            If member2 IsNot Nothing AndAlso member2.[GetType]() = GetType(DataTypes.V5.TeamResponse) Then membersToRemove.Add(member2)

            Dim queryParams2 = New MemberQueryParameters() With {
                .UserId = member2.MemberId.ToString()
            }

            Dim skills2 As String() = New String() {"MLT3ELEC", "MLT3AUDIT"}

            Dim updatedMember2 = route4MeV5.AddSkillsToDriver(queryParams2, skills2, resultResponse)

            Dim addresses = New Address() {New Address() With {
                .AddressString = "Orion Tower 1, 19/2, Devarbisana halli, KR puram Hobli",
                .IsDepot = True,
                .Latitude = 12.924868075027042,
                .Longitude = 77.686757839656
            }, New Address() With {
                .AddressString = "Janhavi Shelters,Koppa Rd,Yelenahalli, Akshayanagar",
                .Latitude = 12.867198760559722,
                .Longitude = 77.619450599180254,
                .Time = 60 * 60,
                .TimeWindowStart = (10 - 5) * 3600,
                .TimeWindowEnd = (12 - 5) * 3600,
                .Tags = New String() {"MLT3PLUMB"}
            }, New Address() With {
                .AddressString = "Bannerghatta Main Rd, Classic Orchards Layout, Hulimavu, Bengaluru, Karnataka 560076",
                .Latitude = 12.874972927579234,
                .Longitude = 77.59465999732771,
                .Time = 60 * 60,
                .TimeWindowStart = (12 - 5) * 3600,
                .TimeWindowEnd = (14 - 5) * 3600,
                .Tags = New String() {"MLT3PLUMB"}
            }, New Address() With {
                .AddressString = "DLF Newtown,Akshayanagar",
                .Latitude = 12.877644069349397,
                .Longitude = 77.619053984078036,
                .Time = 60 * 60,
                .TimeWindowStart = (14 - 5) * 3600,
                .TimeWindowEnd = (16 - 5) * 3600,
                .Tags = New String() {"MLT3ELEC"}
            }, New Address() With {
                .AddressString = "9th Main, 6th Sector, HSR Layout, Bengaluru, Karnataka 560034",
                .Latitude = 12.914053742570712,
                .Longitude = 77.635100756852822,
                .Time = 60 * 60,
                .TimeWindowStart = (16 - 5) * 3600,
                .TimeWindowEnd = (18 - 5) * 3600,
                .Tags = New String() {"MLT3PLUMB"}
            }}

            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                .RouteName = "Multiple Depot, Multiple Driver Fixed " & DateTime.Now,
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * (9 - 5),
                .VehicleCapacity = 7,
                .VehicleMaxDistanceMI = 10000,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription(),
                .AdvancedConstraints = New DataTypes.V5.RouteAdvancedConstraints() _
                {
                    New DataTypes.V5.RouteAdvancedConstraints() With {
                        .AvailableTimeWindows = New List(Of Integer())() From {
                            New Integer() {60 * 60 * (9 - 5), 60 * 60 * (20 - 5)}
                        },
                        .MaximumCapacity = 30,
                        .MaximumCargoVolume = 15,
                        .Tags = New String() {"MLT3PLUMB"},
                        .Route4meMembersId = New Integer() {CInt(updatedMember1.MemberId)}
                    },
                    New DataTypes.V5.RouteAdvancedConstraints() With {
                        .AvailableTimeWindows = New List(Of Integer())() From {
                            New Integer() {60 * 60 * (9 - 5), 60 * 60 * (20 - 5)}
                        },
                        .MaximumCapacity = 30,
                        .MaximumCargoVolume = 15,
                        .MembersCount = 10,
                        .Tags = New String() {"MLT3ELEC"},
                        .Route4meMembersId = New Integer() {CInt(updatedMember2.MemberId)}
                    }
                }
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .Addresses = addresses,
                .Parameters = parameters
            }
            Dim errorResponse As ResultResponse = Nothing
            Dim dataObject As DataObject = route4Me.RunOptimization(optimizationParameters, errorResponse)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            Dim errorString As String = Nothing
            PrintExampleOptimizationResult(dataObject, errorString)

            RemoveTestOptimizations()
            RemoveTestTeamMembers()
        End Sub
    End Class
End Namespace
