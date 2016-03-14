Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.Text
Imports System.Threading.Tasks

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Errors data-structure
    ''' </summary>
    <DataContract> _
    Public Class ErrorResponse
        <DataMember(Name:="errors")> _
        Public Property Errors() As List(Of [String])
            Get
                Return m_Errors
            End Get
            Set(value As List(Of [String]))
                m_Errors = value
            End Set
        End Property
        Private m_Errors As List(Of [String])
    End Class
End Namespace
