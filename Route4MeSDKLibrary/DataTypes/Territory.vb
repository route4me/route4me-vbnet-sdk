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
        <DataMember(Name:="type")>
        Public Property Type As String

        ''' <summary>
        ''' Territory figure data
        ''' </summary>
        <DataMember(Name:="data")>
        Public Property Data As String()

    End Class
End Namespace
