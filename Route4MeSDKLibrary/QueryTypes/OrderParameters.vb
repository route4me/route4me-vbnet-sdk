﻿
Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class OrderParameters
        Inherits GenericParameters

        ''' <summary>
        ''' Limit per page, if you use 0 you will get all records
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="limit", EmitDefaultValue:=False)> _
        Public Property Limit() As System.Nullable(Of UInteger)
            Get
                Return m_Limit
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Limit = value
            End Set
        End Property
        Private m_Limit As System.Nullable(Of UInteger)

        ''' <summary>
        ''' Offset
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="offset", EmitDefaultValue:=False)> _
        Public Property Offset() As System.Nullable(Of UInteger)
            Get
                Return m_Offset
            End Get
            Set(value As System.Nullable(Of UInteger))
                m_Offset = value
            End Set
        End Property
        Private m_Offset As System.Nullable(Of UInteger)

        ''' <summary>
        ''' if query is array search engine will search by fields, if query is string will search by all text fields. Array / string.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="query", EmitDefaultValue:=False)> _
        Public Property Query() As String
            Get
                Return m_Query
            End Get
            Set(value As String)
                m_Query = value
            End Set
        End Property
        Private m_Query As String

        ''' <summary>
        ''' Use it for get specific fields. String / coma separated
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="fields", EmitDefaultValue:=False)> _
        Public Property Fields() As String
            Get
                Return m_Fields
            End Get
            Set(value As String)
                m_Fields = value
            End Set
        End Property
        Private m_Fields As String

        ''' <summary>
        ''' filter routed/unrouted. enum(all,routed,unrouted)
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="display", EmitDefaultValue:=False)> _
        Public Property Display() As String
            Get
                Return m_Display
            End Get
            Set(value As String)
                m_Display = value
            End Set
        End Property
        Private m_Display As String

        ''' <summary>
        ''' Order ID.
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="order_id", EmitDefaultValue:=False)> _
        Public Property OrderId() As System.Nullable(Of Integer)
            Get
                Return m_OrderId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_OrderId = value
            End Set
        End Property
        Private m_OrderId As System.Nullable(Of Integer)

        ''' <summary>
        ''' Date an order was inserted
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="day_added_YYMMDD", EmitDefaultValue:=False)> _
        Public Property DayAddedYYMMDD() As String
            Get
                Return m_DayAddedYYMMDD
            End Get
            Set(value As String)
                m_DayAddedYYMMDD = value
            End Set
        End Property
        Private m_DayAddedYYMMDD As String

        ''' <summary>
        ''' Date an order was scheduled for
        ''' </summary>
        <HttpQueryMemberAttribute(Name:="scheduled_for_YYMMDD", EmitDefaultValue:=False)> _
        Public Property ScheduledForYYMMDD() As String
            Get
                Return m_ScheduledForYYMMDD
            End Get
            Set(value As String)
                m_ScheduledForYYMMDD = value
            End Set
        End Property
        Private m_ScheduledForYYMMDD As String

    End Class
End Namespace
