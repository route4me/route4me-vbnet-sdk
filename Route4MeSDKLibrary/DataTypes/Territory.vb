Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Territory parameters
    ''' </summary>
    <DataContract> _
    Public NotInheritable Class Territory
        ''' <summary>
        ''' Territory type (circle, rectangle, polygon)
        ''' </summary>
        <DataMember(Name:="type")> _
        Public Property Type() As String
            Get
                Return m_Type
            End Get
            Set(value As String)
                m_Type = Value
            End Set
        End Property
        Private m_Type As String

        ''' <summary>
        ''' Territory figure data
        ''' </summary>
        <DataMember(Name:="data")> _
        Public Property Data() As String()
            Get
                Return m_Data
            End Get
            Set(value As String())
                m_Data = Value
            End Set
        End Property
        Private m_Data As String()
    End Class
End Namespace
