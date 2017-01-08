Imports Route4MeSDKLibrary.Route4MeSDK.DataTypes
Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Runtime.Serialization
Namespace Route4MeSDK.QueryTypes
    Public NotInheritable Class MemberParameters
        Inherits GenericParameters

        <HttpQueryMemberAttribute(Name:="session_guid", EmitDefaultValue:=False)> _
        Public Property SessionGuid() As String
            Get
                Return m_SessionGuid
            End Get
            Set(value As String)
                m_SessionGuid = value
            End Set
        End Property
        Private m_SessionGuid As String

        <HttpQueryMemberAttribute(Name:="format", EmitDefaultValue:=False)> _
        Public Property hFormat() As String
            Get
                Return m_hFormat
            End Get
            Set(value As String)
                m_hFormat = value
            End Set
        End Property
        Private m_hFormat As String

        <HttpQueryMemberAttribute(Name:="member_id", EmitDefaultValue:=False)> _
        Public Property MemberId() As System.Nullable(Of Integer)
            Get
                Return m_MemberId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As System.Nullable(Of Integer)

        <HttpQueryMemberAttribute(Name:="plan", EmitDefaultValue:=False)> _
        Public Property Plan() As String
            Get
                Return m_Plan
            End Get
            Set(value As String)
                m_Plan = value
            End Set
        End Property
        Private m_Plan As String

        <HttpQueryMemberAttribute(Name:="member_type", EmitDefaultValue:=False)> _
        Public Property MemberType() As System.Nullable(Of Integer)
            Get
                Return m_MemberType
            End Get
            Set(value As System.Nullable(Of Integer))
                m_MemberType = value
            End Set
        End Property
        Private m_MemberType As System.Nullable(Of Integer)

        <DataMember(Name:="strEmail", EmitDefaultValue:=False)> _
        Public Property StrEmail() As String
            Get
                Return m_StrEmail
            End Get
            Set(value As String)
                m_StrEmail = value
            End Set
        End Property
        Private m_StrEmail As String

        <DataMember(Name:="strPassword", EmitDefaultValue:=False)> _
        Public Property StrPassword() As String
            Get
                Return m_StrPassword
            End Get
            Set(value As String)
                m_StrPassword = value
            End Set
        End Property
        Private m_StrPassword As String

        <DataMember(Name:="format", EmitDefaultValue:=False)> _
        Public Property Format() As String
            Get
                Return m_Format
            End Get
            Set(value As String)
                m_Format = value
            End Set
        End Property
        Private m_Format As String

        <DataMember(Name:="strIndustry", EmitDefaultValue:=False)> _
        Public Property StrIndustry() As String
            Get
                Return m_StrIndustry
            End Get
            Set(value As String)
                m_StrIndustry = value
            End Set
        End Property
        Private m_StrIndustry As String

        <DataMember(Name:="strFirstName", EmitDefaultValue:=False)> _
        Public Property StrFirstName() As String
            Get
                Return m_StrFirstName
            End Get
            Set(value As String)
                m_StrFirstName = value
            End Set
        End Property
        Private m_StrFirstName As String

        <DataMember(Name:="strLastName", EmitDefaultValue:=False)> _
        Public Property StrLastName() As String
            Get
                Return m_StrLastName
            End Get
            Set(value As String)
                m_StrLastName = value
            End Set
        End Property
        Private m_StrLastName As String

        <DataMember(Name:="chkTerms", EmitDefaultValue:=False)> _
        Public Property ChkTerms() As System.Nullable(Of Integer)
            Get
                Return m_ChkTerms
            End Get
            Set(value As System.Nullable(Of Integer))
                m_ChkTerms = value
            End Set
        End Property
        Private m_ChkTerms As System.Nullable(Of Integer)

        <DataMember(Name:="device_type", EmitDefaultValue:=False)> _
        Public Property DeviceType() As String
            Get
                Return m_DeviceType
            End Get
            Set(value As String)
                m_DeviceType = value
            End Set
        End Property
        Private m_DeviceType As String

        <DataMember(Name:="strPassword_1", EmitDefaultValue:=False)> _
        Public Property StrPassword_1() As String
            Get
                Return m_StrPassword1
            End Get
            Set(value As String)
                m_StrPassword1 = value
            End Set
        End Property
        Private m_StrPassword1 As String

        <DataMember(Name:="strPassword_2", EmitDefaultValue:=False)> _
        Public Property StrPassword_2() As String
            Get
                Return m_StrPassword2
            End Get
            Set(value As String)
                m_StrPassword2 = value
            End Set
        End Property
        Private m_StrPassword2 As String

    End Class
End Namespace
