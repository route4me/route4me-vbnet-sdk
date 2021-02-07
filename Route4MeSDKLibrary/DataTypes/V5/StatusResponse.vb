Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response from the requests returning only boolean parameter 'status'
    ''' </summary>
    ''' <seealso cref="Route4MeSDK.QueryTypes.GenericParameters" />
    <DataContract>
    Public NotInheritable Class StatusResponse
        Inherits GenericParameters

        ''' <summary>
        ''' Status of the request process.
        ''' </summary>
        ''' <value>
        '''   <c>true</c> if request finished successfully; otherwise, <c>false</c>.
        ''' </value>
        <DataMember(Name:="status", EmitDefaultValue:=False)>
        Public Property status As Boolean

    End Class

End Namespace

