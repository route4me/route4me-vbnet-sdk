Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Avoidance zone parameters
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class AvoidanceZoneParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Device Id
        ''' </summary>
        <IgnoreDataMember>
        <HttpQueryMemberAttribute(Name:="device_id", EmitDefaultValue:=False)>
        Public Property DeviceID As String

        ''' <summary>
        ''' Territory Id
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="territory_id", EmitDefaultValue:=False)>
        Public Property TerritoryId As String

        ''' <summary>
        ''' Territory name
        ''' </summary>
        <DataMember(Name:="territory_name")>
        Public Property TerritoryName As String

        ''' <summary>
        ''' Territory color
        ''' </summary>
        <DataMember(Name:="territory_color")>
        Public Property TerritoryColor As String

        ''' <summary>
        ''' Member Id
        ''' </summary>
        <DataMember(Name:="member_id")>
        Public Property MemberId As String

        ''' <summary>
        ''' Territory parameters
        ''' </summary>
        <DataMember(Name:="territory")>
        Public Property Territory As Territory

    End Class
End Namespace
