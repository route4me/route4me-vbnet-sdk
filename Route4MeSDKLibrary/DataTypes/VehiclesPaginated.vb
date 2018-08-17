Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract>
    Public NotInheritable Class VehiclesPaginated
        Inherits GenericParameters

        <DataMember(Name:="current_page", EmitDefaultValue:=False)>
        Public Property CurrentPage As Integer

        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property Data As VehicleV4Response()

        <DataMember(Name:="first_page_url", EmitDefaultValue:=False)>
        Public Property FirstPageUrl As String

        <DataMember(Name:="from", EmitDefaultValue:=False)>
        Public Property From As Integer

        <DataMember(Name:="last_page", EmitDefaultValue:=False)>
        Public Property LastPage As Integer

        <DataMember(Name:="last_page_url", EmitDefaultValue:=False)>
        Public Property LastPageUrl As String

        <DataMember(Name:="next_page_url", EmitDefaultValue:=False)>
        Public Property NextPageUrl As String

        <DataMember(Name:="path", EmitDefaultValue:=False)>
        Public Property Path As String

        <DataMember(Name:="per_page", EmitDefaultValue:=False)>
        Public Property PerPage As Integer

        <DataMember(Name:="prev_page_url", EmitDefaultValue:=False)>
        Public Property PrevPageUrl As String

        <DataMember(Name:="to", EmitDefaultValue:=False)>
        Public Property [To] As Integer

        <DataMember(Name:="total", EmitDefaultValue:=False)>
        Public Property Total As Integer
    End Class
End Namespace