Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes

    ''' <summary>
    ''' Order Territories for an optimization payload. 
    ''' </summary>
    <DataContract>
    Public NotInheritable Class OrderTerritories
        Inherits GenericParameters

        ''' <summary>
        ''' If true, split each territory to own optimization
        ''' </summary>
        <DataMember(Name:="split_territories", EmitDefaultValue:=False)>
        Public Property SplitTerritories As Boolean?

        ''' <summary>
        ''' An array of the territory IDs
        ''' </summary>
        <DataMember(Name:="territories_id", EmitDefaultValue:=False)>
        Public Property TerritoriesId As String()

        ''' <summary>
        ''' Order filters.
        ''' </summary>
        <DataMember(Name:="filters", EmitDefaultValue:=False)>
        Public Property filters As FilterDetails

    End Class

End Namespace