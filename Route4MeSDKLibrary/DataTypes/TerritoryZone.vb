Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Avoidance Zone
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class TerritoryZone
        '''<summary>
        ''' Avoidance zone id
        '''</summary>
        <DataMember(Name:="territory_id")>
        Public Property TerritoryId As String

        '''<summary>
        ''' Territory name
        '''</summary>
        <DataMember(Name:="territory_name")>
        Public Property TerritoryName As String

        '''<summary>
        ''' Territory color 
        '''</summary>
        <DataMember(Name:="territory_color")>
        Public Property TerritoryColor As String

        '''<summary>
        ''' Enclosed territory addresses 
        '''</summary>
        <DataMember(Name:="addresses")>
        Public Property Addresses As Integer()

        '''<summary>
        ''' Enclosed territory orders 
        '''</summary>
        <DataMember(Name:="orders")>
        Public Property Orders As Integer()

        '''<summary>
        ''' Member Id
        '''</summary>
        <DataMember(Name:="member_id")>
        Public Property MemberId As String

        '''<summary>
        ''' Territory parameters
        '''</summary>
        <DataMember(Name:="territory")>
        Public Property Territory As Territory

    End Class
End Namespace
