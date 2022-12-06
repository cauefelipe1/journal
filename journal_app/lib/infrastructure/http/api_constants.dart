class ApiConstants {
  static const IdentityEndpoints identity = IdentityEndpoints();
  static const VehicleEndpoints vehicle = VehicleEndpoints();
}

class IdentityEndpoints {
  const IdentityEndpoints();

  static const String _basePath = "identity";
  final String login = "$_basePath/login";
  final String refreshToken = "$_basePath/refreshToken";
}

class VehicleEndpoints {
  const VehicleEndpoints();

  static const String _basePath = "vehicle";
  final String allBrands = "$_basePath/brands";
}
