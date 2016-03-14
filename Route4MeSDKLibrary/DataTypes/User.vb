Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes
    <DataContract> _
    Public NotInheritable Class User
        'the id of the member inside the route4me system
        <DataMember(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId() As System.Nullable(Of Integer)
            Get
                Return m_MemberId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MemberId = Value
            End Set
        End Property
        Private m_MemberId As System.Nullable(Of Integer)

        <DataMember(Name:="account_type_id", EmitDefaultValue:=False)> _
        Public Property AccountTypeId() As System.Nullable(Of Integer)
            Get
                Return m_AccountTypeId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_AccountTypeId = Value
            End Set
        End Property
        Private m_AccountTypeId As System.Nullable(Of Integer)

        <DataMember(Name:="member_type", EmitDefaultValue:=False)> _
        Public Property MemberType() As String
            Get
                Return m_MemberType
            End Get
            Set(value As String)
                m_MemberType = Value
            End Set
        End Property
        Private m_MemberType As String

        <DataMember(Name:="member_first_name")> _
        Public Property MemberFirstName() As String
            Get
                Return m_MemberFirstName
            End Get
            Set(value As String)
                m_MemberFirstName = Value
            End Set
        End Property
        Private m_MemberFirstName As String

        <DataMember(Name:="member_last_name")> _
        Public Property MemberLasttName() As String
            Get
                Return m_MemberLasttName
            End Get
            Set(value As String)
                m_MemberLasttName = Value
            End Set
        End Property
        Private m_MemberLasttName As String

        <DataMember(Name:="member_email")> _
        Public Property MemberEmail() As String
            Get
                Return m_MemberEmail
            End Get
            Set(value As String)
                m_MemberEmail = Value
            End Set
        End Property
        Private m_MemberEmail As String

        <DataMember(Name:="phone_number")> _
        Public Property PhoneNumber() As String
            Get
                Return m_PhoneNumber
            End Get
            Set(value As String)
                m_PhoneNumber = Value
            End Set
        End Property
        Private m_PhoneNumber As String

        <DataMember(Name:="readonly_user", EmitDefaultValue:=False)> _
        Public Property ReadonlyUser() As System.Nullable(Of Boolean)
            Get
                Return m_ReadonlyUser
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_ReadonlyUser = Value
            End Set
        End Property
        Private m_ReadonlyUser As System.Nullable(Of Boolean)

        <DataMember(Name:="show_superuser_addresses", EmitDefaultValue:=False)> _
        Public Property ShowSuperuserAddresses() As System.Nullable(Of Boolean)
            Get
                Return m_ShowSuperuserAddresses
            End Get
            Set(value As System.Nullable(Of Boolean))
                m_ShowSuperuserAddresses = Value
            End Set
        End Property
        Private m_ShowSuperuserAddresses As System.Nullable(Of Boolean)
    End Class
End Namespace