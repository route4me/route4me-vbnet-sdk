Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Avoidance zone query
    ''' </summary>
    Public NotInheritable Class AvoidanceZoneQuery
        Inherits GenericParameters
        ''' <summary>
        ''' Device Id
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="device_id", EmitDefaultValue:=False)>
        Public Property DeviceID As String

        ''' <summary>
        ''' Territory Id
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="territory_id", EmitDefaultValue:=False)>
        Public Property TerritoryId As String

    End Class
End Namespace
