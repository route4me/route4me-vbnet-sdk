Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5
    ''' <summary>
    ''' Response data structure. As default in the failure case, 
    ''' sometimes - in the success case too.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class ResultResponse
        ''' <summary>
        ''' Status  (true/false)
        ''' </summary>
        <DataMember(Name:="status")>
        Public Property Status As Boolean

        ''' <summary>
        ''' Status code
        ''' </summary>
        <DataMember(Name:="code")>
        Public Property Code As Integer

        ''' <summary>
        ''' Exit code
        ''' </summary>
        <DataMember(Name:="exit_code")>
        Public Property ExitCode As Integer

        ''' <summary>
        ''' An array of the error messages.
        ''' </summary>
        <DataMember(Name:="messages")>
        Public Property Messages As Dictionary(Of String, String())

    End Class
End Namespace