class ApiConstants {
  static const IdentityEndpoints identity = IdentityEndpoints();
  static const VehicleEndpoints vehicle = VehicleEndpoints();
  static const VehicleEventsEndpoints vehicleEvents = VehicleEventsEndpoints();
}

class IdentityEndpoints {
  const IdentityEndpoints();

  static const String _basePath = "identity";
  final String login = "$_basePath/login";
  final String refreshToken = "$_basePath/refreshToken";
  final String userData = "$_basePath/userData";
  final String checkIfAuthenticated = "$_basePath/checkIfAuthenticated";
}

class VehicleEndpoints {
  const VehicleEndpoints();

  static const String _basePath = "vehicle";
  final String allBrands = "$_basePath/brands";
  final String driverVehicles = "$_basePath/by_main_driver";
}

class VehicleEventsEndpoints {
  const VehicleEventsEndpoints();

  static const String _basePath = "vehicle_event";
  final String vehicleEventsByVehicle = "$_basePath/by_vehicle";
}
