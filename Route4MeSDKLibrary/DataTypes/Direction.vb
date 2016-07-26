Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Direction
    ''' </summary>
    Public NotInheritable Class Direction
        ''' <summary>
        ''' Direction location
        ''' </summary>
        <DataMember(Name:="location")> _
        Public Property Location() As Location
            Get
                Return m_Location
            End Get
            Set(value As Location)
                m_Location = value
            End Set
        End Property
        Private m_Location As Location

        ''' <summary>
        ''' Maneuver steps
        ''' </summary>
        <DataMember(Name:="steps")> _
        Public Property Step1() As Step1()
            Get
                Return m_Step1
            End Get
            Set(value As Step1())
                m_Step1 = value
            End Set
        End Property
        Private m_Step1 As Step1()
    End Class
End Namespace