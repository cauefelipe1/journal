class ApiConstants {
  static const String baseUrl = 'https://localhost:7043/api';
  //static const String baseUrl = 'https://10.0.2.2:7043/api';

  static final IdentityEndpoints identity = IdentityEndpoints();
  static final VehicleEndpoints vehicle = VehicleEndpoints();
}

class IdentityEndpoints {
  static const String _basePath = "${ApiConstants.baseUrl}/identity";
  final String login = "$_basePath/login";
  final String refreshToken = "$_basePath/refreshToken";
}

class VehicleEndpoints {
  static const String _basePath = "${ApiConstants.baseUrl}/vehicle";
  final String allBrands = "$_basePath/brands";
}
