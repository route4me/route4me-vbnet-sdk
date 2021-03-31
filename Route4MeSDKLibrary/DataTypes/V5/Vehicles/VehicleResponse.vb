Imports System.Runtime.Serialization

Namespace Route4MeSDK.DataTypes.V5

    ''' <summary>
    ''' Response for the endpoint /vehicles/license
    ''' </summary>
    <DataContract>
    Public NotInheritable Class VehicleResponse
        Inherits QueryTypes.GenericParameters

        ''' <summary>
        ''' Vehicle data
        ''' </summary>
        <DataMember(Name:="data", EmitDefaultValue:=False)>
        Public Property Data As DataVehicle

    End Class

    ''' <summary>
    ''' The structure of the property Data of the DataVehicle type object.
    ''' </summary>
    <DataContract>
    Public Class DataVehicle

        ''' <summary>
        ''' Vehicle object
        ''' </summary>
        <DataMember(Name:="vehicle", EmitDefaultValue:=False)>
        Public Property Vehicle As VehicleReduced
    End Class

    ''' <summary>
    ''' Reduced vehicle structure
    ''' </summary>
    <DataContract>
    Public Class VehicleReduced

        ''' <summary>
        ''' The vehicle ID
        ''' </summary>
        <DataMember(Name:="vehicle_id", EmitDefaultValue:=False)>
        Public Property VehicleId As String

        ''' <summary>
        ''' Vehicle alias
        ''' </summary>
        <DataMember(Name:="vehicle_alias", EmitDefaultValue:=False)>
        Public Property VehicleAlias As String

        ''' <summary>
        ''' Vehicle VIN
        ''' </summary>
        <DataMember(Name:="vehicle_vin", EmitDefaultValue:=False)>
        Public Property VehicleVin As String

        ''' <summary>
        ''' A license plate of the vehicle.
        ''' </summary>
        <DataMember(Name:="vehicle_license_plate", EmitDefaultValue:=False)>
        Public Property VehicleLicensePlate As String

        ''' <summary>
        ''' Vehicle maker brend.
        ''' <para>Available values</para>
        ''' "american coleman", "bmw", "chevrolet", "ford", "freightliner", "gmc",
        ''' <para>"hino", "honda", "isuzu", "kenworth", "mack", "mercedes-benz", "mitsubishi", </para>
        ''' "navistar", "nissan", "peterbilt", "renault", "scania", "sterling", "toyota",
        ''' <para>"volvo", "western star" </para>
        ''' </summary>
        <DataMember(Name:="vehicle_make", EmitDefaultValue:=False)>
        Public Property VehicleMake As String

        ''' <summary>
        ''' Vehicle model year
        ''' </summary>
        <DataMember(Name:="vehicle_model_year", EmitDefaultValue:=False)>
        Public Property VehicleModelYear As Integer?

        ''' <summary>
        ''' Vehicle model
        ''' </summary>
        <DataMember(Name:="vehicle_model", EmitDefaultValue:=False)>
        Public Property VehicleModel As String

        ''' <summary>
        ''' The year, vehicle was acquired
        ''' </summary>
        <DataMember(Name:="vehicle_year_acquired", EmitDefaultValue:=False)>
        Public Property VehicleYearAcquired As Integer?

        ''' <summary>
        ''' A cost of the new vehicle
        ''' </summary>
        <DataMember(Name:="vehicle_cost_new", EmitDefaultValue:=False)>
        Public Property VehicleCostNew As Double?

        ''' <summary>
        ''' If true, the vehicle was purchased new.
        ''' </summary>
        <DataMember(Name:="purchased_new", EmitDefaultValue:=False)>
        Public Property PurchasedNew As Boolean?

        ''' <summary>
        ''' Start date of the license
        ''' </summary>
        <DataMember(Name:="license_start_date", EmitDefaultValue:=False)>
        Public Property LicenseStartDate As String

        ''' <summary>
        ''' End date of the license
        ''' </summary>
        <DataMember(Name:="license_end_date", EmitDefaultValue:=False)>
        Public Property LicenseEndDate As String

        ''' <summary>
        ''' If equal to '1', the vehicle is operational.
        ''' </summary>
        <DataMember(Name:="is_operational", EmitDefaultValue:=False)>
        Public Property IsOsperational As Boolean?

    End Class
End Namespace
