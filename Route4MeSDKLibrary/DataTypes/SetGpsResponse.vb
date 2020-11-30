Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Response for the GPS setting request.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class SetGpsResponse
        Inherits GenericParameters

        ''' <summary>
        ''' Status of the GPS setting request process.
        ''' </summary>
        ''' <value>
        '''   <c>true</c> if the GPS setting request finished successfully; otherwise, <c>false</c>.
        ''' </value>
        <DataMember(Name:="status")>
        Public Property Status As Boolean

        ''' <summary>
        ''' Unique ID of the GPS points group.
        ''' <remarks><para>
        ''' On the first request you do Not need to provide a tx_id. 
        ''' On the second request you provide the tx_id which you got back 
        ''' from the first request. The tx_id Is the unique group of points 
        ''' related to this specific route transaction.
        ''' </para></remarks>
        ''' </summary>
        <DataMember(Name:="tx_id", EmitDefaultValue:=False)>
        Public Property TX_ID As String

    End Class
End Namespace

