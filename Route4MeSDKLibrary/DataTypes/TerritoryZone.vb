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
        <DataMember(Name:="territory_id")> _
        Public Property TerritoryId() As String
            Get
                Return m_TerritoryId
            End Get
            Set(value As String)
                m_TerritoryId = value
            End Set
        End Property
        Private m_TerritoryId As String

        '''<summary>
        ''' Territory name
        '''</summary>
        <DataMember(Name:="territory_name")> _
        Public Property TerritoryName() As String
            Get
                Return m_TerritoryName
            End Get
            Set(value As String)
                m_TerritoryName = value
            End Set
        End Property
        Private m_TerritoryName As String

        '''<summary>
        ''' Territory color 
        '''</summary>
        <DataMember(Name:="territory_color")> _
        Public Property TerritoryColor() As String
            Get
                Return m_TerritoryColor
            End Get
            Set(value As String)
                m_TerritoryColor = value
            End Set
        End Property
        Private m_TerritoryColor As String

        '''<summary>
        ''' Territory addresses 
        '''</summary>
        <DataMember(Name:="addresses")> _
        Public Property addresses() As Integer()
            Get
                Return m_addresses
            End Get
            Set(value As Integer())
                m_addresses = value
            End Set
        End Property
        Private m_addresses As Integer()

        '''<summary>
        ''' Member Id
        '''</summary>
        <DataMember(Name:="member_id")> _
        Public Property MemberId() As String
            Get
                Return m_MemberId
            End Get
            Set(value As String)
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As String

        '''<summary>
        ''' Territory parameters
        '''</summary>
        <DataMember(Name:="territory")> _
        Public Property Territory() As Territory
            Get
                Return m_Territory
            End Get
            Set(value As Territory)
                m_Territory = value
            End Set
        End Property
        Private m_Territory As Territory
    End Class
End Namespace
