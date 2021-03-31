Imports Route4MeSDKLibrary.Route4MeSDK
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes.V5
Imports Route4MeSDKLibrary.Route4MeSDK.Route4MeManagerV5
Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes.V5

Namespace Route4MeSDKTest.Examples
    Partial Public NotInheritable Class Route4MeExamples
        Public Sub CreateOptimizationWithDriverSkills()
            Dim route4Me = New Route4MeManagerV5(ActualApiKey)

            Dim ownerId As Integer? = GetOwnerMemberId()
            If ownerId Is Nothing Then Return

            Dim newMemberParameters1 = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "John1",
                .MemberLastName = "Doe1",
                .MemberCompany = "Test Member Created",
                .MemberEmail = GetTestEmail().Replace("+", "1+"),
                .OwnerMemberId = CInt(ownerId)
            }

            newMemberParameters1.SetMemberType(MemberTypes.Driver)

            Dim route4MeV5 = New Route4MeManagerV5(ActualApiKey)
            Dim resultResponse As ResultResponse = Nothing

            Dim member1 = route4MeV5.CreateTeamMember(newMemberParameters1, resultResponse)
            If member1 IsNot Nothing AndAlso member1.[GetType]() = GetType(TeamResponse) Then membersToRemove.Add(member1)

            Dim queryParams1 = New MemberQueryParameters() With {
                .UserId = member1.MemberId.ToString()
            }

            Dim skills1 As String() = New String() {"Class A CDL", "Forklift"}

            Dim updatedMember1 = route4MeV5.AddSkillsToDriver(queryParams1, skills1, resultResponse)

            Dim newMemberParameters2 = New TeamRequest() With {
                .NewPassword = testPassword,
                .MemberFirstName = "John2",
                .MemberLastName = "Doe2",
                .MemberCompany = "Test Member Created",
                .MemberEmail = GetTestEmail().Replace("+", "2+"),
                .OwnerMemberId = CInt(ownerId)
            }

            newMemberParameters2.SetMemberType(MemberTypes.Driver)

            Dim member2 = route4MeV5.CreateTeamMember(newMemberParameters2, resultResponse)
            If member2 IsNot Nothing AndAlso member2.[GetType]() = GetType(TeamResponse) Then membersToRemove.Add(member2)

            Dim queryParams2 = New MemberQueryParameters() With {
                .UserId = member2.MemberId.ToString()
            }

            Dim skills2 As String() = New String() {"Forklift", "Skid Steer Loader"}

            Dim updatedMember2 = route4MeV5.AddSkillsToDriver(queryParams2, skills2, resultResponse)

            Dim addresses = New Address() {New Address() With {
                .AddressString = "3634 W Market St, Fairlawn, OH 44333",
                .IsDepot = True,
                .Latitude = 41.135762259364,
                .Longitude = -81.629313826561,
                .Time = 300,
                .TimeWindowStart = 28800,
                .TimeWindowEnd = 29465
            }, New Address() With {
                .AddressString = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                .Latitude = 41.135762259364,
                .Longitude = -81.629313826561,
                .Time = 300,
                .TimeWindowStart = 29465,
                .TimeWindowEnd = 30529
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 30529,
                .TimeWindowEnd = 33779,
                .Tags = New String() {"Class A CDL", "Forklift"}
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .IsDepot = True,
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 100,
                .TimeWindowStart = 33779,
                .TimeWindowEnd = 33944
            }, New Address() With {
                .AddressString = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                .Latitude = 41.162971496582,
                .Longitude = -81.479049682617,
                .Time = 300,
                .TimeWindowStart = 33944,
                .TimeWindowEnd = 34801,
                .Tags = New String() {"Forklift", "Skid Steer Loader"}
            }, New Address() With {
                .AddressString = "1659 Hibbard Dr, Stow, OH 44224",
                .Latitude = 41.194505989552,
                .Longitude = -81.443351581693,
                .Time = 300,
                .TimeWindowStart = 34801,
                .TimeWindowEnd = 36366
            }, New Address() With {
                .AddressString = "2705 N River Rd, Stow, OH 44224",
                .Latitude = 41.145240783691,
                .Longitude = -81.410247802734,
                .Time = 300,
                .TimeWindowStart = 36366,
                .TimeWindowEnd = 39173
            }, New Address() With {
                .AddressString = "10159 Bissell Dr, Twinsburg, OH 44087",
                .Latitude = 41.340042114258,
                .Longitude = -81.421226501465,
                .Time = 300,
                .TimeWindowStart = 39173,
                .TimeWindowEnd = 41617
            }, New Address() With {
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                .Latitude = 41.148578643799,
                .Longitude = -81.429229736328,
                .Time = 300,
                .TimeWindowStart = 41617,
                .TimeWindowEnd = 43660
            }, New Address() With {
                .AddressString = "367 Cathy Dr, Munroe Falls, OH 44262",
                .Latitude = 41.148578643799,
                .Longitude = -81.429229736328,
                .Time = 300,
                .TimeWindowStart = 43660,
                .TimeWindowEnd = 46392
            }, New Address() With {
                .AddressString = "512 Florida Pl, Barberton, OH 44203",
                .Latitude = 41.003671512008,
                .Longitude = -81.598461046815,
                .Time = 300,
                .TimeWindowStart = 46392,
                .TimeWindowEnd = 48389
            }, New Address() With {
                .AddressString = "559 W Aurora Rd, Northfield, OH 44067",
                .Latitude = 41.315116882324,
                .Longitude = -81.558746337891,
                .Time = 50,
                .TimeWindowStart = 48389,
                .TimeWindowEnd = 48449
            }, New Address() With {
                .AddressString = "3933 Klein Ave, Stow, OH 44224",
                .Latitude = 41.169467926025,
                .Longitude = -81.429420471191,
                .Time = 300,
                .TimeWindowStart = 48449,
                .TimeWindowEnd = 50152
            }, New Address() With {
                .AddressString = "2148 8th St, Cuyahoga Falls, OH 44221",
                .Latitude = 41.136692047119,
                .Longitude = -81.493492126465,
                .Time = 300,
                .TimeWindowStart = 50152,
                .TimeWindowEnd = 51982
            }, New Address() With {
                .AddressString = "3731 Osage St, Stow, OH 44224",
                .Latitude = 41.161357879639,
                .Longitude = -81.42293548584,
                .Time = 100,
                .TimeWindowStart = 51982,
                .TimeWindowEnd = 52180
            }, New Address() With {
                .AddressString = "3731 Osage St, Stow, OH 44224",
                .Latitude = 41.161357879639,
                .Longitude = -81.42293548584,
                .Time = 300,
                .TimeWindowStart = 52180,
                .TimeWindowEnd = 54379
            }}

            Dim parameters = New RouteParameters() With {
                .AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                .RouteName = "Multiple Depot, Multiple Driver",
                .RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                .RouteTime = 60 * 60 * 7,
                .RouteMaxDuration = 86400,
                .VehicleCapacity = 7,
                .VehicleMaxDistanceMI = 10000,
                .Optimize = Optimize.Distance.GetEnumDescription(),
                .DistanceUnit = DistanceUnit.MI.GetEnumDescription(),
                .DeviceType = DeviceType.Web.GetEnumDescription(),
                .TravelMode = TravelMode.Driving.GetEnumDescription(),
                .Metric = Metric.Geodesic,
                .AdvancedConstraints = New RouteAdvancedConstraints() {New RouteAdvancedConstraints() With {
                    .AvailableTimeWindows = New List(Of Integer())() From {
                        New Integer() {25200, 39600},
                        New Integer() {57600, 61200}
                    },
                    .MaximumCapacity = 30,
                    .MaximumCargoVolume = 15,
                    .MembersCount = 10,
                    .Tags = New String() {"Forklift", "Skid Steer Loader"},
                    .Route4meMembersId = New Integer() _
                        {
                            CInt(updatedMember1.MemberId),
                            CInt(updatedMember2.MemberId)
                        }
                }}
            }

            Dim optimizationParameters = New OptimizationParameters() With {
                .addresses = addresses,
                .parameters = parameters
            }
            Dim resultResponse1 As ResultResponse = Nothing
            Dim dataObject As DataObject = route4Me.RunOptimization(
                optimizationParameters,
                resultResponse1)

            OptimizationsToRemove = New List(Of String)() From {
                If(dataObject?.OptimizationProblemId, Nothing)
            }

            PrintExampleOptimizationResult(
                dataObject,
                If(resultResponse1.Messages.Count > 0,
                            String.Join(Environment.NewLine, resultResponse1.Messages),
                            ""
                  )
            )

            RemoveTestOptimizations()
            RemoveTestTeamMembers()
        End Sub
    End Class
End Namespace
