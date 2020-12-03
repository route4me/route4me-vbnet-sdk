Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes

Namespace Route4MeSDK.DataTypes.V5
    ''' <summary>
    ''' The data structure of a retrieved driver review.
    ''' </summary>
    <DataContract>
    Public NotInheritable Class DriverReview
        Inherits GenericParameters

        ''' <summary>
        ''' Driver Rating ID
        ''' </summary>
        <DataMember(Name:="rating_id")>
        Public Property RatingId As String

        ''' <summary>
        ''' The tracking number of the route destination
        ''' </summary>
        <DataMember(Name:="tracking_number")>
        Public Property TrackingNumber As String

        ''' <summary>
        ''' A review the driver got
        ''' </summary>
        <DataMember(Name:="review")>
        Public Property Review As String

        ''' <summary>
        ''' The rating assigned to the driver.
        ''' Available values: 1,2,3,4
        ''' </summary>
        <DataMember(Name:="rating")>
        <Range(1, 4)>
        Public Property Rating As Double?

        ''' <summary>
        ''' When the review was created.
        ''' </summary>
        <DataMember(Name:="added_at")>
        Public Property AddedAt As String
    End Class

    ''' <summary>
    ''' The data structure of a retrieved driver reviews list.
    ''' </summary>
    Public NotInheritable Class DriverReviewsResponse
        ''' <summary>
        ''' An array of the driver reviews.
        ''' </summary>
        <DataMember(Name:="data")>
        Public Property Data As DriverReview()

        ''' <summary>
        ''' The response pagination info.
        ''' </summary>
        <DataMember(Name:="simple_pagination")>
        Public Property SimplePagination As SimplePaginationData

        ''' <summary>
        ''' Statistics by driver rating.
        ''' </summary>
        <DataMember(Name:="total")>
        Public Property Total As TypeQuantity()
    End Class

    ''' <summary>
    ''' Data structure of the response pagination info.
    ''' </summary>
    Public NotInheritable Class SimplePaginationData
        ''' <summary>
        ''' Driver reviews number per page.
        ''' </summary>
        <DataMember(Name:="per_page")>
        Public Property PerPage As Integer?

        ''' <summary>
        ''' Current page number in the driver reviews collection.
        ''' </summary>
        <DataMember(Name:="current_page")>
        Public Property CurrentPage As Integer?

        ''' <summary>
        ''' Path to the driver review addon.
        ''' </summary>
        <DataMember(Name:="path")>
        Public Property Path As String

        <DataMember(Name:="first")>
        Public Property First As String

        <DataMember(Name:="prev")>
        Public Property Previous As Integer?

        <DataMember(Name:="next")>
        Public Property [Next] As Integer?
    End Class

    ''' <summary>
    ''' Driver rating quantity by types.
    ''' </summary>
    Public NotInheritable Class TypeQuantity
        <DataMember(Name:="type")>
        Public Property Type As Integer

        <DataMember(Name:="quantity")>
        Public Property Quantity As Integer
    End Class
End Namespace