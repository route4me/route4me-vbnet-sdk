Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes
    ''' <summary>
    ''' Avoidance zone query
    ''' </summary>
    Public NotInheritable Class TerritoryQuery
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

        ''' <summary>
        ''' Id equal to 1, the enclosed addresses will be inserted in the response.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="addresses", EmitDefaultValue:=False)>
        Public Property Addresses As Integer?

        ''' <summary>
        ''' Id equal to 1, the enclosed orders will be inserted in the response.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="orders", EmitDefaultValue:=False)>
        Public Property Orders As Integer?

    End Class
End Namespace