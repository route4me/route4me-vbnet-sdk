Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Avoidance Zone
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class AvoidanceZone
        '''<summary>
        ''' Avoidance zone id
        '''</summary>
        <DataMember(Name:="territory_id")> _
        Public Property TerritoryId As String

        '''<summary>
        ''' Territory name
        '''</summary>
        <DataMember(Name:="territory_name")> _
        Public Property TerritoryName As String

        '''<summary>
        ''' Territory color 
        '''</summary>
        <DataMember(Name:="territory_color")> _
        Public Property TerritoryColor As String

        '''<summary>
        ''' Member Id
        '''</summary>
        <DataMember(Name:="member_id")> _
        Public Property MemberId As String

        '''<summary>
        ''' Territory parameters
        '''</summary>
        <DataMember(Name:="territory")> _
        Public Property Territory As Territory
    End Class
End Namespace
