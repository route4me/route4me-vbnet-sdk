Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    ''' <summary>
    ''' Response from the create vehicle request. See also <seealso cref="VehicleV4Response"/>.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleV4CreateResponse
        Inherits GenericParameters
        ''' <summary>
        ''' Status of the request process.
        ''' </summary>
        ''' <value>
        '''   <c>true</c> if request finished successfully; otherwise, <c>false</c>.
        ''' </value>
        <DataMember(Name:="status", EmitDefaultValue:=False)>
        Public Property status As Boolean

        ''' <summary>
        ''' The vehicle Guid.
        ''' </summary>
        <DataMember(Name:="vehicle_guid", EmitDefaultValue:=False)>
        Public Property VehicleGuid As String

        ''' <summary>
        ''' <c>true</c> True, if the vehicle Is New.
        ''' </summary>
        <DataMember(Name:="new", EmitDefaultValue:=False)>
        Public Property [New] As Boolean
    End Class

End Namespace

