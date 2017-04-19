Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes

    <DataContract> _
    Public NotInheritable Class HybridOptimizationParameters
        Inherits GenericParameters
        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="target_date_string", EmitDefaultValue:=False)> _
        Public Property target_date_string As String

        ' Don't serialize as JSON
        <IgnoreDataMember> _
        <HttpQueryMemberAttribute(Name:="timezone_offset_minutes", EmitDefaultValue:=False)> _
        Public Property timezone_offset_minutes As Integer

    End Class
End Namespace