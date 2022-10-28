class ApiConstants {
  static const String baseUrl = 'https://localhost:7043/api';

  static final IdentityEndpoints identity = IdentityEndpoints();
}

class IdentityEndpoints {
  static const String _basePath = "${ApiConstants.baseUrl}/identity";
  final String login = "$_basePath/login";
}
