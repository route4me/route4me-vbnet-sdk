Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response from the process of retrieving the vehicle profiles.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleProfilesResponse
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' Current page of the paginated vehicle profiles list.
        ''' </summary>
        <DataMember(Name:="current_page", EmitDefaultValue:=False)>
        Public Property CurrentPage As Integer?

        ''' <summary>
        ''' An array of the vehicle profiles
        ''' </summary>
        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property Data As VehicleProfile()

        ''' <summary>
        ''' URL to the first page of the paginated vehicle profiles list.
        ''' </summary>
        <DataMember(Name:="first_page_url", EmitDefaultValue:=False)>
        Public Property FirstPageUrl As String

        ''' <summary>
        ''' From which vehicle profile is starting the page
        ''' </summary>
        <DataMember(Name:="from", EmitDefaultValue:=False)>
        Public Property From As Integer?

        ''' <summary>
        ''' Last page
        ''' </summary>
        <DataMember(Name:="last_page", EmitDefaultValue:=False)>
        Public Property LastPage As Integer?

        ''' <summary>
        ''' URL to the last page of the paginated vehicle profiles list.
        ''' </summary>
        <DataMember(Name:="last_page_url", EmitDefaultValue:=False)>
        Public Property LastPageUrl As String

        ''' <summary>
        ''' URL to the next page of the paginated vehicle profiles list.
        ''' </summary>
        <DataMember(Name:="next_page_url", EmitDefaultValue:=False)>
        Public Property NextPageUrl As String

        ''' <summary>
        ''' Path to the API endpoint
        ''' </summary>
        <DataMember(Name:="path", EmitDefaultValue:=False)>
        Public Property Path As String

        ''' <summary>
        ''' Vehicles number per page
        ''' </summary>
        <DataMember(Name:="per_page", EmitDefaultValue:=False)>
        Public Property PerPage As Integer?

        ''' <summary>
        ''' URL to the previous page
        ''' </summary>
        <DataMember(Name:="prev_page_url", EmitDefaultValue:=False)>
        Public Property PreviousPageUrl As String

        ''' <summary>
        ''' To which vehicle profile is ending the page
        ''' </summary>
        <DataMember(Name:="to", EmitDefaultValue:=False)>
        Public Property [To] As Integer?

        ''' <summary>
        ''' Total number of the vehicles.
        ''' </summary>
        <DataMember(Name:="total", EmitDefaultValue:=False)>
        Public Property Total As Integer?

    End Class
End Namespace
