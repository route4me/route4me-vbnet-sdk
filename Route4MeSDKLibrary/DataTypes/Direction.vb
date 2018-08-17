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
        Public Property Location As Location

        ''' <summary>
        ''' Maneuver steps
        ''' </summary>
        <DataMember(Name:="steps")> _
        Public Property Step1 As Step1()
    End Class
End Namespace