Imports Route4MeSDKLibrary.Route4MeSDK.QueryTypes
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes

    <DataContract> _
    Public NotInheritable Class VehicleResponse
        Inherits GenericParameters

        <DataMember(Name:="vehicle_id")> _
        Public Property VehicleId() As String
            Get
                Return m_VehicleId
            End Get
            Set(value As String)
                m_VehicleId = value
            End Set
        End Property
        Private m_VehicleId As String

        <DataMember(Name:="created_time")> _
        Public Property CreatedTime() As String
            Get
                Return m_CreatedTime
            End Get
            Set(value As String)
                m_CreatedTime = value
            End Set
        End Property
        Private m_CreatedTime As String

        <DataMember(Name:="member_id")> _
        Public Property MemberId() As String
            Get
                Return m_MemberId
            End Get
            Set(value As String)
                m_MemberId = value
            End Set
        End Property
        Private m_MemberId As String

        <DataMember(Name:="vehicle_alias")> _
        Public Property VehicleAlias() As String
            Get
                Return m_VehicleAlias
            End Get
            Set(value As String)
                m_VehicleAlias = value
            End Set
        End Property
        Private m_VehicleAlias As String

        <DataMember(Name:="vehicle_vin")> _
        Public Property VehicleVin() As String
            Get
                Return m_VehicleVin
            End Get
            Set(value As String)
                m_VehicleVin = value
            End Set
        End Property
        Private m_VehicleVin As String

        <DataMember(Name:="vehicle_reg_state")> _
        Public Property VehicleRegState() As String
            Get
                Return m_VehicleRegState
            End Get
            Set(value As String)
                m_VehicleRegState = value
            End Set
        End Property
        Private m_VehicleRegState As String

        <DataMember(Name:="vehicle_reg_state_id")> _
        Public Property VehicleRegStateId() As System.Nullable(Of Integer)
            Get
                Return m_VehicleRegStateId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_VehicleRegStateId = value
            End Set
        End Property
        Private m_VehicleRegStateId As System.Nullable(Of Integer)

        <DataMember(Name:="vehicle_reg_country")> _
        Public Property VehicleRegCountry() As String
            Get
                Return m_VehicleRegCountry
            End Get
            Set(value As String)
                m_VehicleRegCountry = value
            End Set
        End Property
        Private m_VehicleRegCountry As String

        <DataMember(Name:="vehicle_reg_country_id")> _
        Public Property VehicleRegCountryId() As System.Nullable(Of Integer)
            Get
                Return m_VehicleRegCountryId
            End Get
            Set(value As System.Nullable(Of Integer))
                m_VehicleRegCountryId = value
            End Set
        End Property
        Private m_VehicleRegCountryId As System.Nullable(Of Integer)

        <DataMember(Name:="vehicle_license_plate")> _
        Public Property VehicleLicensePlate() As String
            Get
                Return m_VehicleLicensePlate
            End Get
            Set(value As String)
                m_VehicleLicensePlate = value
            End Set
        End Property
        Private m_VehicleLicensePlate As String

        <DataMember(Name:="vehicle_make")> _
        Public Property VehicleMake() As String
            Get
                Return m_VehicleMake
            End Get
            Set(value As String)
                m_VehicleMake = value
            End Set
        End Property
        Private m_VehicleMake As String

        <DataMember(Name:="vehicle_model_year")> _
        Public Property VehicleModelYear() As System.Nullable(Of Integer)
            Get
                Return m_VehicleModelYear
            End Get
            Set(value As System.Nullable(Of Integer))
                m_VehicleModelYear = value
            End Set
        End Property
        Private m_VehicleModelYear As System.Nullable(Of Integer)

        <DataMember(Name:="vehicle_model")> _
        Public Property VehicleModel() As String
            Get
                Return m_VehicleModel
            End Get
            Set(value As String)
                m_VehicleModel = value
            End Set
        End Property
        Private m_VehicleModel As String

        <DataMember(Name:="vehicle_year_acquired")> _
        Public Property VehicleYearAcquired() As System.Nullable(Of Integer)
            Get
                Return m_VehicleYearAcquired
            End Get
            Set(value As System.Nullable(Of Integer))
                m_VehicleYearAcquired = value
            End Set
        End Property
        Private m_VehicleYearAcquired As System.Nullable(Of Integer)

        <DataMember(Name:="vehicle_cost_new")> _
        Public Property VehicleCostNew() As System.Nullable(Of Double)
            Get
                Return m_VehicleCostNew
            End Get
            Set(value As System.Nullable(Of Double))
                m_VehicleCostNew = value
            End Set
        End Property
        Private m_VehicleCostNew As System.Nullable(Of Double)

        <DataMember(Name:="license_start_date")> _
        Public Property LicenseStartDate() As String
            Get
                Return m_LicenseStartDate
            End Get
            Set(value As String)
                m_LicenseStartDate = value
            End Set
        End Property
        Private m_LicenseStartDate As String

        <DataMember(Name:="license_end_date")> _
        Public Property LicenseEndDate() As String
            Get
                Return m_LicenseEndDate
            End Get
            Set(value As String)
                m_LicenseEndDate = value
            End Set
        End Property
        Private m_LicenseEndDate As String

        <DataMember(Name:="vehicle_axle_count")> _
        Public Property VehicleAxleCount() As System.Nullable(Of Integer)
            Get
                Return m_VehicleAxleCount
            End Get
            Set(value As System.Nullable(Of Integer))
                m_VehicleAxleCount = value
            End Set
        End Property
        Private m_VehicleAxleCount As System.Nullable(Of Integer)

        <DataMember(Name:="mpg_city")> _
        Public Property MpgCity() As System.Nullable(Of Double)
            Get
                Return m_MpgCity
            End Get
            Set(value As System.Nullable(Of Double))
                m_MpgCity = value
            End Set
        End Property
        Private m_MpgCity As System.Nullable(Of Double)

        <DataMember(Name:="mpg_highway")> _
        Public Property MpgHighway() As System.Nullable(Of Double)
            Get
                Return m_MpgHighway
            End Get
            Set(value As System.Nullable(Of Double))
                m_MpgHighway = value
            End Set
        End Property
        Private m_MpgHighway As System.Nullable(Of Double)

        <DataMember(Name:="fuel_type")> _
        Public Property FuelType() As String
            Get
                Return m_FuelType
            End Get
            Set(value As String)
                m_FuelType = value
            End Set
        End Property
        Private m_FuelType As String

        <DataMember(Name:="height_inches")> _
        Public Property HeightInches() As System.Nullable(Of Double)
            Get
                Return m_HeightInches
            End Get
            Set(value As System.Nullable(Of Double))
                m_HeightInches = value
            End Set
        End Property
        Private m_HeightInches As System.Nullable(Of Double)

        <DataMember(Name:="weight_lb")> _
        Public Property WeightLb() As System.Nullable(Of Double)
            Get
                Return m_WeightLb
            End Get
            Set(value As System.Nullable(Of Double))
                m_WeightLb = value
            End Set
        End Property
        Private m_WeightLb As System.Nullable(Of Double)

    End Class
End Namespace
