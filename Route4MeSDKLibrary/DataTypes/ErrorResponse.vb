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
        <DataMember(Name:="errors")>
        Public Property Errors As List(Of [String])

    End Class
End Namespace
