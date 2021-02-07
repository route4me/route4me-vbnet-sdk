Imports System.Runtime.Serialization
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Route datatable configuration
    ''' </summary>
    <DataContract>
    Public NotInheritable Class RouteDataTableConfigResponse
        Inherits GenericParameters

        ''' <summary>
        ''' API Capabilities
        ''' </summary>
        <DataMember(Name:="api_capabilities", EmitDefaultValue:=False)>
        Public Property ApiCapabilities As ApiCapabilitiesCls

        ''' <summary>
        ''' API Preferences
        ''' </summary>
        <DataMember(Name:="api_preferences", EmitDefaultValue:=False)>
        Public Property ApiPreferences As ApiPreferencesCls

    End Class

    Public Class ApiCapabilitiesCls

        ''' <summary>
        ''' Sortable Fields
        ''' </summary>
        <DataMember(Name:="sortable_fields", EmitDefaultValue:=False)>
        Public Property SortableFields As String()

        ''' <summary>
        ''' Combinations of the sortable fields
        ''' </summary>
        <DataMember(Name:="sortable_fields_combinations", EmitDefaultValue:=False)>
        Public Property SortableFieldsCombinations As List(Of String())

        ''' <summary>
        ''' If true, multi-sorting enabled.
        ''' </summary>
        <DataMember(Name:="multi_sorting_enabled", EmitDefaultValue:=False)>
        Public Property MultiSortingEnabled As Boolean?

        ''' <summary>
        ''' An array of the filterable fields.
        ''' </summary>
        <DataMember(Name:="filterable_fields", EmitDefaultValue:=False)>
        Public Property FilterableFields As String()

        ''' <summary>
        ''' If true, search enabled.
        ''' </summary>
        <DataMember(Name:="search", EmitDefaultValue:=False)>
        Public Property Search As Boolean?
    End Class

    Public Class ApiPreferencesCls

        ''' <summary>
        ''' Force the server side search.
        ''' </summary>
        <DataMember(Name:="force_server_side_search", EmitDefaultValue:=False)>
        Public Property ForceServerSideSearch As Boolean?

        ''' <summary>
        ''' If true, the search result loaded partially.
        ''' </summary>
        <DataMember(Name:="partial_load", EmitDefaultValue:=False)>
        Public Property PartialLoad As Boolean?

        ''' <summary>
        ''' If true, simple pagination enabled.
        ''' </summary>
        <DataMember(Name:="simple_pagination", EmitDefaultValue:=False)>
        Public Property SimplePagination As Boolean?
    End Class

End Namespace

