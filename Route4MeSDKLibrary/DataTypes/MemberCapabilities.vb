Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Member capabilities data structure
    ''' </summary>
    <DataContract>
    Public Class MemberCapabilities
        Inherits GenericParameters
        ''' <summary>
        ''' Array of the avoidance zone IDs
        ''' </summary>
        <DataMember(Name:="avoid", EmitDefaultValue:=False)>
        Public Property Avoid As String()

        ''' <summary>
        ''' Road avoid options: "Highways", "Tolls", "highways,tolls".
        ''' </summary>
        <DataMember(Name:="avoid_roads", EmitDefaultValue:=False)>
        Public Property AvoidRoads As String()

        ''' <summary>
        ''' Restriction options
        ''' </summary>
        <DataMember(Name:="features", EmitDefaultValue:=False)>
        Public Property Features As Dictionary(Of String, Boolean)

        ''' <summary>
        ''' Travel modes: "Highways", "Tolls", "highways,tolls"
        ''' </summary>
        <DataMember(Name:="travelModes", EmitDefaultValue:=False)>
        Public Property TravelModes As Dictionary(Of String, String)

        ''' <summary>
        ''' Navigate options
        ''' </summary>
        <DataMember(Name:="navigateBy", EmitDefaultValue:=False)>
        Public Property NavigateBy As String()

        ''' <summary>
        ''' Array of the license modules
        ''' </summary>
        <DataMember(Name:="LicensedModules", EmitDefaultValue:=False)>
        Public Property LicensedModules As String()

        ''' <summary>
        ''' If true, the member subscription Is commercial.
        ''' </summary>
        <DataMember(Name:="commercial", EmitDefaultValue:=False)>
        Public Property Commercial As Boolean
    End Class
End Namespace
