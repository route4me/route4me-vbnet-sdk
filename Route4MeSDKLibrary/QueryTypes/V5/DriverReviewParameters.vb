Imports System.Runtime.Serialization

Namespace Route4MeSDK.QueryTypes.V5
    Public NotInheritable Class DriverReviewParameters
        Inherits GenericParameters

        ''' <summary>
        ''' The driver rating to search for.
        ''' Available values: 1,2,3,4
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="rating", EmitDefaultValue:=False)>
        Public Property Rating As Integer?

        ''' <summary>
        ''' The driver ID
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)>
        Public Property MemberId As Integer?

        ''' <summary>
        ''' Start of the time filter. (e.g. "2020-11-26")
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="start", EmitDefaultValue:=False)>
        Public Property Start As String

        ''' <summary>
        ''' End of the time filter. (e.g. "2020-11-29")
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="end", EmitDefaultValue:=False)>
        Public Property [End] As String

        ''' <summary>
        ''' A page number of the driver reviews collection to retrieve.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="page", EmitDefaultValue:=False)>
        Public Property Page As Integer?

        ''' <summary>
        ''' Diver reviews per page.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="per_page", EmitDefaultValue:=False)>
        Public Property PerPage As Integer?

        ''' <summary>
        ''' Rating ID
        ''' </summary>
        <DataMember(Name:="rating_id")>
        Public Property RatingId As String

    End Class
End Namespace
