
Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class OrderParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Limit per page, if you use 0 you will get all records
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)>
        Public Property limit As UInteger?

        ''' <summary>
        ''' Offset
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)>
        Public Property offset As UInteger?

        ''' <summary>
        ''' if query is array search engine will search by fields, if query is string will search by all text fields. Array / string.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)> _
        Public Property query As String

        ''' <summary>
        ''' Use it for get specific fields. String / coma separated
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="fields", EmitDefaultValue:=False)> _
        Public Property fields As String

        ''' <summary>
        ''' filter routed/unrouted. enum(all,routed,unrouted)
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="display", EmitDefaultValue:=False)> _
        Public Property display As String

        ''' <summary>
        ''' Order ID.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="order_id", EmitDefaultValue:=False)> _
        Public Property order_id As String

        ''' <summary>
        ''' Date an order was inserted
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="day_added_YYMMDD", EmitDefaultValue:=False)> _
        Public Property day_added_YYMMDD As String

        ''' <summary>
        ''' Date an order was scheduled for
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="scheduled_for_YYMMDD", EmitDefaultValue:=False)> _
        Public Property scheduled_for_YYMMDD As String

    End Class
End Namespace
